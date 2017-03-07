Imports Oracle.DataAccess.Client
Partial  Class NewsControl
    Inherits System.Web.UI.UserControl

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
        LoadNews()
    End Sub

#Region "LoadNews"
    Private Sub LoadNews()

        Dim ods As DataSet
        Dim OraParams() As OracleParameter = New OracleParameter(0) {}

        Try

            OraParams(0) = New OracleParameter("the_news", OracleDbType.RefCursor, ParameterDirection.Output)
            OraParams(0).Value = Nothing

            ods = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "NEWS_GET", OraParams)

            DataList1.DataSource = ods

            DataList1.DataBind()

        Catch oe As OracleException
            DataList1.Visible = False
            NewsError.Text = "There was an error creating the list of current news. " + oe.Message
            NewsError.Visible = True

        End Try

    End Sub
#End Region

End Class
