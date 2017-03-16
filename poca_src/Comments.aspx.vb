Imports OpenSmtp.Mail
Imports System.Net.Mail

Partial Class Comments
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
        PO = Session("LoggedInUser")
        lblFrom.Text = PO.FullName

        If ConfigurationManager.AppSettings("ForceErrorMessage") Then
            Throw New Exception("*** DEBUG TESTING ERROR ***")
        End If
    End Sub

    Private Sub btnImageButton_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnImageButton.Click

        If txtComments.Text.Trim = "" Then
            MessageLabel.Text = "Please enter some comments."
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
            SmtpClient.Credentials = New System.Net.NetworkCredential(Convert.ToString(ConfigurationManager.AppSettings("EmailUsername")), Convert.ToString(ConfigurationManager.AppSettings("EmailUserpassword")))
            SmtpClient.EnableSsl = ConfigurationManager.AppSettings("EmailEnableSSL")

            'Setup the Email.
            Dim RegEmailAddress As String() = FDA.SiteSettings.GetSiteSettings("FEEDBACK_EMAIL").Split(";")
            Dim SplitEmailAddress As String

            MailMesg = New System.Net.Mail.MailMessage()
            MailMesg.From = New MailAddress(PO.EmailAddress)
            For Each SplitEmailAddress In RegEmailAddress
                MailMesg.To.Add(New MailAddress(SplitEmailAddress.Trim()))
            Next

            'Set the subject
            MailMesg.Subject = "POCA System Comment Form"

            'Set the body - use VbCrLf to insert a carriage return
            MailMesg.Body = "POCA user " + PO.FullName + " (" + PO.EmailAddress + ") sent following comments:" + Microsoft.VisualBasic.vbCrLf + Microsoft.VisualBasic.vbCrLf + Me.txtComments.Text

            'Now, to send the message, use the Send method of the SmtpMail class
            Try
                panelEnterComments.Visible = False
                panelCommentsSent.Visible = True
                SmtpClient.Send(MailMesg)
            Catch SmtpError As System.Net.Mail.SmtpException
                CommentsSent.Text = "There was an application error: " + SmtpError.Message
            End Try

        Else    'IntranetMode Or PublicMode with EmailServerType Custom

            'Create an instance of the MailMessage class
            Dim MailMesg As OpenSmtp.Mail.MailMessage
            Dim SmtpServer As OpenSmtp.Mail.Smtp = PO.GetEmailServer()

            'Setup the Email.
            Dim RegEmailAddress As String() = FDA.SiteSettings.GetSiteSettings("FEEDBACK_EMAIL").Split(";")
            Dim SplitEmailAddress As String

            MailMesg = New OpenSmtp.Mail.MailMessage()
            MailMesg.From = New EmailAddress(PO.EmailAddress)
            For Each SplitEmailAddress In RegEmailAddress
                MailMesg.To.Add(New EmailAddress(SplitEmailAddress.Trim()))
            Next

            'Set the subject
            MailMesg.Subject = "POCA System Comment Form"

            'Set the body - use VbCrLf to insert a carriage return
            MailMesg.Body = "POCA user " + PO.FullName + " (" + PO.EmailAddress + ") sent following comments:" + Microsoft.VisualBasic.vbCrLf + Microsoft.VisualBasic.vbCrLf + Me.txtComments.Text

            'Now, to send the message, use the Send method of the SmtpMail class
            Try
                panelEnterComments.Visible = False
                panelCommentsSent.Visible = True
                SmtpServer.SendMail(MailMesg)
            Catch SmtpError As OpenSmtp.Mail.SmtpException
                CommentsSent.Text = "There was an application error: " + SmtpError.Message
            End Try

        End If

    End Sub
End Class
