Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Partial Class DatasourceDetail
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
        PO = Session("LoggedInUser")
        If Not IsPostBack Then
            ShowDataDesc()
        End If
    End Sub

    Private Sub ShowDataDesc()
        Dim DSName As String = Request("DSName")
        Dim DSDesc As String = Request("DSDesc")

        If DSDesc = "" Then
            DSDesc = "There is no additional information available for this datasource."
        End If

        lblDataSourceName.Text = "Datasource Details for: " & DSName
        lblDataSourceDetail.Text = DSDesc
    End Sub

End Class
