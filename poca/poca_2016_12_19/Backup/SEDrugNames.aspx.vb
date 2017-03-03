Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Partial Class SEDrugNames
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

        PO = Session("LoggedInUser")

        Dim ods As DataSet
        Dim arrOParams() As OracleParameter = New OracleParameter(2) {}
        Dim boolValidate As Boolean = False
        Dim seletedRange As Integer = ddlDateRange.SelectedValue()
        Dim startDate, curDate As Date
        curDate = Date.Now.AddDays(1).ToString()

        Select Case seletedRange
            Case 1
                startDate = curDate.AddDays(-7)
            Case 2
                startDate = curDate.AddDays(-14)
            Case 3
                startDate = curDate.AddMonths(-1)
            Case 4
                startDate = curDate.AddYears(-1)
            Case 5
                startDate = curDate.AddYears(-30)
        End Select

        arrOParams(0) = New OracleParameter("begin_date", OracleDbType.Date)
        arrOParams(0).Value = startDate.ToString("dd-MMM-yyyy")

        arrOParams(1) = New OracleParameter("end_date", OracleDbType.Date)
        arrOParams(1).Value = curDate.ToString("dd-MMM-yyyy")

        arrOParams(2) = New OracleParameter("safety_eval_drugs_ref_cur", OracleDbType.RefCursor, ParameterDirection.Output)
        arrOParams(2).Value = Nothing

        Try
            ods = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "se_names_entered_get", arrOParams)
            Session("FdaGridSource") = ods.Tables(0)
            FDAGrid1.DataSource = ods
            FDAGrid1.DataBind()
        Catch ex As Exception

        End Try

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

    Protected Sub ddlDateRange_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlDateRange.SelectedIndexChanged

    End Sub
End Class
