Partial Class ActRequestEmail
    Inherits System.Web.UI.Page
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
        If Not Page.IsPostBack Then
            GetActRequestEmail()
        End If

    End Sub

    Private Sub GetActRequestEmail()

        Try
            AcctRequestEmail.Text = PO.GetSettingValue("REGISTRATION_EMAIL")
        Catch ex As Exception
            MessageLabel.Text = ex.Message
        End Try

    End Sub

    Private Sub SetActRequestEmail()

        Try
            If PO.ModifySettingValue("REGISTRATION_EMAIL", AcctRequestEmail.Text, Oracle.DataAccess.Client.OracleDbType.Varchar2, 1) Then
                MessageLabel.Text = "Your setting has been saved."
            End If

        Catch ex As Exception
            MessageLabel.Text = ex.Message
        End Try

    End Sub

    Private Sub btnImageButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnImageButton.Click
        SetActRequestEmail()
    End Sub
End Class
