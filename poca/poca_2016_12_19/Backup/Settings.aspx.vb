Partial Class Settings
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim CurrentUser As PPC.FDA.Person.PersonObject = Session("LoggedInUser")
        If CurrentUser.UserGroup.ToUpper() = "ADMINISTRATORS" Or CurrentUser.UserGroup.ToUpper() = "CONSULT COORDINATORS" Then
            advancedSettingsPanel.Visible = True
            If CurrentUser.UserGroup.ToUpper() = "ADMINISTRATORS" Then
                ' Show the Add/Edit user link.
                ' Show the Modify Level of Concern link.
                FindControl("userAdminLink").Visible = True
                'FindControl("changePswrdPolicy").Visible = True
                adminList.Visible = True
            Else
                adminList.Visible = False
            End If
        End If
    End Sub

End Class
