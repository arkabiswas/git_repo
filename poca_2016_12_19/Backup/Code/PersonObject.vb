Imports System
Imports System.Collections
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports System.Security.Cryptography

Namespace FDA.Person

    <Serializable()> Public Enum SearchView
        Simple = 0
        Advanced = 1
    End Enum


    <Serializable()> Public Class PersonObject
        Private m_FirstName As String
        Private m_LastName As String
        Private m_FullName As String
        Private m_EmailAddress As String
        Private m_HashedPassword As Byte()
        Private m_Sid As String
        Private m_Threshold As Integer
        Private m_PageResults As Integer
        Private m_SearchView As SearchView
        Private m_UserName As String
        Private m_LastError As String
        Private m_UserGroup As String
        Private m_UserID As String

        Sub New()
            Me.UserName = "SE"
        End Sub

#Region "Person Object Properties"

        Property UserGroup() As String
            Get
                Return m_UserGroup
            End Get
            Set(ByVal Value As String)
                m_UserGroup = Value
            End Set
        End Property

        Property UserID() As String
            Get
                Return m_UserID
            End Get
            Set(ByVal Value As String)
                m_UserID = Value
            End Set
        End Property

        Property FirstName() As String
            Get
                Return m_FirstName
            End Get
            Set(ByVal Value As String)
                m_FirstName = Value
            End Set
        End Property

        Property LastName() As String
            Get
                Return m_LastName
            End Get
            Set(ByVal Value As String)
                m_LastName = Value
            End Set
        End Property

        ReadOnly Property FullName() As String
            Get
                Return m_FirstName + " " + m_LastName
            End Get
        End Property

        Property EmailAddress() As String
            Get
                Return m_EmailAddress
            End Get
            Set(ByVal Value As String)
                m_EmailAddress = Value
            End Set
        End Property

        Property Password() As Byte()
            Get
                Return m_HashedPassword
            End Get
            Set(ByVal Value As Byte())
                m_HashedPassword = Value
            End Set
        End Property

        Property SessionID() As String
            Get
                Return m_Sid
            End Get
            Set(ByVal Value As String)
                m_Sid = Value
            End Set
        End Property

        Property Threshold() As Integer
            Get
                Return m_Threshold
            End Get
            Set(ByVal Value As Integer)
                m_Threshold = Value
            End Set
        End Property

        Property PageResultsCount() As Integer
            Get
                Return m_PageResults
            End Get
            Set(ByVal Value As Integer)
                m_PageResults = Value
            End Set
        End Property

        Property SearchViewType() As SearchView
            Get
                Return m_SearchView
            End Get
            Set(ByVal Value As SearchView)
                m_SearchView = Value
            End Set
        End Property

        Property UserName() As String
            Get
                Return m_UserName
            End Get
            Set(ByVal Value As String)
                m_UserName = Value
            End Set
        End Property

        Property LastError() As String
            Get
                Return m_LastError
            End Get
            Set(ByVal Value As String)
                m_LastError = Value
            End Set
        End Property
#End Region

#Region "Person Object Functions"

#Region "User Helper Functions"
        Shared Function HashPasswordMD5(ByVal password As String) As Byte()
            'Non FIPS Complicant
            'MD5CryptoServiceProvider() '- 16
            '--------------------------
            'FIPS Compliant
            'HMACSHA1() - 20
            'SHA1CryptoServiceProvider() - 20
            'MACTripleDES() - 8

            Dim Hasher As New SHA1CryptoServiceProvider()

            Dim hashedPword As Byte()
            Dim encoder As New System.Text.UTF8Encoding()

            hashedPword = Hasher.ComputeHash(encoder.GetBytes(password))

            Return hashedPword

        End Function

