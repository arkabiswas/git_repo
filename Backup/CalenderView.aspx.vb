Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports System.Text
Partial Class CalenderView
    Inherits System.Web.UI.Page
    Private EPDDataSet As DataSet
    Private PO As PPC.FDA.Person.PersonObject

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
            Try
                If Request.QueryString("date") <> "" Then
                    Dim CurrentDate As Date = CType(Request.QueryString("date"), Date)
                    SetupDataSet(CurrentDate.Month, CurrentDate.Year)
                    EPDCalender.TodaysDate = CurrentDate
                End If

            Catch ex As Exception
                ' If we have an Exception we will just display today's date.
            End Try

        End If

    End Sub

    Private Sub SetupDataSet(ByVal SelectedMonth As String, ByVal SelectedYear As String)

        Dim OraParams() As OracleParameter = New OracleParameter(2) {}
        Try

            Dim CompleteDate As Date = SelectedMonth + "/1/" + SelectedYear

            OraParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
            OraParams(0).Value = PO.UserName

            OraParams(1) = New OracleParameter("date_in", OracleDbType.Date)
            OraParams(1).Value = CompleteDate.ToString("dd-MMM-yyyy")

            OraParams(2) = New OracleParameter("the_calendar_entry", OracleDbType.RefCursor, ParameterDirection.Output)
            OraParams(2).Value = Nothing

            EPDDataSet = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "calendar_get", OraParams)

        Catch oe As OracleException

        Catch ex As Exception

        End Try

    End Sub

    Private Sub EPDCalender_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles EPDCalender.DayRender

        If EPDDataSet Is Nothing And Not e.Day.IsOtherMonth Then
            SetupDataSet(e.Day.Date.Month, e.Day.Date.Year)
        End If

        ' If the dataset is still empty then there are no items for the time range specified.
        If Not EPDDataSet Is Nothing Then
            Dim Dr As DataRow
            Dim CellInformation As StringBuilder = New StringBuilder()
            CellInformation.Append("<span class=DayItem><br />")

            For Each Dr In EPDDataSet.Tables(0).Rows
                Dim EPDdate As DateTime = CType(Dr("calendar_date"), DateTime)
                If EPDdate.Equals(e.Day.Date) Then
                    CellInformation.Append("<br /><a href='")
                    CellInformation.Append(String.Format("watchlist.aspx?cid={0}&clt={1}", Dr("consult_number"), Dr("consult_name")))
                    CellInformation.Append("'>" + Dr("consult_name") + "</a>")
                End If
            Next

            CellInformation.Append("</span>")
            e.Cell.Controls.Add(New LiteralControl(CellInformation.ToString()))
        End If

    End Sub

End Class
