Imports PPC.FDA.Controls
Imports PPC.SearchEngine
Imports MetaBuilders.WebControls
Imports System.IO
Imports Microsoft.VisualBasic.CompilerServices
Imports Excel = Microsoft.Office.Interop.Excel
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports System.Globalization
Imports System.Threading


Partial Class Search
    Inherits Page

    Private searchEng As SearchEngine
    Private searchEngineResults As ScoredNames

    ' Methods
    Public Sub New()
        searchEngineResults = New ScoredNames
    End Sub

    'Replaced functionality for 508 Compliancy in grid
    'Private Sub GenerateSelectedItemsString(ByVal grid As DataGrid, ByRef qryString As String)
    '    Dim SelectedRows As RowSelectorColumn = RowSelectorColumn.FindColumn(grid)
    '    Dim SelectedRowIndex As Integer
    '    For Each SelectedRowIndex In SelectedRows.SelectedIndexes
    '        If Not qryString = String.Empty Then qryString += ","
    '        qryString += grid.DataKeys(SelectedRowIndex)
    '    Next
    'End Sub

    Private Sub GenerateSelectedItemsString(ByVal grid As DataGrid, ByRef qryString As String)
        For Each DemoGridItem As DataGridItem In grid.Items
            Dim curCheckbox As CheckBox = CType(DemoGridItem.Cells(0).Controls(1), CheckBox)
            If curCheckbox.Checked Then
                If Not qryString = String.Empty Then qryString += ","
                qryString += grid.DataKeys(DemoGridItem.ItemIndex)
            End If
        Next
    End Sub

    '02/04/2005 - NOT USED
    Public Sub ToggleSelectAll(ByVal sender As Object, ByVal e As EventArgs)
        Dim grid As DataGrid
        grid = CType(sender.parent.parent.parent.parent, DataGrid)

        Dim checked As Boolean = CType(sender.checked, Boolean)

        For Each DemoGridItem As DataGridItem In grid.Items
            Dim curCheckbox As CheckBox = CType(DemoGridItem.Cells(0).Controls(1), CheckBox)
            curCheckbox.Checked = checked
        Next
    End Sub

    'NOT USED 02/04/2005
    'Private Sub AddToWatchList(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles addToWatchBottom.Click, addToWatchTop.Click
    Private Sub AddToWatchList(ByVal sender As Object, ByVal e As ImageClickEventArgs)

        Dim qryString As String = String.Empty

        If Not gridOrthographicSearch Is Nothing Then
            GenerateSelectedItemsString(gridOrthographicSearch, qryString)
        End If
        If Not gridMergedSearch Is Nothing Then
            GenerateSelectedItemsString(gridMergedSearch, qryString)
        End If
        If Not gridPhoneticSearch Is Nothing Then
            GenerateSelectedItemsString(gridPhoneticSearch, qryString)
        End If
        If Not gridTextSearch Is Nothing Then
            GenerateSelectedItemsString(gridTextSearch, qryString)
        End If
        'If Not gridSpellingSearch Is Nothing Then
        '    GenerateSelectedItemsString(gridSpellingSearch, qryString)
        'End If

        'JAYESH CODE
        If Not gridOrthographicSearchAll Is Nothing Then
            GenerateSelectedItemsString(gridOrthographicSearchAll, qryString)
        End If
        If Not gridMergedSearchAll Is Nothing Then
            GenerateSelectedItemsString(gridMergedSearchAll, qryString)
        End If
        If Not gridPhoneticSearchAll Is Nothing Then
            GenerateSelectedItemsString(gridPhoneticSearchAll, qryString)
        End If
        If Not gridTextSearchAll Is Nothing Then
            GenerateSelectedItemsString(gridTextSearchAll, qryString)
        End If

        ClientScript.RegisterStartupScript(Me.GetType(), "addtowl", "<script language='javascript'>window.open('addtowl.aspx?items=" + qryString + "', '_blank', 'toolbars=no,statusbar=no,scrollbars=yes,resizable=yes');<" + "/script>")
        'RegisterStartupScript
    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Confirms that an HtmlForm control is rendered for the specified ASP.NET server control at run time. */
        Exit Sub
    End Sub

    Private Sub ExportToExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles exportToExcelBottom.Click, exportToExcelTop.Click

        Dim visibleTabCount As Int32 = Convert.ToInt32(tabpanelMerged.Visible) + Convert.ToInt32(tabpanelPhonetic.Visible) + Convert.ToInt32(tabpanelOrthographic.Visible) + Convert.ToInt32(tabpanelText.Visible)
        Dim activeTab As String = ""
        If visibleTabCount = 1 Then
            If tabpanelMerged.Visible Then
                activeTab = "Merged"
            ElseIf tabpanelPhonetic.Visible Then
                activeTab = "Phonetic"
            ElseIf tabpanelOrthographic.Visible Then
                activeTab = "Orthographic"
            ElseIf tabpanelText.Visible Then
                activeTab = "Text"
            ElseIf tabPanelAll.Visible Then     'This should not happen
                activeTab = "All"
            End If
        Else
            If (searchEng.PhoneticSearch And searchEng.OrthoSearch And searchEng.TextSearch) Then
                If searchTabContainer.ActiveTabIndex = 0 Then
                    activeTab = "Merged"
                ElseIf searchTabContainer.ActiveTabIndex = 1 Then
                    activeTab = "Phonetic"
                ElseIf searchTabContainer.ActiveTabIndex = 2 Then
                    activeTab = "Orthographic"
                ElseIf searchTabContainer.ActiveTabIndex = 3 Then
                    activeTab = "Text"
                ElseIf searchTabContainer.ActiveTabIndex = 4 Then
                    activeTab = "All"
                End If
            ElseIf (searchEng.PhoneticSearch And searchEng.OrthoSearch) Then
                If searchTabContainer.ActiveTabIndex = 0 Then
                    activeTab = "Merged"
                ElseIf searchTabContainer.ActiveTabIndex = 1 Then
                    activeTab = "Phonetic"
                ElseIf searchTabContainer.ActiveTabIndex = 2 Then
                    activeTab = "Orthographic"
                ElseIf searchTabContainer.ActiveTabIndex = 3 Then
                    activeTab = "All"
                End If
            ElseIf (searchEng.PhoneticSearch And searchEng.TextSearch) Then
                If searchTabContainer.ActiveTabIndex = 0 Then
                    activeTab = "Phonetic"
                ElseIf searchTabContainer.ActiveTabIndex = 1 Then
                    activeTab = "Text"
                ElseIf searchTabContainer.ActiveTabIndex = 2 Then
                    activeTab = "All"
                End If
            ElseIf (searchEng.OrthoSearch And searchEng.TextSearch) Then
                If searchTabContainer.ActiveTabIndex = 0 Then
                    activeTab = "Orthographic"
                ElseIf searchTabContainer.ActiveTabIndex = 1 Then
                    activeTab = "Text"
                ElseIf searchTabContainer.ActiveTabIndex = 2 Then
                    activeTab = "All"
                End If
            End If
        End If


        GenerateData()


        Dim cultureInfo As CultureInfo = Thread.CurrentThread.CurrentCulture
        Dim textInfo As TextInfo = cultureInfo.TextInfo

        Dim ResultsTable As DataTable
        Dim view1 As DataView = New DataView
        Dim view3 As DataView = New DataView
        Dim SearchResultsFilter As String
        Dim text1 As String

        If activeTab.Contains("Merged") Or activeTab.Contains("Phonetic") Or activeTab.Contains("Orthographic") Then
            ResultsTable = Me.searchEngineResults.Tables.Item(0)
            view1 = ResultsTable.DefaultView
            SearchResultsFilter = StringType.FromInteger(Me.searchEng.ResultThreshold)
            text1 = activeTab & "Score"
            view1.RowFilter = String.Format("{0} >= {1}", text1, SearchResultsFilter)
            view1.Sort = text1 & " DESC"

        ElseIf activeTab.Contains("All") Then
            ResultsTable = Me.searchEngineResults.Tables.Item(0)
            view1 = ResultsTable.DefaultView
            SearchResultsFilter = StringType.FromInteger(Me.searchEng.ResultThreshold)

            Dim OrthoCount As Integer = Convert.ToInt32(gridOrthographicHeaderAll.Text.Substring(gridOrthographicHeaderAll.Text.IndexOf("of") + 3).Replace(",", ""))
            Dim PhoneticCount As Integer = Convert.ToInt32(gridPhoneticHeaderAll.Text.Substring(gridPhoneticHeaderAll.Text.IndexOf("of") + 3).Replace(",", ""))
            If OrthoCount > PhoneticCount Then
                text1 = "OrthographicScore"
            Else
                text1 = "PhoneticScore"
            End If
            'Commented on 02/28/2016 to render superset of orthographic and phonatic results
            'view1.RowFilter = String.Format("{0} >= {1}", text1, SearchResultsFilter)
            view1.Sort = text1 & " DESC"

        ElseIf activeTab.Contains("Text") Then
            ResultsTable = Me.searchEngineResults.Tables.Item(1)
            view1 = ResultsTable.DefaultView
            view1.Sort = "Name" & " ASC"
        End If


        Dim headTable As Table = New Table

        'Add Head Table header information at the top
        Dim searchTypeStr As String = activeTab & " Search Result" & vbNewLine
        If activeTab.Contains("Merged") Then
            searchTypeStr = "Combined Search Result"
        End If
        Dim dateStr As String = "Search Date: " & DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") & vbNewLine
        Dim searchTermStr As String = "Search Term: " & searchEng.SearchTerm & vbNewLine

        Dim headRow1 As TableRow = New TableRow

        Dim headCell1 As TableCell = New TableCell
        Dim headCell2 As TableCell = New TableCell
        Dim headCell3 As TableCell = New TableCell
        Dim headCell4 As TableCell = New TableCell
        Dim headCell5 As TableCell = New TableCell

        headCell1.Text = searchTypeStr
        headCell2.Text = ""
        headCell3.Text = searchTermStr
        headCell4.Text = ""
        headCell5.Text = dateStr

        headRow1.Cells.Add(headCell1)
        headRow1.Cells.Add(headCell2)
        headRow1.Cells.Add(headCell3)
        headRow1.Cells.Add(headCell4)
        headRow1.Cells.Add(headCell5)

        headTable.Rows.Add(headRow1)

        Dim headStringWriter As StringWriter = New System.IO.StringWriter()
        Dim headHtmlTextWriter As HtmlTextWriter = New HtmlTextWriter(headStringWriter)
        headTable.RenderControl(headHtmlTextWriter)



        Dim table As Table = New Table
        table.GridLines = GridLines.Both

        'Add Header Row
        Dim th As TableRow = New TableRow
        Dim th1 As TableCell = New TableCell
        Dim th2 As TableCell = New TableCell
        Dim th3 As TableCell = New TableCell
        Dim th4 As TableCell = New TableCell
        Dim th5 As TableCell = New TableCell
        Dim th6 As TableCell = New TableCell
        Dim th7 As TableCell = New TableCell
        Dim th8 As TableCell = New TableCell
        Dim th9 As TableCell = New TableCell
        Dim th10 As TableCell = New TableCell
        Dim th11 As TableCell = New TableCell
        Dim th12 As TableCell = New TableCell
        Dim th13 As TableCell = New TableCell
        Dim th14 As TableCell = New TableCell

        th1.Text = "Name of Concern"
        th1.Font.Bold = True
        th1.BackColor = Color.Blue
        th1.ForeColor = Color.White
        th1.Font.Size = 14

        If activeTab.Contains("All") Or activeTab.Contains("Merged") Then
            th2.Text = "Orthographic Score"
            th2.Font.Bold = True
            th2.BackColor = Color.Blue
            th2.ForeColor = Color.White
            th2.Font.Size = 14

            th3.Text = "Phonetic Score"
            th3.Font.Bold = True
            th3.BackColor = Color.Blue
            th3.ForeColor = Color.White
            th3.Font.Size = 14

            th4.Text = "Combined Score"
            th4.Font.Bold = True
            th4.BackColor = Color.Blue
            th4.ForeColor = Color.White
            th4.Font.Size = 14

            th5.Text = "Data Sources"
            th5.Font.Bold = True
            th5.BackColor = Color.Blue
            th5.ForeColor = Color.White
            th5.Font.Size = 14
        Else
            th2.Text = activeTab & " Score"
            th2.Font.Bold = True
            th2.BackColor = Color.Blue
            th2.ForeColor = Color.White
            th2.Font.Size = 14

            th3.Text = "Data Sources"
            th3.Font.Bold = True
            th3.BackColor = Color.Blue
            th3.ForeColor = Color.White
            th3.Font.Size = 14
        End If

        th6.Text = "Active Ingredient"
        th6.Font.Bold = True
        th6.BackColor = Color.Blue
        th6.ForeColor = Color.White
        th6.Font.Size = 14

        th7.Text = "Strength"
        th7.Font.Bold = True
        th7.BackColor = Color.Blue
        th7.ForeColor = Color.White
        th7.Font.Size = 14

        th8.Text = "Dosage Form"
        th8.Font.Bold = True
        th8.BackColor = Color.Blue
        th8.ForeColor = Color.White
        th8.Font.Size = 14

        th9.Text = "Route"
        th9.Font.Bold = True
        th9.BackColor = Color.Blue
        th9.ForeColor = Color.White
        th9.Font.Size = 14

        th10.Text = "Dose And Frequency"
        th10.Font.Bold = True
        th10.BackColor = Color.Blue
        th10.ForeColor = Color.White
        th10.Font.Size = 14

        th11.Text = "Appendix"
        th11.Font.Bold = True
        th11.BackColor = Color.Blue
        th11.ForeColor = Color.White
        th11.Font.Size = 14

        th12.Text = "FMEA"
        th12.Font.Bold = True
        th12.BackColor = Color.Blue
        th12.ForeColor = Color.White
        th12.Font.Size = 14

        th13.Text = "Comments"
        th13.Font.Bold = True
        th13.BackColor = Color.Blue
        th13.ForeColor = Color.White
        th13.Font.Size = 14

        th14.Text = "Is Deleted"
        th14.Font.Bold = True
        th14.BackColor = Color.Blue
        th14.ForeColor = Color.White
        th14.Font.Size = 14



        th.Cells.Add(th1)
        If activeTab.Contains("Text") = False Then
            th.Cells.Add(th2)
        End If
        th.Cells.Add(th3)
        If activeTab.Contains("All") Or activeTab.Contains("Merged") Then
            th.Cells.Add(th4)
            th.Cells.Add(th5)
        End If
        th.Cells.Add(th6)
        th.Cells.Add(th7)
        th.Cells.Add(th8)
        th.Cells.Add(th9)
        th.Cells.Add(th10)
        th.Cells.Add(th11)
        th.Cells.Add(th12)
        th.Cells.Add(th13)
        th.Cells.Add(th14)
        table.Rows.Add(th)


        Dim bkColor As Boolean = True
        For Each row As DataRowView In view1
            Dim tr As TableRow = New TableRow
            Dim td1 As TableCell = New TableCell
            Dim td2 As TableCell = New TableCell
            Dim td3 As TableCell = New TableCell
            Dim td4 As TableCell = New TableCell
            Dim td5 As TableCell = New TableCell
            Dim td6 As TableCell = New TableCell
            Dim td7 As TableCell = New TableCell
            Dim td8 As TableCell = New TableCell
            Dim td9 As TableCell = New TableCell
            Dim td10 As TableCell = New TableCell
            Dim td11 As TableCell = New TableCell
            Dim td12 As TableCell = New TableCell
            Dim td13 As TableCell = New TableCell
            Dim td14 As TableCell = New TableCell

            Dim dataSourceCode As String = ""
            Dim dataSourceName As String = ""

            td1.Text = textInfo.ToTitleCase(row(0).ToString().ToLower)

            If activeTab.Contains("Text") = False Then
                If activeTab.Contains("All") Or activeTab.Contains("Merged") Then
                    td2.Text = row(2)
                    td3.Text = row(1)
                    td4.Text = row(3)
                    td2.HorizontalAlign = HorizontalAlign.Center
                    td3.HorizontalAlign = HorizontalAlign.Center
                    td4.HorizontalAlign = HorizontalAlign.Center

                    dataSourceCode = row(4)
                    dataSourceName = PPC.FDA.Data.OracleDataFactory.GetDataSourceName(row(4))
                    td5.Text = dataSourceName
                    'td5.Width = 300
                Else
                    If activeTab.Contains("Phonetic") Then
                        td2.Text = row(1)
                    ElseIf activeTab.Contains("Orthographic") Then
                        td2.Text = row(2)
                    End If
                    td2.HorizontalAlign = HorizontalAlign.Center

                    dataSourceCode = row(4)
                    dataSourceName = PPC.FDA.Data.OracleDataFactory.GetDataSourceName(row(4))
                    td3.Text = dataSourceName
                    'td3.Width = 300
                End If
            Else
                dataSourceCode = row(1)
                dataSourceName = PPC.FDA.Data.OracleDataFactory.GetDataSourceName(row(1))
                td3.Text = dataSourceName
                'td3.Width = 300
            End If


            'Product details goes to td6 to td9.
            'td6 - Active Ingredient
            'td7 - Strength
            'td8 - Dosage Form
            'td9 - Route
            Dim activeIngredient As String = ""
            Dim strength As String = ""
            Dim dosageForm As String = ""
            Dim route As String = ""

            'Active Ingredient
            Dim activeIngredientStr As String = ""
            If activeTab.Contains("Text") = False Then
                activeIngredientStr = row(6).ToString()
            Else
                activeIngredientStr = row(3).ToString()
            End If

            activeIngredientStr = activeIngredientStr.Replace("\r\n", "$")
            Dim activeIngredientLines As String() = activeIngredientStr.Split(New Char() {"$"c})
            Dim activeIngredientLine As String
            'System.Array.Sort(activeIngredientLines)
            If (activeIngredientStr.Contains("Drugs At FDA:") Or activeIngredientStr.Contains("drugs@FDA:")) Then
                For Each activeIngredientLine In activeIngredientLines
                    If (activeIngredientLine.Contains("Drugs At FDA:") Or activeIngredientLine.Contains("drugs@FDA:")) Then
                        activeIngredientLine = activeIngredientLine.Replace("Drugs At FDA:", "")
                        activeIngredientLine = activeIngredientLine.Replace("drugs@FDA:", "")

                        If activeIngredientLine.Length > 0 Then
                            If activeIngredient.Length = 0 Then
                                activeIngredient = activeIngredientLine.Trim
                            Else
                                Dim temp As String = activeIngredient.Replace("<br style='mso-data-placement:same-cell;' />", "#")
                                If (Array.IndexOf(temp.Split("#"), activeIngredientLine.Trim) = -1) Then
                                    activeIngredient = activeIngredient + "<br style='mso-data-placement:same-cell;' />" + activeIngredientLine.Trim
                                End If
                            End If
                        End If
                    End If
                Next

            ElseIf activeIngredientStr.Contains("RxNorm:") Then
                For Each activeIngredientLine In activeIngredientLines
                    If activeIngredientLine.Contains("RxNorm:") Then
                        activeIngredientLine = activeIngredientLine.Replace("RxNorm:", "")

                        If activeIngredientLine.Length > 0 Then
                            If activeIngredient.Length = 0 Then
                                activeIngredient = activeIngredientLine.Trim
                            Else
                                activeIngredient = activeIngredient + "<br style='mso-data-placement:same-cell;' />" + activeIngredientLine.Trim
                            End If
                        End If
                    End If
                Next
            End If


            'Strength
            Dim strengthStr As String = ""
            If activeTab.Contains("Text") = False Then
                strengthStr = row(8).ToString()
            Else
                strengthStr = row(5).ToString()
            End If

            strengthStr = strengthStr.Replace("\r\n", "$")
            Dim strengthLines As String() = strengthStr.Split(New Char() {"$"c})
            Dim strengthLine As String
            System.Array.Sort(strengthLines)
            If (strengthStr.Contains("Drugs At FDA:") Or strengthStr.Contains("drugs@FDA:")) Then
                For Each strengthLine In strengthLines
                    If (strengthLine.Contains("Drugs At FDA:") Or strengthLine.Contains("drugs@FDA:")) Then
                        strengthLine = strengthLine.Replace("Drugs At FDA:", "")
                        strengthLine = strengthLine.Replace("drugs@FDA:", "")

                        If strengthLine.Length > 0 Then
                            If strength.Length = 0 Then
                                strength = strengthLine.Trim
                            Else
                                Dim temp As String = strength.Replace("<br style='mso-data-placement:same-cell;' />", "#")
                                If (Array.IndexOf(temp.Split("#"), strengthLine.Trim) = -1) Then
                                    strength = strength + "<br style='mso-data-placement:same-cell;' />" + strengthLine.Trim
                                End If
                            End If
                        End If
                    End If
                Next
            End If


            'Dosage Form and Route
            Dim dosageFormStr As String = ""
            If activeTab.Contains("Text") = False Then
                dosageFormStr = row(7).ToString()
            Else
                dosageFormStr = row(4).ToString()
            End If

            dosageFormStr = dosageFormStr.Replace("\r\n", "$")
            Dim dosageFormLines As String() = dosageFormStr.Split(New Char() {"$"c})
            Dim dosageFormLine As String
            System.Array.Sort(dosageFormLines)
            If (dosageFormStr.Contains("Drugs At FDA:") Or dosageFormStr.Contains("drugs@FDA:")) Then
                For Each dosageFormLine In dosageFormLines
                    If (dosageFormLine.Contains("Drugs At FDA:") Or dosageFormLine.Contains("drugs@FDA:")) Then
                        dosageFormLine = dosageFormLine.Replace("Drugs At FDA:", "")
                        dosageFormLine = dosageFormLine.Replace("drugs@FDA:", "")

                        dosageFormLine = dosageFormLine.Trim
                        If dosageForm.Length = 0 Then
                            If dosageFormLine.Contains(";") Then
                                Dim pos As Integer = dosageFormLine.IndexOf(";")
                                dosageForm = dosageFormLine.Substring(0, pos).Trim
                                route = dosageFormLine.Substring(pos + 1).Trim
                            Else
                                dosageForm = dosageFormLine.Trim
                            End If
                        Else
                            If dosageFormLine.Contains(";") Then
                                Dim pos As Integer = dosageFormLine.IndexOf(";")
                                Dim tempDosage As String = dosageFormLine.Substring(0, pos).Trim
                                Dim tempRoute As String = dosageFormLine.Substring(pos + 1).Trim

                                Dim temp1 As String = dosageForm.Replace("<br style='mso-data-placement:same-cell;' />", "#")
                                If (Array.IndexOf(temp1.Split("#"), tempDosage) = -1) Then
                                    dosageForm = dosageForm + "<br style='mso-data-placement:same-cell;' />" + tempDosage
                                End If

                                Dim temp2 As String = route.Replace("<br style='mso-data-placement:same-cell;' />", "#")
                                If (Array.IndexOf(temp2.Split("#"), tempRoute) = -1) Then
                                    route = route + "<br style='mso-data-placement:same-cell;' />" + tempRoute
                                End If
                            Else
                                Dim temp As String = dosageForm.Replace("<br style='mso-data-placement:same-cell;' />", "#")
                                If (Array.IndexOf(temp.Split("#"), dosageFormLine) = -1) Then
                                    dosageForm = dosageForm + "<br style='mso-data-placement:same-cell;' />" + dosageFormLine
                                End If
                            End If
                        End If
                    End If
                Next
            End If


            'Route
            'If activeTab.Contains("Text") = False Then
            '    route = row(9).ToString()
            'Else
            '    route = row(6).ToString()
            'End If


            td6.Text = activeIngredient
            td7.Text = strength
            td8.Text = dosageForm
            td9.Text = route


            'Empty coulmns as per requirements
            'td10 - Dose And Frequency
            'td11 - Appendix
            'td12 - FMEA
            'td13 - Comments
            td10.Text = ""
            td11.Text = ""
            td12.Text = ""
            td13.Text = ""

            Dim isDeletedStr As String
            If activeTab.Contains("Text") = False Then
                isDeletedStr = row(10).ToString()
            Else
                isDeletedStr = row(7).ToString()
            End If
            If (isDeletedStr = "Y") Then
                td14.Text = "Yes"
            Else
                td14.Text = ""
            End If


            If bkColor Then
                td1.BackColor = Color.FromArgb(224, 224, 224)
                td2.BackColor = Color.FromArgb(224, 224, 224)
                td3.BackColor = Color.FromArgb(224, 224, 224)
                td4.BackColor = Color.FromArgb(224, 224, 224)
                td5.BackColor = Color.FromArgb(224, 224, 224)
                td6.BackColor = Color.FromArgb(224, 224, 224)
                td7.BackColor = Color.FromArgb(224, 224, 224)
                td8.BackColor = Color.FromArgb(224, 224, 224)
                td9.BackColor = Color.FromArgb(224, 224, 224)
                td10.BackColor = Color.FromArgb(224, 224, 224)
                td11.BackColor = Color.FromArgb(224, 224, 224)
                td12.BackColor = Color.FromArgb(224, 224, 224)
                td13.BackColor = Color.FromArgb(224, 224, 224)
                td14.BackColor = Color.FromArgb(224, 224, 224)
                bkColor = False
            Else
                td1.BackColor = Color.White
                td2.BackColor = Color.White
                td3.BackColor = Color.White
                td4.BackColor = Color.White
                td5.BackColor = Color.White
                td6.BackColor = Color.White
                td7.BackColor = Color.White
                td8.BackColor = Color.White
                td9.BackColor = Color.White
                td10.BackColor = Color.White
                td11.BackColor = Color.White
                td12.BackColor = Color.White
                td13.BackColor = Color.White
                td14.BackColor = Color.White
                bkColor = True
            End If

            tr.VerticalAlign = VerticalAlign.Top
            tr.Cells.Add(td1)
            If activeTab.Contains("Text") = False Then
                tr.Cells.Add(td2)
            End If
            tr.Cells.Add(td3)
            If activeTab.Contains("All") Or activeTab.Contains("Merged") Then
                tr.Cells.Add(td4)
                tr.Cells.Add(td5)
            End If
            tr.Cells.Add(td6)
            tr.Cells.Add(td7)
            tr.Cells.Add(td8)
            tr.Cells.Add(td9)
            tr.Cells.Add(td10)
            tr.Cells.Add(td11)
            tr.Cells.Add(td12)
            tr.Cells.Add(td13)
            tr.Cells.Add(td14)
            table.Rows.Add(tr)
        Next


        Dim stringWriter As StringWriter = New System.IO.StringWriter()
        Dim htmlTextWriter As HtmlTextWriter = New HtmlTextWriter(stringWriter)
        table.RenderControl(htmlTextWriter)


        'Now write to response to open/save in Excel
        Dim fileName As String = searchEng.SearchTerm
        If fileName.Contains("%") Or fileName.Contains("_") Then
            fileName = fileName.Replace("%", "").Replace("_", "")
        End If
        If activeTab.Contains("Merged") Then
            fileName = fileName & "_" & "CombinedSearchResults.xls"
        Else
            fileName = fileName & "_" & activeTab & "SearchResults.xls"
        End If

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=" & fileName)
        Response.ContentType = "application/vnd.ms-excel"
        'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        Response.Write(headStringWriter.ToString() & stringWriter.ToString())
        'Response.Write(stringWriter.ToString())
        Response.End()

    End Sub


    Private Sub DoSearch()
        Try
            GenerateData()
            'If (searchEng.PhoneticSearch And searchEng.OrthoSearch) Then SetupDataGrid(gridMergedSearch, gridMergedHeader, "Orthographic & Phonetic", "MergedScore")
            'If searchEng.PhoneticSearch Then SetupDataGrid(gridPhoneticSearch, gridPhoneticHeader, "Phonetic", "PhoneticScore")
            'If searchEng.OrthoSearch Then SetupDataGrid(gridOrthographicSearch, gridOrthographicHeader, "Orthographic", "OrthographicScore")
            'If searchEng.TextSearch Then SetupDataGrid(gridTextSearch, gridTextHeader, "Text", "Name")

            tabpanelMerged.Visible = False
            tabpanelOrthographic.Visible = False
            tabpanelPhonetic.Visible = False
            tabpanelText.Visible = False
            'tabPanelSpelling.Visible = False
            tabPanelAll.Visible = False

            If (searchEng.PhoneticSearch And searchEng.OrthoSearch And searchEng.TextSearch) Then
                SetupDataGrid(gridMergedSearch, gridMergedHeader, "Orthographic & Phonetic", "MergedScore")
                SetupDataGrid(gridPhoneticSearch, gridPhoneticHeader, "Phonetic", "PhoneticScore")
                SetupDataGrid(gridOrthographicSearch, gridOrthographicHeader, "Orthographic", "OrthographicScore")
                SetupDataGrid(gridTextSearch, gridTextHeader, "Text", "Name")
                tabpanelMerged.Visible = True
                tabpanelOrthographic.Visible = True
                tabpanelPhonetic.Visible = True
                tabpanelText.Visible = True

                SetupDataGrid(gridMergedSearchAll, gridMergedHeaderAll, "Orthographic & Phonetic", "MergedScore")
                SetupDataGrid(gridPhoneticSearchAll, gridPhoneticHeaderAll, "Phonetic", "PhoneticScore")
                SetupDataGrid(gridOrthographicSearchAll, gridOrthographicHeaderAll, "Orthographic", "OrthographicScore")
                SetupDataGrid(gridTextSearchAll, gridTextHeaderAll, "Text", "Name")
                tabPanelAll.Visible = True
                gridMergedPanelAll.Visible = True
                gridPhoneticPanelAll.Visible = True
                gridOrthographicPanelAll.Visible = True
                gridTextPanelAll.Visible = True
                'gridTextPanelAll.Visible = gridTextSearchAll.Visible

            ElseIf (searchEng.PhoneticSearch And searchEng.OrthoSearch) Then
                SetupDataGrid(gridMergedSearch, gridMergedHeader, "Orthographic & Phonetic", "MergedScore")
                SetupDataGrid(gridPhoneticSearch, gridPhoneticHeader, "Phonetic", "PhoneticScore")
                SetupDataGrid(gridOrthographicSearch, gridOrthographicHeader, "Orthographic", "OrthographicScore")
                tabpanelMerged.Visible = True
                tabpanelOrthographic.Visible = True
                tabpanelPhonetic.Visible = True

                SetupDataGrid(gridMergedSearchAll, gridMergedHeaderAll, "Orthographic & Phonetic", "MergedScore")
                SetupDataGrid(gridPhoneticSearchAll, gridPhoneticHeaderAll, "Phonetic", "PhoneticScore")
                SetupDataGrid(gridOrthographicSearchAll, gridOrthographicHeaderAll, "Orthographic", "OrthographicScore")
                tabPanelAll.Visible = True
                gridMergedPanelAll.Visible = True
                gridPhoneticPanelAll.Visible = True
                gridOrthographicPanelAll.Visible = True
                gridTextPanelAll.Visible = False

            ElseIf (searchEng.PhoneticSearch And searchEng.TextSearch) Then
                SetupDataGrid(gridPhoneticSearch, gridPhoneticHeader, "Phonetic", "PhoneticScore")
                SetupDataGrid(gridTextSearch, gridTextHeader, "Text", "Name")
                tabpanelPhonetic.Visible = True
                tabpanelText.Visible = True

                SetupDataGrid(gridPhoneticSearchAll, gridPhoneticHeaderAll, "Phonetic", "PhoneticScore")
                SetupDataGrid(gridTextSearchAll, gridTextHeaderAll, "Text", "Name")
                tabPanelAll.Visible = True
                gridMergedPanelAll.Visible = False
                FindControlRecursive(Me, "gridMergedAll").Attributes.Remove("class")
                gridPhoneticPanelAll.Visible = True
                gridOrthographicPanelAll.Visible = False
                FindControlRecursive(Me, "gridOrthographicAll").Attributes.Remove("class")
                gridTextPanelAll.Visible = True
                'gridTextPanelAll.Visible = gridTextSearchAll.Visible

            ElseIf (searchEng.OrthoSearch And searchEng.TextSearch) Then
                SetupDataGrid(gridOrthographicSearch, gridOrthographicHeader, "Orthographic", "OrthographicScore")
                SetupDataGrid(gridTextSearch, gridTextHeader, "Text", "Name")
                tabpanelOrthographic.Visible = True
                tabpanelText.Visible = True

                SetupDataGrid(gridOrthographicSearchAll, gridOrthographicHeaderAll, "Orthographic", "OrthographicScore")
                SetupDataGrid(gridTextSearchAll, gridTextHeaderAll, "Text", "Name")
                tabPanelAll.Visible = True
                gridMergedPanelAll.Visible = False
                FindControlRecursive(Me, "gridMergedAll").Attributes.Remove("class")
                gridPhoneticPanelAll.Visible = False
                FindControlRecursive(Me, "gridPhoneticAll").Attributes.Remove("class")
                gridOrthographicPanelAll.Visible = True
                gridTextPanelAll.Visible = True
                'gridTextPanelAll.Visible = gridTextSearchAll.Visible

            ElseIf searchEng.PhoneticSearch Then
                SetupDataGrid(gridPhoneticSearch, gridPhoneticHeader, "Phonetic", "PhoneticScore")
                tabpanelPhonetic.Visible = True

            ElseIf searchEng.OrthoSearch Then
                SetupDataGrid(gridOrthographicSearch, gridOrthographicHeader, "Orthographic", "OrthographicScore")
                tabpanelOrthographic.Visible = True

            ElseIf searchEng.TextSearch Then
                SetupDataGrid(gridTextSearch, gridTextHeader, "Text", "Name")
                tabpanelText.Visible = True

            'ElseIf searchEng.SpellingSearch Then
            '    SetupDataGrid(gridSpellingSearch, gridSpellingHeader, "Spelling", "SpellingScore")
            '    tabPanelSpelling.Visible = True
            End If

        Catch DoSearchException As Exception

            Dim ErrorMessageLabel As New Label
            ErrorMessageLabel.Text = ("An error occured during the processing of the records: " & DoSearchException.Message & Environment.NewLine & DoSearchException.StackTrace)
            Me.Page.Controls.Add(ErrorMessageLabel)

        End Try
    End Sub

    Private Sub GenerateData()
        Dim results As String
        results = searchEng.SearchResults
        If results Is Nothing Then
            'results = String.Empty
            results = "<ScoredResults />"
        End If
        Dim reader1 As New StringReader(results)
        'If (reader1.Peek > -1) Then
        Me.searchEngineResults.ReadXml(reader1)
        'Else
        'Me.searchEngineResults.ReadXml(TextReader.Null)
        'End If
    End Sub

    Private Sub GenerateDataGrid(ByVal ResultsTable As DataTable, ByVal GridToEdit As DataGrid, ByVal SearchResultsPageSize As Integer, ByVal SearchResultsFilter As String, ByVal SearchResultsSort As String)
        Dim view1 As DataView = ResultsTable.DefaultView
        If (GridToEdit.ID.ToUpper.IndexOf("TEXT") <= -1) Then
            Dim text1 As String = GridToEdit.Columns(1).SortExpression
            view1.RowFilter = String.Format("{0} >= {1}", text1, SearchResultsFilter)
        End If
        If (view1.Count = 0) Then
            Throw New DataException("There were no matches found.")
        End If
        If (StringType.StrCmp(SearchResultsSort, "", False) <> 0) Then
            Dim obj2 As Object = GridToEdit.Attributes.Item("SortExpression")
            Dim obj1 As Object = GridToEdit.Attributes.Item("SortASC")
            GridToEdit.Attributes.Item("SortExpression") = SearchResultsSort
            GridToEdit.Attributes.Item("SortASC") = "No"
            If GridToEdit.ID.Contains("Text") Then
                GridToEdit.Attributes.Item("SortASC") = "Yes"
            End If
            If (ObjectType.ObjTst(SearchResultsSort, obj2, False) = 0) Then
                If (ObjectType.ObjTst(obj1, "Yes", False) = 0) Then
                    GridToEdit.Attributes.Item("SortASC") = "No"
                Else
                    GridToEdit.Attributes.Item("SortASC") = "Yes"
                End If
            End If
        End If
        view1.Sort = GridToEdit.Attributes.Item("SortExpression")

        'JAYESH COMMENTED TO ADD Below code
        'If (StringType.StrCmp(GridToEdit.Attributes.Item("SortASC"), "No", False) = 0) Then
        '    Dim view2 As DataView = view1
        '    view2.Sort = (view2.Sort & " DESC")
        'End If

        'GridToEdit.DataSource = view1
        'GridToEdit.PageSize = SearchResultsPageSize
        'If (view1.Count <= SearchResultsPageSize) Then
        '    GridToEdit.AllowPaging = False
        'End If



        If (StringType.StrCmp(GridToEdit.Attributes.Item("SortASC"), "No", False) = 0) Then
            view1.Sort = (view1.Sort & " DESC")
        End If

        GridToEdit.DataSource = view1
        'If GridToEdit.ID.Contains("All") Then
        'GridToEdit.AllowPaging = False
        'Else
        GridToEdit.PageSize = SearchResultsPageSize
        If (view1.Count <= SearchResultsPageSize) Then
            GridToEdit.AllowPaging = False
        Else
            GridToEdit.AllowPaging = True
        End If
        'End If

        'Jayesh Code - COMMENT 02/04/2015
        'Dim view3 As DataView = ModifyDataSourceInDataView(view1, GridToEdit.ID)
        'view3.Sort = GridToEdit.Attributes.Item("SortExpression")
        'If (StringType.StrCmp(GridToEdit.Attributes.Item("SortASC"), "No", False) = 0) Then
        '    view3.Sort = (view3.Sort & " DESC")
        'End If

        'GridToEdit.DataSource = view3
        'If GridToEdit.ID.Contains("All") Then
        '    GridToEdit.AllowPaging = False
        'Else
        '    GridToEdit.PageSize = SearchResultsPageSize
        '    If (view3.Count <= SearchResultsPageSize) Then
        '        GridToEdit.AllowPaging = False
        '    Else
        '        GridToEdit.AllowPaging = True
        '    End If
        'End If
    End Sub

    Private Function GridHeader(ByVal GridName As String, ByVal MatchPercent As String) As String
        If (StringType.StrCmp(GridName, "Text", False) = 0) Then
            Return String.Format("<span class=""gridTitle"">{0} Matches</span>", GridName)
        End If
        Return String.Format("<span class=""gridTitle"">{0} Matches</span> greater than {1}%", GridName, MatchPercent)
    End Function

    'Private Sub GridSortCommand(ByVal source As Object, ByVal e As DataGridSortCommandEventArgs) Handles gridMergedSearch.SortCommand, gridOrthographicSearch.SortCommand, gridPhoneticSearch.SortCommand, gridTextSearch.SortCommand
    'Private Sub GridSortCommand(ByVal source As Object, ByVal e As DataGridSortCommandEventArgs) Handles gridMergedSearch.SortCommand, gridOrthographicSearch.SortCommand, gridPhoneticSearch.SortCommand, gridTextSearch.SortCommand, gridSpellingSearch.SortCommand, gridMergedSearchAll.SortCommand, gridOrthographicSearchAll.SortCommand, gridPhoneticSearchAll.SortCommand, gridTextSearchAll.SortCommand
    Private Sub GridSortCommand(ByVal source As Object, ByVal e As DataGridSortCommandEventArgs) Handles gridMergedSearch.SortCommand, gridOrthographicSearch.SortCommand, gridPhoneticSearch.SortCommand, gridTextSearch.SortCommand, gridMergedSearchAll.SortCommand, gridOrthographicSearchAll.SortCommand, gridPhoneticSearchAll.SortCommand, gridTextSearchAll.SortCommand
        Dim ClickedSortHeader As DataGridItem = CType(e.CommandSource, DataGridItem)
        Dim SortedGrid As FDAGrid = CType(ClickedSortHeader.Parent.Parent, FDAGrid)
        GenerateData()
        SetupDataGrid(SortedGrid, e.SortExpression)
        SortedGrid.DataBind()

        'JAYESH CODE - set active tab index now
        searchTabContainer.ActiveTabIndex = GetActiveTabIndex(SortedGrid.ID)
        searchUpdatePanel.Update()
    End Sub

    'Private Sub GridItemDataBoundCommand(ByVal source As Object, ByVal e As DataGridItemEventArgs) Handles gridMergedSearch.ItemDataBound, gridOrthographicSearch.ItemDataBound, gridPhoneticSearch.ItemDataBound, gridSpellingSearch.ItemDataBound, gridMergedSearchAll.ItemDataBound, gridOrthographicSearchAll.ItemDataBound, gridPhoneticSearchAll.ItemDataBound
    Private Sub GridItemDataBoundCommand(ByVal source As Object, ByVal e As DataGridItemEventArgs) Handles gridMergedSearch.ItemDataBound, gridOrthographicSearch.ItemDataBound, gridPhoneticSearch.ItemDataBound, gridMergedSearchAll.ItemDataBound, gridOrthographicSearchAll.ItemDataBound, gridPhoneticSearchAll.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim score As Integer = Convert.ToInt32(e.Item.Cells(1).Text)

            If score >= 70 Then
                e.Item.BackColor = Color.LightBlue
            ElseIf score >= 55 Then
                e.Item.BackColor = Color.LightGray
            Else
                e.Item.BackColor = Color.FromArgb(255, 255, 153)
            End If

            'Dim grid1 As FDAGrid = CType(source, FDAGrid)
            'If grid1.ID.Contains("Spelling") Then
            '    Dim drugNameLbl As HyperLink = CType(e.Item.FindControl("DrugNameHL"), HyperLink)
            '    If drugNameLbl.Text.ToUpper.Substring(0, 2) = searchEng.SearchTerm.ToUpper.Substring(0, 2) Then
            '        e.Item.Cells(0).BackColor = Color.PaleVioletRed
            '    End If
            'End If
        End If
    End Sub

    Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Init
        Me.InitializeComponent()
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Dim PO As PPC.FDA.Person.PersonObject = CType(Session("LoggedInUser"), PPC.FDA.Person.PersonObject)
        If PO Is Nothing Then
            Server.Transfer("default.aspx")
            'Response.Redirect("default.aspx")
        End If

        If Session("FinishedSearch") Then
            searchEng = CType(Session("Engine"), SearchEngine)
            searchRunning.Visible = False
            searchResults.Visible = True

            lblSearchTerm.Text = searchEng.SearchTerm
            If Not Page.IsPostBack Then
                DoSearch()
            End If
        Else
            Response.Write("<META HTTP-EQUIV=Refresh CONTENT=""3; URL=""> ")
            'Response.Write("<META HTTP-EQUIV=Refresh CONTENT=""10; URL=""> ")
        End If
    End Sub

    'Private Sub PageIndexChanged(ByVal source As Object, ByVal e As DataGridPageChangedEventArgs) Handles gridMergedSearch.PageIndexChanged, gridPhoneticSearch.PageIndexChanged, gridOrthographicSearch.PageIndexChanged, gridTextSearch.PageIndexChanged
    'Private Sub PageIndexChanged(ByVal source As Object, ByVal e As DataGridPageChangedEventArgs) Handles gridMergedSearch.PageIndexChanged, gridPhoneticSearch.PageIndexChanged, gridOrthographicSearch.PageIndexChanged, gridTextSearch.PageIndexChanged, gridSpellingSearch.PageIndexChanged, gridMergedSearchAll.PageIndexChanged, gridPhoneticSearchAll.PageIndexChanged, gridOrthographicSearchAll.PageIndexChanged, gridTextSearchAll.PageIndexChanged
    Private Sub PageIndexChanged(ByVal source As Object, ByVal e As DataGridPageChangedEventArgs) Handles gridMergedSearch.PageIndexChanged, gridPhoneticSearch.PageIndexChanged, gridOrthographicSearch.PageIndexChanged, gridTextSearch.PageIndexChanged, gridMergedSearchAll.PageIndexChanged, gridPhoneticSearchAll.PageIndexChanged, gridOrthographicSearchAll.PageIndexChanged, gridTextSearchAll.PageIndexChanged
        Dim item1 As DataGridItem = CType(e.CommandSource, DataGridItem)

        Dim grid1 As FDAGrid = CType(item1.Parent.Parent, FDAGrid)
        grid1.CurrentPageIndex = e.NewPageIndex
        Me.GenerateData()
        Dim grid2 As DataGrid = grid1

        If grid2.ID = "gridMergedSearch" Then SetupDataGrid(grid2, gridMergedHeader, "Orthographic & Phonetic", Nothing)
        If grid2.ID = "gridPhoneticSearch" Then SetupDataGrid(grid2, gridPhoneticHeader, "Phonetic", Nothing)
        If grid2.ID = "gridOrthographicSearch" Then SetupDataGrid(grid2, gridOrthographicHeader, "Orthographic", Nothing)
        If grid2.ID = "gridTextSearch" Then SetupDataGrid(grid2, gridTextHeader, "Text", Nothing)
        'If grid2.ID = "gridSpellingSearch" Then SetupDataGrid(grid2, gridSpellingHeader, "Spelling", Nothing)
        If grid2.ID = "gridMergedSearchAll" Then SetupDataGrid(grid2, gridMergedHeaderAll, "Orthographic & Phonetic", Nothing)
        If grid2.ID = "gridPhoneticSearchAll" Then SetupDataGrid(grid2, gridPhoneticHeaderAll, "Phonetic", Nothing)
        If grid2.ID = "gridOrthographicSearchAll" Then SetupDataGrid(grid2, gridOrthographicHeaderAll, "Orthographic", Nothing)
        If grid2.ID = "gridTextSearchAll" Then SetupDataGrid(grid2, gridTextHeaderAll, "Text", Nothing)

        grid1 = CType(grid2, FDAGrid)
        grid1.DataBind()

        'JAYESH CODE - set active tab now
        searchTabContainer.ActiveTabIndex = GetActiveTabIndex(grid2.ID)
        searchUpdatePanel.Update()
    End Sub

    Private Sub SetupDataGrid(ByRef GridToSetup As DataGrid, ByVal GridSort As String)
        Me.SetupDataGrid(GridToSetup, Nothing, Nothing, GridSort)
    End Sub

    Private Function GetCurrentPageInformation(ByVal GridToSetup As DataGrid, ByVal recordCount As Integer) As String

        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder
        sb.Append(":&nbsp;&nbsp;&nbsp;Results ")
        sb.Append((GridToSetup.CurrentPageIndex() * Me.searchEng.ItemsPerPage + 1).ToString())
        sb.Append(" - ")

        If recordCount <= ((GridToSetup.CurrentPageIndex() + 1) * Me.searchEng.ItemsPerPage) Then
            sb.Append(recordCount.ToString())
        Else
            sb.Append(((GridToSetup.CurrentPageIndex() + 1) * Me.searchEng.ItemsPerPage).ToString())
        End If

        sb.Append(" of ")
        sb.Append(recordCount.ToString("#,###"))

        Return sb.ToString()
    End Function

    Private Function GetCurrentPageInformationForAllView(ByVal GridToSetup As DataGrid, ByVal recordCount As Integer) As String

        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder
        sb.Append(":&nbsp;&nbsp;&nbsp;Results " & recordCount)
        Return sb.ToString()
    End Function

    Private Sub SetupDataGrid(ByRef GridToSetup As DataGrid, ByVal GridLabel As Label, ByVal GridTitle As String, ByVal GridSort As String)

        Try
            'GetCurrentPageInformation(GridToSetup, Me.searchEngineResults.Tables.Item(1).Rows.Count)
            Dim pageInformation As String

            If (GridToSetup.ID.IndexOf("Text") > -1) Then
                Me.GenerateDataGrid(Me.searchEngineResults.Tables.Item(1), GridToSetup, Me.searchEng.ItemsPerPage, "", GridSort)
            Else
                Me.GenerateDataGrid(Me.searchEngineResults.Tables.Item(0), GridToSetup, Me.searchEng.ItemsPerPage, StringType.FromInteger(Me.searchEng.ResultThreshold), GridSort)
            End If

            If (Not GridLabel Is Nothing) Then
                Dim dvResults As DataView = CType(GridToSetup.DataSource, DataView)
                If (GridToSetup.ID.IndexOf("All") > -1) Then
                    'pageInformation = GetCurrentPageInformationForAllView(GridToSetup, dvResults.Count)
                    pageInformation = GetCurrentPageInformation(GridToSetup, dvResults.Count)
                    SetPanelHeight(GridToSetup, dvResults.Count)
                Else
                    pageInformation = GetCurrentPageInformation(GridToSetup, dvResults.Count)
                End If
                GridLabel.Text = Me.GridHeader(GridTitle, Me.searchEng.ResultThreshold.ToString) & pageInformation
                GridLabel.Visible = True

                'Set legends
                If (GridToSetup.ID.ToUpper.IndexOf("TEXT") <= -1) Then
                    Dim countTotal As Integer = dvResults.Count

                    Dim dvResults1 As DataView = CType(GridToSetup.DataSource, DataView)
                    Dim text1 As String = GridToSetup.Columns(1).SortExpression

                    Dim lable1Condition As Integer = 70
                    If Me.searchEng.ResultThreshold > lable1Condition Then
                        lable1Condition = Me.searchEng.ResultThreshold
                    End If
                    'dvResults1.RowFilter = String.Format("{0} >= {1}", text1, "70")
                    dvResults1.RowFilter = String.Format("{0} >= {1}", text1, StringType.FromInteger(lable1Condition))
                    Dim count1 As Integer = dvResults1.Count
                    Dim label1 As Label = CType(FindControlRecursive(Me, GridToSetup.ID.Replace("Search", "Legend1")), Label)
                    label1.Text = "70% and Higher: " & count1
                    label1.Visible = True

                    Dim lable2Condition As Integer = 55
                    If Me.searchEng.ResultThreshold > lable2Condition Then
                        lable2Condition = Me.searchEng.ResultThreshold
                    End If
                    'dvResults1.RowFilter = String.Format("{0} >= {1}", text1, "55")
                    dvResults1.RowFilter = String.Format("{0} >= {1}", text1, StringType.FromInteger(lable2Condition))
                    Dim count2 As Integer = dvResults1.Count
                    Dim label2 As Label = CType(FindControlRecursive(Me, GridToSetup.ID.Replace("Search", "Legend2")), Label)
                    label2.Text = "Between 55% and 69%: " & (count2 - count1)
                    label2.Visible = True

                    dvResults1.RowFilter = String.Format("{0} >= {1}", text1, StringType.FromInteger(Me.searchEng.ResultThreshold))
                    Dim label3 As Label = CType(FindControlRecursive(Me, GridToSetup.ID.Replace("Search", "Legend3")), Label)
                    label3.Text = "54% and Lower: " & (countTotal - count2)
                    label3.Visible = True
                End If
            End If

            GridToSetup.DataKeyField = "ProductNameUid"
            GridToSetup.DataBind()
            GridToSetup.Visible = True
        Catch exception2 As DataException

            If (Not GridLabel Is Nothing) Then
                GridLabel.Text = GridTitle
                GridLabel.Font.Bold = True
                GridLabel.Font.Italic = True
                GridLabel.Visible = True
            End If

            ProjectData.SetProjectError(exception2)
            Dim exception1 As DataException = exception2
            'Dim label1 As Label = CType(Me.FindControl(GridToSetup.ID.Replace("Search", "Message")), Label)
            Dim label1 As Label = CType(FindControlRecursive(Me, GridToSetup.ID.Replace("Search", "Message")), Label)
            label1.Text = exception1.Message
            label1.Visible = True

            SetPanelHeight(GridToSetup, 0)
            ProjectData.ClearProjectError()
        End Try
    End Sub

    'Jayesh Code
    Private Function ModifyDataSourceInDataView(ByVal orgDV As DataView, ByVal gridID As String) As DataView

        Dim orgDT As DataTable
        orgDT = orgDV.ToTable()

        Dim orgDT2 As DataTable
        orgDT2 = orgDV.ToTable()

        Dim modDT As DataTable = orgDT.Clone()
        modDT.Clear()
        Dim modDV As DataView = New DataView(modDT)

        Dim selectCondition As String = ""
        Try
            Dim distinctRow As DataRow
            For Each distinctRow In orgDT.Rows

                selectCondition = "Name = '" & distinctRow(0).ToString().Replace("'", "") & "'"
                Dim rowInModDT() As DataRow = modDT.Select(selectCondition)
                If rowInModDT.Length = 0 Then
                    Dim rows() As DataRow = orgDT2.Select(selectCondition)
                    Dim value As String = ""
                    Dim dr As DataRow
                    For Each dr In rows
                        If gridID.Contains("Text") Then
                            value += dr(1).ToString() & ";"
                        Else
                            value += dr(4).ToString() & ";"
                        End If
                    Next
                    Dim charsToTrim() As Char = {";"c}
                    value = value.Trim(charsToTrim)

                    'Sort the datasources
                    Dim outValue As String = ""
                    If value.Contains(";") Then
                        Dim A As Array = value.Split(";")
                        Array.Sort(A)
                        Dim x As Int32
                        For x = 0 To A.Length - 1
                            outValue += A(x).ToString() & ";"
                        Next
                        outValue = outValue.Trim(charsToTrim)
                    Else
                        outValue = value
                    End If

                    If gridID.Contains("Text") Then
                        modDT.Rows.Add(distinctRow(0).ToString(), outValue, distinctRow(2).ToString())
                    Else
                        modDT.Rows.Add(distinctRow(0).ToString(), distinctRow(1).ToString(), distinctRow(2).ToString(), distinctRow(3).ToString(), outValue, distinctRow(5).ToString())
                    End If

                    value = ""
                    outValue = ""
                End If
            Next

        Catch Excep As Exception
            Dim ErrorMessageLabel As New Label
            ErrorMessageLabel.Text = ("SelectCondition is: " & selectCondition & "    An error occured during the processing of the records: " & Excep.Message & Environment.NewLine & Excep.StackTrace)
            Me.Page.Controls.Add(ErrorMessageLabel)
        End Try

        Return modDV
    End Function

    Private Function GetActiveTabIndex(ByVal gridID As String) As Integer
        Dim activeTabIndex As Integer = 0
        If (searchEng.PhoneticSearch And searchEng.OrthoSearch And searchEng.TextSearch) Then
            If gridID.Equals("gridMergedSearch") Then
                activeTabIndex = 0
            ElseIf gridID.Equals("gridPhoneticSearch") Then
                activeTabIndex = 1
            ElseIf gridID.Equals("gridOrthographicSearch") Then
                activeTabIndex = 2
            ElseIf gridID.Equals("gridTextSearch") Then
                activeTabIndex = 3
            Else
                activeTabIndex = 4
            End If
        ElseIf (searchEng.PhoneticSearch And searchEng.OrthoSearch) Then
            If gridID.Equals("gridMergedSearch") Then
                activeTabIndex = 0
            ElseIf gridID.Equals("gridPhoneticSearch") Then
                activeTabIndex = 1
            ElseIf gridID.Equals("gridOrthographicSearch") Then
                activeTabIndex = 2
            Else
                activeTabIndex = 3
            End If
        ElseIf (searchEng.PhoneticSearch And searchEng.TextSearch) Then
            If gridID.Equals("gridPhoneticSearch") Then
                activeTabIndex = 0
            ElseIf gridID.Equals("gridTextSearch") Then
                activeTabIndex = 1
            Else
                activeTabIndex = 2
            End If
        ElseIf (searchEng.OrthoSearch And searchEng.TextSearch) Then
            If gridID.Equals("gridOrthographicSearch") Then
                activeTabIndex = 0
            ElseIf gridID.Equals("gridTextSearch") Then
                activeTabIndex = 1
            Else
                activeTabIndex = 2
            End If
        End If

        Return activeTabIndex
    End Function

    Private Function SetPanelHeight(ByRef grid As DataGrid, ByVal count As Integer) As Integer

        If (count > 20) Then
            If grid.ID = "gridMergedSearchAll" Then
                gridMergedPanelAll.Height = 500
            ElseIf grid.ID = "gridPhoneticSearchAll" Then
                gridPhoneticPanelAll.Height = 500
            ElseIf grid.ID = "gridOrthographicSearchAll" Then
                gridOrthographicPanelAll.Height = 500
            ElseIf grid.ID = "gridTextSearchAll" Then
                gridTextPanelAll.Height = 500
            End If
        Else
            If (count = 0) Then
                If grid.ID = "gridMergedSearchAll" Then
                    gridMergedPanelAll.Height = 0
                ElseIf grid.ID = "gridPhoneticSearchAll" Then
                    gridPhoneticPanelAll.Height = 0
                ElseIf grid.ID = "gridOrthographicSearchAll" Then
                    gridOrthographicPanelAll.Height = 0
                ElseIf grid.ID = "gridTextSearchAll" Then
                    gridTextPanelAll.Height = 0
                End If
            Else
                If grid.ID = "gridMergedSearchAll" Then
                    gridMergedPanelAll.Height = count * 25 + 30
                ElseIf grid.ID = "gridPhoneticSearchAll" Then
                    gridPhoneticPanelAll.Height = count * 25 + 30
                ElseIf grid.ID = "gridOrthographicSearchAll" Then
                    gridOrthographicPanelAll.Height = count * 25 + 30
                ElseIf grid.ID = "gridTextSearchAll" Then
                    gridTextPanelAll.Height = count * 25 + 30
                End If
            End If
        End If
    End Function

    'Finds a control on a page given the page and the name
    Function FindControlRecursive(ByVal Root As Control, ByVal Name As String)
        If Root.ID = Name Then
            Return Root
        End If

        Dim ctl As Control
        For Each ctl In Root.Controls
            If (Not IsDBNull(FindControlRecursive(ctl, Name))) Then
                Return FindControlRecursive(ctl, Name)
            End If
        Next

        Return DBNull.Value
    End Function

    Private Function GetProductDetails(ByVal ProductName As String, ByVal DataSourceName As String) As String

        Dim ProductDetails As String = ""
        'Dim OraParams() As OracleParameter = New OracleParameter(3) {}
        Dim OraParams() As OracleParameter = New OracleParameter(2) {}
        Dim ods As DataSet
        Dim PO As FDA.Person.PersonObject = Session("LoggedInUser")
        Dim myView As DataView

        Try
            OraParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("product_name_in", OracleDbType.Varchar2)
            OraParams(1).Value = ProductName

            'OraParams(2) = New OracleParameter("ds_list_in", OracleDbType.Varchar2)
            'OraParams(2).Value = DataSourceName

            'OraParams(3) = New OracleParameter("the_list", OracleDbType.RefCursor, ParameterDirection.Output)
            'OraParams(3).Value = Nothing

            OraParams(2) = New OracleParameter("the_list", OracleDbType.RefCursor, ParameterDirection.Output)
            OraParams(2).Value = Nothing

            ods = FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "static_detail_get", OraParams)
            'ods = FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "get_product_details", OraParams)
            myView = ods.Tables(0).DefaultView
            'myView.RowFilter = String.Format("{0} IN ({1})", "DATA_SOURCE", DataSourceName.Replace(";", ","))
            myView.Sort = "DATA_SOURCE" & " ASC"

            If ods.Tables(0).DefaultView.Count = 0 Then
                ProductDetails = "There are no product details"
            Else
                Dim row As DataRow
                For Each row In myView.ToTable().Rows
                    If DataSourceName.ToUpper().Contains(row.Item("DATA_SOURCE").ToString().TrimEnd().ToUpper()) Then
                        If ProductDetails.Equals("") = False Then
                            'ProductDetails = ProductDetails & " vbLf "
                            'ProductDetails = ProductDetails & " \r\n"
                            'ProductDetails = ProductDetails & Environment.NewLine
                            'ProductDetails = ProductDetails & "&#13;&#10;"
                            ProductDetails = ProductDetails & " <br style='mso-data-placement:same-cell;' />"
                        End If

                        ProductDetails = ProductDetails & row.Item("DATA_SOURCE").ToString() & ": "
                        'ProductDetails = ProductDetails & row.Item("DOSAGE_FORM").ToString() & " | "
                        'ProductDetails = ProductDetails & row.Item("POTENCY").ToString() & " | "
                        'ProductDetails = ProductDetails & row.Item("ROUTE").ToString() & " | "
                        'ProductDetails = ProductDetails & row.Item("ACTIVE_INGREDIENT").ToString()

                        Dim dosage As String = row.Item("DOSAGE_FORM").ToString().Replace("N/A", "")
                        If (dosage.ToUpper().Equals("SEE ACTIVE INGREDIENT") = False) And (dosage.ToUpper().Equals("") = False) Then
                            ProductDetails = ProductDetails & dosage & " | "
                        End If

                        Dim potency As String = row.Item("POTENCY").ToString().Replace("N/A", "")
                        If potency.ToUpper().Equals("") = False Then
                            ProductDetails = ProductDetails & potency & " | "
                        End If

                        Dim route As String = row.Item("ROUTE").ToString().Replace("N/A", "")
                        If route.ToUpper().Equals("") = False Then
                            ProductDetails = ProductDetails & route & " | "
                        End If

                        Dim activeIngredient As String = row.Item("ACTIVE_INGREDIENT").ToString().Replace("N/A", "")
                        If activeIngredient.ToUpper().Equals("") = False Then
                            ProductDetails = ProductDetails & activeIngredient
                        End If
                    End If
                Next

            End If

        Catch oe As OracleException

        End Try

        Return ProductDetails
    End Function

    Private Function GetProductDetails_DBFormatted(ByVal ProductName As String, ByVal DataSourceName As String) As String

        Dim ProductDetails As String = ""
        Dim OraParams() As OracleParameter = New OracleParameter(3) {}
        Dim ods As DataSet
        Dim PO As FDA.Person.PersonObject = Session("LoggedInUser")

        Try
            OraParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("product_name_in", OracleDbType.Varchar2)
            OraParams(1).Value = ProductName

            OraParams(2) = New OracleParameter("ds_list_in", OracleDbType.Varchar2)
            OraParams(2).Value = DataSourceName

            OraParams(3) = New OracleParameter("the_list", OracleDbType.RefCursor, ParameterDirection.Output)
            OraParams(3).Value = Nothing

            ods = FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "get_product_details_big", OraParams)

            If ods.Tables(0).DefaultView.Count = 0 Then
                ProductDetails = "There are no product details"
            Else
                ProductDetails = ods.Tables(0).Rows(0).Item("LIST_DETAILS").ToString
            End If

        Catch oe As OracleException

        End Try

        Return ProductDetails
    End Function

    Private Function DecodedQueryValue() As String
        Dim TempUrl As String = HttpUtility.UrlDecode(Request.RawUrl())
        Dim i As Integer = TempUrl.IndexOf("prdname=") + 8
        TempUrl = TempUrl.Substring(i, TempUrl.Length - i)
        Return TempUrl
    End Function

End Class