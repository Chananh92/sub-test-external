<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Caches.ascx.cs" Inherits="BetAndWin.PaymentService.Web.Controls.Maintenance.Caches" %>
<%@ Register TagPrefix="frm" Assembly="Framework.Web.UI" NameSpace="Framework.Web.UI"%>
<%@ Import Namespace="Framework.Caching" %>

<asp:Repeater ID="cachesRepeater" runat="server" OnItemCreated="cachesRepeater_ItemCreated">
	<HeaderTemplate>
		<table class="account-list" border="0" cellpadding="0" cellspacing="0" width="50%">
			<tr class="headerbar">
				<td class="ulcorner"><img border="0" src="Images/spacer.gif" width="6" height="6" alt="" /></td>
				<td class="caption" width="100%" colspan="4">
					<frm:LocalizableLabel id="cachesLabel" runat="Server">Caches</frm:LocalizableLabel></td>
				<td class="urcorner"><img border="0" src="Images/spacer.gif" width="6" height="6" alt="" /></td>
			</tr>
			<tr class="topbar">
	    		<td class="borderleft">&nbsp;</td>
				<td class="title"><frm:LocalizableLabel id="scopeLabel" runat="Server">Scope</frm:LocalizableLabel></td>
				<td class="title"><frm:LocalizableLabel id="nameLabel" runat="Server">Name</frm:LocalizableLabel></td>
				<td class="title"><frm:LocalizableLabel id="descriptionLabel" runat="Server">ItemCount</frm:LocalizableLabel></td>
				<td class="title"><frm:LocalizableLabel id="expireLabel" runat="Server">Expire</frm:LocalizableLabel></td>
    			<td class="borderleft">&nbsp;</td>
			</tr>
	</HeaderTemplate>
	<ItemTemplate>
		<tr class="normal" id="cacheRow" runat="server">
			<td class="borderleft">&nbsp;</td>
			<td class="item"><%# Microsoft.Security.Application.Encoder.HtmlEncode(((ICache)Container.DataItem).Scope.ToString()) %></td>
			<td class="item">
				<a href='default.aspx?view=Maintenance/Cache&Scope=<%# Microsoft.Security.Application.Encoder.UrlEncode(((ICache)Container.DataItem).Scope.ToString()) %>&Name=<%# Microsoft.Security.Application.Encoder.UrlEncode(((ICache)Container.DataItem).Name)%>'>
			         <%# Microsoft.Security.Application.Encoder.HtmlEncode(((ICache)Container.DataItem).Name)%>
				</a>
			</td>
			<td class="item"><%# ((ICache)Container.DataItem).ItemCount%></td>
			<td class="item">
                <frm:HtmlEncodedLabel ID="cacheName" runat="server" Visible="False" Text="<%# ((ICache)Container.DataItem).Name %>"></frm:HtmlEncodedLabel>
			    <asp:CheckBox ID="expireCheckBox" runat="server" Checked="false" />
			</td>
			<td class="borderright">&nbsp;</td>
		</tr>
	</ItemTemplate>
	<FooterTemplate>
		<tr class="bottombar">
			<td class="llcorner"><img border="0" src="Images/spacer.gif" width="6" height="6" alt="" /></td>
			<td colspan="4" class="borderbottom"><img alt="" src="Images/spacer.gif" border="0" width="1" height="1" /></td>
			<td class="lrcorner"><img border="0" src="Images/spacer.gif" width="6" height="6" alt="" /></td>
		</tr>
		</table> 
        <table class="account-new" cellSpacing="0" cellPadding="0" border="0" width="50%">
	        <colgroup>
		        <col width="6" />
		        <col width="30%" />
		        <col width="70%" />
		        <col width="6" />
	        </colgroup>
	        <tr>
		        <td class="title" colSpan="4"><frm:LocalizableLabel id="actionsHeaderLabel" runat="server" EnableViewState="False">Actions</frm:localizablelabel></td>
	        </tr>
	        <tr>
		        <td class="borderleft">&nbsp;</td>
		        <td class="label">&nbsp;</td>
		        <td class="controls">
			        <asp:Button ID="expireButton" runat="server" OnClick="expireButton_Click" Text="Expire Selected" OnClientClick="return confirm('Are you sure to expire selected caches?');"/>
		        </td>
		        <td class="borderright">&nbsp;</td>
	        </tr>
	        <tr class="bottombar-dark">
		        <td class="llcorner"><div>&nbsp;</div></td>
		        <td class="bottomlabel" colSpan="2"><div>&nbsp;</div></td>
		        <td class="lrcorner"><div>&nbsp;</div></td>
	        </tr>
        </table>
	</FooterTemplate>
</asp:Repeater>
&nbsp;




<%// SVN_version: $svn_version_id$ %>