Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports CAM.PasswordGeneratorLibrary

Partial Class UserAdmin
    Inherits System.Web.UI.Page
    Protected PO As PPC.FDA.Person.PersonObject

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
        If Not Page.IsPostBack Then
            LoadDropDownLists()
        End If

    End Sub

    Private Sub LoadDropDownLists()

        Usergrouplist.DataSource = DDLDataBind("User_Group")
        Usergrouplist.DataTextField = "Description"
        Usergrouplist.DataValueField = "List_Item"
        Usergrouplist.DataBind()

        Dim odr As OracleDataReader
        odr = PPC.FDA.Data.OracleDataFactory.ExecuteReader(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "userlist_get", New OracleParameter("the_userlist", OracleDbType.RefCursor, ParameterDirection.Output))

        UserList.Items.Clear()
        While odr.Read
            Dim litem As New ListItem()
            litem.Text = odr.GetString(4)
            litem.Value = odr.GetString(0)
            UserList.Items.Add(litem)
        End While

        Dim odr2 As OracleDataReader
        odr2 = PPC.FDA.Data.OracleDataFactory.ExecuteReader(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "userlist_get_disabled", New OracleParameter("the_userlist", OracleDbType.RefCursor, ParameterDirection.Output))

        DisabledUserList.Items.Clear()
        While odr2.Read
            Dim litem As New ListItem
            litem.Text = odr2.GetString(4)
            litem.Value = odr2.GetString(0)
            DisabledUserList.Items.Add(litem)
        End While

    End Sub

#Region "DDLDataBind"
    Private Function DDLDataBind(ByVal ListName As String) As DataView
        Dim OraParams() As OracleParameter = New OracleParameter(2) {}
        Dim ods As DataSet
        Try

            OraParams(0) = New OracleParameter("USERNAME_IN", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("listname_in", OracleDbType.Varchar2)
            OraParams(1).Value = ListName

            OraParams(2) = New OracleParameter("pick_list", OracleDbType.RefCursor, ParameterDirection.Output)
            OraParams(2).Value = Nothing

            ods = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "pick_list_get", OraParams)
            Return New DataView(ods.Tables(0))

        Catch oe As OracleException
            ErrorLabel.Text = "Error: " + oe.Message
            ErrorLabel.Visible = True

        Catch ex As Exception
            ErrorLabel.Text = "Error: " + ex.Message
            ErrorLabel.Visible = True

        End Try
    End Function
