Imports OpenSmtp.Mail
Imports System.Net.Mail
Imports System.Configuration
Imports System.Text.RegularExpressions

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

        If txtRequestAccountName.Text.Trim.Length = 0 Then
            MessageLabel.Text = "Please enter your name!"
            MessageLabel.ForeColor = Color.Red
            MessageLabel.Visible = True
            Return
        End If

        If txtRequestAccountEmail.Text.Trim.Length = 0 Then
            MessageLabel.Text = "Please enter your email address!"
            MessageLabel.ForeColor = Color.Red
            MessageLabel.Visible = True
            Return
        End If

        Dim email As New Regex("([\w-+]+(?:\.[\w-+]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7})")
        If email.IsMatch(txtRequestAccountEmail.Text) = False Then
            MessageLabel.Text = "Email Address Format is not valid. Please try again."
            MessageLabel.ForeColor = Color.Red
            MessageLabel.Visible = True
            Return
        End If


        If Session("AppMode").ToString().Contains("PublicMode") And ConfigurationManager.AppSettings("EmailServerType").ToUpper().Contains("GMAIL") Then

            'Create an instance of the MailMessage class
            Dim MailMesg As New System.Net.Mail.MailMessage
            Dim SmtpClient As New System.Net.Mail.SmtpClient

            SmtpClient.Host = ConfigurationManager.AppSettings("EmailServer")
            SmtpClient.Port = ConfigurationManager.AppSettings("EmailPort")
            SmtpClient.Credentials = New System.Net.NetworkCredential(ConfigurationManager.AppSettings("EmailUsername"), ConfigurationManager.AppSettings("EmailUserpassword"))
            SmtpClient.EnableSsl = ConfigurationManager.AppSettings("EmailEnableSSL")

            'Setup the Email.
            Dim RegEmailAddress As String() = FDA.SiteSettings.GetSiteSettings("REGISTRATION_EMAIL").Split(";")
            Dim SplitEmailAddress As String

            MailMesg = New System.Net.Mail.MailMessage()
            MailMesg.From = New MailAddress(txtRequestAccountEmail.Text)
            For Each SplitEmailAddress In RegEmailAddress
                MailMesg.To.Add(New MailAddress(SplitEmailAddress.Trim()))
            Next

            'Set the subject
            MailMesg.Subject = "POCA System Account Request"

            'Set the body - use VbCrLf to insert a carriage return
            MailMesg.Body = txtRequestAccountName.Text & " is requesting an account for the POCA system. His email address is " & txtRequestAccountEmail.Text & "."

            'Now, to send the message, use the Send method of the SmtpMail class
            Try
                panelSendEmail.Visible = False
                MailSent.Visible = True
                SmtpClient.Send(MailMesg)
            Catch SmtpError As System.Net.Mail.SmtpException
                MailSent.Text = "There was an application error: " + SmtpError.Message
            End Try

        Else    'IntranetMode Or PublicMode with EmailServerType Custom

            'Create an instance of the MailMessage class
            Dim MailMesg As OpenSmtp.Mail.MailMessage
            Dim MailServer As New OpenSmtp.Mail.Smtp()

            MailServer.Host = ConfigurationManager.AppSettings("EmailServer")
            MailServer.Username = ConfigurationManager.AppSettings("EmailUsername")
            MailServer.Password = ConfigurationManager.AppSettings("EmailUserpassword")

            'Setup the Email.
            Dim RegEmailAddress As String() = FDA.SiteSettings.GetSiteSettings("REGISTRATION_EMAIL").Split(";")
            Dim SplitEmailAddress As String

            MailMesg = New OpenSmtp.Mail.MailMessage()
            MailMesg.From = New EmailAddress(txtRequestAccountEmail.Text)
            For Each SplitEmailAddress In RegEmailAddress
                MailMesg.To.Add(New EmailAddress(SplitEmailAddress.Trim()))
            Next

            'Set the subject
            MailMesg.Subject = "POCA System Account Request"

            'Set the body - use VbCrLf to insert a carriage return
            MailMesg.Body = txtRequestAccountName.Text & " is requesting an account for the POCA system. His email address is " & txtRequestAccountEmail.Text & "."

            'Now, to send the message, use the Send method of the SmtpMail class
            Try
                panelSendEmail.Visible = False
                MailSent.Visible = True
                MailServer.SendMail(MailMesg)
            Catch SmtpError As OpenSmtp.Mail.SmtpException
                MailSent.Text = "There was an application error: " + SmtpError.Message
            End Try
        End If

    End Sub
End Class
