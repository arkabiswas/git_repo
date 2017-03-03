<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DataSourceDetail.aspx.vb" Inherits="PPC.DataSourceDetail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Datasource Details for: <%=Request("DSName")%></title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
		    <br/>
		    <asp:Label id="lblDataSourceName" runat="server" Font-Size="Medium" Font-Underline="True" Font-Bold="True" />
		    <br/><br/>
			<asp:Label id="lblDataSourceDetail" runat="server" Font-Size="100%" />			
			<br/><br/>
		</form>
	</body>
</HTML>
