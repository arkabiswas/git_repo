Imports System.Configuration
Imports System.Data
Imports Oracle.DataAccess.Client
Imports System.Web.Security
Imports System.Security.Cryptography
Imports System.Text


Partial Class WebForm1
    Inherits System.Web.UI.Page
    Protected WithEvents lblErrors As System.Web.UI.WebControls.Label
    Protected WithEvents LinkReset As System.Web.UI.WebControls.LinkButton
    Protected WithEvents LinkChange As System.Web.UI.WebControls.LinkButton
    Protected WithEvents LinkRequest As System.Web.UI.WebControls.LinkButton

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

        'Clear Session variables
        If Not IsPostBack Then
            Session.RemoveAll()
        End If

        Dim appMode As String = ConfigurationManager.AppSettings("ApplicationMode")
        Session("AppMode") = appMode

        If appMode.Contains("IntranetMode") Then
            divInternalLogin.Visible = True
            divPublicLogin.Visible = False
        Else
            divInternalLogin.Visible = False
            divPublicLogin.Visible = True
        End If


        'Internal Login
        If appMode.Contains("IntranetMode") Then
            If Not IsPostBack Then
                Dim userName() As String = Split(User.Identity.Name, "\")
                Session("POCA_User") = userName(1).ToUpper
                'Session("POCA_User") = "ADMIN"

                Dim LoginPerson As New PPC.FDA.Person.PersonObject()

                If LoginPerson.User_Login(Session("POCA_User")) Then
                    Session("LoggedInUser") = LoginPerson
                    Session("SID") = Session.SessionID
                    Session("UserFullName") = LoginPerson.FullName()

                    reqAcct.Visible = False
                    lblMessage.Font.Bold = True
                    lblMessage.Font.Size = 10
                    lblMessage.Font.Italic = False
                    lblMessage.ForeColor = Color.Black
                    lblMessage.Text = "Welcome to the POCA System!" + Environment.NewLine + LoginPerson.FullName()
                    lblMessage.Text = lblMessage.Text.Replace(Environment.NewLine, "<br />")

                Else
                    lnkLogin.Visible = False
                    lblMessage.Text = PPC.FDA.Data.OracleDataFactory.CleanOracleError(LoginPerson.LastError)
                    If (lblMessage.Text.ToUpper().IndexOf("USER NOT FOUND") > -1) Then
                        lblMessage.Text = "Invalid User!"
                    ElseIf (lblMessage.Text.ToUpper().IndexOf("DISABLED") > -1) Then
                        Dim RegEmailAddress As String() = FDA.SiteSettings.GetSiteSettings("FEEDBACK_EMAIL").Split(";")
                        lblMessage.Text = "User is currently disabled. Please contact the <a href=""mailto:" & RegEmailAddress(0).ToString() + """>System Administrator</a>."
                        reqAcct.Visible = False
                    End If
                End If
            End If
        End If

        'Public login
        If appMode.Contains("PublicMode") Then

        End If

    End Sub

    Protected Sub lnkLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkLogin.Click
        Response.Redirect("Home.aspx",False)
    End Sub

    Private Sub login_user()

        Dim LoginPerson As New PPC.FDA.Person.PersonObject()
        Try

            If LoginPerson.User_Login_Public(txtLogin.Text, txtPassword.Text) Then
                FormsAuthentication.SetAuthCookie(txtLogin.Text, False)
                Session("LoggedInUser") = New PPC.FDA.Person.PersonObject
                Session("LoggedInUser") = LoginPerson
                Session("SID") = Session.SessionID
                Response.Redirect("Home.aspx")
            Else
                Dim errMessage As String
                errMessage = PPC.FDA.Data.OracleDataFactory.CleanOracleError(LoginPerson.LastError)

                If (errMessage.ToUpper().IndexOf("DISABLED") > -1) Then
                    'Setup the Email.
                    Dim RegEmailAddress As String() = FDA.SiteSettings.GetSiteSettings("FEEDBACK_EMAIL").Split(";")

                    lblLoginValidate.Text = errMessage + ". Please contact the <a href=""mailto:" & RegEmailAddress(0).ToString() + """>System Administrator</a>."
                Else
                    lblLoginValidate.Text = errMessage

                    If lblLoginValidate.Text.ToLower() = "password must be changed" Then Response.Redirect("ChangePassword.aspx?exp=mc&uid=" + txtLogin.Text)
                End If

                lblLoginValidate.Visible = True
            End If

        Catch ex As Exception
            ' There must have been some error. Throw it back to the client.
            Trace.Write("An error occured in LoginUser function: " + ex.Message + Environment.NewLine + ex.StackTrace)
            lblLoginValidate.Text = "There was an error that occured. Please try again."
            lblLoginValidate.Visible = True
        End Try

    End Sub

    Private Sub btnImageButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnImageButton.Click
        'validate login and password
        login_user()
    End Sub

   
End Class