#End Region

    Private PasswordInValidMessage As String
    Private Function ValidatePasswordRules(ByVal PasswordToValidate As String) As Boolean

        Dim ThisSite As FDA.SiteSettings

        With ThisSite
            If PasswordToValidate.Length < Int32.Parse(.GetSiteSettings("min_password_length")) Then
                PasswordInValidMessage = String.Format("Your new password must be {0} characters long.", .GetSiteSettings("min_password_length"))
                Return False
            End If

            If Not System.Text.RegularExpressions.Regex.Matches(PasswordToValidate, "[!@#$%&*]").Count >= Int32.Parse(.GetSiteSettings("password_special_char")) Then
                PasswordInValidMessage = String.Format("Your new password must contain {0} special characters.", .GetSiteSettings("password_special_char"))
                Return False
            End If
        End With

        Return True

    End Function

    Private Sub AddUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AddUser.Click

        ErrorLabel.ForeColor = Color.Red
        ErrorLabel.Visible = True

        'If ValidatePasswordRules(Password.Text) Then

        Dim OraParams() As OracleParameter = New OracleParameter(7) {}
        Dim ods As DataSet
        Dim RetVal As Integer
        '    Dim iSuccess As Integer


        Try
            OraParams(0) = New OracleParameter("USERID_IN", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserID

            OraParams(1) = New OracleParameter("username_in", OracleDbType.Varchar2)
            OraParams(1).Value = Username.Text

            OraParams(2) = New OracleParameter("firstname_in", OracleDbType.Varchar2)
            OraParams(2).Value = Firstname.Text

            OraParams(3) = New OracleParameter("lastname_in", OracleDbType.Varchar2)
            OraParams(3).Value = Lastname.Text

            OraParams(4) = New OracleParameter("email_in", OracleDbType.Varchar2)
            OraParams(4).Value = Email.Text

            OraParams(5) = New OracleParameter("group_in", OracleDbType.Varchar2)
            OraParams(5).Value = Usergrouplist.SelectedItem.Value

            OraParams(6) = New OracleParameter("password_in", OracleDbType.Raw)
            OraParams(6).Value = PO.HashPasswordMD5("none") 'NOT NEEDED AS SSO IS IMPLEMENTED

            OraParams(7) = New OracleParameter("returnVal_out", OracleDbType.Double, ParameterDirection.Output)
            OraParams(7).Value = Nothing

            RetVal = PPC.FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "user_add", OraParams)

            If OraParams(7).Value > 0 Then
                ErrorLabel.Text = "Add Successful."
                LoadDropDownLists()
            Else
                ErrorLabel.Text = "Please check the values you entered and then re-submit."
            End If

        Catch oe As OracleException
            ErrorLabel.Text = PPC.FDA.Data.OracleDataFactory.CleanOracleError(oe.Message)
        End Try

        'Else
        'ErrorLabel.Text = PasswordInValidMessage
        'End If

    End Sub

    'Private Sub ResetPwd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResetPwd.Click
    '    ResetPassClick(UserList)
    'End Sub

    'Private Sub ResetDisabledPwd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResetDisabledPwd.Click
    '    ResetPassClick(DisabledUserList)
    'End Sub

    Private Sub EditUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditUser.Click
        EditUserClick(UserList)
    End Sub

    Private Sub EditDisabledUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditDisabledUser.Click
        EditUserClick(DisabledUserList)
    End Sub

    Private Sub EditUserClick(ByVal list As System.Web.UI.WebControls.DropDownList)
        Dim Url As String
        Url = "UserEdit.aspx?userid=" + list.SelectedItem.Value
        Response.Redirect(Url)
    End Sub

    Private Sub ResetPassClick(ByVal list As System.Web.UI.WebControls.DropDownList)
        'The random password generator.
        Dim pwdGenerator As New RandomPasswordGenerator(PasswordOptions.AllCharacters)
        Dim RandomPassword = pwdGenerator.Generate(Integer.Parse(FDA.SiteSettings.GetSiteSettings("min_password_length")))

        'Create an instance of the MailMessage class
        Dim MailMesg As New OpenSmtp.Mail.MailMessage
        Dim MailServer As New OpenSmtp.Mail.Smtp

        Dim OtherUser As FDA.Person.PersonObject = FDA.Person.PersonObject.GetUserInformation("", list.SelectedItem.Text)

        MailServer.Host = ConfigurationManager.AppSettings("EmailServer")
        MailServer.Username = ConfigurationManager.AppSettings("EmailUsername")
        MailServer.Password = ConfigurationManager.AppSettings("EmailUserpassword")

        'Setup the Email.
        Dim RegEmailAddress As String() = FDA.SiteSettings.GetSiteSettings("REGISTRATION_EMAIL").Split(";")
        MailMesg.From = New OpenSmtp.Mail.EmailAddress(RegEmailAddress(0).ToString())
        MailMesg.To.Add(New OpenSmtp.Mail.EmailAddress(OtherUser.EmailAddress))

        'Set the subject
        MailMesg.Subject = "POCA System Temporary Password."

        'Set the body - use VbCrLf to insert a carriage return
        MailMesg.Body = "Here is your requested temporary password. You will need to change this after you sucessfully login." + Microsoft.VisualBasic.vbCrLf + Microsoft.VisualBasic.vbTab + "Temporary Password: " + RandomPassword

        ' Make sure the message Label is visible.
        ErrorLabel.ForeColor = Color.Red
        ErrorLabel.Visible = True

        Try
            If PPC.FDA.SiteSettings.ResetPassword(OtherUser.UserName, RandomPassword) Then
                MailServer.SendMail(MailMesg)
                ErrorLabel.Text = "A temporary password has been sent to the email address that that was provided for this account."
            Else
                ErrorLabel.Text = "Your password failed to be reset. Please try again or contact the Systems Administrator."
            End If
        Catch SmtpError As OpenSmtp.Mail.SmtpException
            ErrorLabel.Text = "There was an error sending the email: " + SmtpError.Message
        Catch OracleError As OracleException
            ErrorLabel.Text = PPC.FDA.Data.OracleDataFactory.CleanOracleError(OracleError.Message)
        Catch ex As Exception
            ErrorLabel.Text = "There was a general error. Please try again."
        End Try
    End Sub
End Class
