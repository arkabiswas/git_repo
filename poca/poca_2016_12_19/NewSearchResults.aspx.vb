'Imports PPC.FDA.Modules
Imports PPC.FDA.Controls
Imports MetaBuilders.WebControls
Imports Oracle.DataAccess.Client

Partial Class NewSearchResults
    Inherits System.Web.UI.Page
    Protected PO As PPC.FDA.Person.PersonObject
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents Imagebutton1 As System.Web.UI.WebControls.ImageButton
    Protected WithEvents Button2 As System.Web.UI.WebControls.Button
    Protected ItemsPerPage As Integer
    Protected DataSources As Hashtable
    Protected SearchSources As Hashtable

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        PO = Session("LoggedInUser")
        ItemsPerPage = Integer.Parse(PO.GetSettingValue("RESULTS_PER_PAGE"))
        Dim searchctl As SimpleSearchControl = Page.FindControl("SimpleSearchControl1")

        If Not IsPostBack Then

            Dim SearchTerm As String
            Dim NameDataSet As DataSet
            Dim SearchItems() As String = New String(3) {}

            Try
                SearchTerm = CStr(Context.Items("SearchTerm"))
                DataSources = CType(Session("DataSources"), Hashtable)
                SearchSources = CType(Session("SearchSources"), Hashtable)
                Dim FromArchive As Boolean = CBool(context.Items("FromArchive"))

                ' Add this to the search archive. 
                If Not FromArchive Then
                    AddToArchive(SearchTerm, DataSources, SearchSources)
                End If

                searchctl.SearchTextField = SearchTerm

                Viewstate.Add("_SearchTerm", SearchTerm)
                Viewstate.Add("_Datasources", DataSources)
                viewstate.Add("_Searchsources", SearchSources)

                SearchText.Text = SearchTerm
                NameDataSet = GetDataSet(DataSources)

            Catch ex As Exception
                SearchText.Text = "Nothing"
            End Try

            If Context.Items("OrthoSearch") <> "" And Context.Items("PhoneSearch") <> "" Then
                SearchItems(0) = "MergedSearch"
                LoadDataGrid(NameDataSet, SearchTerm, "Merged", MergedGrid, MergedMatchLabel, MergedNotFound, "MergedCost", "MergedCost", 0)
            End If

            If Context.Items("OrthoSearch") <> "" Then
                SearchItems(1) = "OrthoSearch"
                searchctl.OrthoChecked = True
                LoadDataGrid(NameDataSet, SearchTerm, "Orthographic", OrthoGrid, OrthoMatchLabel, OrthoNotFound, "cost", "cost", 0)
            End If

            If Context.Items("PhoneSearch") <> "" Then
                searchctl.PhoneChecked = True
                SearchItems(2) = "PhoneSearch"
                LoadDataGrid(NameDataSet, SearchTerm, "Phonetic", PhoneticGrid, PhoneticMatchLabel, PhoneticNotFound, "costphonetic", "costphonetic", 0)
            End If

            If Context.Items("TextSearch") <> "" Then
                SearchItems(3) = "TextSearch"
                searchctl.TextChecked = True
                DoTextSearch(SearchTerm)
            End If

        End If

    End Sub

    Private Sub SetDatasources()
        DataSources = CType(ViewState("_Datasources"), Hashtable)
    End Sub

