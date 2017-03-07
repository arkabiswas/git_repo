Imports MetaBuilders.WebControls

Partial Class DeleteRecord
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents searchTermResults As System.Web.UI.WebControls.Repeater

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        errorMessage.Visible = False
        searchResults.Visible = True
        preloadSearch()
    End Sub

    Private Sub searchSubmitButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles searchSubmitButton.Click
        GenerateTextSearch()
    End Sub

    Private Sub GenerateTextSearch()
        Dim po As FDA.Person.PersonObject = Session("LoggedInUser")
        Dim dataSourceCommaList As String = String.Empty

        If Cache("dataSourceCommaList") Is Nothing Then
            Dim dataSourceList As Oracle.DataAccess.Client.OracleDataReader = SearchEngineData.BindList("record_source", po.UserName)
            While dataSourceList.Read
                If dataSourceList("description").ToString().ToUpper() <> "NEW CONSULT" Then
                    If dataSourceCommaList.Length > 0 Then dataSourceCommaList += ","
                    dataSourceCommaList += dataSourceList("pick_list_item_id").ToString()
                End If
            End While
            dataSourceList.Close()
            ' we'll add the list to the cache.
            Cache.Add("dataSourceCommaList", dataSourceCommaList, Nothing, DateTime.MaxValue, New TimeSpan(0, 10, 0), Caching.CacheItemPriority.Default, Nothing)
        Else
            dataSourceCommaList = Cache("dataSourceCommaList")
        End If

        searchResults.DataSource = SearchEngineData.TextSearchForDelete(searchTerm.Text, dataSourceCommaList)
        searchResults.DataBind()

        If searchResults.Items.Count = 0 Then
            searchResults.Visible = False
            errorMessage.Text = "There were no matches found."
            errorMessage.Visible = True
        End If
    End Sub

    Public Sub ConfirmDelete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim RecID As String = ""
        For Each RowItem As DataGridItem In searchResults.Items
            Dim curSelected As RadioButton = CType(RowItem.Cells(0).Controls(1), RadioButton)
            If curSelected.Checked Then
                RecID = searchResults.DataKeys(RowItem.ItemIndex)
                Exit For
            End If
        Next

        If RecID.Length = 0 Then
            errorMessage.Text = "You must select an item."
            errorMessage.Visible = "true"
        Else
            Server.Transfer("deleterecordconfirm.aspx?id=" + RecID)
        End If

        'Updated for 508 Compliacy
        'Dim selectionColumn As RowSelectorColumn = RowSelectorColumn.FindColumn(searchResults)

        'If selectionColumn.SelectedIndexes.Length = 0 Then
        '    errorMessage.Text = "You must select an item."
        '    errorMessage.Visible = "true"
        'Else
        '    Dim selectedItem As Integer = selectionColumn.SelectedIndexes(0)
        '    Server.Transfer("deleterecordconfirm.aspx?id=" + searchResults.DataKeys(selectedItem))
        'End If
    End Sub

    Public Sub preloadSearch()
        Dim searchQuery As String
        'Dim preLoad

        'searchQuery = Request.QueryString.Get("query")

        'If Session("PreLoad") = Nothing Then
        '    preLoad = Request.QueryString.Get("preload")
        'Else
        '    Session("PreLoad") = Nothing
        '    preLoad = ""
        'End If

        If Not Session("SearchQuery") = Nothing Then
            searchQuery = CStr(Session("SearchQuery"))

            If searchQuery <> "" And Session("PreLoad") = True Then
                Session("PreLoad") = False
                searchTerm.Text = searchQuery
                GenerateTextSearch()
            End If
        End If

        'Session.Remove("SearchQuery")
        'Session.Add("SearchQuery", searchTerm.Text())
        Session("SearchQuery") = searchTerm.Text()
    End Sub

    Public Sub rdoDataCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        For Each RowItem As DataGridItem In searchResults.Items
            Dim curSelected As RadioButton = CType(RowItem.Cells(0).Controls(1), RadioButton)
            If curSelected.Checked Then
                curSelected.Checked = False
            End If
        Next

        'ADD New Checkbox Value
        Dim NewRow As RadioButton = sender
        Dim row As DataGridItem = NewRow.NamingContainer
        NewRow.FindControl("rdoData")
        NewRow.Checked = True

    End Sub
End Class
