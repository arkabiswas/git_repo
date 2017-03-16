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
            If po.SetManualDatabaseAdd(manualAddName.Text.Trim) Then
                manualAddMessage.Text = "Manual name added successfully."

                'Increase <Product Count> and Update <Date Updated> in Session("DATA_SOURCES") for Safety Evaluator datasource
                Dim DataSources As DataSet
                DataSources = CType(Session("DATA_SOURCES"), DataSet)
                For Each Row As DataRow In DataSources.Tables(0).Rows
                    Dim dsName As String = Row("DESCRIPTION").ToString()
                    If dsName.ToUpper().Contains("SAFETY") Then
                        Dim countStr As String = Row("DATA_SOURCE_RECORDS").ToString()
                        Dim count As Int32 = Convert.ToInt32(countStr) + 1
                        Row("DATA_SOURCE_RECORDS") = count.ToString()
                        Row("DATA_SOURCE_UPDATED") = Date.Today.ToString("MM-dd-yyyy")
                        Exit For
                    End If
                Next
                Session("DATA_SOURCES") = DataSources
            Else
                manualAddMessage.Text = "That name already exists in the database."
            End If

        Catch ex As Exception
            manualAddMessage.Text = ex.Message
        End Try
    End Sub

End Class
