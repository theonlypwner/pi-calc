using System;
using System.Collections.Generic;
using System.Text;
using java.math;

namespace Pi
{
	class BigDec : BigDecimal
	{
		public BigDec() : base(0) { }

		// class definition
		public static implicit operator BigDec(int i)
		{
			BigDecimal r = new BigDecimal(i);
			return (BigDec)r;
		}

		public static implicit operator BigDec(double d) {
			BigDecimal r = new BigDecimal(d);
			return (BigDec)r;
		}

		public bool Equals(BigDec d) { return this == d; }
		public override bool Equals(object val) { return val is BigDec && this == (BigDec)val; }
		public override int GetHashCode() { return intValue() ^ scale(); }

		// comparison operators
		public static bool operator ==(BigDec a, BigDec b){ return a.compareTo(b) == 0; }
		public static bool operator !=(BigDec a, BigDec b) { return !(a == b); }

		public static bool operator >(BigDec a, BigDec b) { return a.compareTo(b) == 1; }
		public static bool operator <(BigDec a, BigDec b) { return a.compareTo(b) == -1; }
		public static bool operator >=(BigDec a, BigDec b) { return a > b || a == b; }
		public static bool operator <=(BigDec a, BigDec b) { return a < b || a == b; }

		// arithmetic operators
		public static BigDec operator -(BigDec a){
			BigDec result = a;
			result.negate();
			return result;
		}
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

		// factorial
		public static BigDec Factorial(ulong i)
		{
			return 1;
		}
	}
}
