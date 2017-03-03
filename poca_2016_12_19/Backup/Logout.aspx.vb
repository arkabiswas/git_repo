Partial Class Logout
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

        lblMessage.Font.Bold = True
        lblMessage.Font.Size = 10
        lblMessage.Font.Italic = False
        lblMessage.ForeColor = Color.Black 

        lblMessage.Text = "Thank you for using the POCA System!" + Environment.NewLine + Session("UserFullName") + ", " + "your session has ended. Select Begin POCA Session to begin a new session."
        lblMessage.Text = lblMessage.Text.Replace(Environment.NewLine, "<br />")

        Try
            Dim PO As PPC.FDA.Person.PersonObject = Session("LoggedInUser")
            PO.User_Logout()
        Catch ex As Exception
            ' the session probably doesn't exist anymore. 
        Finally
            'System.Web.Security.FormsAuthentication.SignOut()
            Session.Clear() 
        End Try
    End Sub

    Protected Sub lnkLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkLogin.Click
        Response.Redirect("Home.aspx?id=BeginSession")
    End Sub
End Class
