Imports System.Threading
Imports System.Text
Imports Oracle.DataAccess
Imports Oracle.DataAccess.Client


Partial  Class SearchBox
    Inherits UserControl

    Private advanceSearch As Boolean
    Private searchDataSources As String
    Public PO As PPC.FDA.Person.PersonObject


    ' Properties
    Public Property SearchTerm() As String
        Get
            Return Me.SearchText.Text
        End Get
        Set(ByVal Value As String)
            Me.SearchText.Text = Value
        End Set
    End Property

    Public Property SearchSources() As String
        Get
            Return Me.searchDataSources
        End Get
        Set(ByVal Value As String)
            Me.searchDataSources = Value
        End Set
    End Property

    Public Property IsOrthoChecked() As Boolean
        Get
            Return Me.chkOrthographic.Checked
        End Get
        Set(ByVal Value As Boolean)
            Me.chkOrthographic.Checked = Value
        End Set
    End Property

    Public Property IsPhonChecked() As Boolean
        Get
            Return Me.chkPhonetic.Checked
        End Get
        Set(ByVal Value As Boolean)
            Me.chkPhonetic.Checked = Value
        End Set
    End Property

    'Public Property IsSpellingChecked() As Boolean
    '    Get
    '        Return Me.chkSpelling.Checked
    '    End Get
    '    Set(ByVal Value As Boolean)
    '        Me.chkSpelling.Checked = Value
    '    End Set
    'End Property

    Public Property IsTextChecked() As Boolean
        Get
            Return Me.chkText.Checked
        End Get
        Set(ByVal Value As Boolean)
            Me.chkText.Checked = Value
        End Set
    End Property


    Private _addFaxSearch As Boolean
    Private _additionalFactorsWeight As Integer

    Public Property AddFaxSearch() As Boolean
        Get
            Return Me._addFaxSearch
        End Get
        Set(ByVal Value As Boolean)
            Me._addFaxSearch = Value
        End Set
    End Property

    Public Property AdditionalFactorsWeight() As Integer
        Get
            Return Me._additionalFactorsWeight
        End Get
        Set(ByVal Value As Integer)
            Me._additionalFactorsWeight = Value
        End Set
    End Property



    ' Methods
    Public Sub New()
    End Sub

    Public Sub GenerateSearchEngine()

        Dim engine1 As New SearchEngine
        Try

            engine1.SearchTerm = SearchText.Text
            engine1.OrthoSearch = chkOrthographic.Checked
            engine1.OrthographicWeight = Integer.Parse(PO.GetSettingValue("ORTHO_WEIGHT"))
            engine1.PhoneticSearch = chkPhonetic.Checked
            engine1.PhoneticWeight = Integer.Parse(PO.GetSettingValue("PHONO_WEIGHT"))
            'engine1.SpellingSearch = chkSpelling.Checked
            'engine1.SpellingWeight = Integer.Parse(PO.GetSettingValue("ORTHO_WEIGHT"))
            engine1.TextSearch = chkText.Checked

            If Not SearchSources Is Nothing Then
                engine1.DataSources = SearchSources
            Else
                engine1.DataSources = GetSelectedDataSources()
            End If

            engine1.AddFaxSearch = AddFaxSearch
            engine1.AdditionalFactorsWeight = AdditionalFactorsWeight

            engine1.ItemsPerPage = Integer.Parse(PO.GetSettingValue("RESULTS_PER_PAGE"))
            engine1.ResultThreshold = Integer.Parse(PO.GetSettingValue("ORTHOGRAPHIC_THRESHOLD"))
            engine1.CurrentUserId = PO.UserName

            Dim NamesDataset As DataSet = SearchEngineData.GetNamesDataSet(PO.UserName, engine1.DataSources)
            engine1.SearchResults = SearchLogic.PerformSearch(engine1, NamesDataset)

        Catch EngineException As Exception
            SyncLock Session.SyncRoot
                Session.Item("ErrorMessage") = "An error occured during the processing of the records: " & EngineException.Message & Environment.NewLine & EngineException.StackTrace
            End SyncLock
        Finally
            SyncLock Session.SyncRoot
                Session("Engine") = engine1
                Session("FinishedSearch") = True
            End SyncLock
        End Try
    End Sub

    Public Function GetSelectedDataSources()

        'Dim DataSources As String = ConfigurationManager.AppSettings.Item("DefaultDataSource")
        Dim DataSources As String = ""

        If PNLAdvancedDetails.Visible Then
            Dim MultipleDatasources As Boolean = False
            Dim DatasourceList As New StringBuilder

            Dim DataSourceItem As ListItem
            Dim DataGridSourceItem As DataGridItem

            For Each DataGridSourceItem In dgDatasources.Items
                Dim cbDatasources As CheckBox = CType(DataGridSourceItem.FindControl("cbSearch"), CheckBox)

                If cbDatasources.Checked Then
                    Dim pickListId As Integer = DataGridSourceItem.Cells(1).Text
                    Dim description As String = DataGridSourceItem.Cells(2).Text

                    ' Check whether additional factors is checked.
                    If description = "Additional Factors" Then
                        AddFaxSearch = True
                        AdditionalFactorsWeight = Integer.Parse(PO.GetSettingValue("FAX_WEIGHT"))
                    Else
                        If MultipleDatasources Then DatasourceList.Append(",")
                        DatasourceList.Append(pickListId)
                        MultipleDatasources = True
                    End If

                End If
            Next

            If (DatasourceList.Length > 0) Then
                DataSources = DatasourceList.ToString
            End If

        End If

        Return DataSources

    End Function

    Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Init
        Me.InitializeComponent()
    End Sub

    'Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
    '    PO = CType(Session("LoggedInUser"), PPC.FDA.Person.PersonObject)
    '    Dim homePage As Boolean
    '    homePage = False

    '    'Only show the basic search on the home page
    '    If Request.ServerVariables("URL").ToLower().IndexOf("home.aspx") > 0 Then
    '        homePage = True
    '    End If

    '    If Not IsPostBack Then
    '        If (advanceSearch Or Session("AdvancedSearch") = "True") And Not homePage Then
    '            lblTitle.Text = "Advanced Search"
    '            PNLAdvancedDetails.Visible = True
    '        End If
    '        dgDatasources.DataSource = GetDetailedDatasources()
    '        dgDatasources.DataBind()
    '    End If
    'End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        PO = CType(Session("LoggedInUser"), PPC.FDA.Person.PersonObject)
        If PO Is Nothing Then
            Response.Redirect("default.aspx")
            'Server.Transfer("default.aspx")
        Else
            If Not IsPostBack Then
                'If (advanceSearch Or Session("AdvancedSearch") = "True") And Not homePage Then
                lblTitle.Text = "Drug Name Search"
                PNLAdvancedDetails.Visible = True
                'End If

                Dim DataSources As DataSet
                If Session("DATA_SOURCES") Is Nothing Then
                    DataSources = GetDetailedDatasources()
                Else
                    DataSources = CType(Session("DATA_SOURCES"), DataSet)
                End If
                'dgDatasources.DataSource = GetDetailedDatasources()
                dgDatasources.DataSource = DataSources
                dgDatasources.DataBind()
            End If
        End If
    End Sub

    Private Function GetDetailedDatasources() As DataSet

        Dim OraParams() As OracleParameter = New OracleParameter(2) {}
        Dim dsDatasources As DataSet

        OraParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
        OraParams(0).Value = PO.UserName

        OraParams(1) = New OracleParameter("listname_in", OracleDbType.Varchar2)
        OraParams(1).Value = "record_source"

        OraParams(2) = New OracleParameter("data_source_list", OracleDbType.RefCursor, ParameterDirection.Output)

        dsDatasources = FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "data_source_list_get", OraParams)

        'Dim addFactorsRow As System.Data.DataRow = dsDatasources.Tables(0).NewRow()

        'addFactorsRow("DESCRIPTION") = "Additional Factors"
        'addFactorsRow("PICK_LIST_ITEM_ID") = -1

        'dsDatasources.Tables(0).Rows.InsertAt(addFactorsRow, 0)

        'Limit Datasoucres for Public version - Drugs At FDA, RxNorm and Safety Evaluator
        If Session("AppMode").ToString().Contains("PublicMode") Then
            Dim dsPublicDatasources As DataSet = dsDatasources.Clone
            Dim dsRow As DataRow
            For Each dsRow In dsDatasources.Tables(0).Rows
                If (dsRow(7).ToString = "2458" Or dsRow(7).ToString = "2098" Or dsRow(7).ToString = "581") Then
                    dsPublicDatasources.Tables(0).ImportRow(dsRow)
                End If
            Next
            dsDatasources = dsPublicDatasources.Copy
        End If

        Session("DATA_SOURCES") = dsDatasources
        Return dsDatasources

    End Function




    Private Function ValidateValue(ByVal ValidateString As String) As String
        If ValidateString = "" Then
            Return Nothing
        Else
            Return ValidateString
        End If
    End Function



    Private Sub AddSearchToArchive()

        Dim Counter, RetVal As Integer
        Dim SearchSourceList As String = ""
        Dim KeyValue As DictionaryEntry
        Dim searchSource As String

        Dim OraParams() As OracleParameter = New OracleParameter(3) {}

        If chkOrthographic.Checked Then
            SearchSourceList = "902"
        End If

        If chkPhonetic.Checked Then
            If SearchSourceList.Length = 0 Then
                SearchSourceList = "903"
            Else
                SearchSourceList += ",903"
            End If
        End If

        If chkText.Checked Then
            If SearchSourceList.Length = 0 Then
                SearchSourceList = "904"
            Else
                SearchSourceList += ",904"
            End If
        End If

        Try
            OraParams(0) = New OracleParameter("search_term_in", OracleDbType.Varchar2)
            OraParams(0).Value = SearchTerm

            OraParams(1) = New OracleParameter("userid_in", OracleDbType.Varchar2)
            OraParams(1).Value = PO.UserID

            OraParams(2) = New OracleParameter("search_type_pick", OracleDbType.Varchar2)
            OraParams(2).Value = ValidateValue(SearchSourceList)

            OraParams(3) = New OracleParameter("data_source_pick", OracleDbType.Varchar2)
            OraParams(3).Value = ValidateValue(GetSelectedDataSources())

            RetVal = FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "archive_search_add", OraParams)

            If Not RetVal <> 0 Then
                Trace.Write("Search not saved to the archive : " + SearchTerm + ControlChars.NewLine)
                Trace.Write(ControlChars.Tab + GetSelectedDataSources() + ControlChars.NewLine)
                Trace.Write(ControlChars.Tab + SearchSourceList + ControlChars.NewLine)
            End If

        Catch oe As OracleException
            Trace.Write("Search not saved to the archive : " + SearchTerm + ControlChars.NewLine)
            Trace.Write("Error Message: " + oe.Message + ControlChars.NewLine)
            Trace.Write(ControlChars.Tab + GetSelectedDataSources() + ControlChars.NewLine)
            Trace.Write(ControlChars.Tab + SearchSourceList + ControlChars.NewLine)
        End Try

    End Sub

    Private Sub SearchReset_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles SearchReset.Click

        Try
            SearchText.Text = ""
            chkOrthographic.Checked = True
            chkPhonetic.Checked = True
            'chkSpelling.Checked = False
            chkText.Checked = False

            lblValidate.Text = ""
            lblValidate.Visible = False

            Dim dgItem As DataGridItem
            For Each dgItem In dgDatasources.Items
                Dim chkBxSearch As CheckBox = CType(dgItem.FindControl("cbSearch"), CheckBox)
                Dim description As String = dgItem.Cells(1).Text
                If (description = "2098" Or description = "2458" Or description = "581") Then
                    chkBxSearch.Checked = True
                Else
                    chkBxSearch.Checked = False
                End If
            Next

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub SearchSubmit_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles SearchSubmit.Click

        Try
            'Validate if all search fields present
            If SearchText.Text.Trim.Length = 0 Then
                lblValidate.Text = "Please enter a value in the Search Text box."
                lblValidate.Visible = True
                'ElseIf chkOrthographic.Checked = False And chkPhonetic.Checked = False And chkText.Checked = False And chkSpelling.Checked = False Then
            ElseIf chkOrthographic.Checked = False And chkPhonetic.Checked = False And chkText.Checked = False Then
                lblValidate.Text = "Please check at least one Search Type."
                lblValidate.Visible = True
            ElseIf GetSelectedDataSources().Equals("") Then
                lblValidate.Text = "Please check at least one Data Source."
                lblValidate.Visible = True
            Else
                lblValidate.Text = ""
                lblValidate.Visible = False
            End If

            'If Spelling is checked, then no other search can be done - At this time
            'If chkSpelling.Checked Then
            '    If chkOrthographic.Checked = True Or chkPhonetic.Checked = True Or chkText.Checked = True Then
            '        lblValidate.Text = "Please select only Spelling Search Type."
            '        lblValidate.Visible = True
            '    End If
            'End If

            If lblValidate.Visible = True Then
                Return
            End If

            ' Setup the defaults
            Session.Item("FinishedSearch") = False
            Session.Item("Results") = ""
            Session.Item("Engine") = Nothing

            ' Create the thread that the search will sit on.
            Dim thread1 As New Thread(New ThreadStart(AddressOf GenerateSearchEngine))
            thread1.Priority = ThreadPriority.Lowest
            thread1.Start()

            AddSearchToArchive()

            ' Redirect to the search page.
            Response.Redirect("search.aspx")
        Catch ThreadExiting As ThreadAbortException
            ' The thread has exited, which probably means that something has gone wrong. 
            Trace.Write("Thread Abortion Exception: " + ThreadExiting.Message)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub Check_Clicked(ByVal sender As Object, ByVal e As EventArgs)
        Dim ck1 As CheckBox = CType(sender, CheckBox)
        Dim dgItem As DataGridItem = CType(ck1.NamingContainer, DataGridItem)
        'now we've got what we need!
    End Sub

    Private Sub dgDataSourcesItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles dgDatasources.ItemDataBound
        If (e.Item.ItemType = ListItemType.Item Or
            e.Item.ItemType = ListItemType.AlternatingItem) Then

            Dim dgItem As DataGridItem = CType(e.Item, DataGridItem)

            Dim description As String = dgItem.Cells(1).Text
            If (description = "2098" Or description = "2458" Or description = "581") Then
                Dim cbDatasources As CheckBox = CType(dgItem.FindControl("cbSearch"), CheckBox)
                cbDatasources.Checked = True
            End If

        End If
    End Sub

    ' Properties
    Public Property AdvancedSearch() As Boolean
        Get
            Return Me.advanceSearch
        End Get
        Set(ByVal Value As Boolean)
            Me.advanceSearch = Value
            PNLAdvancedDetails.Visible = Value
        End Set
    End Property

End Class