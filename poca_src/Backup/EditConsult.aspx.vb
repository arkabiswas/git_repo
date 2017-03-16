Imports Oracle.DataAccess.Client
Partial Class EditConsult
    Inherits System.Web.UI.Page
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents TxtConsultID As System.Web.UI.WebControls.TextBox
    Protected WithEvents ProductID As System.Web.UI.WebControls.TextBox
    Protected WithEvents ProductNameId As System.Web.UI.WebControls.TextBox
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
            PopulateDDL()
            BTNDelete.Attributes.Add("onClick", "return ConfirmDelete();")
            LoadEditConsult()
        End If
    End Sub

#Region "DDLDataBind"
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

#Region "PopulateDDL"
    Private Sub PopulateDDL()

        DDLAppType.DataSource = DDLDataBind("APPLICATION_TYPE")
        DDLAppType.DataTextField = "Description"
        DDLAppType.DataValueField = "PICK_LIST_ITEM_ID"
        DDLAppType.DataBind()

    End Sub
#End Region

#Region "LoadEditConsult"
    Private Sub LoadEditConsult()

        Dim odr As OracleDataReader
        Dim ods As DataSet

        Dim OraParams() As OracleParameter = New OracleParameter(3) {}

        Try
            OraParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("consult_number_in", OracleDbType.Varchar2)
            OraParams(1).Value = Request("cid")

            OraParams(2) = New OracleParameter("product_name_in", OracleDbType.Varchar2)
            OraParams(2).Value = Request("clt")

            OraParams(3) = New OracleParameter("the_consult", OracleDbType.RefCursor, ParameterDirection.Output)
            OraParams(3).Value = Nothing

            ods = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "CONSULT_GET", OraParams)

            Dim dr As DataRow

            Try
                For Each dr In ods.Tables(0).Rows
                    Select Case dr("product_type_name").ToString()
                        Case "Alternative" ' Alternative Name Processing
                            AlternativeName.Text = dr("product_name").ToString()
                            AlternateNameUID.Text = dr("name_type").ToString() + "|" + dr("consult_id").ToString() + "|" + dr("product_id").ToString() + "|" + dr("product_name_id").ToString()
                        Case "Established" ' Established Name Processing
                            EstablishedName.Text = dr("product_name").ToString()
                            EstablishedNameUID.Text = dr("name_type").ToString() + "|" + dr("consult_id").ToString() + "|" + dr("product_id").ToString() + "|" + dr("product_name_id").ToString()
                        Case "Proprietary", "Unknown" ' Product Name Processing
                            TxtProductName.Text = dr("product_name").ToString()
                            ProductNameUID.Text = dr("name_type").ToString() + "|" + dr("consult_id").ToString() + "|" + dr("product_id").ToString() + "|" + dr("product_name_id").ToString()

                            TxtConsultNumber.Text = dr("consult_number").ToString()
                            TxtAppNumber.Text = dr("app_number").ToString()
                            TxtProductModifier.Text = dr("product_modifier").ToString()
                            ProductDetailsLink.NavigateUrl = "AddProductDetails.aspx?prdname=" + TxtProductName.Text + "&pid=" + dr("PRODUCT_ID").ToString()
                            Comments.Text = dr("CONSULT_NARRATIVE").ToString()

                            If Not dr("dt_epd").ToString() = "" Then
                                EPDDate.Text = String.Format("{0:MM-dd-yyyy}", CType(dr("dt_epd").ToString(), DateTime))
                            End If

                            If Not dr("dt_received").ToString() = "" Then
                                TxtDTReceived.Text = String.Format("{0:MM-dd-yyyy}", CType(dr("dt_received").ToString(), DateTime))
                            End If

                            DDLAppType.Items.FindByValue(dr("app_type").ToString()).Selected = True

                    End Select
                Next

            Catch ex As Exception
                ErrorLabel.Text = ex.Message
                ErrorLabel.Visible = True
            End Try


        Catch oe As OracleException
            ErrorLabel.Text = "There was an error: " + oe.Message
            ErrorLabel.ForeColor = Color.Red
            ErrorLabel.Visible = True
        End Try

    End Sub
