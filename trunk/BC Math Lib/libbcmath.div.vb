Partial Public Class libbcmath
	''' <summary>Divides two arbitrary precision numbers</summary>
	''' <param name="dividend">The big (base) number</param>
	''' <param name="divisor">The number to divide it by</param>
	''' <param name="scale">The minimum scale to use</param>
	Public Shared Function Divide(ByRef dividend As BCNum, ByRef divisor As BCNum, ByVal scale As Integer) As BCNum
		Dim quot, qval As New BCNum	' @return qout
		Dim num1, num2, mval As List(Of Byte) ' string
		Dim ptr1, ptr2, n2ptr, qptr As Integer ' pointers
		Dim scale1, val As Integer
		Dim len1, len2, scale2, qdigits, extra, count As Integer
		Dim qdig, borrow, carry As Integer
		Dim zero As Boolean	' char
		Dim norm, qguess As Byte
		'var ptrs; // return object from one_mul

		' Test for divide by zero. (return failure)
		If IsZero(divisor) Then Return New BCNum With {.length = 1, .value = New List(Of Byte)(New Byte() {1}), .sign = MINUS} ' return -1

		' Test for zero divide by anything (return zero)
		If IsZero(dividend) Then Return NewNum(1, scale1)

		' Test for n1 equals n2 (return 1 as n1 nor n2 are zero)
		' /*
		'If DoCompare(n1, n2) = 0 Then
		'	quot = NewNum(1, scale)
		'	quot(0) = 1
		'	Return quot
		'End If
		' */

		' Test for divide by 1.  If it is we must truncate.
		If IsOne(divisor) Then
			qval = NewNum(dividend.length, scale)	' qval = bc_new_num (n1->n_len, scale);
			qval.sign = If(dividend.sign = divisor.sign, PLUS, MINUS)
			memset(qval.value, dividend.length, 0, scale)	' memset (&qval->n_value[n1->n_len],0,scale);
			' memcpy(qval->n_value, n1->n_value, n1->n_len + MIN(n1->n_scale,scale));
			memcpy(qval.value, 0, dividend.value, 0, dividend.length + Math.Min(dividend.scale, scale))
			' returning here should work, but not in the C source
			Return qval
		End If

		' Set up the divide.  Move the decimal point on n1 by n2's scale.
		' Remember, zeros on the end of num2 are wasted effort for dividing.
		scale2 = divisor.scale ' scale2 = n2->n_scale;
		n2ptr = divisor.length + scale2 - 1	' n2ptr = (unsigned char *) n2.n_value+n2.n_len+scale2-1;
		While scale2 > 0 And divisor(n2ptr) = 0
			n2ptr -= 1
			scale2 -= 1
		End While

		len1 = dividend.length + scale2
		scale1 = dividend.scale - scale2
		If (scale1 < scale) Then : extra = scale - scale1
		Else : extra = 0
		End If

		num1 = safe_emalloc(1, dividend.length + dividend.scale, extra + 2)	' num1 = (unsigned char *) safe_emalloc (1, n1.n_len+n1.n_scale, extra+2);
		'if (num1 === null) {
		'	libbcmath.bc_out_of_memory();
		'}
		memset(num1, 0, 0, dividend.length + dividend.scale + extra + 2) ' memset (num1, 0, n1->n_len+n1->n_scale+extra+2);
		memcpy(num1, 1, dividend.value, 0, dividend.length + dividend.scale)	' memcpy (num1+1, n1.n_value, n1.n_len+n1.n_scale);

		len2 = divisor.length + scale2 ' len2 = n2->n_len + scale2;
		num2 = safe_emalloc(1, len2, 1)	' num2 = (unsigned char *) safe_emalloc (1, len2, 1);
		'if (num2 === null) {
		'	libbcmath.bc_out_of_memory();
		'}
		memcpy(num2, 0, divisor.value, 0, len2)	' memcpy (num2, n2.n_value, len2);
		num2(len2) = 0 ' *(num2+len2) = 0;
		n2ptr = 0 ' n2ptr = num2;

		While num2(n2ptr) = 0 ' while (*n2ptr == 0)
			n2ptr += 1
			len2 -= 1
		End While

		' Calculate the number of quotient digits.
		If len2 > len1 + scale Then
			qdigits = scale + 1
			zero = True
		Else
			zero = False
			If (len2 > len1) Then : qdigits = scale + 1	' One for the zero integer part.
			Else : qdigits = len1 - len2 + scale + 1
			End If
		End If

		' Allocate and zero the storage for the quotient.
		qval = NewNum(qdigits - scale, scale) ' qval = bc_new_num (qdigits-scale,scale);
		memset(qval.value, 0, 0, qdigits) ' memset (qval->n_value, 0, qdigits);

		' Allocate storage for the temporary storage mval.
		mval = safe_emalloc(1, len2, 1)	' mval = (unsigned char *) safe_emalloc (1, len2, 1);
		'if (mval === null) {
		'	libbcmath.bc_out_of_memory();
		'}

		' Now for the full divide algorithm.
		If Not zero Then
			'  Normalize
			' //norm = libbcmath.cint(10 / (libbcmath.cint(n2.n_value[n2ptr]) + 1)); //norm =  10 / ((int)*n2ptr + 1);
			norm = CByte(Math.Floor(10 / (divisor(n2ptr) + 1)))	' norm =  10 / ((int)*n2ptr + 1);
			If norm <> 1 Then
				OneMult(num1, 0, len1 + scale1 + extra + 1, norm, num1, 0) ' libbcmath._one_mult(num1, len1+scale1+extra+1, norm, num1);
				' UNDONE: @CHECK Is the pointer affected by the call? if so, maybe need to adjust points on return?
			End If

			' Initialize divide loop.
			qdig = 0
			If len2 > len1 Then : qptr = len2 - len1 ' qptr = (unsigned char *) qval.n_value+len2-len1;
			Else : qptr = 0	' qptr = (unsigned char *) qval.n_value;
			End If

			' Loop
			Do While qdig <= len1 + scale - len2
				' Calculate the quotient digit guess.
				If divisor(n2ptr) = num1(qdig) Then : qguess = 9
				Else : qguess = CByte(Math.Floor((num1(qdig) * 10 + num1(qdig + 1)) / divisor(n2ptr)))
				End If
				' Test qguess.
				' if (n2ptr[1]*qguess > (num1[qdig]*10 + num1[qdig+1] - *n2ptr*qguess)*10 + num1[qdig+2]) {
				If divisor(n2ptr + 1) * qguess > (num1(qdig) * 10 + num1(qdig + 1) - divisor(n2ptr) * qguess) * 10 + num1(qdig + 2) Then
					qguess -= CByte(1)
					' And again.
					' if (n2ptr[1]*qguess > (num1[qdig]*10 + num1[qdig+1] - *n2ptr*qguess)*10 + num1[qdig+2])
					If divisor(n2ptr + 1) * qguess > (num1(qdig) * 10 + num1(qdig + 1) - divisor(n2ptr) * qguess) * 10 + num1(qdig + 2) Then
						qguess -= CByte(1)
					End If
				End If

				' Multiply and subtract.
				borrow = 0
				If qguess <> 0 Then
					mval(0) = 0	' UNDONE: @CHECK is this to fix ptr2 < 0? ' *mval = 0; 
					OneMult(divisor.value, n2ptr, len2, qguess, mval, 1)	' _one_mult (n2ptr, len2, qguess, mval+1)

					ptr1 = qdig + len2 ' (unsigned char *) num1+qdig+len2;
					ptr2 = len2	' (unsigned char *) mval+len2;

					' UNDONE: @CHECK Does a negative pointer return null?
					' ptr2 can be < 0 here as ptr1 = len2, thus count < len2+1 will always fail ?
					For count = 0 To len2
						If ptr2 < 0 Then
							'val = libbcmath.cint(num1[ptr1]) - 0 - borrow;    //val = (int) *ptr1 - (int) *ptr2-- - borrow;
							val = num1(ptr1) - 0 - borrow ' val = (int) *ptr1 - (int) *ptr2-- - borrow;
						Else
							'val = libbcmath.cint(num1[ptr1]) - libbcmath.cint(mval[ptr2--]) - borrow;    //val = (int) *ptr1 - (int) *ptr2-- - borrow;
							val = num1(ptr1) - mval(ptr2) - borrow ' val = (int) *ptr1 - (int) *ptr2-- - borrow;
							ptr2 -= 1
						End If
						If val < 0 Then
							val += 10
							borrow = 1
						Else : borrow = 0
						End If
						num1(ptr1) = CByte(val)
						ptr1 -= 1
					Next
				End If

				' Test for negative result.
				If borrow = 1 Then
					qguess -= CByte(1)
					ptr1 = qdig + len2 ' (unsigned char *) num1+qdig+len2;
					ptr2 = len2 - 1	' (unsigned char *) n2ptr+len2-1;
					carry = 0
					For count = 0 To len2 - 1
						If ptr2 < 0 Then
							' val = libbcmath.cint(num1[ptr1]) + 0 + carry; //val = (int) *ptr1 + (int) *ptr2-- + carry;
							val = num1(ptr1) + 0 + carry ' val = (int) *ptr1 + (int) *ptr2-- + carry;
						Else
							' val = libbcmath.cint(num1[ptr1]) + libbcmath.cint(n2.n_value[ptr2--]) + carry; //val = (int) *ptr1 + (int) *ptr2-- + carry;
							val = num1(ptr1) + divisor(ptr2) + carry ' val = (int) *ptr1 + (int) *ptr2-- + carry;
							ptr2 -= 1
						End If
						If val > 9 Then
							val -= 10
							carry = 1
						Else : carry = 0
						End If
						num1(ptr1) = CByte(val)	' *ptr1-- = val;
						ptr1 -= 1
					Next
					If carry = 1 Then
						' UNDONE: @CHECKs present in this block
						' num1[ptr1] = cint((num1[ptr1] + 1) % 10);  // *ptr1 = (*ptr1 + 1) % 10; // @CHECK
						num1(ptr1) = CByte((num1(ptr1) + 1) Mod 10)	' *ptr1 = (*ptr1 + 1) % 10; // @CHECK
					End If
				End If

				' We now know the quotient digit.
				qval(qptr) = qguess	' *qptr++ =  qguess;
				qptr += 1
				qdig += 1
			Loop
		End If

		' Clean up and return the number.
		qval.sign = If(dividend.sign = divisor.sign, libbcmath.PLUS, libbcmath.MINUS)
		If IsZero(qval) Then qval.sign = PLUS
		RemoveLeadingZeros(qval)

		' Return the value after the "hard work"
		Return qval
	End Function

	''' <summary>
	''' Some utility routines for the divide:  First a one digit multiply.
	''' NUM (with SIZE digits) is multiplied by DIGIT and the result is
	''' placed into RESULT.  It is written so that NUM and RESULT can be
	''' the same pointers.
	''' </summary>
	''' <param name="num">A reference to the array to multiply</param>
	''' <param name="n_ptr">The offset of the first array</param>
	''' <param name="size">Number of iterations</param>
	''' <param name="digit">The digit to multiply it by</param>
	''' <param name="result">A reference to the result array</param>
	''' <param name="r_ptr">The offset of the second array</param>
	Protected Shared Sub OneMult(ByRef num As List(Of Byte), ByVal n_ptr As Integer, ByVal size As Integer, _
			 ByVal digit As Byte, ByRef result As List(Of Byte), ByVal r_ptr As Integer)
		Dim carry, value As Byte
		Dim nptr, rptr As Integer ' pointers
		If digit = 0 Then : memset(result, r_ptr, 0, size) ' memset (result, 0, size);
		ElseIf digit = 1 Then : memcpy(result, r_ptr, num, n_ptr, size)	' memcpy (result, num, size);
		Else
			' Initialize
			nptr = n_ptr + size - 1	' nptr = (unsigned char *) (num+size-1);
			rptr = r_ptr + size - 1	' rptr = (unsigned char *) (result+size-1);
			carry = 0

			While size > 0
				size -= 1
				value = num(nptr) * digit + carry ' value = *nptr-- * digit + carry;
				nptr -= 1
				result(rptr) = value Mod BASE	' @CHECK cint //*rptr-- = value % BASE;
				rptr -= 1
				carry = CByte(Math.Floor(value / BASE))	  ' @CHECK cint //carry = value / BASE;
			End While

			If carry <> 0 Then result(rptr) = carry
		End If
	End Sub
End Class
