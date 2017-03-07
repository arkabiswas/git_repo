<%@ Page Language="vb" AutoEventWireup="false" Codebehind="News.aspx.vb" Inherits="PPC.News" buffer="True" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>News Item</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:Label id="Header" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 36px" runat="server" Width="207px" Font-Size="100%" Font-Bold="True"></asp:Label>
			<asp:Label id="Teaser" style="Z-INDEX: 102; LEFT: 8px; POSITION: absolute; TOP: 71px" runat="server" Width="211px" Font-Size="100%" Font-Italic="True"></asp:Label>
			<asp:Label id="NewsItem" style="Z-INDEX: 103; LEFT: 8px; POSITION: absolute; TOP: 107px" 