<%@ Register TagPrefix="cc1" Namespace="PPC.FDA.Controls" Assembly="PPC.FDA.Controls.FDAGrid" %>
<%@ Page Language="vb" enableViewStateMac="false" AutoEventWireup="false" Codebehind="AlternateSearchView.aspx.vb" Inherits="PPC.AlternateSearchView" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Home</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" summary="This table is used for page layout.">
				<tr>
					<td vAlign="top">
						<table class="tableFont" id="Table1" cellSpacing="1" cellPadding="1" width="75%" border="0">
							<tr>
								<td colSpan="4">Search Results</td>
							</tr>
							<tr>
								<td colSpan="4"><b>Search Term:</b>&nbsp;<i><asp:label id="SearchText" runat="server"></asp:label></i></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<!-- Merged DataGrid / Labels --->
			<asp:Label id="MergedMatchLabel" runat="server" Visible="False">
				<br>
				<strong><em>Orthographic & Phonetic Matches</em></strong>
			</asp:Label>
			<cc1:FDAGrid id="MergedGrid" AllowPaging="False" AutoGenerateColumns="False" runat="server" visible="False" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" CellSpacing="0" ForeColor="Black" IsSortedAscending="True" AllowSorting="True" CellPadding="2" GridLines="Horizontal" Font-Size="120%" Font-Names="Verdana" Width="35%">
				<AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>
				<ItemStyle BackColor="White"></ItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
				<PagerStyle HorizontalAlign="Right" BackColor="White" Mode="NumericPages"></PagerStyle>
				<Columns>
					<asp:BoundColumn HeaderStyle-Width="45%" DataField="ProductName" SortExpression="ProductName" HeaderText="Name of Concern"></asp:BoundColumn>
					<asp:BoundColumn HeaderStyle-Width="45%" DataField="MergedCost" SortExpression="MergedCost" HeaderText="Match Percentage"></asp:BoundColumn>
				</Columns>
			</cc1:FDAGrid>
			<asp:Label id="MergedNotFound" runat="server" Visible="False" />
			<!-- OrthoGraphic DataGrid / Labels --->
			<asp:Label id="OrthoMatchLabel" runat="server" Visible="False">
				<BR>
				<strong><em>Orthographic Matches</em></strong>
			</asp:Label>
			<cc1:FDAGrid id="OrthoGrid" AllowPaging="False" AutoGenerateColumns="False" runat="server" visible="False" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" CellSpacing="0" ForeColor="Black" IsSortedAscending="True" AllowSorting="True" CellPadding="2" GridLines="Horizontal" Font-Size="120%" Font-Names="Verdana" Width="35%">
				<AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
				<ItemStyle BackColor="#E0E0E0"></ItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
				<PagerStyle HorizontalAlign="Right" BackColor="White" Mode="NumericPages"></PagerStyle>
				<Columns>
					<asp:BoundColumn HeaderStyle-Width="45%" DataField="ProductName" HeaderText="Name of Concern" SortExpression="ProductName"></asp:BoundColumn>
					<asp:BoundColumn HeaderStyle-Width="45%" DataField="Cost" HeaderText="Match Percentage" SortExpression="Cost"></asp:BoundColumn>
				</Columns>
			</cc1:FDAGrid>
			<asp:Label id="OrthoNotFound" runat="server" Visible="False" />
			<!-- OrthoGraphic DataGrid / Labels --->
			<!-- Phonetic DataGrid / Labels --->
			<asp:Label id="PhoneticMatchLabel" runat="server" Visible="False">
				<BR>
				<strong><em>Phonetic Matches</em></strong>
			</asp:Label>
			<cc1:FDAGrid id="PhoneticGrid" AllowPaging="False" GridLines="Horizontal" AutoGenerateColumns="False" runat="server" visible="False" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" CellSpacing="0" ForeColor="Black" IsSortedAscending="True" AllowSorting="True" CellPadding="2" Font-Size="120%" Font-Names="Verdana" Width="35%">
				<AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>
				<ItemStyle BackColor="White"></ItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
				<PagerStyle HorizontalAlign="Right" BackColor="White" Mode="NumericPages" BorderColor="Black" BorderStyle="Solid"></PagerStyle>
				<Columns>
					<asp:BoundColumn HeaderStyle-Width="45%" DataField="ProductName" SortExpression="ProductName" HeaderText="Name of Concern"></asp:BoundColumn>
					<asp:BoundColumn HeaderStyle-Width="45%" DataField="CostPhonetic" SortExpression="CostPhonetic" HeaderText="Match Percentage"></asp:BoundColumn>
				</Columns>
			</cc1:FDAGrid>
			<asp:Label id="PhoneticNotFound" runat="server" Visible="False" />
			<!-- Text Results  OnPageIndexChanged="TextGrid_PageIndexChanged" OnSortCommand="TextGrid_SortCommand" -->
			<asp:Label id="TextMatchLabel" runat="server" Visible="False">
				<BR>
				<strong><em>Text Search</em></strong>
			</asp:Label>
			<cc1:FDAGrid id="TextGrid" AllowPaging="False" GridLines="Horizontal" AutoGenerateColumns="False" runat="server" visible="False" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" CellSpacing="0" ForeColor="Black" IsSortedAscending="True" AllowSorting="True" CellPadding="2" Font-Size="120%" Font-Names="Verdana" Width="35%">
				<AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>
				<ItemStyle BackColor="White"></ItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Left" ForeColor="White" BackColor="Blue"></HeaderStyle>
				<PagerStyle HorizontalAlign="Right" BackColor="White" Mode="NumericPages" BorderColor="Black" BorderStyle="Solid"></PagerStyle>
				<Columns>
					<asp:BoundColumn HeaderStyle-Width="85%" DataField="NAME" SortExpression="NAME" HeaderText="Name of Concern"></asp:BoundColumn>
				</Columns>
			</cc1:FDAGrid>
			<asp:Label id="TextNotFound" runat="server" Visible="False" />
			<BR>
			<!-- Text Results -->
		</form>
	</body>
</HTML>
