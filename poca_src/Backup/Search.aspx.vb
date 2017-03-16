Imports PPC.FDA.Controls
Imports PPC.SearchEngine
Imports MetaBuilders.WebControls
Imports System.IO
Imports Microsoft.VisualBasic.CompilerServices


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

    Public Sub ToggleSelectAll(ByVal sender As Object, ByVal e As EventArgs)
        Dim grid As DataGrid
        grid = CType(sender.parent.parent.parent.parent, DataGrid)

        Dim checked As Boolean = CType(sender.checked, Boolean)

        For Each DemoGridItem As DataGridItem In grid.Items
            Dim curCheckbox As CheckBox = CType(DemoGridItem.Cells(0).Controls(1), CheckBox)
            curCheckbox.Checked = checked
        Next
    End Sub

    Private Sub AddToWatchList(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles addToWatchBottom.Click, addToWatchTop.Click

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

        ClientScript.RegisterStartupScript(Me.GetType(), "addtowl", "<script language='javascript'>window.open('addtowl.aspx?items=" + qryString + "', '_blank', 'toolbars=no,statusbar=no,scrollbars=yes,resizable=yes');<" + "/script>")
        'RegisterStartupScript
    End Sub

    '''
    '''
    Private Sub DoSearch()
        Try
            GenerateData()
            If (searchEng.PhoneticSearch And searchEng.OrthoSearch) Then SetupDataGrid(gridMergedSearch, gridMergedHeader, "Orthographic & Phonetic", "MergedScore")
            If searchEng.PhoneticSearch Then SetupDataGrid(gridPhoneticSearch, gridPhoneticHeader, "Phonetic", "PhoneticScore")
            If searchEng.OrthoSearch Then SetupDataGrid(gridOrthographicSearch, gridOrthographicHeader, "Orthographic", "OrthographicScore")
            If searchEng.TextSearch Then SetupDataGrid(gridTextSearch, gridTextHeader, "Text", "Name")

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
            Dim text1 As String = GridToEdit.Columns(2).SortExpression
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
            If (ObjectType.ObjTst(SearchResultsSort, obj2, False) = 0) Then
                If (ObjectType.ObjTst(obj1, "Yes", False) = 0) Then
                    GridToEdit.Attributes.Item("SortASC") = "No"
                Else
                    GridToEdit.Attributes.Item("SortASC") = "Yes"
                End If
            End If
        End If
        view1.Sort = GridToEdit.Attributes.Item("SortExpression")
        If (StringType.StrCmp(GridToEdit.Attributes.Item("SortASC"), "No", False) = 0) Then
            Dim view2 As DataView = view1
            view2.Sort = (view2.Sort & " DESC")
        End If
        GridToEdit.DataSource = view1
        GridToEdit.PageSize = SearchResultsPageSize
        If (view1.Count <= SearchResultsPageSize) Then
            GridToEdit.AllowPaging = False
        End If
    End Sub

    Private Function GridHeader(ByVal GridName As String, ByVal MatchPercent As String) As String
        If (StringType.StrCmp(GridName, "Text", False) = 0) Then
            Return String.Format("<span class=""gridTitle"">{0} Matches</span>", GridName)
        End If
        Return String.Format("<span class=""gridTitle"">{0} Matches</span> greater then {1}%", GridName, MatchPercent)
    End Function

    Private Sub GridSortCommand(ByVal source As Object, ByVal e As DataGridSortCommandEventArgs) Handles gridMergedSearch.SortCommand, gridOrthographicSearch.SortCommand, gridPhoneticSearch.SortCommand, gridTextSearch.SortCommand
        Dim ClickedSortHeader As DataGridItem = CType(e.CommandSource, DataGridItem)
        Dim SortedGrid As FDAGrid = CType(ClickedSortHeader.Parent.Parent, FDAGrid)
        GenerateData()
        SetupDataGrid(SortedGrid, e.SortExpression)
        SortedGrid.DataBind()
    End Sub

    Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Init
        Me.InitializeComponent()
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
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

    Private Sub PageIndexChanged(ByVal source As Object, ByVal e As DataGridPageChangedEventArgs) Handles gridMergedSearch.PageIndexChanged, gridPhoneticSearch.PageIndexChanged, gridOrthographicSearch.PageIndexChanged, gridTextSearch.PageIndexChanged
        Dim item1 As DataGridItem = CType(e.CommandSource, DataGridItem)

        Dim grid1 As FDAGrid = CType(item1.Parent.Parent, FDAGrid)
        grid1.CurrentPageIndex = e.NewPageIndex
        Me.GenerateData()
        Dim grid2 As DataGrid = grid1

        If grid2.ID = "gridMergedSearch" Then SetupDataGrid(grid2, gridMergedHeader, "Orthographic & Phonetic", Nothing)
        If grid2.ID = "gridPhoneticSearch" Then SetupDataGrid(grid2, gridPhoneticHeader, "Phonetic", Nothing)
        If grid2.ID = "gridOrthographicSearch" Then SetupDataGrid(grid2, gridOrthographicHeader, "Orthographic", Nothing)
        If grid2.ID = "gridTextSearch" Then SetupDataGrid(grid2, gridTextHeader, "Text", Nothing)

        grid1 = CType(grid2, FDAGrid)
        grid1.DataBind()
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


    Private Sub SetupDataGrid(ByRef GridToSetup As DataGrid, ByVal GridLabel As Label, ByVal GridTitle As String, ByVal GridSort As String)

        Try
            GetCurrentPageInformation(GridToSetup, Me.searchEngineResults.Tables.Item(1).Rows.Count)
            Dim pageInformation As String

            If (GridToSetup.ID.IndexOf("Text") > -1) Then
                Me.GenerateDataGrid(Me.searchEngineResults.Tables.Item(1), GridToSetup, Me.searchEng.ItemsPerPage, "", GridSort)
            Else
                Me.GenerateDataGrid(Me.searchEngineResults.Tables.Item(0), GridToSetup, Me.searchEng.ItemsPerPage, StringType.FromInteger(Me.searchEng.ResultThreshold), GridSort)
            End If

            If (Not GridLabel Is Nothing) Then
                Dim dvResults As DataView = CType(GridToSetup.DataSource, DataView)
                pageInformation = GetCurrentPageInformation(GridToSetup, dvResults.Count)
                GridLabel.Text = Me.GridHeader(GridTitle, Me.searchEng.ResultThreshold.ToString) & pageInformation
                GridLabel.Visible = True
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
            Dim label1 As Label = CType(Me.FindControl(GridToSetup.ID.Replace("Search", "Message")), Label)
            label1.Text = exception1.Message
            label1.Visible = True
            ProjectData.ClearProjectError()
        End Try
    End Sub

End Class