#Region "DoTextSearch"
    Private Sub DoTextSearch(ByVal SearchTerm As String)

        Dim ods As DataSet
        Dim odv As DataView
        Dim DataSourceList As String = ""
        Dim KeyValue As DictionaryEntry
        Dim oraparams() As OracleParameter = New OracleParameter(2) {}

        If Not DataSources Is Nothing Then
            For Each KeyValue In DataSources
                If Not KeyValue.Value = "addl_fax" Then
                    If Not DataSourceList = "" Then
                        DataSourceList = DataSourceList + ","
                    End If
                    DataSourceList = DataSourceList + KeyValue.Value
                End If
            Next
            If DataSourceList = "" Then
                DataSourceList = ConfigurationManager.AppSettings("DefaultDataSource")
            End If
        End If

        oraparams(0) = New OracleParameter("TERM_IN", OracleDbType.Varchar2)
        oraparams(0).Value = SearchTerm

        oraparams(1) = New OracleParameter("source_list_in", OracleDbType.Long)
        oraparams(1).Value = DataSourceList

        oraparams(2) = New OracleParameter("THE_NAMES", OracleDbType.RefCursor, ParameterDirection.Output)
        oraparams(2).Value = Nothing

        Try
            ods = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "SEARCH_SIMPLE_NAME", oraparams)
            odv = New DataView(ods.Tables(0))

            If odv.Count > 0 Then
                If odv.Count > ItemsPerPage Then
                    TextGrid.AllowPaging = True
                End If

                TextGrid.PageSize = ItemsPerPage
                TextGrid.DataSource = ods
                TextGrid.DataBind()

                TextGrid.Visible = True
                TextMatchLabel.Visible = True
            Else
                TextMatchLabel.Visible = True
                TextNotFound.Text = "There are no names found."
                TextNotFound.Visible = True
            End If

        Catch oe As OracleException
            ' don't do anything.
        End Try
    End Sub

#End Region

#Region "LoadDataGrid"
    Private Sub LoadDataGrid(ByVal NameData As DataSet, ByVal SearchTerm As String, ByVal TypeOfGrid As String, ByVal DGToBind As FDAGrid, ByVal NameLabel As Label, ByVal ErrorLabel As Label, ByVal SortExpression As String, ByVal RowFilter As String, ByVal PageNumber As Integer)

        'Dim SearchClass As Object
        'Dim SearchThreshold As Integer
        'Dim SearchDV As DataView

        'Select Case TypeOfGrid

        '    Case "Merged"
        '        '      SearchClass = New Merged
        '        SearchThreshold = Integer.Parse(PO.GetSettingValue("ORTHOGRAPHIC_THRESHOLD"))
        '        NameLabel.Text = "<br /><strong><em>Orthographic & Phonetic Matches</em></strong> matches greater than " + SearchThreshold.ToString() + "%"

        '    Case "Orthographic"
        '        SearchClass = New Orthographic
        '        SearchThreshold = Integer.Parse(PO.GetSettingValue("ORTHOGRAPHIC_THRESHOLD"))
        '        NameLabel.Text = "<br /><strong><em>Orthographic Matches</em></strong> matches greater than " + SearchThreshold.ToString() + "%"

        '    Case "Phonetic"
        '        SearchClass = New Phonetic
        '        SearchThreshold = Integer.Parse(PO.GetSettingValue("PHONETIC_THRESHOLD"))
        '        NameLabel.Text = "<br /><strong><em>Phonetic Matches</em></strong> matches greater than " + SearchThreshold.ToString() + "%"

        '    Case Else
        'End Select

        'Try
        '    If DataSources.Contains("Additional Factors") And TypeOfGrid = "Merged" Then
        '        Dim SearchDataSet As DataSet
        '        Dim OrthoWeight, PhonoWeight, Aweight As String
        '        OrthoWeight = PO.GetSettingValue("ORTHO_WEIGHT")
        '        PhonoWeight = PO.GetSettingValue("PHONO_WEIGHT")
        '        Aweight = PO.GetSettingValue("FAX_WEIGHT")

        '        SearchDataSet = SearchClass.Calculate(NameData, SearchTerm, OrthoWeight, PhonoWeight, Aweight)
        '        SearchDV = New DataView(SearchDataSet.Tables(0), RowFilter + ">" + SearchThreshold.ToString(), RowFilter + " DESC", DataViewRowState.CurrentRows)
        '    Else
        '        SearchDV = New DataView(SearchClass.Calculate(NameData, SearchTerm).Tables(0), RowFilter + " > " + SearchThreshold.ToString(), RowFilter + " DESC", DataViewRowState.CurrentRows)
        '    End If

        '    If SearchDV.Count > 0 Then

        '        If SearchDV.Count > ItemsPerPage Then
        '            DGToBind.AllowPaging = True
        '        End If

        '        DGToBind.PageSize = ItemsPerPage

        '        Dim SortString As String = DGToBind.SortExpression

        '        If Not SortString Is Nothing Then
        '            SearchDV.Sort = SortString
        '            If Not DGToBind.IsSortedAscending Then
        '                SearchDV.Sort += " DESC"
        '            End If
        '        End If

        '        DGToBind.DataKeyField = "u_name"
        '        DGToBind.DataSource = SearchDV
        '        DGToBind.DataBind()

        '        DGToBind.Visible = True
        '        NameLabel.Visible = True
        '    Else
        '        NameLabel.Visible = True
        '        ErrorLabel.Visible = True
        '        ErrorLabel.Text = " There were no matches found."
        '    End If

        'Catch ex As Exception
        '    ' Nothing
        'End Try

    End Sub
