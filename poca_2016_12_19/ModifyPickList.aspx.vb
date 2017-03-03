Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Partial Class ModifyPickList
    Inherits System.Web.UI.Page
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
        If Not Page.IsPostBack Then
            LoadGrid()
        End If

    End Sub

    Private Sub LoadGrid()

        PickListGrid.DataSource = CreateDataSet()
        PickListGrid.DataKeyField = "pick_list_item_id"
        PickListGrid.DataBind()

    End Sub

    Private Function CreateDataSet() As OracleDataReader

        Dim OraParams() As OracleParameter = New OracleParameter(2) {}
        Dim odr As OracleDataReader

        Try
            OraParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("listname_in", OracleDbType.Varchar2)
            OraParams(1).Value = "Level_of_Concern"

            OraParams(2) = New OracleParameter("pick_list", OracleDbType.RefCursor, ParameterDirection.Output)
            OraParams(2).Value = Nothing

            odr = FDA.Data.OracleDataFactory.ExecuteReader(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "pick_list_get", OraParams)

            Return odr

        Catch oe As OracleException
            ErrorLabel.Text = oe.Message
            Return Nothing
        End Try

    End Function

    Private Sub PickListGrid_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles PickListGrid.EditCommand
        PickListGrid.EditItemIndex = e.Item.ItemIndex
        LoadGrid()
    End Sub

    Private Sub PickListGrid_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles PickListGrid.CancelCommand
        PickListGrid.EditItemIndex = -1
        LoadGrid()
    End Sub

    Public Function SetListItem(ByVal Input As String) As String
        Dim RetVal As Integer
        RetVal = Convert.ToInt32(Input) + 1
        Return " List Item " & RetVal & ": "
    End Function

    Private Sub PickListGrid_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles PickListGrid.UpdateCommand

        If e.CommandName = "Update" Then

            Dim OraParams() As OracleParameter = New OracleParameter(5) {}
            Dim DescriptionText As TextBox = CType(e.Item.FindControl("description"), TextBox)
            Dim ListItem As Label = CType(e.Item.FindControl("ListItem"), Label)
            Dim PickListId As Label = CType(e.Item.FindControl("PickListId"), Label)
            Dim SequenceLabel As Label = CType(e.Item.FindControl("SequenceLabel"), Label)
            Dim RetVal As Integer

            Try

                OraParams(0) = New OracleParameter("i_pick_list_id_in", OracleDbType.Double)
                OraParams(0).Value = PickListId.Text

                OraParams(1) = New OracleParameter("i_pick_list_item_id_in", OracleDbType.Double)
                OraParams(1).Value = PickListGrid.DataKeys(e.Item.ItemIndex)

                OraParams(2) = New OracleParameter("u_item_name", OracleDbType.Varchar2)
                OraParams(2).Value = ListItem.Text

                OraParams(3) = New OracleParameter("u_description_in", OracleDbType.Varchar2)
                OraParams(3).Value = DescriptionText.Text

                OraParams(4) = New OracleParameter("username_in", OracleDbType.Varchar2)
                OraParams(4).Value = PO.UserName

                OraParams(5) = New OracleParameter("sequence_in", OracleDbType.Double)
                OraParams(5).Value = 1
                'OraParams(5).Value = GetSequence(e.Item.ItemIndex + 1, SequenceLabel.Text)

                RetVal = FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "pick_list_modify", OraParams)

                If RetVal <> 0 Then
                    ErrorLabel.Text = "Update Successful"
                    PickListGrid.EditItemIndex = -1
                    LoadGrid()
                End If

            Catch oe As OracleException
                ErrorLabel.Text = oe.Message
            End Try
        End If


    End Sub

    Private Function GetSequence(ByVal ItemIndex As Integer, ByVal CurrentSequence As String) As Integer

        If ItemIndex <> CType(CurrentSequence, Integer) Then
            Return ItemIndex
        Else
            Return CurrentSequence
        End If

    End Function
End Class
