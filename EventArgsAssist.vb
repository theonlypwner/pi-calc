''' <summary>Holds an object for an event that is compatiable with System.EventArgs</summary>
''' <typeparam name="T">Type of the variable</typeparam>
''' <remarks>Generic code for almost any application</remarks>
Public Class EventArgs(Of T)
	Inherits System.EventArgs
	Private data As T

	''' <param name="v">Default value for the first variable</param>
	Public Sub New(ByVal v As T)
		data = v
	End Sub

	''' <summary>Provides access to the variable</summary>
	Public Property Value() As T
		Set(ByVal v As T)
			data = v
		End Set
		Get
			Return data
		End Get
	End Property
End Class

''' <summary>Holds two objects for an event that is compatiable with System.EventArgs</summary>
''' <typeparam name="T">Type of the first variable</typeparam>
''' <typeparam name="U">Type if the second variable</typeparam>
''' <remarks>Generic code for almost any application, but less; most only need one variable</remarks>
Public Class EventArgs(Of T, U)
	Inherits EventArgs(Of T)
	Private data2 As U

	''' <param name="v">Default value for the first variable</param>
	''' <param name="v2">Default value for the second variable</param>
	Public Sub New(ByVal v As T, ByVal v2 As U)
		MyBase.New(v)
		data2 = v2
	End Sub

	''' <summary>Provides access to the second variable</summary>
	Public Property SecondValue() As U
		Set(ByVal v As U)
			data2 = v
		End Set
		Get
			Return data2
		End Get
	End Property
End Class
