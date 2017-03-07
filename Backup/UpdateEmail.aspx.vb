Partial Class UpdateEmail
    Inherits System.Web.UI.Page
    Protected WithEvents querytest As System.Web.UI.WebControls.Label
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
        If Not IsPostBack Then
            PO = Session("LoggedInUser")
            lblCurrentEmail.Text = PO.EmailAddress
        End If
    End Sub

    Private Sub btnImageButton_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnImageButton.Click

        PO = Session("LoggedInUser")

        Try
            If PO.UpdateEmail(txtNewEmail.Text) Then
                lblCurrentEmail.Text = PO.EmailAddress
                MessageLabel.Text = "Your Email Address has been updated!"
                MessageLabel.ForeColor = Color.Red
                MessageLabel.Visible = True
            Else
                MessageLabel.Text = "Updating your email address has failed."
                MessageLabel.ForeColor = Color.Red
                MessageLabel.Visible = True
            End If

        Catch eex As Exception
            MessageLabel.Text = "There was an error updating your email address: " + eex.Message
            MessageLabel.ForeColor = Color.Red
            MessageLabel.Visible = True
        End Try

    End Sub
End Class
