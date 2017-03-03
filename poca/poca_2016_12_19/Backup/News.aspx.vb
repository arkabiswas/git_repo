Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Partial Class News
    Inherits System.Web.UI.Page

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
        GetNewsItem()
    End Sub

#Region "GetNewsItem"
    ' GetNewsItem will retrieve the selected news item and then populate the
    ' input boxes with the appropriate values. 
    Private Sub GetNewsItem()

        Dim NewsDataSet As DataSet
        Dim DR As DataRow

        Try
            NewsDataSet = GetAllNews()

            For Each DR In NewsDataSet.Tables(0).Select("NEWS_ID = " + Request.QueryString("newsid"))
                Header.Text = DR("headline")
                Teaser.Text = DR("teaser")
                NewsItem.Text = DR("news_text")
            Next

        Catch ex As Exception
            NewsItem.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "GetAllNews"
    ' Get all News items from the database, this will be either used to bind
    ' to a dropdownlist or looped through to retrieve a single record. 
    Private Function GetAllNews() As DataSet

        Dim ods As DataSet
        Dim OraParams() As OracleParameter = New OracleParameter(0) {}

        Try

            OraParams(0) = New OracleParameter("the_news", OracleDbType.RefCursor, ParameterDirection.Output)
            OraParams(0).Value = Nothing

            ods = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "NEWS_GET", OraParams)

            Return ods

        Catch oe As OracleException

            NewsItem.Text = "There was an error creating the list of current news. " + oe.Message
            NewsItem.Visible = True

        End Try

    End Function
#End Region

End Class
