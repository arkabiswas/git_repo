Imports System.Collections
Imports Oracle.DataAccess.Client

Partial Class SelectDatasource
    Inherits System.Web.UI.Page

    Protected PO As PPC.FDA.Person.PersonObject
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents LinkButton1 As System.Web.UI.WebControls.LinkButton

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

            If PO Is Nothing Then
                Server.Transfer("default.aspx")
            End If

            BindFileList()
        End If

    End Sub


    Private Sub BindFileList()

        Dim appVars As Hashtable = Session("appVars")

        Dim OraConnection As New OracleConnection(ConfigurationManager.AppSettings.Item("ConnectionString"))

        Dim OraParams() As OracleParameter = New OracleParameter(1) {}
        Dim reader As OracleDataReader

        Try

            OraParams(0) = New OracleParameter("directory_in", OracleDbType.Varchar2)
            OraParams(0).Value = ConfigurationManager.AppSettings.Item("FileUploadDirectory")

            OraParams(1) = New OracleParameter("the_file_list", OracleDbType.RefCursor, ParameterDirection.Output)
            OraParams(1).Value = Nothing

            reader = PPC.FDA.Data.OracleDataFactory.ExecuteReader(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "DATA_ACCESS_DIR_LIST", OraParams)

            rpFileList.DataSource = reader
            rpFileList.DataBind()

            reader.Close()

        Catch ex As Exception
            Throw
        Finally
            OraConnection.Close()
        End Try

    End Sub



    Private Sub rpFileList_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rpFileList.ItemCommand

        If e.CommandName = "loadFile" Then

            If Session("appVars") Is Nothing Then
                Dim appVars As Hashtable = New Hashtable
                appVars.Add("refreshFileName", e.CommandArgument)
                Session.Add("appVars", appVars)
            Else
                Dim appVars As Hashtable = Session("appVars")
                appVars.Item("refreshFileName") = e.CommandArgument
            End If

            CheckDataAccessStatus()
        End If

    End Sub


    Private Sub CheckDataAccessStatus()

        PO = Session("LoggedInUser")
        Dim appVars As Hashtable = Session("appVars")

        Dim OraConnection As New OracleConnection(ConfigurationManager.AppSettings.Item("ConnectionString"))

        Dim OraParams() As OracleParameter = New OracleParameter(3) {}
        Dim rowsAffected As Integer

        OraParams(0) = New OracleParameter("i_pick_list_id_in", OracleDbType.Int32)
        OraParams(0).Value = CInt(appVars("refreshPickListId"))

        OraParams(1) = New OracleParameter("record_source_id_in", OracleDbType.Int32)
        OraParams(1).Value = CInt(appVars("refreshPickListItemId"))

        OraParams(2) = New OracleParameter("username_in", OracleDbType.Varchar2)
        OraParams(2).Value = PO.UserName

        OraParams(3) = New OracleParameter("file_date_out", OracleDbType.Date, ParameterDirection.Output)

        rowsAffected = FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "DATA_ACCESS_STATUS", OraParams)

        If Not OraParams(3).Value Is DBNull.Value Then
            appVars.Item("refreshLastDate") = OraParams(3).Value
        End If

        Server.Transfer("DatasourceWarning.aspx")

    End Sub

End Class