#End Region

#Region "GetDataSet"
    Private Function GetDataSet(ByVal DataSources As Hashtable) As DataSet

        Dim TempTable As DataTable
        Dim CacheDataSet, SearchDataSet As DataSet

        ' Create the Dataset that will be used for the following 
        ' search procedures. First we will check the cache to see if we can use 
        ' that, if not we will create a new dataset and then cache that.

        Dim DataSourceList As String = ""
        Dim CacheName As String = ""
        Dim KeyValue As DictionaryEntry

        If Not DataSources Is Nothing Then
            For Each KeyValue In DataSources
                If Not KeyValue.Value = "addl_fax" Then
                    If Not DataSourceList = "" Then
                        DataSourceList = DataSourceList + ","
                    End If
                    CacheName = CacheName + KeyValue.Value
                    DataSourceList = DataSourceList + KeyValue.Value
                End If
            Next
            If DataSourceList = "" Then
                DataSourceList = ConfigurationManager.AppSettings("DefaultDataSource")
                CacheName = ConfigurationManager.AppSettings("DefaultDataSource").Replace(",", "")
            End If
        End If

        '      CacheDataSet = Cache(CacheName + "CacheDataSet")
        SearchDataSet = New DataSet

        'Check for the Cached DataSet If it's not there then we will get a fresh
        'copy of the data and then cache it. 
        If CacheDataSet Is Nothing Then

            Dim OraParams() As OracleParameter = New OracleParameter(2) {}

            Try


                OraParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
                OraParams(0).Value = PO.UserName

                OraParams(1) = New OracleParameter("source_list_in", OracleDbType.Varchar2)
                OraParams(1).Value = DataSourceList

                OraParams(2) = New OracleParameter("name_list_out", OracleDbType.RefCursor, ParameterDirection.Output)
                OraParams(2).Value = Nothing

                CacheDataSet = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "search_name_get.search_name_get", OraParams)

            Catch oe As OracleException
                Response.Redirect("error.aspx")
            End Try

            '            Cache.Add(CacheName + "CacheDataSet", CacheDataSet, Nothing, Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1), Caching.CacheItemPriority.Default, Nothing)
        End If

        ' Since cached objects are actually pointers to the data, if we make any changes
        ' to the dataset after this, it will be reflected in the cache. So we create a new dataset
        ' and copy the table from the cached one to the new one
        TempTable = CacheDataSet.Tables(0).Copy()
        SearchDataSet.Tables.Add(TempTable)

        Return SearchDataSet

    End Function
