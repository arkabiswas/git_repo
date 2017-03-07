<%@ Control Language="vb" AutoEventWireup="false" Codebehind="TrackingHeader.ascx.vb" Inherits="PPC.TrackingHeader" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<link href="tracking.css" type="text/css" rel="stylesheet" />
<link href="Styles.css" type="text/css" rel="stylesheet" />

	<div class="skiplinks"> 
        <a href="#SkipMainSiteLinks" class="skiplinks" tabindex="1" title="Skip Main Site Links">Skip Main Site Links</a>
    </div>
    
<table style="LEFT:-10px; POSITION:relative" borderColor="black" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr style="height:22px; background-color:Black;">
		<td align="left" width="50%">&nbsp;
			<asp:label id="lblWelcome" CssClass="header" Runat="server" />
		</td>
		<td align="right" width="*">
			<asp:label id="lblInfoBar" CssClass="infobarlink" Runat="server"></asp:label>
			<asp:hyperlink id="Hyperlink1" runat="server" CssClass="infobarlink" navigateurl="~/Home.aspx" enableviewstate="False" forecolor="white" title="Home">Home</asp:hyperlink>
			<span style="FONT-WEIGHT: bold; COLOR: white">| </span>
			<asp:hyperlink id="HyperLink2" CssClass="infobarlink" Runat="server" NavigateUrl="~/StartSearch.aspx" EnableViewState="False" forecolor="white" title="Search">Search</asp:hyperlink>
			<span style="FONT-WEIGHT: bold; COLOR: white">| </span>
			<asp:hyperlink id="HyperLink3" CssClass="infobarlink" Runat="server" NavigateUrl="~/Settings.aspx" EnableViewState="False" forecolor="white" title="Settings">Settings</asp:hyperlink>
			<span style="FONT-WEIGHT: bold; COLOR: white">| </span>
			<asp:hyperlink id="Hyperlink6" cssclass="infobarlink" runat="server" navigateurl="~/Comments.aspx" enableviewstate="False" forecolor="white" title="Comments">Comments</asp:hyperlink>&nbsp;
			<span style="FONT-WEIGHT: bold; COLOR: white">| </span>
			<asp:hyperlink id="HyperLink4" CssClass="infobarlink" Runat="server" NavigateUrl="#" onClick="javascript:window.open('Docs/HelpDoc.pdf'); return false;" EnableViewState="False" forecolor="white" title="Help">Help</asp:hyperlink>
			<span style="FONT-WEIGHT: bold; COLOR: white">| </span>
			<asp:hyperlink id="HyperLink5" cssclass="infobarlink" runat="server" navigateurl="~/Logout.aspx" enableviewstate="False" forecolor="white" title="End Session">End Session</asp:hyperlink>&nbsp;
		</td>
	</tr>    
	<tr bgColor="white">
		<td class="logostyle" style="WIDTH: 638px; HEIGHT: 34px" colSpan="2">
			<IMG id="imgTrackLogo" src="header/bptrack_logo.jpg" runat="server" alt="FDA Repository Logo"/>
			<asp:label style="POSITION:relative; TOP:-5px" id="lblTitle" runat="server" font-bold="false" font-size="130%" forecolor="#ffffff" width="266px" backcolor="Transparent" bordercolor="Transparent" borderstyle="None" borderwidth="0px"></asp:label>
		</td>
	</tr>
	<tr>
		<td id="SkipMainSiteLinks" class="gContentSection" colSpan="2">
			<asp:label id="lblLocation" runat="server" font-size="80%" style="LEFT:97px; POSITION:relative"></asp:label>
		</td>
	</tr>
</table>
<br>
