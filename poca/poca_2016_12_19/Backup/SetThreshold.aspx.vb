Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Partial Class SetThreshold
    Inherits System.Web.UI.Page
    Protected WithEvents txtPageResults As System.Web.UI.WebControls.TextBox
    Protected PO As New PPC.FDA.Person.PersonObject()

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

    Private Sub User_GetThreshold()

        PO = Session("LoggedInUser")

        Try
            txtThreshold.Text = PO.GetSettingValue("ORTHOGRAPHIC_THRESHOLD")
        Catch oe As OracleException
            ' We won't do anything, since we will be updating the threshold
            ' anyway.
            MessageLabel.Text = "An error has occured. Please check your input and try again."
            MessageLabel.Visible = True
            MessageLabel.ForeColor = Color.Red
        End Try
    End Sub

    Private Sub User_SetThreshold()

        PO = Session("LoggedInUser")

        Try
            PO.ModifySettingValue("ORTHOGRAPHIC_THRESHOLD", txtThreshold.Text, OracleDbType.Int32)
            PO.ModifySettingValue("PHONETIC_THRESHOLD", txtThreshold.Text, OracleDbType.Int32)
            MessageLabel.Text = "Your setting has been saved."
            MessageLabel.ForeColor = Color.Red
            MessageLabel.Visible = True
        Catch ex As Exception
            ' Whoops there is an error catch and write out the message.
            MessageLabel.Text = "An error has occured. Please check your input and try again."
            MessageLabel.Visible = True
            MessageLabel.ForeColor = Color.Red
        Catch oe As OracleException
            ' Whoops there is an error catch and write out the message.
            MessageLabel.Text = "An error has occured. Please check your input and try again."
            MessageLabel.Visible = True
            MessageLabel.ForeColor = Color.Red
        End Try

    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            User_GetThreshold()
        End If

    End Sub

    Private Sub btnImageButton_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnImageButton.Click

        User_SetThreshold()

    End Sub
End Class
