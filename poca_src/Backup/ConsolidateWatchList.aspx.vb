Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Partial Class ConsolidateWatchList
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
        'Put user code to initialize the page here
        PO = Session("LoggedInUser")
        If Not IsPostBack Then
            LoadConsultGrid()
        End If

        If Request("clt") <> "" Then
            ConsolidateWatchList(Request("clt"), Request("cid"))
        End If

    End Sub

    ' LoadConsultGrid calls the appropriate procedure and then takes the 
    ' non-assigned consults and binds them to the grid.
    '
    Private Sub LoadConsultGrid()

        Dim ods As DataSet
        Dim odv As DataView
        Dim OraParams() As OracleParameter = New OracleParameter(1) {}

        Try
            OraParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("cur_results", OracleDbType.RefCursor, ParameterDirection.Output)
            OraParams(1).Value = Nothing

            ods = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "CONSULT_GET_UNCONSOLIDATED", OraParams)
            'odv = New DataView(ods.Tables(0), "assigned_to = ' '", "", DataViewRowState.Unchanged)
            ' changed 3-9-2004 
            odv = New DataView(ods.Tables(0), "", "", DataViewRowState.Unchanged)
            ConsultGrid.DataSource = odv
            ConsultGrid.DataBind()

        Catch oe As OracleException
            ErrorLabel.Text = oe.Message
        End Try

    End Sub

    ' ConsolidateWatchList takes the selected consult name and id and
    ' sends them to the consolidate stored procedure.
    '
    Private Sub ConsolidateWatchList(ByVal ConsultName As String, ByVal ConsultID As String)

        Dim OraParams() As OracleParameter = New OracleParameter(2) {}
        Dim RetVal As Integer

        Try

            OraParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("consult_number_in", OracleDbType.Varchar2)
            OraParams(1).Value = ConsultID

            OraParams(2) = New OracleParameter("product_name_in", OracleDbType.Varchar2)
            OraParams(2).Value = ConsultName

            RetVal = FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "watch_list_consolidate", OraParams)
            If RetVal <> 0 Then
                Response.Redirect("settings.aspx")
            Else
                ErrorLabel.Text = "There was an error consolidating this watchlist. Please try again."
            End If

        Catch oe As OracleException
            ErrorLabel.Text = "There was an error consolidating this watchlist. Please try again.<br>" & oe.Message
        End Try

    End Sub

    ' ItemDataBound takes the datarow and creates a new HyperLink control to 
    ' continue on to the 2nd step.
    '
    Private Sub ConsultGrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles ConsultGrid.ItemDataBound
        'If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

        '    Dim RowCells As TableCellCollection = e.Item.Cells
        '    Dim HyperLinkCtl As HyperLink = New HyperLink()

        '    HyperLinkCtl.NavigateUrl = String.Format("ConsolidateWatchList.aspx?cid={0}&clt={1}", RowCells(1).Text, Server.UrlEncode(RowCells(0).Text))
        '    HyperLinkCtl.Text = RowCells(0).Text

        '    RowCells(0).Controls.Add(HyperLinkCtl)
        'End If
    End Sub

End Class
