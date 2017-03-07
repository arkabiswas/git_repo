<%@ Register TagPrefix="POCA" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ErrorPage.aspx.vb" Inherits="PPC.ErrorPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Error Page</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="tracking.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="scripts.js" type="text/javascript"></script>
		<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<POCA:TRACKINGHEADER id="TrackingHeader1" runat="server" />
			<asp:Label id="ErrorLabel" style="Z-INDEX: 101; LEFT: 75px; POSITION: absolute; TOP: 144px" runat="server" CssClass="ErrorPage">An error has occurred. A log of this error has been saved. Please contact your system administrator with the details that resulted in this error.</asp:Label></form>
	</body>
</HTML>