#End Region

    Public Sub PhoneticGrid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles PhoneticGrid.SortCommand
        SetDatasources()
        LoadDataGrid(GetDataSet(DataSources), ViewState("_SearchTerm"), "Phonetic", PhoneticGrid, PhoneticMatchLabel, PhoneticNotFound, e.SortExpression, "CostPhonetic", 0)
    End Sub

    Public Sub PhoneticGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles PhoneticGrid.PageIndexChanged
        SetDatasources()
        PhoneticGrid.CurrentPageIndex = e.NewPageIndex
        LoadDataGrid(GetDataSet(DataSources), ViewState("_SearchTerm"), "Phonetic", PhoneticGrid, PhoneticMatchLabel, PhoneticNotFound, PhoneticGrid.SortExpression, "CostPhonetic", 0)
    End Sub

    Public Sub OrthoGrid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles OrthoGrid.SortCommand
        SetDatasources()
        LoadDataGrid(GetDataSet(DataSources), ViewState("_SearchTerm"), "Orthographic", OrthoGrid, OrthoMatchLabel, OrthoNotFound, e.SortExpression, "Cost", 0)
    End Sub

    Public Sub OrthoGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles OrthoGrid.PageIndexChanged
        SetDatasources()
        OrthoGrid.CurrentPageIndex = e.NewPageIndex
        LoadDataGrid(GetDataSet(DataSources), ViewState("_SearchTerm"), "Orthographic", OrthoGrid, OrthoMatchLabel, OrthoNotFound, OrthoGrid.SortExpression, "Cost", 0)
    End Sub

    Public Sub MergedGrid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles MergedGrid.SortCommand
        SetDatasources()
        LoadDataGrid(GetDataSet(DataSources), ViewState("_SearchTerm"), "Merged", MergedGrid, MergedMatchLabel, MergedNotFound, e.SortExpression, "MergedCost", 0)
    End Sub

    Public Sub MergedGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles MergedGrid.PageIndexChanged
        SetDatasources()
        MergedGrid.CurrentPageIndex = e.NewPageIndex
        LoadDataGrid(GetDataSet(DataSources), ViewState("_SearchTerm"), "Merged", MergedGrid, MergedMatchLabel, MergedNotFound, MergedGrid.SortExpression, "MergedCost", 0)
    End Sub

    Public Sub TextGrid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles TextGrid.SortCommand
        SetDatasources()
    End Sub

    Public Sub TextGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles TextGrid.PageIndexChanged
        SetDatasources()
        TextGrid.CurrentPageIndex = e.NewPageIndex
        DoTextSearch(Viewstate("_SearchTerm"))
    End Sub

#Region "AddToArchive"
    Private Sub AddToArchive(ByVal SearchTerm As String, ByVal DataSources As Hashtable, ByVal SearchSources As Hashtable)

        Dim Counter, RetVal As Integer
        Dim DataSourceList As String = ""
        Dim SearchSourceList As String = ""
        Dim KeyValue As DictionaryEntry

        Dim OraParams() As OracleParameter = New OracleParameter(3) {}

        If Not DataSources Is Nothing Then
            For Each KeyValue In DataSources
                If Not DataSourceList = "" Then
                    DataSourceList = DataSourceList + ","
                End If
                DataSourceList = DataSourceList + KeyValue.Value
            Next
        End If

        If Not SearchSources Is Nothing Then
            For Each KeyValue In SearchSources
                If Not SearchSourceList = "" Then
                    SearchSourceList = SearchSourceList + ","
                End If
                SearchSourceList = SearchSourceList + KeyValue.Value
            Next
        End If

        Try
            OraParams(0) = New OracleParameter("search_term_in", OracleDbType.Varchar2)
            OraParams(0).Value = SearchTerm

            OraParams(1) = New OracleParameter("userid_in", OracleDbType.Varchar2)
            OraParams(1).Value = PO.UserID

            OraParams(2) = New OracleParameter("search_type_pick", OracleDbType.Varchar2)
            OraParams(2).Value = ValidateValue(SearchSourceList)

            OraParams(3) = New OracleParameter("data_source_pick", OracleDbType.Varchar2)
            OraParams(3).Value = ValidateValue(DataSourceList)

            RetVal = FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "archive_search_add", OraParams)

            If Not RetVal <> 0 Then
                Trace.Write("Search not saved to the archive : " + SearchTerm + ControlChars.NewLine)
                Trace.Write(ControlChars.Tab + DataSourceList + ControlChars.NewLine)
                Trace.Write(ControlChars.Tab + SearchSourceList + ControlChars.NewLine)
            End If

        Catch oe As OracleException
            Trace.Write("Search not saved to the archive : " + SearchTerm + ControlChars.NewLine)
            Trace.Write("Error Message: " + oe.Message + ControlChars.NewLine)
            Trace.Write(ControlChars.Tab + DataSourceList + ControlChars.NewLine)
            Trace.Write(ControlChars.Tab + SearchSourceList + ControlChars.NewLine)
        End Try

    End Sub
