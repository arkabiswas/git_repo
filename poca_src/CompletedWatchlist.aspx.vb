Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports PPC.FDA.Controls
Imports PPC.FDA.Data

Partial Class CompletedWatchlist
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

    ' Global Page-Level Vars
    Private PO As PPC.FDA.Person.PersonObject
    Private ods As DataSet
    Private odv As DataView

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        PO = Session("LoggedInUser")


        If Not IsPostBack Then

            LoadConsult()

            ' Set the DDMacComments Label to non-visible
            '            Page.FindControl("WatchListCommentsLabel").Visible = False

            ' Create out dataset of watchlist data.
            ods = CreateDataSet(Request("cid"), Request("clt"))

            ' Set the name of the Watchlist. 
            WatchListTitle.Text = "Watchlist for " + Request("clt") & "<br>"

            ' Load the Watchlist DropDownList for multiple WatchLists.
            LoadWatchListDDL(ods)

            If DDLWatchLists.SelectedItem Is Nothing Then
                DDLWatchLists.Visible = False
                WatchListHistory.Visible = False
                HistoryErrorLabel.Visible = False
                FDAGrid2.Visible = False
                LoadDataGrid(FDAGrid1, ods, Nothing)
            Else
                LoadDataGrid(FDAGrid1, ods, DDLWatchLists.SelectedItem.Value)
                If DDLWatchLists.SelectedItem.Text.ToUpper.StartsWith("CONSOLIDATED") Then
                    LoadHistory(DDLWatchLists.SelectedItem.Value)
                Else
                    WatchListHistory.Visible = False
                    HistoryErrorLabel.Visible = False
                    FDAGrid2.Visible = False
                End If
            End If

        End If

    End Sub

#Region "LoadConsult"
    Private Sub LoadConsult()

        Dim odr As OracleDataReader
        Dim ods As DataSet

        Dim OraParams() As OracleParameter = New OracleParameter(3) {}

        Try
            OraParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("consult_number_in", OracleDbType.Varchar2)
            OraParams(1).Value = Request("cid")

            OraParams(2) = New OracleParameter("product_name_in", OracleDbType.Varchar2)
            OraParams(2).Value = Request("clt")

            OraParams(3) = New OracleParameter("the_consult", OracleDbType.RefCursor, ParameterDirection.Output)
            OraParams(3).Value = Nothing

            ods = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "CONSULT_GET", OraParams)

            Dim dr As DataRow

            Try
                For Each dr In ods.Tables(0).Rows
                    Select Case dr("product_type_name").ToString()
                        Case "Alternative" ' Alternative Name Processing
                            lblAlternativeName.Text = dr("product_name").ToString()
                            '                          lblAlternateNameUID.Text = dr("name_type").ToString() + "|" + dr("consult_id").ToString() + "|" + dr("product_id").ToString() + "|" + dr("product_name_id").ToString()
                        Case "Established" ' Established Name Processing
                            lblEstablishedName.Text = dr("product_name").ToString()
                            '                           lblEstablishedNameUID.Text = dr("name_type").ToString() + "|" + dr("consult_id").ToString() + "|" + dr("product_id").ToString() + "|" + dr("product_name_id").ToString()
                        Case "Proprietary", "Unknown" ' Product Name Processing
                            lblProductName.Text = dr("product_name").ToString()
                            '                            lblProductNameUID.Text = dr("name_type").ToString() + "|" + dr("consult_id").ToString() + "|" + dr("product_id").ToString() + "|" + dr("product_name_id").ToString()

                            lblConsultNumber.Text = dr("consult_number").ToString()
                            lblAppNumber.Text = dr("app_number").ToString()
                            lblProductModifier.Text = dr("product_modifier").ToString()
                            lblComments.Text = dr("CONSULT_NARRATIVE").ToString()

                            If Not dr("dt_epd").ToString() = "" Then
                                lblEPDDate.Text = String.Format("{0:MM-dd-yyyy}", CType(dr("dt_epd").ToString(), DateTime))
                            End If

                            If Not dr("dt_received").ToString() = "" Then
                                lblDTReceived.Text = String.Format("{0:MM-dd-yyyy}", CType(dr("dt_received").ToString(), DateTime))
                            End If

                            lblAppType.Text = dr("app_type_name").ToString()

                    End Select
                Next

            Catch ex As Exception
                ErrorLabel.Text = ex.Message
                ErrorLabel.Visible = True
            End Try


        Catch oe As OracleException
            ErrorLabel.Text = "There was an error: " + oe.Message
            ErrorLabel.ForeColor = Color.Red
            ErrorLabel.Visible = True
        End Try

    End Sub

#End Region

#Region "LoadWatchListDDL"
    ' LoadWatchListDDL will search through the dataset and determine the unique WatchLists 
    ' and then add them to the drop down list.
    Private Sub LoadWatchListDDL(ByVal WatchListDataSet As DataSet)

        Dim dr As DataRow
        Dim PrevVal As String

        For Each dr In WatchListDataSet.Tables(0).Rows
            If Not PrevVal = dr("watch_list_type") + " " + dr("list_owner") Then
                PrevVal = dr("watch_list_type") + " " + dr("list_owner")

                Dim li As New ListItem
                li.Text = dr("watch_list_type").ToString() + " - " + dr("list_owner").ToString()
                li.Value = dr("watch_list_id").ToString()

                DDLWatchLists.Items.Add(li)
            End If
        Next
    End Sub
