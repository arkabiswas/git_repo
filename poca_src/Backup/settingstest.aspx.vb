Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Partial Class settingstest
    Inherits System.Web.UI.Page
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label

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
        Dim PO As New PPC.FDA.BusinessObjects.Person()
        Dim SC As New PPC.FDA.BusinessObjects.SettingsCollection()
        Dim IST As New PPC.FDA.BusinessObjects.SettingType()
        Dim inc As Integer

        For inc = 1 To 20
            Dim st As New PPC.FDA.BusinessObjects.SettingType()
            st.SettingName = inc.ToString()
            st.SettingValue = inc.ToString()
            SC.Add(st)
        Next

        PO.Settings = SC

        For Each IST In SC
            Dim lb As New Label()
            lb.Text = IST.SettingName + "| " + IST.SettingValue
            Page.Controls.Add(lb)
        Next

    End Sub

End Class
