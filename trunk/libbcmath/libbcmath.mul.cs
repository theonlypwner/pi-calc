using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
public partial class libbcmath
{
	public const byte MUL_BASE_DIGITS = 80;
	// #define MUL_SMALL_DIGITS mul_base_digits/4
	public const byte MUL_SMALL_DIGITS = 20;

	/// <summary>Multiplies two arbitrary precision numbers</summary>
	/// <param name="factor">The first factor</param>
	/// <param name="multiplier">The second factor</param>
	/// <param name="scale">The minimum scale to use</param>
	public static BCNum Multiply(BCNum factor, BCNum multiplier, int scale = defaultScale)
	{
		BCNum pval = new BCNum();
		int len1 = 0;
		int len2 = 0;
		int full_scale = 0;
		int prod_scale = 0;

		// Initialize things
		len1 = factor.length + factor.scale;
		len2 = multiplier.length + multiplier.scale;
		full_scale = factor.scale + multiplier.scale;
		prod_scale = Math.Min(full_scale, Math.Max(scale, Math.Max(factor.scale, multiplier.scale)));

		// //pval = init_num() ' allow pass by ref
		// Do the multiply
		pval = RecMul(factor, len1, multiplier, len2, full_scale);

		// Assign to prod and clean up the number.
		pval.sign = factor.sign == multiplier.sign ? PLUS : MINUS;
		// //pval.n_value = pval.n_ptr; // @FIX
		pval.length = len2 + len1 + 1 - full_scale;
		pval.scale = prod_scale;
		RemoveLeadingZeros(pval);
		if (IsZero(pval))
			pval.sign = PLUS;
		// //bc_free_num (prod);
		return pval;
	}

	protected static BCNum NewSubNum(int length, int scale, ref List<byte> value)
	{
		BCNum temp = new BCNum();
		temp.length = length;
		temp.scale = scale;
		temp.value = value;
		return temp;
	}

	/// <summary>Simple multiplication</summary>
	/// <param name="n1">First number</param>
	/// <param name="n1len">Length of first number</param>
	/// <param name="n2">Second number</param>
	/// <param name="n2len">Length of second number</param>
	private static BCNum SimpMul(BCNum n1, int n1len, BCNum n2, int n2len)
	{
		BCNum prod = new BCNum();
		int n1ptr = 0;
		int n2ptr = 0;
		int pvptr = 0;
		// char *n1ptr, *n2ptr, *pvptr;
		int n1end = 0;
		int n2end = 0;
		// char *n1end, *n2end; ' To the end of n1 and n2.
		int indx = 0;
		int sum = 0;
		int prodlen = 0;
		// int indx, sum, prodlen;

		prodlen = n1len + n2len + 1;

		prod = NewNum(prodlen, 0);

		n1end = n1len - 1;
		// (char *) (n1->n_value + n1len - 1);
		n2end = n2len - 1;
		// (char *) (n2->n_value + n2len - 1);
		pvptr = prodlen - 1;
		// (char *) ((*prod)->n_value + prodlen - 1);
		sum = 0;

		// Here is the loop...
		for (indx = 0; indx <= prodlen - 2; indx++) {
			n1ptr = n1end - Math.Max(0, indx - n2len + 1);
			// (char *) (n1end - MAX(0, indx-n2len+1));
			n2ptr = n2end - Math.Min(indx, n2len - 1);
			// (char *) (n2end - MIN(indx, n2len-1));
			while (n1ptr >= 0 & n2ptr <= n2end) {
				sum += n1[n1ptr] * n2[n2ptr];
				// sum += *n1ptr-- * *n2ptr++;
				n1ptr -= 1;
				n2ptr += 1;
			}
			prod(pvptr) = Convert.ToByte(Math.Floor(sum % libbcmath.BASE));
			// *pvptr-- = sum % BASE;
			pvptr -= 1;
			sum = Convert.ToInt32(Math.Floor(sum / BASE));
			// sum = sum / BASE;
		}
		prod(pvptr) = Convert.ToByte(sum);
		// *pvptr = sum;
		return prod;
	}