#End Region

        '#Region "User Login"
        '        Public Function User_Login(ByVal UserName As String, ByVal UserPassword As String) As Boolean

        '            If UserName Is Nothing Or UserPassword Is Nothing Then
        '                Return False
        '            End If

        '            Dim ods As DataSet
        '            Dim arrOParams() As OracleParameter = New OracleParameter(2) {}
        '            Dim dr As DataRow
        '            Dim boolValidate As Boolean = False

        '            arrOParams(0) = New OracleParameter("USERNAME_IN", OracleDbType.Varchar2)
        '            arrOParams(0).Value = UserName

        '            arrOParams(1) = New OracleParameter("PASSWORD_IN", OracleDbType.Raw)
        '            arrOParams(1).Value = HashPasswordMD5(UserPassword)

        '            arrOParams(2) = New OracleParameter("THE_USER", OracleDbType.RefCursor, ParameterDirection.Output)
        '            arrOParams(2).Value = Nothing

        '            Try
        '                ods = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "USER_LOGIN", arrOParams)

        '                For Each dr In ods.Tables(0).Rows
        '                    Me.EmailAddress = dr.Item(4)
        '                    Me.UserName = dr.Item(1)
        '                    Me.FirstName = dr.Item(2)
        '                    Me.LastName = dr.Item(3)
        '                    Me.UserID = dr.Item(0)
        '                    Me.UserGroup = dr.Item(5)
        '                    Me.Password = HashPasswordMD5(UserPassword)
        '                    boolValidate = True
        '                Next

        '            Catch iore As IndexOutOfRangeException
        '                ' Nothing right now.            
        '            Catch oe As OracleException
        '                Me.LastError = oe.Message
        '                Return False
        '            End Try

        '            If boolValidate = False Then
        '                Me.LastError = "The User ID and Password combination you entered is not valid." 'the user was not validated
        '            End If

        '            Return boolValidate

        '        End Function
        '#End Region

#Region "User Login"
        Public Function User_Login(ByVal UserName As String) As Boolean

            If UserName Is Nothing Then
                Return False
            End If

            Dim ods As DataSet
            Dim arrOParams() As OracleParameter = New OracleParameter(1) {}
            Dim dr As DataRow
            Dim boolValidate As Boolean = False

            arrOParams(0) = New OracleParameter("USERNAME_IN", OracleDbType.Varchar2)
            arrOParams(0).Value = UserName 

            arrOParams(1) = New OracleParameter("THE_USER", OracleDbType.RefCursor, ParameterDirection.Output)
            arrOParams(1).Value = Nothing

            Try
                ods = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "NEW_USER_LOGIN", arrOParams)

                For Each dr In ods.Tables(0).Rows
                    Me.EmailAddress = dr.Item(4)
                    Me.UserName = dr.Item(1)
                    Me.FirstName = dr.Item(2)
                    Me.LastName = dr.Item(3)
                    Me.UserID = dr.Item(0)
                    Me.UserGroup = dr.Item(5) 
                    boolValidate = True
                Next

            Catch iore As IndexOutOfRangeException
                ' Nothing right now.            
            Catch oe As OracleException
                Me.LastError = oe.Message
                Return False
            End Try 

            Return boolValidate

        End Function
#End Region

#Region "User Logout"
        Public Function User_Logout() As Boolean

            Dim arrOParams() As OracleParameter = New OracleParameter(5) {}

            arrOParams(0) = New OracleParameter("action_name_in", OracleDbType.Varchar2)
            arrOParams(0).Value = "LOGOUT"

            arrOParams(1) = New OracleParameter("username_in", OracleDbType.Varchar2)
            arrOParams(1).Value = Me.UserName

            arrOParams(2) = New OracleParameter("last_name_in", OracleDbType.Varchar2)
            arrOParams(2).Value = Me.LastName

            arrOParams(3) = New OracleParameter("first_name_in", OracleDbType.Varchar2)
            arrOParams(3).Value = Me.FirstName

            arrOParams(4) = New OracleParameter("email_in", OracleDbType.Varchar2)
            arrOParams(4).Value = Me.EmailAddress

            arrOParams(5) = New OracleParameter("action_info_in", OracleDbType.Varchar2)
            arrOParams(5).Value = "Successful"

            Try
                PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "sys_log_add", arrOParams)
                Return True
            Catch oe As OracleException
                Me.LastError = PPC.FDA.Data.OracleDataFactory.CleanOracleError(oe.Message)
                Return False
            End Try

        End Function
#End Region

