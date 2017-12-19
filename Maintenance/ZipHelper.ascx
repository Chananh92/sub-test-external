<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ZipHelper.ascx.cs" Inherits="BetAndWin.PaymentService.Web.Controls.Maintenance.ZipHelper"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="frm" Assembly="Framework.Web.UI" Namespace="Framework.Web.UI" %>
<table class="account-new" cellpadding="0" cellspacing="0" border="0" width="400">
    <tr>
        <td class="ulcorner" width="6">
            <img height="6" alt="" src="Images/spacer.gif" width="6" border="0" /></td>
        <td class="title" colspan="3">
            <frm:LocalizableLabel ID="pcfLabel" runat="server" EnableViewState="False">Input</frm:LocalizableLabel></td>
        <td class="urcorner" width="6">
            <img height="6" alt="" src="Images/spacer.gif" width="6" border="0" /></td>
    </tr>
    <tr>
        <td class="ulcorner" width="6">
            <img height="6" alt="" src="Images/spacer.gif" width="6" border="0" /></td>
        <td colspan="3">
            <textarea id="inputArea" rows="15" cols="80" runat="server" wrap="soft" enableviewstate="false"></textarea></td>
        <td class="urcorner" width="6">
            <img height="6" alt="" src="Images/spacer.gif" width="6" border="0" /></td>
    </tr>
    <tr>
        <td colspan="5">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="ulcorner">
            <img height="6" alt="" src="Images/spacer.gif" width="6" border="0" /></td>
        <td class="title" colspan="3">
            <frm:LocalizableLabel ID="Localizablelabel1" runat="server" EnableViewState="False">Output</frm:LocalizableLabel></td>
        <td class="urcorner">
            <img height="6" alt="" src="Images/spacer.gif" width="6" border="0" /></td>
    </tr>
    <tr>
        <td class="ulcorner" width="6">
            <img height="6" alt="" src="Images/spacer.gif" width="6" border="0" /></td>
        <td colspan="3">
            <textarea id="outputArea" rows="15" cols="80" runat="server" wrap="soft" enableviewstate="false"></textarea></td>
        <td class="urcorner" width="6">
            <img height="6" alt="" src="Images/spacer.gif" width="6" border="0" /></td>
    </tr>
    <tr class="bar">
        <td class="borderleft">
            &nbsp;</td>
        <td class="label" align="left" width="175">
            <frm:LocalizableButton ID="packButton" runat="server" EnableViewState="False" Font-Bold="True"
                Text="Pack" OnClientClick="ClearOutput();"></frm:LocalizableButton>
            <frm:LocalizableButton ID="unpackButton" runat="server" EnableViewState="False" Font-Bold="True"
                Text="Unpack" OnClientClick="ClearOutput();"></frm:LocalizableButton>
            <frm:LocalizableButton ID="clearButton" runat="server" EnableViewState="False" Font-Bold="True"
                Text="Clear" OnClientClick="ClearOutput();"></frm:LocalizableButton>
            <br />
            <br />
            Conversion:
            <select id="dataTypeDropDownList" runat="server" enableviewstate="true">
            </select>
        </td>
        <td class="label" align="left" width="213">
            <table border="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        Compressor:</td>
                    <td>
                        <select runat="server" id="algorythmDropDownList" enableviewstate="true">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        Zip level:</td>
                    <td>
                        <select runat="server" id="compressionLevelDropDownList" enableviewstate="true">
                            <option value="0">Store only</option>
                            <option value="1">Minimum</option>
                            <option value="2">2</option>
                            <option value="3">3</option>
                            <option value="4">4</option>
                            <option value="5">Normal</option>
                            <option value="6">6</option>
                            <option value="7">7</option>
                            <option value="8">8</option>
                            <option value="9" selected="selected">Maximum</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        Zip password:</td>
                    <td>
                        <input type="text" size="15" runat="server" id="passwordTextBox" /></td>
                </tr>
            </table>
        </td>
        <td class="borderright" colspan="2">
            &nbsp;</td>
    </tr>
    <tr class="bottombar-dark">
        <td class="llcorner">
            <div>
                &nbsp;</div>
        </td>
        <td class="bottomlabel" colspan="3">
            <div>
                &nbsp;</div>
        </td>
        <td class="lrcorner">
            <div>
                &nbsp;</div>
        </td>
    </tr>
</table>

<script type="text/javascript"><!--
function ClearOutput()
{
    var outputArea = document.getElementById("_ctl0:outputArea");
    if(outputArea.value.indexOf("<") >= 0)
        outputArea.value = "";
}
//--></script>

<%// SVN_version: $svn_version_id$ %>
