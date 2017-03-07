Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Partial Class AddProductDetails
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
            ProductNameLabel.Text = "Product Details for " + Request("PrdName") + ":"
            BindDetailGrid()
            BindNewDetails()
        End If

    End Sub

    ' BindDetailGrid querys the database for existing product details. and then binds to the
    ' datagrid.
    '
    Private Sub BindDetailGrid()
        Dim OraParams() As OracleParameter = New OracleParameter(2) {}
        Dim ods As DataSet

        Try
            OraParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("product_name_in", OracleDbType.Varchar2)
            OraParams(1).Value = Request("PrdName")

            OraParams(2) = New OracleParameter("the_list", OracleDbType.RefCursor, ParameterDirection.Output)
            OraParams(2).Value = Nothing

            ods = FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "product_detail_get", OraParams)

            If ods.Tables(0).DefaultView.Count > 0 Then
                If ods.Tables(0).DefaultView.Count < 7 Then
                    ProductDetailsGrid.AllowPaging = False
                End If
                NoProductLabel.Visible = False
                ProductDetailsGrid.DataSource = ods
                ProductDetailsGrid.DataKeyField = "product_id"
                ProductDetailsGrid.DataBind()
            Else
                NoProductLabel.Visible = True
            End If

        Catch oe As OracleException
            ErrorLabel.Text = oe.Message
        End Try
    End Sub

    ' ItemDataBound is fired off everytime a row from the database has been boung
    ' to the grid but has not been rendered to the browser yet.
    '
    Private Sub ProductDetailsGrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles ProductDetailsGrid.ItemDataBound

        If e.Item.ItemType = ListItemType.EditItem Then
            ModifyDataRow(e)
        End If

        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            ShowValue(e)
        End If

    End Sub

    ' ShowValue takes a datagrid row and determines what value to dislpay
    '
    '
    Private Sub ShowValue(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)

        Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
        Dim ItemLabel As Label

        If drv.Item("pick_list_value").ToString() = String.Empty Then
            ProductDetailsGrid.Columns(1).Visible = True
            ItemLabel = CType(e.Item.FindControl("TextValueLabel"), Label)
            If drv.Item("text_value") Is DBNull.Value Then
                ItemLabel.Text = ""
            Else
                ItemLabel.Text = drv.Item("text_value")
            End If
            ItemLabel.Visible = True
        Else
            ProductDetailsGrid.Columns(1).Visible = True
            ItemLabel = CType(e.Item.FindControl("PickListLabel"), Label)
            If drv.Item("pick_list_value") Is DBNull.Value Then
                ItemLabel.Text = ""
            Else
                ItemLabel.Text = drv.Item("pick_list_value")
            End If
            ItemLabel.Visible = True
        End If

    End Sub

    ' ModifyDataRow will take the dataitem and process it with appropriate 
    ' dropdownlists and text fields.
    '
    Private Sub ModifyDataRow(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)

        Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)

        If drv.Item("pick_list").ToString() = String.Empty Then
            ProductDetailsGrid.Columns(1).Visible = True
            CType(e.Item.FindControl("TextValue"), TextBox).Visible = True
        Else
            ProductDetailsGrid.Columns(1).Visible = True
            Dim ProductDropDown As DropDownList = e.Item.FindControl("DDLPickListValue")

            ProductDropDown.Visible = True
            ProductDropDown.DataSource = DDLDataBind(drv.Item("PICK_LIST"))
            ProductDropDown.DataTextField = "description"
            ProductDropDown.DataValueField = "list_item"
            ProductDropDown.DataBind()
            If Not drv.Item("pick_list_value") Is DBNull.Value Then
                ProductDropDown.Items.FindByValue(drv.Item("pick_list_value")).Selected = True
            End If
        End If

    End Sub

#Region "DDLDataBind"
    ' DDLDataBind will take in the picklist name and then return a view
    ' of the data to be bind to picklist.
    '
    Private Function DDLDataBind(ByVal ListName As String) As DataView
        Dim OraParams() As OracleParameter = New OracleParameter(2) {}
        Dim ods As DataSet
        Try

            OraParams(0) = New OracleParameter("USERNAME_IN", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("listname_in", OracleDbType.Varchar2)
            OraParams(1).Value = ListName

            OraParams(2) = New OracleParameter("pick_list", OracleDbType.RefCursor, ParameterDirection.Output)
            OraParams(2).Value = Nothing

            ods = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "pick_list_get", OraParams)
            Return New DataView(ods.Tables(0))

        Catch oe As OracleException
            ErrorLabel.Text = "Error: " + oe.Message
            ErrorLabel.Visible = True

        Catch ex As Exception
            ErrorLabel.Text = "Error: " + ex.Message
            ErrorLabel.Visible = True

        End Try
    End Function
