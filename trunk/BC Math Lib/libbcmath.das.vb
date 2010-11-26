Partial Public Class libbcmath
	''' <summary>Performs a raw addition iteration</summary>
	''' <param name="n1">The base number</param>
	''' <param name="n2">The addend to add to the base</param>
	''' <param name="scale_min">The smallest scale to use</param>
	Protected Shared Function DoAdd(ByRef n1 As BCNum, ByRef n2 As BCNum, Optional ByVal scale_min As Integer = 0) As BCNum
		Dim sum As New BCNum
		Dim sum_scale, sum_digits As Integer
		Dim n1ptr, n2ptr, sumptr As Integer
		Dim n1bytes, n2bytes As Integer
		Dim carry, tmp As Byte

		' Prepare sum.
		sum_scale = Math.Max(n1.scale, n2.scale)
		sum_digits = Math.Max(n1.length, n2.length) + 1
		sum = new_num(sum_digits, Math.Max(sum_scale, scale_min))


		'/* Not needed? [Still in C++]
		'if (scale_min > sum_scale) {
		'	sumptr = (char *) (sum->n_value + sum_scale + sum_digits);
		'	for (count = scale_min - sum_scale; count > 0; count--) {
		'		*sumptr++ = 0;
		'	}
		'}
		'*/

		' Start with the fraction part. Initialize the pointers.
		n1bytes = n1.scale
		n2bytes = n2.scale
		n1ptr = n1.length + n1bytes - 1
		n2ptr = n2.length + n2bytes - 1
		sumptr = sum_scale + sum_digits - 1

		' Add the fraction part. First copy the longer fraction (ie when adding 1.2345 to 1 we know .2345 is correct already) .
		If n1bytes > n2bytes Then ' n1 has more decimals then n2
			While n1bytes > n2bytes
				sum.value(sumptr) = n1.value(n1ptr)
				sumptr -= 1
				n1ptr -= 1
				n1bytes -= 1
			End While
		ElseIf n2bytes > n1bytes Then ' n2 has more decimals then n1
			While n2bytes > n1bytes
				sum.value(sumptr) = n2.value(n2ptr)
				sumptr -= 1
				n2ptr -= 1
				n2bytes -= 1
			End While
		End If

		' Now add the remaining fraction part and equal size integer parts.
		n1bytes += n1.length
		n2bytes += n2.length
		carry = 0
		While n1bytes > 0 And n2bytes > 0
			' Add the two numbers together
			tmp = n1.value(n1ptr) + n2.value(n2ptr) + carry	' *sumptr = *n1ptr-- + *n2ptr-- + carry;
			n1ptr -= 1
			n2ptr -= 1

			If tmp >= BASE Then
				carry = 1
				tmp -= BASE	' subtract 10, add a carry
			Else : carry = 0
			End If
			sum.value(sumptr) = tmp
			sumptr -= 1
			n1bytes -= 1
			n2bytes -= 1
		End While

		' Now add carry the [rest of the] longer integer part.
		If n1bytes = 0 Then
			' n2 is a bigger number then n1
			While n2bytes > 0
				n2bytes -= 1
				tmp = n2.value(n2ptr) + carry ' *sumptr = *n2ptr-- + carry;
				n2ptr -= 1
				If tmp > BASE Then
					carry = 1
					tmp -= BASE
				Else : carry = 0
				End If
				sum.value(sumptr) = tmp
				sumptr -= 1
			End While
		Else
			' n1 is bigger than n2
			While n1bytes > 0
				n1bytes -= 1
				tmp = n1.value(n1ptr) + carry ' *sumptr = *n1ptr-- + carry;
				n1ptr -= 1
				If tmp > BASE Then
					carry = 1
					tmp -= BASE
				Else : carry = 0
				End If
				sum.value(sumptr) = tmp
				sumptr -= 1
			End While
		End If

		' Set final carry.
		If carry = 1 Then
			sum.value(sumptr) += CByte(1) ' *sumptr += 1;
		End If

		' Adjust sum and return.
		RemoveLeadingZeros(sum)
		Return sum
	End Function

	''' <summary>Performs a raw subtraction iteration</summary>
	''' <param name="minuend">The big number to subtract from</param>
	''' <param name="subtrahend">The small number to subtract</param>
	''' <param name="scale_min">The minimum scale to use</param>
	Protected Shared Function DoSub(ByRef minuend As BCNum, ByRef subtrahend As BCNum, Optional ByVal scale_min As Integer = 0) As BCNum
		'var diff; //bc_num
		'var diff_scale, diff_len; // int
		'var min_scale, min_len; // int
		'var n1ptr, n2ptr, diffptr; // int
		'var borrow, count, val; // int

		'// Allocate temporary storage.
		'diff_len    = libbcmath.MAX(n1.n_len,   n2.n_len);
		'diff_scale  = libbcmath.MAX(n1.n_scale, n2.n_scale);
		'min_len     = libbcmath.MIN(n1.n_len,   n2.n_len);
		'min_scale   = libbcmath.MIN(n1.n_scale, n2.n_scale);
		'diff        = libbcmath.bc_new_num(diff_len, libbcmath.MAX(diff_scale, scale_min));

		'/* Not needed?
		'// Zero extra digits made by scale_min.
		'if (scale_min > diff_scale) {
		'	diffptr = (char *) (diff->n_value + diff_len + diff_scale);
		'	for (count = scale_min - diff_scale; count > 0; count--) {
		'		*diffptr++ = 0;
		'	}
		'}
		'*/

		'// Initialize the subtract.
		'n1ptr   = (n1.n_len + n1.n_scale -1);
		'n2ptr   = (n2.n_len + n2.n_scale -1);
		'diffptr = (diff_len + diff_scale -1);

		'// Subtract the numbers.
		'borrow = 0;

		'// Take care of the longer scaled number.
		'if (n1.n_scale != min_scale) {
		'	// n1 has the longer scale
		'	for (count = n1.n_scale - min_scale; count > 0; count--) {
		'		diff.n_value[diffptr--] = n1.n_value[n1ptr--];
		'		// *diffptr-- = *n1ptr--;
		'	}
		'} else {
		'	// n2 has the longer scale
		'	for (count = n2.n_scale - min_scale; count > 0; count--) {
		'		val = 0 - n2.n_value[n2ptr--] - borrow;
		'		//val = - *n2ptr-- - borrow;
		'		if (val < 0) {
		'			val += libbcmath.BASE;
		'			borrow = 1;
		'		} else {
		'			borrow = 0;
		'			diff.n_value[diffptr--] = val;
		'			//*diffptr-- = val;
		'		}
		'	}
		'}

		'// Now do the equal length scale and integer parts.
		'for (count = 0; count < min_len + min_scale; count++) {
		'	val = n1.n_value[n1ptr--] - n2.n_value[n2ptr--] - borrow;
		'	//val = *n1ptr-- - *n2ptr-- - borrow;
		'	if (val < 0) {
		'		val += libbcmath.BASE;
		'		borrow = 1;
		'	} else {
		'		borrow = 0;
		'	}
		'	diff.n_value[diffptr--] = val;
		'	//*diffptr-- = val;
		'}

		'// If n1 has more digits then n2, we now do that subtract.
		'if (diff_len != min_len) {
		'	for (count = diff_len - min_len; count > 0; count--) {
		'		val = n1.n_value[n1ptr--] - borrow;
		'		// val = *n1ptr-- - borrow;
		'		if (val < 0) {
		'			val += libbcmath.BASE;
		'			borrow = 1;
		'		} else {
		'			borrow = 0;
		'		}
		'		diff.n_value[diffptr--] = val;
		'	}
		'}

		'// Clean up and return.
		'libbcmath._bc_rm_leading_zeros(diff);
		'return diff;
	End Function
End Class
