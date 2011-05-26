using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
public partial class libbcmath
{
	/// <summary>
	/// In some places we need to check if the number NUM is almost zero.
	/// Specifically, all but the last digit is 0 and the last digit is 1.
	/// Last digit is defined by scale.
	/// </summary>
	/// <param name="num">The number to check</param>
	/// <param name="scale">The last digit</param>
	public static bool IsNearZero(BCNum num, int scale)
	{
		int count = 0;
		int nptr = 0;

		// Error checking
		if (scale > num.scale)
			scale = num.scale;

		// Initialize
		count = num.length + scale;
		nptr = 0;

		// The check
		while (count > 0 & num(nptr) == 0) {
			nptr += 1;
			count -= 1;
		}

		nptr -= 1;
		return count == 0 | (count == 1 & num(nptr) == 1);
	}
}