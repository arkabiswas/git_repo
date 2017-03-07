Imports Microsoft.VisualBasic.CompilerServices
Imports Oracle.DataAccess.Client
Imports System
Imports System.Configuration
Imports System.Data

Public Class SearchEngineData
    ' Methods
    Public Sub New()
    End Sub

    Public Shared Function BindList(ByVal listName As String, ByVal userName As String) As IDataReader

        Dim OraConnection As New OracleConnection(ConfigurationManager.AppSettings.Item("ConnectionString"))
        Dim reader1 As IDataReader
        Dim parameterArray1 As OracleParameter

        Try
            Dim OraCommand As OracleCommand = OraConnection.CreateCommand

            OraCommand.CommandText = "pick_list_get"
            OraCommand.CommandType = CommandType.StoredProcedure

            parameterArray1 = New OracleParameter("USERNAME_IN", OracleDbType.Varchar2)
            parameterArray1.Value = userName
            OraCommand.Parameters.Add(parameterArray1)

            parameterArray1 = New OracleParameter("listname_in", OracleDbType.Varchar2)
            parameterArray1.Value = listName
            OraCommand.Parameters.Add(parameterArray1)

            parameterArray1 = New OracleParameter("pick_list", OracleDbType.RefCursor, ParameterDirection.Output)
            parameterArray1.Value = Nothing
            OraCommand.Parameters.Add(parameterArray1)

            OraConnection.Open()
            reader1 = OraCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return reader1
        Catch exception3 As OracleException
            OraConnection.Close()
            Throw New ApplicationException(exception3.Message)
        End Try

    End Function

    Public Shared Function GetAdditionalFactorsScore(ByVal SearchWord As String, ByVal DatabaseWord As String, ByVal userRequestingResource As String) As Double
        Dim num1 As Double
        Dim parameterArray1 As OracleParameter() = New OracleParameter(4 - 1) {}
        Dim connection1 As New OracleConnection(ConfigurationManager.AppSettings.Item("ConnectionString"))
        Dim command1 As New OracleCommand
        Dim adapter1 As New OracleDataAdapter
        Try
            connection1.Open()
            command1.Connection = connection1
            command1.CommandType = CommandType.StoredProcedure
            command1.CommandText = "addl_fax_search"

            parameterArray1(0) = New OracleParameter("username_in", 126)
            parameterArray1(0).Value = userRequestingResource
            command1.Parameters.Add(parameterArray1(0))

            parameterArray1(1) = New OracleParameter("name_1_in", 126)
            parameterArray1(1).Value = SearchWord
            command1.Parameters.Add(parameterArray1(1))

            parameterArray1(2) = New OracleParameter("name_2_in", 126)
            parameterArray1(2).Value = DatabaseWord
            command1.Parameters.Add(parameterArray1(2))

            parameterArray1(3) = New OracleParameter("result", OracleDbType.Double, ParameterDirection.Output)
            parameterArray1(3).Value = Nothing
            command1.Parameters.Add(parameterArray1(3))

            num1 = DoubleType.FromString(StringType.FromObject(command1.ExecuteScalar))
        Catch exception2 As OracleException
            num1 = 0
        Finally
            connection1.Close()
        End Try
        Return num1
    End Function

    Public Shared Function GetNamesDataSet(ByVal UserID As String, ByVal DataSources As String) As DataSet
        Dim set1 As New DataSet
        Try
            Dim connection1 As New OracleConnection(ConfigurationManager.AppSettings("ConnectionString"))
            'Dim command1 As New OracleCommand("search_name_get.search_name_get", connection1)
            'Dim command1 As New OracleCommand("search_name_get.search_name_get_delim", connection1)
            'Dim command1 As New OracleCommand("search_name_get.search_name_get_details", connection1)
            'Dim command1 As New OracleCommand("search_name_get.search_name_get_details_big", connection1)
            'Dim command1 As New OracleCommand("SEARCH_NAME_GET.SEARCH_NAME_GET_DETAILS_BIGDEL", connection1)
            Dim command1 As New OracleCommand("SEARCH_NAME_GET.SEARCH_NAME_GET_DETAILS_DEL", connection1)

            command1.CommandType = CommandType.StoredProcedure
            command1.Parameters.Add("username_in", OracleDbType.Varchar2).Value = UserID
            command1.Parameters.Add("source_list_in", OracleDbType.Varchar2).Value = DataSources
            command1.Parameters.Add("name_list_out", OracleDbType.RefCursor, ParameterDirection.Output).Value = Nothing

            Dim adapter1 As New OracleDataAdapter(command1)
            adapter1.Fill(set1)
            Return set1
        Catch exception2 As Exception
            Throw New Exception(exception2.Message)
        End Try

    End Function

    Public Shared Function TextSearch(ByVal searchText As String, ByVal datasourceList As String) As IDataReader
        Dim reader2 As IDataReader
        Dim parameterArray1 As OracleParameter() = New OracleParameter(3 - 1) {}
        Try
            parameterArray1(0) = New OracleParameter("term_in", OracleDbType.Varchar2)
            parameterArray1(0).Value = searchText
            parameterArray1(1) = New OracleParameter("source_list_in", OracleDbType.Varchar2)
            parameterArray1(1).Value = datasourceList
            parameterArray1(2) = New OracleParameter("the_names", OracleDbType.RefCursor, ParameterDirection.Output)
            parameterArray1(2).Value = Nothing
            Dim connection1 As New OracleConnection(ConfigurationManager.AppSettings.Item("ConnectionString"))
            Dim command1 As OracleCommand = connection1.CreateCommand
            command1.CommandText = "search_simple_name"
            command1.CommandType = CommandType.StoredProcedure
            Dim parameterArray2 As OracleParameter() = parameterArray1
            Dim num1 As Integer
            For num1 = 0 To parameterArray2.Length - 1
                Dim parameter1 As OracleParameter = parameterArray2(num1)
                command1.Parameters.Add(parameter1)
            Next num1
            connection1.Open()
            reader2 = command1.ExecuteReader(CommandBehavior.CloseConnection)
        Catch exception2 As OracleException
            ProjectData.SetProjectError(exception2)
            Dim exception1 As OracleException = exception2
            Throw New ApplicationException(exception1.Message)
            ProjectData.ClearProjectError()
        End Try
        Return reader2
    End Function

    Public Shared Function TextSearchForDelete(ByVal searchText As String, ByVal datasourceList As String) As IDataReader
        Dim reader2 As IDataReader
        Dim parameterArray1 As OracleParameter() = New OracleParameter(3 - 1) {}
        Try
            parameterArray1(0) = New OracleParameter("term_in", OracleDbType.Varchar2)
            parameterArray1(0).Value = searchText
            parameterArray1(1) = New OracleParameter("source_list_in", OracleDbType.Varchar2)
            parameterArray1(1).Value = datasourceList
            parameterArray1(2) = New OracleParameter("the_names", OracleDbType.RefCursor, ParameterDirection.Output)
            parameterArray1(2).Value = Nothing
            Dim connection1 As New OracleConnection(ConfigurationManager.AppSettings.Item("ConnectionString"))
            Dim command1 As OracleCommand = connection1.CreateCommand
            command1.CommandText = "delete_drug_name"
            command1.CommandType = CommandType.StoredProcedure
            Dim parameterArray2 As OracleParameter() = parameterArray1
            Dim num1 As Integer
            For num1 = 0 To parameterArray2.Length - 1
                Dim parameter1 As OracleParameter = parameterArray2(num1)
                command1.Parameters.Add(parameter1)
            Next num1
            connection1.Open()
            reader2 = command1.ExecuteReader(CommandBehavior.CloseConnection)
        Catch exception2 As OracleException
            ProjectData.SetProjectError(exception2)
            Dim exception1 As OracleException = exception2
            Throw New ApplicationException(exception1.Message)
            ProjectData.ClearProjectError()
        End Try
        Return reader2
    End Function

End Class
