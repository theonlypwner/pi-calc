Partial Public Class libbcmath
	Public Const MUL_BASE_DIGITS As Byte = 80
	Public Const MUL_SMALL_DIGITS As Byte = CByte(MUL_BASE_DIGITS / 4)	' #define MUL_SMALL_DIGITS mul_base_digits/4

	''' <summary>Multiplies two arbitrary precision numbers</summary>
	''' <param name="factor">The first factor</param>
	''' <param name="multiplier">The second factor</param>
	''' <param name="scale">The minimum scale to use</param>
	Public Shared Function Multiply(ByVal factor As BCNum, ByVal multiplier As BCNum, Optional ByVal scale As Integer = defaultScale) As BCNum
		Dim pval As New BCNum
		Dim len1, len2 As Integer
		Dim full_scale, prod_scale As Integer

		' Initialize things
		len1 = factor.length + factor.scale
		len2 = multiplier.length + multiplier.scale
		full_scale = factor.scale + multiplier.scale
		prod_scale = Math.Min(full_scale, Math.Max(scale, Math.Max(factor.scale, multiplier.scale)))

		' //pval = init_num() ' allow pass by ref
		' Do the multiply
		pval = RecMul(factor, len1, multiplier, len2, full_scale)

		' Assign to prod and clean up the number.
		pval.sign = If(factor.sign = multiplier.sign, PLUS, MINUS)
		' //pval.n_value = pval.n_ptr; // @FIX
		pval.length = len2 + len1 + 1 - full_scale
		pval.scale = prod_scale
		RemoveLeadingZeros(pval)
		If IsZero(pval) Then pval.sign = PLUS
		' //bc_free_num (prod);
		Return pval
	End Function

	Protected Shared Function NewSubNum(ByVal length As Integer, ByVal scale As Integer, ByRef value As List(Of Byte)) As BCNum
		Dim temp As New BCNum
		temp.length = length
		temp.scale = scale
		temp.value = value
		Return temp
	End Function

	''' <summary>Simple multiplication</summary>
	''' <param name="n1">First number</param>
	''' <param name="n1len">Length of first number</param>
	''' <param name="n2">Second number</param>
	''' <param name="n2len">Length of second number</param>
	Private Shared Function SimpMul(ByRef n1 As BCNum, ByVal n1len As Integer, ByRef n2 As BCNum, ByVal n2len As Integer) As BCNum
		Dim prod As New BCNum
		Dim n1ptr, n2ptr, pvptr As Integer ' char *n1ptr, *n2ptr, *pvptr;
		Dim n1end, n2end As Integer	' char *n1end, *n2end; ' To the end of n1 and n2.
		Dim indx, sum, prodlen As Integer ' int indx, sum, prodlen;

		prodlen = n1len + n2len + 1

		prod = NewNum(prodlen, 0)

		n1end = n1len - 1 ' (char *) (n1->n_value + n1len - 1);
		n2end = n2len - 1 ' (char *) (n2->n_value + n2len - 1);
		pvptr = prodlen - 1	' (char *) ((*prod)->n_value + prodlen - 1);
		sum = 0

		' Here is the loop...
		For indx = 0 To prodlen - 2
			n1ptr = n1end - Math.Max(0, indx - n2len + 1) ' (char *) (n1end - MAX(0, indx-n2len+1));
			n2ptr = n2end - Math.Min(indx, n2len - 1) ' (char *) (n2end - MIN(indx, n2len-1));
			While n1ptr >= 0 And n2ptr <= n2end
				sum += n1(n1ptr) * n2(n2ptr) ' sum += *n1ptr-- * *n2ptr++;
				n1ptr -= 1
				n2ptr += 1
			End While
			prod(pvptr) = CByte(Math.Floor(sum Mod libbcmath.BASE))	' *pvptr-- = sum % BASE;
			pvptr -= 1
			sum = CInt(Math.Floor(sum / BASE))	' sum = sum / BASE;
		Next
		prod(pvptr) = CByte(sum)	' *pvptr = sum;
		Return prod
	End Function

	''' <summary>
	''' A special adder/subtractor for the recursive divide and conquer
	''' multiply algorithm.  Note: if sub is called, accum must
	''' be larger that what is being subtracted.  Also, accum and val
	''' must have n_scale = 0.  (e.g. they must look like integers. *)
	''' </summary>
	''' <param name="accum">This is passed by reference and contains the result.</param>
	''' <param name="val">The other number that will be used in calculation, but be unaltered</param>
	''' <param name="shift">Offset of digits to skip</param>
	''' <param name="subtract">Subtract the numers</param>
	Protected Shared Sub Shift_AddSub(ByRef accum As BCNum, ByVal val As BCNum, ByVal shift As Integer, ByVal subtract As Boolean)
		Dim accp, valp As Integer ' signed char *accp, *valp;
		Dim count As Integer ' int  count, carry;
		Dim carry As Byte

		count = val.length
		If val(0) = 0 Then count -= 1

		' assert (accum->n_len+accum->n_scale >= shift+count);
		If accum.length + accum.scale < shift + count Then
			Throw New Exception("len + scale < shift + count") ' I (and the original coder) think(s) that is what assert does
		End If

		' Set up pointers and others
		accp = accum.length + accum.scale - shift - 1 ' (signed char *)(accum->n_value + accum->n_len + accum->n_scale - shift - 1);
		valp = 1 ' (signed char *)(val->n_value + val->n_len - 1);
		val.length = 1 ' same as above (valp = val.length = 1)
		carry = 0
		If subtract Then
			' Subtraction; carry is really borrow.
			While count > 0
				count -= 1
				accum(accp) -= val(valp) + carry	' *accp -= *valp-- + carry;
				valp -= 1
				If accum(accp) < 0 Then	' if (*accp < 0)
					carry = 1
					accum(accp) += BASE	' *accp-- += BASE;
					' accp -= 1 ' see below
				Else
					carry = 0
					' accp -= 1 ' see below
				End If
				accp -= 1 ' either way, it needs to decrement this
			End While
			While carry > 0
				accum(accp) -= carry ' *accp -= carry;
				If (accum(accp) < 0) Then	' if (*accp < 0)
					accum(accp) += BASE	' *accp-- += BASE;
					accp -= 1
				Else
					carry = 0
				End If
			End While
		Else
			' Addition
			While count > 0
				count -= 1
				accum(accp) += CByte(val(valp) + carry)	' *accp += *valp-- + carry;
				valp -= 1
				If accum(accp) > BASE - 1 Then ' if (*accp > (BASE-1))
					carry = 1
					accum(accp) -= BASE	' *accp-- -= BASE;
					' accp -=1 ' see below
				Else
					carry = 0
					' accp -= 1 ' see below
				End If
				accp -= 1 ' either way, it needs to be decremented
			End While
			While carry > 0
				accum(accp) += carry ' *accp += carry;
				If accum(accp) > BASE - 1 Then ' if (*accp > (BASE-1))
					accum(accp) -= BASE	' *accp-- -= BASE;
					accp -= 1
				Else : carry = 0
				End If
			End While
		End If
	End Sub

	''' <summary>
	''' Recursive divide and conquer multiply algorithm.
	''' Based on
	''' Let u = u0 + u1*(b^n)
	''' Let v = v0 + v1*(b^n)
	''' Then uv = (B^2n+B^n)*u1*v1 + B^n*(u1-u0)*(v0-v1) + (B^n+1)*u0*v0
	''' B is the base of storage, number of digits in u1, u0 close to equal.
	''' </summary>
	''' <param name="u">The first number</param>
	''' <param name="ulen">Length of the first number</param>
	''' <param name="v">The second number</param>
	''' <param name="vlen">The length of the second number</param>
	''' <param name="full_scale">The full scale</param>
	Private Shared Function RecMul(ByVal u As BCNum, ByVal ulen As Integer, ByVal v As BCNum, ByVal vlen As Integer, ByVal full_scale As Integer) As BCNum
		Dim prod As New BCNum ' @return
		Dim u0, u1, v0, v1 As New BCNum
		Dim u0len, v0len As Integer
		Dim m1, m2, m3, d1, d2 As New BCNum
		Dim n, prodlen As Integer, m1zero As Boolean
		Dim d1len, d2len As Integer

		' Base case?
		If (ulen + vlen) < libbcmath.MUL_BASE_DIGITS Or ulen < libbcmath.MUL_SMALL_DIGITS Or vlen < libbcmath.MUL_SMALL_DIGITS Then
			Return SimpMul(u, ulen, v, vlen) ' , full_scale)
		End If

		' Calculate n -- the u and v split point in digits.
		n = CInt(Math.Floor((Math.Max(ulen, vlen) + 1) / 2))

		' Split u and v.
		If ulen < n Then
			u1 = InitNum()	' u1 = bc_copy_num (BCG(_zero_));
			u0 = NewSubNum(ulen, 0, u.value)
		Else
			u1 = NewSubNum(ulen - n, 0, u.value)
			u0 = NewSubNum(n, 0, u.value.GetRange(ulen - n, u.value.Count - ulen + n))
		End If
		If vlen < n Then
			v1 = InitNum()	' bc_copy_num (BCG(_zero_));
			v0 = NewSubNum(vlen, 0, v.value)
		Else
			v1 = NewSubNum(vlen - n, 0, v.value)
			v0 = NewSubNum(n, 0, v.value.GetRange(vlen - n, v.value.Count - ulen + n))
		End If
		RemoveLeadingZeros(u1)
		RemoveLeadingZeros(u0)
		u0len = u0.length
		RemoveLeadingZeros(v1)
		RemoveLeadingZeros(v0)
		v0len = v0.length

		m1zero = IsZero(u1) Or IsZero(v1)

		' Calculate sub results ...
		d1 = InitNum()	' needed?
		d2 = InitNum()	' needed?
		d1 = DoSub(u1, u0, 0)
		d1len = d1.length

		d2 = DoSub(v0, v1, 0)
		d2len = d2.length

		' Do recursive multiplies and shifted adds.
		If m1zero Then : m1 = InitNum()	' bc_copy_num (BCG(_zero_));
		Else : m1 = RecMul(u1, u1.length, v1, v1.length, 0)
		End If
		If IsZero(d1) Or IsZero(d2) Then : m2 = InitNum() ' bc_copy_num (BCG(_zero_));
		Else : m2 = RecMul(d1, d1len, d2, d2len, 0)
		End If

		If IsZero(u0) Or IsZero(v0) Then : m3 = InitNum()	' bc_copy_num (BCG(_zero_));
		Else : m3 = RecMul(u0, u0.length, v0, v0.length, 0)
		End If

		' Initialize product
		prodlen = ulen + vlen + 1
		prod = NewNum(prodlen, 0)

		If Not m1zero Then
			Shift_AddSub(prod, m1, 2 * n, False)
			Shift_AddSub(prod, m1, 2 * n, False)
			Shift_AddSub(prod, m1, n, False)
		End If
		Shift_AddSub(prod, m3, n, False)
		Shift_AddSub(prod, m3, 0, False)
		Shift_AddSub(prod, m2, n, d1.sign <> d2.sign)

		Return prod
		' Now clean up?
		' bc_free_num (&u1);
		' bc_free_num (&u0);
		' bc_free_num (&v1);
		' bc_free_num (&m1);
		' bc_free_num (&v0);
		' bc_free_num (&m2);
		' bc_free_num (&m3);
		' bc_free_num (&d1);
		' bc_free_num (&d2);
	End Function
End Class
