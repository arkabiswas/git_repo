Imports Oracle.DataAccess.Client

Partial Class DeleteRecordConfirm
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
        If Not IsPostBack Then
            DeterminePageView()
        End If
    End Sub

    Private Sub DeterminePageView()

        Try
            ' Make sure we have all the drug details we need.
            PPC.FDA.Data.OracleDataFactory.GetDrugnameAndDatasource(Request.QueryString("id"), drugNameToDisplay, drugDataSourceToDisplay)
            ' This will generate the appropriate page text.
            IsOnWatchlist(Request.QueryString("id"))

        Catch ex As Exception
            confirmNoButton.Visible = False
            confirmYesButton.Visible = False
            errorMessage.Text = ex.Message
            errorMessage.Visible = True
        End Try


    End Sub

#Region " IsOnWatchlist "
    Private Function IsOnWatchlist(ByVal ProductUid As String) As Boolean

        'Try
        '    Dim datasourceInfoConnection As New Oracle.DataAccess.Client.OracleConnection(ConfigurationManager.AppSettings("ConnectionString"))
        '    Dim datasourceInfoCommand As Oracle.DataAccess.Client.OracleCommand = datasourceInfoConnection.CreateCommand()
        '    Dim datasourceParameter As OracleParameter

        '    datasourceInfoCommand.CommandText = "product_on_watchlist"
        '    datasourceInfoCommand.CommandType = CommandType.StoredProcedure

        '    datasourceParameter = New OracleParameter("product_name_uid_in", OracleDbType.Varchar2, ParameterDirection.Input)
        '    datasourceParameter.Value = ProductUid
        '    datasourceInfoCommand.Parameters.Add(datasourceParameter)

        '    datasourceParameter = New OracleParameter("watchlists", OracleDbType.RefCursor, ParameterDirection.Output)
        '    datasourceInfoCommand.Parameters.Add(datasourceParameter)

        '    Dim datasourceDataset As New DataSet
        '    Dim datasourceDataAdapter As New OracleDataAdapter(datasourceInfoCommand)
        '    datasourceDataAdapter.Fill(datasourceDataset)

        '    If datasourceDataset.Tables(0).Rows.Count > 0 Then
        '        listOfWatchlists.DataSource = datasourceDataset
        '        listOfWatchlists.DataBind()
        '        listOfWatchlists.Visible = True
        '        datasourceWarning.Text = String.Format("The Product name '<em>{0}</em>' from the datasource '<em>{1}</em>' is currently on a watchlist. Are you sure you would like to delete this product record?", drugNameToDisplay, drugDataSourceToDisplay)
        '    Else
        '        datasourceWarning.Text = String.Format("Are you sure that you want to delete the product name '<em>{0}</em>' from the datasource '<em>{1}</em>'?", drugNameToDisplay, drugDataSourceToDisplay)
        '    End If

        'Catch datasourceInfoException As OracleException
        '    Throw New DataException("There was an error retrieving the watchlists: " + PPC.FDA.Data.OracleDataFactory.CleanOracleError(datasourceInfoException.Message))
        'Catch ex As Exception
        '    Throw New ApplicationException("There was an error retrieving the watchlists: " + ex.Message)
        'End Try

        datasourceWarning.Text = String.Format("Are you sure that you want to delete the product name '<em>{0}</em>' from the datasource '<em>{1}</em>'?", drugNameToDisplay, drugDataSourceToDisplay)

    End Function
#End Region

    Public Sub confirmYesButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Server.Transfer("DeleteRecordFinish.aspx?id=" + Request.QueryString("id"))
    End Sub

    Public Sub confirmNoButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("settings.aspx")
    End Sub

End Class
