Imports System.Data.SqlClient
Imports TOWCommonFunction
Imports System.Data
Imports System.Data.SqlTypes

Partial Class fgrn
    Inherits System.Web.UI.Page
    Dim ObjComFun As New TOWCommonFunction()
    Dim SqlCon As New SqlConnection(ObjComFun.CoonecStr)
    Public Shared AddEditMode As Integer
    Dim GRNID As Integer
    Dim cnt As Integer
    Dim strSql As String
    Dim DT_GRN As DataTable
    Dim ParamName As String()
    Dim ParamValue As String()
    Dim booleanDupli As Boolean
    Dim Str As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            cnt = 0
            txtmsg.Text = ""
            If Session("UserId") = "" Then
                Response.Redirect("~/FrmLogin.aspx?A=2")
            End If
            btnsave.Attributes.Add("onclick", "return forSave()")
            ShowRecords()
            If Not IsPostBack Then
                txtInvoicedate.Text = Now.Date.ToString("dd/MM/yyyy")
                AddEditMode = 0
                lblSesion.Text = "1"
                Session("DT_GRN") = ""
                DT_GRN = New DataTable(Session("DT_GRN"))
                Fill_ddl()
                GetMaxNo()
                txtSerialNo.Focus()
                CheckRights()
                If Not Request.QueryString("A") = Nothing Then
                    Dim id As String = Request.QueryString("A").ToString()
                    Fill_AllControls(id)
                    pnlMain.Enabled = False
                    btnsave.Enabled = False
                End If
                If Not Request.QueryString("B") = Nothing Then
                    Dim id As String = Request.QueryString("B").ToString()
                    lblGRNId.Text = id
                    AddEditMode = 1
                    btnsave.Enabled = True
                    Fill_AllControls(id)
                    Modify()
                    ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "ActivePanel", "javascript:ActivePanel(2); ", True)
                ElseIf Not Request.QueryString("C") = Nothing Then
                    Delete(Convert.ToInt32(Request.QueryString("C").ToString()))
                    ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "tmp", "<script type='text/javascript'>var uri = window.location.toString();if (uri.indexOf('?') > 0) {var clean_uri = uri.substring(0, uri.indexOf('?'));window.history.replaceState({}, document.title, clean_uri);ActivePanel(1);}</script>", False)
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "ActivePanel", "javascript:ActivePanel(1); ", True)
                End If
            End If
        Catch ex As Exception
            txtmsg.Text = "Error in Page Load" & ex.Message
            txtmsg.ForeColor = Drawing.Color.Red
        End Try
    End Sub

    Private Sub Fill_AllControls(ByVal id As String)
        lblGRNId.Text = id
        ParamName = New String(19) {}
        ParamValue = New String(19) {}
        ParamName(0) = "@PINO"
        ParamName(1) = "@InvNo "
        ParamName(2) = "@InvDate"
        ParamName(3) = "@GrossAmt"
        ParamName(4) = "@DiscAmt"
        ParamName(5) = "@TaxAmt"
        ParamName(6) = "@VatAmt"
        ParamName(7) = "@NetAmt"
        ParamName(8) = "@AmtPaid"
        ParamName(9) = "@TXNYear"
        ParamName(10) = "@CompId"
        ParamName(11) = "@WardID"
        ParamName(12) = "@POID"
        ParamName(13) = "@Remarks"
        ParamName(14) = "@PurchaseFrom"
        ParamName(15) = "@GRNNo"
        ParamName(16) = "@PINOVB"
        ParamName(17) = "@Createdby"
        ParamName(18) = "@UpdateBy"
        ParamName(19) = "@Mode"

        ParamValue(0) = id
        ParamValue(1) = "0"
        ParamValue(2) = Now.Date
        ParamValue(3) = "0"
        ParamValue(4) = "0"
        ParamValue(5) = "0"
        ParamValue(6) = "0"
        ParamValue(7) = "0"
        ParamValue(8) = "0"
        ParamValue(9) = "0"
        ParamValue(10) = "0"
        ParamValue(11) = "0"
        ParamValue(12) = "0"
        ParamValue(13) = "0"
        ParamValue(14) = "0"
        ParamValue(15) = "0"
        ParamValue(16) = "0"
        ParamValue(17) = "0"
        ParamValue(18) = "0"
        ParamValue(19) = "2"
        Dim Dt As New DataTable()
        Dt = ObjComFun.GetDataByProc("SP_GRNMain", ParamName, ParamValue)
        If Dt.Rows.Count > 0 Then
            txtSerialNo.Text = Dt.Rows(0)("GRNNo").ToString()
            txtGRNNo.Text = Dt.Rows(0)("PINOVB").ToString()
            txtinvoiceno.Text = Dt.Rows(0)("InvNo").ToString()
            txtInvoicedate.Text = Dt.Rows(0)("InvDate").ToString()
            txtPurchaseFrom.Text = Dt.Rows(0)("PurchaseFrom").ToString()
            ddlPurchaseOrder.Items.Insert(0, New ListItem(Dt.Rows(0)("PurOrderId").ToString(), Dt.Rows(0)("POID").ToString()))
            ddlPurchaseOrder.SelectedValue = Dt.Rows(0)("POID").ToString()
            txtTotalGross.Text = Dt.Rows(0)("GrossAmt").ToString()
            txtTotalDisc.Text = Dt.Rows(0)("DiscAmt").ToString()
            txtTotalEx.Text = Dt.Rows(0)("TaxAmt").ToString()
            txtTotalVat.Text = Dt.Rows(0)("VatAmt").ToString()
            txtTotalNet.Text = Dt.Rows(0)("NetAmt").ToString()
            txtRemarksBottam.Text = Dt.Rows(0)("Remarks").ToString()
        End If

        ParamName = New String(20) {}
        ParamValue = New String(20) {}
        ParamName(0) = "@PrId"
        ParamName(1) = "@PINO "
        ParamName(2) = "@GlobalItemId"
        ParamName(3) = "@OrderQty"
        ParamName(4) = "@ReceivedQty"
        ParamName(5) = "@RejectQty"
        ParamName(6) = "@Rate"
        ParamName(7) = "@Amt"
        ParamName(8) = "@DisPer"
        ParamName(9) = "@DisAmt"
        ParamName(10) = "@ExPer"
        ParamName(11) = "@ExAmt"
        ParamName(12) = "@VatPer"
        ParamName(13) = "@VatAmt"
        ParamName(14) = "@NetAmt"
        ParamName(15) = "@TXNYear"
        ParamName(16) = "@CompId"
        ParamName(17) = "@WardID"
        ParamName(18) = "@Remarks"
        ParamName(19) = "@PODetailId"
        ParamName(20) = "@Mode"

        ParamValue(0) = "0"
        ParamValue(1) = id
        ParamValue(2) = "0"
        ParamValue(3) = "0"
        ParamValue(4) = "0"
        ParamValue(5) = "0"
        ParamValue(6) = "0"
        ParamValue(7) = "0"
        ParamValue(8) = "0"
        ParamValue(9) = "0"
        ParamValue(10) = "0"
        ParamValue(11) = "0"
        ParamValue(12) = "0"
        ParamValue(13) = "0"
        ParamValue(14) = "0"
        ParamValue(15) = "0"
        ParamValue(16) = "0"
        ParamValue(17) = "0"
        ParamValue(18) = "0"
        ParamValue(19) = "0"
        ParamValue(20) = "2"
        Session("DT_GRN") = ""
        DT_GRN = New DataTable(Session("DT_GRN"))
        DT_GRN = ObjComFun.GetDataByProc("SP_GRNDetails", ParamName, ParamValue)
        If DT_GRN.Rows.Count > 0 Then
            lblSesion.Text = "2"
            GRDPO.DataSource = DT_GRN
            GRDPO.DataBind()
            GRDPO.Visible = True
            Session("DT_GRN") = DT_GRN

            If GRDPO.Rows.Count > 0 Then
                For RowCount As Integer = 0 To (GRDPO.Rows.Count - 1)
                    Dim chk As CheckBox = CType(GRDPO.Rows(RowCount).FindControl("chkSelect"), CheckBox)
                    Dim txtRemarks As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtRemarks"), TextBox)
                    chk.Checked = True
                    chk.Enabled = False
                    txtRemarks.Enabled = False
                Next
            End If
        End If
    End Sub

    Private Sub GetMaxNo()
        strSql = "select isnull(Max(GRNNo+1),1) From Item_Purc where wardid=" & Session("BranchID").ToString() & " and CompId=" & Session("CompanyID").ToString() & ""
        txtSerialNo.Text = ObjComFun.GetColumn1BySqlQuery(strSql)
    End Sub

    Private Sub CheckRights()
        Dim frmName As String = System.IO.Path.GetFileName(System.Web.HttpContext.Current.Request.Url.AbsolutePath())
        Dim str As String = "select EditRights,DeleteRights  from Mast_ModuleUser where Uid=" & Session("UserId").ToString() & "  and"
        str += "  FormId =(Select srid from Mast_Menu where TargetFileName like'%" & frmName & "')"
        Dim DtCheckRights As New DataTable()
        DtCheckRights = ObjComFun.GetTableBySqlQuery(str)
        If DtCheckRights.Rows.Count > 0 Then
            If DtCheckRights.Rows(0)("EditRights").ToString() = "True" Then
                Session("EditRights") = "1"
            Else
                Session("EditRights") = "0"
            End If
            If DtCheckRights.Rows(0)("DeleteRights").ToString() = "True" Then
                Session("DeleteRights") = "1"
            Else
                Session("DeleteRights") = "0"
            End If
        End If
    End Sub

    Private Sub Fill_ddl()
        strSql = " Select ' PO Number' as PurOrderId,'0' as POID Union SELECT PurOrderId,POID FROM tbl_PurchaseOrderMain"
        strSql += " where Isreceived=0 and CompId=" & Session("CompanyId").ToString() & ""
        strSql += " and WardId=" & Session("BranchID").ToString() & ""
        strSql += " and POID not in(select distinct POID from Item_Purc where CompId=" & Session("CompanyId").ToString() & ""
        strSql += " and WardId=" & Session("BranchID").ToString() & ")"
        ObjComFun.FillDropDownList(ddlPurchaseOrder, strSql, "PurOrderId", "POID")
    End Sub

    Protected Sub AddBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AddBtn.Click
        strSql = "  select pd.PODetailID,0 as PRId,0 as PINO,g.GlobalItemName+'-'+c.CompanyName as GlobalItemName,"
        strSql += " pd.GlobalItemId,it.ItemType,U.UnitName as Unit,pd.ItemQuantity as Quantity,pd.ItemQuantity-"
        strSql += " pd.itembalance as ReceivedQty,0 as RejectQty,pd.Rate,pd.Amt,pd.DisPer,pd.DisAmt,pd.ExPer,pd.ExAmt,"
        strSql += " pd.VatPer,pd.VatAmt,pd.NetAmt,0 as ScrapQty,'' as Remarks"
        strSql += " from tbl_PurchaseOrderDetail  as pd"
        strSql += " left join tbl_PurchaseOrderMain as pm on pm.POId=pd.POId"
        strSql += " left join Mast_GlobalItem as g on g.GlobalItemID=pd.GlobalItemId"
        strSql += " left join Mast_ItemType as it on it.ItemTypeId=g.ItemTypeId"
        strSql += " left join Mast_Item_Unit as U on U.UnitId=g.UnitID"
        strSql += " left join Mast_Item_Comp as c on c.CompanyId=g.CompanyId"
        strSql += " where pd.POId=" & ddlPurchaseOrder.SelectedValue & ""
        strSql += " and pd.itembalance<pd.ItemQuantity"
        strSql += " and pm.CompId=" & Session("CompID") & ""
        DT_GRN = New DataTable("")
        DT_GRN = ObjComFun.GetTableBySqlQuery(strSql)
        If DT_GRN.Rows.Count > 0 Then
            GRDPO.DataSource = DT_GRN
            GRDPO.DataBind()
            Session("DT_GRN") = DT_GRN
            GRDPO.Visible = True
            GRDPO.Focus()
        End If
    End Sub

    Protected Sub GRDPO_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GRDPO.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim ChkSel As CheckBox
            Dim txtScrapQty, txtRejectQty, txtReceivedQty, txtQty, txtRate, txtAmt, txtDisPer, txtDisAmt, txtExPer, txtExAmt, txtVatPer, txtVatAmt, txtNetAmt As TextBox
            Dim hdnScrapQty, hdnRejectQty, hdnReceivedQty, hdnQty, hdnNetAmt, hdnVatAmt, hdnVatPer, hdnExAmt, hdnExPer, hdnDisAmt, hdnDisPer, hdnAmt, hdnRate As HiddenField
            ChkSel = CType(e.Row.FindControl("chkSelect"), CheckBox)
            txtQty = CType(e.Row.FindControl("txtQty"), TextBox)
            hdnQty = CType(e.Row.FindControl("hdnQty"), HiddenField)
            txtReceivedQty = CType(e.Row.FindControl("txtReceivedQty"), TextBox)
            hdnReceivedQty = CType(e.Row.FindControl("hdnReceivedQty"), HiddenField)
            txtRejectQty = CType(e.Row.FindControl("txtRejectQty"), TextBox)
            hdnRejectQty = CType(e.Row.FindControl("hdnRejectQty"), HiddenField)
            txtScrapQty = CType(e.Row.FindControl("txtScrapQty"), TextBox)
            hdnScrapQty = CType(e.Row.FindControl("hdnScrapQty"), HiddenField)

            txtRate = CType(e.Row.FindControl("txtRate"), TextBox)
            txtAmt = CType(e.Row.FindControl("txtAmt"), TextBox)
            txtDisPer = CType(e.Row.FindControl("txtDisPer"), TextBox)
            txtDisAmt = CType(e.Row.FindControl("txtDisAmt"), TextBox)
            txtExPer = CType(e.Row.FindControl("txtExPer"), TextBox)
            txtExAmt = CType(e.Row.FindControl("txtExAmt"), TextBox)
            txtVatPer = CType(e.Row.FindControl("txtVatPer"), TextBox)
            txtVatAmt = CType(e.Row.FindControl("txtVatAmt"), TextBox)
            txtNetAmt = CType(e.Row.FindControl("txtNetAmt"), TextBox)
            hdnNetAmt = CType(e.Row.FindControl("hdnNetAmt"), HiddenField)
            hdnVatAmt = CType(e.Row.FindControl("hdnVatAmt"), HiddenField)
            hdnVatPer = CType(e.Row.FindControl("hdnVatPer"), HiddenField)
            hdnExAmt = CType(e.Row.FindControl("hdnExAmt"), HiddenField)
            hdnExPer = CType(e.Row.FindControl("hdnExPer"), HiddenField)
            hdnDisAmt = CType(e.Row.FindControl("hdnDisAmt"), HiddenField)
            hdnDisPer = CType(e.Row.FindControl("hdnDisPer"), HiddenField)
            hdnAmt = CType(e.Row.FindControl("hdnAmt"), HiddenField)
            hdnRate = CType(e.Row.FindControl("hdnRate"), HiddenField)

            Dim scriptStr1 As String = _
    "javascript:forSelect( '" & ChkSel.ClientID & "','" & txtQty.ClientID & "','" & hdnQty.ClientID & "','" & txtRate.ClientID & "','" & txtAmt.ClientID & "','" & txtDisPer.ClientID & "','" & txtDisAmt.ClientID & "','" & txtExPer.ClientID & "','" & txtExAmt.ClientID & "','" & txtVatPer.ClientID & "','" & txtVatAmt.ClientID & "','" & txtNetAmt.ClientID & "','" & hdnNetAmt.ClientID & "','" & hdnVatAmt.ClientID & "','" & hdnVatPer.ClientID & "','" & hdnExAmt.ClientID & "','" & hdnExPer.ClientID & "','" & hdnDisAmt.ClientID & "','" & hdnDisPer.ClientID & "','" & hdnAmt.ClientID & "','" & hdnRate.ClientID & "','" & txtScrapQty.ClientID & "','" & txtRejectQty.ClientID & "','" & txtReceivedQty.ClientID & "','" & hdnScrapQty.ClientID & "','" & hdnRejectQty.ClientID & "','" & hdnReceivedQty.ClientID & "')"
            ChkSel.Attributes.Add("onclick", scriptStr1)

            Dim scriptStr2 As String = _
   "javascript:forAmount( '" & ChkSel.ClientID & "','" & txtQty.ClientID & "','" & hdnQty.ClientID & "','" & txtRate.ClientID & "','" & txtAmt.ClientID & "','" & txtDisPer.ClientID & "','" & txtDisAmt.ClientID & "','" & txtExPer.ClientID & "','" & txtExAmt.ClientID & "','" & txtVatPer.ClientID & "','" & txtVatAmt.ClientID & "','" & txtNetAmt.ClientID & "','" & hdnNetAmt.ClientID & "','" & hdnVatAmt.ClientID & "','" & hdnVatPer.ClientID & "','" & hdnExAmt.ClientID & "','" & hdnExPer.ClientID & "','" & hdnDisAmt.ClientID & "','" & hdnDisPer.ClientID & "','" & hdnAmt.ClientID & "','" & hdnRate.ClientID & "','" & txtScrapQty.ClientID & "','" & txtRejectQty.ClientID & "','" & txtReceivedQty.ClientID & "','" & hdnScrapQty.ClientID & "','" & hdnRejectQty.ClientID & "','" & hdnReceivedQty.ClientID & "')"
            txtReceivedQty.Attributes.Add("onblur", scriptStr2)
            txtRate.Attributes.Add("onblur", scriptStr2)
            txtDisPer.Attributes.Add("onblur", scriptStr2)
            txtVatPer.Attributes.Add("onblur", scriptStr2)
            txtExPer.Attributes.Add("onblur", scriptStr2)
        End If
    End Sub

    Protected Sub ddlPurchaseOrder_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPurchaseOrder.SelectedIndexChanged
        If ddlPurchaseOrder.SelectedValue = 0 Then
            txtPurchaseFrom.Text = ""
        Else
            strSql = "select pr.Name from tbl_PurchaseOrderMain as p"
            strSql += " left join Tbl_Supplier as pr on pr.PartyId=p.VendoreId"
            strSql += " where p.POID=" & ddlPurchaseOrder.SelectedValue
            strSql += " and p.CompID=" & Session("CompanyID") & ""
            txtPurchaseFrom.Text = ObjComFun.GetColumn1BySqlQuery(strSql)
            AddBtn.Focus()
        End If
    End Sub

    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AddEditMode = 0 Then
            Str = "SELECT COUNT(PINO)ISEXIST FROM Item_Purc"
            Str += " where PINOVB='" & txtGRNNo.Text.Trim() & "'"
            Str += " and CompId=" & Session("CompanyId") & ""
            Dim ISEXIST As Integer
            ISEXIST = ObjComFun.GetColumn1BySqlQuery(Str)
            If ISEXIST > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "alert", "javascript:ActivePanel(2);alert('Alredy Exist.');", True)
                txtGRNNo.Focus()
                txtTotalGross.Text = hdnTotalGross.Value
                txtTotalDisc.Text = hdnTotalDisc.Value
                txtTotalEx.Text = hdnTotalEx.Value
                txtTotalVat.Text = hdnTotalVat.Value
                txtTotalNet.Text = hdnTotalNet.Value
                Return
            End If
            InsertData()
            ShowRecords()
            strSql = " select isnull(ItemQuantity,0)as ItemQuantity,isnull(itembalance,0)as itembalance,isnull(RejectedQty,0)as RejectedQty from tbl_PurchaseOrderDetail where POId=" & ddlPurchaseOrder.SelectedValue
            Dim dt As New DataTable()
            dt = ObjComFun.GetTableBySqlQuery(strSql)
            Dim Status As Integer = 0
            If dt.Rows.Count > 0 Then
                Dim Order, Rec, _Rejected As Decimal
                For C As Integer = 0 To dt.Rows.Count - 1
                    Order = Convert.ToDecimal(dt.Rows(C)("ItemQuantity"))
                    Rec = Convert.ToDecimal(dt.Rows(C)("itembalance"))
                    _Rejected = Convert.ToDecimal(dt.Rows(C)("RejectedQty"))
                    If Not Order = (Rec + _Rejected) Then
                        Status = 1
                    End If
                Next
            End If
            If Status = 0 Then
                strSql = " Update tbl_PurchaseOrderMain set IsReceived=" & GRNID & " where POId=" & ddlPurchaseOrder.SelectedValue
                ObjComFun.ExecuteQuery(strSql, 0)
            End If
            Fill_ddl()
        Else
            Str = "SELECT COUNT(PINO)ISEXIST FROM Item_Purc"
            Str += " where "
            Str += " PINOVB='" & txtGRNNo.Text.Trim() & "'"
            Str += " and CompId=" & Session("CompanyId") & ""
            Str += " and PINO<>" & lblGRNId.Text & ""
            Dim ISEXIST As Integer
            ISEXIST = ObjComFun.GetColumn1BySqlQuery(Str)
            If ISEXIST > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "alert", "javascript:ActivePanel(2);alert('Alredy Exist.');", True)
                txtGRNNo.Focus()
                Return
            End If
            UpdateData()
            ShowRecords()
            strSql = " select isnull(ItemQuantity,0)as ItemQuantity,isnull(itembalance,0)as itembalance,isnull(RejectedQty,0)as RejectedQty from tbl_PurchaseOrderDetail where POId=" & ddlPurchaseOrder.SelectedValue
            Dim dt As New DataTable()
            dt = ObjComFun.GetTableBySqlQuery(strSql)
            Dim Status As Integer = 0
            If dt.Rows.Count > 0 Then
                Dim Order, Rec, _Rejected As Decimal

                For C As Integer = 0 To dt.Rows.Count - 1
                    Order = Convert.ToDecimal(dt.Rows(C)("ItemQuantity"))
                    Rec = Convert.ToDecimal(dt.Rows(C)("itembalance"))
                    _Rejected = Convert.ToDecimal(dt.Rows(C)("RejectedQty"))
                    If Not Order = (Rec + _Rejected) Then
                        Status = 1
                    End If
                Next
            End If
            If Status = 0 Then
                strSql = " Update tbl_PurchaseOrderMain set IsReceived=" & lblGRNId.Text.Trim() & " where POId=" & ddlPurchaseOrder.SelectedValue
                ObjComFun.ExecuteQuery(strSql, 0)
            End If
            Fill_ddl()
        End If
    End Sub

    Private Sub InsertData()
        Dim Row_ID As String = " select isnull(Max(PINO+1),1) From Item_Purc"
        Row_ID = ObjComFun.GetColumn1BySqlQuery(Row_ID)
        GRNID = Convert.ToInt64(Row_ID)

        Dim PrId_ID As String = " select isnull(Max(PrId+1),1) From Item_PurcDet"
        PrId_ID = ObjComFun.GetColumn1BySqlQuery(PrId_ID)
        GetMaxNo()
        Dim Tr As SqlTransaction = Nothing
        Dim Cmd As SqlCommand
        Try
            Dim CoonecStr As String
            CoonecStr = ConfigurationManager.AppSettings.Get("ConAntonyBombay").ToString()
            SqlCon.ConnectionString = CoonecStr
            If SqlCon.State = ConnectionState.Closed Then
                SqlCon.Open()
            End If
            Cmd = New SqlCommand()
            Cmd.Connection = SqlCon
            Tr = SqlCon.BeginTransaction()
            Dim InvDate As SqlDateTime

            If txtInvoicedate.Text.Length > 0 Then
                InvDate = ObjComFun.GetStringToDate(txtInvoicedate.Text)
            Else
                InvDate = SqlDateTime.Null
            End If
            Cmd.CommandText = "SP_GRNMain"
            Cmd.Parameters.AddWithValue("@PINO", Row_ID)
            Cmd.Parameters.AddWithValue("@InvNo", txtinvoiceno.Text.Trim())
            Cmd.Parameters.AddWithValue("@InvDate", InvDate)
            Cmd.Parameters.AddWithValue("@GrossAmt", hdnTotalGross.Value.Replace(",", ""))
            Cmd.Parameters.AddWithValue("@DiscAmt", hdnTotalDisc.Value.Replace(",", ""))
            Cmd.Parameters.AddWithValue("@TaxAmt", hdnTotalEx.Value.Replace(",", ""))
            Cmd.Parameters.AddWithValue("@VatAmt", hdnTotalVat.Value.Replace(",", ""))
            Cmd.Parameters.AddWithValue("@NetAmt", hdnTotalNet.Value.Replace(",", ""))
            Cmd.Parameters.AddWithValue("@AmtPaid", 0)
            Cmd.Parameters.AddWithValue("@TxnYear", Session("TxnYear"))
            Cmd.Parameters.AddWithValue("@CompId", Session("CompanyId"))
            Cmd.Parameters.AddWithValue("@WardId", Session("BranchID"))
            Cmd.Parameters.AddWithValue("@POID", ddlPurchaseOrder.SelectedValue)
            Cmd.Parameters.AddWithValue("@Remarks", txtRemarksBottam.Text.Trim())
            Cmd.Parameters.AddWithValue("@PurchaseFrom", txtPurchaseFrom.Text.Trim())
            Cmd.Parameters.AddWithValue("@GRNNo", txtSerialNo.Text.Trim())
            Cmd.Parameters.AddWithValue("@PINOVB", txtGRNNo.Text.Trim())
            Cmd.Parameters.AddWithValue("@Createdby", Session("UserId"))
            Cmd.Parameters.AddWithValue("@UpdateBy", Session("UserId"))
            Cmd.Parameters.AddWithValue("@Mode", 1)
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.Transaction = Tr
            Cmd.Connection = SqlCon
            Cmd.ExecuteNonQuery()
            Cmd.Parameters.Clear()

            If GRDPO.Rows.Count > 0 Then
                For RowCount As Integer = 0 To (GRDPO.Rows.Count - 1)
                    Dim chk As CheckBox = CType(GRDPO.Rows(RowCount).FindControl("chkSelect"), CheckBox)
                    If chk.Checked = True Then
                        Dim lblPODetailID As Label = CType(GRDPO.Rows(RowCount).FindControl("lblPODetailID"), Label)
                        Dim lblPRId As Label = CType(GRDPO.Rows(RowCount).FindControl("lblPRId"), Label)
                        Dim lblPINO As Label = CType(GRDPO.Rows(RowCount).FindControl("lblPINO"), Label)
                        Dim lblGlobalItemId As Label = CType(GRDPO.Rows(RowCount).FindControl("lblGlobalItemId"), Label)
                        Dim txtQty As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtQty"), TextBox)
                        Dim txtReceivedQty As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtReceivedQty"), TextBox)
                        Dim txtRejectQty As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtRejectQty"), TextBox)
                        Dim txtRate As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtRate"), TextBox)
                        Dim txtAmt As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtAmt"), TextBox)
                        Dim txtDisPer As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtDisPer"), TextBox)
                        Dim txtDisAmt As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtDisAmt"), TextBox)
                        Dim txtExPer As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtExPer"), TextBox)
                        Dim txtExAmt As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtExAmt"), TextBox)
                        Dim txtVatPer As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtVatPer"), TextBox)
                        Dim txtVatAmt As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtVatAmt"), TextBox)
                        Dim txtNetAmt As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtNetAmt"), TextBox)
                        Dim txtScrapQty As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtScrapQty"), TextBox)
                        Dim txtRemarks As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtRemarks"), TextBox)
                        If txtQty.Text.Trim() = "" Then txtQty.Text = "0"
                        If txtRate.Text.Trim() = "" Then txtRate.Text = "0"
                        If txtAmt.Text.Trim() = "" Then txtAmt.Text = "0"
                        If txtDisPer.Text.Trim() = "" Then txtDisPer.Text = "0"
                        If txtDisAmt.Text.Trim() = "" Then txtDisAmt.Text = "0"
                        If txtExPer.Text.Trim() = "" Then txtExPer.Text = "0"
                        If txtExAmt.Text.Trim() = "" Then txtExAmt.Text = "0"
                        If txtVatPer.Text.Trim() = "" Then txtVatPer.Text = "0"
                        If txtVatAmt.Text.Trim() = "" Then txtVatAmt.Text = "0"
                        If txtNetAmt.Text.Trim() = "" Then txtNetAmt.Text = "0"
                        If txtRejectQty.Text.Trim() = "" Then txtRejectQty.Text = "0"
                        If txtReceivedQty.Text.Trim() = "" Then txtReceivedQty.Text = "0"
                        If txtScrapQty.Text.Trim() = "" Then txtScrapQty.Text = "0"

                        Dim amt As Decimal = Convert.ToDecimal(txtReceivedQty.Text.Replace(",", "")) * Convert.ToDecimal(txtRate.Text.Replace(",", ""))
                        Dim discamt As Decimal = (amt * Convert.ToDecimal(txtDisPer.Text.Replace(",", ""))) / 100
                        Dim examt As Decimal = ((amt - discamt) * Convert.ToDecimal(txtExPer.Text.Replace(",", ""))) / 100
                        Dim vatamt As Decimal = ((amt - discamt + examt) * Convert.ToDecimal(txtVatPer.Text.Replace(",", ""))) / 100
                        Dim netamt As Decimal = amt - discamt + examt + vatamt

                        Cmd.CommandText = "SP_GRNDetails"
                        Cmd.Parameters.AddWithValue("@PrId", Convert.ToInt64(PrId_ID) + RowCount)
                        Cmd.Parameters.AddWithValue("@PINO", Row_ID)
                        Cmd.Parameters.AddWithValue("@GlobalItemId", lblGlobalItemId.Text.Trim())
                        Cmd.Parameters.AddWithValue("@OrderQty", txtQty.Text.Replace(",", ""))
                        Cmd.Parameters.AddWithValue("@ReceivedQty", txtReceivedQty.Text.Replace(",", ""))
                        Cmd.Parameters.AddWithValue("@RejectQty", txtRejectQty.Text.Replace(",", ""))
                        Cmd.Parameters.AddWithValue("@Rate", txtRate.Text.Replace(",", ""))
                        Cmd.Parameters.AddWithValue("@Amt", amt)
                        Cmd.Parameters.AddWithValue("@DisPer", txtDisPer.Text.Replace(",", ""))
                        Cmd.Parameters.AddWithValue("@DisAmt", discamt)
                        Cmd.Parameters.AddWithValue("@ExPer", txtExPer.Text.Replace(",", ""))
                        Cmd.Parameters.AddWithValue("@ExAmt", examt)
                        Cmd.Parameters.AddWithValue("@VatPer", txtVatPer.Text.Replace(",", ""))
                        Cmd.Parameters.AddWithValue("@VatAmt", vatamt)
                        Cmd.Parameters.AddWithValue("@NetAmt", netamt)
                        Cmd.Parameters.AddWithValue("@TxnYear", Session("TxnYear"))
                        Cmd.Parameters.AddWithValue("@CompId", Session("CompanyId"))
                        Cmd.Parameters.AddWithValue("@WardId", Session("BranchID"))
                        Cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text.Trim())
                        Cmd.Parameters.AddWithValue("@PODetailId", lblPODetailID.Text.Trim())
                        Cmd.Parameters.AddWithValue("@Mode", 1)
                        Cmd.CommandType = CommandType.StoredProcedure
                        Cmd.Transaction = Tr
                        Cmd.Connection = SqlCon
                        Cmd.ExecuteNonQuery()
                        Cmd.Parameters.Clear()

                        strSql = " Update tbl_PurchaseOrderDetail set itembalance=itembalance+" & txtReceivedQty.Text.Replace(",", "") & ","
                        strSql += " RejectedQty=" & txtRejectQty.Text.Replace(",", "") & ""
                        strSql += " where PODetailID =" & lblPODetailID.Text.Trim() & ""
                        Cmd.CommandType = CommandType.Text
                        Cmd.CommandText = strSql
                        Cmd.Transaction = Tr
                        Cmd.Connection = SqlCon
                        Cmd.ExecuteNonQuery()

                        strSql = "Update Mast_ItemStk set QtyClosing=QtyClosing+" & txtReceivedQty.Text.Replace(",", "") & ""
                        strSql += " where GlobalItemId =" & lblGlobalItemId.Text.Trim() & ""
                        strSql += " and BranchID=" & Session("BranchID") & ""
                        Cmd.CommandType = CommandType.Text
                        Cmd.CommandText = strSql
                        Cmd.Transaction = Tr
                        Cmd.Connection = SqlCon
                        Cmd.ExecuteNonQuery()

                        strSql = " Update Mast_RateDetails set CurrentQty=CurrentQty+" & txtReceivedQty.Text.Replace(",", "") & ""
                        strSql += " where GlobalItemID=" & lblGlobalItemId.Text.Trim() & ""
                        strSql += " And Rate=" & txtRate.Text.Replace(",", "") & ""
                        strSql += " And DiscPer=" & txtDisPer.Text.Replace(",", "") & ""
                        strSql += " And VatPer=" & txtVatPer.Text.Replace(",", "") & ""
                        strSql += " and CompId=" & Session("CompanyId").ToString() & " and WardId=" & Session("BranchID") & ""
                        Cmd.CommandType = CommandType.Text
                        Cmd.CommandText = strSql
                        Cmd.Transaction = Tr
                        Cmd.Connection = SqlCon
                        Cmd.ExecuteNonQuery()
                    End If
                Next
            End If

            Tr.Commit()
            ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "getConfirmation", "javascript:getConfirmation(); ", True)
            BlankControls()
            btnsave.Enabled = True
            GetMaxNo()
            txtSerialNo.Focus()
            txtGRNNo.Enabled = True
        Catch ex As Exception
            Tr.Rollback()
            txtmsg.Text = "Error in InsertData()" & ex.Message
            txtmsg.ForeColor = Drawing.Color.Red
        Finally
            If SqlCon.State = ConnectionState.Open Then
                SqlCon.Close()
            End If
        End Try
    End Sub

    Private Sub UpdateData()
        strSql = "select POID from Item_Purc where PINO=" & lblGRNId.Text.Trim() & ""
        Dim vr_PoId As Integer = 0
        vr_PoId = Convert.ToInt64(ObjComFun.GetColumn1BySqlQuery(strSql))
        strSql = "select PrId,Isnull(ReceivedQty,0) as ReceivedQty,PODetailId ,GlobalItemId,Isnull(RejectQty,0) as RejectQty from Item_PurcDet  where PINO=" & lblGRNId.Text.Trim()
        Dim Dt_GrId As New DataTable()
        Dt_GrId = ObjComFun.GetTableBySqlQuery(strSql)

        strSql = "select GlobalItemId,ReceivedQty,Rate,DisPer as DiscPer,VatPer from Item_PurcDet"
        strSql += " where PINO = " & lblGRNId.Text.Trim() & ""
        strSql += " and WardID=" & Session("BranchId").ToString() & ""
        strSql += " and CompId=" & Session("CompanyId").ToString() & ""
        Dim DtForRate As New DataTable()
        DtForRate = ObjComFun.GetTableBySqlQuery(strSql)

        Dim PrId_ID As String = " select isnull(Max(PrId+1),1) From Item_PurcDet"
        PrId_ID = ObjComFun.GetColumn1BySqlQuery(PrId_ID)
        Dim Tr As SqlTransaction = Nothing
        Dim Cmd As SqlCommand
        Try
            Dim CoonecStr As String
            CoonecStr = ConfigurationManager.AppSettings.Get("ConAntonyBombay").ToString()
            SqlCon.ConnectionString = CoonecStr
            If SqlCon.State = ConnectionState.Closed Then
                SqlCon.Open()
            End If
            Cmd = New SqlCommand()
            Cmd.Connection = SqlCon
            Tr = SqlCon.BeginTransaction()
            Dim InvDate As SqlDateTime

            If txtInvoicedate.Text.Length > 0 Then
                InvDate = ObjComFun.GetStringToDate(txtInvoicedate.Text)
            Else
                InvDate = SqlDateTime.Null
            End If

            If Dt_GrId.Rows.Count > 0 Then
                For Icnt As Integer = 0 To Dt_GrId.Rows.Count - 1
                    strSql = "Update tbl_PurchaseOrderDetail Set itembalance=itembalance-" & Convert.ToInt64(Dt_GrId.Rows(Icnt)("ReceivedQty")) & ", RejectedQty=RejectedQty-" & Convert.ToInt64(Dt_GrId.Rows(Icnt)("RejectQty")) & "  Where PODetailID=" & Dt_GrId.Rows(Icnt)("PODetailId").ToString() & " "
                    Cmd.CommandType = CommandType.Text
                    Cmd.CommandText = strSql
                    Cmd.Transaction = Tr
                    Cmd.Connection = SqlCon
                    Cmd.ExecuteNonQuery()

                    strSql = "Update Mast_ItemStk Set QtyClosing=QtyClosing-" & Convert.ToInt64(Dt_GrId.Rows(Icnt)("ReceivedQty")) & "  Where GlobalItemId=" & Dt_GrId.Rows(Icnt)("GlobalItemId").ToString() & " and CompId=" & Session("CompanyId").ToString() & " and BranchID=" & Session("BranchID").ToString() & "  "
                    Cmd.CommandType = CommandType.Text
                    Cmd.CommandText = strSql
                    Cmd.Transaction = Tr
                    Cmd.Connection = SqlCon
                    Cmd.ExecuteNonQuery()
                Next
            End If
            If DtForRate.Rows.Count > 0 Then
                For Icnt As Integer = 0 To DtForRate.Rows.Count - 1
                    strSql = "Update Mast_RateDetails Set CurrentQty=CurrentQty-" & Convert.ToInt64(DtForRate.Rows(Icnt)("ReceivedQty")) & " "
                    strSql += " Where Rate=" & DtForRate.Rows(Icnt)("Rate").ToString() & " "
                    strSql += " and DiscPer=" & DtForRate.Rows(Icnt)("DiscPer").ToString() & " "
                    strSql += " and VatPer=" & DtForRate.Rows(Icnt)("VatPer").ToString() & " "
                    strSql += " and GlobalItemId=" & DtForRate.Rows(Icnt)("GlobalItemId").ToString() & " "
                    Cmd.CommandType = CommandType.Text
                    Cmd.CommandText = strSql
                    Cmd.Transaction = Tr
                    Cmd.Connection = SqlCon
                    Cmd.ExecuteNonQuery()
                Next
            End If

            strSql = " Update tbl_PurchaseOrderMain set IsReceived=0 where POID=" & vr_PoId
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = strSql
            Cmd.Transaction = Tr
            Cmd.Connection = SqlCon
            Cmd.ExecuteNonQuery()

            strSql = " Delete From  Item_PurcDet  where PINO=" & lblGRNId.Text.Trim()
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = strSql
            Cmd.Transaction = Tr
            Cmd.Connection = SqlCon
            Cmd.ExecuteNonQuery()

            Cmd.CommandText = "SP_GRNMain"
            Cmd.Parameters.AddWithValue("@PINO", lblGRNId.Text.Trim())
            Cmd.Parameters.AddWithValue("@InvNo", txtinvoiceno.Text.Trim())
            Cmd.Parameters.AddWithValue("@InvDate", InvDate)
            Cmd.Parameters.AddWithValue("@GrossAmt", hdnTotalGross.Value.Replace(",", ""))
            Cmd.Parameters.AddWithValue("@DiscAmt", hdnTotalDisc.Value.Replace(",", ""))
            Cmd.Parameters.AddWithValue("@TaxAmt", hdnTotalEx.Value.Replace(",", ""))
            Cmd.Parameters.AddWithValue("@VatAmt", hdnTotalVat.Value.Replace(",", ""))
            Cmd.Parameters.AddWithValue("@NetAmt", hdnTotalNet.Value.Replace(",", ""))
            Cmd.Parameters.AddWithValue("@AmtPaid", 0)
            Cmd.Parameters.AddWithValue("@TxnYear", Session("TxnYear"))
            Cmd.Parameters.AddWithValue("@CompId", Session("CompanyId"))
            Cmd.Parameters.AddWithValue("@WardId", Session("BranchID"))
            Cmd.Parameters.AddWithValue("@POID", ddlPurchaseOrder.SelectedValue)
            Cmd.Parameters.AddWithValue("@Remarks", txtRemarksBottam.Text.Trim())
            Cmd.Parameters.AddWithValue("@PurchaseFrom", txtPurchaseFrom.Text.Trim())
            Cmd.Parameters.AddWithValue("@GRNNo", txtSerialNo.Text.Trim())
            Cmd.Parameters.AddWithValue("@PINOVB", txtGRNNo.Text.Trim())
            Cmd.Parameters.AddWithValue("@Createdby", Session("UserId"))
            Cmd.Parameters.AddWithValue("@UpdateBy", Session("UserId"))
            Cmd.Parameters.AddWithValue("@Mode", 3)
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.Transaction = Tr
            Cmd.Connection = SqlCon
            Cmd.ExecuteNonQuery()
            Cmd.Parameters.Clear()

            If GRDPO.Rows.Count > 0 Then
                For RowCount As Integer = 0 To (GRDPO.Rows.Count - 1)
                    Dim chk As CheckBox = CType(GRDPO.Rows(RowCount).FindControl("chkSelect"), CheckBox)
                    If chk.Checked = True Then
                        Dim lblPODetailID As Label = CType(GRDPO.Rows(RowCount).FindControl("lblPODetailID"), Label)
                        Dim lblPRId As Label = CType(GRDPO.Rows(RowCount).FindControl("lblPRId"), Label)
                        Dim lblPINO As Label = CType(GRDPO.Rows(RowCount).FindControl("lblPINO"), Label)
                        Dim lblGlobalItemId As Label = CType(GRDPO.Rows(RowCount).FindControl("lblGlobalItemId"), Label)
                        Dim txtQty As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtQty"), TextBox)
                        Dim txtReceivedQty As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtReceivedQty"), TextBox)
                        Dim txtRejectQty As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtRejectQty"), TextBox)
                        Dim txtRate As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtRate"), TextBox)
                        Dim txtAmt As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtAmt"), TextBox)
                        Dim txtDisPer As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtDisPer"), TextBox)
                        Dim txtDisAmt As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtDisAmt"), TextBox)
                        Dim txtExPer As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtExPer"), TextBox)
                        Dim txtExAmt As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtExAmt"), TextBox)
                        Dim txtVatPer As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtVatPer"), TextBox)
                        Dim txtVatAmt As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtVatAmt"), TextBox)
                        Dim txtNetAmt As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtNetAmt"), TextBox)
                        Dim txtScrapQty As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtScrapQty"), TextBox)
                        Dim txtRemarks As TextBox = CType(GRDPO.Rows(RowCount).FindControl("txtRemarks"), TextBox)
                        If txtQty.Text.Trim() = "" Then txtQty.Text = "0"
                        If txtRate.Text.Trim() = "" Then txtRate.Text = "0"
                        If txtAmt.Text.Trim() = "" Then txtAmt.Text = "0"
                        If txtDisPer.Text.Trim() = "" Then txtDisPer.Text = "0"
                        If txtDisAmt.Text.Trim() = "" Then txtDisAmt.Text = "0"
                        If txtExPer.Text.Trim() = "" Then txtExPer.Text = "0"
                        If txtExAmt.Text.Trim() = "" Then txtExAmt.Text = "0"
                        If txtVatPer.Text.Trim() = "" Then txtVatPer.Text = "0"
                        If txtVatAmt.Text.Trim() = "" Then txtVatAmt.Text = "0"
                        If txtNetAmt.Text.Trim() = "" Then txtNetAmt.Text = "0"
                        If txtRejectQty.Text.Trim() = "" Then txtRejectQty.Text = "0"
                        If txtReceivedQty.Text.Trim() = "" Then txtReceivedQty.Text = "0"
                        If txtScrapQty.Text.Trim() = "" Then txtScrapQty.Text = "0"

                        Dim amt As Decimal = Convert.ToDecimal(txtReceivedQty.Text.Replace(",", "")) * Convert.ToDecimal(txtRate.Text.Replace(",", ""))
                        Dim discamt As Decimal = (amt * Convert.ToDecimal(txtDisPer.Text.Replace(",", ""))) / 100
                        Dim examt As Decimal = ((amt - discamt) * Convert.ToDecimal(txtExPer.Text.Replace(",", ""))) / 100
                        Dim vatamt As Decimal = ((amt - discamt + examt) * Convert.ToDecimal(txtVatPer.Text.Replace(",", ""))) / 100
                        Dim netamt As Decimal = amt - discamt + examt + vatamt
                        Cmd.CommandText = "SP_GRNDetails"
                        Cmd.Parameters.AddWithValue("@PrId", Convert.ToInt64(PrId_ID) + RowCount)
                        Cmd.Parameters.AddWithValue("@PINO", lblGRNId.Text.Trim())
                        Cmd.Parameters.AddWithValue("@GlobalItemId", lblGlobalItemId.Text.Trim())
                        Cmd.Parameters.AddWithValue("@OrderQty", txtQty.Text.Replace(",", ""))
                        Cmd.Parameters.AddWithValue("@ReceivedQty", txtReceivedQty.Text.Replace(",", ""))
                        Cmd.Parameters.AddWithValue("@RejectQty", txtRejectQty.Text.Replace(",", ""))
                        Cmd.Parameters.AddWithValue("@Rate", txtRate.Text.Replace(",", ""))
                        Cmd.Parameters.AddWithValue("@Amt", amt)
                        Cmd.Parameters.AddWithValue("@DisPer", txtDisPer.Text.Replace(",", ""))
                        Cmd.Parameters.AddWithValue("@DisAmt", discamt)
                        Cmd.Parameters.AddWithValue("@ExPer", txtExPer.Text.Replace(",", ""))
                        Cmd.Parameters.AddWithValue("@ExAmt", examt)
                        Cmd.Parameters.AddWithValue("@VatPer", txtVatPer.Text.Replace(",", ""))
                        Cmd.Parameters.AddWithValue("@VatAmt", vatamt)
                        Cmd.Parameters.AddWithValue("@NetAmt", netamt)
                        Cmd.Parameters.AddWithValue("@TxnYear", Session("TxnYear"))
                        Cmd.Parameters.AddWithValue("@CompId", Session("CompanyId"))
                        Cmd.Parameters.AddWithValue("@WardId", Session("BranchID"))
                        Cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text.Trim())
                        Cmd.Parameters.AddWithValue("@PODetailId", lblPODetailID.Text.Trim())
                        Cmd.Parameters.AddWithValue("@Mode", 1)
                        Cmd.CommandType = CommandType.StoredProcedure
                        Cmd.Transaction = Tr
                        Cmd.Connection = SqlCon
                        Cmd.ExecuteNonQuery()
                        Cmd.Parameters.Clear()

                        strSql = " Update tbl_PurchaseOrderDetail set itembalance=itembalance+" & txtReceivedQty.Text.Replace(",", "") & ","
                        strSql += " RejectedQty=" & txtRejectQty.Text.Replace(",", "") & " "
                        strSql += "  where PODetailID =" & lblPODetailID.Text.Trim() & ""
                        Cmd.CommandType = CommandType.Text
                        Cmd.CommandText = strSql
                        Cmd.Transaction = Tr
                        Cmd.Connection = SqlCon
                        Cmd.ExecuteNonQuery()

                        strSql = "Update Mast_ItemStk set QtyClosing=QtyClosing+" & txtReceivedQty.Text.Replace(",", "") & ""
                        strSql += "  where GlobalItemId =" & lblGlobalItemId.Text.Trim() & ""
                        strSql += " and BranchID=" & Session("BranchID") & " "
                        Cmd.CommandType = CommandType.Text
                        Cmd.CommandText = strSql
                        Cmd.Transaction = Tr
                        Cmd.Connection = SqlCon
                        Cmd.ExecuteNonQuery()

                        strSql = " Update Mast_RateDetails set CurrentQty=CurrentQty+" & txtReceivedQty.Text.Replace(",", "") & ""
                        strSql += " where GlobalItemID = " & lblGlobalItemId.Text.Trim() & " "
                        strSql += " And Rate =" & txtRate.Text.Replace(",", "") & ""
                        strSql += " And DiscPer =" & txtDisPer.Text.Replace(",", "") & ""
                        strSql += " And VatPer =" & txtVatPer.Text.Replace(",", "") & ""
                        strSql += " and CompId=" & Session("CompanyId").ToString() & " and WardId=" & Session("BranchID") & "  "
                        Cmd.CommandType = CommandType.Text
                        Cmd.CommandText = strSql
                        Cmd.Transaction = Tr
                        Cmd.Connection = SqlCon
                        Cmd.ExecuteNonQuery()
                    End If
                Next
            End If

            Tr.Commit()
            ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "tmp", "<script type='text/javascript'>var uri = window.location.toString();if (uri.indexOf('?') > 0) {var clean_uri = uri.substring(0, uri.indexOf('?'));window.history.replaceState({}, document.title, clean_uri);ActivePanel(1);alert('Updated successfully!');}</script>", False)
            BlankControls()
            btnsave.Enabled = True
            GetMaxNo()
            txtSerialNo.Focus()
            txtGRNNo.Enabled = True
        Catch ex As Exception
            Tr.Rollback()
            txtmsg.Text = "Error in UpdateData()" & ex.Message
            txtmsg.ForeColor = Drawing.Color.Red
        Finally
            If SqlCon.State = ConnectionState.Open Then
                SqlCon.Close()
            End If
        End Try
    End Sub

    Protected Sub Delete(ID As Integer)
        If cnt = 0 Then
            cnt = 1
            If Session("DeleteRights") = "0" Then
                txtmsg.Text = "Delete Rights Is Not Assignd For You"
                txtmsg.ForeColor = Drawing.Color.Red
                Return
            End If
            txtmsg.Text = ""
            strSql = "select POID  from Item_Purc where PINO= " & ID & ""
            Dim vr_PoId As Integer = 0
            vr_PoId = Convert.ToInt64(ObjComFun.GetColumn1BySqlQuery(strSql))
            strSql = "select PrId,Isnull(ReceivedQty,0) as ReceivedQty,PODetailId ,GlobalItemId,Isnull(RejectQty,0) as RejectQty from Item_PurcDet  where PINO=" & ID

            Dim Dt_GrId As New DataTable()
            Dt_GrId = ObjComFun.GetTableBySqlQuery(strSql)
            strSql = "select GlobalItemId,ReceivedQty,Rate,DisPer as DiscPer,VatPer  from Item_PurcDet "
            strSql += " where PINO = " & ID & ""
            strSql += " and WardID=" & Session("BranchID").ToString() & ""
            strSql += " and CompId=" & Session("CompanyId").ToString() & ""
            Dim DtForRate As New DataTable()
            DtForRate = ObjComFun.GetTableBySqlQuery(strSql)

            Dim Tr As SqlTransaction = Nothing
            Dim Cmd As SqlCommand
            Try
                Dim CoonecStr As String
                CoonecStr = ConfigurationManager.AppSettings.Get("ConAntonyBombay").ToString()
                SqlCon.ConnectionString = CoonecStr
                If SqlCon.State = ConnectionState.Closed Then
                    SqlCon.Open()
                End If
                Cmd = New SqlCommand()
                Cmd.Connection = SqlCon
                Tr = SqlCon.BeginTransaction()
                If Dt_GrId.Rows.Count > 0 Then
                    For Icnt As Integer = 0 To Dt_GrId.Rows.Count - 1
                        strSql = "Update tbl_PurchaseOrderDetail Set itembalance=itembalance-" & Convert.ToInt64(Dt_GrId.Rows(Icnt)("ReceivedQty")) & ", RejectedQty=RejectedQty-" & Convert.ToInt64(Dt_GrId.Rows(Icnt)("RejectQty")) & "  Where PODetailID=" & Dt_GrId.Rows(Icnt)("PODetailId").ToString() & " "
                        Cmd.CommandType = CommandType.Text
                        Cmd.CommandText = strSql
                        Cmd.Transaction = Tr
                        Cmd.Connection = SqlCon
                        Cmd.ExecuteNonQuery()
                        strSql = "Update Mast_ItemStk Set QtyClosing=QtyClosing-" & Convert.ToInt64(Dt_GrId.Rows(Icnt)("ReceivedQty")) & "  Where GlobalItemId=" & Dt_GrId.Rows(Icnt)("GlobalItemId").ToString() & " and CompId=" & Session("CompanyId").ToString() & "  and BranchID=" & Session("BranchID").ToString() & "  "
                        Cmd.CommandType = CommandType.Text
                        Cmd.CommandText = strSql
                        Cmd.Transaction = Tr
                        Cmd.Connection = SqlCon
                        Cmd.ExecuteNonQuery()
                    Next
                End If
                If DtForRate.Rows.Count > 0 Then
                    For Icnt As Integer = 0 To DtForRate.Rows.Count - 1
                        strSql = "Update Mast_RateDetails Set CurrentQty=CurrentQty-" & Convert.ToInt64(DtForRate.Rows(Icnt)("ReceivedQty")) & " "
                        strSql += " Where Rate=" & DtForRate.Rows(Icnt)("Rate").ToString() & " "
                        strSql += " and DiscPer=" & DtForRate.Rows(Icnt)("DiscPer").ToString() & " "
                        strSql += " and VatPer=" & DtForRate.Rows(Icnt)("VatPer").ToString() & " "
                        strSql += " and GlobalItemId=" & DtForRate.Rows(Icnt)("GlobalItemId").ToString() & " "
                        Cmd.CommandType = CommandType.Text
                        Cmd.CommandText = strSql
                        Cmd.Transaction = Tr
                        Cmd.Connection = SqlCon
                        Cmd.ExecuteNonQuery()
                    Next
                End If
                strSql = " Update tbl_PurchaseOrderMain set IsReceived=0 where POID=" & vr_PoId
                Cmd.CommandType = CommandType.Text
                Cmd.CommandText = strSql
                Cmd.Transaction = Tr
                Cmd.Connection = SqlCon
                Cmd.ExecuteNonQuery()

                strSql = " Delete From  Item_PurcDet  where PINO=" & ID
                Cmd.CommandType = CommandType.Text
                Cmd.CommandText = strSql
                Cmd.Transaction = Tr
                Cmd.Connection = SqlCon
                Cmd.ExecuteNonQuery()

                strSql = " Delete From  Item_Purc  where PINO=" & ID
                Cmd.CommandType = CommandType.Text
                Cmd.CommandText = strSql
                Cmd.Transaction = Tr
                Cmd.Connection = SqlCon
                Cmd.ExecuteNonQuery()

                Tr.Commit()
                ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "tmp", "<script type='text/javascript'>var uri = window.location.toString();if (uri.indexOf('?') > 0) {var clean_uri = uri.substring(0, uri.indexOf('?'));window.history.replaceState({}, document.title, clean_uri);ActivePanel(1);alert('Deleted successfully!');}</script>", False)
                BlankControls()
                btnsave.Enabled = True
                GetMaxNo()
                txtSerialNo.Focus()
                Fill_ddl()
                txtGRNNo.Enabled = True
            Catch ex As Exception
                Tr.Rollback()
                If ex.Message.Substring(0, 61).ToUpper().Trim() = ObjComFun.vr_deleteExcpn Then
                    txtmsg.Text = "Record Can't Deleted."
                Else
                    txtmsg.Text = "Error in btnDelete_Click " & ex.Message
                End If
                txtmsg.ForeColor = Drawing.Color.Red
            Finally
                If SqlCon.State = ConnectionState.Open Then
                    SqlCon.Close()
                End If
            End Try
        End If
    End Sub

    Private Sub BlankControls()
        txtinvoiceno.Text = ""
        txtInvoicedate.Text = Now.Date.ToString("dd/MM/yyyy")
        txtPurchaseFrom.Text = ""
        txtRemarksBottam.Text = ""
        txtTotalDisc.Text = "0"
        txtTotalEx.Text = "0"
        txtTotalGross.Text = "0"
        txtTotalNet.Text = "0"
        txtTotalVat.Text = "0"
        hdnTotalDisc.Value = "0"
        hdnTotalEx.Value = "0"
        hdnTotalGross.Value = "0"
        hdnTotalNet.Value = "0"
        hdnTotalVat.Value = "0"
        GRDPO.Visible = False
        txtGRNNo.Text = ""
        AddEditMode = 0
        lblSesion.Text = "1"
        Session("DT_GRN") = ""
        DT_GRN = New DataTable(Session("DT_GRN"))
    End Sub

    Protected Sub Modify()
        Try
            If Session("EditRights") = "0" Then
                txtmsg.Text = "Edit Rights Is Not Assignd For You"
                txtmsg.ForeColor = Drawing.Color.Red
                Return
            End If
            AddEditMode = 1
            btnsave.Enabled = True
            pnlMain.Enabled = True
            txtGRNNo.Enabled = False
            txtinvoiceno.Focus()
        Catch ex As Exception
            txtmsg.Text = "Error in btnmodify_Click" & ex.Message
            txtmsg.ForeColor = Drawing.Color.Red
        End Try
    End Sub

    Protected Sub ShowRecords()
        Try
           Dim Literal1 As New Literal()
                Str = "select a.PINO,a.GRNNo,a.PINOVB,a.InvNo,convert(varchar,a.InvDate,103) as InvDate,a.PurchaseFrom,b.PurOrderId,"
                Str += " convert(varchar,a.CreatedDate,103)as CreatedDate,convert(varchar,b.purOrderDate,103)as purOrderDate"
                Str += " From item_purc as a"
                Str += " left join tbl_PurchaseOrderMain as b on a.poid=b.poid"
                Str += " where a.compId=" & Session("CompanyId").ToString & ""
                Str += " and a.WardId=" & Session("BranchID").ToString & ""
                Str += " and a.TxnYear=" & Session("TxnYear").ToString & ""
                Str += "  order by a.GRNNo desc"
                Dim dt As New DataTable()
                dt = ObjComFun.GetTableBySqlQuery(Str)
            Dim sb As System.Text.StringBuilder = New StringBuilder()
                sb.Append("<table class='table table-hover table-nomargin table-bordered dataTable dataTable-column_filter' data-column_filter_types='null,text,select,select,daterange,text,null' data-column_filter_dateformat='dd/mm/yy' data-nosort='0'>")
                sb.Append("<thead>")
                sb.Append("<tr>")
                sb.Append("<th>SR. NO</th>")
                sb.Append("<th>GRN NO</th>")
                sb.Append("<th>GRN DATE</th>")
                sb.Append("<th>PO NO</th>")
                sb.Append("<th>PO DATE</th>")
                sb.Append("<th>INVOICE NO</th>")
                sb.Append("<th>INVOICE DATE</th>")
                sb.Append("<th>PURCHASE FROM</th>")
                sb.Append("<th class='hidden-480'>Options</th>")
                sb.Append("</tr>")
                sb.Append("</thead>")
                sb.Append("<tbody>")
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        sb.Append("<tr>")
                        sb.Append("<td style='width:8%; text-align:center;'>" & Convert.ToInt32(i + 1) & "</td>")
                        sb.Append("<td>" & dt.Rows(i)("PINOVB").ToString() & "</td>")
                        sb.Append("<td>" & dt.Rows(i)("CreatedDate").ToString() & "</td>")
                        sb.Append("<td>" & dt.Rows(i)("PurOrderId").ToString() & "</td>")
                        sb.Append("<td>" & dt.Rows(i)("purOrderDate").ToString() & "</td>")
                        sb.Append("<td>" & dt.Rows(i)("InvNo").ToString() & "</td>")
                        sb.Append("<td>" & dt.Rows(i)("InvDate").ToString() & "</td>")
                        sb.Append("<td>" & dt.Rows(i)("PurchaseFrom").ToString() & "</td>")
                        sb.Append("<td class='hidden-480' style='width:15%; text-align:center;'>")
                    sb.Append("<a onclick=""javascript:return confirm('Do You Really Want To Edit This?');"" href=""fgrn.aspx?B=" & dt.Rows(i)("PINO").ToString() & """ runat=""server"" class=""btn btn-success"" rel=""tooltip"" title=""Edit""><i class='fa fa-edit'></i></a>")
                    sb.Append("<a onclick=""javascript:return confirm('Do You Really Want To Delete This?');"" href=""fgrn.aspx?C=" & dt.Rows(i)("PINO").ToString() & """ runat=""server"" class=""btn btn-inverse"" rel=""tooltip"" title=""Delete""><i class='fa fa-times'></i></a>")
                    sb.Append("<a target=""_blank"" href=""NewReports/GrNSingle.aspx?GrnId=" & dt.Rows(i)("PINO").ToString() & """ runat=""server"" class=""btn btn-success"" rel=""tooltip"" title=""Print""><i class='fa fa-print'></i></a>")
                        sb.Append("</td>")
                        sb.Append("</tr>")
                    Next
                    sb.Append("</tbody>")
                    sb.Append("</table>")
                    PlaceHolder1.Controls.Clear()
                    Literal1.Text = sb.ToString()
                    PlaceHolder1.Controls.Add(Literal1)
                Else
                    For i As Integer = 0 To dt.Rows.Count - 1
                        sb.Append("<tr>")
                        sb.Append("<td style='width:8%; text-align:center;'>" & Convert.ToInt32(i + 1) & "</td>")
                        sb.Append("<td>" & dt.Rows(i)("PINOVB").ToString() & "</td>")
                        sb.Append("<td>" & dt.Rows(i)("CreatedDate").ToString() & "</td>")
                        sb.Append("<td>" & dt.Rows(i)("PurOrderId").ToString() & "</td>")
                        sb.Append("<td>" & dt.Rows(i)("purOrderDate").ToString() & "</td>")
                        sb.Append("<td>" & dt.Rows(i)("InvNo").ToString() & "</td>")
                        sb.Append("<td>" & dt.Rows(i)("InvDate").ToString() & "</td>")
                        sb.Append("<td>" & dt.Rows(i)("PurchaseFrom").ToString() & "</td>")
                        sb.Append("<td class='hidden-480' style='width:10%; text-align:center;'>")
                    sb.Append("<a onclick=""javascript:return confirm('Do You Really Want To Edit This?');"" href=""fgrn.aspx?B=" & dt.Rows(i)("PINO").ToString() & """ runat=""server"" class=""btn btn-success"" rel=""tooltip"" title=""Edit""><i class='fa fa-edit'></i></a>")
                    sb.Append("<a onclick=""javascript:return confirm('Do You Really Want To Delete This?');"" href=""fgrn.aspx?C=" & dt.Rows(i)("PINO").ToString() & """ runat=""server"" class=""btn btn-inverse"" rel=""tooltip"" title=""Delete""><i class='fa fa-times'></i></a>")
                        sb.Append("</td>")
                        sb.Append("</tr>")
                    Next
                    sb.Append("</tbody>")
                    sb.Append("</table>")
                    PlaceHolder1.Controls.Clear()
                    Literal1.Text = sb.ToString()
                    PlaceHolder1.Controls.Add(Literal1)
                End If

        Catch ex As Exception
            txtmsg.Text = "Error in btnShowRecords_Click" & ex.Message
            txtmsg.ForeColor = Drawing.Color.Red
        End Try
    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        BlankControls()
        btnsave.Enabled = True
        GetMaxNo()
        pnlMain.Enabled = True
        txtSerialNo.Focus()
        Fill_ddl()
        txtGRNNo.Enabled = True
        txtSerialNo.Focus()
        ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "tmp", "<script type='text/javascript'>var uri = window.location.toString();if (uri.indexOf('?') > 0) {var clean_uri = uri.substring(0, uri.indexOf('?'));window.history.replaceState({}, document.title, clean_uri);}</script>", False)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "ActivePanel", "javascript:ActivePanel(1); ", True)
    End Sub

    Protected Sub BtnReturntoview_Click(sender As Object, e As System.EventArgs) Handles BtnReturntoview.Click
        BlankControls()
        btnsave.Enabled = True
        GetMaxNo()
        pnlMain.Enabled = True
        txtSerialNo.Focus()
        Fill_ddl()
        txtGRNNo.Enabled = True
        txtSerialNo.Focus()
        ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "tmp", "<script type='text/javascript'>var uri = window.location.toString();if (uri.indexOf('?') > 0) {var clean_uri = uri.substring(0, uri.indexOf('?'));window.history.replaceState({}, document.title, clean_uri);}</script>", False)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "ActivePanel", "javascript:ActivePanel(1); ", True)
    End Sub
End Class
