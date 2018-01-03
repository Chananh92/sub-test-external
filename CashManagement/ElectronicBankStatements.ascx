<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ElectronicBankStatements.ascx.cs" Inherits="BetAndWin.PaymentService.Web.Controls.CashManagement.ElectronicBankStatements" %>
<%@ Import Namespace="Microsoft.Security.Application"%>
<%@ Register TagPrefix="ctrl" Namespace="BetAndWin.PaymentService.Web.Controls" Assembly="BetAndWin.PaymentService.Web" %>
<%@ Register TagPrefix="frm" Assembly="Framework.Web.UI" NameSpace="Framework.Web.UI"%>
<%@ Register TagPrefix="fsm2" TagName="EBSCFilter" Src="~/Controls/CashManagement/ElectronicBankStatementCommonFilter.ascx" %>
<%@ Import Namespace="System.Data" %>
<!-- start filters -->
<table class="account-new" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td class="ulcorner"><IMG height="6" alt="" src="Images/spacer.gif" width="6" border="0" /></td>
		<td class="title" colSpan="2"><frm:LocalizableLabel id="filtersLabel" Runat="Server">Filters</frm:LocalizableLabel></td>
		<td class="urcorner"><IMG height="6" alt="" src="Images/spacer.gif" width="6" border="0" /></td>
	</tr>
	<fsm2:EBSCFilter id="ebscFilter" runat="server"></fsm2:EBSCFilter>
	<tr class="bar">
		<td class="borderleft">&nbsp;</td>
		<td class="label" align="center" colSpan="2">
			<frm:LocalizableButton id="viewButton" runat="server" EnableViewState="False" Text="View" Font-Bold="True" onclick="viewButton_Click"></frm:LocalizableButton>
		</td>
		<td class="borderright">&nbsp;</td>
	</tr>
	<tr class="bottombar-dark">
		<td class="llcorner"><div>&nbsp;</div></td>
		<td class="bottomlabel" colSpan="2"><div>&nbsp;</div></td>
		<td class="lrcorner"><div>&nbsp;</div></td>
	</tr>
</table>&nbsp; 
<!-- end filters -->
<br />
<asp:repeater id="bankStatementsRepeater" runat="server" EnableViewState="False" OnItemCreated="bankStatementsRepeater_ItemCreated" OnItemDataBound="bankStatementsRepeater_ItemDataBound" OnItemCommand="bankStatementsRepeater_ItemCommand">
	<HeaderTemplate>
		<!-- begin bank statements list -->
		<table class="account-list" border="0" cellpadding="0" cellspacing="0">
			<tr class="headerbar">
				<td class="ulcorner"><img border="0" src="Images/spacer.gif" width="6" height="6" alt="" /></td>
				<td colspan="9" class="caption" width="100%">
					<frm:LocalizableLabel id="resultsLabel" Runat="Server" Visible="false">Results</frm:LocalizableLabel>
					&nbsp;
					<frm:HtmlEncodedLabel runat="server" id="itemCountLabel" Visible="false" />
				</td>
				<td class="urcorner"><img border="0" src="Images/spacer.gif" width="6" height="6" alt="" /></td>
			</tr>
			<tr class="topbar">
				<td class="borderleft">&nbsp;</td>
				<td class="title">
					<frm:LocalizableLabel id="LocalizableLabel1" Runat="Server" EncodeText="false">BankName<br />Account</frm:LocalizableLabel><br/>
					<frm:LocalizableLabel id="accountNumberHeaderLabel" Runat=server Visible="false">AccountNumber</frm:LocalizableLabel>
				</td>
				<td class="title"><frm:LocalizableLabel id="LocalizableLabel2" Runat="Server">BankStatementDate</frm:LocalizableLabel></td>
				<td class="title"><frm:LocalizableLabel id="LocalizableLabel3" Runat="Server">BankStatementNumber</frm:LocalizableLabel></td>
				<td class="title"><frm:LocalizableLabel id="LocalizableLabel4" Runat="Server">Currency</frm:LocalizableLabel></td>
				<td class="title"><frm:LocalizableLabel id="LocalizableLabel5" Runat="Server">StartingAccountBalance</frm:LocalizableLabel></td>
				<td class="title"><frm:LocalizableLabel id="LocalizableLabel6" Runat="Server">CreditsSumAmount</frm:LocalizableLabel></td>
				<td class="title"><frm:LocalizableLabel id="LocalizableLabel7" Runat="Server">DebitsSumAmount</frm:LocalizableLabel></td>
				<td class="title"><frm:LocalizableLabel id="LocalizableLabel8" Runat="Server">EndingAccountBalance</frm:LocalizableLabel></td>
				<td class="title"><frm:LocalizableLabel id="LocalizableLabel9" Runat="Server">ClosingAvailableBalance</frm:LocalizableLabel></td>		
				<td class="borderright">&nbsp;</td>
			</tr>
	</HeaderTemplate>
	<ItemTemplate>
		<tr class="normal" id="bankStatementsRow" runat="server">
			<td class="borderleft">&nbsp;</td>
			<td class="item">
				<frm:HtmlEncodedLabel id="bankNameLabel" runat="server" /><br />
				<asp:LinkButton id="viewAccountNumberButton" runat="server" CommandName="ViewAccountNumber" CommandArgument='<%#((int)((IDataRecord)Container.DataItem)["BankAccountID"]).ToString()%>'>
					<%# Microsoft.Security.Application.Encoder.HtmlEncode(((IDataRecord)Container.DataItem)["BankAccountName"].ToString())%>
				</asp:LinkButton><br />
				<frm:HtmlEncodedLabel id="accountNumberLabel" runat="server" Visible="false" />
			</td>
			<td class="item">
				<a href='default.aspx?view=CashManagement/ElectronicBankStatement&ID=<%#((int)((IDataRecord)Container.DataItem)["ExternalPaymentBatchID"]).ToString()%>'>
					<frm:HtmlEncodedLabel id="bankStatementDateLabel" runat="server" />
				</a>
			</td>
			<td class="item"><frm:HtmlEncodedLabel id="statementNumberLabel" runat="server" /></td>
			<td class="item"><frm:HtmlEncodedLabel id="currencyLabel" runat="server" /></td>
			<td class="item"><frm:HtmlEncodedLabel id="openingBalanceLabel" runat="server" /></td>
			<td class="item">
				<a href='default.aspx?view=CashManagement/ElectronicBankStatement&Direction=Credit&ID=<%#((int)((IDataRecord)Container.DataItem)["ExternalPaymentBatchID"]).ToString()%>'>
					<frm:HtmlEncodedLabel id="creditAmountLabel" runat="server" />
				</a>
			</td>
			<td class="item">
				<a href='default.aspx?view=CashManagement/ElectronicBankStatement&Direction=Debit&ID=<%#((int)((IDataRecord)Container.DataItem)["ExternalPaymentBatchID"]).ToString()%>'>
					<frm:HtmlEncodedLabel id="debitAmountLabel" runat="server" />
				</a>
			</td>
			<td class="item"><frm:HtmlEncodedLabel id="closingBalanceLabel" runat="server" /></td>
			<td class="item"><frm:HtmlEncodedLabel id="availBalanceLabel" runat="server" /></td>
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
		<!-- end bank statements list -->
	</FooterTemplate>
</asp:repeater>

<%// SVN_version: $svn_version_id$ %>
