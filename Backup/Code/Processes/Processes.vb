' Static Model
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Namespace FDA.BusinessObjects

    Public Class Processes

        Public Sub New()
            MyBase.New()
        End Sub

        Public Function UserLogin(ByVal LoginName As String, _
              ByVal Password As String) As Person

            If LoginName = "" Or Password = "" Then
                Throw New Exception("Invalid Login Name or Password")
            End If

            Dim ods As DataSet
            Dim arrOParams() As OracleParameter = New OracleParameter(2) {}
            Dim dr As DataRow
            Dim HashedPassword As Byte()
            Dim LoggedInPerson As New Person()

            HashedPassword = HashPassword(Password)

            arrOParams(0) = New OracleParameter("USERNAME_IN", OracleDbType.Varchar2)
            arrOParams(0).Value = LoginName

            arrOParams(1) = New OracleParameter("PASSWORD_IN", OracleDbType.Raw)
            arrOParams(1).Value = HashedPassword

            arrOParams(2) = New OracleParameter("THE_USER", OracleDbType.RefCursor, ParameterDirection.Output)
            arrOParams(2).Value = Nothing

            Try
                ods = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "USER_LOGIN", arrOParams)
                If ods.Tables(0).Rows.Count > 0 Then

                    For Each dr In ods.Tables(0).Rows

                        LoggedInPerson.EmailAddress = dr.Item(4)
                        LoggedInPerson.UserName = dr.Item(1)
                        LoggedInPerson.FirstName = dr.Item(2)
                        LoggedInPerson.LastName = dr.Item(3)
                        LoggedInPerson.UserID = dr.Item(0)
                        LoggedInPerson.UserGroup = dr.Item(5)
                        LoggedInPerson.Password = HashedPassword

                    Next

                    Return LoggedInPerson

                Else
                    Throw New Exception("Invalid User Name or Password")
                End If
            Catch oe As OracleException
                Throw New Exception("Invalid User Name or Password")
            End Try

        End Function

        Public Sub ResetPassword(ByVal EmailAddress As String, _
                ByVal Name As String)

        End Sub

        Public Function HashPassword(ByVal Password As String) As Byte()

        End Function

        Public Function UpdateEmail(ByVal Person As Person, _
                ByVal EmailAddress As String) As Boolean

        End Function

        Public Sub RequestAccount(ByVal Name As String, _
              ByVal EmailAddress As String)

        End Sub

        Public Function LoadSettings(ByVal SettingsFor As Person) As PPC.FDA.BusinessObjects.SettingsCollection

        End Function

        Private Function SendEmail(ByVal EmailTo As String, _
               ByVal EmailFrom As String, _
               ByVal FullNameTo As String, _
               ByVal FullNameFrom As String) As Integer

        End Function

        ' CreateOracleParametersArray is a shared function that will create a parameter collection
        ' based on the number of params requested but the NumberOfParams Integer that is passed in.
        '
        Public Shared Function CreateOracleParametersArray(ByVal NumberOfParams As Integer) As OracleParameterCollection

            Dim OraCollection As OracleParameterCollection

            Dim Increment As Integer

            For Increment = 0 To NumberOfParams
                Dim OraParam = New OracleParameter
                OraCollection.Add(OraParam)
            Next

            Return OraCollection

        End Function

#Region "DDLDataBind"

        Public Shared Function DDLDataBind(ByVal ListName As String, ByVal CurrentUserName As String) As DataView
            Dim OraParams() As OracleParameter = New OracleParameter(2) {}
            Dim ods As DataSet
            Try

                OraParams(0) = New OracleParameter("USERNAME_IN", OracleDbType.Varchar2)
                OraParams(0).Value = CurrentUserName

                OraParams(1) = New OracleParameter("listname_in", OracleDbType.Varchar2)
                OraParams(1).Value = ListName

                OraParams(2) = New OracleParameter("pick_list", OracleDbType.RefCursor, ParameterDirection.Output)
                OraParams(2).Value = Nothing

                ods = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(HttpContext.Current.GetAppConfig("ConnectionString").ToString(), CommandType.StoredProcedure, "pick_list_get", OraParams)
                Return New DataView(ods.Tables(0))

            Catch oe As OracleException
                Throw New ApplicationException(oe.Message)
            Catch ex As Exception
                Throw New ApplicationException(ex.Message)
            End Try
        End Function
#End Region

    End Class ' END CLASS DEFINITION Processes
End Namespace