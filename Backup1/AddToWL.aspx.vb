Imports Oracle.DataAccess.Client
Imports PPC.FDA.Controls
Partial Class AddToWL
    Inherits System.Web.UI.Page
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
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
        PO = CType(Session("LoggedInUser"), PPC.FDA.Person.PersonObject)
        If Not Page.IsPostBack Then
            PopulateWatchlist()

            Dim NewString() As String
            NewString = WLDropDown.SelectedItem.Value.Split("|")
            DisplayWatchList(NewString(1), NewString(0))
        End If

    End Sub

#Region "PopulateWatchList"
    Private Sub PopulateWatchlist()

        Dim ods As DataSet
        Dim odr As DataRow
        Dim OraParams() As OracleParameter = New OracleParameter(1) {}

        Try
            OraParams(0) = New OracleParameter("USERNAME_IN", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("USER_WATCH_LIST", OracleDbType.RefCursor, ParameterDirection.Output)
            OraParams(1).Value = Nothing

            ods = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "USER_WATCH_LIST_GET", OraParams)

            For Each odr In ods.Tables(0).Rows
                odr(0) = odr(0) + "|" + odr(1)
            Next

            WLDropDown.DataSource = ods
            WLDropDown.DataTextField = "PRODUCT_NAME"
            WLDropDown.DataValueField = "CONSULT_NUMBER"
            WLDropDown.DataBind()

        Catch ex As Exception
            lblCounter.Text = ex.Message
            lblCounter.Visible = True
        End Try

    End Sub
#End Region

#Region "Display WatchList"
    Protected Sub DisplayWatchList(ByVal ConsultName As String, ByVal ConsultID As String)

        Dim ItemArray() As String
        Dim WLItem As String
        Dim OraParams() = New OracleParameter(9) {}

        Dim WLTable As DataTable = New DataTable("NewConcerns")
        Dim WLDr As DataRow

        WLTable.Columns.Add(New DataColumn("name_of_concern", System.Type.GetType("System.String")))
        WLTable.Columns.Add(New DataColumn("name_of_concern_type", System.Type.GetType("System.String")))
        WLTable.Columns.Add(New DataColumn("type_of_concern", System.Type.GetType("System.String")))
        WLTable.Columns.Add(New DataColumn("level_of_concern", System.Type.GetType("System.String")))
        WLTable.Columns.Add(New DataColumn("product_name_uid", System.Type.GetType("System.String")))

        ItemArray = Request.QueryString("items").Split(",")
        If ItemArray.Length <> 0 Then
            For Each WLItem In ItemArray
                If Not WLItem = String.Empty Then
                    WLDr = WLTable.NewRow()
                    WLDr("name_of_concern") = GetNameFromUid(WLItem)
                    WLDr("product_name_uid") = WLItem
                    WLTable.Rows.Add(WLDr)
                End If
            Next

            PNLWatchList.Visible = True
            DataGrid1.Visible = True
            DataGrid1.DataSource = WLTable
            DataGrid1.DataKeyField = "product_name_uid"
            DataGrid1.DataBind()
        Else
            PNLWatchList.Visible = True
            DataGrid1.Visible = False
            ErrorLabel.Text = "You didn't select any items from your search."
            ErrorLabel.Visible = True
        End If
    End Sub
#End Region

    Private Function GetNameFromUid(ByVal ProductNameUid As String) As String

        Dim commandString As String = String.Format("SELECT u_name FROM mr_product_name WHERE ui_mr_product_name_uid = HEXTORAW('{0}')", ProductNameUid)
        Dim prodcutNameReader As OracleDataReader = PPC.FDA.Data.OracleDataFactory.ExecuteReader(ConfigurationManager.AppSettings("ConnectionString"), CommandType.Text, commandString)
        Dim productNameReturn As String = String.Empty

        While prodcutNameReader.Read()
            productNameReturn = prodcutNameReader.GetString(0)
        End While
        prodcutNameReader.Close()

        Return productNameReturn
    End Function

