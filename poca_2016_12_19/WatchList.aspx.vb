Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports PPC.FDA.Controls
Imports PPC.FDA.Data

Partial Class WatchList
    Inherits System.Web.UI.Page

    'Page Controls

    ' Global Page-Level Vars
    Private PO As PPC.FDA.Person.PersonObject
    Private ods As DataSet
    Private odv As DataView
    Private StrTypeConcern As String
    Private StrLevelConcern As String

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
        ErrorLabel.Text = ""
        DDMacError.Text = ""

        If Not Page.IsPostBack Then
            ' Set the DDMacComments Label to non-visible
            Page.FindControl("WatchListCommentsLabel").Visible = False

            ' Create out dataset of watchlist data.
            ods = CreateDataSet(Request("cid"), Request("clt"))

            ' Set the name of the Watchlist. 
            WatchListTitle.Text = "Watchlist for " + Request("clt")

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

        If Request.Form("custom_action") = "delete" Then
            DeleteItem(Request.Form("delete_narrative"))
        End If

    End Sub

#Region "DataGrid ItemCommand"

    Private Sub dg_itemcommand(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles FDAGrid1.ItemCommand
        If e.CommandName = "Add" Then

            Dim _selected As Integer
            Dim _qrystring As String
            Dim _NameConcernType As TextBox = e.Item.FindControl("NewNameofConcern")
            Dim _TypeConcern As DropDownList = e.Item.FindControl("NewDDLTypeofConcern")
            Dim _LevelConcern As DropDownList = e.Item.FindControl("NewDDLLevelofConcern")
            Dim _Narrative As TextBox = e.Item.FindControl("NewNarrative")

            Dim OraParams() As OracleParameter = New OracleParameter(10) {}

            OraParams(0) = New OracleParameter("USERNAME_IN", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("CONSULT_NUMBER_IN", OracleDbType.Varchar2)
            OraParams(1).Value = Request("cid")

            OraParams(2) = New OracleParameter("PRODUCT_NAME_IN", OracleDbType.Varchar2)
            OraParams(2).Value = Request("clt")

            OraParams(3) = New OracleParameter("NAME_OF_CONCERN_UID_IN", OracleDbType.Varchar2)
            OraParams(3).Value = Nothing

            OraParams(4) = New OracleParameter("LIST_TYPE_IN", OracleDbType.Varchar2)
            OraParams(4).Value = WatchListType.Text

            OraParams(5) = New OracleParameter("NAME_OF_CONCERN_IN", OracleDbType.Varchar2)
            OraParams(5).Value = _NameConcernType.Text

            OraParams(6) = New OracleParameter("NAME_OF_CONCERN_MOD_IN", OracleDbType.Varchar2)
            OraParams(6).Value = Nothing

            OraParams(7) = New OracleParameter("TYPE_OF_CONCERN_IN", OracleDbType.Varchar2)
            OraParams(7).Value = _TypeConcern.SelectedItem.Value

            OraParams(8) = New OracleParameter("LEVEL_OF_CONCERN_IN", OracleDbType.Varchar2)
            OraParams(8).Value = _LevelConcern.SelectedItem.Value

            OraParams(9) = New OracleParameter("SEQUENCE_IN", OracleDbType.Int32)
            OraParams(9).Value = 1

            OraParams(10) = New OracleParameter("NARRATIVE_IN", OracleDbType.Clob)
            If _Narrative.Text = "" Then
                OraParams(10).Value = Nothing
            Else
                OraParams(10).Value = _Narrative.Text
            End If

            Try
                Dim retval As Integer
                retval = OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "watch_list_add", OraParams)
                If retval >= 0 Then
                    ErrorLabel.Text = "Add successful."
                    ErrorLabel.ForeColor = Color.Red
                    ErrorLabel.Visible = True

                    ods = CreateDataSet(Request("cid"), Request("clt"))

                    If DDLWatchLists.SelectedItem Is Nothing Then
                        LoadWatchListDDL(ods)
                    End If
                    LoadDataGrid(FDAGrid1, ods, DDLWatchLists.SelectedItem.Value)

                    Dim WatchListComments As HtmlGenericControl = FindControl("WatchListComments")
                    If Not WatchListComments Is Nothing Then
                        WatchListComments.Visible = True
                    End If

                    If DDLWatchLists.SelectedItem.Text.ToUpper.IndexOf("CONSOLIDATED") > 0 Then
                        LoadHistory(DDLWatchLists.SelectedItem.Value)
                    End If

                    _Narrative.Text = sender.GetType.ToString
                End If

            Catch oe As OracleException
                ErrorLabel.Text = PPC.FDA.Data.OracleDataFactory.CleanOracleError(oe.Message)
                ErrorLabel.Visible = True
            Catch ex As Exception
                ErrorLabel.Text = "There was a problem. The error follows: " + ControlChars.NewLine + ex.Message
                ErrorLabel.Visible = True
            End Try
        End If
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
            ErrorLabel.Text = "Error: " + oe.Message
            ErrorLabel.Visible = True

        Catch ex As Exception
            ErrorLabel.Text = "Error: " + ex.Message
            ErrorLabel.Visible = True

        End Try
    End Function
#End Region

    Public Sub Grid_Edit(ByVal Sender As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles FDAGrid1.EditCommand
        FDAGrid1.EditItemIndex = e.Item.ItemIndex
        FDAGrid1.Columns(6).Visible = True
        StrTypeConcern = CType(e.Item.FindControl("TypeConcern"), Label).Text
        StrLevelConcern = CType(e.Item.FindControl("LevelConcern"), Label).Text
        LoadDataGrid(FDAGrid1, CreateDataSet(Request("cid"), Request("clt")), DDLWatchLists.SelectedItem.Value)
    End Sub

    Private Sub Grid_Cancel(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles FDAGrid1.CancelCommand
        FDAGrid1.EditItemIndex = -1
        FDAGrid1.Columns(6).Visible = False
        LoadDataGrid(FDAGrid1, CreateDataSet(Request("cid"), Request("clt")), DDLWatchLists.SelectedItem.Value)
    End Sub

    Public Sub Grid_Update(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles FDAGrid1.UpdateCommand

        Dim _NameConcernType As TextBox = e.Item.FindControl("NameofConcern")

        If _NameConcernType.Text = "" Then
            ErrorLabel.Text = "You must enter a name of concern."
            ErrorLabel.ForeColor = Color.Red
            ErrorLabel.Visible = True
        Else
            ModifyWatchList(e)
        End If

    End Sub

#Region "Item_DataBound"

    ' Item_DataBound is called each time a row has been 
    ' databound to the grid.
    Private Sub Item_DataBound(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles FDAGrid1.ItemDataBound
        Dim _ddltype As DropDownList
        Dim _ddllevel As DropDownList

        If e.Item.ItemType = ListItemType.Item Then
            ' Lets try and grab the watchlist type.
            Try
                Dim DRV As DataRowView = e.Item.DataItem
                WatchListType.Text = DRV("watch_list_type").ToString()
                DDMACComments.Text = DRV("ddmac_comments").ToString()
                DDMacCommentsLabel.Text = DRV("ddmac_comments").ToString()
            Catch ex As Exception
                ' There isn't a watchlist type, so we will default to individual.
                WatchListType.Text = "Individual"
            End Try

        End If

        If e.Item.ItemType = ListItemType.EditItem Then
            Try

                _ddltype = e.Item.FindControl("DDLTypeofconcern")
                _ddllevel = e.Item.FindControl("DDLLevelofconcern")

                _ddltype.DataSource = DDLDataBind("Type_of_concern")
                _ddltype.DataBind()

                _ddllevel.DataSource = DDLDataBind("Level_of_concern")
                _ddllevel.DataBind()

                _ddltype.SelectedIndex = _ddltype.Items.IndexOf(_ddltype.Items.FindByText(StrTypeConcern))
                _ddllevel.SelectedIndex = _ddllevel.Items.IndexOf(_ddllevel.Items.FindByText(StrLevelConcern))

            Catch ex As Exception

            End Try

        End If

        If e.Item.ItemType = ListItemType.Footer Then

            Try

                Dim _ddlTC As DropDownList = e.Item.FindControl("NewDDLTypeofconcern")
                Dim _ddlLC As DropDownList = e.Item.FindControl("NewDDLLevelofconcern")

                _ddlTC.DataSource = DDLDataBind("Type_of_concern")
                _ddlTC.DataBind()

                _ddlLC.DataSource = DDLDataBind("Level_of_Concern")
                _ddlLC.DataBind()

            Catch ex As Exception

            End Try

        End If

    End Sub
#End Region

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
                ErrorLabel.Text = "<br> There are no consults on this watchlist. Please add a new one below."
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

        If DDLWatchLists.SelectedItem.Text.ToUpper.StartsWith("CONSOLIDATED") Then
            LoadHistory(DDLWatchLists.SelectedItem.Value)
        Else
            WatchListHistory.Visible = False
            HistoryErrorLabel.Visible = False
            FDAGrid2.Visible = False
        End If
    End Sub

    Private Function ValidateValue(ByVal inputvalue As String) As String

        If inputvalue = "" Then
            Return Nothing
        Else
            Return inputvalue
        End If

    End Function

#Region "ModifyWatchList"
    ' ModifyWatchList is a sub that will update the watchlist items.
    '
    '
    Private Sub ModifyWatchList(ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)

        Dim _NameConcernType As TextBox = e.Item.FindControl("NameofConcern")
        Dim _TypeConcern As DropDownList = e.Item.FindControl("DDLTypeofConcern")
        Dim _LevelConcern As DropDownList = e.Item.FindControl("DDLLevelofconcern")
        Dim _Narrative As TextBox = e.Item.FindControl("Narrative")
        Dim ProductNameId As HtmlInputHidden = e.Item.FindControl("ProductNameId")

        Dim OraParams() As OracleParameter = New OracleParameter(10) {}
        Dim RetVal As Integer
        Dim IsDelete As Integer = 0

        Try
            OraParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("watch_list_id_in", OracleDbType.Varchar2)
            OraParams(1).Value = FDAGrid1.DataKeys(e.Item.ItemIndex)

            OraParams(2) = New OracleParameter("name_of_concern_id_in", OracleDbType.Varchar2)
            OraParams(2).Value = ProductNameId.Value

            OraParams(3) = New OracleParameter("name_of_concern_in", OracleDbType.Varchar2)
            OraParams(3).Value = _NameConcernType.Text

            OraParams(4) = New OracleParameter("type_of_concern_in", OracleDbType.Varchar2)
            OraParams(4).Value = _TypeConcern.SelectedItem.Value

            OraParams(5) = New OracleParameter("level_of_concern_in", OracleDbType.Varchar2)
            OraParams(5).Value = _LevelConcern.SelectedItem.Value

            OraParams(6) = New OracleParameter("sequence_in", OracleDbType.Int32)
            OraParams(6).Value = 0

            OraParams(7) = New OracleParameter("narrative_in", OracleDbType.Clob, ParameterDirection.Input)
            OraParams(7).Value = ValidateValue(_Narrative.Text)

            OraParams(8) = New OracleParameter("delete_in", OracleDbType.Double)
            OraParams(8).Value = Nothing

            OraParams(9) = New OracleParameter("delete_narrative_in", OracleDbType.Clob)
            OraParams(9).Value = Nothing

            OraParams(10) = New OracleParameter("ddmac_comments_in", OracleDbType.Clob)
            OraParams(10).Value = ValidateValue(DDMACComments.Text)

            RetVal = OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "watch_list_modify", OraParams)

            If RetVal <> 0 Then
                ErrorLabel.Text = "Update Successful"
                ErrorLabel.ForeColor = Color.Red
                ErrorLabel.Visible = True

                FDAGrid1.EditItemIndex = -1
                FDAGrid1.Columns(6).Visible = False

                ods = CreateDataSet(Request("cid"), Request("clt"))
                If DDLWatchLists.SelectedItem Is Nothing Then
                    LoadWatchListDDL(ods)
                End If
                LoadDataGrid(FDAGrid1, ods, DDLWatchLists.SelectedItem.Value)

                If DDLWatchLists.SelectedItem.Text.ToUpper.IndexOf("CONSOLIDATED") > 0 Then
                    LoadHistory(DDLWatchLists.SelectedItem.Value)
                End If
            Else
                ErrorLabel.Text = "Update failed. Please check your values and try again."
                ErrorLabel.ForeColor = Color.Red
                ErrorLabel.Visible = True
            End If

        Catch oe As OracleException
            ErrorLabel.Text = OracleDataFactory.CleanOracleError(oe.Message)
            ErrorLabel.ForeColor = Color.Red
            ErrorLabel.Visible = True
        Catch OtherException As Exception
            ErrorLabel.Text = OtherException.Message
            ErrorLabel.ForeColor = Color.Red
            ErrorLabel.Visible = True
        End Try
    End Sub
#End Region

#Region "DeleteItem"
    '
    ' Delete Item will delete an item from the watchlist
    '
    Private Sub DeleteItem(ByVal ItemNarrative As String)

        Dim DGItem As DataGridItem = FDAGrid1.Items(FDAGrid1.EditItemIndex)


        Dim _NameConcernType As TextBox = DGItem.FindControl("NameofConcern")
        Dim _TypeConcern As DropDownList = DGItem.FindControl("DDLTypeofConcern")
        Dim _LevelConcern As DropDownList = DGItem.FindControl("DDLLevelofconcern")
        Dim _Narrative As TextBox = DGItem.FindControl("Narrative")
        Dim ProductNameId As HtmlInputHidden = DGItem.FindControl("ProductNameId")

        Dim OraParams() As OracleParameter = New OracleParameter(9) {}
        Dim RetVal As Integer
        Dim IsDelete As Integer = 0

        Try
            OraParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("watch_list_id_in", OracleDbType.Varchar2)
            OraParams(1).Value = FDAGrid1.DataKeys(DGItem.ItemIndex)

            OraParams(2) = New OracleParameter("name_of_concern_id_in", OracleDbType.Varchar2)
            OraParams(2).Value = ProductNameId.Value

            OraParams(3) = New OracleParameter("name_of_concern_in", OracleDbType.Varchar2)
            OraParams(3).Value = _NameConcernType.Text

            OraParams(4) = New OracleParameter("type_of_concern_in", OracleDbType.Varchar2)
            OraParams(4).Value = _TypeConcern.SelectedItem.Value

            OraParams(5) = New OracleParameter("level_of_concern_in", OracleDbType.Varchar2)
            OraParams(5).Value = _LevelConcern.SelectedItem.Value

            OraParams(6) = New OracleParameter("sequence_in", OracleDbType.Int32)
            OraParams(6).Value = 0

            OraParams(7) = New OracleParameter("narrative_in", OracleDbType.Clob, ParameterDirection.Input)
            OraParams(7).Value = ValidateValue(_Narrative.Text)

            OraParams(8) = New OracleParameter("delete_in", OracleDbType.Int32)
            OraParams(8).Value = 1

            OraParams(9) = New OracleParameter("delete_narrative_in", OracleDbType.Clob, ParameterDirection.Input)
            OraParams(9).Value = ValidateValue(ItemNarrative)

            RetVal = FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "watch_list_modify", OraParams)

            If RetVal <> 0 Then
                ErrorLabel.Text = "Item Deleted"
                ErrorLabel.ForeColor = Color.Red
                ErrorLabel.Visible = True

                FDAGrid1.EditItemIndex = -1
                FDAGrid1.Columns(6).Visible = False

                ods = CreateDataSet(Request("cid"), Request("clt"))
                If DDLWatchLists.SelectedItem Is Nothing Then
                    LoadWatchListDDL(ods)
                End If
                LoadDataGrid(FDAGrid1, ods, DDLWatchLists.SelectedItem.Value)
                If DDLWatchLists.SelectedItem.Text.ToUpper.IndexOf("CONSOLIDATED") > 0 Then
                    LoadHistory(DDLWatchLists.SelectedItem.Value)
                End If

            Else
                ErrorLabel.Text = "Update failed. Please check your values and try again."
                ErrorLabel.ForeColor = Color.Red
                ErrorLabel.Visible = True
            End If


        Catch oe As OracleException
            ErrorLabel.Text = oe.Message
            ErrorLabel.ForeColor = Color.Red
            ErrorLabel.Visible = True
        End Try
    End Sub
#End Region

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

    Private Sub FDAGrid1_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles FDAGrid1.SortCommand
        If DDLWatchLists.SelectedItem Is Nothing Then
            LoadDataGrid(FDAGrid1, CreateDataSet(Request("cid"), Request("clt")), Nothing)
        Else
            LoadDataGrid(FDAGrid1, CreateDataSet(Request("cid"), Request("clt")), DDLWatchLists.SelectedItem.Value)
        End If

    End Sub


    Private Sub PrintScreen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PrintScreen.Click

        If DDLWatchLists.Visible = False Then
            LoadDataGrid(FDAGrid1, CreateDataSet(Request("cid"), Request("clt")), Nothing)
        Else
            LoadDataGrid(FDAGrid1, CreateDataSet(Request("cid"), Request("clt")), DDLWatchLists.SelectedItem.Value)
        End If


        ' Hide None Print Related Items.

        Page.FindControl("Trackingheader2").Visible = False
        Page.FindControl("WatchListComments").Visible = False
        Page.FindControl("WatchListCommentsLabel").Visible = True
        FDAGrid1.ShowFooter = False
        DDLWatchLists.Visible = False
        PrintScreen.Visible = False
        SeperatorPipe.Visible = False
        DownloadScreen.Visible = False
        WatchListHistory.Visible = False
        HistoryErrorLabel.Visible = False
        FDAGrid2.Visible = False

    End Sub

    Private Sub DownloadScreen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DownloadScreen.Click

        Dim WLid As String
        If DDLWatchLists.Visible = False Then
            WLid = ""
        Else
            WLid = DDLWatchLists.SelectedItem.Value
        End If

        Server.Execute("DownloadWatchList.aspx?cid=" + Request("cid") + "&clt=" + Request("clt") + "&wl=" + WLid)

    End Sub

    Private Sub DDMACAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDMACAdd.Click
        If DDMACComments.Text = String.Empty Then
            DDMacError.Text = "You must enter something into the text box to add a comment."
        Else
            Dim OraParams() As OracleParameter = New OracleParameter(10) {}

            OraParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("watch_list_id_in", OracleDbType.Varchar2)
            OraParams(1).Value = DDLWatchLists.SelectedItem.Value

            OraParams(2) = New OracleParameter("name_of_concern_id_in", OracleDbType.Varchar2)
            OraParams(2).Value = Nothing

            OraParams(3) = New OracleParameter("name_of_concern_in", OracleDbType.Varchar2)
            OraParams(3).Value = Nothing

            OraParams(4) = New OracleParameter("type_of_concern_in", OracleDbType.Varchar2)
            OraParams(4).Value = Nothing

            OraParams(5) = New OracleParameter("level_of_concern_in", OracleDbType.Varchar2)
            OraParams(5).Value = Nothing

            OraParams(6) = New OracleParameter("sequence_in", OracleDbType.Int32)
            OraParams(6).Value = Nothing

            OraParams(7) = New OracleParameter("narrative_in", OracleDbType.Clob, ParameterDirection.Input)
            OraParams(7).Value = Nothing

            OraParams(8) = New OracleParameter("delete_in", OracleDbType.Int32)
            OraParams(8).Value = Nothing

            OraParams(9) = New OracleParameter("delete_narrative_in", OracleDbType.Clob, ParameterDirection.Input)
            OraParams(9).Value = Nothing

            OraParams(10) = New OracleParameter("ddmac_comments_in", OracleDbType.Clob)
            OraParams(10).Value = ValidateValue(DDMACComments.Text)

            Try
                PPC.FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "watch_list_modify", OraParams)
                DDMacError.Text = "Comment added successfully."

            Catch DDMACException As OracleException
                DDMacError.Text = "Comment add failed: " + DDMACException.Message
            End Try
        End If

    End Sub

End Class
