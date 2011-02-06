using System;
using System.Collections.Generic;
using System.Text;
using java.math;

namespace Pi
{
	/// <summary>Provides an improved interface to BigDecimal.</summary>
	class BigDec : BigDecimal
	{
		// members
		/// <summary>The hardcoded type of rounding to use.</summary>
		public const int roundType = ROUND_HALF_UP;
		public const IFormatProvider formatter = null;
		public static int defaultPrecision = 32;

		// constructs
		/// <summary>Creates a BigDec with the value of zero.</summary>
		public BigDec() : base(0) { }
		/// <summary>Creates a BigDec with the value of the specified long integer.</summary>
		public BigDec(long i) : base(i) { setScale(defaultPrecision, roundType);  }
		/// <summary>Creates a BigDec with the value of the specified double-precision floating point.</summary>
		public BigDec(double i) : base(i) { setScale(defaultPrecision, roundType); }

		// class overrides
		/// <summary>Compares a BigDec to this instance.</summary>
		/// <param name="d">The BigDec to compare this to.</param>
		/// <returns>True if the values are the same, false otherwise.</returns>
		public bool Equals(BigDec d) { return this == d; }
		public override bool Equals(object val) { return val is BigDec && this == (BigDec)val; }
		public override int GetHashCode() { return intValue() ^ scale(); }

		// conversions
		public static implicit operator BigDec(long i) { return new BigDec(i); }
		public static implicit operator BigDec(double i) { return new BigDec(i); }
		public static explicit operator long(BigDec i) { return i.longValue(); }
		public static explicit operator double(BigDec i) { return i.doubleValue(); }
		public static explicit operator string(BigDec i) { return i.ToString(formatter); }

		// comparison operators
		public static bool operator ==(BigDec a, BigDec b){ return a.compareTo(b) == 0; }
		public static bool operator <(BigDec a, BigDec b) { return a.compareTo(b) == -1; }
		public static bool operator >(BigDec a, BigDec b) { return a.compareTo(b) == 1; }
		// negate of the above
		public static bool operator !=(BigDec a, BigDec b) { return !(a == b); }
		public static bool operator >=(BigDec a, BigDec b) { return !(a < b); }
		public static bool operator <=(BigDec a, BigDec b) { return !(a > b); }
		public static bool operator !(BigDec a) { return a == 0; }

		// unary plus
		public static BigDec operator +(BigDec a){ return a; }
		// negate
		public static BigDec operator -(BigDec a){
			BigDec result = a;
			result.negate();
			return result;
		}
		// arithmetic operators
		public static BigDec operator +(BigDec a, BigDec b) {
			BigDec result = a;
			result.add(b);
			return result;
		}
		public static BigDec operator -(BigDec a, BigDec b)
		{
			BigDec result = a;
			result.subtract(b);
			return result;
		}
		public static BigDec operator *(BigDec a, BigDec b)
		{
			BigDec result = a;
			result.multiply(b);
			return result;
		}
		public static BigDec operator /(BigDec a, BigDec b)
		{
			BigDec result = a;
			result.divide(b, defaultPrecision, roundType);
			return result;
		}
		public static BigDec operator ^(BigDec a, int b)
		{
			if (!a || b == 1) return a; // zero raised to anything OR anything raised to one
			if (b == 0) return 1; // zero exponents are one
			BigDec ret = a;
			int remaining = Math.Abs(b);
			while (--remaining > 0) ret *= a;
			if (b < 0) ret = 1 / ret; // negative exponents
			return ret;
		}

		/// <summary>Calculates a factorial based on an unsigned number.</summary>
		/// <param name="i">The number to calculate the factorial of.</param>
		/// <returns>A BigDec with the calculated factorial.</returns>
		public static BigDec Factorial(ulong i)
		{
			if (i <= 2) return i == 2 ? 2 : 1;
			return i * Factorial(i - 1);
		}
		/// <summary>Converts the signed number to unsigned and calculates the factorial</summary>
		/// <param name="i">The number</param>
		/// <returns>The result of Factorial(i) where i is casted to (ulong)i</returns>
		public static BigDec Factorial(long i) { return Factorial((ulong)i); }
	}
}
