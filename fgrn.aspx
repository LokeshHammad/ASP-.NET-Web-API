<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="fgrn.aspx.vb" Inherits="fgrn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="FLAT/css/Common.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function showPopUp() {
            var modalSaveMsg = '<%= mpeSaving.ClientID %>';
            $find(modalSaveMsg).show();
            return true;
        }
        function HidePopUp() {
            $find('<%= mpeSaving.ClientID%>').hide();
            return false;
        }
        function Active() {
            $('#L1').removeClass('active');
            $('#L2').removeClass('active');
            $('#L3').removeClass('active');
            $('#L4').removeClass('active');
            $('#L5').removeClass('active');
            $('#L6').addClass('active');
            $('#L7').removeClass('active');
            $('#L8').removeClass('active');
            $('#L9').removeClass('active');
			$('#L10').removeClass('active');
        }
        window.onload = function () {
            Active();
        };
        function ActivePanel(APID) {
            if (APID == 1) {
                document.getElementById('<%=PnlShow.ClientID%>').style.display = 'inline';
                document.getElementById('<%=PnlAddNew.ClientID%>').style.display = 'none';
            }
            else if (APID == 2) {
                document.getElementById('<%=PnlShow.ClientID%>').style.display = 'none';
                document.getElementById('<%=PnlAddNew.ClientID%>').style.display = 'inline';
            }
            else {
                document.getElementById('<%=PnlShow.ClientID%>').style.display = 'none';
                document.getElementById('<%=PnlAddNew.ClientID%>').style.display = 'none';
            }
        }
        function getConfirmation() {
            var retVal = confirm("GRN Details Saved Successfully. Do you want to add more?");
            if (retVal == true) {
                ActivePanel(2);
                return true;
            }
            else {
                ActivePanel(1);
                return false;
            }
        }
        function forSave() {
            if (document.getElementById("<%=txtGRNNo.ClientID %>").value == false) {
                alert("GRN No. Required.");
                document.getElementById("<%=txtGRNNo.ClientID %>").focus();
                return false
            }
            if (document.getElementById("<%=txtinvoiceno.ClientID %>").value == false) {
                alert("Invoice No. Required.");
                document.getElementById("<%=txtinvoiceno.ClientID %>").focus();
                return false
            }
            if (document.getElementById("<%=txtInvoicedate.ClientID %>").value == false) {
                alert("Invoice Date Required.");
                document.getElementById("<%=txtInvoicedate.ClientID %>").focus();
                return false
            }
            if (document.getElementById("<%=ddlPurchaseOrder.ClientID %>").value == false) {
                alert("Purchase Order No. Required.");
                document.getElementById("<%=ddlPurchaseOrder.ClientID %>").focus();
                return false
            }
            showPopUp();
            return true;
        }
        function forok() {
            if (document.getElementById("<%=ddlPurchaseOrder.ClientID %>").value == false) {
                alert("Purchase Order No. Required.");
                document.getElementById("<%=ddlPurchaseOrder.ClientID %>").focus();
                return false
            }
            return true;
        }
        function forSelect(chkBox, txtQty, hdnQty, txtRate, txtAmt, txtDisPer, txtDisAmt, txtExPer, txtExAmt, txtVatPer, txtVatAmt, txtNetAmt, hdnNetAmt, hdnVatAmt, hdnVatPer, hdnExAmt, hdnExPer, hdnDisAmt, hdnDisPer, hdnAmt, hdnRate, txtScrapQty, txtRejectQty, txtReceivedQty, hdnScrapQty, hdnRejectQty, hdnReceivedQty) {
            var CHKVALUE = (document.getElementById(chkBox).checked);
            if (CHKVALUE == false) {
                document.getElementById(txtReceivedQty).disabled = true;
                document.getElementById(txtRejectQty).disabled = true;
                document.getElementById(txtRate).disabled = true;
                document.getElementById(txtDisPer).disabled = true;
                document.getElementById(txtExPer).disabled = true;
                document.getElementById(txtVatPer).disabled = true;
                document.getElementById(txtScrapQty).disabled = true;
                var vr_GrandTotal = document.getElementById("<%=hdnTotalNet.ClientID %>").value;
                var vr_NetAmt = document.getElementById(txtNetAmt).value;
                document.getElementById(hdnNetAmt).value = vr_NetAmt;
                vr_GrandTotal = parseFloat(vr_GrandTotal) - parseFloat(vr_NetAmt);
                document.getElementById("<%=txtTotalNet.ClientID %>").value = vr_GrandTotal.toFixed(2);
                document.getElementById("<%=hdnTotalNet.ClientID %>").value = vr_GrandTotal.toFixed(2);
                var vr_GrandTotal = document.getElementById("<%=hdnTotalGross.ClientID %>").value;
                var vr_NetAmt = document.getElementById(txtAmt).value;
                document.getElementById(hdnAmt).value = vr_NetAmt;
                vr_GrandTotal = parseFloat(vr_GrandTotal) - parseFloat(vr_NetAmt);
                document.getElementById("<%=txtTotalGross.ClientID %>").value = vr_GrandTotal.toFixed(2);
                document.getElementById("<%=hdnTotalGross.ClientID %>").value = vr_GrandTotal.toFixed(2);
                var vr_GrandTotal = document.getElementById("<%=hdnTotalDisc.ClientID %>").value;
                var vr_NetAmt = document.getElementById(txtDisAmt).value;
                document.getElementById(hdnDisAmt).value = vr_NetAmt;
                vr_GrandTotal = parseFloat(vr_GrandTotal) - parseFloat(vr_NetAmt);
                document.getElementById("<%=txtTotalDisc.ClientID %>").value = vr_GrandTotal.toFixed(2);
                document.getElementById("<%=hdnTotalDisc.ClientID %>").value = vr_GrandTotal.toFixed(2);
                var vr_GrandTotal = document.getElementById("<%=hdnTotalEx.ClientID %>").value;
                var vr_NetAmt = document.getElementById(txtExAmt).value;
                document.getElementById(hdnExAmt).value = vr_NetAmt;
                vr_GrandTotal = parseFloat(vr_GrandTotal) - parseFloat(vr_NetAmt);
                document.getElementById("<%=txtTotalEx.ClientID %>").value = vr_GrandTotal.toFixed(2);
                document.getElementById("<%=hdnTotalEx.ClientID %>").value = vr_GrandTotal.toFixed(2);
                var vr_GrandTotal = document.getElementById("<%=hdnTotalVat.ClientID %>").value;
                var vr_NetAmt = document.getElementById(txtVatAmt).value;
                document.getElementById(hdnVatAmt).value = vr_NetAmt;
                vr_GrandTotal = parseFloat(vr_GrandTotal) - parseFloat(vr_NetAmt);
                document.getElementById("<%=txtTotalVat.ClientID %>").value = vr_GrandTotal.toFixed(2);
                document.getElementById("<%=hdnTotalVat.ClientID %>").value = vr_GrandTotal.toFixed(2);
            }
            else {

                document.getElementById(txtReceivedQty).disabled = false;
                document.getElementById(txtRejectQty).disabled = false;
                document.getElementById(txtRate).disabled = false;
                document.getElementById(txtDisPer).disabled = false;
                document.getElementById(txtExPer).disabled = false;
                document.getElementById(txtVatPer).disabled = false;
                document.getElementById(txtScrapQty).disabled = false;
                var vr_GrandTotal = document.getElementById("<%=hdnTotalNet.ClientID %>").value;
                var vr_NetAmt = document.getElementById(txtNetAmt).value;
                document.getElementById(hdnNetAmt).value = vr_NetAmt;
                vr_GrandTotal = parseFloat(vr_GrandTotal) + parseFloat(vr_NetAmt);
                document.getElementById("<%=txtTotalNet.ClientID %>").value = vr_GrandTotal.toFixed(2);
                document.getElementById("<%=hdnTotalNet.ClientID %>").value = vr_GrandTotal.toFixed(2);
                var vr_GrandTotal = document.getElementById("<%=hdnTotalGross.ClientID %>").value;
                var vr_NetAmt = document.getElementById(txtAmt).value;
                document.getElementById(hdnAmt).value = vr_NetAmt;
                vr_GrandTotal = parseFloat(vr_GrandTotal) + parseFloat(vr_NetAmt);
                document.getElementById("<%=txtTotalGross.ClientID %>").value = vr_GrandTotal.toFixed(2);
                document.getElementById("<%=hdnTotalGross.ClientID %>").value = vr_GrandTotal.toFixed(2);
                var vr_GrandTotal = document.getElementById("<%=hdnTotalDisc.ClientID %>").value;
                var vr_NetAmt = document.getElementById(txtDisAmt).value;
                document.getElementById(hdnDisAmt).value = vr_NetAmt;
                vr_GrandTotal = parseFloat(vr_GrandTotal) + parseFloat(vr_NetAmt);
                document.getElementById("<%=txtTotalDisc.ClientID %>").value = vr_GrandTotal.toFixed(2);
                document.getElementById("<%=hdnTotalDisc.ClientID %>").value = vr_GrandTotal.toFixed(2);
                var vr_GrandTotal = document.getElementById("<%=hdnTotalEx.ClientID %>").value;
                var vr_NetAmt = document.getElementById(txtExAmt).value;
                document.getElementById(hdnExAmt).value = vr_NetAmt;
                vr_GrandTotal = parseFloat(vr_GrandTotal) + parseFloat(vr_NetAmt);
                document.getElementById("<%=txtTotalEx.ClientID %>").value = vr_GrandTotal.toFixed(2);
                document.getElementById("<%=hdnTotalEx.ClientID %>").value = vr_GrandTotal.toFixed(2);
                var vr_GrandTotal = document.getElementById("<%=hdnTotalVat.ClientID %>").value;
                var vr_NetAmt = document.getElementById(txtVatAmt).value;
                document.getElementById(hdnVatAmt).value = vr_NetAmt;
                vr_GrandTotal = parseFloat(vr_GrandTotal) + parseFloat(vr_NetAmt);
                document.getElementById("<%=txtTotalVat.ClientID %>").value = vr_GrandTotal.toFixed(2);
                document.getElementById("<%=hdnTotalVat.ClientID %>").value = vr_GrandTotal.toFixed(2);
            }
        }
        function forAmount(chkBox, txtQty, hdnQty, txtRate, txtAmt, txtDisPer, txtDisAmt, txtExPer, txtExAmt, txtVatPer, txtVatAmt, txtNetAmt, hdnNetAmt, hdnVatAmt, hdnVatPer, hdnExAmt, hdnExPer, hdnDisAmt, hdnDisPer, hdnAmt, hdnRate, txtScrapQty, txtRejectQty, txtReceivedQty, hdnScrapQty, hdnRejectQty, hdnReceivedQty) {
            var CHKVALUE = (document.getElementById(chkBox).checked);

            if (CHKVALUE == false) {
                document.getElementById(hdnAmt).value = parseFloat(document.getElementById(txtAmt).value);
                var vr_GrandTotalDisc = parseFloat(document.getElementById("<%=txtTotalGross.ClientID %>").value) + parseFloat(document.getElementById(txtAmt).value) - parseFloat(document.getElementById(hdnAmt).value);
                document.getElementById("<%=txtTotalGross.ClientID %>").value = vr_GrandTotalDisc.toFixed(2);
                document.getElementById("<%=hdnTotalGross.ClientID %>").value = vr_GrandTotalDisc.toFixed(2);
                document.getElementById(hdnAmt).value = parseFloat(document.getElementById(txtAmt).value);

                document.getElementById(hdnDisAmt).value = parseFloat(document.getElementById(txtDisAmt).value);
                var vr_GrandTotalDisc = parseFloat(document.getElementById("<%=txtTotalDisc.ClientID %>").value) + parseFloat(document.getElementById(txtDisAmt).value) - parseFloat(document.getElementById(hdnDisAmt).value);
                document.getElementById("<%=txtTotalDisc.ClientID %>").value = vr_GrandTotalDisc.toFixed(2);
                document.getElementById("<%=hdnTotalDisc.ClientID %>").value = vr_GrandTotalDisc.toFixed(2);
                document.getElementById(hdnDisAmt).value = parseFloat(document.getElementById(txtDisAmt).value);

                document.getElementById(hdnExAmt).value = parseFloat(document.getElementById(txtExAmt).value);
                var vr_GrandTotalex = parseFloat(document.getElementById("<%=txtTotalEx.ClientID %>").value) + parseFloat(document.getElementById(txtExAmt).value) - parseFloat(document.getElementById(hdnExAmt).value);
                document.getElementById("<%=txtTotalEx.ClientID %>").value = vr_GrandTotalex.toFixed(2);
                document.getElementById("<%=hdnTotalEx.ClientID %>").value = vr_GrandTotalex.toFixed(2);
                document.getElementById(hdnExAmt).value = parseFloat(document.getElementById(txtExAmt).value);

                document.getElementById(hdnVatAmt).value = parseFloat(document.getElementById(txtVatAmt).value);
                var vr_GrandTotalVAT = parseFloat(document.getElementById("<%=txtTotalVat.ClientID %>").value) + parseFloat(document.getElementById(txtVatAmt).value) - parseFloat(document.getElementById(hdnVatAmt).value);
                document.getElementById("<%=txtTotalVat.ClientID %>").value = vr_GrandTotalVAT.toFixed(2);
                document.getElementById("<%=hdnTotalVat.ClientID %>").value = vr_GrandTotalVAT.toFixed(2);
                document.getElementById(hdnVatAmt).value = parseFloat(document.getElementById(txtVatAmt).value);

                document.getElementById(hdnNetAmt).value = parseFloat(document.getElementById(txtNetAmt).value);
                var vr_GrandTotal = parseFloat(document.getElementById("<%=txtTotalNet.ClientID %>").value) + parseFloat(document.getElementById(txtNetAmt).value) - parseFloat(document.getElementById(hdnNetAmt).value);
                document.getElementById("<%=txtTotalNet.ClientID %>").value = vr_GrandTotal.toFixed(2);
                document.getElementById("<%=hdnTotalNet.ClientID %>").value = vr_GrandTotal.toFixed(2);
                document.getElementById(hdnNetAmt).value = parseFloat(document.getElementById(txtNetAmt).value);
            }
            else {
                var Qty = document.getElementById(txtReceivedQty).value;
                var Rate = document.getElementById(txtRate).value;
                var Amount = parseFloat(Qty) * parseFloat(Rate);
                document.getElementById(txtAmt).value = Amount.toFixed(2);

                var vr_DisAmt = Amount;
                var old_DA = document.getElementById(hdnAmt).value;
                var da = parseFloat(document.getElementById("<%=txtTotalGross.ClientID %>").value);
                var vr_DiscAmtTotal = parseFloat(da) + parseFloat(vr_DisAmt) - parseFloat(old_DA);
                document.getElementById("<%=txtTotalGross.ClientID %>").value = vr_DiscAmtTotal.toFixed(2);
                document.getElementById("<%=hdnTotalGross.ClientID %>").value = vr_DiscAmtTotal.toFixed(2);
                document.getElementById(hdnAmt).value = Amount.toFixed(2);
                //Disc Amount
                var DisPer = document.getElementById(txtDisPer).value;
                var DisAmt = parseFloat(DisPer / 100) * parseFloat(Amount);
                document.getElementById(txtDisAmt).value = DisAmt.toFixed(2);

                var vr_DisAmt = DisAmt;
                var old_DA = document.getElementById(hdnDisAmt).value;
                var da = parseFloat(document.getElementById("<%=txtTotalDisc.ClientID %>").value);
                var vr_DiscAmtTotal = parseFloat(da) + parseFloat(vr_DisAmt) - parseFloat(old_DA);
                document.getElementById("<%=txtTotalDisc.ClientID %>").value = vr_DiscAmtTotal.toFixed(2);
                document.getElementById("<%=hdnTotalDisc.ClientID %>").value = vr_DiscAmtTotal.toFixed(2);
                document.getElementById(hdnDisAmt).value = DisAmt.toFixed(2);

                var ExPer = document.getElementById(txtExPer).value;
                var ExAmt = parseFloat(ExPer / 100) * parseFloat(Amount);
                document.getElementById(txtExAmt).value = ExAmt.toFixed(2);

                var vr_ExAmt = ExAmt;
                var old_EA = document.getElementById(hdnExAmt).value;
                var ea = parseFloat(document.getElementById("<%=txtTotalEx.ClientID %>").value);
                var vr_ExAmtTotal = parseFloat(ea) + parseFloat(vr_ExAmt) - parseFloat(old_EA);
                document.getElementById("<%=txtTotalEx.ClientID %>").value = vr_ExAmtTotal.toFixed(2);
                document.getElementById("<%=hdnTotalEx.ClientID %>").value = vr_ExAmtTotal.toFixed(2);
                document.getElementById(hdnExAmt).value = ExAmt.toFixed(2);

                //Vat Amount
                var VatPer = document.getElementById(txtVatPer).value;
                var VatAmt = parseFloat(VatPer / 100) * parseFloat(Amount);
                document.getElementById(txtVatAmt).value = VatAmt.toFixed(2);

                var vr_VatAmt = VatAmt;
                var old_vA = document.getElementById(hdnVatAmt).value;
                var va = parseFloat(document.getElementById("<%=txtTotalVat.ClientID %>").value);
                var vr_vatAmtTotal = parseFloat(va) + parseFloat(vr_VatAmt) - parseFloat(old_vA);
                document.getElementById("<%=txtTotalVat.ClientID %>").value = vr_vatAmtTotal.toFixed(2);
                document.getElementById("<%=hdnTotalVat.ClientID %>").value = vr_vatAmtTotal.toFixed(2);
                document.getElementById(hdnVatAmt).value = VatAmt.toFixed(2);

                //Net Amount
                var NetAmount = 0;
                NetAmount = parseFloat(Amount) - parseFloat(DisAmt) + parseFloat(ExAmt) + parseFloat(VatAmt);
                document.getElementById(txtNetAmt).value = NetAmount.toFixed(2);
                var vr_NetAmount = NetAmount;
                var old_vr_NetAmount = document.getElementById(hdnNetAmt).value;

                var na = parseFloat(document.getElementById("<%=txtTotalNet.ClientID %>").value);
                var vr_NetTotal = parseFloat(na) + parseFloat(vr_NetAmount) - parseFloat(old_vr_NetAmount);
                document.getElementById("<%=txtTotalNet.ClientID %>").value = vr_NetTotal.toFixed(2);
                document.getElementById("<%=hdnTotalNet.ClientID %>").value = vr_NetTotal.toFixed(2);
                document.getElementById(hdnNetAmt).value = NetAmount.toFixed(2);
            }
        }
    </script>
	<script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
        prm.add_beginRequest(BeginRequestHandler);
        // Raised after an asynchronous postback is finished and control has been returned to the browser.
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            //Shows the modal popup - the update progress
            var popup = $find('<%= mpeSaving.ClientID %>');
            if (popup != null) {
                popup.show();
            }
        }
        function EndRequestHandler(sender, args) {
            //Hide the modal popup - the update progress
            var popup = $find('<%= mpeSaving.ClientID %>');
            if (popup != null) {
                popup.hide();
            }
        }
    </script>
    <asp:Panel ID="PnlShow" runat="server" Style="display: none;">
        <div class="row">
            <div class="col-sm-12">
                <div class="box box-color orange box-condensed box-bordered">
                    <div class="box-title">
                        <table width="100%">
                            <tr>
                                <td align="left" valign="middle">
                                    <h3>
                                        GRN DETAILS
                                    </h3>
                                </td>
                                <td align="right" valign="middle" style="padding-right: 10px;">
                                    <button class="btn btn-inverse" onclick='ActivePanel(2); return false;'>
                                        ADD NEW</button>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="box-content nopadding">
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="PnlAddNew" runat="server" Style="display: none;">
        <div class="row">
            <div class="col-sm-12">
                <div class="box box-color orange box-condensed box-bordered">
                    <div class="box-title">
                        <table width="100%">
                            <tr>
                                <td align="left" valign="middle">
                                    <h3>
                                        GRN DETAILS
                                    </h3>
                                </td>
                                <td align="right" valign="middle" style="padding-right: 10px;">
                                    <%--<button class="btn btn-inverse" onclick='ActivePanel(1); return false;'>
                                        RETURN TO VIEW</button>--%>
                                    <asp:Button ID="BtnReturntoview" runat="server" Text="RETURN TO VIEW" class="btn btn-inverse" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="box-content nopadding" align="center">
                        <table>
                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblGRNId" runat="server" Visible="False" Font-Names="Arial" Font-Size="6pt"></asp:Label>
                                    <asp:Label ID="lblSesion" runat="server" Font-Names="Arial" ForeColor="#CC3300" Font-Size="6pt"
                                        Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <table border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td colspan="6" align="left" valign="top">
                                                        <asp:TextBox ID="txtmsg" runat="server" BackColor="Transparent" BorderStyle="None"
                                                            Font-Bold="False" Font-Size="8pt" ForeColor="Red" Width="100%" AutoCompleteType="Disabled"
                                                            Font-Names="Arial" ReadOnly="True" Height="16px" Style="margin-right: 0px"></asp:TextBox>
                                                        <asp:TextBox ID="txttbleMsg" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                            BorderStyle="None" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"
                                                            ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top">
                                    <asp:Panel ID="pnlMain" runat="server">
                                        <asp:UpdatePanel ID="UpdMain" runat="server">
                                            <ContentTemplate>
                                                <table class="style1">
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td align="right">
                                                                        Serial No :
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtSerialNo" runat="server" ReadOnly="True" TabIndex="1" Width="150px"
                                                                            class="form-control" Style="font-size: 12px; font-weight: normal; font-family: Arial;"></asp:TextBox>
                                                                    </td>
                                                                    <td align="right">
                                                                        <span style="color: #FF0000; font-weight: bold">*</span>GRN No :
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtGRNNo" runat="server" TabIndex="1" Width="80px" class="form-control"
                                                                            Style="font-size: 12px; font-weight: normal; font-family: Arial;"></asp:TextBox>
                                                                    </td>
                                                                    <td align="right">
                                                                        <span style="color: #FF0000; font-weight: bold">*</span>Invoice No :
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtinvoiceno" runat="server" TabIndex="2" Width="100px" class="form-control"
                                                                            Style="font-size: 12px; font-weight: normal; font-family: Arial;"></asp:TextBox>
                                                                    </td>
                                                                    <td align="right">
                                                                        Invoice Date :
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtInvoicedate" runat="server" TabIndex="3" Width="100px" Wrap="False"
                                                                            class="form-control" Style="font-size: 12px; font-weight: normal; font-family: Arial;"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                                            ClearTextOnInvalid="true" CultureName="ar-LY" DisplayMoney="Left" Enabled="True"
                                                                            ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                                            OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="txtInvoicedate">
                                                                        </cc1:MaskedEditExtender>
                                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                            PopupButtonID="ImageButton1" PopupPosition="BottomRight" TargetControlID="txtInvoicedate">
                                                                        </cc1:CalendarExtender>
                                                                        <cc1:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                                            ControlToValidate="txtInvoicedate" Display="Dynamic" Font-Names="Arial" Font-Size="8pt"
                                                                            ValidationGroup="VldgrppopDriver" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" Height="19px"
                                                                            ImageUrl="~/Images/datepicker.gif" TabIndex="4" Width="20px" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="9">
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <span style="color: #FF0000; font-weight: bold">*</span>PO No :
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlPurchaseOrder" runat="server" AutoPostBack="True" Width="150px"
                                                                            TabIndex="5" Height="33px" class="form-control" Style="font-size: 12px; font-weight: normal;
                                                                            font-family: Arial;">
                                                                        </asp:DropDownList>
                                                                        <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server" PromptCssClass="ListSearchExtender"
                                                                            QueryPattern="Contains" TargetControlID="ddlPurchaseOrder">
                                                                        </cc1:ListSearchExtender>
                                                                    </td>
                                                                    <td align="right">
                                                                        Purchase From :
                                                                    </td>
                                                                    <td align="left" colspan="4">
                                                                        <asp:TextBox ID="txtPurchaseFrom" runat="server" TabIndex="6" Width="265px" class="form-control"
                                                                            Style="font-size: 12px; font-weight: normal; font-family: Arial;"></asp:TextBox>
                                                                    </td>
                                                                    <td align="left" colspan="2">
                                                                        <asp:Button ID="AddBtn" runat="server" TabIndex="7" Text="OK" ValidationGroup="GrdBtnAdd"
                                                                            Height="34px" class="btn btn-warning" Width="50px" OnClientClick='return forok();' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="pnlGrd" runat="server" BorderColor="Silver" BorderWidth="1px" Height="400px"
                                                                ScrollBars="Auto" Style="margin-right: 0px" Width="770px">
                                                                <asp:GridView ID="GRDPO" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                                                    CellSpacing="1" DataKeyNames="PODetailID" GridLines="None" PageSize="50" Width="1500px">
                                                                    <FooterStyle Font-Bold="True" />
                                                                    <RowStyle BackColor="#EFF3FB" />
                                                                    <EditRowStyle BackColor="#2461BF" />
                                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ID" Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPODetailID" runat="server" Text='<%# Eval("PODetailID") %>'></asp:Label>
                                                                                <asp:Label ID="lblPRId" runat="server" Text='<%# Eval("PRId") %>'></asp:Label>
                                                                                <asp:Label ID="lblPINO" runat="server" Text='<%# Eval("PINO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Item Name-Company">
                                                                            <ItemTemplate>
                                                                                <div style="width: 120px;">
                                                                                    <asp:Label ID="lblGlobalItemName" runat="server" Text='<%# Eval("GlobalItemName") %>'></asp:Label>
                                                                                    <asp:Label ID="lblGlobalItemId" runat="server" Text='<%# Eval("GlobalItemId") %>'
                                                                                        Visible="false"></asp:Label>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Item Type">
                                                                            <ItemTemplate>
                                                                                <div style="width: 100px;">
                                                                                    <asp:Label ID="lblItemType" runat="server" Text='<%# Eval("ItemType") %>'></asp:Label>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Unit">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("Unit") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Ordered Qty">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField ID="hdnQty" runat="server" />
                                                                                <asp:TextBox ID="txtQty" runat="server" ReadOnly="true" Text='<%# Eval("Quantity") %>'
                                                                                    Width="65px" class="form-control" Style="font-size: 12px; font-weight: normal;
                                                                                    font-family: Arial;">
                                                                                </asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="txtQty11" runat="server" TargetControlID="txtQty"
                                                                                    ValidChars="1234567-890.">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Received Qty">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField ID="hdnReceivedQty" runat="server" />
                                                                                <asp:TextBox ID="txtReceivedQty" runat="server" Enabled="false" Text='<%# Eval("ReceivedQty") %>'
                                                                                    Width="65px" class="form-control" Style="font-size: 12px; font-weight: normal;
                                                                                    font-family: Arial;">
                                                                                </asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="ftxttxtRecQty" runat="server" TargetControlID="txtReceivedQty"
                                                                                    ValidChars="1234567-890.">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Rejected Qty">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField ID="hdnRejectQty" runat="server" />
                                                                                <asp:TextBox ID="txtRejectQty" runat="server" Enabled="false" class="form-control"
                                                                                    Style="font-size: 12px; font-weight: normal; font-family: Arial;" Text='<%# Eval("RejectQty") %>'
                                                                                    Width="65px">
                                                                                </asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="ftxttxRejectQty" runat="server" TargetControlID="txtRejectQty"
                                                                                    ValidChars="1234567-890.">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Rate">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtRate" runat="server" Enabled="false" ReadOnly="true" Text='<%# Eval("Rate") %>'
                                                                                    Width="65px" class="form-control" Style="font-size: 12px; font-weight: normal;
                                                                                    font-family: Arial;"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="txtRate11" runat="server" TargetControlID="txtRate"
                                                                                    ValidChars="1234567-890.">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                                <asp:HiddenField ID="hdnRate" runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Amount">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtAmt" runat="server" ReadOnly="true" Text='<%# Eval("Amt") %>'
                                                                                    Width="65px" class="form-control" Style="font-size: 12px; font-weight: normal;
                                                                                    font-family: Arial;"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="txtAmt11" runat="server" TargetControlID="txtAmt"
                                                                                    ValidChars="1234567-890.">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                                <asp:HiddenField ID="hdnAmt" runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Disc %">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtDisPer" runat="server" Enabled="false" ReadOnly="true" Text='<%# Eval("DisPer") %>'
                                                                                    Width="65px" class="form-control" Style="font-size: 12px; font-weight: normal;
                                                                                    font-family: Arial;"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="txtDisc11" runat="server" TargetControlID="txtDisPer"
                                                                                    ValidChars="1234567-890.">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                                <asp:HiddenField ID="hdnDisPer" runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Disc. Amt.">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtDisAmt" runat="server" AutoPostBack="false" ReadOnly="true" Text='<%# Eval("DisAmt") %>'
                                                                                    Width="65px" class="form-control" Style="font-size: 12px; font-weight: normal;
                                                                                    font-family: Arial;"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="txtDiscAmt11" runat="server" TargetControlID="txtDisAmt"
                                                                                    ValidChars="1234567-890.">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                                <asp:HiddenField ID="hdnDisAmt" runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Ex. %">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtExPer" runat="server" Enabled="false" ReadOnly="true" Text='<%# Eval("ExPer") %>'
                                                                                    Width="65px" class="form-control" Style="font-size: 12px; font-weight: normal;
                                                                                    font-family: Arial;"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="txtEx11" runat="server" TargetControlID="txtExPer"
                                                                                    ValidChars="1234567-890.">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                                <asp:HiddenField ID="hdnExPer" runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Ex. Amt">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtExAmt" runat="server" ReadOnly="true" Text='<%# Eval("ExAmt") %>'
                                                                                    Width="65px" class="form-control" Style="font-size: 12px; font-weight: normal;
                                                                                    font-family: Arial;"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="txtExAmt11" runat="server" TargetControlID="txtExAmt"
                                                                                    ValidChars="1234567-890.">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                                <asp:HiddenField ID="hdnExAmt" runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Vat %">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtVatPer" runat="server" Enabled="false" ReadOnly="true" Text='<%# Eval("VatPer") %>'
                                                                                    Width="65px" class="form-control" Style="font-size: 12px; font-weight: normal;
                                                                                    font-family: Arial;"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="txtVat11" runat="server" TargetControlID="txtVatPer"
                                                                                    ValidChars="1234567-890.">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                                <asp:HiddenField ID="hdnVatPer" runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Vat Amt">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtVatAmt" runat="server" ReadOnly="true" Text='<%# Eval("VatAmt") %>'
                                                                                    Width="65px" class="form-control" Style="font-size: 12px; font-weight: normal;
                                                                                    font-family: Arial;"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="txtVatAmt11" runat="server" TargetControlID="txtVatAmt"
                                                                                    ValidChars="1234567-890.">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                                <asp:HiddenField ID="hdnVatAmt" runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Net Amt">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtNetAmt" runat="server" ReadOnly="true" Text='<%# Eval("NetAmt") %>'
                                                                                    Width="65px" class="form-control" Style="font-size: 12px; font-weight: normal;
                                                                                    font-family: Arial;"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="NetAmt11" runat="server" TargetControlID="txtNetAmt"
                                                                                    ValidChars="1234567-890.">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                                <asp:HiddenField ID="hdnNetAmt" runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Scrap Qty">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField ID="hdnScrapQty" runat="server" />
                                                                                <asp:TextBox ID="txtScrapQty" runat="server" Enabled="false" Text='<%# Eval("ScrapQty") %>'
                                                                                    Width="65px" class="form-control" Style="font-size: 12px; font-weight: normal;
                                                                                    font-family: Arial;">
                                                                                </asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="ftxttxRejectQtytxtScrapQty" runat="server" TargetControlID="txtScrapQty"
                                                                                    ValidChars="1234567-890.">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Remarks">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Eval("Remarks") %>' Width="150px"
                                                                                    class="form-control" Style="font-size: 12px; font-weight: normal; font-family: Arial;"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                                                                    <HeaderStyle BackColor="#8f8f8f" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                                                                        Height="30px" />
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="9">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td align="right" valign="middle">
                                                                        Remark :
                                                                    </td>
                                                                    <td rowspan="5" valign="top">
                                                                        <asp:TextBox ID="txtRemarksBottam" runat="server" Height="200px" TabIndex="8" TextMode="MultiLine"
                                                                            Width="518px" Wrap="False" class="form-control" Style="font-size: 12px; font-weight: normal;
                                                                            font-family: Arial; resize: none;"></asp:TextBox>
                                                                    </td>
                                                                    <td align="right">
                                                                        Gross Amount :
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTotalGross" runat="server" ReadOnly="true" TabIndex="15" Width="100px"
                                                                            class="form-control" Style="font-size: 12px; font-weight: normal; font-family: Arial;
                                                                            text-align: right;">0.00</asp:TextBox>
                                                                        <asp:HiddenField ID="hdnTotalGross" runat="server" Value="0" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                    </td>
                                                                    <td align="right">
                                                                        Disc. Amount :
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTotalDisc" runat="server" ReadOnly="true" TabIndex="16" Width="100px"
                                                                            class="form-control" Style="font-size: 12px; font-weight: normal; font-family: Arial;
                                                                            text-align: right;">0.00</asp:TextBox>
                                                                        <asp:HiddenField ID="hdnTotalDisc" runat="server" Value="0" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                    </td>
                                                                    <td align="right">
                                                                        Ex Amount :
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTotalEx" runat="server" ReadOnly="true" TabIndex="17" Width="100px"
                                                                            class="form-control" Style="font-size: 12px; font-weight: normal; font-family: Arial;
                                                                            text-align: right;">0.00</asp:TextBox>
                                                                        <asp:HiddenField ID="hdnTotalEx" runat="server" Value="0" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                    </td>
                                                                    <td align="right">
                                                                        Vat Amount :
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTotalVat" runat="server" ReadOnly="true" TabIndex="18" Width="100px"
                                                                            class="form-control" Style="font-size: 12px; font-weight: normal; font-family: Arial;
                                                                            text-align: right;">0.00</asp:TextBox>
                                                                        <asp:HiddenField ID="hdnTotalVat" runat="server" Value="0" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                    </td>
                                                                    <td align="right">
                                                                        Net Amount :
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTotalNet" runat="server" AutoPostBack="false" ReadOnly="true"
                                                                            TabIndex="19" Width="100px" class="form-control" Style="font-size: 12px; font-weight: normal;
                                                                            font-family: Arial; text-align: right;">0.00</asp:TextBox>
                                                                        <asp:HiddenField ID="hdnTotalNet" runat="server" Value="0" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnsave" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="middle">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="middle">
                                    <table>
                                        <tr>
                                            <td valign="top">
                                                <asp:Button ID="btnsave" TabIndex="9" runat="server" Text="Save" CssClass="btn btn-success"
                                                    Width="95px" OnClientClick="if(forSave()) return ValidateEmail(); else return false;">
                                                </asp:Button>
                                            </td>
                                            <td valign="top">
                                                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CausesValidation="False"
                                                    TabIndex="10" CssClass="btn btn-inverse" Width="95px"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top">
                                    <asp:Button ID="btnHiddenPopUp" runat="server" Style="display: none" />
                                    <cc1:ModalPopupExtender ID="mpeSaving" runat="server" BackgroundCssClass="modalBackground"
                                        DropShadow="true" PopupControlID="panSaving" RepositionMode="RepositionOnWindowScroll"
                                        TargetControlID="btnHiddenPopUp" />
                                    <div style="text-align: center; padding: 10px;">
                                        <asp:Panel ID="panSaving" runat="server" BackColor="#ffffdd" Style="display: none;">
                                            <asp:Image ID="LabelMsgSaving" runat="server" ImageUrl="~/flat/images/loading.gif" />
                                        </asp:Panel>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
