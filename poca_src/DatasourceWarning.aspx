<%@ Register TagPrefix="poca" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DatasourceWarning.aspx.vb" Inherits="PPC.DatasourceWarning" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>DatasourceWarning</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<style>#datasourceWarning { MARGIN: 65px 100px 0px; WIDTH: 400px }
	#datasourceWarning IMG { PADDING-RIGHT: 0px; FLOAT: left; MARGIN-RIGHT: 20px }
	#confirmationButtons { MARGIN-TOP: 10px }
	#confirmationButtons INPUT { WIDTH: 50px; MARGIN-RIGHT: 5px }
		</style>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<poca:trackingheader id="TrackingHeader1" runat="server" location="<a href='Settings.aspx' title='Settings'>Return To Settings</a>"
				title="Datasource Maintenance" />
			<div id="datasourceWarning"><IMG height="66" src="Images/exclamation.jpg" alt="Exclamation" width="62">
				<asp:label id="lblDateLoaded" runat="server">mm/dd/yyyy</asp:label>
				Do you want to continue refreshing/loading this datasource?
				<div id="confirmationButtons">
					<asp:Button id="btnYes" Width="60px" runat="server" Text="Yes"></asp:Button>
					<asp:Button id="btnNo" Width="60px" runat="server" Text="No"></asp:Button></div>
			</div>
		</form>
	</body>
</HTML>
