Imports Oracle.DataAccess.Client
Partial Class NewsActionPage
    Inherits System.Web.UI.Page
    Protected PO As PPC.FDA.Person.PersonObject


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

        If Not Page.IsPostBack Then
            LoadNews()
            LoadDropDownLists()
        End If

    End Sub

#Region "LoadDropDownLists"
    ' LoadDropDownLists load up the dropdownlists for selecting values from.
    Private Sub LoadDropDownLists()

        DDLCategory.DataSource = DDLDataBind("News_Category")
        DDLCategory.DataTextField = "Description"
        DDLCategory.DataValueField = "List_Item"
        DDLCategory.DataBind()

        DDLPriority.DataSource = DDLDataBind("News_Priority")
        DDLPriority.DataTextField = "Description"
        DDLPriority.DataValueField = "List_Item"
        DDLPriority.DataBind()

    End Sub
#End Region

#Region "DDLDataBind"
    Private Function DDLDataBind(ByVal ListName As String) As DataView
        Dim OraParams() As OracleParameter = New OracleParameter(2) {}
        Dim ods As DataSet
        Try

            OraParams(0) = New OracleParameter("USERNAME_IN", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("listname_in", OracleDbType.Varchar2)
            OraParams(1).Value = ListName

            OraParams(2) = New OracleParameter("pick_list", OracleDbType.RefCursor, ParameterDirection.Output)
            OraParams(2).Value = Nothing

            ods = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "pick_list_get", OraParams)
            Return New DataView(ods.Tables(0))

        Catch oe As OracleException
            CurrentNewsLabel.Text = "Error: " + oe.Message
            CurrentNewsLabel.Visible = True

        Catch ex As Exception
            CurrentNewsLabel.Text = "Error: " + ex.Message
            CurrentNewsLabel.Visible = True

        End Try
    End Function
#End Region

#Region "LoadNews"
    ' LoadNews retrieves the data to be binded to the dropdownlist listing
    ' all the news items.
    Private Sub LoadNews()

        Dim ods As DataSet
        Dim OraParams() As OracleParameter = New OracleParameter(0) {}

        Try

            DDLNewsItems.DataSource = GetAllNews()
            DDLNewsItems.DataTextField = "Headline"
            DDLNewsItems.DataValueField = "news_id"
            DDLNewsItems.DataBind()

        Catch oe As OracleException

            CurrentNewsLabel.Text = "There was an error creating the list of current news. " + oe.Message
            CurrentNewsLabel.Visible = True

        End Try

    End Sub
#End Region

#Region "GetNewsItem"
    ' GetNewsItem will retrieve the selected news item and then populate the
    ' input boxes with the appropriate values. 
    Private Sub GetNewsItem()

        Dim NewsDataSet As DataSet
        Dim DR As DataRow

        Try
            NewsDataSet = GetAllNews()
            LoadDropDownLists()

            For Each DR In NewsDataSet.Tables(0).Select("NEWS_ID = " + DDLNewsItems.SelectedItem.Value)
                TXTNewsid.Text = DDLNewsItems.SelectedItem.Value
                TxtHeadline.Text = DR("headline")
                TxtTeaser.Text = DR("teaser")
                TXTExpire.Text = DR("EXP_DATE").Format("MM-dd-yyyy")
                DDLCategory.Items.FindByText(DR("category")).Selected = True
                DDLPriority.Items.FindByText(DR("priority")).Selected = True
                TANews.Text = DR("news_text")
            Next

        Catch ex As Exception
            CurrentNewsLabel.Text = ex.Message
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

            CurrentNewsLabel.Text = "There was an error getting the list of current news. " + oe.Message
            CurrentNewsLabel.Visible = True

        End Try

    End Function
#End Region

#Region "AddUpdateNewsItem"
    ' AddUpdateNewsItem will determine if an item is being updated or added to the database. It then will add/update the data. 
    Private Sub AddUpdateNewsItem()

        Dim NewsId = DBNull.Value
        If TXTNewsid.Text <> "" Then
            NewsId = TXTNewsid.Text
        End If

        Dim OraParams() As OracleParameter = New OracleParameter(7) {}
        Dim RetVal As Integer

        Try

            OraParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("headline_in", OracleDbType.Varchar2)
            OraParams(1).Value = TxtHeadline.Text

            OraParams(2) = New OracleParameter("teaser_in", OracleDbType.Varchar2)
            OraParams(2).Value = TxtTeaser.Text

            OraParams(3) = New OracleParameter("news_in", OracleDbType.Clob, ParameterDirection.Input)
            OraParams(3).Value = TANews.Text

            OraParams(4) = New OracleParameter("category_in", OracleDbType.Varchar2)
            OraParams(4).Value = DDLCategory.SelectedItem.Value

            OraParams(5) = New OracleParameter("proirity_in", OracleDbType.Varchar2)
            OraParams(5).Value = DDLPriority.SelectedItem.Value

            OraParams(6) = New OracleParameter("expire_dt_in", OracleDbType.Date)
            OraParams(6).Value = CType(TXTExpire.Text, Date).ToString("dd-MMM-yyyy")

            OraParams(7) = New OracleParameter("news_id_in", OracleDbType.Decimal)
            OraParams(7).Value = NewsId

            RetVal = FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "news_modify", OraParams)

            If RetVal <> 0 Then
                CurrentNewsLabel.Text = "News Item Added/Saved."
                CurrentNewsLabel.Visible = True
                LoadNews()
            End If

        Catch ex As Exception
            CurrentNewsLabel.Text = ex.Message
            CurrentNewsLabel.Visible = True
        End Try

    End Sub
#End Region

    Private Sub BTNGetNews_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNGetNews.Click
        GetNewsItem()
    End Sub

    Private Sub BTNSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNSubmit.Click
        If Page.IsValid Then
            AddUpdateNewsItem()
        End If
    End Sub

    Private Sub BTNReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNReset.Click

        TXTNewsid.Text = ""
        TxtHeadline.Text = ""
        TxtTeaser.Text = ""
        TXTExpire.Text = ""
        DDLCategory.SelectedIndex = 0
        DDLPriority.SelectedIndex = 0
        TANews.Text = ""
        CurrentNewsLabel.Text = ""

    End Sub
End Class
