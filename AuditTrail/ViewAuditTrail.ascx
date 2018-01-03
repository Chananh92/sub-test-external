<%@ Control language="C#" autoeventwireup="true" codebehind="ViewAuditTrail.ascx.cs" inherits="BetAndWin.PaymentService.Web.Controls.AuditTrail.ViewAuditTrail" %>
<%@ Import namespace="Microsoft.Security.Application" %>
<%@ Register tagprefix="pf" tagname="DTFilter" src="~/Controls/PaymentFilters/DateTimeFilter.ascx" %>
<%@ Import namespace="System.Data" %>
<div runat="server" id="auditTrailFilter">
	<table class="account-new filters" cellspacing="0" cellpadding="5" border="0">
		<tr class="caption"><td colspan="2">FILTERS</td></tr>
		<tr><pf:DTFilter id="fromCreatedOnControl" label="FromCreatedOn" runat="server" isoptional="true"></pf:DTFilter></tr>
		<tr><pf:DTFilter id="toCreatedOnControl" label="ToCreatedOn" runat="server" isoptional="true"></pf:DTFilter></tr>
		<tr>
			<td class="label" style="width: 250px"><frm:LocalizableLabel id="entityLabel" runat="Server">Entity</frm:LocalizableLabel></td>
			<td class="controls" style="width: 350px"><asp:DropDownList id="entityDropDownList" runat="server"></asp:DropDownList></td>
		</tr>
		<tr>
			<td class="label"><frm:LocalizableLabel id="rowIDLabel" runat="Server">RowID</frm:LocalizableLabel></td>
			<td class="controls"><asp:TextBox id="rowIDTextBox" runat="server"></asp:TextBox></td>
		</tr>
		<tr>
			<td class="label"><frm:LocalizableLabel id="operationLabel" runat="Server">Operation</frm:LocalizableLabel></td>
			<td class="controls"><asp:DropDownList id="operationDropDownList" runat="server"></asp:DropDownList></td>
		</tr>
		<tr>
			<td class="label"><frm:LocalizableLabel id="reasonLabel" runat="Server">Reason</frm:LocalizableLabel></td>
			<td class="controls"><asp:TextBox id="reasonTextBox" runat="server" width="100%"></asp:TextBox></td>
		</tr>
		<tr>
			<td class="label"><frm:LocalizableLabel id="applicationNameLabel" runat="Server">ApplicationName</frm:LocalizableLabel></td>
			<td class="controls"><asp:TextBox id="applicationNameTextBox" runat="server" width="100%"></asp:TextBox></td>
		</tr>
		<tr>
			<td class="label"><frm:LocalizableLabel id="computerNameLabel" runat="Server">HostName</frm:LocalizableLabel></td>
			<td class="controls"><asp:TextBox id="computerNameTextBox" runat="server"></asp:TextBox></td>
		</tr>
		<tr>
			<td class="label"><frm:LocalizableLabel id="identityLabel" runat="Server">Identity</frm:LocalizableLabel></td>
			<td class="controls"><asp:TextBox id="identityTextBox" runat="server" width="100%"></asp:TextBox></td>
		</tr>
		<tr>
			<td class="label"><frm:LocalizableLabel id="dbUserNameLabel" runat="Server">DbUserName</frm:LocalizableLabel></td>
			<td class="controls"><asp:TextBox id="dbUserNameTextBox" runat="server" width="100%"></asp:TextBox></td>
		</tr>
		<tr>
			<td class="label"><frm:LocalizableLabel id="statePatternLabel" runat="Server">StatePattern</frm:LocalizableLabel></td>
			<td class="controls">
				<asp:TextBox id="statePatternTextBox" runat="server" width="100%"></asp:TextBox>
				<asp:CustomValidator id="statePatternCV" runat="server" controltovalidate="statePatternTextBox" errormessage="TooFewFilteringValues" onservervalidate="statePatternCV_ServerValidate" display="Dynamic"></asp:CustomValidator>
			</td>
		</tr>
		<tr>
			<td class="label"><frm:LocalizableLabel id="countOfItemsLabel" runat="Server">CountOfItems</frm:LocalizableLabel></td>
			<td class="controls"><asp:TextBox id="countOfItemsTextBox" runat="server" text="100"></asp:TextBox></td>
		</tr>
		<tr class="bar">
			<td colspan="2" align="center" class="label"><asp:Button id="viewButton" runat="server" text="View" style="font-weight: bold" onclick="viewButton_Click" /></td>
		</tr>
	</table>
	<br />
	<br />
