Imports AdaptiveAlgorithm
Imports Microsoft.VisualBasic.CompilerServices
Imports PPC.LASA.Phonetic
Imports System
Imports System.Collections
Imports System.Data
Imports System.Diagnostics

Public Class SearchLogic
    ' Methods
    Public Sub New()
    End Sub

    Private Shared Function CalculateAddFax(ByVal Engine As SearchEngine, ByVal DBName As String, ByVal PhoneticValue As Integer, ByVal OrthoValue As Integer) As Integer
        Dim num1 As Double = SearchEngineData.GetAdditionalFactorsScore(Engine.SearchTerm, DBName, "tuckerj")
        Return CType(Math.Round(CType(((((CType((Engine.PhoneticWeight * PhoneticValue), Double) / CType(Engine.PhoneticWeight, Double)) + (CType((Engine.OrthographicWeight * OrthoValue), Double) / CType(Engine.OrthographicWeight, Double))) + ((Engine.AdditionalFactorsWeight * num1) / CType(Engine.AdditionalFactorsWeight, Double))) / 100), Double)), Integer)
    End Function

    Private Shared Sub CreateNewRow(ByRef returntable As ScoredNames.ScoredNamesDataTable, ByVal Name As String, ByVal OrthoValue As Integer, ByVal PhoneticValue As Integer, ByVal MergedValue As Integer, ByVal RecordSourceId As String, ByVal ProductNameUid As String)
        Dim row1 As ScoredNames.ScoredNamesRow = returntable.NewScoredNamesRow
        row1.Name = Name
        row1.PhoneticScore = PhoneticValue
        row1.OrthographicScore = OrthoValue
        row1.MergedScore = MergedValue
        row1.RecordSourceId = RecordSourceId
        row1.ProductNameUid = ProductNameUid

        returntable.Rows.Add(row1)
    End Sub

    Public Shared Function PerformSearch(ByVal ThisSearchEngine As SearchEngine, ByVal NamesList As DataSet) As String
        Dim NameRow As DataRow
        Dim CreateRow As Boolean = False
        Dim names1 As New ScoredNames
        Dim table1 As ScoredNames.ScoredNamesDataTable = names1.ScoredNames
        Dim aline1 As New Aline(ThisSearchEngine.SearchTermPhonetic)
        Dim graphic1 As New OrthoGraphic

        Try
            For Each NameRow In NamesList.Tables(0).Rows
                Dim num1 As Integer
                Dim num2 As Integer
                Dim num3 As Double

                If ThisSearchEngine.PhoneticSearch Then
                    Try
                        num3 = aline1.GetPercentage(aline1.GetPhoneticMatch(SearchEngine.ConvertAline(NameRow("u_name_normalized").ToString)))
                        If (num3 > ThisSearchEngine.ResultThreshold) Then CreateRow = True
                    Catch exception1 As IndexOutOfRangeException
                        Trace.WriteLine(String.Format("{0} on word {1}", exception1.Message, NameRow("u_name_normalized".ToString)))
                    End Try
                End If
                If ThisSearchEngine.OrthoSearch Then
                    Try
                        num2 = CType(Math.Round(Math.Round(CType((graphic1.score(ThisSearchEngine.SearchTermNormal, SearchEngine.NormalizeString(NameRow("u_name").ToString)) * 100), Double))), Integer)
                        If (num2 > ThisSearchEngine.ResultThreshold) Then CreateRow = True
                    Catch ex As Exception
                        Trace.WriteLine("Ortho Error: " + ex.Message + Environment.NewLine + ex.StackTrace)
                        Trace.WriteLine(String.Format("Ortho Error Words {0} compared {1}", ThisSearchEngine.SearchTermNormal, SearchEngine.NormalizeString(NameRow("u_name").ToString)))
                    End Try
                End If
                If ((ThisSearchEngine.OrthoSearch And ThisSearchEngine.PhoneticSearch) And CreateRow) Then
                    If ThisSearchEngine.AddFaxSearch Then
                        num1 = SearchLogic.CalculateAddFax(ThisSearchEngine, NameRow("u_name").ToString, CType(Math.Round(num3), Integer), num2)
                    Else
                        num1 = CType(Math.Round(CType(((num2 + num3) / 2), Double)), Integer)
                    End If
                End If
                If CreateRow Then
                    SearchLogic.CreateNewRow(table1, NameRow("u_name").ToString, num2, CType(Math.Round(num3), Integer), num1, NameRow("i_record_source_id").ToString(), NameRow("ui_mr_product_name_uid").ToString())
                    CreateRow = False
                End If
            Next

        Catch SearchException As Exception
            Trace.WriteLine(SearchException.Message + Environment.NewLine + SearchException.StackTrace)
        End Try

        If ThisSearchEngine.TextSearch Then
            Dim table2 As ScoredNames.TextResultsDataTable = names1.TextResults
            Dim reader1 As IDataReader = SearchEngineData.TextSearch(ThisSearchEngine.SearchTerm, ThisSearchEngine.DataSources)
            Do While reader1.Read
                Dim row2 As ScoredNames.TextResultsRow = table2.NewTextResultsRow
                row2.Name = reader1.GetString(0)
                row2.RecordSourceId = reader1(3).ToString()
                row2.ProductNameUid = reader1.GetString(4)
                table2.Rows.Add(row2)
            Loop
            reader1.Close()
        End If
        Return names1.GetXml
    End Function

End Class
