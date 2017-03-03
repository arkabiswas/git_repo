<%@ Register TagPrefix="cc1" Namespace="PPC.FDA.Controls" Assembly="PPC.FDA.Controls.FDAGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AddToWL.aspx.vb" Inherits="PPC.AddToWL" %>
<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Add to Watchlist</title>
		<script language="javascript" src="scripts.js" type="text/javascript"></script>
		<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body ms_positioning="GridLayout">
		<form id="Form1" method="post" runat="server">
			<uc1:trackingheader id="Trackingheader1" title="Add to WatchList" runat="server" Location="<a href='javascript:window.close();'>Close this window</a>"></uc1:trackingheader><br>
			<asp:label id="lblCounter" style="Z-INDEX: 101; LEFT: 96px; POSITION: absolute; TOP: 91px" runat="server"></asp:label><asp:dropdownlist id="WLDropDown" style="Z-INDEX: 103; LEFT: 212px; POSITION: absolute; TOP: 158px" runat="server"></asp:dropdownlist><asp:label id="Label1" style="Z-INDEX: 104; LEFT: 96px; POSITION: absolute; TOP: 118px" runat="server" Width="270px">Select the Watchlist you would like to add the selected consults to:</asp:label><asp:panel id="PNLWatchList" style="Z-INDEX: 105; LEFT: 96px; POSITION: absolute; TOP: 198px" runat="server" Height="190px" Width="412px" Visible="False">
				<cc1:FDAGRID AllowPaging="False" id="DataGrid1" style="LEFT: 0px; TOP: 1px" runat="server" Width="480px" AutoGenerateColumns="False" visible="False" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" CellSpacing="0" ForeColor="Black" IsSortedAscending="True" CellPadding="2" GridLines="Horizontal" Font-Size="120%" Font-Names="Verdana" ShowFooter="True">
					<ALTERNATINGITEMSTYLE BackColor="#E0E0E0" VerticalAlign="top"></ALTERNATINGITEMSTYLE>
					<ITEMSTYLE BackColor="White" VerticalAlign="top"></ITEMSTYLE>
					<HEADERSTYLE Font-Bold="True" ForeColor="White" BackColor="Blue" HorizontalAlign="Left" VerticalAlign="Top"></HEADERSTYLE>
					<FooterStyle backcolor="blue" forecolor="white"></FooterStyle>
					<PAGERSTYLE BackColor="White" HorizontalAlign="Right" Mode="NumericPages"></PAGERSTYLE>
					<COLUMNS>
						<ASP:BOUNDCOLUMN HeaderText="Name of Concern" DataField="name_of_concern" HeaderStyle-Width="30%"></ASP:BOUNDCOLUMN>
						<asp:templatecolumn HeaderText="Concern Type">
							<ItemTemplate>
								<asp:DropDownList ID="DDLTypeConcern" Runat="server" DataTextField="Description" DataValueField="List_Item"></asp:DropDownList>
							</ItemTemplate>
						</asp:templatecolumn>
						<asp:templatecolumn HeaderText="Level of Concern">
							<ItemTemplate>
								<asp:DropDownList ID="DDLLevelConcern" Runat="server" DataTextField="Description" DataValueField="List_Item"></asp:DropDownList>
							</ItemTemplate>
						</asp:templatecolumn>
						<asp:templatecolumn HeaderText="Narrative">
							<ItemTemplate>
								<asp:TextBox TextMode="MultiLine" ID="TANarrative" Runat="server" Columns="20" Rows="6" />
							</ItemTemplate>
							<footertemplate>
								<asp:Button id="BTNAddtowl" CommandName="Add" runat="server" Text="Add"></asp:Button>
							</footertemplate>
						</asp:templatecolumn>
					</COLUMNS>
				</cc1:FDAGRID>
				<asp:Label id="ErrorLabel" runat="server" Width="348px" Visible="False"></asp:Label>
			</asp:panel>
		</form>
	</body>
</HTML>
