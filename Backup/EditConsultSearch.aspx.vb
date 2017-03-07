Imports Oracle.DataAccess.Client
Imports PPC.FDA.Controls
Partial Class EditConsultSearch
    Inherits System.Web.UI.Page
    Protected PO As PPC.FDA.Person.PersonObject

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
            LoadConsultGrid()
        End If

    End Sub

#Region "LoadConsultGrid"
    Private Sub LoadConsultGrid()

        Dim ods As DataSet
        Dim OraParams() As OracleParameter = New OracleParameter(2) {}

        Try
            OraParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("FILTER_IN", OracleDbType.Varchar2)
            OraParams(1).Value = "all"

            OraParams(2) = New OracleParameter("cur_results", OracleDbType.RefCursor, ParameterDirection.Output)
            OraParams(2).Value = Nothing

            ods = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "CONSULT_GET_OPEN", OraParams)
            FDAGrid1.DataSource = ods
            FDAGrid1.DataBind()

        Catch oe As OracleException

        End Try


    End Sub
#End Region


    Private Sub FDAGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles FDAGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim RowCells As TableCellCollection = e.Item.Cells
            Dim HyperLinkCtl As HyperLink = New HyperLink()

            HyperLinkCtl.NavigateUrl = String.Format("EditConsult.aspx?cid={0}&clt={1}", RowCells(1).Text, Server.UrlEncode(RowCells(0).Text))
            HyperLinkCtl.Text = RowCells(0).Text

            RowCells(0).Controls.Add(HyperLinkCtl)

        End If
    End Sub
End Class
