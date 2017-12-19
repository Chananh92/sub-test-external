<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ElectronicBankStatementCommonFilter.ascx.cs" Inherits="BetAndWin.PaymentService.Web.Controls.CashManagement.ElectronicBankStatementCommonFilter" %>

<%@ Register TagPrefix="fsm2" TagName="MFilter" Src="~/Controls/PaymentFilters/MerchantFilter.ascx" %>
<%@ Register TagPrefix="fsm2" TagName="BAFilter" Src="~/Controls/PaymentFilters/BankAccountFilter.ascx" %>
<%@ Register TagPrefix="fsm2" TagName="DTFilter" Src="~/Controls/PaymentFilters/DateTimeFilter.ascx" %>

<%@ Register TagPrefix="frm" Assembly="Framework.Web.UI" NameSpace="Framework.Web.UI"%>

<!-- ---------------------------------------------------Start filter declaration----------------------------------- -->

<tr>
    <td class="borderleft">&nbsp;</td>
	    <fsm2:MFilter id="merchantFilter" AutoPostBack="True" runat="server" IsAllAllowed="false" OnSelectedIndexChanged="merchantFilter_SelectedIndexChanged">
	    </fsm2:MFilter>
	<td class="borderright">&nbsp;</td>
</tr>
<tr>	
	<td class="borderleft">&nbsp;</td>
	    <fsm2:BAFilter id="bankAccountFilter" AutoPostBack="False" runat="server" IsAllAllowed="true">
	    </fsm2:BAFilter>
	<td class="borderright">&nbsp;</td>
</tr>	
<tr>	
	<td class="borderleft">&nbsp;</td>
	    <fsm2:DTFilter id="bankStatementDateFilter" AutoPostBack="False" Label="BankStatementDate" runat="server" IsOptional=false>
	    </fsm2:DTFilter>
	<td class="borderright">&nbsp;</td>
</tr>

<%// SVN_version: $svn_version_id$ %>