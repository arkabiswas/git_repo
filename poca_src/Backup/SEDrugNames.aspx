<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SEDrugNames.aspx.vb" Inherits="PPC.SEDrugNames" %>
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
			<P><asp:label id="Label1" runat="server" Width="381px">Select the Date Range you would like to view from the list below.</asp:label>
                <br />
                <asp:DropDownList ID="ddlDateRange" runat="server" AutoPostBack="True" 
                    Width="174px">
                    <asp:ListItem Selected="True" Value="1">Show Last Week</asp:ListItem>
                    <asp:ListItem Value="2">Show Last Two Weeks</asp:ListItem>
                    <asp:ListItem Value="3">Show Last Month</asp:ListItem>
                    <asp:ListItem Value="4">Show Last Year</asp:ListItem>
                    <asp:ListItem Value="5">Show All</asp:ListItem>
                </asp:DropDownList>
            </P>
			<P><cc1:fdagrid id="FDAGrid1" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
					CellSpacing="2" ForeColor="Black" IsSortedAscending="False" AllowSorting="True" CellPadding="2"
					GridLines="Horizontal" Font-Size="120%" Font-Names="Verdana" AllowPaging="False" AutoGenerateColumns="False">
					<PagerStyle HorizontalAlign="Right" BackColor="Gainsboro" Mode="NumericPages"></PagerStyle>
					<AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>
					<ItemStyle BackColor="White"></ItemStyle>
					<HeaderStyle Font-Bold="True" HorizontalAlign="Left" ForeColor="White" BackColor="Blue"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="U_NAME" SortExpression="U_NAME" HeaderText="Drug Name"></asp:BoundColumn>						
						<asp:BoundColumn DataField="DT_CREATED" SortExpression="DT_CREATED" HeaderText="Date Entered"></asp:BoundColumn>
						<asp:BoundColumn DataField="USER_NAME" SortExpression="USER_NAME" HeaderText="Entered By"></asp:BoundColumn>
					</Columns>
				</cc1:fdagrid></P>
		</form>
	</body>
</HTML>

