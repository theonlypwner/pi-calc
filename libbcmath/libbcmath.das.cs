using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
public partial class libbcmath
{
	/// <summary>Performs a raw addition iteration</summary>
	/// <param name="n1">The base number</param>
	/// <param name="n2">The addend to add to the base</param>
	/// <param name="scale_min">The smallest scale to use</param>
	private static BCNum DoAdd(BCNum n1, BCNum n2, int scale_min = 0)
	{
		BCNum sum = new BCNum();
		int sum_scale = 0;
		int sum_digits = 0;
		int n1ptr = 0;
		int n2ptr = 0;
		int sumptr = 0;
		int n1bytes = 0;
		int n2bytes = 0;
		byte carry = 0;
		byte tmp = 0;

		// Prepare sum.
		sum_scale = Math.Max(n1.scale, n2.scale);
		sum_digits = Math.Max(n1.length, n2.length) + 1;
		sum = NewNum(sum_digits, Math.Max(sum_scale, scale_min));


		// Not needed? [Still in C++]
		//if (scale_min > sum_scale) {
		//	sumptr = (char *) (sum->n_value + sum_scale + sum_digits);
		//	for (count = scale_min - sum_scale; count > 0; count--) {
		//		*sumptr++ = 0;
		//	}
		//}

		// Start with the fraction part. Initialize the pointers.
		n1bytes = n1.scale;
		n2bytes = n2.scale;
		n1ptr = n1.length + n1bytes - 1;
		n2ptr = n2.length + n2bytes - 1;
		sumptr = sum_scale + sum_digits - 1;

		// Add the fraction part. First copy the longer fraction (ie when adding 1.2345 to 1 we know .2345 is correct already) .
		// n1 has more decimals then n2
		if (n1bytes > n2bytes) {
			while (n1bytes > n2bytes) {
				sum(sumptr) = n1[n1ptr];
				sumptr -= 1;
				n1ptr -= 1;
				n1bytes -= 1;
			}
		// n2 has more decimals then n1
		} else if (n2bytes > n1bytes) {
			while (n2bytes > n1bytes) {
				sum(sumptr) = n2[n2ptr];
				sumptr -= 1;
				n2ptr -= 1;
				n2bytes -= 1;
			}
		}

		// Now add the remaining fraction part and equal size integer parts.
		n1bytes += n1.length;
		n2bytes += n2.length;
		carry = 0;
		while (n1bytes > 0 & n2bytes > 0) {
			// Add the two numbers together
			tmp = n1[n1ptr] + n2[n2ptr] + carry;
			// *sumptr = *n1ptr-- + *n2ptr-- + carry;
			n1ptr -= 1;
			n2ptr -= 1;

			if (tmp >= BASE) {
				carry = 1;
				tmp -= BASE;
				// subtract 10, add a carry
			} else {
				carry = 0;
			}
			sum(sumptr) = tmp;
			sumptr -= 1;
			n1bytes -= 1;
			n2bytes -= 1;
		}

		// Now add carry the [rest of the] longer integer part.
		if (n1bytes == 0) {
			// n2 is a bigger number then n1
			while (n2bytes > 0) {
				n2bytes -= 1;
				tmp = n2[n2ptr] + carry;
				// *sumptr = *n2ptr-- + carry;
				n2ptr -= 1;
				if (tmp > BASE) {
					carry = 1;
					tmp -= BASE;
				} else {
					carry = 0;
				}
				sum(sumptr) = tmp;
				sumptr -= 1;
			}
		} else {
			// n1 is bigger than n2
			while (n1bytes > 0) {
				n1bytes -= 1;
				tmp = n1[n1ptr] + carry;
				// *sumptr = *n1ptr-- + carry;
				n1ptr -= 1;
				if (tmp > BASE) {
					carry = 1;
					tmp -= BASE;
				} else {
					carry = 0;
				}
				sum(sumptr) = tmp;
				sumptr -= 1;
			}
		}

		// Set final carry.
		if (carry == 1) {
			sum(sumptr) += Convert.ToByte(1);
			// *sumptr += 1;
		}

		// Adjust sum and return.
		RemoveLeadingZeros(sum);
		return sum;
	}

	/// <summary>Performs a raw subtraction iteration</summary>
	/// <param name="n1">The big number to subtract from</param>
	/// <param name="n2">The small number to subtract</param>
	/// <param name="scale_min">The minimum scale to use</param>
	private static BCNum DoSub(BCNum n1, BCNum n2, int scale_min = 0)
	{
		BCNum diff = new BCNum();
		int diff_scale = 0;
		int diff_len = 0;
		int min_scale = 0;
		int min_len = 0;
		int n1ptr = 0;
		int n2ptr = 0;
		int diffptr = 0;
		int borrow = 0;
		int count = 0;
		int val = 0;

		// Allocate temporary storage.
		diff_len = Math.Max(n1.length, n2.length);
		diff_scale = Math.Max(n1.scale, n2.scale);
		min_len = Math.Min(n1.length, n2.length);
		min_scale = Math.Min(n1.scale, n2.scale);
		diff = NewNum(diff_len, Math.Max(diff_scale, scale_min));

		// Not needed? [Still in JavaScript and C++]
		// Zero extra digits made by scale_min
		//if (scale_min > diff_scale) {
		//	diffptr = (char *) (diff->n_value + diff_len + diff_scale);
		//	for (count = scale_min - diff_scale; count > 0; count--) {
		//		*diffptr++ = 0;
		//	}
		//}

		// Initialize the subtract.
		n1ptr = n1.length + n1.scale - 1;
		n2ptr = n2.length + n2.scale - 1;
		diffptr = diff_len + diff_scale - 1;

		// Subtract the numbers
		borrow = 0;

		// Take care of the longer scaled number.
		// n1 has the longer scale
		if (n1.scale != min_scale) {
			for (count = n1.scale - min_scale; count >= 1; count += -1) {
				diff[diffptr] = n1[n1ptr];
				// *diffptr-- = *n1ptr--;
				diffptr -= 1;
				n1ptr -= 1;
			}
		// n2 has the longer scale
		} else {
			for (count = n2.scale - min_scale; count >= 1; count += -1) {
				val = n2[n2ptr] - borrow;
				// val = - *n2ptr-- - borrow;
				n2ptr -= 1;
				if (val < 0) {
					val += BASE;
					borrow = 1;
				} else {
					borrow = 0;
					diff[diffptr] = Convert.ToByte(val);
					// *diffptr-- = val;
					diffptr -= 1;
				}
			}
		}

		// Now do the equal length scale and integer parts.
		for (count = 0; count <= min_len + min_scale - 1; count++) {
			val = n1[n1ptr] - n2[n2ptr] - borrow;
			// val = *n1ptr-- - *n2ptr-- - borrow;
			n1ptr -= 1;
			n2ptr -= 1;
			if (val < 0) {
				val += BASE;
				borrow = 1;
			} else {
				borrow = 0;
			}
			diff[diffptr] = Convert.ToByte(val);
			// *diffptr-- = val;
			diffptr -= 1;
		}

		// If n1 has more digits then n2, we now do that subtract.
		if (diff_len != min_len) {
			for (count = diff_len - min_len; count >= 1; count += -1) {
				val = n1[n1ptr] - borrow;
				// val = *n1ptr-- - borrow;
				n1ptr -= 1;
				if (val < 0) {
					val += BASE;
					borrow = 1;
				} else {
					borrow = 0;
				}
				diff[diffptr] = Convert.ToByte(val);
				diffptr -= 1;
			}
		}

		// Clean up and return.
		RemoveLeadingZeros(diff);
		return diff;
	}
}