</div>
<asp:Repeater id="viewAuditTrailRepeater" runat="server" onitemcreated="viewAuditTrailRepeater_ItemCreated" onitemdatabound="viewAuditTrailRepeater_ItemDataBound" enableviewstate="False">
	<HeaderTemplate>
		<!-- begin payments list -->
		<table class="account-list" border="0" cellpadding="0" cellspacing="0" style="width: 98%">
			<tr class="headerbar">
				<td class="ulcorner"><img border="0" src="Images/spacer.gif" width="6" height="6" alt="" /></td>
				<td colspan="10" class="caption"><frm:LocalizableLabel id="resultsLabel" runat="Server">AuditTrailRecords</frm:LocalizableLabel></td>
				<td class="urcorner"><img border="0" src="Images/spacer.gif" width="6" height="6" alt="" /></td>
			</tr>
			<tr class="topbar">
				<td class="borderleft">&nbsp;</td>
				<td class="title"><frm:LocalizableLabel id="CreatedLabel" runat="Server">CreatedOn</frm:LocalizableLabel></td>
				<td class="title"><frm:LocalizableLabel id="TableLabel" runat="Server">Entity</frm:LocalizableLabel></td>
				<td class="title"><frm:LocalizableLabel id="RecordLabel" runat="Server">RowID</frm:LocalizableLabel></td>
				<td class="title"><frm:LocalizableLabel id="OperationLabel" runat="Server">Operation</frm:LocalizableLabel></td>
				<td class="title"><frm:LocalizableLabel id="ReasonLabel" runat="Server">Reason</frm:LocalizableLabel></td>
				<td class="title"><frm:LocalizableLabel id="IdentityLabel" runat="Server">Identity</frm:LocalizableLabel></td>
				<td class="title"><frm:LocalizableLabel id="DbUserlabel" runat="Server">DbUserName</frm:LocalizableLabel></td>
				<td class="title"><frm:LocalizableLabel id="ApplicationLabel" runat="Server">ApplicationName</frm:LocalizableLabel></td>
				<td class="title"><frm:LocalizableLabel id="HostLabel" runat="Server">HostName</frm:LocalizableLabel></td>
				<td class="title"><frm:LocalizableLabel id="StateLabel" runat="Server">State</frm:LocalizableLabel></td>
				<td class="borderleft">&nbsp;</td>
			</tr>
	</HeaderTemplate>
	<ItemTemplate>
		<tr id="paymentMethodRow" runat="server">
			<td class="borderleft">&nbsp;</td>
			<td class="item"><nobr><%# DateTimeHelper.FormatAsLocal((DateTime)((IDataRecord)Container.DataItem)["CreatedOn"])%></nobr></td>
			<td class="item"><nobr><%# Microsoft.Security.Application.Encoder.HtmlEncode(((IDataRecord)Container.DataItem)["EntityName"].ToString()) %></nobr></td>
			<td class="item"><%# (long)((IDataRecord)Container.DataItem)["RowID"]%></td>
			<td class="item"><%# Microsoft.Security.Application.Encoder.HtmlEncode(((IDataRecord)Container.DataItem)["OperationTypeName"].ToString()) %></td>
			<td class="item"><nobr><frm:ExpandableLiteral id="reasonExpandableLiteral" runat="server" EtcText="" MaxLength="20" Text='<%# ((IDataRecord)Container.DataItem)["Reason"] %>' ShowText="<img border='0' title='Show expanded text' src='Images/plus.gif' />" HideText="<img border='0' title='Hide expanded text' src='Images/minus.gif' />"></frm:ExpandableLiteral></nobr></td>
			<td class="item"><nobr><frm:ExpandableLiteral id="identityNameExpandableLiteral" runat="server" EtcText="" MaxLength="20" Text='<%# ((IDataRecord)Container.DataItem)["CreatedByIdentityName"] %>' ShowText="<img border='0' title='Show expanded text' src='Images/plus.gif' />" HideText="<img border='0' title='Hide expanded text' src='Images/minus.gif' />"></frm:ExpandableLiteral></nobr></td>
			<td class="item"><%# Microsoft.Security.Application.Encoder.HtmlEncode(((IDataRecord)Container.DataItem)["CreatedByDbUser"].ToString()) %></td>
			<td class="item"><nobr><frm:ExpandableLiteral id="applicationNameExpandableLiteral" runat="server" EtcText="" MaxLength="20" Text='<%# ((IDataRecord)Container.DataItem)["ApplicationName"] %>' ShowText="<img border='0' title='Show expanded text' src='Images/plus.gif' />" HideText="<img border='0' title='Hide expanded text' src='Images/minus.gif' />"></frm:ExpandableLiteral></nobr></td>
			<td class="item"><%# Microsoft.Security.Application.Encoder.HtmlEncode(((IDataRecord)Container.DataItem)["HostName"].ToString()) %></td>
			<td class="item"><a href='default.aspx?view=AuditTrail/ViewAuditTrailState&amp;id=<%# (long)((IDataRecord)Container.DataItem)["AuditTrailID"]%>'>View</a></td>
			<td class="borderright">&nbsp;</td>
		</tr>
	</ItemTemplate>
	<FooterTemplate>
		<tr class="bottombar">
			<td class="llcorner"><img border="0" src="Images/spacer.gif" width="6" height="6" alt="" /></td>
			<td colspan="10" class="borderbottom"><img alt="" src="Images/spacer.gif" border="0" width="1" height="1" /></td>
			<td class="lrcorner"><img border="0" src="Images/spacer.gif" width="6" height="6" alt="" /></td>
		</tr>
		</table> </table>
		<!-- end payments list -->
	</FooterTemplate>
</asp:Repeater>
<%// SVN_version: $svn_version_id$ %>
