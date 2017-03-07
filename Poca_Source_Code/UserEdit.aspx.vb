Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Partial Class UserEdit
    Inherits System.Web.UI.Page
    Protected WithEvents CompareValidator1 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents RequiredFieldValidator4 As System.Web.UI.WebControls.RequiredFieldValidator
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
        'Put user code to initialize the page here
        PO = Session("LoggedInUser")
        If Not Page.IsPostBack Then
            If Request.QueryString("userid") <> "" Then
                LoadEditValues(Request.QueryString("userid"))
                ViewState.Add("_UserId", Request.QueryString("userid"))
            Else
                ErrorLabel.Text = "You must select a user first."
            End If
        End If

    End Sub

    Private Sub LoadEditValues(ByVal userid As String)

        Dim EditPerson As New PPC.FDA.BusinessObjects.Person()
        EditPerson = RetrieveUser(userid)

        Usergrouplist.DataSource = DDLDataBind("User_Group")
        Usergrouplist.DataTextField = "Description"
        Usergrouplist.DataValueField = "List_Item"
        Usergrouplist.DataBind()

        If Not EditPerson Is Nothing Then
            Username.Text = EditPerson.UserName
            Firstname.Text = EditPerson.FirstName
            Lastname.Text = EditPerson.LastName
            Email.Text = EditPerson.EmailAddress
            Usergrouplist.Items.FindByValue(EditPerson.UserGroup).Selected = True
        End If

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

    '   Retrieve User will call out to a stored procedure and then create a person object 
    '   that gets returned back to the calling procedure.
    '
    Private Function RetrieveUser(ByVal userid As String) As PPC.FDA.BusinessObjects.Person

        Dim EditPerson As New PPC.FDA.BusinessObjects.Person()
        Dim odr As OracleDataReader
        Dim dr As DataRow

        Try
            odr = PPC.FDA.Data.OracleDataFactory.ExecuteReader(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "userlist_get_all", New OracleParameter("the_userlist", OracleDbType.RefCursor, ParameterDirection.Output))

            While odr.Read
                If odr.GetString(0).Equals(userid) Then
                    EditPerson.EmailAddress = odr("u_email")
                    EditPerson.FirstName = odr("u_first_name")
                    EditPerson.LastName = odr("u_last_name")
                    EditPerson.UserGroup = odr("u_user_group_name")
                    EditPerson.UserName = odr("u_username")
                    EditPerson.UserID = odr.GetString(0)

                    If odr("enabled") = 1 Then
                        EnableUser.Visible = False
                        DisableUser.Visible = True
                    Else
                        EnableUser.Visible = True
                        DisableUser.Visible = False
                    End If

                    Exit While
                End If
            End While

        Catch ex As Exception
            ErrorLabel.Text = "Error Finding User"
        End Try

        Return EditPerson
    End Function

    Private Sub UpdateUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UpdateUser.Click
        DoUserUpdate()
    End Sub

    Private Sub DoUserUpdate()

        Dim OraParams() As OracleParameter = New OracleParameter(5) {}
        Dim retval As Integer

        Try
            OraParams(0) = New OracleParameter("userid_in", OracleDbType.Varchar2)
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

            retval = PPC.FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "user_update_admin", OraParams)

            If retval <> 0 Then
                ErrorLabel.Text = "Update Sucessful."
            Else
                ErrorLabel.Text = "There was an error updating this user."
            End If

        Catch ex As Exception
            ErrorLabel.Text = "There was an error updating this user."
        End Try
    End Sub

    ' DoDisableUser will take the username and the logged in user's id
    ' then call the delete procedure and and disable that user.
    Private Sub DoDisableUser()

        Dim OraParams() As OracleParameter = New OracleParameter(1) {}
        Dim retval As Integer

        Try
            OraParams(0) = New OracleParameter("userid_in", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserID

            OraParams(1) = New OracleParameter("username_in", OracleDbType.Varchar2)
            OraParams(1).Value = Username.Text

            retval = PPC.FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "user_delete", OraParams)

            If retval <> 0 Then
                ErrorLabel.Text = "User Sucessfully Disabled."
            Else
                ErrorLabel.Text = "There was an error updating this user."
            End If

        Catch OracleError As OracleException
            ErrorLabel.Text = FDA.Data.OracleDataFactory.CleanOracleError(OracleError.Message)
        Catch ex As Exception
            ErrorLabel.Text = "There was an error updating this user."
        End Try

    End Sub


    Private Sub DoEnableUser()

        Dim OraParams() As OracleParameter = New OracleParameter(1) {}
        Dim retval As Integer

        Try
            OraParams(0) = New OracleParameter("userid_in", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserID

            OraParams(1) = New OracleParameter("username_in", OracleDbType.Varchar2)
            OraParams(1).Value = Username.Text

            retval = PPC.FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "user_enable", OraParams)

            If retval <> 0 Then
                ErrorLabel.Text = "User Sucessfully Enabled."
            Else
                ErrorLabel.Text = "There was an error updating this user."
            End If

        Catch OracleError As OracleException
            ErrorLabel.Text = FDA.Data.OracleDataFactory.CleanOracleError(OracleError.Message)
        Catch ex As Exception
            ErrorLabel.Text = "There was an error updating this user."
        End Try

    End Sub


    Private Sub Disable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisableUser.Click
        DoDisableUser()
        EnableUser.Visible = True
        DisableUser.Visible = False
    End Sub

    Private Sub EnableUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnableUser.Click
        DoEnableUser()
        EnableUser.Visible = False
        DisableUser.Visible = True
    End Sub
End Class