#End Region


    Private Sub DDLWatchLists_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLWatchLists.SelectedIndexChanged
        LoadDataGrid(FDAGrid1, CreateDataSet(Request("cid"), Request("clt")), DDLWatchLists.SelectedItem.Value)

        WatchListTitle.Text = "Watchlist for " + Request("clt") & "<br>"

        If DDLWatchLists.SelectedItem.Text.ToUpper.StartsWith("CONSOLIDATED") Then
            LoadHistory(DDLWatchLists.SelectedItem.Value)
        Else
            WatchListHistory.Visible = False
            HistoryErrorLabel.Visible = False
            FDAGrid2.Visible = False
        End If
    End Sub


#Region "LoadDataGrid"

    ' LoadDataGrid will take the data that is sent to it and bind it to the appropriate dataset. 
    Private Sub LoadDataGrid(ByVal WLDataGrid As FDAGrid, ByVal WLDataSet As DataSet, ByVal WatchListSelected As String)

        Dim SortString As String = WLDataGrid.SortExpression
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
            If Not SortString Is Nothing Then
                WLDV.Sort = SortString
                If Not WLDataGrid.IsSortedAscending Then
                    WLDV.Sort += " DESC"
                End If
            End If
            WLDataGrid.DataSource = WLDV
        Else
            WLDV = New DataView(WLDataSet.Tables(0))
            If Not SortString Is Nothing Then
                WLDV.Sort = SortString
                If Not WLDataGrid.IsSortedAscending Then
                    WLDV.Sort += " DESC"
                End If
            End If
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
            odv = New DataView(ods.Tables(0))

            If odv.Count <= 0 Then
                ErrorLabel.Text = "<br> There are no consults on this watchlist."
                ErrorLabel.Visible = True

                Dim WatchListComments As HtmlGenericControl = FindControl("WatchListComments")
                If Not WatchListComments Is Nothing Then
                    WatchListComments.Visible = False
                End If
            End If

            Return ods

        Catch oe As OracleException
            ' Oracle Error
            ErrorLabel.Text = oe.Message
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

#Region "LoadHistory"

    Private Sub LoadHistory(ByVal WatchListId As String)


        Dim arrOParams() As OracleParameter = New OracleParameter(2) {}
        Dim HistDataSet As DataSet
        Dim HistDataView As DataView
        WatchListHistory.Visible = True
        HistoryErrorLabel.Visible = True
        FDAGrid2.Visible = True

        Try
            arrOParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
            arrOParams(0).Value = PO.UserName

            arrOParams(1) = New OracleParameter("watch_list_id_in", OracleDbType.Varchar2)
            arrOParams(1).Value = WatchListId

            arrOParams(2) = New OracleParameter("the_list", OracleDbType.RefCursor, ParameterDirection.Output)
            arrOParams(2).Value = Nothing

            HistDataSet = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "watch_list_get_history", arrOParams)
            HistDataView = New DataView(HistDataSet.Tables(0))

            If HistDataView.Count <= 0 Then
                HistoryErrorLabel.Text = "<br> There is not history associated to this watchlist."
            End If

            FDAGrid2.DataSource = HistDataSet
            FDAGrid2.DataBind()

        Catch oe As OracleException
            ' Oracle Error
            HistoryErrorLabel.Text = oe.Message
            HistoryErrorLabel.Visible = True

        Catch ex As Exception
            ' Application Error
            HistoryErrorLabel.Text = ex.Message
            HistoryErrorLabel.Visible = True

        End Try
    End Sub

#End Region



    Private Sub SetupPrintAndDownload()


        FDAGrid1.Columns(0).Visible = False
        FDAGrid1.Columns(1).Visible = True

        If DDLWatchLists.Visible = False Then
            LoadDataGrid(FDAGrid1, CreateDataSet(Request("cid"), Request("clt")), Nothing)
        Else
            LoadDataGrid(FDAGrid1, CreateDataSet(Request("cid"), Request("clt")), DDLWatchLists.SelectedItem.Value)
            WatchListTitle.Text = DDLWatchLists.SelectedItem.Text
        End If

        FDAGrid1.ShowFooter = False
        DDLWatchLists.Visible = False

        Page.FindControl("printDownloadSec").Visible = False
        '    WatchListHistory.Visible = False
        HistoryErrorLabel.Visible = False
        '   FDAGrid2.Visible = False

    End Sub
#Region "Print and Download Functions"

    Private Sub PrintScreen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PrintScreen.Click
        SetupPrintAndDownload()
    End Sub

    Private Sub DownloadScreen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DownloadScreen.Click
        ' Set this to the Excel MIME Type so the browser will treat it like
        ' an Excel file.
        Response.ContentType = "application/vnd.ms-excel"
        Response.Charset = ""

        SetupPrintAndDownload()
        Page.FindControl("TrackingHeader1").Visible = False
        Page.FindControl("cssPage").Visible = False

        Dim WLid As String
        If DDLWatchLists.Visible = False Then
            WLid = ""
        Else
            WLid = DDLWatchLists.SelectedItem.Value
        End If

        '        Server.Execute("DownloadWatchList.aspx?cid=" + Request("cid") + "&clt=" + Request("clt") + "&wl=" + WLid)

    End Sub

#End Region

End Class
