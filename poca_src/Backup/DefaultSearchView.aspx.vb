Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Partial Class DefaultSearchView
    Inherits System.Web.UI.Page
    Protected PO As New PPC.FDA.Person.PersonObject()

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

    Private Sub User_GetSearch()
        Dim SelectedValue As String
        PO = Session("LoggedInUser")

        Try
            SelectedValue = PO.GetSettingValue("SEARCH_VIEW")
            If SelectedValue.ToUpper() = "SIMPLE" Then
                SimpleSearch.Checked = True
                AdvancedView.Checked = False
            Else
                SimpleSearch.Checked = False
                AdvancedView.Checked = True
            End If

        Catch oe As OracleException
            ' We won't do anything, since we will be updating the threshold
            ' anyway.
            MessageLabel.Text = "An error has occured. Please check your input and try again."
            MessageLabel.Visible = True
            MessageLabel.ForeColor = Color.Red
        End Try

    End Sub

    Private Sub User_SetSearch()
        Dim SearchType As String
        PO = Session("LoggedInUser")

        If SimpleSearch.Checked Then
            SearchType = "Simple"
        Else
            SearchType = "Advanced"
        End If

        Try
            PO.ModifySettingValue("SEARCH_VIEW", SearchType, OracleDbType.Varchar2)
            MessageLabel.Text = "Your setting has been saved."
            MessageLabel.ForeColor = Color.Red
            MessageLabel.Visible = True
        Catch ex As Exception
            ' Whoops there is an error catch and write out the message.
            MessageLabel.Text = "An error has occured. Please check your input and try again."
            MessageLabel.Visible = True
            MessageLabel.ForeColor = Color.Red
        Catch oe As OracleException
            ' Whoops there is an error catch and write out the message.
            MessageLabel.Text = "An error has occured. Please check your input and try again."
            MessageLabel.Visible = True
            MessageLabel.ForeColor = Color.Red
        End Try


    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            User_GetSearch()
        End If

    End Sub

    Private Sub btnImageButton_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnImageButton.Click
        'insert code to save default search view here
        User_SetSearch()
    End Sub

End Class
