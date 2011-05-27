using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
public partial class libbcmath
{
	/// <summary>Divides two arbitrary precision numbers</summary>
	/// <param name="dividend">The big (base) number</param>
	/// <param name="divisor">The number to divide it by</param>
	/// <param name="scale">The minimum scale to use</param>
	public static BCNum Divide(BCNum dividend, BCNum divisor, int scale)
	{
		dynamic quot = default(BCNum);
		BCNum qval = new BCNum();
		// @return qout
		List<byte> num1 = null;
		List<byte> num2 = null;
		List<byte> mval = null;
		// string
		int ptr1 = 0;
		int ptr2 = 0;
		int n2ptr = 0;
		int qptr = 0;
		// pointers
		int scale1 = 0;
		int val = 0;
		int len1 = 0;
		int len2 = 0;
		int scale2 = 0;
		int qdigits = 0;
		int extra = 0;
		int count = 0;
		int qdig = 0;
		int borrow = 0;
		int carry = 0;
		bool zero = false;
		// char
		byte norm = 0;
		byte qguess = 0;
		//var ptrs; // return object from one_mul

		// Test for divide by zero. (return failure)
		if (IsZero(divisor))
			return new BCNum {
				length = 1,
				value = new List<byte>(new byte[] { 1 }),
				sign = MINUS
			};
		// return -1

		// Test for zero divide by anything (return zero)
		if (IsZero(dividend))
			return NewNum(1, scale1);

		// Test for n1 equals n2 (return 1 as n1 nor n2 are zero)
		// /*
		//If DoCompare(n1, n2) = 0 Then
		//	quot = NewNum(1, scale)
		//	quot(0) = 1
		//	Return quot
		//End If
		// */

		// Test for divide by 1.  If it is we must truncate.
		if (IsOne(divisor)) {
			qval = NewNum(dividend.length, scale);
			// qval = bc_new_num (n1->n_len, scale);
			qval.sign = dividend.sign == divisor.sign ? PLUS : MINUS;
			memset(qval.value, dividend.length, 0, scale);
			// memset (&qval->n_value[n1->n_len],0,scale);
			// memcpy(qval->n_value, n1->n_value, n1->n_len + MIN(n1->n_scale,scale));
			memcpy(qval.value, 0, dividend.value, 0, dividend.length + Math.Min(dividend.scale, scale));
			// returning here should work, but not in the C source
			return qval;
		}

		// Set up the divide.  Move the decimal point on n1 by n2's scale.
		// Remember, zeros on the end of num2 are wasted effort for dividing.
		scale2 = divisor.scale;
		// scale2 = n2->n_scale;
		n2ptr = divisor.length + scale2 - 1;
		// n2ptr = (unsigned char *) n2.n_value+n2.n_len+scale2-1;
		while (scale2 > 0 & divisor[n2ptr] == 0) {
			n2ptr -= 1;
			scale2 -= 1;
		}

		len1 = dividend.length + scale2;
		scale1 = dividend.scale - scale2;
		if ((scale1 < scale)) {
			extra = scale - scale1;
		} else {
			extra = 0;
		}

		num1 = safe_emalloc(1, dividend.length + dividend.scale, extra + 2);
		// num1 = (unsigned char *) safe_emalloc (1, n1.n_len+n1.n_scale, extra+2);
		//if (num1 === null) {
		//	libbcmath.bc_out_of_memory();
		//}
		memset(num1, 0, 0, dividend.length + dividend.scale + extra + 2);
		// memset (num1, 0, n1->n_len+n1->n_scale+extra+2);
		memcpy(num1, 1, dividend.value, 0, dividend.length + dividend.scale);
		// memcpy (num1+1, n1.n_value, n1.n_len+n1.n_scale);

		len2 = divisor.length + scale2;
		// len2 = n2->n_len + scale2;
		num2 = safe_emalloc(1, len2, 1);
		// num2 = (unsigned char *) safe_emalloc (1, len2, 1);
		//if (num2 === null) {
		//	libbcmath.bc_out_of_memory();
		//}
		memcpy(num2, 0, divisor.value, 0, len2);
		// memcpy (num2, n2.n_value, len2);
		// num2(len2) = 0 ' *(num2+len2) = 0;
		n2ptr = 0;
		// n2ptr = num2;

		// while (*n2ptr == 0)
		while (num2[n2ptr] == 0) {
			n2ptr += 1;
			len2 -= 1;
		}

		// Calculate the number of quotient digits.
		if (len2 > len1 + scale) {
			qdigits = scale + 1;
			zero = true;
		} else {
			zero = false;
			if ((len2 > len1)) {
				qdigits = scale + 1;
				// One for the zero integer part.
			} else {
				qdigits = len1 - len2 + scale + 1;
			}
		}

		// Allocate and zero the storage for the quotient.
		qval = NewNum(qdigits - scale, scale);
		// qval = bc_new_num (qdigits-scale,scale);
		memset(qval.value, 0, 0, qdigits);
		// memset (qval->n_value, 0, qdigits);

		// Allocate storage for the temporary storage mval.
		mval = safe_emalloc(1, len2, 1);
		// mval = (unsigned char *) safe_emalloc (1, len2, 1);
		//if (mval === null) {
		//	libbcmath.bc_out_of_memory();
		//}

		// Now for the full divide algorithm.
		if (!zero) {
			//  Normalize
			// //norm = libbcmath.cint(10 / (libbcmath.cint(n2.n_value[n2ptr]) + 1)); //norm =  10 / ((int)*n2ptr + 1);
			norm = Convert.ToByte(Math.Floor(10 / (divisor[n2ptr] + 1)));
			// norm =  10 / ((int)*n2ptr + 1);
			if (norm != 1) {
				OneMult(num1, 0, len1 + scale1 + extra + 1, norm, ref num1, 0);
				// libbcmath._one_mult(num1, len1+scale1+extra+1, norm, num1);
				// UNDONE: @CHECK Is the pointer affected by the call? if so, maybe need to adjust points on return?
			}

			// Initialize divide loop.
			qdig = 0;
			if (len2 > len1) {
				qptr = len2 - len1;
				// qptr = (unsigned char *) qval.n_value+len2-len1;
			} else {
				qptr = 0;
				// qptr = (unsigned char *) qval.n_value;
			}

			// Loop
			while (qdig <= len1 + scale - len2) {
				// Calculate the quotient digit guess.
				if (divisor[n2ptr] == num1[qdig]) {
					qguess = 9;
				} else {
					qguess = Convert.ToByte(Math.Floor((num1[qdig] * 10 + num1[qdig + 1]) / divisor[n2ptr]));
				}
				// Test qguess.
				// if (n2ptr[1]*qguess > (num1[qdig]*10 + num1[qdig+1] - *n2ptr*qguess)*10 + num1[qdig+2]) {
				if (divisor[n2ptr + 1] * qguess > (num1[qdig] * 10 + num1[qdig + 1] - divisor[n2ptr] * qguess) * 10 + num1[qdig + 2]) {
					qguess -= Convert.ToByte(1);
					// And again.
					// if (n2ptr[1]*qguess > (num1[qdig]*10 + num1[qdig+1] - *n2ptr*qguess)*10 + num1[qdig+2])
					if (divisor[n2ptr + 1] * qguess > (num1[qdig] * 10 + num1[qdig + 1] - divisor[n2ptr] * qguess) * 10 + num1[qdig + 2]) {
						qguess -= Convert.ToByte(1);
					}
				}

				// Multiply and subtract.
				borrow = 0;
				if (qguess != 0) {
					// mval(0) = 0	' UNDONE: @CHECK is this to fix ptr2 < 0? ' *mval = 0; 
					OneMult(divisor.value, n2ptr, len2, qguess, ref mval, 1);
					// _one_mult (n2ptr, len2, qguess, mval+1)

					ptr1 = qdig + len2;
					// (unsigned char *) num1+qdig+len2;
					ptr2 = len2;
					// (unsigned char *) mval+len2;

					// UNDONE: @CHECK Does a negative pointer return null?
					// ptr2 can be < 0 here as ptr1 = len2, thus count < len2+1 will always fail ?
					for (count = 0; count <= len2; count++) {
						if (ptr2 < 0) {
							//val = libbcmath.cint(num1[ptr1]) - 0 - borrow;    //val = (int) *ptr1 - (int) *ptr2-- - borrow;
							val = num1[ptr1] - 0 - borrow;
							// val = (int) *ptr1 - (int) *ptr2-- - borrow;
						} else {
							//val = libbcmath.cint(num1[ptr1]) - libbcmath.cint(mval[ptr2--]) - borrow;    //val = (int) *ptr1 - (int) *ptr2-- - borrow;
							val = num1[ptr1] - mval[ptr2] - borrow;
							// val = (int) *ptr1 - (int) *ptr2-- - borrow;
							ptr2 -= 1;
						}
						if (val < 0) {
							val += 10;
							borrow = 1;
						} else {
							borrow = 0;
						}
						num1[ptr1] = Convert.ToByte(val);
						ptr1 -= 1;
					}
				}

				// Test for negative result.
				if (borrow == 1) {
					qguess -= Convert.ToByte(1);
					ptr1 = qdig + len2;
					// (unsigned char *) num1+qdig+len2;
					ptr2 = len2 - 1;
					// (unsigned char *) n2ptr+len2-1;
					carry = 0;
					for (count = 0; count <= len2 - 1; count++) {
						if (ptr2 < 0) {
							// val = libbcmath.cint(num1[ptr1]) + 0 + carry; //val = (int) *ptr1 + (int) *ptr2-- + carry;
							val = num1[ptr1] + 0 + carry;
							// val = (int) *ptr1 + (int) *ptr2-- + carry;
						} else {
							// val = libbcmath.cint(num1[ptr1]) + libbcmath.cint(n2.n_value[ptr2--]) + carry; //val = (int) *ptr1 + (int) *ptr2-- + carry;
							val = num1[ptr1] + divisor[ptr2] + carry;
							// val = (int) *ptr1 + (int) *ptr2-- + carry;
							ptr2 -= 1;
						}
						if (val > 9) {
							val -= 10;
							carry = 1;
						} else {
							carry = 0;
						}
						num1[ptr1] = Convert.ToByte(val);
						// *ptr1-- = val;
						ptr1 -= 1;
					}
					if (carry == 1) {
						// UNDONE: @CHECKs present in this block
						// num1[ptr1] = cint((num1[ptr1] + 1) % 10);  // *ptr1 = (*ptr1 + 1) % 10; // @CHECK
						num1[ptr1] = Convert.ToByte((num1[ptr1] + 1) % 10);
						// *ptr1 = (*ptr1 + 1) % 10; // @CHECK
					}
				}

				// We now know the quotient digit.
				qval(qptr) = qguess;
				// *qptr++ =  qguess;
				qptr += 1;
				qdig += 1;
			}
		}

		// Clean up and return the number.
		qval.sign = dividend.sign == divisor.sign ? libbcmath.PLUS : libbcmath.MINUS;
		if (IsZero(qval))
			qval.sign = PLUS;
		RemoveLeadingZeros(qval);

		// Return the value after the "hard work"
		return qval;
	}

