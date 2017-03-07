Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Partial Class Home
    Inherits System.Web.UI.Page
    Protected WithEvents theader As TrackingHeader
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

        'SRA(PB) - Testing multi-email error handling
        'Throw New Exception("This is a Testing Error")

        If ((Session("POCA_User") = "") And Request.QueryString("id") = "BeginSession") Then
            Dim userName() As String = Split(User.Identity.Name, "\")
            Session("POCA_User") = userName(1).ToUpper
            Dim LoginPerson As New PPC.FDA.Person.PersonObject()

            If LoginPerson.User_Login(Session("POCA_User")) Then
                Session("LoggedInUser") = LoginPerson
                Session("SID") = Session.SessionID
                Session("UserFullName") = LoginPerson.FullName()

                PO = Session("LoggedInUser")
            End If
        Else : PO = Session("LoggedInUser")
        End If


        If Not IsPostBack Then
            Dim s As String = ConfigurationManager.AppSettings("CustomDate").ToString()

            If PO.UserGroup.ToUpper() = "CONSULT COORDINATORS" Or PO.UserGroup.ToUpper().Contains("ADMINISTRATORS") Then
                'PostGrid.Columns(4).Visible = True
                'theader = Page.FindControl("TrackingHeader1")
                'theader.Location = theader.Location + " | <a href='AddConsult.aspx' alt='Click here to add a new consults'>Add New Consult</a> | <a href='EditConsultSearch.aspx' alt='Click here to edit a consult'>Edit Consult</a>"
            End If

            If PO.UserGroup.ToUpper() = "BUSINESS ADMINISTRATORS" Then
                ManualAdd.Visible = True
            End If

            'LoadDatagrid("pre", PreConsults, lblPreErrors, "PRODUCT_NAME")
            'LoadDatagrid("post", PostGrid, PostLabel, "PRODUCT_NAME")
        End If

    End Sub

#Region " LoadDataGrid Sub takes in the Consult Type and DataGrid and loads it up"
    Private Sub LoadDatagrid(ByVal ConsultType As String, ByVal ConsultDG As DataGrid, ByVal ErrorLabel As Label, ByVal SortColumn As String)

        Dim ods As DataSet
        Dim arrOParams() As OracleParameter = New OracleParameter(2) {}
        Dim dr As DataRow
        Dim boolValidate As Boolean = False

        arrOParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
        arrOParams(0).Value = PO.UserName

        arrOParams(1) = New OracleParameter("consult_type_in", OracleDbType.Varchar2)
        arrOParams(1).Value = ConsultType

        arrOParams(2) = New OracleParameter("the_consults", OracleDbType.RefCursor, ParameterDirection.Output)
        arrOParams(2).Value = Nothing

        Try
            ods = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "consult_get_open", arrOParams)

            Dim dc As DataColumn = New DataColumn()
            Dim Iterator As Integer
            Iterator = 0

            Dim odv As DataView = New DataView(ods.Tables(0))
            odv.Sort = GetSortExpression(ConsultType, SortColumn)

            If odv.Count <= 0 Then
                ErrorLabel.Text = "You do not have any watchlists availible."
                ErrorLabel.Visible = True
            Else

                If ConsultType = "post" Then
                End If

                'modify datatable to accomodate extra columns ...
                ConsultDG.DataSource = odv
                ConsultDG.DataBind()

            End If

        Catch oe As OracleException
            ErrorLabel.Text = "An error has occured " + ControlChars.NewLine + oe.Message

        End Try

    End Sub
#End Region

