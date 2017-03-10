Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Namespace FDA

    Public Class SiteSettings
        Shared Function GetSiteSettings(ByVal SettingName As String) As String

            If SettingName Is Nothing Or SettingName = String.Empty Then
                Return String.Empty
            End If

            Dim oraparams() As OracleParameter = New OracleParameter(1) {}
            Dim ods As DataSet
            Dim dr As DataRow
            Dim ReturnValue As String = String.Empty

            oraparams(0) = New OracleParameter("USERNAME_IN", OracleDbType.Varchar2, ParameterDirection.Input)
            oraparams(0).Value = "SE"

            oraparams(1) = New OracleParameter("THE_SETTINGS", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            oraparams(1).Value = Nothing

            Try
                ods = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "SETTING_GET", oraparams)
                For Each dr In ods.Tables(0).Rows
                    If dr.Item(0) = SettingName.ToUpper Then
                        ReturnValue = dr.Item(4).ToString()
                    End If
                Next

                Return ReturnValue

            Catch oe As OracleException
                Return String.Empty
            End Try

        End Function


        Shared Function ResetPassword(ByVal UserToReset As String, ByVal NewPassword As String) As Boolean
            Dim OraParams() As OracleParameter = New OracleParameter(2) {}
            Dim RetVal As Integer

            OraParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
            OraParams(0).Value = UserToReset

            OraParams(1) = New OracleParameter("new_password_in", OracleDbType.Raw)
            OraParams(1).Value = PPC.FDA.Person.PersonObject.HashPasswordMD5(NewPassword)

            OraParams(2) = New OracleParameter("userid_in", OracleDbType.Varchar2)
            OraParams(2).Value = "01"

            Try
                RetVal = PPC.FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "admin_change_password", OraParams)
                If RetVal <> 0 Then
                    Return True
                End If
                Return False
            Catch OracleError As OracleException
                Throw New ApplicationException(PPC.FDA.Data.OracleDataFactory.CleanOracleError(OracleError.Message))
            End Try
        End Function
    End Class

End Namespace

