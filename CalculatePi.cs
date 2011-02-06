using System;
using System.Collections.Generic;
using System.Text;

namespace Pi
{
	public delegate void ByteHandler(byte b);

	/// <summary>Calculates Pi with the BigDec as storage</summary>
	public class CalculatePi
	{
		public const string CrLf = "\r\n";
		/// <summary>The time at which the calculation started</summary>
		public long startTime;
		protected int precision;
		public string final;


		public const int extraDigits = 3;

		/// <summary>Creates a new CalculatePi with the specified precision</summary>
		/// <param name="p">The precision to use</param>
		public CalculatePi(int p) {
			precision = p + extraDigits;
		}
		
		
		// events
		/// <summary>Raised upon completion</summary>
		public event EventHandler onComplete;
		/// <summary>Raised during the calculation</summary>
		public event ByteHandler onProgress;

		public void process() {
			// store start time
			startTime = DateTime.Now.Ticks; // 1 tick = 100-nanoseconds = tenth-microsecond
			// create buffers
			BigDec result, buffer;
			int limit = (int)Math.Ceiling(precision / 14d - 1d), checkinterval = Math.Max(limit / 100, 1);
			result = buffer = 0;
			result.setScale(limit);
			buffer.setScale(limit);
			// calculate
			for (int i = 0; i < limit; i++) {
				buffer = BigDec.Factorial(4 * i);
				buffer *= 1103 + 26390 * i;
				buffer /= (BigDec.Factorial(i) * 396 ^ i) ^ 4;
				result += buffer;
				if (onProgress != null && i % checkinterval == 0) onProgress((byte)(i * 100 / limit));
			}
			buffer = null; // empty buffer
			result = 1 / (result * Math.Sqrt(2) / 4900.5); // r * 2 / 9081 = r / 4900.5
			// store result
			final = result.ToString();
			result = null; // empty old result
			if(onComplete != null) onComplete(this, null); // raised completion event
		}

		/// <summary>Holds a result string with start timestamp and difference to finish</summary>
		public struct timedResult
		{
			public enum resultType : byte { BufferOnly, First2K, All }
			public string s;
			public long timeStart;
			public resultType type;
			public timedResult(resultType _type) { s = ""; timeStart = DateTime.Now.Ticks; type = _type; }
		}
		public timedResult ResultData(timedResult.resultType t = timedResult.resultType.BufferOnly) {
			timedResult ret = new timedResult(t);
			if (final.Length < 1) {
				ret.s = "No buffer";
				return ret;
			}
			else if(t == timedResult.resultType.BufferOnly){
				ret.s = "Buffer not displayed";
				return ret;
			}
			if (t == timedResult.resultType.First2K && precision - extraDigits > Program.MainForm1.KprecisionP() * 2) {
				ret.s = "Result is trimmed" + CrLf;
			}
			ret.s = ret.s.Substring(0, Program.MainForm1.KprecisionP() * 2 + 2);
			return ret;
		}
	}
}
