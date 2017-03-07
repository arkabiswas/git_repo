<%@ Register TagPrefix="poca" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DatasourceDeleteWarning.aspx.vb" Inherits="PPC.DatasourceDeleteWarning" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>DatasourceDeleteWarning</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<style>#datasourceWarning { MARGIN: 65px 100px 0px; WIDTH: 400px }
	#datasourceWarning IMG { FLOAT: left; MARGIN-BOTTOM: 20px; MARGIN-RIGHT: 20px }
	#confirmationButtons { MARGIN-TOP: 10px }
	#confirmationButtons INPUT { WIDTH: 50px; MARGIN-RIGHT: 5px }
		</style>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<poca:trackingheader id="TrackingHeader" runat="server" location="<a href='Settings.aspx' title='Settings'>Return To Settings</a>"
				title="Datasource Maintenance" />
			<div id="datasourceWarning"><IMG height="66" src="Images/exclamation.jpg" alt="Exclamation" width="62">
				You have selected to delete a datasource. By deleting the datasource, all 
				information contained within the datasource will also be deleted. Are you sure 
				that you want to continue?
				<div id="confirmationButtons">
					<asp:Button id="btnYes" Width="60px" runat="server" Text="Yes"></asp:Button>
					<asp:Button id="btnNo" Width="60px" runat="server" Text="No"></asp:Button></div>
			</div>
		</form>
	</body>
</HTML>
