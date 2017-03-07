Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Public Class UARReport
    Inherits System.Web.UI.Page
    Protected PO As PPC.FDA.Person.PersonObject

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        PO = Session("LoggedInUser")
        If Not Page.IsPostBack Then
            Submit_Click(sender, e)
        End If

    End Sub

    Private Sub Submit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click

        MessageLabel.Text = ""

        Dim OraParams() As OracleParameter = New OracleParameter(2) {}
        
        Try
            OraParams(0) = New OracleParameter("begin_dt", OracleDbType.Date)
            Try
                OraParams(0).Value = ValidateDate(txtBeginDate.Text)
            Catch ex As Exception
                MessageLabel.Text = "Begin Date format is not correct. Please verify input and try again."
                Return
            End Try

            OraParams(1) = New OracleParameter("end_dt", OracleDbType.Date)
            Try
                OraParams(1).Value = ValidateDate(txtEndDate.Text)
            Catch ex As Exception
                MessageLabel.Text = "End Date format is not correct. Please verify input and try again."
                Return
            End Try

            If String.IsNullOrEmpty(txtBeginDate.Text.Trim) = False AndAlso String.IsNullOrEmpty(txtEndDate.Text.Trim) = False Then
                Dim beginDate As Date = CType(txtBeginDate.Text, Date)
                Dim endDate As Date = CType(txtEndDate.Text, Date)
                If beginDate.Date > endDate.Date Then
                    MessageLabel.Text = "Begin Date cannot be later then End Date. Please try again."
                    Return
                End If
            End If

            OraParams(2) = New OracleParameter("the_userlist", OracleDbType.RefCursor, ParameterDirection.Output)
            OraParams(2).Value = Nothing

            Dim ods As DataSet
            'ods = FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "userlist_get", New OracleParameter("the_userlist", OracleDbType.RefCursor, ParameterDirection.Output))
            ods = FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "get_all_users", OraParams)

            gridAll.DataSource = ods.Tables(0).DefaultView
            gridAll.DataBind()
            gridHeaderAll.Text = "Total All Users: " & ods.Tables(0).DefaultView.Count

            Dim view1 As DataView = ods.Tables(0).DefaultView
            view1.RowFilter = String.Format("{0} = {1}", "STATUS", "1")
            gridEnabled.DataSource = view1
            gridEnabled.DataBind()
            gridHeaderEnabled.Text = "Total Enabled Users: " & view1.Count

            Dim view2 As DataView = ods.Tables(0).DefaultView
            view2.RowFilter = String.Format("{0} = {1}", "STATUS", "0")
            gridDisabled.DataSource = view2
            gridDisabled.DataBind()
            gridHeaderDisabled.Text = "Total Disabled Users: " & view2.Count

        Catch ex As Exception
            MessageLabel.Text = "There was an error: " + ex.Message
        End Try

    End Sub

    Private Sub ExportToExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExportToExcel.Click

        Dim activeTab As String = ""
        If uarTabContainer.ActiveTabIndex = 0 Then
            activeTab = "All"
        ElseIf uarTabContainer.ActiveTabIndex = 1 Then
            activeTab = "Enabled"
        Else
            activeTab = "Disabled"
        End If

        Dim table As Table = New Table
        table.GridLines = GridLines.Both

        'Add Header Row
        Dim th As TableRow = New TableRow
        Dim th1 As TableCell = New TableCell
        Dim th2 As TableCell = New TableCell
        Dim th3 As TableCell = New TableCell
        Dim th4 As TableCell = New TableCell
        Dim th5 As TableCell = New TableCell
        Dim th6 As TableCell = New TableCell

        th1.Text = "User Name"
        th1.Font.Bold = True
        th1.BackColor = Color.Blue
        th1.ForeColor = Color.White
        th1.Font.Size = 14

        th2.Text = "Full Name"
        th2.Font.Bold = True
        th2.BackColor = Color.Blue
        th2.ForeColor = Color.White
        th2.Font.Size = 14

        th3.Text = "Email"
        th3.Font.Bold = True
        th3.BackColor = Color.Blue
        th3.ForeColor = Color.White
        th3.Font.Size = 14

        th4.Text = "User Group"
        th4.Font.Bold = True
        th4.BackColor = Color.Blue
        th4.ForeColor = Color.White
        th4.Font.Size = 14

        th5.Text = "Status"
        th5.Font.Bold = True
        th5.BackColor = Color.Blue
        th5.ForeColor = Color.White
        th5.Font.Size = 14

        th6.Text = "Create/Update Date"
        th6.Font.Bold = True
        th6.BackColor = Color.Blue
        th6.ForeColor = Color.White
        th6.Font.Size = 14

        th.Cells.Add(th1)
        th.Cells.Add(th2)
        th.Cells.Add(th3)
        th.Cells.Add(th4)
        th.Cells.Add(th5)
        th.Cells.Add(th6)
        table.Rows.Add(th)

        Dim bkColor As Boolean = True
        Dim rows As GridViewRowCollection

        If activeTab = "All" Then
            rows = gridAll.Rows
        ElseIf activeTab = "Enabled" Then
            rows = gridEnabled.Rows
        Else
            rows = gridDisabled.Rows
        End If

        For Each row As GridViewRow In rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim tr As TableRow = New TableRow
                Dim td1 As TableCell = New TableCell
                Dim td2 As TableCell = New TableCell
                Dim td3 As TableCell = New TableCell
                Dim td4 As TableCell = New TableCell
                Dim td5 As TableCell = New TableCell
                Dim td6 As TableCell = New TableCell

                Dim lblFName As Label = CType(row.FindControl("lblFirstName"), Label)
                Dim lblLName As Label = CType(row.FindControl("lblLastName"), Label)
                Dim lblStatus As Label = CType(row.FindControl("lblStatus"), Label)
                Dim lblStatusDate As Label = CType(row.FindControl("lblStatusDate"), Label)

                td1.Text = row.Cells(0).Text
                td2.Text = lblFName.Text + " " + lblLName.Text
                td3.Text = row.Cells(2).Text
                td4.Text = row.Cells(3).Text
                td5.Text = lblStatus.Text
                td6.Text = lblStatusDate.Text

                If bkColor Then
                    td1.BackColor = Color.FromArgb(224, 224, 224)
                    td2.BackColor = Color.FromArgb(224, 224, 224)
                    td3.BackColor = Color.FromArgb(224, 224, 224)
                    td4.BackColor = Color.FromArgb(224, 224, 224)
                    td5.BackColor = Color.FromArgb(224, 224, 224)
                    td6.BackColor = Color.FromArgb(224, 224, 224)
                    bkColor = False
                Else
                    td1.BackColor = Color.White
                    td2.BackColor = Color.White
                    td3.BackColor = Color.White
                    td4.BackColor = Color.White
                    td5.BackColor = Color.White
                    td6.BackColor = Color.White
                    bkColor = True
                End If

                tr.VerticalAlign = VerticalAlign.Top
                tr.Cells.Add(td1)
                tr.Cells.Add(td2)
                tr.Cells.Add(td3)
                tr.Cells.Add(td4)
                tr.Cells.Add(td5)
                tr.Cells.Add(td6)

                If activeTab = "All" Then
                    table.Rows.Add(tr)
                Else
                    If activeTab = lblStatus.Text Then
                        table.Rows.Add(tr)
                    End If
                End If
            End If
        Next

        Dim fileName As String = activeTab & "UserAccessReport.xls"

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=" & fileName)
        Response.ContentType = "application/vnd.ms-excel"

        Dim stringWriter As System.IO.StringWriter = New System.IO.StringWriter()
        Dim htmlTextWriter As HtmlTextWriter = New HtmlTextWriter(stringWriter)

        table.RenderControl(htmlTextWriter)
        Response.Write(stringWriter.ToString())
        Response.End()

    End Sub

    ' ValidateDate takes a string date and formats it for oracle consumption.
    Private Function ValidateDate(ByVal Value As String) As String
        Dim time1 As Date

        If (String.Compare(Value, "", 0) = 0) Then
            Return Nothing
        End If

        time1 = CType(Value, Date)
        Return time1.ToString("dd-MMM-yyyy")
    End Function

End Class