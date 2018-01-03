<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ElectronicBankStatement.ascx.cs" Inherits="BetAndWin.PaymentService.Web.Controls.CashManagement.ElectronicBankStatement" %>
<%@ Import Namespace="Microsoft.Security.Application"%>
<%@ Register TagPrefix="ctrl" Namespace="BetAndWin.PaymentService.Web.Controls" Assembly="BetAndWin.PaymentService.Web" %>
<%@ Register TagPrefix="frm" Assembly="Framework.Web.UI" NameSpace="Framework.Web.UI"%>
<%@ Register TagPrefix="fsm2" TagName="EBStatementDetails" Src="~/Controls/CashManagement/ElectronicBankStatementDetails.ascx" %>
<%@ Import namespace="System.Data"%>
<fsm2:EBStatementDetails id="bankStatementDetails" runat="server"></fsm2:EBStatementDetails>
<br />
<asp:repeater id="bankStatementTransactionsRepeater" runat="server" EnableViewState="False" OnItemCreated="bankStatementTransactionsRepeater_ItemCreated" OnItemDataBound="bankStatementTransactionsRepeater_ItemDataBound">
	<HeaderTemplate>
		<table class="account-list" border="0" cellpadding="0" cellspacing="0">
			<tr class="headerbar">
				<td class="ulcorner"><img border="0" src="Images/spacer.gif" width="6" height="6" alt="" /></td>
				<td colspan="9" class="caption" width="100%">
					<frm:LocalizableLabel id="resultsLabel" Runat="Server">Transactions</frm:localizablelabel>
					&nbsp;
					<frm:HtmlEncodedLabel runat="server" id="transactionCountLabel" />
				</td>
				<td class="urcorner"><img border="0" src="Images/spacer.gif" width="6" height="6" alt="" /></td>
			</tr>
			<tr class="topbar">
				<td class="borderleft">&nbsp;</td>
				<td class="title"><frm:LocalizableLabel id="LocalizableLabel1" Runat="Server">ValueDate</frm:localizablelabel></td>
				<td class="title"><frm:LocalizableLabel id="LocalizableLabel2" Runat="Server">EntryDate</frm:localizablelabel></td>
				<td class="title"><frm:LocalizableLabel id="LocalizableLabel3" Runat="Server">Credit/Debit</frm:localizablelabel></td>
				<td class="title"><frm:LocalizableLabel id="LocalizableLabel4" Runat="Server">Amount</frm:localizablelabel></td>
				<td class="title"><frm:LocalizableLabel id="LocalizableLabel5" Runat="Server">Currency</frm:localizablelabel></td>
				<td class="title"><frm:LocalizableLabel id="LocalizableLabel6" Runat="Server">TransactionType</frm:localizablelabel></td>
				<td class="title"><frm:LocalizableLabel id="LocalizableLabel7" Runat="Server">AccountOwnerReference</frm:localizablelabel></td>
				<td class="title"><frm:LocalizableLabel id="LocalizableLabel8" Runat="Server">AccountServicingInstitutionReference</frm:localizablelabel></td>
				<td class="title"><frm:LocalizableLabel id="LocalizableLabel9" Runat="Server">TransactionInfo</frm:localizablelabel></td>
				<td class="borderright">&nbsp;</td>
			</tr>
	</HeaderTemplate>
	<ItemTemplate>
		<tr class="normal" id="bankStatementTransactionRow" runat="server">
			<td class="borderleft">&nbsp;</td>
			<td class="item"><frm:HtmlEncodedLabel runat="server" id="valueDateLabel" /></td>
			<td class="item"><frm:HtmlEncodedLabel  id="entryDateLabel" runat="server" /></td>
			<td class="item"><frm:HtmlEncodedLabel id="paymentDirectionLabel" runat="server" /></td>
			<td class="item"><frm:HtmlEncodedLabel id="transactionAmountLabel" runat="server" /></td>
			<td class="item"><%# Microsoft.Security.Application.Encoder.HtmlEncode(((IDataRecord)Container.DataItem)["Currency"].ToString())%></td>
			<td class="item"><%# Microsoft.Security.Application.Encoder.HtmlEncode(((IDataRecord)Container.DataItem)["TransactionType"].ToString())%></td>
			<td class="item"><%# Microsoft.Security.Application.Encoder.HtmlEncode(((IDataRecord)Container.DataItem)["AccountOwnerReference"].ToString())%></td>
			<td class="item"><%# Microsoft.Security.Application.Encoder.HtmlEncode(((IDataRecord)Container.DataItem)["AccountServicingReference"].ToString())%></td>
			<td class="item"><%# Microsoft.Security.Application.Encoder.HtmlEncode(((IDataRecord)Container.DataItem)["TransactionInfo"].ToString())%></td>
			<td class="borderright">&nbsp;</td>
		</tr>
	</ItemTemplate>
	<FooterTemplate>
		<tr class="bottombar">
			<td class="llcorner"><img border="0" src="Images/spacer.gif" width="6" height="6" alt="" /></td>
			<td colspan="9" class="borderbottom"><img alt="" src="Images/spacer.gif" border="0" width="1" height="1" /></td>
			<td class="lrcorner"><img border="0" src="Images/spacer.gif" width="6" height="6" alt="" /></td>
		</tr>
		</table> 
	</FooterTemplate>
</asp:repeater>