#End Region

    Private Sub UpdateConsult(ByVal ProductName As String, ByVal ProductNameId As String, ByVal ProductNameType As String, ByVal DoDelete As Boolean)

        Dim OraParams() As OracleParameter = New OracleParameter(14) {}
        Dim RetVal As Integer

        Try

            OraParams(0) = New OracleParameter("USERNAME_IN", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("consult_number_in", OracleDbType.Varchar2)
            OraParams(1).Value = TxtConsultNumber.Text

            OraParams(2) = New OracleParameter("product_name_id_in", OracleDbType.Varchar2)
            OraParams(2).Value = ProductNameId

            OraParams(3) = New OracleParameter("product_name_in", OracleDbType.Varchar2)
            OraParams(3).Value = ProductName

            OraParams(4) = New OracleParameter("product_type_in", OracleDbType.Double)
            OraParams(4).Value = Nothing

            OraParams(5) = New OracleParameter("product_name_type_in", OracleDbType.Double)
            OraParams(5).Value = ProductNameType

            OraParams(6) = New OracleParameter("product_name_modifier_in", OracleDbType.Varchar2)
            OraParams(6).Value = ValidateValue(TxtProductModifier.Text)

            OraParams(7) = New OracleParameter("application_number_in", OracleDbType.Varchar2)
            OraParams(7).Value = ValidateValue(TxtAppNumber.Text)

            OraParams(8) = New OracleParameter("application_type_id_in", OracleDbType.Double)
            OraParams(8).Value = ValidateValue(DDLAppType.SelectedItem.Value)

            OraParams(9) = New OracleParameter("dt_requested_in", OracleDbType.Date)
            OraParams(9).Value = Nothing

            OraParams(10) = New OracleParameter("dt_received_in", OracleDbType.Date)
            OraParams(10).Value = ValidateDate(TxtDTReceived.Text)

            If DoDelete Then
                OraParams(11) = New OracleParameter("dt_completed_in", OracleDbType.Date)
                OraParams(11).Value = ValidateDate(Date.Now.ToString)
            Else
                OraParams(11) = New OracleParameter("dt_completed_in", OracleDbType.Date)
                OraParams(11).Value = Nothing
            End If

            OraParams(12) = New OracleParameter("usan_stem_name_in", OracleDbType.Varchar2)
            OraParams(12).Value = Nothing

            OraParams(13) = New OracleParameter("consult_narr_in", OracleDbType.Clob)
            OraParams(13).Value = ValidateValue(Comments.Text)

            OraParams(14) = New OracleParameter("returnVal_out", OracleDbType.Double, ParameterDirection.Output)
            OraParams(14).Value = Nothing

            ' Update the base consult first.
            RetVal = PPC.FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "CONSULT_UPDATE", OraParams)
            ' If there is a value in the EPD Date Field Schedule the EPD.
            If EPDDate.Text <> "" And DoDelete = False Then
                If Not TxtProductName.Text = String.Empty Then
                    ScheduleEPD(TxtProductName.Text)
                End If

                If Not AlternativeName.Text = String.Empty Then
                    ScheduleEPD(AlternativeName.Text)
                End If
            End If

            ' If RetVal > 0 Then
            If OraParams(14).Value > 0 Then
                ErrorLabel.Text = "Update Successful."
                ErrorLabel.ForeColor = Color.Red
                ErrorLabel.Visible = True
            Else
                ErrorLabel.Text = "Please check the values you entered and then re-submit."
                ErrorLabel.ForeColor = Color.Red
                ErrorLabel.Visible = True
            End If

        Catch oe As OracleException
            If oe.Message.IndexOf("Consult not found") > -1 Then
                ErrorLabel.Text = "An Error Occured: Consult name " + ProductName + " not found."
            Else
                ErrorLabel.Text = oe.Message
            End If
            ErrorLabel.ForeColor = Color.Red
            ErrorLabel.Visible = True
        End Try

    End Sub

    '
    ' Schedule EPD Date.
    '
    Private Function ScheduleEPD(ByVal ProductName As String) As Boolean
        Dim OraParams() As OracleParameter = New OracleParameter(3) {}
        Dim RetVal As Integer

        Try

            OraParams(0) = New OracleParameter("USERNAME_IN", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("consult_number_in", OracleDbType.Varchar2)
            OraParams(1).Value = TxtConsultNumber.Text

            OraParams(2) = New OracleParameter("product_name_in", OracleDbType.Varchar2)
            OraParams(2).Value = ProductName

            OraParams(3) = New OracleParameter("dt_epd_in", OracleDbType.Date)
            OraParams(3).Value = ValidateDate(EPDDate.Text)


            RetVal = PPC.FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "CONSULT_SCHEDULE_EPD", OraParams)

            If RetVal > 0 Then
                Return True
            Else
                Return False
            End If

        Catch OE As OracleException
            Throw New ApplicationException(OE.Message)
        End Try

    End Function

    '
    '
    '
    Private Function ValidateValue(ByVal inputvalue As String) As String

        If inputvalue = "" Then
            Return Nothing
        Else
            Return inputvalue
        End If

    End Function

    '
    '
    '
    Private Function ValidateDate(ByVal Value As String) As String

        Dim time1 As Date

        If (String.Compare(Value, "", 0) = 0) Then
            Return Nothing
        End If

        time1 = CType(Value, Date)
        Return time1.ToString("dd-MMM-yyyy")

    End Function

    Private Function ParseConsultIds(ByVal ConsultIds As String) As String()
        Return ConsultIds.Split("|")
    End Function

    ' Delete button has been clicked
    '
    Private Sub BTNDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNDelete.Click

        Dim arryConsultUID As String() = ParseConsultIds(ProductNameUID.Text)
        UpdateConsult(TxtProductName.Text, arryConsultUID(3), arryConsultUID(0), True)

        If Not AlternativeName.Text = String.Empty Then
            arryConsultUID = ParseConsultIds(AlternateNameUID.Text)
            If Not arryConsultUID(0) = String.Empty Then UpdateConsult(AlternativeName.Text, arryConsultUID(3), arryConsultUID(0), True)
        End If

        If Not EstablishedName.Text = String.Empty Then
            arryConsultUID = ParseConsultIds(EstablishedNameUID.Text)
            If Not arryConsultUID(0) = String.Empty Then UpdateConsult(EstablishedName.Text, arryConsultUID(3), arryConsultUID(0), True)
        End If

    End Sub

    ' Update button has been clicked.
    ' 
    Private Sub BtnADD_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnADD.Click

        Dim arryConsultUID As String() = ParseConsultIds(ProductNameUID.Text)
        UpdateConsult(TxtProductName.Text, arryConsultUID(3), arryConsultUID(0), False)

        If Not AlternativeName.Text = String.Empty Then
            arryConsultUID = ParseConsultIds(AlternateNameUID.Text)
            If Not arryConsultUID(0) = String.Empty Then UpdateConsult(AlternativeName.Text, arryConsultUID(3), arryConsultUID(0), False)
        End If

        If Not EstablishedName.Text = String.Empty Then
            arryConsultUID = ParseConsultIds(EstablishedNameUID.Text)
            If Not arryConsultUID(0) = String.Empty Then UpdateConsult(EstablishedName.Text, arryConsultUID(3), arryConsultUID(0), False)
        End If

    End Sub
End Class
