using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
public sealed partial class libbcmath
{
	// Constants
	public const char PLUS = '+';
	public const char MINUS = '-';
	public const byte BASE = 10; // must be 10

	public const int defaultScale = 0;
	/// <summary>Basic Number Structure</summary>
	public struct BCNum
	{
		/// <summary>The sign of the character</summary>
		public char sign;
		/// <summary>Digits before the decimal point</summary>
		public int length;
		/// <summary>Digits after the decimal point</summary>
		public int scale;
		/// <summary>Array for value; 1.23 = [1, 2, 3]</summary>

		public List<byte> value;
		/// <summary>Converts this number to a string</summary>
		public override string ToString()
		{
			string r = "";
			string tmp = "";
			foreach (byte c in value) {
				tmp += c.ToString();
			}
			// add minus sign (if applicable) then add the integer part
			r = this.sign == libbcmath.PLUS ? "" : this.sign + tmp.Substring(0, this.length);
			// if there are decimal places, add a . and the decimal part
			if (this.scale > 0) {
				r += '.' + tmp.Substring(this.length, this.scale);
			}
			return r;
		}

		/// <summary>Gets a shallow copy of this instance of BCNum</summary>
		public BCNum GetShallowCopy()
		{
			return CreateShallowCopy(this);
		}

		/// <summary>Creates a shallow copy (memberwise clone) of a BCNum</summary>
		/// <param name="obj">An instance of BCNum</param>
		public static BCNum CreateShallowCopy(BCNum obj)
		{
			return (BCNum)obj.MemberwiseClone();
		}

		/// <summary>Get or set the value from a specific index in the value list</summary>
		/// <param name="i">The index of the location</param>
		public byte this[int i] {
			get { return value[i]; }
			set { value.Item(i) = value; }
		}

		public static implicit operator BCNum(int i)
		{
			return BCMath.IntToNum(i);
		}
	}

	/// <summary>Creates a new instance of a structure representing an arbitrary precision number</summary>
	/// <param name="length">The length before the decimal</param>
	/// <param name="scale">The length after the decimal</param>
	public static BCNum NewNum(int length, int scale)
	{
		BCNum temp = new BCNum();
		temp.sign = PLUS;
		temp.length = length;
		temp.scale = scale;
		temp.value = libbcmath.safe_emalloc(length + scale);
		libbcmath.memset(temp.value, 0, 0, length + scale);
		return temp;
	}

	/// <summary>Create a new arbitrary precision number</summary>
	public static BCNum InitNum()
	{
		return libbcmath.NewNum(1, 0);
	}

	/// <summary>Strips zeros until there is only one left</summary>
	/// <param name="num">The arbitrary precision number to strip the zeros from</param>
	static internal void RemoveLeadingZeros(ref BCNum num)
	{
		// We can move value to point to the first non zero digit!
		while (num[0] == 0 & num.length > 1) {
			num.value.RemoveAt(0);
			num.length -= 1;
		}
	}

	/// <summary>Convert to an arbitrary precision detecting the proper scale</summary>
	/// <param name="str">The string to convert</param>
	public static BCNum str2num(string str)
	{
		int p = str.IndexOf('.');
		if (p == -1) {
			return libbcmath.str2num(str, 0);
		} else {
			return libbcmath.str2num(str, (str.Length - p));
		}
	}

	public static BCNum str2num(string s, int scale)
	{
		char[] str = s.ToCharArray();
		BCNum num = new BCNum();
		dynamic ptr = 0;
		dynamic digits = 0;
		dynamic strscale = 0;
		dynamic nptr = 0;
		bool zero_int = false;
		// skip sign
		if (Conversion.Str(0) == PLUS | s[0] == MINUS) {
			ptr += 1;
			// next
		}
		// don't evaluate both sides of 'And'
		while (ptr < str.Length) {
			if (Conversion.Str(ptr) != '0')
				break; // TODO: might not be correct. Was : Exit Do
			// skip leading zeros
			ptr += 1;
			//next
		}
		// count digits
		while (ptr < str.Length) {
			if (!char.IsDigit(Conversion.Str(ptr)))
				break; // TODO: might not be correct. Was : Exit Do
			// VB .NET process both And's sides for some reason
			ptr += 1;
			// next
			digits += 1;
			// counted a digit
		}
		if (ptr < str.Length)
			if (Conversion.Str(ptr) == '.')
				ptr += 1;
		// skip decimal point
		// count digits after decimal point
		while (ptr < str.Length) {
			if (!char.IsDigit(Conversion.Str(ptr)))
				break; // TODO: might not be correct. Was : Exit Do
			ptr += 1;
			// next character
			strscale += 1;
			// digits after decimal point
		}

		if (str.Length < ptr | digits + strscale == 0) {
			return InitNum();
			// invalid number, return zero
		}

		// Adjust numbers and allocate storage and initialize fields.
		strscale = Math.Min(strscale, scale);
		if (digits == 0) {
			zero_int = true;
			digits = 1;
		}

		num = NewNum(digits, strscale);

		// Build the whole number
		ptr = 1;
		if (Conversion.Str(0) == MINUS) {
			num.sign = MINUS;
		} else {
			num.sign = PLUS;
			if (!(Conversion.Str(0) == PLUS))
				ptr = 0;
		}

		// now we start all over again with the counting :(
		// skip leading zeros again
		while (Conversion.Str(ptr) == '0') {
			ptr += 1;
		}

		// Everything before the decimal
		nptr = 0;
		// destination pointer
		if (zero_int) {
			num[0] = 0;
			nptr += 1;
			digits = 0;
		}
		while (digits > 0) {
			num[nptr] = Convert.ToByte(Conversion.Val(Conversion.Str(ptr)));
			nptr += 1;
			ptr += 1;
			digits -= 1;
		}

		// Build the fractional part
		if (strscale > 0) {
			ptr += 1;
			// skip the decimal point!
			while (strscale > 0) {
				num[nptr] = Convert.ToByte(Conversion.Val(Conversion.Str(ptr)));
				nptr += 1;
				ptr += 1;
				strscale -= 1;
			}
		}

		// Finally, return the result
		return num;
	}

	/// <summary>Determines if a number (up to Long) is odd or even</summary>
	/// <param name="a">The number to see if it is odd</param>
	public static bool Odd(long a)
	{
		return (a & 1) > 0;
	}

	/// <summary>Determine if the arbitrary precision number specified is zero or not</summary>
	/// <param name="num">The number to check</param>
	public static bool IsZero(BCNum num)
	{
		int count = num.length + num.scale;
		int nptr = 0;
		while (count > 0 & num[nptr] == 0) {
			nptr += 1;
			count -= 1;
		}
		return count == 0;
	}

	/// <summary>Determine if the arbitrary precision number specified is one</summary>
	/// <param name="num">The number to check</param>
	public static bool IsOne(BCNum num)
	{
		int i = 0;
		for (i = num.length; i <= num.length + num.scale - 1; i++) {
			if (num[i] != 0)
				return false;
			// Decimal found
		}
		for (i = 0; i <= num.length - 2; i++) {
			if (num[i] != 0)
				return false;
			// Non-zero before last number
		}
		return num[num.length - 1] == 1;
		// If the last digit is one
	}

	/// <summary>Inverts the sign (- => +, + => -)</summary>
	/// <param name="sign">The character with the sign to invert</param>
	public static char InvertSign(char sign)
	{
		return sign == PLUS ? MINUS : PLUS;
	}
}