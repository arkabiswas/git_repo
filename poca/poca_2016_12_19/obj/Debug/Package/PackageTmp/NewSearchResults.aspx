<%@ Register TagPrefix="mbrsc" Namespace="MetaBuilders.WebControls" Assembly="MetaBuilders.WebControls.RowSelectorColumn" %>
<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="NewSearchResults.aspx.vb" Inherits="PPC.NewSearchResults" %>
<%@ Register TagPrefix="uc1" TagName="SimpleSearchControl" Src="SimpleSearchControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="PPC.FDA.Controls" Assembly="PPC.FDA.Controls.FDAGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Your Search Results</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
			<script language="javascript" src="scripts.js" type="text/javascript"></script>
			<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
			<script language="javascript">
		<!--
			if(document.images)
			{
				var addwatch1 = new Image();
				addwatch1.src = "Images/addtowatchlist.jpg";
				
				var addwatch2 = new Image();
				addwatch2.src = "Images/addtowatchlist2.jpg";
			}
		-->
			</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<uc1:trackingheader id="TrackingHeader1" title="Search Results" runat="server" Location="<a href='Search.aspx' title='New Search'>New Search</a> "></uc1:trackingheader>
			<table cellSpacing="0" cellPadding="0" width="100%" summary="This table is used for page layout.">
				<tr>
					<td vAlign="top">
						<table class="tableFont" id="Table1" cellSpacing="1" cellPadding="1" width="75%" border="0">
							<tr>
								<td style="FONT-WEIGHT: bold; FONT-SIZE: 100%; TEXT-DECORATION: underline" colSpan="4">Search 
									Results</td>
							</tr>
							<tr>
								<td colSpan="4"><b>Search Term:</b>&nbsp;<i>
										<asp:label id="SearchText" runat="server"></asp:label>
									</i>
								</td>
							</tr>
							<tr>
								<td vAlign="center" align="left" colSpan="4" height="40">
									<asp:imagebutton id="btnAddWatch" onmouseover="Swap(this,addwatch2);" onclick="btnAddWatch_Click" onmouseout="Swap(this,addwatch1);" runat="server" ImageUrl="images/addtowatchlist.jpg" AlternateText="Add selected items to watch list." ImageAlign="Left" BorderStyle="None"></asp:imagebutton>
									<asp:label id="ErrorLabel" runat="server" Width="405px" ForeColor="Red"></asp:label>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<!-- Merged DataGrid / Labels ---><asp:label id="MergedMatchLabel" runat="server" Visible="False">
				<br>
				<BR>
				<strong><em>Orthographic & Phonetic Matches</em> </strong>
			</asp:label>
			<cc1:fdagrid id="MergedGrid" runat="server" BorderStyle="Solid" Width="35%" ForeColor="Black" Font-Names="Verdana" Font-Size="120%" GridLines="Horizontal" CellPadding="2" AllowSorting="True" IsSortedAscending="True" CellSpacing="0" BorderWidth="1px" BorderColor="Black" visible="False" AutoGenerateColumns="False">
				<AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>
				<ItemStyle BackColor="White"></ItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Left" ForeColor="White" BackColor="Blue"></HeaderStyle>
				<PagerStyle HorizontalAlign="Right" BackColor="White" Mode="NumericPages"></PagerStyle>
				<Columns>
					<mbrsc:RowSelectorColumn allowselectall="true" SelectionMode="Multiple" />
					<asp:BoundColumn DataField="u_name" SortExpression="u_name" HeaderText="Name of Concern" />
					<asp:BoundColumn HeaderStyle-Width="45%" DataField="MergedCost" SortExpression="MergedCost" HeaderText="Match Percentage"></asp:BoundColumn>
				</Columns>
			</cc1:fdagrid>
			<asp:label id="MergedNotFound" runat="server" Visible="False">
				<br>
			</asp:label>
			<!-- OrthoGraphic DataGrid / Labels ---><asp:label id="OrthoMatchLabel" runat="server" Visible="False">
				<BR>
				<BR>
				<strong><em>Orthographic Matches</em> </strong>
			</asp:label>
			<cc1:fdagrid id="OrthoGrid" runat="server" BorderStyle="Solid" Width="35%" ForeColor="Black" Font-Names="Verdana" Font-Size="120%" GridLines="Horizontal" CellPadding="2" AllowSorting="True" CellSpacing="0" BorderWidth="1px" BorderColor="Black" visible="False" AutoGenerateColumns="False">
				<AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
				<ItemStyle BackColor="#E0E0E0"></ItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Left" ForeColor="White" BackColor="Blue"></HeaderStyle>
				<PagerStyle HorizontalAlign="Right" BackColor="White" Mode="NumericPages"></PagerStyle>
				<Columns>
					<mbrsc:RowSelectorColumn allowselectall="true" SelectionMode="Multiple" />
					<asp:BoundColumn DataField="u_name" SortExpression="u_name" HeaderText="Name of Concern" />
					<asp:BoundColumn HeaderStyle-Width="45%" DataField="Cost" HeaderText="Match Percentage" SortExpression="Cost"></asp:BoundColumn>
				</Columns>
			</cc1:fdagrid>
			<asp:label id="OrthoNotFound" runat="server" Visible="False">
				<br>
			</asp:label>
			<!-- OrthoGraphic DataGrid / Labels --->
			<!-- Phonetic DataGrid / Labels ---><asp:label id="PhoneticMatchLabel" runat="server" Visible="False">
				<BR>
				<BR>
				<strong><em>Phonetic Matches</em> </strong>
			</asp:label>
			<cc1:fdagrid id="PhoneticGrid" runat="server" BorderStyle="Solid" Width="35%" ForeColor="Black" Font-Names="Verdana" Font-Size="120%" GridLines="Horizontal" CellSpacing="0" CellPadding="2" AllowSorting="True" IsSortedAscending="True" BorderWidth="1px" BorderColor="Black" visible="False" AutoGenerateColumns="False" PageSize="7">
				<AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>
				<ItemStyle BackColor="White"></ItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Left" ForeColor="White" BackColor="Blue"></HeaderStyle>
				<Columns>
					<mbrsc:RowSelectorColumn AllowSelectAll="True"></mbrsc:RowSelectorColumn>
					<asp:BoundColumn DataField="u_name" SortExpression="u_name" HeaderText="Name of Concern"></asp:BoundColumn>
					<asp:BoundColumn DataField="CostPhonetic" SortExpression="CostPhonetic" HeaderText="Match Percentage">
						<HeaderStyle Width="45%"></HeaderStyle>
					</asp:BoundColumn>
				</Columns>
				<PagerStyle BorderColor="Black" BorderStyle="Solid" HorizontalAlign="Right" BackColor="White" Mode="NumericPages"></PagerStyle>
			</cc1:fdagrid>
			<asp:label id="PhoneticNotFound" runat="server" Visible="False">
				<br>
			</asp:label>
			<!-- Phonetic DataGrid / Labels --->
			<!-- Text Results  OnPageIndexChanged="TextGrid_PageIndexChanged" OnSortCommand="TextGrid_SortCommand" --><asp:label id="TextMatchLabel" runat="server" Visible="False">
				<BR>
				<strong><em>Text Search</em> </strong>
			</asp:label>
			<cc1:fdagrid id="TextGrid" runat="server" BorderStyle="Solid" Width="35%" ForeColor="Black" Font-Names="Verdana" Font-Size="120%" GridLines="Horizontal" CellPadding="2" AllowSorting="True" IsSortedAscending="True" CellSpacing="0" BorderWidth="1px" BorderColor="Black" visible="False" AutoGenerateColumns="False">
				<AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>
				<ItemStyle BackColor="White"></ItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Left" ForeColor="White" BackColor="Blue"></HeaderStyle>
				<PagerStyle HorizontalAlign="Right" BackColor="White" Mode="NumericPages" BorderColor="Black" BorderStyle="Solid"></PagerStyle>
				<Columns>
					<mbrsc:RowSelectorColumn allowselectall="true" SelectionMode="Multiple" HeaderStyle-Width="7%" />
					<asp:BoundColumn DataField="u_name" SortExpression="u_name" HeaderText="Name of Concern" />
				</Columns>
			</cc1:fdagrid>
			<asp:label id="TextNotFound" runat="server" Visible="False">
				<br>
			</asp:label>
			<!-- Text Results -->
			<table cellSpacing="0" cellPadding="0" width="100%" summary="This table is used for page layout.">
				<tr>
					<td vAlign="top">
						<table class="tableFont" id="Table1" cellSpacing="1" cellPadding="1" width="75%" border="0">
							<tr>
								<td vAlign="center" align="left" colSpan="4" height="40">
									<asp:imagebutton id="AddToWatchList" onmouseover="Swap(this,addwatch2);" onclick="btnAddWatch_Click" onmouseout="Swap(this,addwatch1);" runat="server" ImageUrl="images/addtowatchlist.jpg" AlternateText="Add selected items to watch list." ImageAlign="Left" BorderStyle="None"></asp:imagebutton>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<uc1:simplesearchcontrol id="SimpleSearchControl1" runat="server" />
		</form>
	</body>
</HTML>
