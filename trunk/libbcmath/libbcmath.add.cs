using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
public partial class libbcmath
{
	/// <summary>Adds two arbitrary precision numbers</summary>
	/// <param name="base">The first number</param>
	/// <param name="addend">The number to add onto it</param>
	/// <param name="scale_min">The minimum scale for the result</param>
	public static BCNum Add(BCNum @base, BCNum addend, int scale_min = defaultScale)
	{
		BCNum sum = new BCNum();
		int cmp_res = -2;
		// Same sign; add
		if (@base.sign == addend.sign) {
			sum.sign = @base.sign;
			sum = DoAdd(@base, addend, scale_min);
		// subtraction must be done
		} else {
			cmp_res = DoCompareAdvanced(@base, addend, false, false);
			switch (cmp_res) {
				case -1:
					// second number is bigger
					sum.sign = addend.sign;
					sum = libbcmath.DoSub(addend, @base, scale_min);
					break;
				case 0:
					// equal numbers
					sum = NewNum(1, Math.Max(scale_min, Math.Max(@base.scale, addend.scale)));
					memset(sum.value, 0, 0, sum.scale + 1);
					break;
				case 1:
					// first number is bigger
					sum.sign = @base.sign;
					sum = libbcmath.DoSub(@base, addend, scale_min);
					break;
			}
		}
		// Return our final answer
		return sum;
	}
}