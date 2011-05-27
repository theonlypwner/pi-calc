using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
public partial class libbcmath
{
	/// <summary>Creates a List big enough for the array</summary>
	/// <param name="size">The size of each block in bytes</param>
	/// <param name="len">The number of blocks</param>
	/// <param name="extra">The extra bytes to allocate</param>
	private static List<byte> safe_emalloc(int size, int len = 1, int extra = 0)
	{
		return new List<byte>(size * len + extra);
	}

	/// <summary>Sets a block of memory (given array) to a specified value</summary>
	/// <param name="src">The array that will be modified by reference</param>
	/// <param name="ptr">The starting point to start the set</param>
	/// <param name="chr">The character (byte) to fill the data</param>
	/// <param name="len">The length to fill</param>
	private static void memset(ref List<byte> src, int ptr, byte chr, int len)
	{
		for (int i = ptr; i < len + ptr; i++) src[i] = chr;
	}

	/// <summary>Copys a block of memory data (from an array to another)</summary>
	/// <param name="dest">The destination to write to, passed by reference</param>
	/// <param name="ptr">The offset of <paramref name="dest" /></param>
	/// <param name="src">The source to read from</param>
	/// <param name="srcptr">The offset of <paramref name="src" /></param>
	/// <param name="len">The number of bytes to copy</param>
	private static void memcpy(ref List<byte> dest, int ptr, List<byte> src, int srcptr, int len)
	{
		for (int i = 0; i < len; i++) src[ptr + i] = src[srcptr + i];
	}
}