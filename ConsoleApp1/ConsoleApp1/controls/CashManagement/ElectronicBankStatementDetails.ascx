<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ElectronicBankStatementDetails.ascx.cs" Inherits="BetAndWin.PaymentService.Web.Controls.CashManagement.ElectronicBankStatementDetails" %>
<%@ Register TagPrefix="ctrl" Namespace="BetAndWin.PaymentService.Web.Controls" Assembly="BetAndWin.PaymentService.Web" %>
<%@ Register TagPrefix="frm" Assembly="Framework.Web.UI" NameSpace="Framework.Web.UI"%>


<table class="account-new" cellspacing="0" cellpadding="0" border="0" width="50%">
	
	<colgroup>
		<col width="6" />
		<col width="30%" />
		<col width="70%" />
		<col width="6" />
	</colgroup>
	
	<tr>
		<td class="title" colSpan="4"><frm:LocalizableLabel id="bankStatementDetailLabel" runat="server" EnableViewState="False">BankStatementDetails</frm:LocalizableLabel></td>
	</tr>
	<tr>
		<td class="borderleft">&nbsp;</td>
		<td class="label"><frm:LocalizableLabel id="bankNameLabel" runat="server" EnableViewState="False">BankName</frm:LocalizableLabel></td>
		<td class="controls"><asp:literal id="bankNameLiteral" runat="server"></asp:literal></td>
		<td class="borderright">&nbsp;</td>
	</tr>
	<tr>
		<td class="borderleft">&nbsp;</td>
		<td class="label"><frm:LocalizableLabel id="accountNameLabel" runat="server" EnableViewState="False">AccountName</frm:LocalizableLabel></td>
		<td class="controls"><asp:literal id="accountNameLiteral" runat="server"></asp:literal></td>
		<td class="borderright">&nbsp;</td>
	</tr>
	<tr>
		<td class="borderleft">&nbsp;</td>
		<td class="label"><frm:LocalizableLabel id="accountNumberLabel" runat="server" EnableViewState="False">AccountNumber</frm:LocalizableLabel></td>
		<td class="controls">
		    <asp:literal id="accountNumberLiteral" runat="server">****</asp:literal>
		    <frm:LocalizableLinkButton ID="viewClearTextAccountNumberButton" runat="server" OnClick="viewClearTextAccountNumberButton_Click">View</frm:LocalizableLinkButton>
		</td>
		<td class="borderright">&nbsp;</td>
	</tr>
	<tr>
		<td class="borderleft">&nbsp;</td>
		<td class="label"><frm:LocalizableLabel id="bankStatementDateLabel" runat="server" EnableViewState="False">BankStatementDate</frm:LocalizableLabel></td>
		<td class="controls"><asp:literal id="bankStatementDateLiteral" runat="server"></asp:literal></td>
		<td class="borderright">&nbsp;</td>
	</tr>
	<tr>
		<td class="borderleft">&nbsp;</td>
		<td class="label"><frm:LocalizableLabel id="bankStatementNumberLabel" runat="server" EnableViewState="False">BankStatementNumber</frm:LocalizableLabel></td>
		<td class="controls">
		<a href='default.aspx?view=CashManagement/ElectronicBankStatement&ID=<%=ExternalPaymentBatchID%>'>
		<asp:literal id="bankStatementNumberLiteral" runat="server"></asp:literal>
		</a>
		</td>
		<td class="borderright">&nbsp;</td>
	</tr>
	<tr>
		<td class="borderleft">&nbsp;</td>
		<td class="label"><frm:LocalizableLabel id="currencyLabel" runat="server" EnableViewState="False">Currency</frm:LocalizableLabel></td>
		<td class="controls"><asp:literal id="currencyLiteral" runat="server"></asp:literal></td>
		<td class="borderright">&nbsp;</td>
	</tr>
	<tr>
		<td class="borderleft">&nbsp;</td>
		<td class="label"><frm:LocalizableLabel id="startingAccountBalanceLabel" runat="server" EnableViewState="False">StartingAccountBalance</frm:LocalizableLabel></td>
		<td class="controls"><asp:literal id="startingAccountBalanceLiteral" runat="server"></asp:literal></td>
		<td class="borderright">&nbsp;</td>
	</tr>
	<tr>
		<td class="borderleft">&nbsp;</td>
		<td class="label"><frm:LocalizableLabel id="creditsSumAmountLabel" runat="server" EnableViewState="False">CreditsSumAmount</frm:LocalizableLabel></td>
		<td class="controls">
		<a href='default.aspx?view=CashManagement/ElectronicBankStatement&Direction=Credit&ID=<%=ExternalPaymentBatchID%>'>	
		    <asp:literal id="creditsSumAmountLiteral" runat="server"></asp:literal>
		</a>
		</td>
		<td class="borderright">&nbsp;</td>
	</tr>
	<tr>
		<td class="borderleft">&nbsp;</td>
		<td class="label"><frm:LocalizableLabel id="debitsSumAmountLabel" runat="server" EnableViewState="False">DebitsSumAmount</frm:LocalizableLabel></td>
		<td class="controls">
		<a href='default.aspx?view=CashManagement/ElectronicBankStatement&Direction=Debit&ID=<%=ExternalPaymentBatchID%>'>	
		    <asp:literal id="debitsSumAmountLiteral" runat="server"></asp:literal>
		</a>
		</td>
		<td class="borderright">&nbsp;</td>
	</tr>
	<tr>
		<td class="borderleft">&nbsp;</td>
		<td class="label"><frm:LocalizableLabel id="endingAccountBalanceLabel" runat="server" EnableViewState="False">EndingAccountBalance</frm:LocalizableLabel></td>
		<td class="controls"><asp:literal id="endingAccountBalanceLiteral" runat="server"></asp:literal></td>
		<td class="borderright">&nbsp;</td>
	</tr>
	<tr>
		<td class="borderleft">&nbsp;</td>
		<td class="label"><frm:LocalizableLabel id="closingAvailableBalanceLabel" runat="server" EnableViewState="False">ClosingAvailableBalance</frm:LocalizableLabel></td>
		<td class="controls"><asp:literal id="closingAvailableBalanceLiteral" runat="server"></asp:literal></td>
		<td class="borderright">&nbsp;</td>
	</tr>
	
	<tr class="bottombar-dark">
		<td class="llcorner"><div>&nbsp;</div>
		</td>
		<td class="bottomlabel" colspan="2"><div>&nbsp;</div>
		</td>
		<td class="lrcorner"><div>&nbsp;</div>
		</td>
	</tr>
	
</table>
<%// SVN_version: $svn_version_id$ %>