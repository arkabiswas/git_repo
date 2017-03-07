Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Partial Class ArchiveSearch
    Inherits System.Web.UI.Page
    Protected PO As FDA.Person.PersonObject

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

        If Request("sr") = "ggg" Then
            ExecuteSearch()
        End If

        If IsPostBack Then
            ErrorLabel.Visible = False
            ResultsGrid.Visible = False
            ResultsLabel.Visible = False
        End If

        If Not IsPostBack Then
            ' Bind the usernamelist dropdownlist

            Dim OraParams() As OracleParameter = New OracleParameter(0) {}
            OraParams(0) = New OracleParameter("the_userlist", OracleDbType.RefCursor, ParameterDirection.Output)
            OraParams(0).Value = Nothing

            UsernameList.DataSource = FDA.Data.OracleDataFactory.ExecuteReader(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "userlist_get", OraParams)
            UsernameList.DataTextField = "u_username"
            UsernameList.DataValueField = "u_username"
            UsernameList.DataBind()
            UsernameList.Items.Insert(0, New ListItem(" ", ""))

            'bind the searchmodulelist dropdownlist
            SearchModuleList.DataSource = DDLDataBind("search_type")
            SearchModuleList.DataTextField = "description"
            SearchModuleList.DataValueField = "pick_list_item_id"
            SearchModuleList.DataBind()
            SearchModuleList.Items.Insert(0, New ListItem(" ", ""))
        End If

    End Sub

    Private Sub SearchReset_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles SearchReset.Click

        Try
            UsernameList.ClearSelection()
            StartDate.Text = ""
            EndDate.Text = ""
            RegularExpressionValidator1.Text = ""
            RegularExpressionValidator2.Text = ""
            SearchModuleList.ClearSelection()
            SearchTerm.Text = ""

            ResultsLabel.Visible = False
            ErrorLabel.Visible = False
            ResultsGrid.Visible = False
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub SearchSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchSubmit.Click
        DoSearch()
    End Sub

    ' DoSearch sub validates values that are inputed and then sends them to 
    ' the archive search procedure wich returns a dataset that is binded to 
    ' a datagrid
    Private Sub DoSearch()

        'Validate if at least one search criteria is entered
        If UsernameList.SelectedItem.Text.Trim = "" And StartDate.Text.Trim.Length = 0 And EndDate.Text.Trim.Length = 0 And SearchModuleList.SelectedItem.Text.Trim = "" And SearchTerm.Text.Trim = "" Then
            ErrorLabel.Text = "Please enter at lease one criteria."
            ErrorLabel.Visible = True
        End If

        'Validate if both start date and end date present, if one of them is present
        If StartDate.Text.Trim.Length <> 0 Then
            If EndDate.Text.Trim.Length = 0 Then
                ErrorLabel.Text = "Please enter End Date."
                ErrorLabel.Visible = True
            End If
        End If

        If EndDate.Text.Trim.Length <> 0 Then
            If StartDate.Text.Trim.Length = 0 Then
                ErrorLabel.Text = "Please enter Start Date."
                ErrorLabel.Visible = True
            End If
        End If

        If ErrorLabel.Visible = True Then
            Return
        End If

        Dim OraParams() As OracleParameter = New OracleParameter(5) {}
        Dim ods As DataSet
        Dim odv As DataView

        Try
            'Dim RetVal As Integer

            OraParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
            OraParams(0).Value = ValidateValue(UsernameList.SelectedItem.Value)

            OraParams(1) = New OracleParameter("high_date_in", OracleDbType.Date)
            OraParams(1).Value = ValidateDate(StartDate.Text)

            OraParams(2) = New OracleParameter("low_date_in", OracleDbType.Date)
            OraParams(2).Value = ValidateDate(EndDate.Text)

            OraParams(3) = New OracleParameter("module_in", OracleDbType.Double)
            OraParams(3).Value = ValidateValue(SearchModuleList.SelectedItem.Value)

            OraParams(4) = New OracleParameter("search_term_in", OracleDbType.Varchar2)
            OraParams(4).Value = ValidateValue(SearchTerm.Text)

            OraParams(5) = New OracleParameter("archive_search_out", OracleDbType.RefCursor, ParameterDirection.Output)
            OraParams(5).Value = Nothing

            ods = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "archive_search_find", OraParams)
            odv = New DataView(ods.Tables(0))

            If odv.Count > 0 Then
                ResultsGrid.Visible = True
                ResultsLabel.Visible = True
                ResultsGrid.DataSource = ods
                ResultsGrid.DataBind()
            Else
                ErrorLabel.Text = "Your search did not find any matches, please try again."
                ErrorLabel.Visible = True
            End If

        Catch oe As OracleException
            ErrorLabel.Text = "There was an error: " + oe.Message
            ErrorLabel.Visible = True
        Catch ex As Exception
            ErrorLabel.Text = "There was an error: " + ex.Message
            ErrorLabel.Visible = True
        End Try

    End Sub

    ' ValidateValue takes in a string input and if that string is empty
    ' it will return Nothing type.
    '
    Private Function ValidateValue(ByVal inputvalue As String) As String

        If inputvalue = "" Then
            Return Nothing
        Else
            Return inputvalue
        End If

    End Function

    ' ValidateDate takes a string date and formats it for oracle
    ' consumption.
    '
    Private Function ValidateDate(ByVal Value As String) As String

        Dim time1 As Date

        If (String.Compare(Value, "", 0) = 0) Then
            Return Nothing
        End If

        time1 = CType(Value, Date)
        Return time1.ToString("dd-MMM-yyyy")

    End Function

    Private Sub AddBlankItem(ByVal DDL As DropDownList)

        Dim BlankItem As ListItem = New ListItem(" ", "")
        DDL.Items.Insert(0, BlankItem)

    End Sub

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

    ' ItemDataBound grabs the current datarow and reformats the first column
    ' to be a hyperlink to this page to setup the search. 
    '
    Private Sub ResultsGrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles ResultsGrid.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim RowCells As TableCellCollection = e.Item.Cells
            Dim HyperLinkCtl As HyperLink = New HyperLink()

            HyperLinkCtl.NavigateUrl = String.Format("ArchiveSearch.aspx?sr=ggg&srtrm={0}&srmod={1}&srsources={2}", RowCells(0).Text, Server.UrlEncode(RowCells(2).Text), Server.UrlEncode(RowCells(4).Text))
            HyperLinkCtl.Text = RowCells(0).Text

            RowCells(0).Controls.Add(HyperLinkCtl)

        End If

    End Sub

    ' ExecuteSearch will preform necessary process the search results and 
    ' will then execute the search procedure.
    '
    Private Sub ExecuteSearch()

        Dim SearchTerm As String = Request("srtrm")
        Dim SearchMods() As String = Request("srmod").Split(",")
        'Dim SearchSources As Hashtable = New Hashtable()
        Dim SearchSources As String = Request("srsources")
        Dim SearchModule As String

        For Each SearchModule In SearchMods
            Select Case SearchModule
                Case "PHONETIC"
                    Context.Items.Add("PhoneSearch", "True")
                    'SearchSources.Add("phonetic", "903")
                    Session("PhoneSearch") = True
                Case "TEXT"
                    Context.Items.Add("TextSearch", "True")
                    'SearchSources.Add("text", "904")
                    Session("TextSearch") = True
                Case "ORTHOGRAPHIC"
                    Context.Items.Add("OrthoSearch", "True")
                    'SearchSources.Add("orthographic", "902")
                    Session("OrthoSearch") = True
            End Select
        Next

        Context.Items.Add("SearchTerm", SearchTerm)
        Session("SearchTerm") = SearchTerm
        context.Items.Add("SearchSources", SearchSources)
        Context.Items.Add("FromArchive", "True")
        Server.Transfer("StartSearch.aspx")

    End Sub

    Private Function GenerateQueryString() As String

        Dim returnQuery As String = "search.aspx?term={0}&srchtype={1}&arch=1"
        Dim returnValues As ArrayList = New ArrayList(1)
        Dim queryTypes() As String = New String(2) {"", "", ""}

        returnValues.Add(Request("srtrm"))

        Dim SearchMods() As String = Request("srmod").Split(",")
        Dim SearchModule As String
        For Each SearchModule In SearchMods
            Select Case SearchModule
                Case "PHONETIC"
                    queryTypes(0) = "903"
                Case "TEXT"
                    queryTypes(1) = "904"
                Case "ORTHOGRAPHIC"
                    queryTypes(2) = "902"
            End Select
        Next

        returnValues.Add(String.Join("|", queryTypes))

        Return String.Format(returnQuery, returnValues.ToArray())

    End Function
End Class
