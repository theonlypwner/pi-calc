using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
public partial class libbcmath
{
	/// <summary>Compare two arbitrary precision numbers</summary>
	/// <param name="n1">The first number (1 if larger)</param>
	/// <param name="n2">The second number (-1 if larger)</param>
	public static sbyte DoCompare(BCNum n1, BCNum n2)
	{
		return DoCompareAdvanced(n1, n2, true, false);
	}

	/// <summary>Raw compare of two arbitrary precision numbers</summary>
	/// <param name="n1">The first number (1 if larger)</param>
	/// <param name="n2">The second number (-1 if larger)</param>
	/// <param name="use_sign">Check the signs, only magnitude is checked if false</param>
	/// <param name="ignore_last">Whether or not to ignore the last digit</param>
	private static sbyte DoCompareAdvanced(BCNum n1, BCNum n2, bool use_sign, bool ignore_last)
	{
		int n1ptr = 0;
		int n2ptr = 0;
		int count = 0;

		// First, compare signs.
		if (use_sign & (n1.sign != n2.sign)) {
			return Convert.ToSByte(n1.sign == PLUS ? 1 : -1);
			// POSITIVE n1 > NEGATIVE n2 = 1
		}
		// the signs must be the same now!

		// Now compare the magnitude.
		if (n1.length > n2.length) {
			return Convert.ToSByte((!use_sign) | n1.sign == PLUS ? 1 : -1);
		} else if (n1.length < n2.length) {
			return Convert.ToSByte((!use_sign) | n1.sign == PLUS ? -1 : 1);
			// same sign
		}

		// If we get here, they have the same number of integer digits.
		// Check the integer part and the equal length part of the fraction.
		count = n1.length + Math.Min(n1.scale, n2.scale);
		while ((count > 0) & (n1[n1ptr] == n2[n2ptr])) {
			n1ptr += 1;
			n2ptr += 1;
			count -= 1;
		}

		if (ignore_last & (count == 1) & (n1.scale == n2.scale))
			return 0;

		if (count != 0) {
			if (n1[n1ptr] > n2[n2ptr]) {
				return Convert.ToSByte((!use_sign) | n1.sign == PLUS ? 1 : -1);
			} else {
				return Convert.ToSByte((!use_sign) | n1.sign == PLUS ? -1 : 1);
			}
		}

		// They are equal up to the last part of the equal part of the fraction.
		if (n1.scale > n2.scale) {
			for (count = (n1.scale - n2.scale); count >= 1; count += -1) {
				if ((n1[n1ptr] != 0)) {
					return Convert.ToSByte((!use_sign) | n1.sign == PLUS ? 1 : -1);
				}
				n1ptr += 1;
			}
		} else if (n1.scale < n2.scale) {
			for (count = (n2.scale - n1.scale); count >= 1; count += -1) {
				if ((n2[n2ptr] != 0)) {
					return Convert.ToSByte((!use_sign) | n1.sign == PLUS ? -1 : 1);
				}
				n2ptr += 1;
			}
		}

		// They must be equal!
		return 0;
	}
}