#Region "Get Personal Setting"
        Public Function GetSettingValue(ByVal SettingName As String) As String

            If SettingName Is Nothing Or SettingName = String.Empty Then
                Return String.Empty
            End If

            Dim oraparams() As OracleParameter = New OracleParameter(1) {}
            Dim ods As DataSet
            Dim dr As DataRow
            Dim ReturnValue As String = String.Empty

            oraparams(0) = New OracleParameter("USERNAME_IN", OracleDbType.Varchar2, ParameterDirection.Input)
            oraparams(0).Value = Me.UserName

            oraparams(1) = New OracleParameter("THE_SETTINGS", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
            oraparams(1).Value = Nothing

            Try
                ods = PPC.FDA.Data.OracleDataFactory.ExecuteDataset(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "SETTING_GET", oraparams)
                For Each dr In ods.Tables(0).Rows
                    If dr.Item(0) = SettingName.ToUpper Then
                        ReturnValue = dr.Item(4).ToString()
                    End If
                Next
            Catch oe As OracleException
                System.Diagnostics.Trace.Write("There was an error getting a personal setting: " + PPC.FDA.Data.OracleDataFactory.CleanOracleError(oe.Message))
            End Try

            Return ReturnValue
        End Function
#End Region

#Region " Modify Personal Settings"

        Public Function ModifySettingValue(ByVal settingname As String, ByVal newvalue As String, ByVal oratype As OracleDbType) As Boolean
            Return ModifySettingValue(settingname, newvalue, oratype, 0)
        End Function

        Public Function ModifySettingValue(ByVal settingname As String, ByVal newvalue As String, ByVal oraType As OracleDbType, ByVal GlobalIn As Integer) As Boolean

            If settingname Is Nothing Or settingname = String.Empty Then
                Return False
            End If

            Dim ORAParams() As OracleParameter = New OracleParameter(4) {}

            ORAParams(0) = New OracleParameter("USERNAME_IN", OracleDbType.Varchar2, ParameterDirection.Input)
            ORAParams(0).Value = Me.UserName

            ORAParams(1) = New OracleParameter("SETTING_NAME_IN", OracleDbType.Varchar2, ParameterDirection.Input)
            ORAParams(1).Value = settingname

            ORAParams(2) = New OracleParameter("SETTING_VALUE_IN", OracleDbType.Varchar2, ParameterDirection.Input)
            ORAParams(2).Value = newvalue

            ORAParams(3) = New OracleParameter("GLOBAL_IN", OracleDbType.Double, ParameterDirection.Input)
            ORAParams(3).Value = GlobalIn

            ORAParams(4) = New OracleParameter("override_in", OracleDbType.Double, ParameterDirection.Input)
            ORAParams(4).Value = GlobalIn

            Try
                PPC.FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "SETTING_MODIFY", ORAParams)
                Return True
            Catch oe As OracleException
                Return False
            End Try

        End Function
#End Region

#Region " Update User's Email"
        Public Function UpdateEmail(ByVal NewEmailAddress As String) As Boolean

            If NewEmailAddress Is Nothing Or NewEmailAddress = String.Empty Then
                Return False
            End If

            Dim ORAParams() As OracleParameter = New OracleParameter(6) {}

            ORAParams(0) = New OracleParameter("USER_ID", OracleDbType.Varchar2, ParameterDirection.Input)
            ORAParams(0).Value = Me.UserID

            ORAParams(1) = New OracleParameter("USERNAME_IN", OracleDbType.Varchar2, ParameterDirection.Input)
            ORAParams(1).Value = Me.UserName

            ORAParams(2) = New OracleParameter("PASSWORD_IN", OracleDbType.Raw, ParameterDirection.Input)
            ORAParams(2).Value = Me.Password

            ORAParams(3) = New OracleParameter("FIRSTNAME_IN", OracleDbType.Varchar2, ParameterDirection.Input)
            ORAParams(3).Value = Me.FirstName

            ORAParams(4) = New OracleParameter("LASTNAME_IN", OracleDbType.Varchar2, ParameterDirection.Input)
            ORAParams(4).Value = Me.LastName

            ORAParams(5) = New OracleParameter("EMAIL_IN", OracleDbType.Varchar2, ParameterDirection.Input)
            ORAParams(5).Value = NewEmailAddress

            ORAParams(6) = New OracleParameter("GROUP_IN", OracleDbType.Varchar2, ParameterDirection.Input)
            ORAParams(6).Value = Me.UserGroup

            Try
                PPC.FDA.Data.OracleDataFactory.ExecuteNonQuery(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "USER_MODIFY", ORAParams)
                Me.EmailAddress = NewEmailAddress
                Return True
            Catch oe As OracleException
                Return False
            End Try

        End Function