#Region " Load Cached Search Items"
    Private Sub Load_Search()

        Dim ctl As PPC.SimpleSearchControl
        Dim po As PPC.FDA.Person.PersonObject

        po = Session("LoggedInUser")
        ctl = FindControl("SimpleSearchControl1")

        Dim SearchType As String
        Try
            SearchType = po.GetSettingValue("SEARCH_VIEW")
            ctl = FindControl("SimpleSearchControl1")

            If SearchType.ToUpper() = "SIMPLE" Then
                ctl.TitleName = "Quick Search"
            Else
                ctl.TitleName = "Advanced Search"
            End If

            If Not Session("SearchTerm") Is Nothing Then
                ctl.SearchTextField = Session("SearchTerm")
                ctl.OrthoChecked = Session("OrthoSearch")
                ctl.PhoneChecked = Session("PhoneSearch")
                ctl.TextChecked = Session("TextSearch")
            End If

        Catch oe As OracleException
            ' UNDONE - Add error label.
        End Try

    End Sub
#End Region

#Region " Gets/Sets Sort expression from the ViewState"
    Private Function GetSortExpression(ByVal SortName As String, ByVal _sortExpression As String) As String
        ' If the sort expression has not been put in
        ' ViewState yet, do it now and return the
        ' sort expression appended with ASC

        If ViewState(SortName) Is Nothing Then
            ViewState(SortName) = _sortExpression + " ASC"
            ' If the sort expression is the same column the user clicked on
            ' Check the sort direction
        ElseIf ((ViewState(SortName).ToString()).StartsWith(_sortExpression)) Then
            ' If the sort direction is ASC, make it DESC
            ' If it is DESC make it ASC
            If ((ViewState(SortName).ToString()).EndsWith("ASC")) Then
                ViewState(SortName) = _sortExpression + " DESC"
            Else
                ViewState(SortName) = _sortExpression + " ASC"
            End If
            ' If the sort expression is a different column that the last
            ' sort expression, set the New sort expression in ViewState
            ' and make its direction ASC
        Else
            ViewState(SortName) = _sortExpression + " DESC"
        End If

        Return ViewState(SortName).ToString()

    End Function
#End Region

    'Private Sub PreConsults_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles PreConsults.SortCommand

    '    LoadDatagrid("pre", PreConsults, lblPreErrors, e.SortExpression)

    'End Sub

    'Private Sub PostGrid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles PostGrid.SortCommand

    '    LoadDatagrid("post", PostGrid, PostLabel, e.SortExpression)

    'End Sub

    'Public Sub PostGrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles PostGrid.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

    '        Dim RowCells As TableCellCollection = e.Item.Cells
    '        Dim HyperLinkCtl As HyperLink = New HyperLink()

    '        HyperLinkCtl.NavigateUrl = String.Format("watchlist.aspx?cid={0}&clt={1}", RowCells(0).Text, Server.UrlEncode(RowCells(2).Text))
    '        HyperLinkCtl.Text = RowCells(2).Text

    '        RowCells(2).Controls.Add(HyperLinkCtl)

    '    End If
    'End Sub

    'Public Sub PreConsults_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles PreConsults.ItemDataBound

    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

    '        Dim RowCells As TableCellCollection = e.Item.Cells
    '        Dim HyperLinkCtl As HyperLink = New HyperLink()
    '        Dim HyperLinkCtl2 As HyperLink = New HyperLink

    '        HyperLinkCtl.NavigateUrl = String.Format("watchlist.aspx?cid={0}&clt={1}", RowCells(0).Text, Server.UrlEncode(RowCells(2).Text))
    '        HyperLinkCtl.Text = RowCells(2).Text

    '        RowCells(2).Controls.Add(HyperLinkCtl)

    '        'DEMO CODE ....
    '        'HyperLinkCtl.NavigateUrl = String.Format("watchlist.aspx?cid={0}&clt={1}", RowCells(0).Text, Server.UrlEncode(RowCells(2).Text))
    '        'HyperLinkCtl.Text = "Watchlist"
    '        'RowCells(4).Controls.Add(HyperLinkCtl)

    '        'HyperLinkCtl2.NavigateUrl = String.Format("EditConsult.aspx?cid={0}&clt={1}", RowCells(0).Text, Server.UrlEncode(RowCells(2).Text))
    '        'HyperLinkCtl2.Text = "Edit"
    '        '    RowCells(5).Controls.Add(HyperLinkCtl2)
    '        'End: Demo code ...


    '    End If

    'End Sub

End Class
