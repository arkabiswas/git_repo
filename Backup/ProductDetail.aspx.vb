Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Partial Class ProductDetail
    Inherits System.Web.UI.Page
    'Protected WithEvents DataList1 As System.Web.UI.WebControls.DataList
    Protected PO As FDA.Person.PersonObject
    Dim myView As DataView

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
            SetupProductDetail()
            BindGrid()
        End If
    End Sub

    Private Sub BindGrid()
        BindGrid("defaultsort")
    End Sub

    Private Sub BindGrid(ByVal SortRow As String)

        Dim OraParams() As OracleParameter = New OracleParameter(2) {}
        Dim ods As DataSet

        Try
            OraParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("product_name_in", OracleDbType.Varchar2)
            OraParams(1).Value = DecodedQueryValue()

            OraParams(2) = New OracleParameter("the_list", OracleDbType.RefCursor, ParameterDirection.Output)
            OraParams(2).Value = Nothing

            ods = FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "static_detail_get", OraParams)

            myView = ods.Tables(0).DefaultView

            If SortRow <> "defaultsort" Then
                If Cache.Get("SortExpression") = SortRow Then
                    myView.Sort = SortRow & " DESC"
                Else
                    myView.Sort = SortRow
                End If

                Cache("SortExpression") = myView.Sort
            End If

            grdDrugDetails.DataSource = myView
            grdDrugDetails.DataBind()

            If ods.Tables(0).DefaultView.Count = 0 Then
                ErrorLabel.Visible = True
            End If

        Catch oe As OracleException

        End Try

    End Sub

    Private Sub SetupProductDetail()
        ProductName.Text = "Product Details for " & DecodedQueryValue()
    End Sub

    Private Function DecodedQueryValue() As String
        Dim TempUrl As String = HttpUtility.UrlDecode(Request.RawUrl())
        Dim i As Integer = TempUrl.IndexOf("prdname=") + 8
        TempUrl = TempUrl.Substring(i, TempUrl.Length - i)
        Return TempUrl
    End Function

    Sub SortEventHandler(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        BindGrid(e.SortExpression.ToString())
    End Sub

    'Private Sub DataList1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles DataList1.ItemDataBound

    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '        Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
    '        Dim ElementDetail As Label = CType(e.Item.FindControl("ElementDetail"), Label)
    '        Dim ElementDatasource As Label = CType(e.Item.FindControl("ElementDatasource"), Label)

    '        If drv.Item("pick_list_value").ToString() = String.Empty Then
    '            ElementDetail.Text = drv.Item("text_value").ToString()
    '        Else
    '            ElementDetail.Text = drv.Item("pick_list_value").ToString()
    '        End If

    '        If Not drv.Item("data_source").ToString() = String.Empty Then
    '            ElementDatasource.Text = "(" & PPC.FDA.Data.OracleDataFactory.GetDataSourceName(drv.Item("data_source").ToString()).Trim() & ")"
    '        End If
    '    End If

    'End Sub
End Class