#End Region

    ' BindNewDetails creates a dataset of the additional factors and then binds them
    ' to a dropdownlist.
    '
    Private Sub BindNewDetails()

        Dim OraParams() As OracleParameter = New OracleParameter(1) {}

        OraParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
        OraParams(0).Value = PO.UserName

        OraParams(1) = New OracleParameter("the_fax", OracleDbType.RefCursor, ParameterDirection.Output)
        OraParams(1).Value = Nothing

        NewProductDetails.DataSource = FDA.Data.OracleDataFactory.ExecuteReader(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "addl_fax_get", OraParams)
        NewProductDetails.DataTextField = "element_label"
        NewProductDetails.DataValueField = "element_id"
        NewProductDetails.DataBind()

    End Sub

    ' UpdateDatarow does both updating of the product detail and also handles
    ' deleting of the detail.
    '
    Private Sub UpdateDataRow(ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs, ByVal DoDelete As Integer)

        Dim OraParams() As OracleParameter = New OracleParameter(6) {}
        Dim retval As Integer
        Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)

        Try
            Dim PickValue As String = ""
            Dim TextValue As String = ""

            If DoDelete = 0 Then
                Try
                    PickValue = CType(e.Item.FindControl("DDLPickListValue"), DropDownList).SelectedItem.Value
                Catch px As System.NullReferenceException
                    ' Use the default value.
                End Try

                Try
                    TextValue = CType(e.Item.FindControl("TextValue"), TextBox).Text
                Catch tx As System.NullReferenceException
                    ' Use the default value
                End Try
            Else
                PickValue = CType(e.Item.FindControl("PickListLabel"), Label).Text
                TextValue = CType(e.Item.FindControl("TextValueLabel"), Label).Text
            End If

            Dim ProductDetail As String = e.Item.Cells(5).Text
            Dim ElementId As String = e.Item.Cells(4).Text

            OraParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("product_id_in", OracleDbType.Varchar2)
            OraParams(1).Value = ProductDetailsGrid.DataKeys(e.Item.ItemIndex)

            OraParams(2) = New OracleParameter("element_id_in", OracleDbType.Double)
            OraParams(2).Value = ElementId

            OraParams(3) = New OracleParameter("pick_list_value_in", OracleDbType.Varchar2)
            OraParams(3).Value = PickValue

            OraParams(4) = New OracleParameter("text_value_in", OracleDbType.Varchar2)
            OraParams(4).Value = TextValue

            OraParams(5) = New OracleParameter("product_detail_id_in", OracleDbType.Double)
            OraParams(5).Value = ProductDetail

            OraParams(6) = New OracleParameter("delete_in", OracleDbType.Double)
            OraParams(6).Value = DoDelete

            retval = FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "product_detail_modify", OraParams)

            If retval <> 0 Then
                ErrorLabel.Text = "Update Sucessfull."
                ProductDetailsGrid.EditItemIndex = -1
                BindDetailGrid()
            Else
                ErrorLabel.Text = "There was an error updating this item. Please try again."
            End If

        Catch oe As OracleException
            ErrorLabel.Text = oe.Message
        End Try

    End Sub

    ' AddNewDetail creates a new product detail from the users selection from
    ' the dropdownlist
    '
    Private Sub AddNewDetail()

        Dim OraParams() As OracleParameter = New OracleParameter(6) {}
        Dim retval As Integer

        Try

            Dim ElementId As String = NewProductDetails.SelectedItem.Value

            OraParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("product_id_in", OracleDbType.Varchar2)
            OraParams(1).Value = Request("pid")

            OraParams(2) = New OracleParameter("element_id_in", OracleDbType.Double)
            OraParams(2).Value = ElementId

            OraParams(3) = New OracleParameter("pick_list_value_in", OracleDbType.Varchar2)
            OraParams(3).Value = Nothing

            OraParams(4) = New OracleParameter("text_value_in", OracleDbType.Varchar2)
            OraParams(4).Value = Nothing

            OraParams(5) = New OracleParameter("product_detail_id_in", OracleDbType.Double)
            OraParams(5).Value = 0

            OraParams(6) = New OracleParameter("delete_in", OracleDbType.Double)
            OraParams(6).Value = 0

            retval = FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "product_detail_modify", OraParams)

            If retval <> 0 Then
                ErrorLabel.Text = "Add Sucessful."
                BindDetailGrid()
            Else
                ErrorLabel.Text = "There was an error adding this item. Please try again."
            End If

        Catch oe As OracleException
            ErrorLabel.Text = oe.Message
        End Try
    End Sub

    Private Sub ProductDetailsGrid_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles ProductDetailsGrid.EditCommand
        ProductDetailsGrid.EditItemIndex = e.Item.ItemIndex
        BindDetailGrid()
    End Sub

    Private Sub ProductDetailsGrid_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles ProductDetailsGrid.CancelCommand
        ProductDetailsGrid.Columns(3).Visible = False
        ProductDetailsGrid.EditItemIndex = -1
        BindDetailGrid()
    End Sub

    Private Sub ProductDetailsGrid_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles ProductDetailsGrid.UpdateCommand
        ProductDetailsGrid.Columns(3).Visible = False
        UpdateDataRow(e, 0)
    End Sub

    Private Sub ProductDetailsGrid_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles ProductDetailsGrid.DeleteCommand
        UpdateDataRow(e, 1)
    End Sub

    Private Sub AddDetailsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddDetailsButton.Click
        AddNewDetail()
    End Sub

End Class
