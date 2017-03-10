<%@ Control Language="vb" AutoEventWireup="false" Codebehind="TrackingHeader.ascx.cs" Inherits="WebControlsTest.Controls.TrackingHeader" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table width="100%" border="0" bgcolor="black" cellpadding="0" cellspacing="0" bordercolor="black">
	<tr>
		<td width="50%" align="left">
			&nbsp;
			<asp:Label ID="lblWelcome" Runat="server" CssClass="header" />
		</td>
		<td width="50%" align="right">			
			<asp:Label ID="lblInfoBar" Runat="server" CssClass="infobarlink" />
			<asp:HyperLink EnableViewState=False NavigateUrl="~/information.aspx" CssClass="infobarlink" Runat=server>information</asp:HyperLink><span style="color:white;font-weight:bold;"> | </span> 
			<asp:HyperLink EnableViewState=False NavigateUrl="~/help.aspx" CssClass="infobarlink" Runat=server>help</asp:HyperLink><span style="color:white;font-weight:bold;"> | </span> 
			<asp:HyperLink EnableViewState=False NavigateUrl="~/about.aspx" CssClass="infobarlink" Runat=server>about</asp:HyperLink><span style="color:white;font-weight:bold;"> | </span> 
			<asp:HyperLink EnableViewState=False NavigateUrl="~/contact.aspx" CssClass="infobarlink" Runat=server>contact</asp:HyperLink>
			&nbsp;
		</td>
	</tr>
	<tr>
		<td colspan="2" class="logostyle"><IMG SRC="~/images/bptrack_logo.jpg" id="imgTrackLogo" runat="server"></td>
	</tr>
</table>
