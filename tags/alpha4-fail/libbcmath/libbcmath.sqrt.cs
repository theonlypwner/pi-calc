using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
public partial class libbcmath
{
	/// <summary>Get the square root of an arbitrary precision number directly</summary>
	/// <param name="num">The (only) number</param>
	/// <param name="scale_min">The minimum scale for the result</param>
	public static bool SquareRoot(ref BCNum num, int scale_min = 0)
	{
		int rscale = 0;
		int cmp_res = 0;
		int cscale = 0;
		bool done = false;
		BCNum guess = default(BCNum);
		BCNum guess1 = default(BCNum);
		BCNum point5 = default(BCNum);
		BCNum diff = default(BCNum);
		// Initial check
		cmp_res = DoCompare(num, 0);
		if (cmp_res < 0) {
			return false;
		} else if (cmp_res == 0) {
			num = 0;
			return true;
		}
		cmp_res = DoCompare(num, 1);
		if (cmp_res == 0) {
			num = 1;
			return true;
		}

		// Initialize the variables.
		rscale = Math.Max(scale_min, num.scale);
		guess = InitNum();
		guess1 = InitNum();
		diff = InitNum();
		point5 = NewNum(1, 1);
		point5(1) = 5;

		// Calculate the initial guess.
		if (cmp_res < 0) {
			// The number is between 0 and 1.  Guess should start at 1.
			guess = 1;
			cscale = num.scale;
		} else {
			// The number is greater than 1.  Guess should start at 10^(exp/2).
			guess = 10;
			guess1 = num.length;
			guess1 = Multiply(guess1, point5, 0);
			guess1.scale = 0;
			// HACK: Link the raise
			// bc_raise (guess, guess1, &guess, 0 TSRMLS_CC);
			cscale = 3;
		}

		// Find the square root using Newton's algorithm.
		done = false;
		while (!(done)) {
			guess1 = guess;
			guess = Divide(num, guess, cscale);
			guess = Add(guess, guess1, 0);
			guess = Multiply(guess, point5, cscale);
			diff = Subtract(guess, guess1, cscale + 1);
			if (IsNearZero(diff, cscale)) {
				if ((cscale < rscale + 1)) {
					cscale = Math.Min(cscale * 3, rscale + 1);
				} else {
					done = true;
				}
			}
		}

		// Assign the number
		num = Divide(guess, 1, rscale);
		return true;
	}
}