#End Region

    ' Validate's string values to be put into Oracle Parameters. 
    ' Since the parameter's aren't smart enough to tell that "" is also null.
    ' so we change the "" to Nothing and all is good.
    '
    Private Function ValidateValue(ByVal ValidateString As String) As String
        If ValidateString = "" Then
            Return Nothing
        Else
            Return ValidateString
        End If
    End Function

#Region "GenerateCommaList"
    Protected Function GenerateCommaList(ByVal GridToCheck As DataGrid) As String

        Dim _dgitem As String
        Dim _selected As Integer
        Dim _rowselcted As RowSelectorColumn = RowSelectorColumn.FindColumn(GridToCheck)
        Dim _qrystring As String

        Try
            For Each _selected In _rowselcted.SelectedIndexes
                _dgitem = GridToCheck.DataKeys(_selected)

                If _qrystring = String.Empty Then
                    _qrystring = _qrystring + _dgitem
                Else
                    _qrystring = _qrystring + "," + _dgitem
                End If

            Next

        Catch ex As Exception
            Return String.Empty
        End Try

        Return _qrystring

    End Function

#End Region

#Region "AddWatch Click"

    Public Sub btnAddWatch_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim SelectedItemsQryString As String

        ' Get Selected Values from Phonetic Grid
        Try
            Dim PGrid As DataGrid = CType(Page.FindControl("PhoneticGrid"), DataGrid)
            SelectedItemsQryString = GenerateCommaList(PGrid)
        Catch
            ' Do nothing just move on. 
        End Try

        ' Get Selected Values from OrthoGraphic Grid
        Try
            Dim OGrid As DataGrid = CType(Page.FindControl("OrthoGrid"), DataGrid)

            If SelectedItemsQryString = String.Empty Then
                SelectedItemsQryString = GenerateCommaList(OGrid)
            Else
                SelectedItemsQryString = SelectedItemsQryString + "," + GenerateCommaList(OGrid)
            End If

        Catch
            ' Do nothing just move on. 
        End Try

        ' Get Selected Values from Merged Grid
        Try
            Dim MGrid As DataGrid = CType(Page.FindControl("MergedGrid"), DataGrid)

            If SelectedItemsQryString = String.Empty Then
                SelectedItemsQryString = GenerateCommaList(MGrid)
            Else
                SelectedItemsQryString = SelectedItemsQryString + "," + GenerateCommaList(MGrid)
            End If
        Catch
            ' Do nothing just move on. 
        End Try

        'RegisterStartupScript
        ClientScript.RegisterStartupScript(Me.GetType(), "os", "<script language='javascript'>window.open('addtowl.aspx?items=" + SelectedItemsQryString + "', '_blank', 'toolbars=no,statusbar=no,scrollbars=yes,resizable=yes');</script>")
        'Response.Redirect("addtowl.aspx?items=" + SelectedItemsQryString)

    End Sub
#End Region

    Public Sub DGDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles OrthoGrid.ItemDataBound, PhoneticGrid.ItemDataBound, MergedGrid.ItemDataBound, TextGrid.ItemDataBound

        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
            Dim HyperCtrl As HyperLink = New HyperLink

            HyperCtrl.ID = "PrdName"
            HyperCtrl.NavigateUrl = String.Format("javascript:var PopUpWin=window.open('ProductDetail.aspx?PrdName={0}', 'ProductDetails', 'toolbars=no,status=no,height=400,width=500')", drv.Item("u_name").ToString())
            HyperCtrl.Text = drv.Item("u_name").ToString()
            e.Item.Cells(1).Controls.Add(HyperCtrl)

        End If

    End Sub

End Class
