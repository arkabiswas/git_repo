<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ProductDetail.aspx.vb" Inherits="PPC.ProductDetail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
		Product Details for <%=Request("PrdName")%>
		</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
		
			<asp:Label id="ProductName" runat="server" Width="480px" Font-Size="Medium" Font-Underline="True" Font-Bold="True"></asp:Label>
			
			<br /><br />
			
			<asp:DataGrid Runat="server" AutoGenerateColumns="False" ID="grdDrugDetails" Width="100%" GridLines="Horizontal" BorderColor="Black" BorderWidth="1" BorderStyle="Solid" AllowSorting="True" OnSortCommand="SortEventHandler">
				<AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Left" ForeColor="White" BackColor="Blue"></HeaderStyle>
				<columns>
					<asp:BoundColumn DataField="DATA_SOURCE" SortExpression="DATA_SOURCE" HeaderText="Data Source"></asp:BoundColumn>
					<asp:BoundColumn DataField="DOSAGE_FORM" SortExpression="DOSAGE_FORM" HeaderText="Dosage Form"></asp:BoundColumn>
					<asp:BoundColumn DataField="POTENCY" SortExpression="POTENCY" HeaderText="Potency"></asp:BoundColumn>
					<asp:BoundColumn DataField="ROUTE" SortExpression="ROUTE" HeaderText="Route"></asp:BoundColumn>
					<asp:BoundColumn DataField="ACTIVE_INGREDIENT" SortExpression="ACTIVE_INGREDIENT" HeaderText="Active Ingredient"></asp:BoundColumn>
				</Columns>
			</asp:DataGrid>
						
			<asp:Label ID="ErrorLabel" Runat="server" Visible="False" ForeColor="Red">
				There are no product details.
			</asp:Label>
		</form>
	</body>
</HTML>
