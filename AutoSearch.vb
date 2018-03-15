Imports System.Web
Imports System.Data.SqlClient
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports TOWCommonFunction
Imports System.Data
Imports System.Collections.Generic
Imports System.Web.SessionState

<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://TransOnWeb.com/", Name:="GetDataSet")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class AutoSearch
    Inherits System.Web.Services.WebService
    Dim ObjComFun As New TOWCommonFunction()

    <WebMethod(EnableSession:=True)> _
    Public Function Get_PartyName(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim StrGetString As String = "Select distinct UPPER(Name)as Name from Tbl_Supplier where Name like '%" & prefixText & "%' and CompId = " & Context.Session("CompanyId").ToString() & ""
        Dim DtTable As New DataTable("DTVehiNo")
        If count = 0 Then
            count = 10
        End If
        DtTable = ObjComFun.GetTableBySqlQuery(StrGetString)
        Dim row As DataRow
        Dim returnData As List(Of String) = New List(Of String)
        For Each row In DtTable.Rows
            returnData.Add(row("Name"))
        Next
        Return returnData.ToArray()
    End Function

    <WebMethod()> _
    Public Function Get_Party_Name(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim StrGetString As String = " Select Distinct Name from Tbl_Supplier where Name like '" & prefixText & "%' "
        Dim DtTable As New DataTable()
        If count = 0 Then
            count = 10
        End If
        DtTable = ObjComFun.GetTableBySqlQuery(StrGetString)
        Dim row As DataRow
        Dim returnData As List(Of String) = New List(Of String)
        For Each row In DtTable.Rows
            returnData.Add(row("Name"))
        Next
        Return returnData.ToArray()
    End Function

    <WebMethod()> _
    Public Function GetCompletionList(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim StrGetString As String = " Select Distinct cityname from Mast_City where CityName  like '" & prefixText & "%' "
        Dim DtTable As New DataTable()
        If count = 0 Then
            count = 10
        End If
        DtTable = ObjComFun.GetTableBySqlQuery(StrGetString)
        Dim row As DataRow
        Dim returnData As List(Of String) = New List(Of String)
        For Each row In DtTable.Rows
            returnData.Add(row("cityname"))
        Next
        Return returnData.ToArray()
    End Function

    <WebMethod()> _
   Public Function GetDataSet(ByVal StrSqlQuery As String) As DataSet
        Dim DtSet As New DataSet()
        Dim DtAdapter As New SqlDataAdapter(StrSqlQuery, ObjComFun.Conn)
        DtAdapter.Fill(DtSet)
        Return DtSet
    End Function

    <WebMethod()> _
      Public Function GetDataSetLRC(ByVal StrSqlQuery As String) As DataSet
        Dim DtSet As New DataSet()
        Dim DtAdapter As New SqlDataAdapter(StrSqlQuery, ObjComFun.Conn)
        DtAdapter.Fill(DtSet)
        Return DtSet
    End Function

    <WebMethod(EnableSession:=True)> _
    Public Function GetGlobalItemNAme(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim StrGetString As String = "Select distinct Globalitemname from Mast_Globalitem where Globalitemname like '%" & prefixText & "%' "
        Dim DtTable As New DataTable("DTVehiNo")
        If count = 0 Then
            count = 10
        End If
        DtTable = ObjComFun.GetTableBySqlQuery(StrGetString)
        Dim row As DataRow
        Dim returnData As List(Of String) = New List(Of String)
        For Each row In DtTable.Rows
            returnData.Add(row("Globalitemname"))
        Next
        Return returnData.ToArray()
    End Function

    <WebMethod(EnableSession:=True)> _
    Public Function GetItemType(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim StrGetString As String = "Select distinct ItemType from Mast_ItemType where ItemType like '%" & prefixText & "%'"
        Dim DtTable As New DataTable("DTVehiNo")
        If count = 0 Then
            count = 10
        End If
        DtTable = ObjComFun.GetTableBySqlQuery(StrGetString)
        Dim row As DataRow
        Dim returnData As List(Of String) = New List(Of String)
        For Each row In DtTable.Rows
            returnData.Add(row("ItemType"))
        Next
        Return returnData.ToArray()
    End Function

    <WebMethod(EnableSession:=True)> _
    Public Function GetCompanyName(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim StrGetString As String = "Select distinct CompanyName from Mast_Item_Comp where CompanyName like '%" & prefixText & "%'"
        Dim DtTable As New DataTable("DTVehiNo")
        If count = 0 Then
            count = 10
        End If
        DtTable = ObjComFun.GetTableBySqlQuery(StrGetString)
        Dim row As DataRow
        Dim returnData As List(Of String) = New List(Of String)
        For Each row In DtTable.Rows
            returnData.Add(row("CompanyName"))
        Next
        Return returnData.ToArray()
    End Function

    'Created By Lokesh on dated : 22/06/2016
    <WebMethod(EnableSession:=True)> _
    Public Function GetSpareNAme(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim StrGetString As String = "Select distinct UPPER(GlobalItemName)as GlobalItemName from Mast_GlobalItem where GlobalItemName like '%" & prefixText & "%' and CompId = " & Context.Session("CompanyId").ToString() & " order by GlobalItemName"
        Dim DtTable As New DataTable("DTVehiNo")
        If count = 0 Then
            count = 10
        End If
        DtTable = ObjComFun.GetTableBySqlQuery(StrGetString)
        Dim row As DataRow
        Dim returnData As List(Of String) = New List(Of String)
        For Each row In DtTable.Rows
            returnData.Add(row("GlobalItemName"))
        Next
        Return returnData.ToArray()
    End Function

    <WebMethod(EnableSession:=True)> _
    Public Function GetPONumber(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim StrGetString As String = "Select distinct UPPER(PurOrderId)as PurOrderId from tbl_PurchaseOrderMaiN where PurOrderId like '%" & prefixText & "%' and CompId = " & Context.Session("CompanyId").ToString() & " order by PurOrderId"
        Dim DtTable As New DataTable("DTVehiNo")
        If count = 0 Then
            count = 10
        End If
        DtTable = ObjComFun.GetTableBySqlQuery(StrGetString)
        Dim row As DataRow
        Dim returnData As List(Of String) = New List(Of String)
        For Each row In DtTable.Rows
            returnData.Add(row("PurOrderId"))
        Next
        Return returnData.ToArray()
    End Function

    <WebMethod(EnableSession:=True)> _
    Public Function GetGRNNumber(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim StrGetString As String = "Select distinct UPPER(PINOVB)as PINOVB from Item_Purc where PINOVB like '%" & prefixText & "%' and CompId = " & Context.Session("CompanyId").ToString() & " order by PINOVB"
        Dim DtTable As New DataTable("DTVehiNo")
        If count = 0 Then
            count = 10
        End If
        DtTable = ObjComFun.GetTableBySqlQuery(StrGetString)
        Dim row As DataRow
        Dim returnData As List(Of String) = New List(Of String)
        For Each row In DtTable.Rows
            returnData.Add(row("PINOVB"))
        Next
        Return returnData.ToArray()
    End Function

    <WebMethod(EnableSession:=True)> _
    Public Function GetVehicleCompletionList(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim StrGetString As String = "Select distinct UnitCode from Tbl_Unit where UnitCode like '%" & prefixText & "%' "
        Dim DtTable As New DataTable("DTVehiNo")
        If count = 0 Then
            count = 10
        End If
        DtTable = ObjComFun.GetTableBySqlQuery(StrGetString)
        Dim row As DataRow
        Dim returnData As List(Of String) = New List(Of String)
        For Each row In DtTable.Rows
            returnData.Add(row("UnitCode"))
        Next
        Return returnData.ToArray()
    End Function
End Class
