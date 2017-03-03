
Imports Oracle.DataAccess.Client

Partial Class DatasourceDeleteWarning
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
        'Put user code to initialize the page here
    End Sub

    Private Sub btnYes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYes.Click

        ' Delete datasource.
        Dim PO As PPC.FDA.Person.PersonObject = Session("LoggedInUser")
        Dim appVars As Hashtable = Session("appVars")

        If (appVars("deletePickListId").ToString() <> "" And appVars("deletePickListItemId").ToString() <> "") Then

            Dim pickListId As String = appVars("deletePickListId").ToString()
            Dim pickListItemId As String = appVars("deletePickListItemId").ToString()

            Dim OraConnection As New OracleConnection(ConfigurationManager.AppSettings.Item("ConnectionString"))

            Dim OraParams() As OracleParameter = New OracleParameter(2) {}


            OraParams(0) = New OracleParameter("i_pick_list_id_in", OracleDbType.Double)
            OraParams(0).Value = pickListId
            OraParams(1) = New OracleParameter("i_pick_list_item_id_in", OracleDbType.Double)
            OraParams(1).Value = pickListItemId
            OraParams(2) = New OracleParameter("username_in", OracleDbType.Varchar2)
            OraParams(2).Value = PO.UserName

            Dim rowsAffected As Integer = FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "data_access_ds_delete", OraParams)

            OraConnection.Close()

        End If
        Server.Transfer("ModifyDatasource.aspx")
    End Sub

    Private Sub btnNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNo.Click
        ' Do nothing.
        Server.Transfer("ModifyDatasource.aspx")
    End Sub
End Class