	/// <summary>
	/// A special adder/subtractor for the recursive divide and conquer
	/// multiply algorithm.  Note: if sub is called, accum must
	/// be larger that what is being subtracted.  Also, accum and val
	/// must have n_scale = 0.  (e.g. they must look like integers. *)
	/// </summary>
	/// <param name="accum">This is passed by reference and contains the result.</param>
	/// <param name="val">The other number that will be used in calculation, but be unaltered</param>
	/// <param name="shift">Offset of digits to skip</param>
	/// <param name="subtract">Subtract the numers</param>
	protected static void Shift_AddSub(BCNum accum, BCNum val, int shift, bool subtract)
	{
		int accp = 0;
		int valp = 0;
		// signed char *accp, *valp;
		int count = 0;
		// int  count, carry;
		byte carry = 0;

		count = val.length;
		if (val[0] == 0)
			count -= 1;

		// assert (accum->n_len+accum->n_scale >= shift+count);
		if (accum.length + accum.scale < shift + count) {
			throw new Exception("len + scale < shift + count");
			// I (and the original coder) think(s) that is what assert does
		}

		// Set up pointers and others
		accp = accum.length + accum.scale - shift - 1;
		// (signed char *)(accum->n_value + accum->n_len + accum->n_scale - shift - 1);
		valp = 1;
		// (signed char *)(val->n_value + val->n_len - 1);
		val.length = 1;
		// same as above (valp = val.length = 1)
		carry = 0;
		if (subtract) {
			// Subtraction; carry is really borrow.
			while (count > 0) {
				count -= 1;
				accum[accp] -= val[valp] + carry;
				// *accp -= *valp-- + carry;
				valp -= 1;
				// if (*accp < 0)
				if (accum[accp] < 0) {
					carry = 1;
					accum[accp] += BASE;
					// *accp-- += BASE;
				// accp -= 1 ' see below
				} else {
					carry = 0;
					// accp -= 1 ' see below
				}
				accp -= 1;
				// either way, it needs to decrement this
			}
			while (carry > 0) {
				accum[accp] -= carry;
				// *accp -= carry;
				// if (*accp < 0)
				if ((accum[accp] < 0)) {
					accum[accp] += BASE;
					// *accp-- += BASE;
					accp -= 1;
				} else {
					carry = 0;
				}
			}
		} else {
			// Addition
			while (count > 0) {
				count -= 1;
				accum[accp] += Convert.ToByte(val[valp] + carry);
				// *accp += *valp-- + carry;
				valp -= 1;
				// if (*accp > (BASE-1))
				if (accum[accp] > BASE - 1) {
					carry = 1;
					accum[accp] -= BASE;
					// *accp-- -= BASE;
				// accp -=1 ' see below
				} else {
					carry = 0;
					// accp -= 1 ' see below
				}
				accp -= 1;
				// either way, it needs to be decremented
			}
			while (carry > 0) {
				accum[accp] += carry;
				// *accp += carry;
				// if (*accp > (BASE-1))
				if (accum[accp] > BASE - 1) {
					accum[accp] -= BASE;
					// *accp-- -= BASE;
					accp -= 1;
				} else {
					carry = 0;
				}
			}
		}
	}

