Imports System
Imports System.Collections
Imports System.Configuration
Imports System.Text
Imports System.Text.RegularExpressions

<Serializable()> _
    Public Class SearchEngine
    ' Methods
    Public Sub New()
        Me.m_ItemsPerPage = Integer.Parse(ConfigurationManager.AppSettings.Item("DefaultItemsPerPage"))
        Me.m_TextSearch = False
        Me.m_OrthoSearch = False
        Me.m_OrthoWeight = -1
        Me.m_PhoneSearch = False
        Me.m_PhoneticWeight = -1
        Me.m_AddFaxSearch = False
        Me.m_AddFaxWeight = -1
    End Sub

    Public Shared Function ConvertAline(ByVal InputString As String) As String
        Dim text1 As String = "[tdkgpbszfvmnlr]"
        Dim text3 As String = InputString
        text3 = Regex.Replace(text3, ("a(" & text1 & ")e$"), "ey$1")
        text3 = Regex.Replace(text3, ("e(" & text1 & ")e$"), "iy$1")
        text3 = Regex.Replace(text3, ("i(" & text1 & ")e$"), "ow$1")
        text3 = Regex.Replace(text3, ("o(" & text1 & ")e$"), "ay$1")
        text3 = Regex.Replace(text3, ("u(" & text1 & ")e$"), "u$1")
        Return Regex.Replace(text3, ("(" & text1 & ")\1"), "$1")
    End Function

    Public Shared Function ConvertEnglish(ByVal InputString As String) As String
        Dim text1 As String = "[tdkgpbszfvmnlr]"
        Dim text3 As String = InputString
        text3 = Regex.Replace(text3, "(ce)", "se")
        text3 = Regex.Replace(text3, "(ci)", "si")
        text3 = Regex.Replace(text3, "(cy)", "si")
        text3 = Regex.Replace(text3, "(che)", "ke")
        text3 = Regex.Replace(text3, "(chi)", "ki")
        text3 = Regex.Replace(text3, "(ch)", "=")
        text3 = Regex.Replace(text3, "(ck)", "k")
        text3 = Regex.Replace(text3, "(c)", "k")
        text3 = Regex.Replace(text3, "(=)", "cV")
        text3 = Regex.Replace(text3, "(j)", "jV")
        text3 = Regex.Replace(text3, "(gg)", "ge")
        text3 = Regex.Replace(text3, "(ge)", "jVe")
        text3 = Regex.Replace(text3, "(gi)", "jVi")
        text3 = Regex.Replace(text3, "(gy)", "jVi")
        text3 = Regex.Replace(text3, "(qu)", "kw")
        text3 = Regex.Replace(text3, "(q)", "k")
        text3 = Regex.Replace(text3, "(ss)", "s")
        text3 = Regex.Replace(text3, "(sh)", "sV")
        text3 = Regex.Replace(text3, "(zh)", "zV")
        text3 = Regex.Replace(text3, "(ph)", "f")
        text3 = Regex.Replace(text3, "(th)", "sD")
        text3 = Regex.Replace(text3, "(ou)", "aw")
        text3 = Regex.Replace(text3, "(ee)", "i")
        text3 = Regex.Replace(text3, "(oo)", "u")
        text3 = Regex.Replace(text3, "(eu)", "u")
        text3 = Regex.Replace(text3, "^x", "z")
        text3 = Regex.Replace(text3, "x", "ks")
        text3 = Regex.Replace(text3, "(ng)", "gN")
        text3 = Regex.Replace(text3, ("a(" & text1 & ")e$"), "ey$1")
        text3 = Regex.Replace(text3, ("e(" & text1 & ")e$"), "iy$1")
        text3 = Regex.Replace(text3, ("i(" & text1 & ")e$"), "ow$1")
        text3 = Regex.Replace(text3, ("o(" & text1 & ")e$"), "ay$1")
        text3 = Regex.Replace(text3, ("u(" & text1 & ")e$"), "u$1")
        Return Regex.Replace(text3, ("(" & text1 & ")\1"), "$1")
    End Function

    Public Shared Function NormalizeString(ByVal inputStringValue As String) As String
        If ((inputStringValue Is Nothing) Or (inputStringValue.Length < 0)) Then
            Throw New ApplicationException("You can not send a zero length string")
        End If

        Dim builder1 As New StringBuilder
        Dim regex1 As New Regex("[A-Za-z]*")
        Dim mtch As System.Text.RegularExpressions.Match
        Dim mtchCol As MatchCollection = regex1.Matches(inputStringValue.ToLower)

        If mtchCol.Count > 0 Then
            Try
                For Each mtch In mtchCol
                    If mtch.Success Then
                        builder1.Append(mtch.Value)
                    End If

                Next
            Catch ex As Exception
                Return String.Empty
            End Try
        End If
        Return builder1.ToString()
    End Function


    ' Properties
    Public Property AddFaxSearch() As Boolean
        Get
            Return Me.m_AddFaxSearch
        End Get
        Set(ByVal Value As Boolean)
            Me.m_AddFaxSearch = Value
        End Set
    End Property

    Public Property AdditionalFactorsWeight() As Integer
        Get
            Return Me.m_AddFaxWeight
        End Get
        Set(ByVal Value As Integer)
            Me.m_AddFaxWeight = Value
        End Set
    End Property

    Public Property CurrentUserId() As String
        Get
            Return Me.m_CurrUserId
        End Get
        Set(ByVal Value As String)
            Me.m_CurrUserId = Value
        End Set
    End Property

    Public Property DataSources() As String
        Get
            Return Me.m_DataSources
        End Get
        Set(ByVal Value As String)
            Me.m_DataSources = Value
        End Set
    End Property

    Public Property ItemsPerPage() As Integer
        Get
            Return Me.m_ItemsPerPage
        End Get
        Set(ByVal Value As Integer)
            Me.m_ItemsPerPage = Value
        End Set
    End Property

    Public Property OrthographicWeight() As Integer
        Get
            Return Me.m_OrthoWeight
        End Get
        Set(ByVal Value As Integer)
            Me.m_OrthoWeight = Value
        End Set
    End Property

    Public Property OrthoSearch() As Boolean
        Get
            Return Me.m_OrthoSearch
        End Get
        Set(ByVal Value As Boolean)
            Me.m_OrthoSearch = Value
        End Set
    End Property

    Public Property PhoneticSearch() As Boolean
        Get
            Return Me.m_PhoneSearch
        End Get
        Set(ByVal Value As Boolean)
            Me.m_PhoneSearch = Value
        End Set
    End Property

    Public Property PhoneticWeight() As Integer
        Get
            Return Me.m_PhoneticWeight
        End Get
        Set(ByVal Value As Integer)
            Me.m_PhoneticWeight = Value
        End Set
    End Property

    Public Property ResultThreshold() As Integer
        Get
            Return Me.m_ResultThreshold
        End Get
        Set(ByVal Value As Integer)
            Me.m_ResultThreshold = Value
        End Set
    End Property

    Public Property SearchResults() As String
        Get
            Return Me.m_SearchResults
        End Get
        Set(ByVal Value As String)
            Me.m_SearchResults = Value
        End Set
    End Property

    Public Property SearchTerm() As String
        Get
            Return Me.m_SearchTerm
        End Get
        Set(ByVal Value As String)
            Me.m_SearchTerm = Value
        End Set
    End Property

    Public ReadOnly Property SearchTermNormal() As String
        Get
            Return SearchEngine.NormalizeString(Me.m_SearchTerm)
        End Get
    End Property

    Public ReadOnly Property SearchTermPhonetic() As String
        Get
            Return SearchEngine.ConvertEnglish(SearchEngine.NormalizeString(Me.m_SearchTerm))
        End Get
    End Property

    Public Property TextSearch() As Boolean
        Get
            Return Me.m_TextSearch
        End Get
        Set(ByVal Value As Boolean)
            Me.m_TextSearch = Value
        End Set
    End Property


    ' Fields
    Private m_AddFaxSearch As Boolean
    Private m_AddFaxWeight As Integer
    Private m_CurrUserId As String
    Private m_DataSources As String
    Private m_ItemsPerPage As Integer
    Private m_OrthoSearch As Boolean
    Private m_OrthoWeight As Integer
    Private m_PhoneSearch As Boolean
    Private m_PhoneticWeight As Integer
    Private m_ResultThreshold As Integer
    Private m_SearchResults As String
    Private m_SearchTerm As String
    Private m_TextSearch As Boolean
End Class
