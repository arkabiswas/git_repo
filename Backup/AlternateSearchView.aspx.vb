'Imports PPC.FDA.Controls
'Imports PPC.FDA.modules
'Imports Oracle.DataAccess.Client

'Public Class AlternateSearchView
'    Inherits System.Web.UI.Page
'    Protected WithEvents SearchText As System.Web.UI.WebControls.Label
'    Protected WithEvents MergedGrid As FDAGrid
'    Protected WithEvents MergedMatchLabel As System.Web.UI.WebControls.Label
'    Protected WithEvents MergedNotFound As System.Web.UI.WebControls.Label
'    Protected WithEvents OrthoGrid As FDAGrid
'    Protected WithEvents OrthoMatchLabel As System.Web.UI.WebControls.Label
'    Protected WithEvents OrthoNotFound As System.Web.UI.WebControls.Label
'    Protected WithEvents PhoneticGrid As FDAGrid
'    Protected WithEvents PhoneticMatchLabel As System.Web.UI.WebControls.Label
'    Protected WithEvents PhoneticNotFound As System.Web.UI.WebControls.Label
'    Protected WithEvents TextGrid As FDAGrid
'    Protected WithEvents TextMatchLabel As System.Web.UI.WebControls.Label
'    Protected WithEvents TextNotFound As System.Web.UI.WebControls.Label
'    Protected ItemsPerPage As Integer
'    Protected PO As PPC.FDA.Person.PersonObject
'    Protected DataSources As Hashtable
'    Protected SearchSources As Hashtable

'#Region " Web Form Designer Generated Code "

'    'This call is required by the Web Form Designer.
'    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

'    End Sub

'    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
'        'CODEGEN: This method call is required by the Web Form Designer
'        'Do not modify it using the code editor.
'        InitializeComponent()
'    End Sub

'#End Region

'    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
'        'Put user code to initialize the page here

'        If Request.Item("view") = "excel" Then
'            Response.ContentType = "application/vnd.ms-excel"
'            ' Remove the charset from the Content-Type header.
'            Response.Charset = ""
'        End If

'        If Not IsPostBack Then
'            PO = Session("LoggedInUser")
'            ItemsPerPage = Integer.Parse(PO.GetSettingValue("RESULTS_PER_PAGE"))

'            Dim SearchTerm As String
'            Dim NameDataSet As DataSet

'            Try
'                SearchTerm = Request("sterm")
'                SearchText.Text = SearchTerm
'                NameDataSet = GetDataSet()
'                DataSources = CType(Session("DataSources"), Hashtable)
'                SearchSources = CType(Session("SearchSources"), Hashtable)

'            Catch ex As Exception
'                SearchText.Text = "Nothing"
'            End Try

'            If Request("mgrid") = "True" Then
'                LoadDataGrid(NameDataSet, SearchTerm, "Merged", MergedGrid, MergedMatchLabel, MergedNotFound, "MergedCost", "MergedCost", 0)
'            End If

'            If Request("ogrid") = "True" Then
'                LoadDataGrid(NameDataSet, SearchTerm, "Orthographic", OrthoGrid, OrthoMatchLabel, OrthoNotFound, "cost", "cost", 0)
'            End If

'            If Request("pgrid") = "True" Then
'                LoadDataGrid(NameDataSet, SearchTerm, "Phonetic", PhoneticGrid, PhoneticMatchLabel, PhoneticNotFound, "costphonetic", "costphonetic", 0)
'            End If

'            If Request("tgrid") = "True" Then
'                DoTextSearch(NameDataSet, SearchTerm)
'            End If

'        End If
'    End Sub

'#Region "DoTextSearch"
'    Private Sub DoTextSearch(ByVal NameData As DataSet, ByVal SearchTerm As String)

'        Dim ods As DataSet
'        Dim odv As DataView

'        Dim oraparams() As OracleParameter = New OracleParameter(1) {}

'        oraparams(0) = New OracleParameter("TERM_IN", OracleDbType.Varchar2)
'        oraparams(0).Value = SearchTerm

'        oraparams(1) = New OracleParameter("THE_NAMES", OracleDbType.RefCursor, ParameterDirection.Output)
'        oraparams(1).Value = Nothing

'        Try
'            ods = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "SEARCH_SIMPLE_NAME", oraparams)
'            odv = New DataView(ods.Tables(0))

'            If odv.Count > 0 Then

'                TextGrid.DataSource = odv
'                TextGrid.DataBind()

'                TextGrid.Visible = True
'                TextMatchLabel.Visible = True
'            Else
'                TextMatchLabel.Visible = True
'                TextNotFound.Text = "There are no names found."
'                TextNotFound.Visible = True
'            End If

