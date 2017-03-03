Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Partial Class CompletedConsults
    Inherits System.Web.UI.Page
    Protected PO As PPC.FDA.Person.PersonObject

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

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
        'Put user code to initialize the page here
        If Not IsPostBack Then
            PO = Session("LoggedInUser")

            Dim ods As DataSet
            Dim arrOParams() As OracleParameter = New OracleParameter(2) {}
            Dim dr As DataRow
            Dim boolValidate As Boolean = False

            arrOParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
            arrOParams(0).Value = PO.UserName

            arrOParams(1) = New OracleParameter("consult_type_in", OracleDbType.Varchar2)
            arrOParams(1).Value = "COMPLETE"

            arrOParams(2) = New OracleParameter("the_consults", OracleDbType.RefCursor, ParameterDirection.Output)
            arrOParams(2).Value = Nothing

            Try
                ods = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "consult_get_open", arrOParams)
                Session("FdaGridSource") = ods.Tables(0)
                FDAGrid1.DataSource = ods
                FDAGrid1.DataBind()
            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub FDAGrid1_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles FDAGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim RowCells As TableCellCollection = e.Item.Cells
            Dim HyperLinkCtl As HyperLink = New HyperLink

            HyperLinkCtl.NavigateUrl = String.Format("CompletedWatchlist.aspx?cid={0}&clt={1}", RowCells(1).Text, Server.UrlEncode(RowCells(0).Text))
            HyperLinkCtl.Text = RowCells(0).Text

            'Demo code
            'Dim HyperLinkCtl As HyperLink = New HyperLink
            'HyperLinkCtl.NavigateUrl = String.Format("CompletedWatchlist.aspx?cid={0}&clt={1}", RowCells(2).Text, Server.UrlEncode(RowCells(1).Text))
            'HyperLinkCtl.Text = "Watchlist"
            'Dim HyperLinkCtl2 As HyperLink = New HyperLink
            'HyperLinkCtl2..NavigateUrl = String.Format("editconsult.aspx?cid={0}&clt={1}", RowCells(2).Text, Server.UrlEncode(RowCells(1).Text))
            'HyperLinkCtl2.Text = "Edit"
            ''            Dim LblDelim As Literal
            ''           LblDelim.Text = "&nbsp;|&nbsp; "
            'RowCells(0).Controls.Add(HyperLinkCtl2)
            '' RowCells(0).Controls.Add(LblDelim)
            'RowCells(0).Controls.Add(HyperLinkCtl)
            'End demo code ...

            RowCells(0).Controls.Add(HyperLinkCtl)

        End If
    End Sub


    Private Sub FDAGrid1_SortCommand(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles FDAGrid1.SortCommand

        If ViewState("sortfield") = e.SortExpression Then
            If ViewState("sortdirection") = " ASC" Then
                ViewState("sortdirection") = " DESC"
            Else
                ViewState("sortdirection") = " ASC"
            End If
        Else
            ViewState("sortfield") = e.SortExpression
            ViewState("sortdirection") = " ASC"
        End If

        Dim dt As DataTable = CType(Session("FdaGridSource"), DataTable)
        Dim dv As DataView = New DataView(dt)

        dv.Sort = ViewState("sortfield") & ViewState("sortdirection")

        FDAGrid1.DataSource = dv
        FDAGrid1.DataBind()
    End Sub


    Private Sub FDAGrid1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FDAGrid1.SelectedIndexChanged

    End Sub
End Class
