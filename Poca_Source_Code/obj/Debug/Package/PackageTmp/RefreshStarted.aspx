<%@ Register TagPrefix="poca" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="RefreshStarted.aspx.vb" Inherits="PPC.RefreshStarted" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>RefreshStarted</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<style>#SearchMessage { BORDER-RIGHT: black 1px dashed; PADDING-RIGHT: 20px; BORDER-TOP: black 1px dashed; PADDING-LEFT: 20px; PADDING-BOTTOM: 20px; MARGIN: 0px 100px 50px; BORDER-LEFT: black 1px dashed; COLOR: black; PADDING-TOP: 20px; BORDER-BOTTOM: black 1px dashed; BACKGROUND-COLOR: #eee }
	#SearchConducted { PADDING-RIGHT: 0px; PADDING-LEFT: 0px; FONT-SIZE: 15px; PADDING-BOTTOM: 0px; MARGIN: 85px 100px 0px; PADDING-TOP: 0px }
		</style>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<poca:trackingheader id="TrackingHeader1" runat="server" location="<a href='Settings.aspx' title='Settings'>Return To Settings</a>"
				title="Datasource Maintenance" />
			<h1 id="SearchConducted">Refreshing/Loading "
				<asp:Label id="lblDatasource" runat="server">Datasource</asp:Label>
				" dated
				<asp:Label id="lblDateOfSource" runat="server">mm/dd/yyyy</asp:Label>.</h1>
			<div id="SearchMessage">
				<p>The refresh process may take some time to complete. An email will be sent to the 
					team when the process has been completed. You may still use the system while 
					this refresh/load is taking place in the background.
				</p>
			</div>
		</form>
	</body>
</HTML>
