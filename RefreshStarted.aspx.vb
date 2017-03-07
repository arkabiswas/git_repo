
Imports Oracle.DataAccess.Client


Partial Class RefreshStarted
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

            Dim appVars As Hashtable = Session("appVars")

            Dim fileName As String = appVars.Item("refreshFileName").ToString()
            Dim pickListId As Integer = CInt(appVars("refreshPickListId"))
            Dim pickListItemId As Integer = CInt(appVars("refreshPickListItemId"))
            Dim fileDate As DateTime = DateTime.Parse(appVars.Item("refreshDate"))
            Dim refreshDatasource As String = appVars.Item("refreshDatasource").ToString()
            Dim isRefresh As Boolean = appVars.Item("isRefresh")

            lblDatasource.Text = refreshDatasource
            lblDateOfSource.Text = fileDate.ToString("MM-dd-yyyy")

            Dim OraConnection As New OracleConnection(ConfigurationManager.AppSettings.Item("ConnectionString"))

            Dim OraCommand As New OracleCommand

            Dim OraParams() As OracleParameter = New OracleParameter(6) {}

            Try
                OraParams(0) = New OracleParameter("load_typ", OracleDbType.Varchar2)
                OraParams(0).Value = "UI"

                OraParams(1) = New OracleParameter("filename_in", OracleDbType.Varchar2)
                OraParams(1).Value = fileName

                OraParams(2) = New OracleParameter("i_pick_list_id_in", OracleDbType.Int32)
                OraParams(2).Value = pickListId

                OraParams(3) = New OracleParameter("record_source_id_in", OracleDbType.Int32)
                OraParams(3).Value = pickListItemId

                OraParams(4) = New OracleParameter("username_in", OracleDbType.Varchar2)
                OraParams(4).Value = PO.UserName

                OraParams(5) = New OracleParameter("file_date_in", OracleDbType.Date)
                OraParams(5).Value = ValidateDate(fileDate)

                OraParams(6) = New OracleParameter("flag", OracleDbType.Int32, ParameterDirection.Output)

                If isRefresh Then
                    FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "DATA_ACCESS_REFRESH_EXEC", OraParams)
                Else
                    FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "data_access_load_exec", OraParams)
                End If

                UpdateDatasources()
            Catch
                Throw
            End Try

        End If

    End Sub



    Private Function ValidateDate(ByVal Value As String) As String

        Dim time1 As Date

        If (String.Compare(Value, "", 0) = 0) Then
            Return Nothing
        End If

        time1 = CType(Value, Date)
        Return time1.ToString("dd-MMM-yyyy")

    End Function


    Private Function UpdateDatasources()

        Dim OraParams() As OracleParameter = New OracleParameter(2) {}
        Dim dsDatasources As DataSet

        OraParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
        OraParams(0).Value = PO.UserName

        OraParams(1) = New OracleParameter("listname_in", OracleDbType.Varchar2)
        OraParams(1).Value = "record_source"

        OraParams(2) = New OracleParameter("data_source_list", OracleDbType.RefCursor, ParameterDirection.Output)

        dsDatasources = FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "data_source_list_get", OraParams)

        'Limit Datasoucres for Public version - Drugs At FDA, RxNorm and Safety Evaluator
        If Session("AppMode").ToString().Contains("PublicMode") Then
            Dim dsPublicDatasources As DataSet = dsDatasources.Clone
            Dim dsRow As DataRow
            For Each dsRow In dsDatasources.Tables(0).Rows
                If (dsRow(7).ToString = "2458" Or dsRow(7).ToString = "2098" Or dsRow(7).ToString = "581") Then
                    dsPublicDatasources.Tables(0).ImportRow(dsRow)
                End If
            Next
            dsDatasources = dsPublicDatasources.Copy
        End If

        Session("DATA_SOURCES") = dsDatasources

    End Function


End Class
