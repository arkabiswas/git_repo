Imports System.Threading

Partial Class DirectSearch
    Inherits UserControl

    Public PO As PPC.FDA.Person.PersonObject

    Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Init
        Me.InitializeComponent()
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        PO = CType(Session("LoggedInUser"), PPC.FDA.Person.PersonObject)
        If PO Is Nothing Then
            Response.Redirect("default.aspx")
        End If
        divScoreResults.Visible = False
    End Sub

    Private Sub SearchReset_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles SearchReset.Click

        txtComparatorDrug.Text = ""
        txtCandidateDrug.Text = ""

        lblValidate.Text = ""
        lblValidate.Visible = False

        divScoreResults.Visible = False
    End Sub

    Private Sub SearchSubmit_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles SearchSubmit.Click

        Try
            'Validate if all search fields present
            If txtCandidateDrug.Text.Trim.Length = 0 Then
                lblValidate.Text = "Please enter a value in the Candidate Drug Text box."
                lblValidate.Visible = True
            ElseIf txtComparatorDrug.Text.Trim.Length = 0 Then
                lblValidate.Text = "Please enter a value in the Comparator Drug Text box."
                lblValidate.Visible = True
            Else
                lblValidate.Text = ""
                lblValidate.Visible = False
            End If

            If lblValidate.Visible = True Then
                Return
            End If

            'Do DrugSearch
            Dim ScoreStr As String = SearchLogic.PerformDirectSearch(txtCandidateDrug.Text.Trim, txtComparatorDrug.Text.Trim, PO)
            Dim Scores As String() = ScoreStr.Split(New Char() {";"c})
            Dim CombinedScore As String = Scores(0)
            Dim OrthoScore As String = Scores(1)
            Dim PhoneticScore As String = Scores(2)
            'Dim ReverseCombinedScore As String = Scores(3)
            'Dim ReverseOrthoScore As String = Scores(4)
            'Dim ReversePhoneticScore As String = Scores(5)

            'Display Scores
            'txtCombinedScore.Text = CombinedScore
            'txtOrthoScore.Text = OrthoScore
            'txtPhoneticScore.Text = PhoneticScore
            lblCombinedScoreVal.Text = CombinedScore
            lblOrthoScoreVal.Text = OrthoScore
            lblPhoneticScoreVal.Text = PhoneticScore
            'lblReverseCombinedScoreVal.Text = ReverseCombinedScore
            'lblReverseOrthoScoreVal.Text = ReverseOrthoScore
            'lblReversePhoneticScoreVal.Text = ReversePhoneticScore
            divScoreResults.Visible = True

        Catch ThreadExiting As ThreadAbortException
            ' The thread has exited, which probably means that something has gone wrong. 
            Trace.Write("Thread Abortion Exception: " + ThreadExiting.Message)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub
End Class