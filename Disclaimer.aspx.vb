Partial Class Disclaimer
    Inherits System.Web.UI.Page

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
        Dim PO As New PPC.FDA.Person.PersonObject()
        Try
            PO = CType(Session("LoggedInUser"), PPC.FDA.Person.PersonObject)
            If PO Is Nothing Then
                Response.Redirect("default.aspx")
            End If
        Catch
            Response.Redirect("default.aspx")
        End Try

    End Sub

    Private Sub ibtnAgree_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnAgree.Click
        'Go to Home page
        Session("disclaimer") = "signed"
        Response.Redirect("Home.aspx")
    End Sub

    Private Sub ibtnLogout_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnLogout.Click
        'Return to login screen
        Session("disclaimer") = "disagreed"
        Response.Redirect("Logout.aspx")
    End Sub
End Class
