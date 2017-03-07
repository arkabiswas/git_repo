Imports Oracle.DataAccess.Client

Partial Class DeleteRecordFinish
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

    Public drugNameToDisplay As String
    Public drugDataSourceToDisplay As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GeneratePage(Request.QueryString("id"))
        ShowReturnString()
    End Sub

    Private Sub GeneratePage(ByVal ProductUid As String)
        Try
            ' Make sure we have the drug details we need.
            PPC.FDA.Data.OracleDataFactory.GetDrugnameAndDatasource(ProductUid, drugNameToDisplay, drugDataSourceToDisplay)
            ' Actually delete this record.
            DeleteRecord(ProductUid)

            'Decrease <Product Count> and Update <Date Updated> in Session("DATA_SOURCES") for Safety Evaluator datasource
            Dim DataSources As DataSet
            DataSources = CType(Session("DATA_SOURCES"), DataSet)
            For Each Row As DataRow In DataSources.Tables(0).Rows
                Dim dsName As String = Row("DESCRIPTION").ToString()
                If dsName.ToUpper().Contains("SAFETY") Then
                    Dim countStr As String = Row("DATA_SOURCE_RECORDS").ToString()
                    Dim count As Int32 = Convert.ToInt32(countStr) - 1
                    Row("DATA_SOURCE_RECORDS") = count.ToString()
                    Row("DATA_SOURCE_UPDATED") = Date.Today.ToString("MM-dd-yyyy")
                    Exit For
                End If
            Next
            Session("DATA_SOURCES") = DataSources

        Catch ex As Exception
            Dim datasourceWarningDiv As HtmlGenericControl = Me.FindControl("datasourceWarning")
            datasourceWarningDiv.Visible = False
            errorMessage.Text = ex.Message
            errorMessage.Visible = True
        End Try
    End Sub

    Private Sub DeleteRecord(ByVal ProductUid As String)
        Dim datasourceInfoConnection As New Oracle.DataAccess.Client.OracleConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim datasourceInfoCommand As Oracle.DataAccess.Client.OracleCommand = datasourceInfoConnection.CreateCommand()
        Dim datasourceParameter As OracleParameter
        Dim CurrentUser As FDA.Person.PersonObject = Session("LoggedInUser")

        Try
            datasourceInfoCommand.CommandText = "product_name_delete"
            datasourceInfoCommand.CommandType = CommandType.StoredProcedure

            datasourceParameter = New OracleParameter("product_name_uid_in", OracleDbType.Varchar2, ParameterDirection.Input)
            datasourceParameter.Value = ProductUid
            datasourceInfoCommand.Parameters.Add(datasourceParameter)

            datasourceParameter = New OracleParameter("username_in", OracleDbType.Varchar2, ParameterDirection.Input)
            datasourceParameter.Value = CurrentUser.UserName
            datasourceInfoCommand.Parameters.Add(datasourceParameter)

            datasourceParameter = New OracleParameter("result", OracleDbType.Varchar2, 10)
            datasourceParameter.Direction = ParameterDirection.Output
            datasourceInfoCommand.Parameters.Add(datasourceParameter)

            datasourceInfoConnection.Open()
            datasourceInfoCommand.ExecuteNonQuery()
            datasourceInfoConnection.Close()

        Catch DeleteRecordException As OracleException
            Throw New DataException("There was an error deleting the record: " + PPC.FDA.Data.OracleDataFactory.CleanOracleError(DeleteRecordException.Message))
        Catch OtherException As Exception
            Throw New ApplicationException("There was an error deleting the record: " + OtherException.Message)
        End Try
    End Sub

    Public Sub ShowReturnString()
        If Not Session("SearchQuery") = Nothing Then
            moveBack.Text = "<a href=""deleterecord.aspx"">Return to search Results for '" & Session("SearchQuery").ToString() & "'</a>"
            Session("PreLoad") = True
        End If
    End Sub

End Class
