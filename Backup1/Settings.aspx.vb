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
        If CurrentUser Is Nothing Then
            Response.Redirect("default.aspx")
        Else

            If Session("AppMode").ToString().Contains("PublicMode") Then
                lnkChangePassword.Visible = True
                If CurrentUser.UserGroup.ToUpper().Contains("ADMINISTRATORS") Then
                    lnkChangePswrdPolicy.Visible = True
                Else
                    lnkChangePswrdPolicy.Visible = False
                End If
            Else
                lnkChangePassword.Visible = False
                lnkChangePswrdPolicy.Visible = False
            End If

            If CurrentUser.UserGroup.ToUpper().Contains("ADMINISTRATORS") Or CurrentUser.UserGroup.ToUpper() = "CONSULT COORDINATORS" Then
                advancedSettingsPanel.Visible = True
                If CurrentUser.UserGroup.ToUpper().Contains("ADMINISTRATORS") Then
                    ' Show the Add/Edit user link.
                    ' Show the Modify Level of Concern link.
                    FindControl("userAdminLink").Visible = True
                    'FindControl("changePswrdPolicy").Visible = True
                    adminList.Visible = True

                    If CurrentUser.UserGroup.ToUpper().Contains("BUSINESS ADMINISTRATORS") Then
                        lnkDBMaint.Visible = False
                        lnkModifyDSNames.Visible = False
                        lnkDeleteDrugName.Visible = True
                        lnkViewSEDrugNames.Visible = True
                    Else  ' CurrentUser.UserGroup.ToUpper() = "SYSTEM ADMINISTRATORS"
                        lnkDBMaint.Visible = True
                        lnkModifyDSNames.Visible = True
                        lnkDeleteDrugName.Visible = False
                        lnkViewSEDrugNames.Visible = False
                    End If

                Else
                    adminList.Visible = False
                End If
            End If
            End If
    End Sub

End Class
