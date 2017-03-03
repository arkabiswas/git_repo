Imports Oracle.DataAccess.Client

Partial  Class SimpleSearchControl
    Inherits System.Web.UI.UserControl
    Protected WithEvents CHKMedical As System.Web.UI.WebControls.CheckBox
    Protected WithEvents SearchPickList As System.Web.UI.WebControls.CheckBoxList
    Protected PO As FDA.Person.PersonObject
    Private m_SearchTitle As String
    Protected WithEvents ProposedNameLanguage As System.Web.UI.WebControls.DropDownList
    Protected WithEvents DatabaseNameLanguage As System.Web.UI.WebControls.DropDownList
    Private m_Advanced As Boolean

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
        'Put user code to initialize the page here
        PO = Session("LoggedInUser")
        If Not IsPostBack Then
            m_SearchTitle = "Quick Search"
            If m_Advanced Then
                BindDatasources()
            End If
            lblValidate.Visible = False
        End If
    End Sub

    Public Property TitleName() As String
        Get
            Return lblTitle.Text
        End Get
        Set(ByVal Value As String)
            m_SearchTitle = Value
            lblTitle.Text = m_SearchTitle
        End Set
    End Property

    Public Property AdvancedControl() As Boolean
        Get
            Return m_Advanced
        End Get
        Set(ByVal Value As Boolean)
            SetAdvancedOptions(Value)
        End Set
    End Property

    Private Sub SetAdvancedOptions(ByVal ISAdvanced As Boolean)

        If ISAdvanced Then
            Me.TitleName = "Advanced Search"
            PNLAdvancedDetails.Visible = True
            m_Advanced = True
            If Not OtherDatasources.Items.Count > 0 Then
                BindDatasources()
            End If
        Else
            Me.TitleName = "Quick Search"
            PNLAdvancedDetails.Visible = False
            m_Advanced = False
        End If

    End Sub

    Private Sub ImageButton1_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click

        If Page.IsValid() Then
            Dim SearchSources As Hashtable = New Hashtable()
            Context.Items.Add("SearchTerm", SearchText.Text)
            Session("SearchTerm") = SearchText.Text

            If chkOrthographic.Checked Then
                Context.Items.Add("OrthoSearch", "True")
                SearchSources.Add("orthographic", "902")
                Session("OrthoSearch") = True
            End If

            If chkPhonetic.Checked Then
                Context.Items.Add("PhoneSearch", "True")
                SearchSources.Add("phonetic", "903")
                Session("PhoneSearch") = True
            End If

            If chkText.Checked Then
                Context.Items.Add("TextSearch", "True")
                SearchSources.Add("text", "904")
                Session("TextSearch") = True
            End If

            ' Determine the selected datasources...
            Dim li As ListItem
            Dim DataSources As Hashtable = New Hashtable()

            For Each li In OtherDatasources.Items
                If li.Selected Then
                    DataSources.Add(li.Text, li.Value)
                End If
            Next

            Session("SearchSources") = SearchSources
            Session("DataSources") = DataSources

            Server.Transfer("NewSearchResults.aspx")

        End If

    End Sub

    Private Sub BindDatasources()

        OtherDatasources.DataSource = DDLDataBind("record_source")
        OtherDatasources.DataTextField = "description"
        OtherDatasources.DataValueField = "pick_list_item_id"
        OtherDatasources.DataBind()
        OtherDatasources.Items.Insert(0, New ListItem("Additional Factors", "addl_fax"))

    End Sub

#Region "DDLDataBind"

    Private Function DDLDataBind(ByVal ListName As String) As DataView
        Dim OraParams() As OracleParameter = New OracleParameter(2) {}
        Dim ods As DataSet
        Try

            OraParams(0) = New OracleParameter("USERNAME_IN", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("listname_in", OracleDbType.Varchar2)
            OraParams(1).Value = ListName

            OraParams(2) = New OracleParameter("pick_list", OracleDbType.RefCursor, ParameterDirection.Output)
            OraParams(2).Value = Nothing

            ods = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "pick_list_get", OraParams)
            Return New DataView(ods.Tables(0))

        Catch oe As OracleException

        Catch ex As Exception

        End Try
    End Function

#End Region

    Public WriteOnly Property SearchTextField() As String
        Set(ByVal Value As String)
            SearchText.Text = Value
        End Set
    End Property

    Public WriteOnly Property OrthoChecked() As Boolean
        Set(ByVal Value As Boolean)
            chkOrthographic.Checked = Value
        End Set
    End Property

    Public WriteOnly Property PhoneChecked() As Boolean
        Set(ByVal Value As Boolean)
            chkPhonetic.Checked = Value
        End Set
    End Property

    Public WriteOnly Property TextChecked() As Boolean
        Set(ByVal Value As Boolean)
            chkText.Checked = Value
        End Set
    End Property


End Class
