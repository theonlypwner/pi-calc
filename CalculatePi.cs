using System;
using System.Collections.Generic;
using System.Text;

namespace Pi
{
	/// <summary>Calculates Pi with the BigDec as storage</summary>
	class CalculatePi
	{
		public int precision;
		public CalculatePi(int p) {
			precision = p + 3;
		}

		public event EventHandler OnComplete;
		public event EventHandler<byte> onProgress;

		BigDec result, buffer;
		void process() {
			int limit = (int)Math.Ceiling(precision / 14d - 1d);
			result = buffer = 0;
			result.setScale(limit);
			buffer.setScale(limit);
			for (int i = 0; i < limit; i++) {
				buffer = BigDec.Factorial(4 * i);
				buffer *= 1103 + 26390 * i;
				buffer /= (BigDec.Factorial(i) * 396 ^ i) ^ 4;
				result += buffer;
				onProgress(this, (byte)(i / limit / 100));
			}
			result = 1 / (result * Math.Sqrt(2) / 4900.5); // r * 2 / 9081 = r / 4900.5
			OnComplete(this, null);
		}
	}
}
