Imports Oracle.DataAccess.Client
Imports PPC.FDA.Data

Partial Class DownloadWL
    Inherits System.Web.UI.Page
    Protected WithEvents DDMACAdd As System.Web.UI.WebControls.Button
    Protected WithEvents DDMacError As System.Web.UI.WebControls.Label
    Private PO As PPC.FDA.Person.PersonObject

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ID = "DownloadWL"

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' Set this to the Excel MIME Type so the browser will treat it like
        ' an Excel file.
        Response.ContentType = "application/vnd.ms-excel"
        Response.Charset = ""

        ' Set the DDMac comments field to non-visible as a default.
        Page.FindControl("WatchListComments").Visible = False

        ' Grab the current user.
        PO = Session("LoggedInUser")

        If Not Page.IsPostBack Then
            WatchListTitle.Text = "Watchlist for " + Request("clt")
            Dim SelectedWatchList As String
            If Request("wl") = "" Then SelectedWatchList = Nothing Else SelectedWatchList = Request("wl")
            LoadDataGrid(DownloadGrid, CreateDataSet(Request("cid"), Request("clt")), SelectedWatchList)
        End If

    End Sub


#Region "LoadDataGrid"

    ' LoadDataGrid will take the data that is sent to it and bind it to the appropriate dataset. 
    Private Sub LoadDataGrid(ByVal WLDataGrid As DataGrid, ByVal WLDataSet As DataSet, ByVal WatchListSelected As String)

        Dim WLDV As DataView

        If WatchListSelected <> Nothing Then
            Dim drows As DataRow()
            Dim dr As DataRow

            drows = WLDataSet.Tables(0).Select("watch_list_id = '" + WatchListSelected + "'")
            Dim clTable As DataTable = WLDataSet.Tables(0).Clone()
            For Each dr In drows
                clTable.ImportRow(dr)
            Next

            WLDV = New DataView(clTable)
            WLDataGrid.DataSource = WLDV
        Else
            WLDV = New DataView(WLDataSet.Tables(0))
            WLDataGrid.DataSource = WLDV
        End If

        WLDataGrid.DataKeyField = "watch_list_id"
        WLDataGrid.DataBind()

    End Sub

#End Region

#Region "CreateDataSet"

    ' CreateDataSet is the primary location on this page to create a dataset that will be
    ' used through out the lifetime of this page.
    Private Function CreateDataSet(ByVal ConsultID As String, ByVal ProductName As String) As DataSet

        Dim ods As DataSet
        Dim WLTableCount As Integer

        Dim arrOParams() As OracleParameter = New OracleParameter(3) {}
        Try
            arrOParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
            arrOParams(0).Value = PO.UserName

            arrOParams(1) = New OracleParameter("consult_number_in", OracleDbType.Varchar2)
            arrOParams(1).Value = ConsultID

            arrOParams(2) = New OracleParameter("product_name_in", OracleDbType.Varchar2)
            arrOParams(2).Value = ProductName

            arrOParams(3) = New OracleParameter("the_watch_list", OracleDbType.RefCursor, ParameterDirection.Output)
            arrOParams(3).Value = Nothing

            ods = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "watch_list_get", arrOParams)
            Dim odv As DataView = New DataView(ods.Tables(0))

            If odv.Count <= 0 Then
                ErrorLabel.Text = "<br> There are no consults on this watchlist. Please add a new one below."
                ErrorLabel.Visible = True
            End If

            Return ods

        Catch oe As OracleException
            ' Oracle Error
            ErrorLabel.Text = PPC.FDA.Data.OracleDataFactory.CleanOracleError(oe.Message)
            ErrorLabel.Visible = True
            Return Nothing

        Catch ex As Exception
            ' Application Error
            ErrorLabel.Text = ex.Message
            ErrorLabel.Visible = True
            Return Nothing

        End Try

    End Function

#End Region

    '''-----------------------------------------------------------------------------
    ''' <summary>
    '''  We capture the ItemDataBound event and grab the DDMac comment for this watchlist.
    ''' </summary>
    ''' <history>
    ''' 	[Jason Tucker] 	2/8/2005	Created
    ''' </history>
    '''-----------------------------------------------------------------------------
    Private Sub DownloadGrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DownloadGrid.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Then
            ' Lets try and grab the watchlist type.
            Try
                Dim DRV As DataRowView = e.Item.DataItem
                WatchListCommentsLabel.Text = DRV("ddmac_comments").ToString()
                Page.FindControl("WatchListComments").Visible = True
            Catch ex As Exception
                ' We don't need todo anything but bow out gracefully.
            End Try

        End If

    End Sub
End Class
