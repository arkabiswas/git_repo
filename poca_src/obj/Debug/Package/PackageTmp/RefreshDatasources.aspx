<%@ Page Language="vb" AutoEventWireup="false" Codebehind="RefreshDatasources.aspx.vb" Inherits="PPC.Refreshdatasources" %>
<%@ Register TagPrefix="poca" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>Refreshdatasources</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="Styles.css" type="text/css" rel="stylesheet" />
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<poca:trackingheader id="TrackingHeader" runat="server" location="<a href='Settings.aspx' title='Settings' style='font-size: 14px; font-weight: bold;'>Return To Settings</a>"
				title="Datasource Maintenance" />
			<p style="PADDING-LEFT: 20px; WIDTH: 400px"></p>
			<div id="RefreshDataSources">
				<div style="left:100px; position:absolute;">
					<span style="float:left; Width:300px; text-align:right; ">Will this be a data load or a data refresh? 
						<!-- Converted to text from label for 508 <label style="MARGIN-LEFT: 10px" for="tester"></label> -->
					</span>
					<span>
						<asp:radiobutton id="rbtnRefresh" runat="server" GroupName="LoadType" Text="Refresh" Checked="True"></asp:radiobutton>
						<asp:radiobutton id="rbtnLoad" runat="server" GroupName="LoadType" Text="Load (If a partial data set is used to load, all other records will be deleted.)"></asp:radiobutton>
					</span>
				</div>
                <br />
                <br />
                <br />
				<div>
					<span style="float:left; Width:300px; text-align:right; ">
						<label for="ddlDatasourcesType">Select the datasource type:&nbsp; </label>
					</span>
					<span style="Width:380px; text-align:left; ">
						<asp:dropdownlist id="ddlDatasourcesType" runat="server"></asp:dropdownlist>
					</span>
				</div>
                <br />
				<div>
					<span style="float:left; Width:300px; text-align:right; ">
						<label for="txtDate">Enter the date of this data:&nbsp; </label>
					</span>
					<span style="Width:380px; text-align:left; ">
						<asp:textbox id="txtDate" runat="server"></asp:textbox>&nbsp;
                        <asp:Label ID="Label2" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;(mm/dd/yyyy)"></asp:Label>
						<%--<asp:requiredfieldvalidator id="rfvDate" runat="server" ControlToValidate="txtDate" Display="Dynamic" ErrorMessage="Please enter a date for this data."></asp:requiredfieldvalidator>
						<asp:regularexpressionvalidator id="revDate" runat="server" ControlToValidate="txtDate" Display="Dynamic" ErrorMessage="Date must be in the following format: mm-dd-yyyy" ValidationExpression="([0-1][0-9](-))([0-3][0-9](-))([1-2][0-9]{3})"></asp:regularexpressionvalidator>--%>
					</span>
				</div>
				<br />
				<br />
				<div>
					<span class="label">&nbsp; </span>
					<span>
						<asp:button id="btnNext" runat="server" Text="Next >>"></asp:button>
                        &nbsp;&nbsp;
                        <asp:Label id="MessageLabel" runat="server" Width="350px" Visible="True" ForeColor="Red"></asp:Label>
					</span>
				</div>
			</div>
		</form>
	</body>
</html>