'        Catch oe As OracleException
'            ' don't do anything.
'        End Try
'    End Sub
'#End Region

'    Private Sub LoadDataGrid(ByVal NameData As DataSet, ByVal SearchTerm As String, ByVal TypeOfGrid As String, ByVal DGToBind As FDAGrid, ByVal NameLabel As Label, ByVal ErrorLabel As Label, ByVal SortExpression As String, ByVal RowFilter As String, ByVal PageNumber As Integer)

'        Dim SearchClass As Object
'        Dim SearchThreshold As Integer
'        Dim SearchDV As DataView

'        Select Case TypeOfGrid

'            Case "Merged"
'                SearchClass = New Merged()
'                SearchThreshold = Integer.Parse(PO.GetSettingValue("ORTHOGRAPHIC_THRESHOLD"))
'                NameLabel.Text = "<br /><strong><em>Orthographic & Phonetic Matches</em></strong> matches greater then " + SearchThreshold.ToString() + "%"

'            Case "Orthographic"
'                SearchClass = New Orthographic()
'                SearchThreshold = Integer.Parse(PO.GetSettingValue("ORTHOGRAPHIC_THRESHOLD"))
'                NameLabel.Text = "<br /><strong><em>Orthographic Matches</em></strong> matches greater then " + SearchThreshold.ToString() + "%"

'            Case "Phonetic"
'                SearchClass = New Phonetic()
'                SearchThreshold = Integer.Parse(PO.GetSettingValue("PHONETIC_THRESHOLD"))
'                NameLabel.Text = "<br /><strong><em>Phonetic Matches</em></strong> matches greater then " + SearchThreshold.ToString() + "%"

'            Case Else
'        End Select

'        Try

'            'SearchDV = New DataView(SearchClass.Calculate(NameData, SearchTerm).Tables(0), RowFilter + " > " + SearchThreshold.ToString(), RowFilter + " DESC", DataViewRowState.CurrentRows)
'            If DataSources.Contains("Additional Factors") And TypeOfGrid = "Merged" Then
'                Dim SearchDataSet As DataSet
'                Dim OrthoWeight, PhonoWeight, Aweight As String
'                OrthoWeight = PO.GetSettingValue("ORTHO_WEIGHT")
'                PhonoWeight = PO.GetSettingValue("PHONO_WEIGHT")
'                Aweight = PO.GetSettingValue("FAX_WEIGHT")

'                SearchDataSet = SearchClass.Calculate(NameData, SearchTerm, OrthoWeight, PhonoWeight, Aweight)
'                SearchDV = New DataView(SearchDataSet.Tables(0), RowFilter + ">" + SearchThreshold.ToString(), RowFilter + " DESC", DataViewRowState.CurrentRows)
'            Else
'                SearchDV = New DataView(SearchClass.Calculate(NameData, SearchTerm).Tables(0), RowFilter + " > " + SearchThreshold.ToString(), RowFilter + " DESC", DataViewRowState.CurrentRows)
'            End If

'            If SearchDV.Count > 0 Then

'                Dim SortString As String = DGToBind.SortExpression

'                If Not SortString Is Nothing Then
'                    SearchDV.Sort = SortString
'                    If Not DGToBind.IsSortedAscending Then
'                        SearchDV.Sort += " DESC"
'                    End If
'                End If

'                DGToBind.DataSource = SearchDV
'                DGToBind.DataBind()

'                DGToBind.Visible = True
'                NameLabel.Visible = True
'            End If

'        Catch ex As Exception
'            'Nothing
'        End Try

'    End Sub

'#Region "GetDataSet Function"
'    Private Function GetDataSet() As DataSet

'        Dim TempTable As DataTable
'        Dim CacheDataSet, SearchDataSet As DataSet

'        ' Create the Dataset that will be used for the following 
'        ' search procedures. First we will check the cache to see if we can use 
'        ' that, if not we will create a new dataset and then cache that.

'        CacheDataSet = Cache("SearchDataSet")
'        SearchDataSet = New DataSet()

'        'Check for the Cached DataSet If it's not there then we will get a fresh
'        'copy of the data and then cache it. 
'        If CacheDataSet Is Nothing Then
'            CacheDataSet = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.Text, "select DISTINCT(u_name) as ProductName from MR_product_name")
'            Cache("SearchDataSet") = CacheDataSet
'        End If

'        ' Since cached objects are actually pointers to the data, if we make any changes
'        ' to the dataset after this, it will be reflected in the cache. So we create a new dataset
'        ' and copy the table from the cached one to the new one
'        TempTable = CacheDataSet.Tables(0).Copy()
'        SearchDataSet.Tables.Add(TempTable)

'        Return SearchDataSet

'    End Function
'#End Region

'End Class
