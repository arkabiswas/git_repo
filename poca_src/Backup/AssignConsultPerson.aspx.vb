' Oracle Namespaces
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

' Email Namespaces
Imports OpenSmtp.Mail

Partial Class AssignConsultPerson
    Inherits System.Web.UI.Page
    Protected WithEvents SelectedName As System.Web.UI.WebControls.Label
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
        'Put user code to initialize the page here
        PO = Session("LoggedInUser")

        If Not IsPostBack Then

            Dim OraParams() As OracleParameter = New OracleParameter(0) {}
            OraParams(0) = New OracleParameter("the_userlist", OracleDbType.RefCursor, ParameterDirection.Output)
            OraParams(0).Value = Nothing

            UsernameList.DataSource = FDA.Data.OracleDataFactory.ExecuteReader(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "userlist_get", OraParams)
            UsernameList.DataTextField = "u_username"
            UsernameList.DataValueField = "ui_user_uid"
            UsernameList.DataBind()

            ConsultAssignLabel.Text = ConsultAssignLabel.Text + Request("clt")

        End If

    End Sub

    Private Sub AssignButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AssignButton.Click
        DoAssign()
    End Sub

    ' DoAssign does all the processing for assign a consult to a user.
    '
    '
    Private Sub DoAssign()

        Dim OraParams() As OracleParameter = New OracleParameter(3) {}
        Dim RetVal As Integer

        Try
            OraParams(0) = New OracleParameter("assigner_in", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("username_in", OracleDbType.Varchar2)
            OraParams(1).Value = UsernameList.Items(UsernameList.SelectedIndex).Text

            OraParams(2) = New OracleParameter("consult_number_in", OracleDbType.Varchar2)
            OraParams(2).Value = Request("cid")

            OraParams(3) = New OracleParameter("product_name_in", OracleDbType.Varchar2)
            OraParams(3).Value = Request("clt")

            RetVal = FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "consult_assign", OraParams)

            If RetVal <> 0 Then
                SendEmail()
            Else
                ErrorLabel.Text = "There was an error. Please verify your selection and try again."
            End If

        Catch oe As OracleException
            ErrorLabel.Text = oe.Message
        End Try
    End Sub

    ' SendMail creates a new mail message that will be sent to 
    ' the assigner. 
    '
    Private Sub SendEmail()
        
        Dim MailMsg As MailMessage
        Dim MailServer As Smtp = PO.GetEmailServer()

        Try

            Dim AssignedPerson As FDA.Person.PersonObject = PO.GetUserInformation(UsernameList.SelectedItem.Value)
            Dim EmailAddressTo As New EmailAddress(AssignedPerson.EmailAddress)
            Dim EmailAddressFrom As New EmailAddress(PO.EmailAddress)

            MailMsg = New MailMessage(EmailAddressFrom, EmailAddressTo)

            MailMsg.Subject = "You have been assigned a consult."

            MailMsg.Body = String.Format("{0},", AssignedPerson.FullName)
            MailMsg.Body = MailMsg.Body + ControlChars.NewLine + ControlChars.NewLine
            MailMsg.Body = MailMsg.Body + "The following consult has been assigned to you for in-depth review:" + ControlChars.NewLine
            MailMsg.Body = MailMsg.Body + ControlChars.Tab + String.Format("Consult Name: {0}", Request("clt")) + ControlChars.NewLine
            MailMsg.Body = MailMsg.Body + ControlChars.Tab + String.Format("Consult ID:   {0}", Request("cid")) + ControlChars.NewLine
            MailMsg.Body = MailMsg.Body + ControlChars.NewLine + ControlChars.NewLine
            MailMsg.Body = MailMsg.Body + "Thank you." + ControlChars.NewLine + "Consult Administrator"

            MailServer.SendMail(MailMsg)

            ErrorLabel.Text = "An email has been successfully sent to " + AssignedPerson.UserName + "."

        Catch SmtpError As OpenSmtp.Mail.SmtpException
            ErrorLabel.Text = SmtpError.Message
        Catch GenError As Exception
            ErrorLabel.Text = GenError.Message
        End Try

    End Sub
End Class
