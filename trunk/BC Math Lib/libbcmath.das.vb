Partial Public Class libbcmath
	''' <summary>Performs a raw addition iteration</summary>
	''' <param name="n1">The base number</param>
	''' <param name="n2">The addend to add to the base</param>
	''' <param name="scale_min">The smallest scale to use</param>
	Private Shared Function DoAdd(ByVal n1 As BCNum, ByVal n2 As BCNum, Optional ByVal scale_min As Integer = 0) As BCNum
		Dim sum As New BCNum
		Dim sum_scale, sum_digits As Integer
		Dim n1ptr, n2ptr, sumptr As Integer
		Dim n1bytes, n2bytes As Integer
		Dim carry, tmp As Byte

		' Prepare sum.
		sum_scale = Math.Max(n1.scale, n2.scale)
		sum_digits = Math.Max(n1.length, n2.length) + 1
		sum = NewNum(sum_digits, Math.Max(sum_scale, scale_min))


		' Not needed? [Still in C++]
		'if (scale_min > sum_scale) {
		'	sumptr = (char *) (sum->n_value + sum_scale + sum_digits);
		'	for (count = scale_min - sum_scale; count > 0; count--) {
		'		*sumptr++ = 0;
		'	}
		'}

		' Start with the fraction part. Initialize the pointers.
		n1bytes = n1.scale
		n2bytes = n2.scale
		n1ptr = n1.length + n1bytes - 1
		n2ptr = n2.length + n2bytes - 1
		sumptr = sum_scale + sum_digits - 1

		' Add the fraction part. First copy the longer fraction (ie when adding 1.2345 to 1 we know .2345 is correct already) .
		If n1bytes > n2bytes Then ' n1 has more decimals then n2
			While n1bytes > n2bytes
				sum(sumptr) = n1(n1ptr)
				sumptr -= 1
				n1ptr -= 1
				n1bytes -= 1
			End While
		ElseIf n2bytes > n1bytes Then ' n2 has more decimals then n1
			While n2bytes > n1bytes
				sum(sumptr) = n2(n2ptr)
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
			tmp = n1(n1ptr) + n2(n2ptr) + carry	' *sumptr = *n1ptr-- + *n2ptr-- + carry;
			n1ptr -= 1
			n2ptr -= 1

			If tmp >= BASE Then
				carry = 1
				tmp -= BASE	' subtract 10, add a carry
			Else : carry = 0
			End If
			sum(sumptr) = tmp
			sumptr -= 1
			n1bytes -= 1
			n2bytes -= 1
		End While

		' Now add carry the [rest of the] longer integer part.
		If n1bytes = 0 Then
			' n2 is a bigger number then n1
			While n2bytes > 0
				n2bytes -= 1
				tmp = n2(n2ptr) + carry	' *sumptr = *n2ptr-- + carry;
				n2ptr -= 1
				If tmp > BASE Then
					carry = 1
					tmp -= BASE
				Else : carry = 0
				End If
				sum(sumptr) = tmp
				sumptr -= 1
			End While
		Else
			' n1 is bigger than n2
			While n1bytes > 0
				n1bytes -= 1
				tmp = n1(n1ptr) + carry	' *sumptr = *n1ptr-- + carry;
				n1ptr -= 1
				If tmp > BASE Then
					carry = 1
					tmp -= BASE
				Else : carry = 0
				End If
				sum(sumptr) = tmp
				sumptr -= 1
			End While
		End If

		' Set final carry.
		If carry = 1 Then
			sum(sumptr) += CByte(1)	' *sumptr += 1;
		End If

		' Adjust sum and return.
		RemoveLeadingZeros(sum)
		Return sum
	End Function

	''' <summary>Performs a raw subtraction iteration</summary>
	''' <param name="n1">The big number to subtract from</param>
	''' <param name="n2">The small number to subtract</param>
	''' <param name="scale_min">The minimum scale to use</param>
	Private Shared Function DoSub(ByVal n1 As BCNum, ByVal n2 As BCNum, Optional ByVal scale_min As Integer = 0) As BCNum
		Dim diff As New BCNum
		Dim diff_scale, diff_len As Integer
		Dim min_scale, min_len As Integer
		Dim n1ptr, n2ptr, diffptr As Integer
		Dim borrow, count, val As Integer

		' Allocate temporary storage.
		diff_len = Math.Max(n1.length, n2.length)
		diff_scale = Math.Max(n1.scale, n2.scale)
		min_len = Math.Min(n1.length, n2.length)
		min_scale = Math.Min(n1.scale, n2.scale)
		diff = NewNum(diff_len, Math.Max(diff_scale, scale_min))

		' Not needed? [Still in JavaScript and C++]
		' Zero extra digits made by scale_min
		'if (scale_min > diff_scale) {
		'	diffptr = (char *) (diff->n_value + diff_len + diff_scale);
		'	for (count = scale_min - diff_scale; count > 0; count--) {
		'		*diffptr++ = 0;
		'	}
		'}

		' Initialize the subtract.
		n1ptr = n1.length + n1.scale - 1
		n2ptr = n2.length + n2.scale - 1
		diffptr = diff_len + diff_scale - 1

		' Subtract the numbers
		borrow = 0

		' Take care of the longer scaled number.
		If n1.scale <> min_scale Then ' n1 has the longer scale
			For count = n1.scale - min_scale To 1 Step -1
				diff(diffptr) = n1(n1ptr) ' *diffptr-- = *n1ptr--;
				diffptr -= 1
				n1ptr -= 1
			Next
		Else ' n2 has the longer scale
			For count = n2.scale - min_scale To 1 Step -1
				val = n2(n2ptr) - borrow ' val = - *n2ptr-- - borrow;
				n2ptr -= 1
				If val < 0 Then
					val += BASE
					borrow = 1
				Else
					borrow = 0
					diff(diffptr) = CByte(val) ' *diffptr-- = val;
					diffptr -= 1
				End If
			Next
		End If

		' Now do the equal length scale and integer parts.
		For count = 0 To min_len + min_scale - 1
			val = n1(n1ptr) - n2(n2ptr) - borrow ' val = *n1ptr-- - *n2ptr-- - borrow;
			n1ptr -= 1
			n2ptr -= 1
			If val < 0 Then
				val += BASE
				borrow = 1
			Else : borrow = 0
			End If
			diff(diffptr) = CByte(val) ' *diffptr-- = val;
			diffptr -= 1
		Next

		' If n1 has more digits then n2, we now do that subtract.
		If diff_len <> min_len Then
			For count = diff_len - min_len To 1 Step -1
				val = n1(n1ptr) - borrow ' val = *n1ptr-- - borrow;
				n1ptr -= 1
				If val < 0 Then
					val += BASE
					borrow = 1
				Else : borrow = 0
				End If
				diff(diffptr) = CByte(val)
				diffptr -= 1
			Next
		End If

		' Clean up and return.
		RemoveLeadingZeros(diff)
		Return diff
	End Function
End Class
