<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CompletedConsults.aspx.vb" Inherits="PPC.CompletedConsults" %>
<%@ Register TagPrefix="cc1" Namespace="PPC.FDA.Controls" Assembly="PPC.FDA.Controls.FDAGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>CompletedConsults</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<uc1:trackingheader id="TrackingHeader1" runat="server" location="<a href='Home.aspx' title='Home'>Return To Home Page</a>"></uc1:trackingheader>
			<P><asp:label id="Label1" runat="server" Width="496px">Select the Completed Consult you would like to view from the list below.</asp:label></P>
			<P><cc1:fdagrid id="FDAGrid1" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
					CellSpacing="2" ForeColor="Black" IsSortedAscending="False" AllowSorting="True" CellPadding="2"
					GridLines="Horizontal" Font-Size="120%" Font-Names="Verdana" AllowPaging="False" AutoGenerateColumns="False">
					<PagerStyle HorizontalAlign="Right" BackColor="Gainsboro" Mode="NumericPages"></PagerStyle>
					<AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>
					<ItemStyle BackColor="White"></ItemStyle>
					<HeaderStyle Font-Bold="True" HorizontalAlign="Left" ForeColor="White" BackColor="Blue"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="PRODUCT_NAME" SortExpression="PRODUCT_NAME" HeaderText="Consult Name"></asp:BoundColumn>						
						<asp:BoundColumn DataField="CONSULT_NUMBER" SortExpression="CONSULT_NUMBER" HeaderText="Consult Number"></asp:BoundColumn>
						<asp:BoundColumn DataField="DT_EPD" SortExpression="DT_EPD" HeaderText="EPD Date"></asp:BoundColumn>
						<asp:BoundColumn DataField="ASSIGNED_TO" SortExpression="ASSIGNED_TO" HeaderText="Reviewer"></asp:BoundColumn>
					</Columns>
				</cc1:fdagrid></P>
		</form>
	</body>
</HTML>

					<!--<asp:BoundColumn DataField="PRODUCT_NAME" SortExpression="" HeaderText="Action"></asp:BoundColumn>-->