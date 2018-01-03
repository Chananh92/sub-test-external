<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreditCardBinRangeBatchStateTypeFilter.ascx.cs" Inherits="BetAndWin.PaymentService.Web.Controls.CreditCardBinRangeBatchStateTypeFilter" %>

<%@ Register TagPrefix="ctrl" Namespace="BetAndWin.PaymentService.Web.Controls" Assembly="BetAndWin.PaymentService.Web" %>
<%@ Register TagPrefix="frm" Assembly="Framework.Web.UI" NameSpace="Framework.Web.UI"%>

<td class="label">
    <frm:LocalizableLabel id="creditCardBinRangeBatchStateTypeLabel" runat="server" EnableViewState="False">
        CreditCardBinRangeBatchStateType
    </frm:localizablelabel></td>
<td class="controls">
    <frm:DropDownListView id="creditCardBinRangeBatchStateTypeCombo" runat="server" Width="165px" ShouldLocalize="false" LocalizedContent="false"/>
</td>
<%// SVN_version: $svn_version_id$ %>
