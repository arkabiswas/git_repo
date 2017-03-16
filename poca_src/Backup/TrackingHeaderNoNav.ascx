<%@ Control Language="vb" AutoEventWireup="false" Codebehind="TrackingHeaderNoNav.ascx.vb" Inherits="PPC.TrackingHeaderNoNav" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<meta name="vs_showGrid" content="False">
<link href="tracking.css" type="text/css" rel="stylesheet">
<table style="LEFT:-10px; POSITION:relative" bordercolor="black" cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr bgcolor="black" height="22">
		<td align="left" width="50%" colspan="2">&nbsp;
			<asp:label id="lblWelcome" cssclass="header" runat="server" /></td>
		<td align="right" width="*">
		</td>
	</tr>
	<tr bgcolor="white">
		<td class="logostyle" style="WIDTH: 638px; HEIGHT: 34px" colspan="3">
			<img id="imgTrackLogo" alt="FDA" src="header/bptrack_logo.jpg" runat="server">
			<asp:label style="POSITION:relative; TOP:-5px" id="lblTitle" runat="server" font-bold="false" font-size="130%" forecolor="#ffffff" width="266px" backcolor="Transparent" bordercolor="Transparent" borderstyle="None" borderwidth="0px" />
		</td>
	</tr>
	<tr>
		<td class="gContentSection" colspan="3">
			<asp:label id="lblLocation" runat="server" Width="555px" font-size="80%" style="LEFT:97px; POSITION:relative" />
		</td>
	</tr>
</table>
<br>