	/// <summary>
	/// Some utility routines for the divide:  First a one digit multiply.
	/// NUM (with SIZE digits) is multiplied by DIGIT and the result is
	/// placed into RESULT.  It is written so that NUM and RESULT can be
	/// the same pointers.
	/// </summary>
	/// <param name="num">A reference to the array to multiply</param>
	/// <param name="n_ptr">The offset of the first array</param>
	/// <param name="size">Number of iterations</param>
	/// <param name="digit">The digit to multiply it by</param>
	/// <param name="result">A reference to the result array</param>
	/// <param name="r_ptr">The offset of the second array</param>
	protected static void OneMult(List<byte> num, int n_ptr, int size, byte digit, ref List<byte> result, int r_ptr)
	{
		byte carry = 0;
		byte value = 0;
		int nptr = 0;
		int rptr = 0;
		// pointers
		if (digit == 0) {
			memset(result, r_ptr, 0, size);
			// memset (result, 0, size);
		} else if (digit == 1) {
			memcpy(result, r_ptr, num, n_ptr, size);
			// memcpy (result, num, size);
		} else {
			// Initialize
			nptr = n_ptr + size - 1;
			// nptr = (unsigned char *) (num+size-1);
			rptr = r_ptr + size - 1;
			// rptr = (unsigned char *) (result+size-1);
			carry = 0;

			while (size > 0) {
				size -= 1;
				value = num[nptr] * digit + carry;
				// value = *nptr-- * digit + carry;
				nptr -= 1;
				result[rptr] = value % BASE;
				// @CHECK cint //*rptr-- = value % BASE;
				rptr -= 1;
				carry = Convert.ToByte(Math.Floor(value / BASE));
				// @CHECK cint //carry = value / BASE;
			}

			if (carry != 0)
				result[rptr] = carry;
		}
	}
}