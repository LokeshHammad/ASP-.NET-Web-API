Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Collections.Generic
Imports System.Net
Imports System.Security.Cryptography

Public Class TOWCommonFunction
#Region "Variable"
    Public Conn As New SqlConnection()
    Public companyId As Integer
    Public DSN As String
    Public LoginUserName As String
    Public gCompId As Long
    Public gWardId As Long
    Public Database, Database1 As String
    Public AdminUsrId, AdminUsrId1, LoginUserId, LoginUserNewName As String
    Public AdminPassword As String
    Public ServerName As String
    Public Databasecode As String
    Public Adminrights As Int32
    Public DateTime, LoginDateTime As DateTime
    Public MenuName, StrMenuName As String
    Public vr_deleteExcpn As String = "THE DELETE STATEMENT CONFLICTED WITH THE REFERENCE CONSTRAINT"

    'Public AddEditMode As Integer
    Public CountMode As Integer
    Public txtDatePicker As TextBox
    Public CoonecStr As String
    Public loginTime As DateTime
    'Public Shared MyMachineIP As New String("")
#End Region
    Public Sub Connect()
        If Conn.State = ConnectionState.Open Then
            Conn.Close()
        End If
        CoonecStr = ConfigurationManager.AppSettings.Get("ConAntonyBombay").ToString()
        Conn.ConnectionString = CoonecStr
        If Conn.State = ConnectionState.Closed Then
            Conn.Open()
        End If
    End Sub
    Public Sub DisConnect()
        If Conn.State = ConnectionState.Open Then
            Conn.Close()
        End If

    End Sub
    Public Shared Function TowFormatNumber(ByVal Value As Double) As Double
        Dim TOWVALUE As Double = FormatNumber(Value, 3)
        Return TOWVALUE
    End Function
    Public Function GetDataSetBySqlQuery(ByVal StrSql As String) As DataSet
        Connect()
        Dim DtSet As New DataSet()
        Dim DtAdapter As New SqlDataAdapter(StrSql, Conn)
        DtAdapter.Fill(DtSet)
        Return DtSet
        DisConnect()
    End Function
    Public Function GetColumn1BySqlQuery(ByVal StrSql As String) As String
        Dim DtRdr As SqlDataReader
        Dim strval As String = Nothing
        Connect()
        Dim Cmd As New SqlCommand(StrSql, Conn)
        DtRdr = Cmd.ExecuteReader()
        If DtRdr.Read() Then
            strval = DtRdr.GetValue(0).ToString()
        End If
        DtRdr.Close()
        DisConnect()
        Return strval
    End Function
    Public Function GetColumn2BySqlQuery(ByVal StrSql As String) As String()
        Dim DtRdr As SqlDataReader
        Connect()
        Dim returnData As List(Of String) = New List(Of String)
        Dim Cmd As New SqlCommand(StrSql, Conn)
        DtRdr = Cmd.ExecuteReader()
        If DtRdr.Read() Then
            Dim strval1 As String = DtRdr.GetValue(0).ToString()
            Dim strval2 As String = DtRdr.GetValue(1).ToString()
            returnData.Add(strval1)
            returnData.Add(strval2)
        End If
        DtRdr.Close()
        DisConnect()
        Return returnData.ToArray()
    End Function
    Public Function GetColumn3BySqlQuery(ByVal StrSql As String) As String()
        Dim DtRdr As SqlDataReader
        Connect()
        Dim returnData As List(Of String) = New List(Of String)
        Dim Cmd As New SqlCommand(StrSql, Conn)
        DtRdr = Cmd.ExecuteReader()
        If DtRdr.Read() Then
            Dim strval1 As String = DtRdr.GetValue(0).ToString()
            Dim strval2 As String = DtRdr.GetValue(1).ToString()
            Dim strval3 As String = DtRdr.GetValue(2).ToString()
            returnData.Add(strval1)
            returnData.Add(strval2)
            returnData.Add(strval3)
        End If
        DtRdr.Close()
        DisConnect()
        Return returnData.ToArray()
    End Function
    Public Function GetColumn4BySqlQuery(ByVal StrSql As String) As String()
        Dim DtRdr As SqlDataReader
        Connect()
        Dim returnData As List(Of String) = New List(Of String)
        Dim Cmd As New SqlCommand(StrSql, Conn)
        DtRdr = Cmd.ExecuteReader()
        If DtRdr.Read() Then
            Dim strval1 As String = DtRdr.GetValue(0).ToString()
            Dim strval2 As String = DtRdr.GetValue(1).ToString()
            Dim strval3 As String = DtRdr.GetValue(2).ToString()
            Dim strval4 As String = DtRdr.GetValue(3).ToString()
            returnData.Add(strval1)
            returnData.Add(strval2)
            returnData.Add(strval3)
            returnData.Add(strval4)
        End If
        DtRdr.Close()
        DisConnect()
        Return returnData.ToArray()
    End Function
    Public Function GetColumn5BySqlQuery(ByVal StrSql As String) As String()
        Dim DtRdr As SqlDataReader
        Connect()
        Dim returnData As List(Of String) = New List(Of String)
        Dim Cmd As New SqlCommand(StrSql, Conn)
        DtRdr = Cmd.ExecuteReader()
        If DtRdr.Read() Then
            Dim strval1 As String = DtRdr.GetValue(0).ToString()
            Dim strval2 As String = DtRdr.GetValue(1).ToString()
            Dim strval3 As String = DtRdr.GetValue(2).ToString()
            Dim strval4 As String = DtRdr.GetValue(3).ToString()
            Dim strval5 As String = DtRdr.GetValue(4).ToString()
            returnData.Add(strval1)
            returnData.Add(strval2)
            returnData.Add(strval3)
            returnData.Add(strval4)
            returnData.Add(strval5)
        End If
        DtRdr.Close()
        DisConnect()
        Return returnData.ToArray()
    End Function
    Public Function GetColumn6BySqlQuery(ByVal StrSql As String) As String()
        Dim DtRdr As SqlDataReader
        Connect()
        Dim returnData As List(Of String) = New List(Of String)
        Dim Cmd As New SqlCommand(StrSql, Conn)
        DtRdr = Cmd.ExecuteReader()
        If DtRdr.Read() Then
            Dim strval1 As String = DtRdr.GetValue(0).ToString()
            Dim strval2 As String = DtRdr.GetValue(1).ToString()
            Dim strval3 As String = DtRdr.GetValue(2).ToString()
            Dim strval4 As String = DtRdr.GetValue(3).ToString()
            Dim strval5 As String = DtRdr.GetValue(4).ToString()
            Dim strval6 As String = DtRdr.GetValue(5).ToString()


            returnData.Add(strval1)
            returnData.Add(strval2)
            returnData.Add(strval3)
            returnData.Add(strval4)
            returnData.Add(strval5)
            returnData.Add(strval6)
        End If
        DtRdr.Close()
        DisConnect()
        Return returnData.ToArray()
    End Function
    Public Function GetColumn8BySqlQuery(ByVal StrSql As String) As String()
        Dim DtRdr As SqlDataReader
        Connect()
        Dim returnData As List(Of String) = New List(Of String)
        Dim Cmd As New SqlCommand(StrSql, Conn)
        DtRdr = Cmd.ExecuteReader()
        If DtRdr.Read() Then
            Dim strval1 As String = DtRdr.GetValue(0).ToString()
            Dim strval2 As String = DtRdr.GetValue(1).ToString()
            Dim strval3 As String = DtRdr.GetValue(2).ToString()
            Dim strval4 As String = DtRdr.GetValue(3).ToString()
            Dim strval5 As String = DtRdr.GetValue(4).ToString()
            Dim strval6 As String = DtRdr.GetValue(5).ToString()
            Dim strval7 As String = DtRdr.GetValue(6).ToString()
            Dim strval8 As String = DtRdr.GetValue(7).ToString()


            returnData.Add(strval1)
            returnData.Add(strval2)
            returnData.Add(strval3)
            returnData.Add(strval4)
            returnData.Add(strval5)
            returnData.Add(strval6)
            returnData.Add(strval7)
            returnData.Add(strval8)
        End If
        DtRdr.Close()
        DisConnect()
        Return returnData.ToArray()
    End Function
    Public Function GetColumn9BySqlQuery(ByVal StrSql As String) As String()
        Dim DtRdr As SqlDataReader
        Connect()
        Dim returnData As List(Of String) = New List(Of String)
        Dim Cmd As New SqlCommand(StrSql, Conn)
        DtRdr = Cmd.ExecuteReader()
        If DtRdr.Read() Then
            Dim strval1 As String = DtRdr.GetValue(0).ToString()
            Dim strval2 As String = DtRdr.GetValue(1).ToString()
            Dim strval3 As String = DtRdr.GetValue(2).ToString()
            Dim strval4 As String = DtRdr.GetValue(3).ToString()
            Dim strval5 As String = DtRdr.GetValue(4).ToString()
            Dim strval6 As String = DtRdr.GetValue(5).ToString()
            Dim strval7 As String = DtRdr.GetValue(6).ToString()
            Dim strval8 As String = DtRdr.GetValue(7).ToString()
            Dim strval9 As String = DtRdr.GetValue(8).ToString()

            returnData.Add(strval1)
            returnData.Add(strval2)
            returnData.Add(strval3)
            returnData.Add(strval4)
            returnData.Add(strval5)
            returnData.Add(strval6)
            returnData.Add(strval7)
            returnData.Add(strval8)
            returnData.Add(strval9)
        End If
        DtRdr.Close()
        DisConnect()
        Return returnData.ToArray()
    End Function
    Public Function GetTableBySqlQuery(ByVal StrSql As String) As DataTable
        Connect()
        Dim DtSet As New DataSet()
        Dim DtTable As New DataTable()
        Dim DtAdapter As New SqlDataAdapter(StrSql, Conn)
        DtAdapter.Fill(DtSet, "DtTable")
        DtTable = DtSet.Tables("DtTable")
        DisConnect()
        Return DtTable
    End Function
    Public Sub FillDropDownList(ByVal DropDownName As DropDownList, ByVal StrSql As String, ByVal Name As String, ByVal Code As String)
        Connect()
        Dim DtSet As New DataSet()
        Dim DtTable As New DataTable()
        Dim DtAdapter As New SqlDataAdapter(StrSql, Conn)
        DtAdapter.Fill(DtSet, "DtTable")
        DtTable = DtSet.Tables("DtTable")
        DropDownName.DataSource = DtTable
        DropDownName.DataTextField = Name
        DropDownName.DataValueField = Code
        DropDownName.DataBind()
        DisConnect()
    End Sub
    Public Sub FillCheckBoxList(ByVal CheckBoxList As CheckBoxList, ByVal StrSql As String, ByVal Name As String, ByVal Code As String)
        Connect()
        Dim DtSet As New DataSet()
        Dim DtTable As New DataTable()
        Dim DtAdapter As New SqlDataAdapter(StrSql, Conn)
        DtAdapter.Fill(DtSet, "DtTable")
        DtTable = DtSet.Tables("DtTable")
        CheckBoxList.DataSource = DtTable
        CheckBoxList.DataTextField = Name
        CheckBoxList.DataValueField = Code
        CheckBoxList.DataBind()
        DisConnect()
    End Sub
    Public Sub FillRadioBtnList(ByVal RadioBtnLstName As RadioButtonList, ByVal StrSql As String, ByVal Name As String, ByVal Code As String)
        Connect()
        Dim DtSet As New DataSet()
        Dim DtTable As New DataTable()
        Dim DtAdapter As New SqlDataAdapter(StrSql, Conn)
        DtAdapter.Fill(DtSet, "DtTable")
        DtTable = DtSet.Tables("DtTable")
        RadioBtnLstName.DataSource = DtTable
        RadioBtnLstName.DataTextField = Name
        RadioBtnLstName.DataValueField = Code
        RadioBtnLstName.DataBind()
        DisConnect()
    End Sub

    Public Sub FillList(ByVal ListName As ListBox, ByVal StrSql As String, ByVal Name As String, ByVal Code As String)
        Connect()
        Dim DtSet As New DataSet()
        Dim DtTable As New DataTable()
        Dim DtAdapter As New SqlDataAdapter(StrSql, Conn)
        DtAdapter.Fill(DtSet, "DtTable")
        DtTable = DtSet.Tables("DtTable")
        ListName.DataSource = DtTable
        ListName.DataTextField = Name
        ListName.DataValueField = Code
        ListName.DataBind()
        DisConnect()
    End Sub
    Public Function GetDataList(ByVal ListName As ListBox, ByVal StrSql As String, ByVal Name As String, ByVal Code As String) As DataList
        Connect()
        Dim DtSet As New DataSet()
        Dim Dtlist As New DataList()
        Dim DtTable As New DataTable()
        Dim DtAdapter As New SqlDataAdapter(StrSql, Conn)
        DtAdapter.Fill(DtSet, "DtTable")
        DtTable = DtSet.Tables("DtTable")
        ListName.DataSource = DtTable
        ListName.DataTextField = Name
        ListName.DataValueField = Code
        ListName.DataBind()
        Return Dtlist
        DisConnect()
    End Function
    Public Function GetList(ByVal TextBoxSearch As ListBox, ByVal StrSql As String, ByVal TableName As String, ByVal ColumnName As String, ByVal ColumnValue As String) As DataList
        Dim StrGetString As String = " Select " & ColumnName & " from " & TableName & " where " & ColumnName & " like '" & StrSql & "%'"
        Dim DtList As New DataList()
        DtList = GetDataList(TextBoxSearch, StrGetString, ColumnName, ColumnValue)
        Return DtList
    End Function

    Public Function CheckDupliCateValueforUpdate(ByVal TableForDupli As String, ByVal ColumnName1 As String, ByVal IdentityCol As String, ByVal ColumnValue1 As String, ByVal IdentityColvalue As String) As Boolean
        Connect()
        Dim StrDuplivalue As String
        StrDuplivalue = " Select " & ColumnName1 & "," & IdentityCol & " from " & TableForDupli & " where  " & ColumnName1 & " = '" & ColumnValue1 & "' and  " & IdentityCol & " <> " & Convert.ToInt16(IdentityColvalue)
        Dim TableRecord As New DataTable()
        TableRecord = GetTableBySqlQuery(StrDuplivalue)
        If TableRecord.Rows.Count = 0 Then
            Return False
        Else
            Return True
        End If
        DisConnect()
    End Function

    Public Function CheckDupliCateValueforInsert(ByVal TableForDupli As String, ByVal ColumnName1 As String, ByVal ColumnValue1 As String) As Boolean
        Connect()
        Dim Rowcount As Integer = 0
        Dim colValue1 As String = ""
        Dim colValue2 As Integer = 0
        Dim StrDuplivalue As String
        StrDuplivalue = " Select " & ColumnName1 & " from " & TableForDupli & " where  " & ColumnName1 & " = '" & ColumnValue1 & "' "
        Dim TableRecord As New DataTable()
        TableRecord = GetTableBySqlQuery(StrDuplivalue)
        If TableRecord.Rows.Count = 0 Then
            Return False
        Else
            Return True
        End If
        DisConnect()
    End Function
    Public Function CheckDupliCateValueforInsertWithCriteria(ByVal TableForDupli As String, ByVal ColumnName1 As String, ByVal Criteria As String) As Boolean
        Connect()
        Dim Rowcount As Integer = 0
        Dim colValue1 As String = ""
        Dim colValue2 As Integer = 0
        Dim StrDuplivalue As String
        StrDuplivalue = " Select " & ColumnName1 & " from " & TableForDupli & " " & Criteria
        Dim TableRecord As New DataTable()
        TableRecord = GetTableBySqlQuery(StrDuplivalue)
        If TableRecord.Rows.Count = 0 Then
            Return False
        Else
            Return True
        End If
        DisConnect()
    End Function

    Public Function CheckDupliCateValueforUpdateWithCriteria(ByVal TableForDupli As String, ByVal ColumnName1 As String, ByVal Criteria As String) As Boolean
        Connect()
        Dim Rowcount As Integer = 0
        Dim colValue1 As String = ""
        Dim colValue2 As Integer = 0
        Dim StrDuplivalue As String
        StrDuplivalue = " Select " & ColumnName1 & " from " & TableForDupli & Criteria
        Dim TableRecord As New DataTable()
        TableRecord = GetTableBySqlQuery(StrDuplivalue)
        If TableRecord.Rows.Count = 0 Then
            Return False
        Else
            Return True
        End If
        DisConnect()
    End Function
    Public Function GetDataByProcedure(ByVal qry As String, ByVal PN1 As String, ByVal PV1 As String, ByVal PN2 As String, ByVal PV2 As String, ByVal PN3 As String, ByVal PV3 As String, ByVal PN4 As String, ByVal PV4 As String, ByVal PN5 As String, ByVal PV5 As String) As DataTable
        Dim ds As New DataSet()
        Dim dt As New DataTable()
        Connect()
        Dim cmd As New SqlCommand(qry, Conn)
        cmd.CommandType = CommandType.StoredProcedure
        If Not PN1 = "0" Then
            cmd.Parameters.AddWithValue(PN1, PV1)
        End If
        If Not PN2 = "0" Then
            cmd.Parameters.AddWithValue(PN2, PV2)
        End If
        If Not PN3 = "0" Then
            cmd.Parameters.AddWithValue(PN3, PV3)
        End If
        If Not PN4 = "0" Then
            cmd.Parameters.AddWithValue(PN4, PV4)
        End If
        If Not PN5 = "0" Then
            cmd.Parameters.AddWithValue(PN5, PV5)
        End If
        Dim da As New SqlDataAdapter()
        da.SelectCommand = cmd
        da.Fill(ds, "dt")
        dt = ds.Tables("dt")
        DisConnect()
        Return dt
    End Function
    Public Sub GetGrid(ByVal Grdview As GridView, ByVal StrSql As String)
        Dim DtSet As New DataSet()
        Dim DtTable As New DataTable()
        Connect()
        Dim DtAdapter As New SqlDataAdapter(StrSql, Conn)
        DtAdapter.Fill(DtSet, "DtTable")
        DtTable = DtSet.Tables("DtTable")
        Grdview.DataSource = DtTable
        Grdview.DataBind()
        DisConnect()
    End Sub
    Public Function ReturnGrid(ByVal Grdview As GridView, ByVal StrSql As String) As GridView
        Dim DtSet As New DataSet()
        Dim DtTable As New DataTable()
        Dim Grdvw As New GridView()
        Connect()
        Dim DtAdapter As New SqlDataAdapter(StrSql, Conn)
        DtAdapter.Fill(DtSet, "DtTable")
        DtTable = DtSet.Tables("DtTable")
        Grdview.DataSource = DtTable
        Grdview.DataBind()
        Grdvw = Grdview.DataSource
        DisConnect()
        Return Grdvw
    End Function

    Public Function ExecuteQuery(ByVal StrSql As String, ByVal Commtimeout As Integer) As Integer
        Dim Cmd As New SqlCommand()
        Dim IntQuery As Integer
        Connect()
        Cmd.Connection = Conn
        Cmd.CommandType = CommandType.Text
        Cmd.CommandText = StrSql
        Cmd.CommandTimeout = Commtimeout
        IntQuery = Cmd.ExecuteNonQuery()
        DisConnect()
        Return IntQuery
    End Function

    Public Function ExecuteProcedure(ByVal StrSql As String) As SqlCommand
        Connect()
        Dim cmd As New SqlCommand()
        cmd.Connection = Conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = StrSql
        'cmd.ExecuteNonQuery();
        Return (cmd)
    End Function
    Public Function ExecuteQuery(ByVal StrSql As String) As Integer
        Dim Cmd As New SqlCommand()
        Dim IntQuery As Integer
        Connect()
        Cmd.Connection = Conn
        Cmd.CommandType = CommandType.Text
        Cmd.CommandText = StrSql
        IntQuery = Cmd.ExecuteNonQuery()
        DisConnect()
        Return IntQuery
    End Function
    Public Function GetStringToDateNow(ByVal Vardate As String) As DateTime
        'Dim SqlDateNull As SqlDateTime
        Dim dd As String = ""
        Dim MM As String = ""
        Dim yyyy As String = ""
        If Vardate.Trim().Length = 0 Then
            Return Date.Now.Date
        End If
        dd = Vardate.Substring(0, 2)
        If dd.Length = 2 Then
            MM = Vardate.Substring(2, 2)
        ElseIf dd.Length = 3 Then
            MM = Vardate.Substring(4, 2)
        End If
        yyyy = Vardate.Substring(5, 4)
        Dim Mdate As New DateTime()
        'Mdate = Convert.ToDateTime(MM & " " & dd & " " & yyyy)
        Vardate = Date.Parse(Vardate)
        Return Vardate
    End Function
    Public Function GetMonth(ByVal MonthNo As Integer) As String
        Dim mName As String = ""
        Select Case (MonthNo)
            Case 1
                mName = "JAN"
            Case 2
                mName = "FEB"

            Case 3
                mName = "MAR"
            Case 4
                mName = "APR"
            Case 5
                mName = "MAY"
            Case 6
                mName = "JUN"
            Case 7
                mName = "JUL"
            Case 8
                mName = "AUG"
            Case 9
                mName = "SEP"
            Case 10
                mName = "OCT"
            Case 11
                mName = "NOV"
            Case 12
                mName = "DEC"
        End Select
        Return mName
    End Function
    Public Function GetStringToDate(ByVal Vardate As String) As DateTime
        Dim Mdate As DateTime
        Mdate = Date.ParseExact(Vardate, "dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
        Return Mdate
    End Function
    Public Function GetStringToDateNull(ByVal Vardate As String) As SqlDateTime
        Dim Mdate As SqlDateTime
        If Vardate.Trim().Length = 0 Then
            Mdate = SqlDateTime.Null
        Else
            Mdate = Date.ParseExact(Vardate, "'dd/MM/yyyy'", New Globalization.CultureInfo("en-US"))
        End If
        Return Mdate
    End Function
    Public Function CheckImageSize(ByVal ImageSize As Integer) As Boolean

        If ImageSize > 700000 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function GetDataByProc(ByVal qry As String, ByVal ParamName As String(), ByVal ParamValue As String()) As DataTable
        Dim ds As New DataSet()
        Dim dt As New DataTable()
        Connect()
        Dim cmd As New SqlCommand(qry, Conn)
        cmd.CommandType = CommandType.StoredProcedure
        If ParamName(0) <> "NULL" Then
            Dim i As Integer = 0, j As Integer = 0
            While i < ParamName.Length
                cmd.Parameters.AddWithValue(ParamName(i), ParamValue(j))
                i += 1
                j += 1
            End While
        End If
        Dim da As New SqlDataAdapter()
        da.SelectCommand = cmd
        da.Fill(ds, "dt")
        dt = ds.Tables("dt")
        DisConnect()
        Return dt
    End Function

    Public Function GetUniqueKey(maxSize As Integer) As [String]
        Dim chars As Char() = New Char(61) {}
        chars = "abcdefghijklmnopqrstuvwxyz1234567890".ToCharArray()
        Dim data As Byte() = New Byte(0) {}
        Dim crypto As New RNGCryptoServiceProvider()
        crypto.GetNonZeroBytes(data)
        data = New Byte(maxSize - 1) {}
        crypto.GetNonZeroBytes(data)
        Dim result As New StringBuilder(maxSize)
        For Each b As Byte In data
            result.Append(chars(b Mod (chars.Length)))
        Next
        Return result.ToString()
    End Function
End Class
