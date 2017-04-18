<%@ Register TagPrefix="cc1" Namespace="PPC.FDA.Controls" Assembly="PPC.FDA.Controls.FDAGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ConsolidateWatchList.aspx.vb" Inherits="PPC.ConsolidateWatchList" %>
<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<head>
		<title>Consolidate A WatchList</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name=GENERATOR>
		<meta content="Visual Basic 7.0" name=CODE_LANGUAGE>
		<meta content=JavaScript name=vs_defaultClientScript>
		<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema><LINK href="Styles.css" type=text/css rel=stylesheet >
	</head>
	<body>
		<form id=Form1 method=post runat="server">
			<uc1:trackingheader id=TrackingHeader1 title="Consolidate Watchlists" runat="server" location="<a href='Settings.aspx' alt='Click here to return to the settings page'>Settings</a>"></uc1:TrackingHeader>
			<p><asp:label id=ErrorLabel runat="server" ForeColor="Red"></asp:Label></p>
			<p><asp:label id=Label1 runat="server"> Select the WatchList you want to Consolidate.</asp:Label></p>
			<p>
				<cc1:fdagrid id=ConsultGrid runat="server" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False" Font-Names="Verdana" Font-Size="120%" GridLines="Horizontal" PageSize="7" CellPadding="2" IsSortedAscending="True" ForeColor="Black" BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" Width="349px">
					<AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>
					<ItemStyle BackColor="White"></ItemStyle>
					<HeaderStyle Font-Bold="True" HorizontalAlign="Left" ForeColor="White" BackColor="Blue"></HeaderStyle>
					<Columns>
						<asp:templatecolumn HeaderText="Consult Name" sortexpression="product_name">
							<ItemTemplate>
								<asp:hyperlink runat="server" text='<%# DataBinder.Eval(Container, "DataItem.product_name") %>' navigateurl='<%# "ConsolidateWatchlist.aspx?clt=" + Server.UrlEncode(DataBinder.Eval(Container, "DataItem.product_name")) + "&cid=" + Server.UrlEncode(Databinder.Eval(Container, "DataItem.consult_number")) %>'></asp:hyperlink>
							</itemtemplate>
						</asp:templatecolumn>
						<asp:BoundColumn DataField="CONSULT_NUMBER" HeaderText="Consult Number"></asp:BoundColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" BackColor="Gainsboro" Mode="NumericPages"></PagerStyle>
				</cc1:FDAGrid>
			</p>
		</FORM>
	</body>
</HTML>
