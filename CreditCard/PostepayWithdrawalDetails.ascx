<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PostepayWithdrawalDetails.ascx.cs" Inherits="BetAndWin.PaymentService.Web.Controls.PostepayWithdrawalDetailsControl" %>
<table class="account-new" border="0" cellpadding="0" cellspacing="0" width="<%= Width %>">
	<tr>
		<td class="title" colspan="4"><frm:LocalizableLabel id="postepayWithdrawalDetails" runat="server" EnableViewState="False">PostepayWithdrawalDetails</frm:LocalizableLabel></td>
	</tr>
	<tr>
		<td class="borderleft">&nbsp;</td>
		<td class="label"><frm:LocalizableLabel id="firstnameLabel" runat="server" EnableViewState="False">Holder Firstname</frm:LocalizableLabel></td>
		<td class="controls"><asp:Literal id="firstnameLiteral" runat="server" EnableViewState="False"></asp:Literal></td>
		<td class="borderright">&nbsp;</td>
	</tr>
	<tr>
		<td class="borderleft">&nbsp;</td>
		<td class="label"><frm:LocalizableLabel id="lastnameLabel" runat="server" EnableViewState="False">Holder LastName</frm:LocalizableLabel></td>
		<td class="controls"><asp:Literal id="lastnameLiteral" runat="server" EnableViewState="False"></asp:Literal></td>
		<td class="borderright">&nbsp;</td>
	</tr>
</table>