
Namespace FDA.BusinessObjects
    ' This is an array of Settings.
    Public Class SettingsCollection
        Inherits CollectionBase

        Default Public Property Item(ByVal index As Integer) As SettingType
            Get
                Return CType(List(index), SettingType)
            End Get
            Set(ByVal Value As SettingType)
                List(index) = Value
            End Set
        End Property

        Public Function Add(ByVal value As SettingType) As Integer
            Return List.Add(value)
        End Function

        Public Sub Insert(ByVal index As Integer, _
            ByVal value As SettingType)
            List.Insert(index, value)
        End Sub

        Public Function IndexOf(ByVal value As SettingType) As Integer
            Return List.IndexOf(value)
        End Function

        Public Sub Remove(ByVal value As SettingType)
            List.Remove(value)
        End Sub

        Public Function Contains(ByVal value As SettingType) As Boolean
            Return List.Contains(value)
        End Function

    End Class ' END CLASS DEFINITION SettingsCollection
End Namespace