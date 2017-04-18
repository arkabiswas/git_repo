Imports OpenSmtp.Mail
Imports System.Configuration
Imports CAM.PasswordGeneratorLibrary

Partial Class ResetPassword
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
        'Put user code to initialize the page here
    End Sub

    Private Sub btnImageButton_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnImageButton.Click

        'Create an instance of the MailMessage class
        Dim MailMesg As New OpenSmtp.Mail.MailMessage
        Dim MailServer As New OpenSmtp.Mail.Smtp()

        Dim PO As FDA.Person.PersonObject = FDA.Person.PersonObject.GetUserInformation("", txtResetAccountName.Text.Trim)

        Try

            If PO.UserID = "" Then
                'Setup the Email.
                Dim FeedBackEmailAddress As String() = FDA.SiteSettings.GetSiteSettings("FEEDBACK_EMAIL").Split(";")
                Throw New ApplicationException("The username given does not exist or has been disabled.  Please contact the <a href=""mailto:" & FeedBackEmailAddress(0).ToString() & """>System Administrator</a> for further assistance.")
            End If

            'The random password generator.
            Dim pwdGenerator As New RandomPasswordGenerator(PasswordOptions.AllCharacters)
            Dim RandomPassword = pwdGenerator.Generate(Integer.Parse(FDA.SiteSettings.GetSiteSettings("min_password_length")))

            MailServer.Host = ConfigurationManager.AppSettings("EmailServer")
            MailServer.Username = ConfigurationManager.AppSettings("EmailUsername")
            MailServer.Password = ConfigurationManager.AppSettings("EmailUserpassword")

            'Setup the Email.
            Dim RegEmailAddress As String() = FDA.SiteSettings.GetSiteSettings("REGISTRATION_EMAIL").Split(";")
            Dim SplitEmailAddress As String

            '#TODO: Update this will a dynamic email address.
            MailMesg.From = New OpenSmtp.Mail.EmailAddress(RegEmailAddress(0))
            MailMesg.To.Add(New OpenSmtp.Mail.EmailAddress(PO.EmailAddress))

            'Set the subject
            MailMesg.Subject = "POCA Account Information"

            'Set the body - use VbCrLf to insert a carriage return
            MailMesg.Body = "Here is your requested temporary password. You will need to change this after you sucessfully login." + Microsoft.VisualBasic.vbCrLf + Microsoft.VisualBasic.vbTab + "Temporary Password: " + RandomPassword
            'Now, to send the message, use the Send method of the SmtpMail class

            ' Let's try and reset the password.
            If PPC.FDA.SiteSettings.ResetPassword(PO.UserName, RandomPassword) Then
                MailServer.SendMail(MailMesg)
                MailSent.Text = "Your password has successfully been reset. You should receive an email in several minutes. If you do not receive an email within an hour please contact your system administrator."
                MailSent.CssClass = ""
            Else
                MailSent.Text = "Your password failed to be reset. Please try again or contact the Systems Administrator."
                MailSent.CssClass = "ErrorMessage"
            End If
            panelSendEmail.Visible = False

        Catch AppError As ApplicationException
            MailSent.Text = "There was an application error: " + AppError.Message
            MailSent.CssClass = "ErrorMessage"
        Catch SmtpError As SmtpException
            MailSent.Text = "There was an email error: " + SmtpError.Message
            MailSent.CssClass = "ErrorMessage"
        Finally
            MailSent.Visible = True
        End Try

    End Sub
End Class
