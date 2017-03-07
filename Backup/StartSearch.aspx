<%@ Register TagPrefix="poca" TagName="SearchBox" Src="SearchBox.ascx" %>
<%@ Register TagPrefix="poca" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="StartSearch.aspx.vb" Inherits="PPC.StartSearch" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Search Database</title>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="Styles.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="scripts.js"></script>
		<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<poca:TrackingHeader id="SearchStartHeader" runat="server" location="<a href='ArchiveSearch.aspx' title='Retrieve Archived Search'>Retrieve Archived Search</a>"
				title="Search Database" />
			<poca:SearchBox id="StartSearchBox" runat="server" />
			<br>
			<%--<asp:linkbutton id="ToggleSearchType" runat="server" causesvalidation="False">Switch to advanced search view</asp:linkbutton>--%>
		</form>
	</body>
</HTML>
