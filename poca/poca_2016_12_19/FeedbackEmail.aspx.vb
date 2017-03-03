Imports System.Text.RegularExpressions

Partial Class FeedbackEmail
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
        If Not IsPostBack Then
            GetFeedBackEmail()
        End If

    End Sub

    Private Sub GetFeedBackEmail()
        Try
            TxtFeedbackEmail.Text = PO.GetSettingValue("FEEDBACK_EMAIL")
        Catch ex As Exception
            Trace.Write("Feedback: " + ex.Message)
        End Try
    End Sub

    Private Sub SetFeedBackEmail()

        Dim email As New Regex("([\w-+]+(?:\.[\w-+]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7})")
        If email.IsMatch(TxtFeedbackEmail.Text) = False Then
            MessageLabel.Text = "Email Address Format is not valid. Please try again."
            MessageLabel.ForeColor = Color.Red
            MessageLabel.Visible = True
            Return
        End If

        Try
            If PO.ModifySettingValue("FEEDBACK_EMAIL", TxtFeedbackEmail.Text, Oracle.DataAccess.Client.OracleDbType.Varchar2, 1) Then
                MessageLabel.Text = "Your settings has been saved."
                MessageLabel.Visible = True
            End If
        Catch ex As Exception
            MessageLabel.Text = "There was an error saving your setting. Please try again."
            MessageLabel.Visible = True
            Trace.Write("Feedback Set:" + ex.Message)
        End Try
    End Sub

    Private Sub btnImageButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnImageButton.Click
        SetFeedBackEmail()
    End Sub

End Class

