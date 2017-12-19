<%@ Control Language="C#" AutoEventWireup="true" Codebehind="ViewAuditTrailState.ascx.cs" EnableViewState="false"
    Inherits="BetAndWin.PaymentService.Web.Controls.AuditTrail.ViewAuditTrailState" %>
<%@ Register TagPrefix="frm" Assembly="Framework.Web.UI" Namespace="Framework.Web.UI" %>
<%@ Register TagPrefix="pf" TagName="DTFilter" Src="~/Controls/PaymentFilters/DateTimeFilter.ascx" %>
<%@ Import Namespace="CQRPayments.PaymentService.DomainModel" %>

<style type="text/css">
span.text-difference
{
    font-weight: bold;
    background-color: black;
    color: #cccccc;
}
</style>

<table  class="account-new filters" cellspacing="0" cellpadding="5" border="0">
    <tr class="caption">
        <td colspan="2">AUDITTRAILRECORD</td>
    </tr>
    <tr>
        <td class="label"><frm:LocalizableLabel id="auditTrailIDLabel" runat="Server">AuditTrailID</frm:LocalizableLabel></td>
        <td class="controls"><asp:Literal id="auditTrailIDLiteral" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <td class="label"><frm:LocalizableLabel id="createdOnLabel" runat="Server">CreatedOn</frm:LocalizableLabel></td>
        <td class="controls"><asp:Literal id="createdOnLiteral" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <td class="label" style="width: 250px"><frm:LocalizableLabel id="entityNameLabel" runat="Server">Entity</frm:LocalizableLabel></td>
        <td class="controls" style="width: 350px"><asp:Literal id="entityNameLiteral" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <td class="label"><frm:LocalizableLabel id="rowIDLabel" runat="Server">RowID</frm:LocalizableLabel></td>
        <td class="controls"><asp:Literal id="rowIDLiteral" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <td class="label"><frm:LocalizableLabel id="operationNameLabel" runat="Server">Operation</frm:LocalizableLabel></td>
        <td class="controls"><asp:Literal id="operationNameLiteral" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <td class="label"><frm:LocalizableLabel id="reasonLabel" runat="Server">Reason</frm:LocalizableLabel></td>
        <td class="controls"><asp:Literal id="reasonLiteral" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <td class="label"><frm:LocalizableLabel id="applicationNameLabel" runat="Server">ApplicationName</frm:LocalizableLabel></td>
        <td class="controls"><asp:Literal id="applicationNameLiteral" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <td class="label"><frm:LocalizableLabel id="identityNameLabel" runat="Server">IdentityName</frm:LocalizableLabel></td>
        <td class="controls"><asp:Literal id="identityNameLiteral" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <td class="label"><frm:LocalizableLabel id="dbUserNameLabel" runat="Server">DbUserName</frm:LocalizableLabel></td>
        <td class="controls"><asp:Literal id="dbUserNameLiteral" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <td class="label"><frm:LocalizableLabel id="hostNameLabel" runat="Server">Host</frm:LocalizableLabel></td>
        <td class="controls"><asp:Literal id="hostNameLiteral" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <td class="label"><frm:LocalizableLabel id="oldStateLabel" runat="Server">OldState</frm:LocalizableLabel></td>
        <td class="controls"><asp:Literal id="oldStateLiteral" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <td class="label"><frm:LocalizableLabel id="newStateLabel" runat="Server">NewState</frm:LocalizableLabel></td>
        <td class="controls"><asp:Literal id="newStateLiteral" runat="server"></asp:Literal></td>
    </tr>
    <tr runat="server" id="stateDecompressedRow" visible="False">
        <td class="label" colspan="2">DecompressedContents</td>
    </tr>
    <tr runat="server" id="oldStateDecompressedRow" visible="False">
        <td class="label"><frm:LocalizableLabel id="oldStateDecompressedLabel" runat="Server">OldStateContentsDecompressed</frm:LocalizableLabel></td>
        <td class="controls"><asp:Literal id="oldStateDecompressedLiteral" runat="server"></asp:Literal></td>
    </tr>
    <tr runat="server" id="newStateDecompressedRow" visible="False">
        <td class="label"><frm:LocalizableLabel id="newStateDecompressedLabel" runat="Server">NewStateContentsDecompressed</frm:LocalizableLabel></td>
        <td class="controls"><asp:Literal id="newStateDecompressedLiteral" runat="server"></asp:Literal></td>
    </tr>
    <tr class="bar">
        <td colspan="2" align="center" class="label"></td>
    </tr>
</table>

<%// SVN_version: $svn_version_id$ %>
