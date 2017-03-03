<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="PPC.FDA.Controls" Assembly="PPC.FDA.Controls.FDAGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CompletedWatchlist.aspx.vb" Inherits="PPC.CompletedWatchlist" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Completed Watchlist</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK id="cssPage" href="Styles.css" type="text/css" rel="stylesheet" runat="server"/>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<uc1:trackingheader id="TrackingHeader1" runat="server"></uc1:trackingheader>

				<br>
				<div id="printDownloadSec" runat="server" visible="true">
					<br>
					<asp:LinkButton ID="PrintScreen" Runat="server">Print</asp:LinkButton>
					<asp:Label ID="SeperatorPipe" Runat="server"> | </asp:Label>
					<asp:LinkButton ID="DownloadScreen" Runat="server">Download</asp:LinkButton>
					<br>
				</div>
				<asp:label id="lblConsultDetails" style="align:left" Text="Consult Details" Font-Bold="True"
					runat="server"></asp:label><br>
				<table cellspacing="0" cellpadding="2">
					<tr>
						<td align="right"><asp:label id="Label1" runat="server">Proprietary Name</asp:label>&nbsp;</td>
						<td><asp:label id="lblProductName" tabIndex="1" runat="server"></asp:label></td>
					</tr>
					<tr>
						<td align="right"><asp:Label id="Label4" runat="server" Width="130px">Alternative Name</asp:Label>&nbsp;</td>
						<td><asp:label id="lblAlternativeName" runat="server" tabIndex="1" /></td>
					</tr>
					<tr>
						<td align="right"><asp:Label id="Label6" runat="server" Width="115px">Established Name</asp:Label>&nbsp;</td>
						<td><asp:label id="lblEstablishedName" runat="server" tabIndex="2" /></td>
					</tr>
					<tr>
						<td align="right"><asp:label id="Label2" runat="server">Product Modifier</asp:label>&nbsp;</td>
						<td><asp:label id="lblProductModifier" tabIndex="3" runat="server"></asp:label></td>
					</tr>
					<tr>
						<td align="right"><asp:label id="Label5" runat="server">Date Received</asp:label>&nbsp;</td>
						<td><asp:label id="lblDTReceived" tabIndex="4" runat="server"></asp:label></td>
					</tr>
					<tr>
						<td align="right"><asp:label id="EPDDateLabel" runat="server">EPD Date</asp:label>&nbsp;</td>
						<td><asp:label id="lblEPDDate" runat="server" tabIndex="5" /></td>
					</tr>
					<tr>
						<td align="right"><asp:label id="Label7" runat="server">Consult Number</asp:label>&nbsp;</td>
						<td><asp:label id="lblConsultNumber" tabIndex="6" runat="server"></asp:label></td>
					</tr>
					<tr>
						<td align="right"><asp:label id="Label8" runat="server">Application Number</asp:label>&nbsp;</td>
						<td><asp:label id="lblAppNumber" tabIndex="7" runat="server"></asp:label></td>
					</tr>
					<tr>
						<td align="right"><asp:label id="Label9" runat="server">Application Type</asp:label>&nbsp;</td>
						<td><asp:label id="lblAppType" tabIndex="8" runat="server"></asp:label></td>
					</tr>
					<tr>
						<td align="right"><asp:Label id="Label10" runat="server" Width="98px">Comments</asp:Label>&nbsp;</td>
						<td><asp:label id="lblComments" runat="server" tabIndex="9"></asp:label></td>
					</tr>
				</table>

			<br>
			<br>
				<asp:label id="WatchListTitle" Font-Bold="True" runat="server"></asp:label>
				<asp:dropdownlist id="DDLWatchLists" AutoPostBack="True" Runat="server"></asp:dropdownlist><br>
				<asp:label id="ErrorLabel" runat="server" Visible="False" /><br>
				<cc1:fdagrid id="FDAGrid1" runat="server" Font-Names="Verdana" Font-Size="120%" GridLines="Horizontal"
					AutoGenerateColumns="False" CellPadding="2" AllowSorting="True" IsSortedAscending="False"
					ForeColor="Black" BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" PageSize="7"
					AllowPaging="False">
					<PagerStyle HorizontalAlign="Right" BackColor="Gainsboro" Mode="NumericPages"></PagerStyle>
					<AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>
					<ItemStyle BackColor="White"></ItemStyle>
					<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
					<Columns>
						<asp:TemplateColumn HeaderText="Name of Concern">	
							<ItemTemplate>
								<a href="#" onclick="window.open('<%# DataBinder.Eval(Container, "DataItem.name_of_concern", "ProductDetail.aspx?PrdName={0}") %>', 'ProductDetails', 'toolbars=no,status=no,height=400,width=500')"><%# DataBinder.Eval(Container, "DataItem.name_of_concern") %></a>											
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="name_of_concern" HeaderText="Name of Concern" Visible="False"></asp:BoundColumn>
						<asp:BoundColumn DataField="type_of_concern" HeaderText="Type of Concern"></asp:BoundColumn>
						<asp:BoundColumn DataField="level_of_concern" HeaderText="Level of Concern"></asp:BoundColumn>
						<asp:templatecolumn HeaderText="Datasource">
							<itemtemplate>
								<%# PPC.FDA.Data.OracleDataFactory.GetDataSourceName(DataBinder.Eval(Container, "DataItem.data_source")) %>
								<input type=hidden runat=server id="ProductNameId" value='<%# DataBinder.Eval(Container, "DataItem.product_name_id") %>' NAME="ProductNameId"/>
							</itemtemplate>
						</asp:templatecolumn>
						<asp:BoundColumn DataField="narrative" HeaderText="Narrative"></asp:BoundColumn>
					</Columns>
				</cc1:fdagrid>
			<br>
			<br>
			<asp:label id="WatchListHistory" Font-Bold="True" runat="server">WatchList History</asp:label>
				<asp:label id="HistoryErrorLabel" runat="server" ForeColor="Red"></asp:label>
				<br>
				<br>
				<cc1:fdagrid id="FDAGrid2" runat="server" AllowPaging="False" Font-Names="Verdana" Font-Size="120%"
					GridLines="Horizontal" AutoGenerateColumns="False" CellPadding="2" AllowSorting="True" IsSortedAscending="True"
					ForeColor="Black" BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" cellspacing="0">
					<PagerStyle HorizontalAlign="Right" BackColor="Gainsboro" Mode="NumericPages"></PagerStyle>
					<AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>
					<ItemStyle BackColor="White"></ItemStyle>
					<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="action" HeaderText="Action"></asp:BoundColumn>
						<asp:BoundColumn DataField="action_desc" HeaderText="Description"></asp:BoundColumn>
						<asp:BoundColumn DataField="action_date" HeaderText="Date" DataFormatString="{0:MM-dd-yyyy}"></asp:BoundColumn>
						<asp:BoundColumn DataField="action_by" HeaderText="Action By"></asp:BoundColumn>
						<asp:BoundColumn DataField="narrative" HeaderText="Narrative"></asp:BoundColumn>
					</Columns>
				</cc1:fdagrid>
			<br>
		</form>
	</body>
</HTML>
