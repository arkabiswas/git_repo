
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Partial Class DatasourceWarning
    Inherits System.Web.UI.Page

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
        'Oracle 10g is now returning a Date oject instead of null when the return date value is null

        Dim appVars As Hashtable = Session("appVars")
        Dim RefreshDate As OracleDate = appVars.Item("refreshLastDate")

        If RefreshDate.IsNull Then
            lblDateLoaded.Visible = False
        Else
            Dim refreshLastDate As String = RefreshDate.Value.ToString("MM-dd-yyyy")
            lblDateLoaded.Text = "You have selected a datasource that has recently been refreshed/loaded on " & _
                                   refreshLastDate & ". "
        End If
    End Sub

    Private Sub btnYes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYes.Click
        Server.Transfer("RefreshStarted.aspx")
    End Sub

    Private Sub btnNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNo.Click
        Server.Transfer("RefreshDatasources.aspx")
    End Sub

End Class
