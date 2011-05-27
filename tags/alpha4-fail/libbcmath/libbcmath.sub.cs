using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
public partial class libbcmath
{
	/// <summary>Subtracts two arbitrary precision numbers</summary>
	/// <param name="minuend">The big (base) number</param>
	/// <param name="subtrahend">The number to subtract from it</param>
	public static BCNum Subtract(BCNum minuend, BCNum subtrahend, int scale_min = defaultScale)
	{
		BCNum diff = new BCNum();
		int cmp_res = 0;
		int res_scale = 0;
		if (minuend.sign != subtrahend.sign) {
			diff = DoAdd(minuend, subtrahend, scale_min);
			diff.sign = minuend.sign;
		// subtraction must be done
		} else {
			// Compare magnitudes
			cmp_res = DoCompareAdvanced(minuend, subtrahend, false, false);
			switch (cmp_res) {
				case -1:
					// The minuend is less than the subtrahend, subtract the minuend from the subtrahend.
					diff = DoSub(subtrahend, minuend, scale_min);
					diff.sign = InvertSign(minuend.sign);
					break;
				case 0:
					// They are equal! Return zero!
					res_scale = Math.Max(scale_min, Math.Max(minuend.scale, subtrahend.scale));
					diff = NewNum(1, res_scale);
					memset(diff.value, 0, 0, res_scale + 1);
					break;
				case 1:
					// The subtrahend is less than the minuend, subtract the subtrahend from the minuend.
					diff = DoSub(minuend, subtrahend, scale_min);
					diff.sign = minuend.sign;
					break;
			}
		}
		// Return the difference
		return diff;
	}
}