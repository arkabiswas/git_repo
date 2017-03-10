Partial  Class ManualAdd
    Inherits System.Web.UI.UserControl

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
        manualAddSubmit.Attributes("onClick") = "return verify();"
    End Sub


    '' They want to send the name to the database.
    Private Sub manualAddSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles manualAddSubmit.Click
        Dim po As PPC.FDA.Person.PersonObject = Session("LoggedInUser")
        manualAddMessage.Visible = True

        Try
            If po.SetManualDatabaseAdd(manualAddName.Text) Then
                manualAddMessage.Text = "Manual name added successfully."
            Else
                manualAddMessage.Text = "That name already exists in the database."
            End If

        Catch ex As Exception
            manualAddMessage.Text = ex.Message
        End Try
    End Sub

End Class
