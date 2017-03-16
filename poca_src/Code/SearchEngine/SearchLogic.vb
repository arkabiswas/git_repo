Imports AdaptiveAlgorithm
Imports Microsoft.VisualBasic.CompilerServices
Imports PPC.LASA.Phonetic
'Imports SpellingAlgorithm
Imports System
Imports System.Collections
Imports System.Data
Imports System.Diagnostics
Imports System.Collections.Generic

Public Class SearchLogic
    ' Methods
    Public Sub New()
    End Sub

    Private Shared Function CalculateAddFax(ByVal Engine As SearchEngine, ByVal DBName As String, ByVal PhoneticValue As Integer, ByVal OrthoValue As Integer) As Integer
        Dim num1 As Double = SearchEngineData.GetAdditionalFactorsScore(Engine.SearchTerm, DBName, "tuckerj")
        Return CType(Math.Round(CType(((((CType((Engine.PhoneticWeight * PhoneticValue), Double) / CType(Engine.PhoneticWeight, Double)) + (CType((Engine.OrthographicWeight * OrthoValue), Double) / CType(Engine.OrthographicWeight, Double))) + ((Engine.AdditionalFactorsWeight * num1) / CType(Engine.AdditionalFactorsWeight, Double))) / 100), Double)), Integer)
    End Function

    'Private Shared Sub CreateNewRow(ByRef returntable As ScoredNames.ScoredNamesDataTable, ByVal Name As String, ByVal OrthoValue As Integer, ByVal PhoneticValue As Integer, ByVal MergedValue As Integer, ByVal RecordSourceId As String, ByVal ProductNameUid As String)
    'Private Shared Sub CreateNewRow(ByRef returntable As ScoredNames.ScoredNamesDataTable, ByVal Name As String, ByVal OrthoValue As Integer, ByVal PhoneticValue As Integer, ByVal MergedValue As Integer, ByVal SpellingValue As Integer, ByVal RecordSourceId As String, ByVal ProductNameUid As String, ByVal ProductDetail As String)
    'Private Shared Sub CreateNewRow(ByRef returntable As ScoredNames.ScoredNamesDataTable, ByVal Name As String, ByVal OrthoValue As Integer, ByVal ReverseOrthoValue As Integer, ByVal PhoneticValue As Integer, ByVal ReversePhoneticValue As Integer, ByVal MergedValue As Integer, ByVal ReverseMergedValue As Integer, ByVal SpellingValue As Integer, ByVal RecordSourceId As String, ByVal ProductNameUid As String, ByVal ProductDetail As String)
    'Private Shared Sub CreateNewRow(ByRef returntable As ScoredNames.ScoredNamesDataTable, ByVal Name As String, ByVal OrthoValue As Integer, ByVal PhoneticValue As Integer, ByVal MergedValue As Integer, ByVal RecordSourceId As String, ByVal ProductNameUid As String, ByVal ProductDetail As String)
    'Private Shared Sub CreateNewRow(ByRef returntable As ScoredNames.ScoredNamesDataTable, ByVal Name As String, ByVal OrthoValue As Integer, ByVal PhoneticValue As Integer, ByVal MergedValue As Integer, ByVal RecordSourceId As String, ByVal ProductNameUid As String, ByVal ProductDetail As String, ByVal IsDeleted As String)
    Private Shared Sub CreateNewRow(ByRef returntable As ScoredNames.ScoredNamesDataTable, ByVal Name As String, ByVal OrthoValue As Integer, ByVal PhoneticValue As Integer, ByVal MergedValue As Integer, ByVal RecordSourceId As String, ByVal ProductNameUid As String, ByVal ActiveIngredient As String, ByVal DosageForm As String, ByVal Potency As String, ByVal Route As String, ByVal IsDeleted As String)
        Dim row1 As ScoredNames.ScoredNamesRow = returntable.NewScoredNamesRow
        row1.Name = Name
        row1.PhoneticScore = PhoneticValue
        row1.OrthographicScore = OrthoValue
        row1.MergedScore = MergedValue
        row1.RecordSourceId = RecordSourceId
        row1.ProductNameUid = ProductNameUid
        row1.ActiveIngredient = ActiveIngredient
        row1.DosageForm = DosageForm
        row1.Potency = Potency
        row1.Route = Route
        row1.IsDeleted = IsDeleted

        returntable.Rows.Add(row1)
    End Sub

    Public Shared Function PerformSearch(ByVal ThisSearchEngine As SearchEngine, ByVal NamesList As DataSet) As String
        Dim NameRow As DataRow
        Dim CreateRow As Boolean = False
        Dim names1 As New ScoredNames
        Dim table1 As ScoredNames.ScoredNamesDataTable = names1.ScoredNames
        Dim aline1 As New Aline(ThisSearchEngine.SearchTermPhonetic)
        Dim graphic1 As New OrthoGraphic

        'New Spelling Search option
        'Dim spelling1 As New SpellingAlgo
        'Dim adjust As Double = Convert.ToDouble(ConfigurationManager.AppSettings("SpellingScoreAdjustment"))

        Try
            For Each NameRow In NamesList.Tables(0).Rows
                Dim mergedScore As Integer
                Dim orthoScore As Integer
                Dim phoneticScore As Double
                'Dim reverseMergedScore As Integer
                'Dim reverseOrthoScore As Integer
                'Dim reversePhoneticScore As Double
                'Dim spellingScore As Integer

                If ThisSearchEngine.PhoneticSearch Then
                    Try
                        phoneticScore = aline1.GetPercentage(aline1.GetPhoneticMatch(SearchEngine.ConvertAline(NameRow("u_name_normalized").ToString)))
                        If (phoneticScore >= ThisSearchEngine.ResultThreshold) Then CreateRow = True

                        'If CreateRow Then
                        '    'Just compare
                        '    Dim aline2 As New Aline(SearchEngine.ConvertEnglish(SearchEngine.NormalizeString(ThisSearchEngine.SearchTerm)))
                        '    Dim phoneticScore2 As Integer = aline2.GetPercentage(aline2.GetPhoneticMatch(SearchEngine.ConvertAline(NameRow("u_name_normalized").ToString)))
                        '    Trace.WriteLine("phoneticScore: " + phoneticScore.ToString + "     And phoneticScore2: " + phoneticScore2.ToString + Environment.NewLine)

                        '    'Dim aline3 As New Aline(SearchEngine.ConvertEnglish(SearchEngine.NormalizeString(ComparatorDrug)))
                        '    'Dim reversePhoneticScoreAbsolute As Integer = aline3.GetPhoneticMatch(SearchEngine.ConvertAline(SearchEngine.NormalizeString(CandidateDrug)))
                        '    'reversePhoneticScore = aline3.GetPercentage(reversePhoneticScoreAbsolute)

                        '    'Dim aline3 As New Aline(SearchEngine.ConvertEnglish(SearchEngine.NormalizeString(NameRow("u_name_normalized").ToString)))
                        '    'reversePhoneticScore = aline3.GetPercentage(aline3.GetPhoneticMatch(SearchEngine.ConvertAline(ThisSearchEngine.SearchTermPhonetic)))
                        '    Dim aline3 As New Aline(SearchEngine.ConvertEnglish(NameRow("u_name_normalized").ToString))
                        '    Dim reversePhoneticScoreAbsolute As Integer = aline3.GetPhoneticMatch(SearchEngine.ConvertAline(SearchEngine.NormalizeString(ThisSearchEngine.SearchTerm)))
                        '    reversePhoneticScore = aline3.GetPercentage(reversePhoneticScoreAbsolute)
                        '    If phoneticScore > reversePhoneticScore Then
                        '        reversePhoneticScore = phoneticScore
                        '    End If
                        'End If
                    Catch exception1 As IndexOutOfRangeException
                        Trace.WriteLine(String.Format("{0} on word {1}", exception1.Message, NameRow("u_name_normalized".ToString)))
                    End Try
                End If

                If ThisSearchEngine.OrthoSearch Then
                    Try
                        orthoScore = CType(Math.Round(Math.Round(CType((graphic1.score(ThisSearchEngine.SearchTermNormal, SearchEngine.NormalizeString(NameRow("u_name").ToString)) * 100), Double))), Integer)
                        If (orthoScore >= ThisSearchEngine.ResultThreshold) Then CreateRow = True

                        'If CreateRow Then
                        '    reverseOrthoScore = CType(Math.Round(Math.Round(CType((graphic1.score(SearchEngine.NormalizeString(NameRow("u_name").ToString), ThisSearchEngine.SearchTermNormal) * 100), Double))), Integer)
                        '    If orthoScore > reverseOrthoScore Then
                        '        reverseOrthoScore = orthoScore
                        '    End If
                        'End If
                    Catch ex As Exception
                        Trace.WriteLine("Ortho Error: " + ex.Message + Environment.NewLine + ex.StackTrace)
                        Trace.WriteLine(String.Format("Ortho Error Words {0} compared {1}", ThisSearchEngine.SearchTermNormal, SearchEngine.NormalizeString(NameRow("u_name").ToString)))
                    End Try
                End If

                'If ThisSearchEngine.SpellingSearch Then
                '    Try
                '        'Spelling Score from Spelling Algorithm
                '        'spellingScore = CType(Math.Round(Math.Round(CType((spelling1.score(ThisSearchEngine.SearchTermNormal, SearchEngine.NormalizeString(NameRow("u_name").ToString)) * 100), Double))), Integer)
                '        'If (spellingScore > ThisSearchEngine.ResultThreshold) Then CreateRow = True

                '        'Get Ortho score
                '        orthoScore = CType(Math.Round(Math.Round(CType((graphic1.score(ThisSearchEngine.SearchTermNormal, SearchEngine.NormalizeString(NameRow("u_name").ToString)) * 100), Double))), Integer)
                '        If (orthoScore > ThisSearchEngine.ResultThreshold) Then CreateRow = True

                '        'Spelling Score from OrthoAlgorithm minus ConfusionMatrix
                '        Dim term1 As String = ThisSearchEngine.SearchTermNormal.Substring(0, 2)
                '        Dim term2 As String = SearchEngine.NormalizeString(NameRow("u_name").ToString).Substring(0, 2)
                '        If term1.ToUpper = term2.ToUpper Then
                '            spellingScore = orthoScore + (orthoScore * adjust / 100)
                '            If spellingScore > 100 Then
                '                spellingScore = 100
                '            End If
                '        Else
                '            spellingScore = orthoScore
                '        End If
                '        'If (spellingScore > ThisSearchEngine.ResultThreshold) Then CreateRow = True
                '    Catch ex As Exception
                '        Trace.WriteLine("Spelling Error: " + ex.Message + Environment.NewLine + ex.StackTrace)
                '        Trace.WriteLine(String.Format("Spelling Error Words {0} compared {1}", ThisSearchEngine.SearchTermNormal, SearchEngine.NormalizeString(NameRow("u_name").ToString)))
                '    End Try
                'End If

                If ((ThisSearchEngine.OrthoSearch And ThisSearchEngine.PhoneticSearch) And CreateRow) Then
                    If ThisSearchEngine.AddFaxSearch Then
                        mergedScore = SearchLogic.CalculateAddFax(ThisSearchEngine, NameRow("u_name").ToString, CType(Math.Round(phoneticScore), Integer), orthoScore)
                    Else
                        mergedScore = CType(Math.Round(CType(((orthoScore + phoneticScore) / 2), Double)), Integer)
                        'reverseMergedScore = CType(Math.Round(CType(((reverseOrthoScore + reversePhoneticScore) / 2), Double)), Integer)
                    End If
                End If

                If CreateRow Then
                    'SearchLogic.CreateNewRow(table1, NameRow("u_name").ToString, orthoScore, CType(Math.Round(phoneticScore), Integer), mergedScore, NameRow("i_record_source_id").ToString(), NameRow("ui_mr_product_name_uid").ToString())

                    Dim inDataSources As String = NameRow("i_record_source_id").ToString()
                    Dim outDataSources As String = GetDistinct(inDataSources, ";")

                    Dim isDeleted As String = NameRow("dt_deleted").ToString()
                    If isDeleted = String.Empty Then
                        isDeleted = "N"
                    Else
                        isDeleted = "Y"
                    End If

                    Dim active_ingredient As String = NameRow("active_ingredient_list").ToString()
                    Dim dosageForm As String = NameRow("dosage_form_list").ToString()
                    Dim potency As String = NameRow("potency_list").ToString()
                    Dim route As String = NameRow("route_list").ToString()

                    'SearchLogic.CreateNewRow(table1, NameRow("u_name").ToString, orthoScore, CType(Math.Round(phoneticScore), Integer), mergedScore, outDataSources, NameRow("ui_mr_product_name_uid").ToString(), "")
                    'SearchLogic.CreateNewRow(table1, NameRow("u_name").ToString, orthoScore, CType(Math.Round(phoneticScore), Integer), mergedScore, outDataSources, NameRow("ui_mr_product_name_uid").ToString(), NameRow("list_details").ToString())
                    'SearchLogic.CreateNewRow(table1, NameRow("u_name").ToString, orthoScore, CType(Math.Round(phoneticScore), Integer), mergedScore, outDataSources, NameRow("ui_mr_product_name_uid").ToString(), NameRow("list_details").ToString(), isDeleted)
                    SearchLogic.CreateNewRow(table1, NameRow("u_name").ToString, orthoScore, CType(Math.Round(phoneticScore), Integer), mergedScore, outDataSources, NameRow("ui_mr_product_name_uid").ToString(), active_ingredient, dosageForm, potency, route, isDeleted)
                    'SearchLogic.CreateNewRow(table1, NameRow("u_name").ToString, orthoScore, CType(Math.Round(phoneticScore), Integer), mergedScore, spellingScore, outDataSources, NameRow("ui_mr_product_name_uid").ToString(), NameRow("list_details").ToString())
                    'SearchLogic.CreateNewRow(table1, NameRow("u_name").ToString, orthoScore, reverseOrthoScore, CType(Math.Round(phoneticScore), Integer), reversePhoneticScore, mergedScore, reverseMergedScore,
                    '                         spellingScore, outDataSources, NameRow("ui_mr_product_name_uid").ToString(), NameRow("list_details").ToString())
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
                row2.RecordSourceId = GetDistinct(reader1(2).ToString(), ";")
                'row2.ProductNameUid = reader1.GetString(3)
                row2.ProductNameUid = ""
                'If Not reader1.IsDBNull(reader1.GetOrdinal("LIST_DETAILS")) Then
                If Not reader1.IsDBNull(reader1.GetOrdinal("active_ingredient_list")) Then
                    'row2.ProductDetail = reader1.GetString(3)
                    row2.ActiveIngredient = reader1.GetString(7)
                End If
                If Not reader1.IsDBNull(reader1.GetOrdinal("dosage_form_list")) Then
                    row2.DosageForm = reader1.GetString(4)
                End If
                If Not reader1.IsDBNull(reader1.GetOrdinal("potency_list")) Then
                    row2.Potency = reader1.GetString(5)
                End If
                If Not reader1.IsDBNull(reader1.GetOrdinal("route_list")) Then
                    row2.Route = reader1.GetString(6)
                End If
                If Not reader1.IsDBNull(reader1.GetOrdinal("dt_deleted")) Then
                    'row2.IsDeleted = reader1.GetDate(1)
                    row2.IsDeleted = "Y"
                Else
                    row2.IsDeleted = "N"
                End If
                table2.Rows.Add(row2)
            Loop
            reader1.Close()
        End If
        Return names1.GetXml
    End Function

    Private Shared Function GetDistinct(ByVal input As String, ByVal delimiter As String) As String
        Dim parts() As String = input.Split(delimiter)
        Dim result As New List(Of String)

        result.Add(parts(0))
        For i As Integer = 1 To parts.Length - 1
            If Not result.Contains(parts(i).Trim()) Then
                result.Add(parts(i).Trim())
            End If
        Next

        Return String.Join(delimiter, result.ToArray())
    End Function

    Public Shared Function PerformDirectSearch(ByVal CandidateDrug As String, ByVal ComparatorDrug As String, ByVal PO As PPC.FDA.Person.PersonObject) As String
        Dim retVal As String = ""

        Try
            Dim mergedScore As Integer
            Dim orthoScore As Integer
            Dim phoneticScore As Double

            'Phonetic Score
            Try
                Dim aline1 As New Aline(SearchEngine.ConvertEnglish(SearchEngine.NormalizeString(CandidateDrug)))
                'Dim phoneticScoreAbsolute As Integer = aline1.GetPhoneticMatch(SearchEngine.ConvertAline(SearchEngine.NormalizeString(ComparatorDrug)))
                Dim phoneticScoreAbsolute As Integer = aline1.GetPhoneticMatch(SearchEngine.ConvertEnglish(SearchEngine.NormalizeString(ComparatorDrug)))
                If phoneticScoreAbsolute < 0 Then
                    phoneticScore = 0
                Else
                    phoneticScore = aline1.GetPercentage(phoneticScoreAbsolute)
                End If
            Catch exception1 As IndexOutOfRangeException
                Trace.WriteLine(String.Format("{0} on word {1}", exception1.Message, ComparatorDrug))
            End Try

            'Orthographic Score
            Dim graphic1 As New OrthoGraphic
            Try
                orthoScore = CType(Math.Round(Math.Round(CType((graphic1.score(SearchEngine.NormalizeString(CandidateDrug), SearchEngine.NormalizeString(ComparatorDrug)) * 100), Double))), Integer)
                If orthoScore < 0 Then
                    orthoScore = 0
                End If
            Catch ex As Exception
                Trace.WriteLine("Ortho Error: " + ex.Message + Environment.NewLine + ex.StackTrace)
                Trace.WriteLine(String.Format("Ortho Error Words {0} compared {1}", SearchEngine.NormalizeString(CandidateDrug), SearchEngine.NormalizeString(ComparatorDrug)))
            End Try

            'Combined Score
            mergedScore = CType(Math.Round(CType(((orthoScore + phoneticScore) / 2), Double)), Integer)

            'RetVal
            retVal = mergedScore.ToString + ";" + orthoScore.ToString + ";" + phoneticScore.ToString

        Catch SearchException As Exception
            Trace.WriteLine(SearchException.Message + Environment.NewLine + SearchException.StackTrace)
        End Try

        Return retVal
    End Function

End Class
