Imports System.Web
Imports System.Web.SessionState
Imports System.Diagnostics
Imports System.Web.Security
Imports System.Net.Mail
Imports System.IO
Imports System.Configuration

Public Class [Global]
    Inherits System.Web.HttpApplication

#Region " Component Designer Generated Code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
    End Sub

#End Region

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        'On Error Resume Next

        Dim errObj As Exception = Server.GetLastError().InnerException()
        Dim LoginPerson As New PPC.FDA.Person.PersonObject()
        Dim errMessage(9) As String
        Dim CreateErrorLog As Boolean = ConfigurationManager.AppSettings("CreateErrorLog")
        Dim SendErrorEmail As Boolean = ConfigurationManager.AppSettings("SendErrorEmail")

        LoginPerson = Session("LoggedInUser")

        'If LoginPerson Is Nothing Or LoginPerson.UserID Is Nothing Then
        If LoginPerson Is Nothing Then
            Response.Redirect("default.aspx")
        Else
            'Return

            errMessage(0) = "<DATETIME>" & DateTime.Now.ToString() & "</DATETIME>"
            errMessage(1) = "<FULLNAME>" & LoginPerson.FullName & "</FULLNAME>"
            errMessage(2) = "<EMAIL><" & LoginPerson.EmailAddress & "></EMAIL>"
            errMessage(3) = "<GROUP>" & LoginPerson.UserGroup & "</GROUP>"
            errMessage(4) = "<USERNAME>" & LoginPerson.UserName & "</USERNAME>"
            errMessage(5) = "<SOURCE>" & errObj.Source.ToString() & "</SOURCE>"
            errMessage(6) = "<INNEREXCEPTION>" & Convert.ToString(errObj.InnerException) & "</INNEREXCEPTION>"
            errMessage(7) = "<ERRORMESSAGE>" & errObj.Message.ToString() & "</ERRORMESSAGE>"
            errMessage(8) = "<STACKTRACE>" & errObj.StackTrace.ToString() & "</STACKTRACE>"

            If (CreateErrorLog) Then WriteToLogFile(errMessage)
            If (SendErrorEmail) Then EmailError(errMessage)
            'Response.Redirect("default.aspx")

        End If

        'Server.ClearError()
        'FormsAuthentication.SignOut()
        'FormsAuthentication.RedirectToLoginPage() 

    End Sub

    Private Sub EmailError(ByVal errMessage As String())

        Dim Server As String = ConfigurationManager.AppSettings("EmailServer").ToString()
        Dim UserName As String = ConfigurationManager.AppSettings("EmailUsername").ToString()
        Dim Password As String = ConfigurationManager.AppSettings("EmailUserpassword").ToString()

        Dim fromEmail As String = ConfigurationManager.AppSettings("ErrorFromEmail").ToString()
        Dim toEmails As String() = ConfigurationManager.AppSettings("ErrorToEmail").ToString().Split(",")

        Dim mailMessage As New MailMessage()
        mailMessage.From = New MailAddress(fromEmail)

        For Each et As String In toEmails
            mailMessage.To.Add(et)
        Next

        mailMessage.IsBodyHtml = False
        mailMessage.Subject = "POCA HAS GENERATED AN ERROR !!"

        For Each item As String In errMessage
            mailMessage.Body += item & Chr(13) & Chr(10)
        Next

        'mailMessage.Priority = MailPriority.High

        Dim sc As SmtpClient = New SmtpClient(Server)
        sc.Credentials = New System.Net.NetworkCredential(UserName, Password)
        If ConfigurationManager.AppSettings("EmailServerType").ToUpper().Contains("GMAIL") Then
            sc.Port = ConfigurationManager.AppSettings("EmailPort")
            sc.EnableSsl = ConfigurationManager.AppSettings("EmailEnableSSL")
        End If
        sc.Send(mailMessage)

    End Sub

    Private Sub WriteToLogFile(ByVal errMessage As String())
        Dim ErrorLogPath As String = ConfigurationManager.AppSettings("ErrorLogPath").ToString()
        Dim LogFilePath As String = Server.MapPath(ErrorLogPath)
        '****
        'Dim LogArray(0) As String
        'LogArray.SetValue(LogFilePath, 0)
        'EmailError(LogArray)
        '****
        Dim SW As StreamWriter
        Dim FI As FileInfo = New FileInfo(LogFilePath)

        If (FI.Exists AndAlso FI.Length > 50000) Then
            Dim currentTime As System.DateTime = System.DateTime.Now
            Dim todayDateStr As String = "_" + currentTime.Year.ToString() + "_" + currentTime.Month.ToString() + "_" + currentTime.Day.ToString()
            Dim BackupErrorLogPath As String = ErrorLogPath.Remove(ErrorLogPath.IndexOf(".txt"), 4).ToUpper() + todayDateStr + ".txt"
            Dim BackupLogFilePath As String = Server.MapPath(BackupErrorLogPath)
            FI.CopyTo(BackupLogFilePath)
            SW = New StreamWriter(LogFilePath, False)
        Else
            SW = New StreamWriter(LogFilePath, True)
        End If

        SW.WriteLine("")
        For Each errLine As String In errMessage
            SW.WriteLine(errLine)
        Next
        SW.Close()

    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

End Class
