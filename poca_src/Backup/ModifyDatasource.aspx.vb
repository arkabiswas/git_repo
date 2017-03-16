
Imports Oracle.DataAccess.Client


Partial Class ModifyDatasource
    Inherits System.Web.UI.Page

    Protected PO As PPC.FDA.Person.PersonObject
    Public _pickListId As String = ""
    Private CurrentEdit As Boolean = False

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not IsPostBack Then
            PO = Session("LoggedInUser")

            If PO Is Nothing Then
                Server.Transfer("default.aspx")
            End If

            rpDatasource.DataSource = SearchEngineData.BindList("record_source", PO.UserName)
            rpDatasource.DataBind()
        End If

    End Sub


    ' This Function is used to get the pick list ID for the add bottom in footer template.
    ' Data source binding does not allow binding data to controls in the footer template.
    Public Function SetPickListId(ByVal pickListId As String) As String

        If _pickListId = "" Then
            _pickListId = pickListId
        End If
        Return pickListId
    End Function




    Private Sub rpDatasource_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rpDatasource.ItemCommand

        Select Case e.CommandName
            Case "Edit"
                ToggleEditAtIndex(e.Item.ItemIndex, False)
            Case "Add"
                Dim txtDatasourceAdd As TextBox = CType(FindNestedControl(rpDatasource, "txtDatasourceAdd"), TextBox)
                Dim description As String = txtDatasourceAdd.Text
                AddDatasource(txtDatasourceAdd.Text, txtDatasourceAdd.Text, e.CommandArgument)
            Case "Update"
                Dim description As String = CType(rpDatasource.Items(e.Item.ItemIndex).FindControl("txtDatasource"), TextBox).Text
                Dim detail_description As String = CType(rpDatasource.Items(e.Item.ItemIndex).FindControl("txtDatasourceDesc"), TextBox).Text
                ModifyDatasource(description, detail_description, e.CommandArgument)
            Case "Cancel"
                ToggleEditAtIndex(e.Item.ItemIndex, True)
            Case "Delete"
                DeleteDatasource(e.CommandArgument)
         End Select

    End Sub


    Private Function FindNestedControl(ByVal cParent As Control, ByVal id As String) As Control


        For Each c As Control In cParent.Controls
            If c.ID = id Then
                Return c
            ElseIf c.Controls.Count <> 0 Then
                Dim foundControl = FindNestedControl(c, id)
                If Not foundControl Is Nothing Then
                    Return foundControl
                End If
            End If
        Next

    End Function



    Private Sub DeleteDatasource(ByVal args As String)

        Dim strArray As String() = args.Split(";")
        Dim pickListId As String = strArray(0)
        Dim pickListItemId As String = strArray(1)

        If Session("appVars") Is Nothing Then
            Dim appVars As Hashtable = New Hashtable
            appVars.Add("deletePickListId", pickListId)
            appVars.Add("deletePickListItemId", pickListItemId)
            Session.Add("appVars", appVars)
        Else
            Dim appVars As Hashtable = Session("appVars")
            appVars.Item("deletePickListId") = pickListId
            appVars.Item("deletePickListItemId") = pickListItemId
        End If

        Server.Transfer("DatasourceDeleteWarning.aspx")

    End Sub


    Private Sub ModifyDatasource(ByVal description As String, ByVal detail_description As String, ByVal args As String)

        PO = Session("LoggedInUser")
        Dim strArray As String() = args.Split(";")
        Dim itemListNameId As String = strArray(0)
        Dim pickListId As String = strArray(1)
        Dim pickListItemId As String = strArray(2)

        Dim OraConnection As New OracleConnection(ConfigurationManager.AppSettings.Item("ConnectionString"))

        Dim OraParams() As OracleParameter = New OracleParameter(4) {}

        OraParams(0) = New OracleParameter("i_pick_list_id_in", OracleDbType.Double)
        OraParams(0).Value = pickListId
        OraParams(1) = New OracleParameter("i_pick_list_item_id_in", OracleDbType.Varchar2)
        OraParams(1).Value = pickListItemId
        OraParams(2) = New OracleParameter("u_description_in", OracleDbType.Varchar2)
        OraParams(2).Value = description
        OraParams(3) = New OracleParameter("u_detail_description_in", OracleDbType.Varchar2)
        OraParams(3).Value = detail_description
        OraParams(4) = New OracleParameter("username_in", OracleDbType.Varchar2)
        OraParams(4).Value = PO.UserName

        Dim rowsAffected As Integer = FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "data_access_ds_modify", OraParams)

        OraConnection.Close()


        rpDatasource.DataSource = SearchEngineData.BindList("record_source", PO.UserName)
        rpDatasource.DataBind()

        CurrentEdit = False
    End Sub

    Private Sub AddDatasource(ByVal description As String, ByVal listItem As String, ByVal pickListId As String)

        PO = Session("LoggedInUser")
        Dim OraConnection As New OracleConnection(ConfigurationManager.AppSettings.Item("ConnectionString"))

        Dim OraParams() As OracleParameter = New OracleParameter(3) {}

        Try

            OraParams(0) = New OracleParameter("i_pick_list_id_in", OracleDbType.Double)
            OraParams(0).Value = pickListId
            OraParams(1) = New OracleParameter("u_description_in", OracleDbType.Varchar2)
            OraParams(1).Value = description
            OraParams(2) = New OracleParameter("username_in", OracleDbType.Varchar2)
            OraParams(2).Value = PO.UserName
            OraParams(3) = New OracleParameter("u_filename_in", OracleDbType.Varchar2)

            Dim rowsAffected As Integer = FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "data_access_ds_add", OraParams)

        Catch
        End Try

        rpDatasource.DataSource = SearchEngineData.BindList("record_source", PO.UserName)
        rpDatasource.DataBind()
    End Sub


    Private Sub ToggleEditAtIndex(ByVal index As Integer, ByVal Cancel As Boolean)
        If Not Cancel Then
            For i As Integer = 0 To rpDatasource.Items.Count - 1
                For Each c As Control In rpDatasource.Items(i).Controls
                    If Not c.ID Is Nothing Then
                        If c.ID = "btnUpdate" And c.Visible Then Return
                    End If
                Next
            Next i
        End If

        For Each c As Control In rpDatasource.Items(index).Controls
            If Not c.ID Is Nothing Then
                c.Visible = Not c.Visible
            End If
        Next
    End Sub

End Class
