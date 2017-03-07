Imports System.Threading

Partial Class StartSearch
    Inherits System.Web.UI.Page
    Private po As PPC.FDA.Person.PersonObject

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

        po = Session("LoggedInUser")

        If Context.Items.Item("SearchTerm") <> Nothing Then

            Try
                Dim searchBox As SearchBox = FindControl("StartSearchBox")

                searchBox.SearchTerm = Context.Items.Item("SearchTerm")
                searchBox.IsOrthoChecked = Context.Items.Item("OrthoSearch")
                searchBox.IsPhonChecked = Context.Items.Item("PhoneSearch")
                'searchBox.IsSpellingChecked = Context.Items.Item("SpellingSearch")
                searchBox.IsTextChecked = Context.Items.Item("TextSearch")
                searchBox.SearchSources = Context.Items.Item("SearchSources")

                searchBox.PO = CType(Session("LoggedInUser"), PPC.FDA.Person.PersonObject)
                searchBox.GenerateSearchEngine()

                ' Create the thread that the search will sit on.
                '                Dim thread1 As New Thread(New ThreadStart(AddressOf searchBox.GenerateSearchEngine))
                '               thread1.Priority = ThreadPriority.Lowest
                '              thread1.Start()

                Response.Redirect("search.aspx")
            Catch ThreadExiting As ThreadAbortException
                ' The thread has exited, which probably means that something has gone wrong. 
                Trace.Write("Thread Abortion Exception: " + ThreadExiting.Message)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End If


        'If search has not yet been viewed - check the users preference
        'If Session("AdvancedSearch") = Nothing Then
        'If po.GetSettingValue("SEARCH_VIEW").ToUpper.Equals("ADVANCED") Then
        Session("AdvancedSearch") = "True"
        'Else
        'Session("AdvancedSearch") = "False"
        'End If
        'End If

        'CreateBox()

    End Sub

    'Private Sub ToggleSearchType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToggleSearchType.Click

    '    If Session("AdvancedSearch") = "False" Then
    '        Session("AdvancedSearch") = "True"
    '    Else
    '        Session("AdvancedSearch") = "False"
    '    End If

    '    Server.Transfer("StartSearch.aspx")
    'End Sub

    Private Sub CreateBox()

        Dim searchBox As SearchBox = FindControl("StartSearchBox")

        Try
            If Session("AdvancedSearch") = "True" Then
                'ToggleSearchType.Text = "Switch to simple search view"
            End If
        Catch SettingsError As ApplicationException
            Dim errorLabel As New Label
            errorLabel.Text = SettingsError.Message
            errorLabel.CssClass = "errorMessage"
            Page.Controls.Add(errorLabel)
        End Try

    End Sub
End Class
