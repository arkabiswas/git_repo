Partial  Class TrackingHeaderNoNav
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

    Private PageTitle As String
    Private PageLocation As String

    Property Title() As String
        Get ' Retrieves the value of the private variable colBColor.
            Return PageTitle
        End Get

        Set(ByVal Value As String)   ' Stores the selected value in the private variable colBColor, and updates the backcolor of the label control lblDisplay.
            PageTitle = Value
            lblTitle.Text = PageTitle
        End Set
    End Property

    Property Location() As String
        Get
            Return PageLocation
        End Get

        Set(ByVal Value As String)
            PageLocation = Value
            lblLocation.Text = PageLocation
        End Set
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        Try
            Dim PO As New PPC.FDA.Person.PersonObject()
            PO = CType(Session("LoggedInUser"), PPC.FDA.Person.PersonObject)
            lblWelcome.Text = "Welcome " & PO.FullName & ". Today is " & Date.Now
        Catch
            lblWelcome.Text = "Welcome. Today is " & Date.Now
        End Try

    End Sub

End Class
