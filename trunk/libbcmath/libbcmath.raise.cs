using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
public partial class libbcmath
{
	/// <summary>Raise an arbitrary precision number to a power of another arbitrary power</summary>
	/// <param name="base">The base number</param>
	/// <param name="power">The power to raise the base to</param>
	/// <param name="scale_min">The minimum scale to use</param>
	public static BCNum Raise(BCNum @base, BCNum power, int scale_min = defaultScale)
	{
		BCNum result = InitNum();
		DoRaise(@base, power, ref result, scale_min);
		return result;
	}

	/// <summary>Calculate a power</summary>
	/// <param name="num1">The base</param>
	/// <param name="num2">The power to raise it to</param>
	/// <param name="result">The result, passed by reference</param>
	/// <param name="scale">The minimum scale to use</param>
	private static void DoRaise(BCNum num1, BCNum num2, ref BCNum result, int scale)
	{
		BCNum temp = default(BCNum);
		BCNum power = default(BCNum);
		long exponent = 0;
		int rscale = 0;
		int pwrscale = 0;
		int calcscale = 0;
		bool neg = false;

		// Check the exponent for scale digits and convert to a long.
		// If num2.scale <> 0 then Throw New Exception("non-zero scale in exponent") ' warning, not error
		exponent = num2long(num2);

		if (exponent == 0 & (num2.length > 1 | num2.value(0) != 0))
			throw new Exception("Exponent too large in raise");

		// Special case if exponent is a zero.
		if (exponent == 0) {
			result = 1;
			return;
		}

		// Other initializations.
		if (exponent < 0) {
			neg = true;
			exponent = -exponent;
			rscale = scale;
		} else {
			neg = false;
			rscale = Math.Min(Convert.ToInt32(num1.scale * exponent), Math.Max(scale, num1.scale));
		}

		// Set initial value of temp.
		power = num1;
		pwrscale = num1.scale;
		while ((exponent & 1) == 0) {
			pwrscale *= 2;
			power = Multiply(power, power, pwrscale);
			exponent = exponent >> 1;
		}
		temp = power;
		calcscale = pwrscale;
		exponent = exponent >> 1;

		// Do the calculation.
		while (exponent > 0) {
			pwrscale *= 2;
			power = Multiply(power, power, pwrscale);
			if ((exponent & 1) == 1) {
				calcscale = pwrscale + calcscale;
				temp = Multiply(temp, power, calcscale);
			}
			exponent = exponent >> 1;
		}

		// Assign the value.
		if ((neg)) {
			result = Divide(1, temp, rscale);
		} else {
			result = temp.GetShallowCopy;
			// *result = temp;
			if ((result.scale > rscale))
				result.scale = rscale;
		}
	}

	/// <summary>The maximum long to return, which fits into an Integer</summary>

	public const int LONG_MAX = 0x7ffffff;
	/// <summary>Converts an arbitrary precision number to a Long</summary>
	/// <param name="num">The arbitrary precision number to convert</param>
	public static long num2long(BCNum num)
	{
		long val = 0;
		int nptr = 0;
		int index = 0;
		// char *, int
		// Extract the int value, ignore the fraction.
		nptr = 0;
		index = num.length;
		// revising the for loop to work with VB
		while ((index > 0) & (val <= (LONG_MAX / BASE))) {
			index -= 1;
			val = val * BASE + num(nptr);
			nptr += 1;
		}

		// Check for overflow.  If overflow, return zero.
		if (index > 0)
			val = 0;
		if (val < 0)
			val = 0;

		// Return the value.
		return num.sign == PLUS ? val : -val;
	}
}