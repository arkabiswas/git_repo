Imports OpenSmtp.Mail

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
        'Create an instance of the MailMessage class
        Dim MailMesg As MailMessage
        Dim SmtpServer As Smtp = PO.GetEmailServer()

        'Setup the Email.
        Dim RegEmailAddress As String() = FDA.SiteSettings.GetSiteSettings("FEEDBACK_EMAIL").Split(";")
        Dim SplitEmailAddress As String

        MailMesg = New MailMessage()
        MailMesg.From = New EmailAddress(PO.EmailAddress)
        For Each SplitEmailAddress In RegEmailAddress
            MailMesg.To.Add(New EmailAddress(SplitEmailAddress.Trim()))
        Next


        'Set the subject
        MailMesg.Subject = "FDA System Comment Form"

        'Set the body - use VbCrLf to insert a carriage return
        MailMesg.Body = Me.txtComments.Text

        'Now, to send the message, use the Send method of the SmtpMail class
        Try
            panelEnterComments.Visible = False
            panelCommentsSent.Visible = True
            SmtpServer.SendMail(MailMesg)
        Catch SmtpError As SmtpException
            CommentsSent.Text = "There was an application error: " + SmtpError.Message
        End Try

    End Sub
End Class