#Region "DataGrid ItemCommand"
    Private Sub dg_itemcommand(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        If e.CommandName = "Add" Then

            Dim NewString() As String
            NewString = WLDropDown.SelectedItem.Value.Split("|")
            AddToWatchList(NewString(1), NewString(0))

        End If
    End Sub
#End Region

#Region "Add To Watchlist"
    Private Sub AddToWatchList(ByVal ConsultName As String, ByVal ConsultID As String)

        Dim _dgitem As DataGridItem
        Dim _selected As Integer
        Dim _qrystring As String
        Dim OraParams() As OracleParameter = New OracleParameter(10) {}

        For Each _dgitem In DataGrid1.Items
            If _dgitem.ItemType = ListItemType.AlternatingItem Or _dgitem.ItemType = ListItemType.Item Then

                Dim _NameConcernType As TextBox = _dgitem.FindControl("TxtConcernType")
                Dim _TypeConcern As DropDownList = _dgitem.FindControl("DDLTypeConcern")
                Dim _LevelConcern As DropDownList = _dgitem.FindControl("DDLLevelConcern")
                Dim _Narrative As TextBox = _dgitem.FindControl("TANarrative")

                OraParams(0) = New OracleParameter("USERNAME_IN", OracleDbType.Varchar2)
                OraParams(0).Value = PO.UserName

                OraParams(1) = New OracleParameter("CONSULT_NUMBER_IN", OracleDbType.Varchar2)
                OraParams(1).Value = ConsultID

                OraParams(2) = New OracleParameter("PRODUCT_NAME_IN", OracleDbType.Varchar2)
                OraParams(2).Value = ConsultName

                OraParams(3) = New OracleParameter("NAME_OF_CONCERN_UID_IN", OracleDbType.Varchar2)
                OraParams(3).Value = DataGrid1.DataKeys(_dgitem.ItemIndex)

                OraParams(4) = New OracleParameter("LIST_TYPE_IN", OracleDbType.Varchar2)
                OraParams(4).Value = "Individual"

                OraParams(5) = New OracleParameter("NAME_OF_CONCERN_IN", OracleDbType.Varchar2)
                OraParams(5).Value = ValidateValue(_dgitem.Cells(0).Text)

                OraParams(6) = New OracleParameter("NAME_OF_CONCERN_MOD_IN", OracleDbType.Varchar2)
                OraParams(6).Value = Nothing

                OraParams(7) = New OracleParameter("TYPE_OF_CONCERN_IN", OracleDbType.Varchar2)
                OraParams(7).Value = _TypeConcern.SelectedItem.Value

                OraParams(8) = New OracleParameter("LEVEL_OF_CONCERN_IN", OracleDbType.Varchar2)
                OraParams(8).Value = _LevelConcern.SelectedItem.Value

                OraParams(9) = New OracleParameter("SEQUENCE_IN", OracleDbType.Int32)
                OraParams(9).Value = 0

                OraParams(10) = New OracleParameter("NARRATIVE_IN", OracleDbType.Clob)
                OraParams(10).Value = ValidateValue(_Narrative.Text)

                Try
                    Dim retval As Integer
                    retval = PPC.FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "watch_list_add", OraParams)
                    If retval >= 0 Then
                        ErrorLabel.Text = "Add successful."
                        ErrorLabel.ForeColor = Color.Red
                        ErrorLabel.Visible = True
                        _TypeConcern.Enabled = False
                        _LevelConcern.Enabled = False
                        _Narrative.Enabled = False
                    End If

                Catch oe As OracleException
                    ErrorLabel.Text = "There was a problem connecting to the database. The error follows: " + ControlChars.NewLine + oe.Message
                    ErrorLabel.Visible = True
                Catch ex As Exception
                    ErrorLabel.Text = "There was a problem. The error follows: " + ControlChars.NewLine + ex.Message
                    ErrorLabel.Visible = True
                End Try
            End If
        Next

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

#Region "DataGrid ItemDataBound"
    Private Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim _ddlTC As DropDownList = e.Item.FindControl("DDLTypeConcern")
            Dim _ddlLC As DropDownList = e.Item.FindControl("DDLLevelConcern")

            _ddlTC.DataSource = DDLDataBind("Type_of_concern")
            _ddlTC.DataBind()

            _ddlLC.DataSource = DDLDataBind("Level_of_Concern")
            _ddlLC.DataBind()

        End If
    End Sub
#End Region

    Private Function ValidateValue(ByVal inputvalue As String) As String

        If inputvalue = "" Then
            Return Nothing
        Else
            Return inputvalue
        End If

    End Function
End Class
