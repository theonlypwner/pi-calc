using System;
using System.Collections.Generic;
using System.Text;

namespace Pi
{
	class CalculatePi
	{
		public int precision;
		public CalculatePi(int p) {
			precision = p + 3;
		}
		BigDec result, buffer;
		void process() {
			int limit = (int)Math.Ceiling(precision / 14d - 1d);
			result = 0;
			buffer = 0;
			for (int i = 0; i < limit; i++) {
				buffer = 0;
			}
			/*
		result = bcdiv(1, (bcmul(12, (result))), precision)
		RaiseEvent onComplete(Me, New EventArgs)
			 */
		}
	}
}
