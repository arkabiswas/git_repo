<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EditConsultSearch.aspx.vb" Inherits="PPC.EditConsultSearch" %>
<%@ Register TagPrefix="cc1" Namespace="PPC.FDA.Controls" Assembly="PPC.FDA.Controls.FDAGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>EditConsultSearch</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<uc1:TrackingHeader id="TrackingHeader1" runat="server"></uc1:TrackingHeader>
			<cc1:FDAGrid id="FDAGrid1" style="Z-INDEX: 101; LEFT: 33px; POSITION: absolute; TOP: 116px" runat="server" Width="349px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellSpacing="2" ForeColor="Black" IsSortedAscending="True" AllowSorting="False" CellPadding="2" PageSize="7" GridLines="Horizontal" Font-Size="120%" Font-Names="Verdana" AllowPaging="False" AutoGenerateColumns="False">
				<AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>
				<ItemStyle BackColor="White"></ItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Left" ForeColor="White" BackColor="Blue"></HeaderStyle>
				<Columns>
					<asp:BoundColumn DataField="PRODUCT_NAME" SortExpression="PRODUCT NAME" HeaderText="Consult Name"></asp:BoundColumn>
					<asp:BoundColumn DataField="CONSULT_NUMBER" HeaderText="Consult Number"></asp:BoundColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" BackColor="Gainsboro" Mode="NumericPages"></PagerStyle>
			</cc1:FDAGrid>
			<asp:Label id="Label1" style="Z-INDEX: 102; LEFT: 39px; POSITION: absolute; TOP: 82px" runat="server" Width="375px">Select the Consult you would like to edit from the list below.</asp:Label>
		</form>
	</body>
</HTML>
