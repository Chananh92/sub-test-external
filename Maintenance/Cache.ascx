<%@ Control language="C#" autoeventwireup="true" codebehind="Cache.ascx.cs" inherits="BetAndWin.PaymentService.Web.Controls.Maintenance.CacheControl" %>
<%@ Import namespace="Microsoft.Security.Application" %>
<%@ Register tagprefix="frm" assembly="Framework.Web.UI" namespace="Framework.Web.UI" %>
<table cellspacing="0" cellpadding="0" border="0" width="50%">
	<tr>
		<td>
			<!-- begin cache details -->
			<table class="account-new" cellspacing="0" cellpadding="0" border="0" width="100%">
				<colgroup>
					<col width="6" />
					<col width="30%" />
					<col width="70%" />
					<col width="6" />
				</colgroup>
				<tr>
					<td class="title" colspan="4"><frm:LocalizableLabel id="titleLabel" runat="server" enableviewstate="False">CacheDetails</frm:LocalizableLabel></td>
				</tr>
				<tr>
					<td class="borderleft">&nbsp;</td>
					<td class="label"><frm:LocalizableLabel id="nameLabel" runat="server" enableviewstate="False">Name</frm:LocalizableLabel></td>
					<td class="controls"><asp:Literal id="nameLiteral" runat="server"></asp:Literal></td>
					<td class="borderright">&nbsp;</td>
				</tr>
				<tr>
					<td class="borderleft">&nbsp;</td>
					<td class="label"><frm:LocalizableLabel id="scopeLabel" runat="server" enableviewstate="False">Scope</frm:LocalizableLabel></td>
					<td class="controls"><asp:DropDownList enabled="false" id="scopeDropDownList" rows="5" cols="1" style="width: 100%" runat="server" /></td>
					<td class="borderright">&nbsp;</td>
				</tr>
				<tr>
					<td class="borderleft">&nbsp;</td>
					<td class="label"><frm:LocalizableLabel id="itemCountLabel" runat="server" enableviewstate="False">ItemCount</frm:LocalizableLabel></td>
					<td class="controls"><asp:Literal id="itemCountLiteral" runat="server"></asp:Literal></td>
					<td class="borderright">&nbsp;</td>
				</tr>
				<tr>
					<td class="borderleft">&nbsp;</td>
					<td class="label"><frm:LocalizableLabel id="itemKeyLabel" runat="server" enableviewstate="False">ItemKey</frm:LocalizableLabel></td>
					<td class="controls"><asp:TextBox id="itemKeyInput" runat="server" style="width: 100%"></asp:TextBox></td>
					<td class="borderright">&nbsp;</td>
				</tr>
				<tr>
					<td class="borderleft">&nbsp;</td>
					<td class="label"><frm:LocalizableLabel id="itemValueLabel" runat="server" enableviewstate="False">ItemValue</frm:LocalizableLabel></td>
					<td class="controls"><asp:TextBox id="itemValueInput" runat="server" style="width: 100%" rows="10" textmode="MultiLine"></asp:TextBox></td>
					<td class="borderright">&nbsp;</td>
				</tr>
				<tr>
					<td class="borderleft">&nbsp;</td>
					<td class="label"></td>
					<td class="controls">
						<asp:Button text="Remove Item" runat="server" id="removeItemButton" onclick="removeItemButton_Click" onclientclick="return getSubmitConfirmation('Are you sure you want to remove item from cache?');" />
						<asp:Button text="Get Item" runat="server" id="getItemButton" onclick="getItemButton_Click" />
					</td>
					<td class="borderright">&nbsp;</td>
				</tr>
				<tr>
					<td class="borderleft">&nbsp;</td>
					<td class="label"></td>
					<td class="controls"><asp:Button text="Expire Whole Cache" runat="server" id="expireCacheButton" onclick="expireCacheButton_Click" onclientclick="return getSubmitConfirmation('Are you sure you want expire whole cache?');" /></td>
					<td class="borderright">&nbsp;</td>
				</tr>
				<tr>
					<td class="borderleft">&nbsp;</td>
					<td class="label"><frm:LocalizableLabel id="cacheContentsLabel" runat="server" enableviewstate="False">CacheContents</frm:LocalizableLabel></td>
					<td class="controls">
						<asp:Panel id="cacheKeysPanel" runat="server" height="100" width="100%" scrollbars="Vertical">
							<asp:Repeater id="cacheKeysRepeater" runat="server" enableviewstate="false" onitemcreated="cacheKeysRepeater_ItemCreated">
								<HeaderTemplate>
									<table>
								</HeaderTemplate>
								<ItemTemplate>
									<tr class="normal">
										<td><a id="cacheKeyLink" runat="server"><%# Microsoft.Security.Application.Encoder.HtmlEncode(((string)Container.DataItem))%></a></td>
									</tr>
								</ItemTemplate>
								<FooterTemplate>
									</table>
								</FooterTemplate>
							</asp:Repeater>
						</asp:Panel>
					</td>
					<td class="borderright">&nbsp;</td>
				</tr>
				<tr class="bottombar-dark">
					<td class="llcorner"><div>&nbsp;</div></td>
					<td class="bottomlabel" colspan="2"><div>&nbsp;</div></td>
					<td class="lrcorner"><div>&nbsp;</div></td>
				</tr>
			</table>
			<!-- end cache details -->
		</td>
		<td>&nbsp;</td>
		<td valign="top">&nbsp;</td>
		<td valign="top"><asp:Literal id="resultsLiteral" runat="server"></asp:Literal></td>
	</tr>
</table>
<br />
<script type="text/javascript"><!--
function getSubmitConfirmation(message)
{
    if(message == null) message = "Are you sure to commit the changes?";
    return confirm(message);
}
//--></script>

<%// SVN_version: $svn_version_id$ %>
