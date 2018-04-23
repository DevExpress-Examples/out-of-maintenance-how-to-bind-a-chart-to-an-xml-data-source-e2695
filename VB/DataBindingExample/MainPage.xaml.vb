Imports Microsoft.VisualBasic
#Region "#code"
Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows.Controls
Imports System.Xml.Linq
Imports DevExpress.Xpf.Charts
'... 

Namespace DataBindingExample
	Public Class GSP
		Private ReadOnly region_Renamed As String
		Private ReadOnly year_Renamed As String
		Private ReadOnly product_Renamed As Double

		Public ReadOnly Property Region() As String
			Get
				Return region_Renamed
			End Get
		End Property
		Public ReadOnly Property Year() As String
			Get
				Return year_Renamed
			End Get
		End Property
		Public ReadOnly Property Product() As Double
			Get
				Return product_Renamed
			End Get
		End Property

		Public Sub New(ByVal region As String, ByVal year As String, ByVal product As Double)
			Me.region_Renamed = region
			Me.year_Renamed = year
			Me.product_Renamed = product
		End Sub
	End Class

	Partial Public Class MainPage
		Inherits UserControl
		Public Sub New()
			InitializeComponent()
			If diagram IsNot Nothing Then
				Dim dataSource As List(Of GSP) = CreateDataSource()
				For Each series As Series In diagram.Series
					series.DataSource = dataSource
				Next series
			End If
		End Sub
		Private Function CreateDataSource() As List(Of GSP)
			Dim document As XDocument = XDocument.Load("GSP.xml")
			Dim dataSource As New List(Of GSP)()
			If document IsNot Nothing Then
				For Each element As XElement In document.Element("GSPs").Elements()
					Dim region As String = element.Element("Region").Value
					Dim year As String = element.Element("Year").Value
					Dim product As Double = _ 
					Convert.ToDouble(element.Element("Product").Value, CultureInfo.InvariantCulture)
					dataSource.Add(New GSP(region, year, product))
				Next element
			End If
			Return dataSource
		End Function
	End Class
End Namespace
#End Region ' #code
