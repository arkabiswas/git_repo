Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Partial Class AssignConsult
    Inherits System.Web.UI.Page
    Protected PO As FDA.Person.PersonObject

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
        If Not IsPostBack Then
            LoadConsultGrid()
        End If

    End Sub

#Region "LoadConsultGrid"
    ' LoadConsultGrid creates a connection to the database and then retrieves
    ' a dataset back and binds it to the grid.
    '
    Private Sub LoadConsultGrid()

        Dim ods As DataSet
        Dim OraParams() As OracleParameter = New OracleParameter(1) {}

        Try
            OraParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("cur_results", OracleDbType.RefCursor, ParameterDirection.Output)
            OraParams(1).Value = Nothing

            ods = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "CONSULT_GET_unassigned", OraParams)
            FDAGrid1.DataSource = ods
            FDAGrid1.DataBind()

        Catch oe As OracleException

        End Try


    End Sub
#End Region

    ' ItemDataBound fires off everytime a dataitem is bound to the grid.
    ' Here we take the values and genereate a hyperlink control to send back.
    '
    'Private Sub FDAGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles FDAGrid1.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

    '        Dim RowCells As TableCellCollection = e.Item.Cells
    '        Dim HyperLinkCtl As HyperLink = New HyperLink()

    '        HyperLinkCtl.NavigateUrl = String.Format("AssignConsultPerson.aspx?cid={0}&clt={1}", RowCells(1).Text, Server.UrlEncode(RowCells(0).Text))
    '        HyperLinkCtl.Text = RowCells(0).Text

    '        RowCells(0).Controls.Add(HyperLinkCtl)

    '    End If
    'End Sub

End Class