	/// <summary>
	/// Recursive divide and conquer multiply algorithm.
	/// Based on
	/// Let u = u0 + u1*(b^n)
	/// Let v = v0 + v1*(b^n)
	/// Then uv = (B^2n+B^n)*u1*v1 + B^n*(u1-u0)*(v0-v1) + (B^n+1)*u0*v0
	/// B is the base of storage, number of digits in u1, u0 close to equal.
	/// </summary>
	/// <param name="u">The first number</param>
	/// <param name="ulen">Length of the first number</param>
	/// <param name="v">The second number</param>
	/// <param name="vlen">The length of the second number</param>
	/// <param name="full_scale">The full scale</param>
	private static BCNum RecMul(BCNum u, int ulen, BCNum v, int vlen, int full_scale)
	{
		BCNum prod = new BCNum();
		// @return
		dynamic u0 = default(BCNum);
		dynamic u1 = default(BCNum);
		dynamic v0 = default(BCNum);
		BCNum v1 = new BCNum();
		int u0len = 0;
		int v0len = 0;
		dynamic m1 = default(BCNum);
		dynamic m2 = default(BCNum);
		dynamic m3 = default(BCNum);
		dynamic d1 = default(BCNum);
		BCNum d2 = new BCNum();
		int n = 0;
		int prodlen = 0;
		bool m1zero = false;
		int d1len = 0;
		int d2len = 0;

		// Base case?
		if ((ulen + vlen) < libbcmath.MUL_BASE_DIGITS | ulen < libbcmath.MUL_SMALL_DIGITS | vlen < libbcmath.MUL_SMALL_DIGITS) {
			return SimpMul(ref u, ulen, ref v, vlen);
			// , full_scale)
		}

		// Calculate n -- the u and v split point in digits.
		n = Convert.ToInt32(Math.Floor((Math.Max(ulen, vlen) + 1) / 2));

		// Split u and v.
		if (ulen < n) {
			u1 = InitNum();
			// u1 = bc_copy_num (BCG(_zero_));
			u0 = NewSubNum(ulen, 0, ref u.value);
		} else {
			u1 = NewSubNum(ulen - n, 0, ref u.value);
			u0 = NewSubNum(n, 0, ref u.value.GetRange(ulen - n, u.value.Count - ulen + n));
		}
		if (vlen < n) {
			v1 = InitNum();
			// bc_copy_num (BCG(_zero_));
			v0 = NewSubNum(vlen, 0, ref v.value);
		} else {
			v1 = NewSubNum(vlen - n, 0, ref v.value);
			v0 = NewSubNum(n, 0, ref v.value.GetRange(vlen - n, v.value.Count - ulen + n));
		}
		RemoveLeadingZeros(u1);
		RemoveLeadingZeros(u0);
		u0len = u0.length;
		RemoveLeadingZeros(v1);
		RemoveLeadingZeros(v0);
		v0len = v0.length;

		m1zero = IsZero(u1) | IsZero(v1);

		// Calculate sub results ...
		d1 = InitNum();
		// needed?
		d2 = InitNum();
		// needed?
		d1 = DoSub(u1, u0, 0);
		d1len = d1.length;

		d2 = DoSub(v0, v1, 0);
		d2len = d2.length;

		// Do recursive multiplies and shifted adds.
		if (m1zero) {
			m1 = InitNum();
			// bc_copy_num (BCG(_zero_));
		} else {
			m1 = RecMul(u1, u1.length, v1, v1.length, 0);
		}
		if (IsZero(d1) | IsZero(d2)) {
			m2 = InitNum();
			// bc_copy_num (BCG(_zero_));
		} else {
			m2 = RecMul(d1, d1len, d2, d2len, 0);
		}

		if (IsZero(u0) | IsZero(v0)) {
			m3 = InitNum();
			// bc_copy_num (BCG(_zero_));
		} else {
			m3 = RecMul(u0, u0.length, v0, v0.length, 0);
		}

		// Initialize product
		prodlen = ulen + vlen + 1;
		prod = NewNum(prodlen, 0);

		if (!m1zero) {
			Shift_AddSub(ref prod, m1, 2 * n, false);
			Shift_AddSub(ref prod, m1, 2 * n, false);
			Shift_AddSub(ref prod, m1, n, false);
		}
		Shift_AddSub(ref prod, m3, n, false);
		Shift_AddSub(ref prod, m3, 0, false);
		Shift_AddSub(ref prod, m2, n, d1.sign != d2.sign);

		return prod;
		// Now clean up?
		// bc_free_num (&u1);
		// bc_free_num (&u0);
		// bc_free_num (&v1);
		// bc_free_num (&m1);
		// bc_free_num (&v0);
		// bc_free_num (&m2);
		// bc_free_num (&m3);
		// bc_free_num (&d1);
		// bc_free_num (&d2);
	}
}