#End Region

#Region "Get a reference to the Email Server"
        Public Function GetEmailServer() As OpenSmtp.Mail.Smtp
            Dim MailServer As New OpenSmtp.Mail.Smtp

            MailServer.Host = ConfigurationManager.AppSettings("EmailServer")
            MailServer.Username = ConfigurationManager.AppSettings("EmailUsername")
            MailServer.Password = ConfigurationManager.AppSettings("EmailUserpassword")

            Return MailServer
        End Function
#End Region

        Public Function SetManualDatabaseAdd(ByVal NameToAdd As String) As Boolean
            If NameToAdd = "" Then
                Return False
            End If

            Dim OraParams() As OracleParameter = New OracleParameter(5) {}

            OraParams(0) = New OracleParameter("username_in", OracleDbType.Varchar2)
            OraParams(0).Value = Me.UserName

            OraParams(1) = New OracleParameter("product_name_in", OracleDbType.Varchar2)
            OraParams(1).Value = NameToAdd

            OraParams(2) = New OracleParameter("name_modifier_in", OracleDbType.Varchar2)
            OraParams(2).Value = Nothing

            OraParams(3) = New OracleParameter("name_type_id_in", OracleDbType.Int16)
            OraParams(3).Value = Nothing

            OraParams(4) = New OracleParameter("product_type_id_in", OracleDbType.Int16)
            OraParams(4).Value = Nothing

            OraParams(5) = New OracleParameter("product_out", OracleDbType.RefCursor, ParameterDirection.Output)
            OraParams(5).Value = Nothing

            Try
                Dim ProductAddReader As OracleDataReader
                ProductAddReader = FDA.Data.OracleDataFactory.ExecuteReader(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "product_add", OraParams)

                If ProductAddReader.Read Then
                    Return True
                End If
                Return False
            Catch ManualAddError As OracleException
                ManualAddError = ManualAddError.GetBaseException()
                Throw New Exception("Manual Add Error: " + ManualAddError.Message)
            End Try

        End Function

        Shared Function GetUserInformation(ByVal UserID As String) As PersonObject
            Return GetUserInformation(UserID, "")
        End Function

        Shared Function GetUserInformation(ByVal UserID As String, ByVal UserName As String) As PersonObject

            ' If the UserID is blank return the current user.
            If UserID = "" And UserName = "" Then
                Return Nothing
            End If

            Dim OtherPerson As New PersonObject
            Dim PersonDataReader As OracleDataReader
            Dim OraParams() As OracleParameter = New OracleParameter(0) {}

            OraParams(0) = New OracleParameter("the_userlist", OracleDbType.RefCursor, ParameterDirection.Output)
            OraParams(0).Value = Nothing

            PersonDataReader = FDA.Data.OracleDataFactory.ExecuteReader(ConfigurationManager.AppSettings("ConnectionString"), CommandType.StoredProcedure, "userlist_get", OraParams)
            While PersonDataReader.Read
                If PersonDataReader.GetString(0).Equals(UserID) Or PersonDataReader.GetString(1).ToLower.Equals(UserName.ToLower) Then
                    With PersonDataReader
                        OtherPerson.UserID = .GetString(0)
                        OtherPerson.UserName = .GetString(1)
                        OtherPerson.FirstName = .GetString(2)
                        OtherPerson.LastName = .GetString(3)
                        OtherPerson.EmailAddress = .GetString(4)
                        OtherPerson.UserGroup = .GetString(5)
                    End With
                    Exit While
                End If
            End While

            Return OtherPerson
        End Function
#End Region

    End Class

End Namespace

