Imports System.Collections
Imports Oracle.DataAccess.Client

Partial Class Refreshdatasources
    Inherits System.Web.UI.Page

    Protected PO As PPC.FDA.Person.PersonObject
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
        'Put user code to initialize the page here

        If Not IsPostBack Then
            PO = Session("LoggedInUser")

            If PO Is Nothing Then
                Server.Transfer("default.aspx")
            End If

            ddlDatasourcesType.DataSource = SearchEngineData.BindList("record_source", PO.UserName)
            ddlDatasourcesType.DataValueField = "PICK_LIST_ITEM_ID"
            ddlDatasourcesType.DataTextField = "DESCRIPTION"
            ddlDatasourcesType.DataBind()
        End If
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If Page.IsValid Then

            PO = Session("LoggedInUser")

            Dim reader As OracleDataReader = SearchEngineData.BindList("record_source", PO.UserName)
            Dim pickListId As Integer
            Dim pickListItemId As Integer = ddlDatasourcesType.SelectedItem.Value
            While reader.Read()
                If reader("PICK_LIST_ITEM_ID") = pickListItemId Then
                    pickListId = reader("PICK_LIST_ID")
                End If
            End While
            reader.Close()

            If Session("appVars") Is Nothing Then
                Dim appVars As Hashtable = New Hashtable
                appVars.Add("refreshPickListId", pickListId)
                appVars.Add("refreshPickListItemId", pickListItemId)
                appVars.Add("refreshDatasource", ddlDatasourcesType.SelectedItem.Text)
                appVars.Add("refreshDate", txtDate.Text)
                appVars.Add("isRefresh", rbtnRefresh.Checked)
                Session.Add("appVars", appVars)
            Else
                Dim appVars As Hashtable = Session("appVars")
                appVars.Item("refreshPickListId") = pickListId
                appVars.Item("refreshPickListItemId") = pickListItemId
                appVars.Item("refreshDatasource") = ddlDatasourcesType.SelectedItem.Text
                appVars.Item("refreshDate") = txtDate.Text
                appVars.Item("isRefresh") = rbtnRefresh.Checked
            End If

            Server.Transfer("SelectDatasource.aspx")

        End If
    End Sub

End Class
