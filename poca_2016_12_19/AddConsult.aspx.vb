Imports Oracle.DataAccess.Client

Partial Class AddConsult
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

            DDLAppType.DataSource = DDLDataBind("APPLICATION_TYPE")
            DDLAppType.DataTextField = "Description"
            DDLAppType.DataValueField = "List_Item"
            DDLAppType.DataBind()

        End If
    End Sub

#Region "DDLDataBind"
    Public Function DDLDataBind(ByVal ListName As String) As DataView
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

    Private Sub AddNewConsult()

        Dim OraParams() As OracleParameter = New OracleParameter(14) {}
        Dim RetVal As Integer

        Try

            OraParams(0) = New OracleParameter("USERNAME_IN", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("consult_number_in", OracleDbType.Varchar2)
            OraParams(1).Value = TxtConsultNumber.Text

            OraParams(2) = New OracleParameter("application_number_in", OracleDbType.Varchar2)
            OraParams(2).Value = ValidateValue(TxtAppNumber.Text)

            OraParams(3) = New OracleParameter("application_type_in", OracleDbType.Varchar2)
            OraParams(3).Value = DDLAppType.SelectedItem.Value

            OraParams(4) = New OracleParameter("dt_requested_in", OracleDbType.Date)
            OraParams(4).Value = Nothing

            OraParams(5) = New OracleParameter("dt_received_in", OracleDbType.Date)
            OraParams(5).Value = ValidateDate(TxtDTReceived.Text)

            OraParams(6) = New OracleParameter("dt_completed_in", OracleDbType.Date)
            OraParams(6).Value = Nothing

            OraParams(7) = New OracleParameter("product_name_in", OracleDbType.Varchar2)
            OraParams(7).Value = TxtProductName.Text

            OraParams(8) = New OracleParameter("alternative_name_in", OracleDbType.Varchar2)
            OraParams(8).Value = ValidateValue(AlternativeName.Text)

            OraParams(9) = New OracleParameter("established_name_in", OracleDbType.Varchar2)
            OraParams(9).Value = ValidateValue(EstablishedName.Text)

            OraParams(10) = New OracleParameter("name_modifier_in", OracleDbType.Varchar2)
            OraParams(10).Value = ValidateValue(TxtProductModifier.Text)

            OraParams(11) = New OracleParameter("product_type_in", OracleDbType.Varchar2)
            OraParams(11).Value = Nothing

            OraParams(12) = New OracleParameter("usan_stem_name_in", OracleDbType.Varchar2)
            OraParams(12).Value = Nothing

            OraParams(13) = New OracleParameter("consult_narr_in", OracleDbType.Clob)
            OraParams(13).Value = ValidateValue(Comments.Text)

            OraParams(14) = New OracleParameter("returnVal_out", OracleDbType.Double, ParameterDirection.Output)
            OraParams(14).Value = Nothing

            RetVal = PPC.FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "consult_add", OraParams)

            'If RetVal > 0 Then
            If OraParams(14).Value > 0 Then
                ErrorLabel.Text = "Add Successful."
                ErrorLabel.ForeColor = Color.Red
                ErrorLabel.Visible = True

                If EPDDate.Text <> "" Then
                    If Not TxtProductName.Text = String.Empty Then
                        ScheduleEPD(TxtProductName.Text)
                    End If

                    If Not AlternativeName.Text = String.Empty Then
                        ScheduleEPD(AlternativeName.Text)
                    End If
                End If

            Else
                ErrorLabel.Text = "Please check the values you entered and then re-submit."
                ErrorLabel.ForeColor = Color.Red
                ErrorLabel.Visible = True
            End If

        Catch oe As OracleException
            ErrorLabel.Text = PPC.FDA.Data.OracleDataFactory.CleanOracleError(oe.Message)
            ErrorLabel.ForeColor = Color.Red
            ErrorLabel.Visible = True
        End Try

    End Sub

    Private Function ValidateDate(ByVal Value As String) As String

        Dim time1 As Date

        If (String.Compare(Value, "", 0) = 0) Then
            Return Nothing
        End If

        time1 = CType(Value, Date)
        Return time1.ToString("dd-MMM-yyyy")

    End Function

    Private Function ValidateValue(ByVal inputvalue As String) As String

        If inputvalue = "" Then
            Return Nothing
        Else
            Return inputvalue
        End If

    End Function

    '   ScheduleEPD is used to schedule the created consult to 
    '   an EPD date.
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

            RetVal = PPC.FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "consult_schedule_epd", OraParams)

            If RetVal > 0 Then
                ErrorLabel.Text = " EPD has been scheduled."
            End If

        Catch Ex As Exception
            ErrorLabel.Text = ErrorLabel.Text + Ex.Message
        Catch OE As OracleException
            ErrorLabel.Text = ErrorLabel.Text + OE.Message
        End Try
    End Function

    Private Sub BtnADD_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnADD.Click

        If Page.IsValid Then
            AddNewConsult()
        End If

    End Sub
End Class
