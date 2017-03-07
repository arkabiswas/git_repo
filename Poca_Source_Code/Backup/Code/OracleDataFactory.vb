Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Namespace FDA.Data

    Public NotInheritable Class OracleDataFactory

#Region "Helper Functions"
        Sub New()
            ' Place holder for the constructor
        End Sub

        Private Shared Sub AttachParameters(ByVal command As OracleCommand, ByVal commandParameters() As OracleParameter)
            Dim p As OracleParameter
            For Each p In commandParameters
                'check for derived output value with no value assigned
                If p.Direction = ParameterDirection.InputOutput And p.Value Is Nothing Then
                    p.Value = Nothing
                End If
                command.Parameters.Add(p)
            Next p
        End Sub 'AttachParameters

        ' This method assigns an array of values to an array of OracleParameters.
        ' Parameters:
        ' -commandParameters - array of OracleParameters to be assigned values
        ' -array of objects holding the values to be assigned
        Private Shared Sub AssignParameterValues(ByVal commandParameters() As OracleParameter, ByVal parameterValues() As Object)

            Dim i As Short
            Dim j As Short

            If (commandParameters Is Nothing) And (parameterValues Is Nothing) Then
                'do nothing if we get no data
                Return
            End If

            ' we must have the same number of values as we pave parameters to put them in
            If commandParameters.Length <> parameterValues.Length Then
                Throw New ArgumentException("Parameter count does not match Parameter Value count.")
            End If

            'value array
            j = commandParameters.Length - 1
            For i = 0 To j
                commandParameters(i).Value = parameterValues(i)
            Next

        End Sub 'AssignParameterValues

        Private Shared Sub PrepareCommand(ByVal command As OracleCommand, _
                                          ByVal connection As OracleConnection, _
                                          ByVal transaction As OracleTransaction, _
                                          ByVal commandType As CommandType, _
                                          ByVal commandText As String, _
                                          ByVal commandParameters() As OracleParameter)

            'if the provided connection is not open, we will open it
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If

            'associate the connection with the command
            command.Connection = connection

            'set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText

            'set the command type
            command.CommandType = commandType

            'attach the command parameters if they are provided
            If Not (commandParameters Is Nothing) Then
                AttachParameters(command, commandParameters)
            End If

            Return
        End Sub 'PrepareCommand

        Public Shared Function CleanOracleError(ByVal OracleMessage As String) As String
            Dim CleanError As String
            Try
                CleanError = OracleMessage.Substring(OracleMessage.IndexOf(": ") + 2)
                CleanError = CleanError.Substring(0, CleanError.IndexOf("ORA"))
                Return CleanError.Trim()
            Catch ex As Exception
                Return CleanError.Trim()
            End Try

        End Function

        '''
        Public Shared Sub GetDrugnameAndDatasource(ByVal ProductUid As String, ByRef drugNameToDisplay As String, ByRef drugDataSourceToDisplay As String)

            Try
                Dim datasourceInfoConnection As New Oracle.DataAccess.Client.OracleConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Dim datasourceInfoCommand As Oracle.DataAccess.Client.OracleCommand = datasourceInfoConnection.CreateCommand()
                Dim datasourceParameter As OracleParameter

                datasourceInfoCommand.CommandText = "get_product_name_ds"
                datasourceInfoCommand.CommandType = CommandType.StoredProcedure

                datasourceParameter = New OracleParameter("product_name_uid_in", OracleDbType.Varchar2, ParameterDirection.Input)
                datasourceParameter.Value = ProductUid
                datasourceInfoCommand.Parameters.Add(datasourceParameter)

                datasourceParameter = New OracleParameter("product_details", OracleDbType.RefCursor, ParameterDirection.Output)
                datasourceInfoCommand.Parameters.Add(datasourceParameter)

                datasourceInfoConnection.Open()
                Dim datasourceInfoReader As OracleDataReader = datasourceInfoCommand.ExecuteReader(CommandBehavior.CloseConnection)

                While datasourceInfoReader.Read
                    drugNameToDisplay = datasourceInfoReader.GetString(0)
                    drugDataSourceToDisplay = GetDataSourceName(datasourceInfoReader(1).ToString())
                End While
                datasourceInfoReader.Close()

            Catch datasourceInfoException As OracleException
                Throw New DataException("There was an error retrieving the drug details: " + PPC.FDA.Data.OracleDataFactory.CleanOracleError(datasourceInfoException.Message))
            Catch ex As Exception
                Throw New ApplicationException("There was an error retrieving the drug details: " + ex.Message)
            End Try

        End Sub

        Public Shared Function GetDataSourceName(ByVal DataSourceId As String) As String
            Dim po As FDA.Person.PersonObject = HttpContext.Current.Session("LoggedInUser")
            Dim dataSourceList As Oracle.DataAccess.Client.OracleDataReader
            Dim dataSourceDescription As String = "Deleted Record"

            If HttpContext.Current.Cache("datasourceName" + DataSourceId) Is Nothing Then

                dataSourceList = SearchEngineData.BindList("record_source", po.UserName)

                While dataSourceList.Read()
                    If dataSourceList("pick_list_item_id").ToString() = DataSourceId Then
                        dataSourceDescription = dataSourceList("description")
                    End If
                End While
                ' let's add this description to the cache.
                HttpContext.Current.Cache("datasourceName" + DataSourceId) = dataSourceDescription
                dataSourceList.Close()
            Else
                dataSourceDescription = HttpContext.Current.Cache("datasourceName" + DataSourceId)
            End If

            Return dataSourceDescription

        End Function

#End Region


#Region "Oracle ExecuteReader"

        ' this enum is used to indicate whether the connection was provided by the caller, or created by SqlHelper, so that
        ' we can set the appropriate CommandBehavior when calling ExecuteReader()
        Private Enum OracleConnectionOwnership
            'Connection is owned and managed by SqlHelper
            Internal
            'Connection is owned and managed by the caller
            [External]
        End Enum 'OracleConnectionOwnership

        Private Overloads Shared Function ExecuteReader(ByVal OraConnection As OracleConnection, _
                                                        ByVal transaction As OracleTransaction, _
                                                        ByVal OraCommandType As CommandType, _
                                                        ByVal OraCommandText As String, _
                                                        ByVal OraCommandParams() As OracleParameter, _
                                                        ByVal OraOwnershop As OracleConnectionOwnership) As OracleDataReader
            Dim cmd As OracleCommand = New OracleCommand
            Dim dr As OracleDataReader

            PrepareCommand(cmd, OraConnection, transaction, OraCommandType, OraCommandText, OraCommandParams)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            cmd.Parameters.Clear()
            Return dr


        End Function

        Public Overloads Shared Function ExecuteReader(ByVal connectionString As String, _
                                                       ByVal commandType As CommandType, _
                                                       ByVal commandText As String) As OracleDataReader
            'pass through the call providing null for the set of OracleParameters
            Return ExecuteReader(connectionString, commandType, commandText, CType(Nothing, OracleParameter()))
        End Function 'ExecuteReader

        Public Overloads Shared Function ExecuteReader(ByVal connectionString As String, _
                                               ByVal commandType As CommandType, _
                                               ByVal commandText As String, _
                                               ByVal ParamArray commandParameters() As OracleParameter) As OracleDataReader
            'create & open a OracleConnection
            Dim cn As New OracleConnection(connectionString)
            cn.Open()

            Try
                'call the private overload that takes an internally owned connection in place of the connection string
                Return ExecuteReader(cn, CType(Nothing, OracleTransaction), commandType, commandText, commandParameters, OracleConnectionOwnership.Internal)
            Catch ex As OracleException
                'if we fail to return the SqlDatReader, we need to close the connection ourselves
                cn.Close() 'ADDED - Added close to close connections 10/07/2009 - PB
                cn.Dispose()
                Throw
            End Try

        End Function 'ExecuteReader

        Public Overloads Shared Function ExecuteReader(ByVal connectionString As String, _
                                               ByVal spName As String, _
                                               ByVal ParamArray parameterValues() As Object) As OracleDataReader
            Dim commandParameters As OracleParameter()

            'if we receive parameter values, we need to figure out where they go
            If Not (parameterValues Is Nothing) And parameterValues.Length > 0 Then
                'pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                'commandParameters = .GetSpParameterSet(connectionString, spName)

                'assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues)

                'call the overload that takes an array of OracleParameters
                Return ExecuteReader(connectionString, CommandType.StoredProcedure, spName, commandParameters)
                'otherwise we can just call the SP without params
            Else
                Return ExecuteReader(connectionString, CommandType.StoredProcedure, spName)
            End If
        End Function 'ExecuteReader

        Public Overloads Shared Function ExecuteReader(ByVal transaction As OracleTransaction, _
                                               ByVal commandType As CommandType, _
                                               ByVal commandText As String) As OracleDataReader
            'pass through the call providing null for the set of OracleParameters
            Return ExecuteReader(transaction, commandType, commandText, CType(Nothing, OracleParameter()))
        End Function 'ExecuteReader

        ' Execute a OracleCommand (that returns a resultset) against the specified OracleTransaction
        ' using the provided parameters.
        ' e.g.:  
        ' Dim dr As SqlDataReader = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders", new OracleParameter("@prodid", 24))
        ' Parameters:
        ' -transaction - a valid OracleTransaction 
        ' -commandType - the CommandType (stored procedure, text, etc.)
        ' -commandText - the stored procedure name or T-SQL command 
        ' -commandParameters - an array of SqlParamters used to execute the command 
        ' Returns: a SqlDataReader containing the resultset generated by the command 
        Public Overloads Shared Function ExecuteReader(ByVal transaction As OracleTransaction, _
                                                       ByVal commandType As CommandType, _
                                                       ByVal commandText As String, _
                                                       ByVal ParamArray commandParameters() As OracleParameter) As OracleDataReader
            'pass through to private overload, indicating that the connection is owned by the caller
            Return ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, OracleConnectionOwnership.External)
        End Function 'ExecuteReader

        ' Execute a stored procedure via a OracleCommand (that returns a resultset) against the specified OracleTransaction 
        ' using the provided parameter values.  This method will discover the parameters for the 
        ' stored procedure, and assign the values based on parameter order.
        ' This method provides no access to output parameters or the stored procedure's return value parameter.
        ' e.g.:  
        ' Dim dr As SqlDataReader = ExecuteReader(trans, "GetOrders", 24, 36)
        ' Parameters:
        ' -transaction - a valid OracleTransaction 
        ' -spName - the name of the stored procedure 
        ' -parameterValues - an array of objects to be assigned as the input values of the stored procedure
        ' Returns: a SqlDataReader containing the resultset generated by the command
        Public Overloads Shared Function ExecuteReader(ByVal transaction As OracleTransaction, _
                                                       ByVal spName As String, _
                                                       ByVal ParamArray parameterValues() As Object) As OracleDataReader
            Dim commandParameters As OracleParameter()

            'if we receive parameter values, we need to figure out where they go
            If Not (parameterValues Is Nothing) And parameterValues.Length > 0 Then
                'commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName)

                AssignParameterValues(commandParameters, parameterValues)

                Return ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters)
                'otherwise we can just call the SP without params
            Else
                Return ExecuteReader(transaction, CommandType.StoredProcedure, spName)
            End If
        End Function 'ExecuteReader
#End Region

#Region "ExecuteDataset"

        ' Execute a OracleCommand (that returns a resultset and takes no parameters) against the database specified in 
        ' the connection string. 
        ' e.g.:  
        ' Dim ds As DataSet = SqlHelper.ExecuteDataset("", commandType.StoredProcedure, "GetOrders")
        ' Parameters:
        ' -connectionString - a valid connection string for a OracleConnection
        ' -commandType - the CommandType (stored procedure, text, etc.)
        ' -commandText - the stored procedure name or T-SQL command
        ' Returns: a dataset containing the resultset generated by the command
        Public Overloads Shared Function ExecuteDataset(ByVal connectionString As String, _
                                                        ByVal commandType As CommandType, _
                                                        ByVal commandText As String) As DataSet
            'pass through the call providing null for the set of OracleParameters
            Return ExecuteDataset(connectionString, commandType, commandText, CType(Nothing, OracleParameter()))
        End Function 'ExecuteDataset

        ' Execute a OracleCommand (that returns a resultset) against the database specified in the connection string 
        ' using the provided parameters.
        ' e.g.:  
        ' Dim ds as Dataset = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders", new OracleParameter("@prodid", 24))
        ' Parameters:
        ' -connectionString - a valid connection string for a OracleConnection
        ' -commandType - the CommandType (stored procedure, text, etc.)
        ' -commandText - the stored procedure name or T-SQL command
        ' -commandParameters - an array of SqlParamters used to execute the command
        ' Returns: a dataset containing the resultset generated by the command
        Public Overloads Shared Function ExecuteDataset(ByVal connectionString As String, _
                                                        ByVal commandType As CommandType, _
                                                        ByVal commandText As String, _
                                                        ByVal ParamArray commandParameters() As OracleParameter) As DataSet
            'create & open a OracleConnection, and dispose of it after we are done.
            Dim cn As New OracleConnection(connectionString)
            Try
                cn.Open()

                'call the overload that takes a connection in place of the connection string
                Return ExecuteDataset(cn, commandType, commandText, commandParameters)
            Catch ex As Exception
                Throw ex
            Finally
                'ADDED - Added Close and Dispose to remove open connections - 10/07/2009 - PB
                cn.Close()
                cn.Dispose()
            End Try
        End Function 'ExecuteDataset

        ' Execute a stored procedure via a OracleCommand (that returns a resultset) against the database specified in 
        ' the connection string using the provided parameter values.  This method will discover the parameters for the 
        ' stored procedure, and assign the values based on parameter order.
        ' This method provides no access to output parameters or the stored procedure's return value parameter.
        ' e.g.:  
        ' Dim ds as Dataset= ExecuteDataset(connString, "GetOrders", 24, 36)
        ' Parameters:
        ' -connectionString - a valid connection string for a OracleConnection
        ' -spName - the name of the stored procedure
        ' -parameterValues - an array of objects to be assigned as the input values of the stored procedure
        ' Returns: a dataset containing the resultset generated by the command
        Public Overloads Shared Function ExecuteDataset(ByVal connectionString As String, _
                                                        ByVal spName As String, _
                                                        ByVal ParamArray parameterValues() As Object) As DataSet

            Dim commandParameters As OracleParameter()

            'if we receive parameter values, we need to figure out where they go
            If Not (parameterValues Is Nothing) And parameterValues.Length > 0 Then
                'pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                'commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName)

                'assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues)

                'call the overload that takes an array of OracleParameters
                Return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, commandParameters)
                'otherwise we can just call the SP without params
            Else
                Return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName)
            End If
        End Function 'ExecuteDataset

        ' Execute a OracleCommand (that returns a resultset and takes no parameters) against the provided OracleConnection. 
        ' e.g.:  
        ' Dim ds as Dataset = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders")
        ' Parameters:
        ' -connection - a valid OracleConnection
        ' -commandType - the CommandType (stored procedure, text, etc.)
        ' -commandText - the stored procedure name or T-SQL command
        ' Returns: a dataset containing the resultset generated by the command
        Public Overloads Shared Function ExecuteDataset(ByVal connection As OracleConnection, _
                                                        ByVal commandType As CommandType, _
                                                        ByVal commandText As String) As DataSet

            'pass through the call providing null for the set of OracleParameters
            Return ExecuteDataset(connection, commandType, commandText, CType(Nothing, OracleParameter()))
        End Function 'ExecuteDataset

        ' Execute a OracleCommand (that returns a resultset) against the specified OracleConnection 
        ' using the provided parameters.
        ' e.g.:  
        ' Dim ds as Dataset = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders", new OracleParameter("@prodid", 24))
        ' Parameters:
        ' -connection - a valid OracleConnection
        ' -commandType - the CommandType (stored procedure, text, etc.)
        ' -commandText - the stored procedure name or T-SQL command
        ' -commandParameters - an array of SqlParamters used to execute the command
        ' Returns: a dataset containing the resultset generated by the command
        Public Overloads Shared Function ExecuteDataset(ByVal connection As OracleConnection, _
                                                        ByVal commandType As CommandType, _
                                                        ByVal commandText As String, _
                                                        ByVal ParamArray commandParameters() As OracleParameter) As DataSet

            'create a command and prepare it for execution
            Dim cmd As New OracleCommand
            Dim ds As New DataSet
            Dim da As OracleDataAdapter

            PrepareCommand(cmd, connection, CType(Nothing, OracleTransaction), commandType, commandText, commandParameters)

            'create the DataAdapter & DataSet
            da = New OracleDataAdapter(cmd)

            'fill the DataSet using default values for DataTable names, etc.
            '''''''''''''''''''''''''''''''''''''''
            If Not ConnectionState.Open = cmd.Connection.State Then
                cmd.Connection.Open()
            End If
            '''''''''''''''''''''''''''''''''''''''

            da.Fill(ds)
            'detach the OracleParameters from the command object, so they can be used again
            cmd.Parameters.Clear()

            'return the dataset
            Return ds

        End Function 'ExecuteDataset

        ' Execute a stored procedure via a OracleCommand (that returns a resultset) against the specified OracleConnection 
        ' using the provided parameter values.  This method will discover the parameters for the 
        ' stored procedure, and assign the values based on parameter order.
        ' This method provides no access to output parameters or the stored procedure's return value parameter.
        ' e.g.:  
        ' Dim ds As Dataset = ExecuteDataset(conn, "GetOrders", 24, 36)
        ' Parameters:
        ' -connection - a valid OracleConnection
        ' -spName - the name of the stored procedure
        ' -parameterValues - an array of objects to be assigned as the input values of the stored procedure
        ' Returns: a dataset containing the resultset generated by the command
        Public Overloads Shared Function ExecuteDataset(ByVal connection As OracleConnection, _
                                                        ByVal spName As String, _
                                                        ByVal ParamArray parameterValues() As Object) As DataSet
            'Return ExecuteDataset(connection, spName, parameterValues)
            Dim commandParameters As OracleParameter()

            'if we receive parameter values, we need to figure out where they go
            If Not (parameterValues Is Nothing) And parameterValues.Length > 0 Then
                'pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                'commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName)

                'assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues)

                'call the overload that takes an array of OracleParameters
                Return ExecuteDataset(connection, CommandType.StoredProcedure, spName, commandParameters)
                'otherwise we can just call the SP without params
            Else
                Return ExecuteDataset(connection, CommandType.StoredProcedure, spName)
            End If

        End Function 'ExecuteDataset


        ' Execute a OracleCommand (that returns a resultset and takes no parameters) against the provided OracleTransaction. 
        ' e.g.:  
        ' Dim ds As Dataset = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders")
        ' Parameters
        ' -transaction - a valid OracleTransaction
        ' -commandType - the CommandType (stored procedure, text, etc.)
        ' -commandText - the stored procedure name or T-SQL command
        ' Returns: a dataset containing the resultset generated by the command
        Public Overloads Shared Function ExecuteDataset(ByVal transaction As OracleTransaction, _
                                                        ByVal commandType As CommandType, _
                                                        ByVal commandText As String) As DataSet
            'pass through the call providing null for the set of OracleParameters
            Return ExecuteDataset(transaction, commandType, commandText, CType(Nothing, OracleParameter()))
        End Function 'ExecuteDataset

        ' Execute a OracleCommand (that returns a resultset) against the specified OracleTransaction
        ' using the provided parameters.
        ' e.g.:  
        ' Dim ds As Dataset = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders", new OracleParameter("@prodid", 24))
        ' Parameters
        ' -transaction - a valid OracleTransaction 
        ' -commandType - the CommandType (stored procedure, text, etc.)
        ' -commandText - the stored procedure name or T-SQL command
        ' -commandParameters - an array of SqlParamters used to execute the command
        ' Returns: a dataset containing the resultset generated by the command
        Public Overloads Shared Function ExecuteDataset(ByVal transaction As OracleTransaction, _
                                                        ByVal commandType As CommandType, _
                                                        ByVal commandText As String, _
                                                        ByVal ParamArray commandParameters() As OracleParameter) As DataSet
            'create a command and prepare it for execution
            Dim cmd As New OracleCommand
            Dim ds As New DataSet
            Dim da As OracleDataAdapter

            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters)

            'create the DataAdapter & DataSet
            da = New OracleDataAdapter(cmd)

            'fill the DataSet using default values for DataTable names, etc.
            da.Fill(ds)

            'detach the OracleParameters from the command object, so they can be used again
            cmd.Parameters.Clear()

            'return the dataset
            Return ds
        End Function 'ExecuteDataset

        ' Execute a stored procedure via a OracleCommand (that returns a resultset) against the specified
        ' OracleTransaction using the provided parameter values.  This method will discover the parameters for the 
        ' stored procedure, and assign the values based on parameter order.
        ' This method provides no access to output parameters or the stored procedure's return value parameter.
        ' e.g.:  
        ' Dim ds As Dataset = ExecuteDataset(trans, "GetOrders", 24, 36)
        ' Parameters:
        ' -transaction - a valid OracleTransaction 
        ' -spName - the name of the stored procedure
        ' -parameterValues - an array of objects to be assigned as the input values of the stored procedure
        ' Returns: a dataset containing the resultset generated by the command
        Public Overloads Shared Function ExecuteDataset(ByVal transaction As OracleTransaction, _
                                                        ByVal spName As String, _
                                                        ByVal ParamArray parameterValues() As Object) As DataSet
            Dim commandParameters As OracleParameter()

            'if we receive parameter values, we need to figure out where they go
            If Not (parameterValues Is Nothing) And parameterValues.Length > 0 Then
                'pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                'commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName)

                'assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues)

                'call the overload that takes an array of OracleParameters
                Return ExecuteDataset(transaction, CommandType.StoredProcedure, spName, commandParameters)
                'otherwise we can just call the SP without params
            Else
                Return ExecuteDataset(transaction, CommandType.StoredProcedure, spName)
            End If
        End Function 'ExecuteDataset

#End Region

#Region "ExecuteNonQuery"

        ' Execute a OracleCommand (that returns no resultset and takes no parameters) against the database specified in 
        ' the connection string. 
        ' e.g.:  
        '  Dim result as Integer =  ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders")
        ' Parameters:
        ' -connectionString - a valid connection string for a OracleConnection
        ' -commandType - the CommandType (stored procedure, text, etc.)
        ' -commandText - the stored procedure name or T-SQL command
        ' Returns: an int representing the number of rows affected by the command
        Public Overloads Shared Function ExecuteNonQuery(ByVal connectionString As String, _
                                                       ByVal commandType As CommandType, _
                                                       ByVal commandText As String) As Integer
            'pass through the call providing null for the set of OracleParameters
            Return ExecuteNonQuery(connectionString, commandType, commandText, CType(Nothing, OracleParameter()))
        End Function 'ExecuteNonQuery

        ' Execute a OracleCommand (that returns no resultset) against the database specified in the connection string 
        ' using the provided parameters.
        ' e.g.:  
        ' Dim result as Integer = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new OracleParameter("@prodid", 24))
        ' Parameters:
        ' -connectionString - a valid connection string for a OracleConnection
        ' -commandType - the CommandType (stored procedure, text, etc.)
        ' -commandText - the stored procedure name or T-SQL command
        ' -commandParameters - an array of SqlParamters used to execute the command
        ' Returns: an int representing the number of rows affected by the command
        Public Overloads Shared Function ExecuteNonQuery(ByVal connectionString As String, _
                                                         ByVal commandType As CommandType, _
                                                         ByVal commandText As String, _
                                                         ByVal ParamArray commandParameters() As OracleParameter) As Integer
            'create & open a OracleConnection, and dispose of it after we are done.
            Dim cn As New OracleConnection(connectionString)
            Try
                cn.Open()

                'call the overload that takes a connection in place of the connection string
                Return ExecuteNonQuery(cn, commandType, commandText, commandParameters)
            Finally
                cn.Close() 'ADDED - Added close to close connections 10/07/2009 - PB
                cn.Dispose()
            End Try
        End Function 'ExecuteNonQuery

        ' Execute a stored procedure via a OracleCommand (that returns no resultset) against the database specified in 
        ' the connection string using the provided parameter values.  This method will discover the parameters for the 
        ' stored procedure, and assign the values based on parameter order.
        ' This method provides no access to output parameters or the stored procedure's return value parameter.
        ' e.g.:  
        '  Dim result as Integer = ExecuteNonQuery(connString, "PublishOrders", 24, 36)
        ' Parameters:
        ' -connectionString - a valid connection string for a OracleConnection
        ' -spName - the name of the stored procedure
        ' -parameterValues - an array of objects to be assigned as the input values of the stored procedure
        ' Returns: an int representing the number of rows affected by the command
        Public Overloads Shared Function ExecuteNonQuery(ByVal connectionString As String, _
                                                         ByVal spName As String, _
                                                         ByVal ParamArray parameterValues() As Object) As Integer
            Dim commandParameters As OracleParameter()

            'if we receive parameter values, we need to figure out where they go
            If Not (parameterValues Is Nothing) And parameterValues.Length > 0 Then
                'pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)

                'commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName)

                'assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues)

                'call the overload that takes an array of OracleParameters
                Return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandParameters)
                'otherwise we can just call the SP without params
            Else
                Return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName)
            End If
        End Function 'ExecuteNonQuery

        ' Execute a OracleCommand (that returns no resultset and takes no parameters) against the provided OracleConnection. 
        ' e.g.:  
        ' Dim result as Integer = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders")
        ' Parameters:
        ' -connection - a valid OracleConnection
        ' -commandType - the CommandType (stored procedure, text, etc.)
        ' -commandText - the stored procedure name or T-SQL command 
        ' Returns: an int representing the number of rows affected by the command
        Public Overloads Shared Function ExecuteNonQuery(ByVal connection As OracleConnection, _
                                                         ByVal commandType As CommandType, _
                                                         ByVal commandText As String) As Integer
            'pass through the call providing null for the set of OracleParameters
            Return ExecuteNonQuery(connection, commandType, commandText, CType(Nothing, OracleParameter()))

        End Function 'ExecuteNonQuery

        ' Execute a OracleCommand (that returns no resultset) against the specified OracleConnection 
        ' using the provided parameters.
        ' e.g.:  
        '  Dim result as Integer = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders", new OracleParameter("@prodid", 24))
        ' Parameters:
        ' -connection - a valid OracleConnection 
        ' -commandType - the CommandType (stored procedure, text, etc.)
        ' -commandText - the stored procedure name or T-SQL command 
        ' -commandParameters - an array of SqlParamters used to execute the command 
        ' Returns: an int representing the number of rows affected by the command 
        Public Overloads Shared Function ExecuteNonQuery(ByVal connection As OracleConnection, _
                                                         ByVal commandType As CommandType, _
                                                         ByVal commandText As String, _
                                                         ByVal ParamArray commandParameters() As OracleParameter) As Integer

            'create a command and prepare it for execution
            Dim cmd As New OracleCommand
            Dim retval As Integer

            PrepareCommand(cmd, connection, CType(Nothing, OracleTransaction), commandType, commandText, commandParameters)

            'finally, execute the command.
            retval = cmd.ExecuteNonQuery()

            'detach the OracleParameters from the command object, so they can be used again
            cmd.Parameters.Clear()

            Return retval

        End Function 'ExecuteNonQuery

        ' Execute a stored procedure via a OracleCommand (that returns no resultset) against the specified OracleConnection 
        ' using the provided parameter values.  This method will discover the parameters for the 
        ' stored procedure, and assign the values based on parameter order.
        ' This method provides no access to output parameters or the stored procedure's return value parameter.
        ' e.g.:  
        '  Dim result as integer = ExecuteNonQuery(conn, "PublishOrders", 24, 36)
        ' Parameters:
        ' -connection - a valid OracleConnection
        ' -spName - the name of the stored procedure 
        ' -parameterValues - an array of objects to be assigned as the input values of the stored procedure 
        ' Returns: an int representing the number of rows affected by the command 
        Public Overloads Shared Function ExecuteNonQuery(ByVal connection As OracleConnection, _
                                                         ByVal spName As String, _
                                                         ByVal ParamArray parameterValues() As Object) As Integer
            Dim commandParameters As OracleParameter()

            'if we receive parameter values, we need to figure out where they go
            If Not (parameterValues Is Nothing) And parameterValues.Length > 0 Then
                'pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                'commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName)

                'assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues)

                'call the overload that takes an array of OracleParameters
                Return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, commandParameters)
                'otherwise we can just call the SP without params
            Else
                Return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName)
            End If

        End Function 'ExecuteNonQuery

        ' Execute a OracleCommand (that returns no resultset and takes no parameters) against the provided OracleTransaction.
        ' e.g.:  
        '  Dim result as Integer = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders")
        ' Parameters:
        ' -transaction - a valid OracleTransaction associated with the connection 
        ' -commandType - the CommandType (stored procedure, text, etc.) 
        ' -commandText - the stored procedure name or T-SQL command 
        ' Returns: an int representing the number of rows affected by the command 
        Public Overloads Shared Function ExecuteNonQuery(ByVal transaction As OracleTransaction, _
                                                         ByVal commandType As CommandType, _
                                                         ByVal commandText As String) As Integer
            'pass through the call providing null for the set of OracleParameters
            Return ExecuteNonQuery(transaction, commandType, commandText, CType(Nothing, OracleParameter()))
        End Function 'ExecuteNonQuery

        ' Execute a OracleCommand (that returns no resultset) against the specified OracleTransaction
        ' using the provided parameters.
        ' e.g.:  
        ' Dim result as Integer = ExecuteNonQuery(trans, CommandType.StoredProcedure, "GetOrders", new OracleParameter("@prodid", 24))
        ' Parameters:
        ' -transaction - a valid OracleTransaction 
        ' -commandType - the CommandType (stored procedure, text, etc.) 
        ' -commandText - the stored procedure name or T-SQL command 
        ' -commandParameters - an array of SqlParamters used to execute the command 
        ' Returns: an int representing the number of rows affected by the command 
        Public Overloads Shared Function ExecuteNonQuery(ByVal transaction As OracleTransaction, _
                                                         ByVal commandType As CommandType, _
                                                         ByVal commandText As String, _
                                                         ByVal ParamArray commandParameters() As OracleParameter) As Integer
            'create a command and prepare it for execution
            Dim cmd As New OracleCommand
            Dim retval As Integer

            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters)

            'finally, execute the command.
            retval = cmd.ExecuteNonQuery()

            'detach the OracleParameters from the command object, so they can be used again
            cmd.Parameters.Clear()

            Return retval

        End Function 'ExecuteNonQuery

        ' Execute a stored procedure via a OracleCommand (that returns no resultset) against the specified OracleTransaction 
        ' using the provided parameter values.  This method will discover the parameters for the 
        ' stored procedure, and assign the values based on parameter order.
        ' This method provides no access to output parameters or the stored procedure's return value parameter.
        ' e.g.:  
        ' Dim result As Integer = SqlHelper.ExecuteNonQuery(trans, "PublishOrders", 24, 36)
        ' Parameters:
        ' -transaction - a valid OracleTransaction 
        ' -spName - the name of the stored procedure 
        ' -parameterValues - an array of objects to be assigned as the input values of the stored procedure 
        ' Returns: an int representing the number of rows affected by the command 
        Public Overloads Shared Function ExecuteNonQuery(ByVal transaction As OracleTransaction, _
                                                         ByVal spName As String, _
                                                         ByVal ParamArray parameterValues() As Object) As Integer
            Dim commandParameters As OracleParameter()

            'if we receive parameter values, we need to figure out where they go
            If Not (parameterValues Is Nothing) And parameterValues.Length > 0 Then
                'pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                'commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName)

                'assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues)

                'call the overload that takes an array of OracleParameters
                Return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters)
                'otherwise we can just call the SP without params
            Else
                Return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName)
            End If
        End Function 'ExecuteNonQuery

#End Region

    End Class



End Namespace
