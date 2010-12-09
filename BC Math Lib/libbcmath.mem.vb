Partial Public Class libbcmath
	''' <summary>Creates a List big enough for the array</summary>
	''' <param name="size">The size of each block in bytes</param>
	''' <param name="len">The number of blocks</param>
	''' <param name="extra">The extra bytes to allocate</param>
	Private Shared Function safe_emalloc(ByVal size As Integer, Optional ByVal len As Integer = 1, Optional ByVal extra As Integer = 0) As List(Of Byte)
		Return New List(Of Byte)(size * len + extra)
	End Function

	''' <summary>Sets a block of memory (given array) to a specified value</summary>
	''' <param name="src">The array that will be modified by reference</param>
	''' <param name="ptr">The starting point to start the set</param>
	''' <param name="chr">The character (byte) to fill the data</param>
	''' <param name="len">The length to fill</param>
	Private Shared Sub memset(ByRef src As List(Of Byte), ByVal ptr As Integer, ByVal chr As Byte, ByVal len As Integer)
		Dim fill(len) As Byte
		For i As Integer = 0 To len - 1
			fill(i) = chr
		Next
		src.RemoveRange(ptr, len)
		src.InsertRange(ptr, fill)
	End Sub

	''' <summary>Copys a block of memory data (from an array to another)</summary>
	''' <param name="dest">The destination to write to, passed by reference</param>
	''' <param name="ptr">The offset of <paramref name="dest" /></param>
	''' <param name="src">The source to read from</param>
	''' <param name="srcptr">The offset of <paramref name="src" /></param>
	''' <param name="len">The number of bytes to copy</param>
	Private Shared Sub memcpy(ByRef dest As List(Of Byte), ByVal ptr As Integer, ByVal src As List(Of Byte), ByVal srcptr As Integer, ByVal len As Integer)
		dest.RemoveRange(ptr, len)
		dest.InsertRange(ptr, src.GetRange(srcptr, len))
	End Sub
End Class
