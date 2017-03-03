<%@ Page Language="vb" AutoEventWireup="false" Codebehind="RefreshDatasources.aspx.vb" Inherits="PPC.Refreshdatasources" %>
<%@ Register TagPrefix="poca" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Refreshdatasources</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<style type="text/css">DIV.row { CLEAR: both; PADDING-TOP: 5px }
	DIV.subrow { CLEAR: both; PADDING-LEFT: 5em; PADDING-TOP: 5px }
	SPAN.label { FLOAT: left; WIDTH: 250px; TEXT-ALIGN: right }
	SPAN.formr { FLOAT: right; WIDTH: 335px; TEXT-ALIGN: left }
		</style>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<poca:trackingheader id="TrackingHeader" runat="server" location="<a href='Settings.aspx' title='Settings'>Return To Settings</a>"
				title="Datasource Maintenance" />
			<p style="PADDING-LEFT: 20px; WIDTH: 400px"></p>
			<div id="RefreshDataSources">
				<div class="row">
					<span class="label">Will this be a data load or a data refresh? 
						<!-- Converted to text from label for 508 <label style="MARGIN-LEFT: 10px" for="tester"></label> -->
					</span>
					<span class="formw">
						<asp:radiobutton id="rbtnRefresh" runat="server" GroupName="LoadType" Text="Refresh" Checked="True"></asp:radiobutton>
						<asp:radiobutton id="rbtnLoad" runat="server" GroupName="LoadType" Text="Load (If a partial data set is used to load, all other records will be deleted.)"></asp:radiobutton>
					</span>
				</div>
				<div class="row">
					<span class="label">
						<label style="MARGIN-LEFT: 10px" for="ddlDatasourcesType">Select the datasource type: </label>
					</span>
					<span class="formw">
						<asp:dropdownlist id="ddlDatasourcesType" runat="server"></asp:dropdownlist>
					</span>
				</div>
				<div class="row">
					<span class="label">
						<label style="MARGIN-LEFT: 10px" for="txtDate">Enter the date of this data: </label>
					</span>
					<span class="formw">
						<asp:textbox id="txtDate" runat="server"></asp:textbox>&nbsp;
						<asp:requiredfieldvalidator id="rfvDate" runat="server" ControlToValidate="txtDate" Display="Dynamic" ErrorMessage="Please enter a date for this data."></asp:requiredfieldvalidator>
						<asp:regularexpressionvalidator id="revDate" runat="server" ControlToValidate="txtDate" Display="Dynamic" ErrorMessage="Date must be in the following format: mm-dd-yyyy" ValidationExpression="([0-1][0-9](-))([0-3][0-9](-))([1-2][0-9]{3})"></asp:regularexpressionvalidator>
					</span>
				</div>
				<div class="row">
					<span class="label">&nbsp; </span>
					<span class="formw">
						<asp:button id="btnNext" runat="server" Text="Next >>"></asp:button>
					</span>
				</div>
			</div>
		</form>
	</body>
</HTML>
