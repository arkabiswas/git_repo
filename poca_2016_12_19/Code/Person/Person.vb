' Static Model

Namespace FDA.BusinessObjects

    Public Class Person

        Private _FirstName As String
        Private _LastName As String
        Private _EmailAddress As String
        Private _UserID As String
        Private _UserName As String
        Private _Password As Byte()
        Private _SettingsCollection As SettingsCollection
        Private _UserGroup As String

        Public Property FirstName() As String
            Get
                Return _FirstName
            End Get
            Set(ByVal Value As String)
                _FirstName = Value
            End Set
        End Property

        Public Property LastName() As String
            Get
                Return _LastName
            End Get
            Set(ByVal Value As String)
                _LastName = Value
            End Set
        End Property

        Public ReadOnly Property FullName() As String
            Get
                Return _FirstName + " " + _LastName
            End Get
        End Property

        Public Property EmailAddress() As String
            Get
                Return _EmailAddress
            End Get
            Set(ByVal Value As String)
                _EmailAddress = Value
            End Set
        End Property

        Public Property UserID() As String
            Get
                Return _UserID
            End Get
            Set(ByVal Value As String)
                _UserID = Value
            End Set
        End Property

        Public Property UserName() As String
            Get
                Return _UserName
            End Get
            Set(ByVal Value As String)
                _UserName = Value
            End Set
        End Property

        Public Property Password() As Byte()
            Get
                Return _Password
            End Get
            Set(ByVal Value As Byte())
                _Password = Value
            End Set
        End Property

        Public Property UserGroup() As String
            Get
                Return _UserGroup
            End Get
            Set(ByVal Value As String)
                _UserGroup = Value
            End Set
        End Property

        Public Property Settings() As SettingsCollection
            Get
                Return _SettingsCollection
            End Get
            Set(ByVal Value As SettingsCollection)
                _SettingsCollection = Value
            End Set
        End Property

    End Class ' END CLASS DEFINITION Person
End Namespace