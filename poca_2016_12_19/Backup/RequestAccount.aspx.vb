Imports OpenSmtp.Mail
Imports System.Configuration

Partial Class RequestAccount
    Inherits System.Web.UI.Page
    Protected WithEvents txtResetAccountName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtResetAccountEmail As System.Web.UI.WebControls.TextBox

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
        Dim MailMesg As MailMessage
        Dim MailServer As New OpenSmtp.Mail.Smtp()

        MailServer.Host = ConfigurationManager.AppSettings("EmailServer")
        MailServer.Username = ConfigurationManager.AppSettings("EmailUsername")
        MailServer.Password = ConfigurationManager.AppSettings("EmailUserpassword")


        'Setup the Email.
        Dim RegEmailAddress As String() = FDA.SiteSettings.GetSiteSettings("REGISTRATION_EMAIL").Split(";")
        Dim SplitEmailAddress As String

        MailMesg = New MailMessage()
        MailMesg.From = New EmailAddress(txtRequestAccountEmail.Text)
        For Each SplitEmailAddress In RegEmailAddress
            MailMesg.To.Add(New EmailAddress(SplitEmailAddress.Trim()))
        Next

        'Set the subject
        MailMesg.Subject = "FDA System Account Request"

        'Set the body - use VbCrLf to insert a carriage return
        MailMesg.Body = txtRequestAccountName.Text & " is requesting an account for the FDA system. "

        'Now, to send the message, use the Send method of the SmtpMail class
        Try
            panelSendEmail.Visible = False
            MailSent.Visible = True
            MailServer.SendMail(MailMesg)
        Catch SmtpError As SmtpException
            MailSent.Text = "There was an application error: " + SmtpError.Message
        End Try

    End Sub
End Class
