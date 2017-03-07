Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Partial Class DynamicWeights
    Inherits System.Web.UI.Page
    Protected WithEvents PhoneticRange As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents OrthoRange As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents RegOrthographicRegPhonetic As System.Web.UI.WebControls.RegularExpressionValidator
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
        If Not IsPostBack Then
            User_Get_Weights()
        End If
    End Sub

    Private Sub User_Get_Weights()

        Try
            Oweight.Text = PO.GetSettingValue("ORTHO_WEIGHT")
            PWeight.Text = PO.GetSettingValue("PHONO_WEIGHT")
            Aweight.Text = PO.GetSettingValue("FAX_WEIGHT")

        Catch oe As OracleException
            ' We won't do anything, since we will be updating the threshold
            ' anyway.
            MessageLabel.Text = "An error has occured. Please check your input and try again."
            MessageLabel.Visible = True
            MessageLabel.ForeColor = Color.Red
        End Try

    End Sub

    Private Sub User_Set_Weights()

        Dim Phon_Value, Ortho_Value, Afax_Value As Integer

        Phon_Value = CType(PWeight.Text, Integer)
        Ortho_Value = CType(Oweight.Text, Integer)
        Afax_Value = CType(Aweight.Text, Integer)

        If Phon_Value + Ortho_Value + Afax_Value = 100 Then

            Try
                If SetWeights("PHONO_WEIGHT", PWeight.Text) And SetWeights("ORTHO_WEIGHT", Oweight.Text) And SetWeights("FAX_WEIGHT", Aweight.Text) Then
                    MessageLabel.Text = "Your settings has been saved."
                    MessageLabel.ForeColor = Color.Red
                    MessageLabel.Visible = True
                Else
                    MessageLabel.Text = "Your setting has not been saved. Please check your input and try again."
                    MessageLabel.ForeColor = Color.Red
                    MessageLabel.Visible = True
                End If

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

        Else
            MessageLabel.Text = "All values must add upto 100. Please verify input and try again."
            MessageLabel.ForeColor = Color.Red
        End If


    End Sub

    Private Function SetWeights(ByVal settingname As String, ByVal newvalue As String) As Boolean

        Dim ORAParams() As OracleParameter = New OracleParameter(4) {}
        Dim RetVal As Integer

        ORAParams(0) = New OracleParameter("USERNAME_IN", OracleDbType.Varchar2)
        ORAParams(0).Value = PO.UserName

        ORAParams(1) = New OracleParameter("SETTING_NAME_IN", OracleDbType.Varchar2)
        ORAParams(1).Value = settingname

        ORAParams(2) = New OracleParameter("SETTING_VALUE_IN", OracleDbType.Varchar2)
        ORAParams(2).Value = newvalue

        ORAParams(3) = New OracleParameter("GLOBAL_IN", OracleDbType.Double)
        ORAParams(3).Value = 1

        ORAParams(4) = New OracleParameter("OVERRIDE_IN", OracleDbType.Double)
        ORAParams(4).Value = 1

        Try
            RetVal = PPC.FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "SETTING_MODIFY", ORAParams)

            If RetVal <> 0 Then
                Return True
            Else
                Return False
            End If

        Catch oe As OracleException
            Return False
        End Try

    End Function

    Private Sub btnImageButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnImageButton.Click
        User_Set_Weights()
    End Sub
End Class
