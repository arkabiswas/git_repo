Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions

Partial Class ChangePassword
    Inherits System.Web.UI.Page
    Private PO As PPC.FDA.Person.PersonObject

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

        If Session("LoggedInUser") Is Nothing Then
            lblReturnText.Text = "<a href='default.aspx' title='Return to Login Screen'>Return to Login Screen</a>"
            If Not Request.QueryString("uid") = String.Empty Then txtLogin.Text = Request.QueryString("uid")
            If Request.QueryString("exp") = "mc" Then lblMustChangeMessage.Visible = True
        Else
            lblReturnText.Text = "<a href='Home.aspx' title='Return Home'>Return Home</a>"
        End If

    End Sub

    'Public Function SQLEncode(ByVal value As String)
    '    SQLEncode = Replace(value, "'", "''")
    'End Function

    Private Function ChangePassword(ByVal username As String, ByVal old_password As String, ByVal new_password As String, ByVal userid As String) As Boolean

        If Page.IsValid Then

            Dim odr As OracleDataReader
            Dim arrOparams() As OracleParameter = New OracleParameter(3) {}
            Dim hdOldPassword As Byte()
            Dim hdNewPassword As Byte()
            Dim ValidUser As Boolean = False

            arrOparams(0) = New OracleParameter("USERNAME_IN", OracleDbType.Varchar2)
            arrOparams(0).Value = username

            arrOparams(1) = New OracleParameter("OLD_PASSWORD", OracleDbType.Raw)
            arrOparams(1).Value = PPC.FDA.Person.PersonObject.HashPasswordMD5(old_password)

            arrOparams(2) = New OracleParameter("NEW_PASSWORD", OracleDbType.Raw)
            arrOparams(2).Value = PPC.FDA.Person.PersonObject.HashPasswordMD5(new_password)

            arrOparams(3) = New OracleParameter("USERID_IN", OracleDbType.Varchar2)
            arrOparams(3).Value = Nothing

            Try
                odr = PPC.FDA.Data.OracleDataFactory.ExecuteReader(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "USER_CHANGE_PASSWORD", arrOparams)
                ValidUser = True

            Catch ex As OracleException
                lblLoginValidate.Text = PPC.FDA.Data.OracleDataFactory.CleanOracleError(ex.Message)
                lblLoginValidate.Visible = True
            End Try

            If ValidUser Then
                panelChangePassword.Visible = False
                If Session("SID") <> Session.SessionID Then
                    lblPasswordChanged.Text = "Your password has been changed. Please <A href='default.aspx' title='Click here to return to the login screen'>click here</A> to return to the login screen."
                Else
                    lblPasswordChanged.Text = "Your password has been changed. Please <A href='Home.aspx' title='Click here to return home'>click here</A> to return home."
                End If
                lblPasswordChanged.Visible = True
            End If

        End If

    End Function

    Private Sub btnImageButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnImageButton.Click
        'update user password if username and password match
        'Redirect to login page once password has been changed
        If ValidatePasswordRules(txtNewPassword.Text) Then
            ChangePassword(txtLogin.Text, txtOldPassword.Text, txtNewPassword.Text, "0")
        Else
            lblLoginValidate.Text = PasswordInValidMessage
            lblLoginValidate.Visible = True
        End If

    End Sub

    Private PasswordInValidMessage As String
    '''-----------------------------------------------------------------------------
    ''' <summary>
    '''  This procedure will validate the password rules from teh database
    ''' </summary>
    ''' <param name="PasswordToValidate">Password to Validate</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Jason Tucker] 	1/27/2005	Created
    ''' </history>
    '''-----------------------------------------------------------------------------
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
End Class
