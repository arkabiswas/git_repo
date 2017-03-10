Partial Class PasswordPolicies
    Inherits System.Web.UI.Page
    Private PO As FDA.Person.PersonObject

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

        ' Setup the PersonObject that contains the currently 
        ' logged in user.
        PO = Session("LoggedInUser")

        ' If this is the first time that page is loaded then 
        ' we will need to get the current settings.
        If Not Page.IsPostBack Then
            With PO
                pwdHistoryLength.Text = .GetSettingValue("retain_password_history")
                pwdExpireDays.Text = .GetSettingValue("max_password_age")
                pwdMinExpireDays.Text = .GetSettingValue("min_password_age")
                pwdMinimumLength.Text = .GetSettingValue("min_password_length")
                pwdSpecialCharacters.Text = .GetSettingValue("password_special_char")
                pwdLoginAttempts.Text = .GetSettingValue("max_login_attempts")
            End With
        End If

    End Sub

    Private Sub btnImageButton_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnImageButton.Click

        If Page.IsValid Then
            Try
                With PO
                    .ModifySettingValue("retain_password_history", pwdHistoryLength.Text, Oracle.DataAccess.Client.OracleDbType.Double, 1)
                    .ModifySettingValue("max_password_age", pwdExpireDays.Text, Oracle.DataAccess.Client.OracleDbType.Double, 1)
                    .ModifySettingValue("min_password_age", pwdMinExpireDays.Text, Oracle.DataAccess.Client.OracleDbType.Double, 1)
                    .ModifySettingValue("min_password_length", pwdMinimumLength.Text, Oracle.DataAccess.Client.OracleDbType.Double, 1)
                    .ModifySettingValue("password_special_char", pwdSpecialCharacters.Text, Oracle.DataAccess.Client.OracleDbType.Double, 1)
                    .ModifySettingValue("max_login_attempts", pwdLoginAttempts.Text, Oracle.DataAccess.Client.OracleDbType.Double, 1)
                End With
                lblErrorMessages.Text = "Password policies have been updated sucessfully."
                lblErrorMessages.Visible = True
            Catch UpdateError As Exception
                lblErrorMessages.Text = "There was an error trying to update the password policies. Please try again."
                lblErrorMessages.Visible = True
            End Try
        End If
    End Sub
End Class
