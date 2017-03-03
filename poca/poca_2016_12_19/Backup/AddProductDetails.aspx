<%@ Register TagPrefix="cc1" Namespace="PPC.FDA.Controls" Assembly="PPC.FDA.Controls.FDAGrid" %>
<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AddProductDetails.aspx.vb" Inherits="PPC.AddProductDetails" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>AddProductDetails</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<uc1:trackingheader id="TrackingHeader1" title="Add/Edit Product Details" runat="server"></uc1:trackingheader><br>
			<asp:label id="ProductNameLabel" style="Z-INDEX: 101; LEFT: 28px; POSITION: absolute; TOP: 98px" runat="server" Font-Size="Larger" Width="239px"></asp:label><asp:label id="ErrorLabel" style="Z-INDEX: 102; LEFT: 334px; POSITION: absolute; TOP: 99px" runat="server" ForeColor="Red" Width="282px"></asp:label><asp:panel id="PNLDetailsGrid" style="Z-INDEX: 103; LEFT: 27px; POSITION: absolute; TOP: 128px" Width="591px" Runat="server">
				<cc1:FDAGrid id="ProductDetailsGrid" AllowPaging="False" runat="server" ShowHeader="False" CellSpacing="0" AutoGenerateColumns="False" Font-Names="Verdana" Font-Size="120%" GridLines="Horizontal" PageSize="7" CellPadding="2" AllowSorting="True" IsSortedAscending="True" ForeColor="Black" BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" Width="515px">
					<AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>
					<ItemStyle BackColor="White"></ItemStyle>
					<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
					<Columns>
						<asp:TemplateColumn>
							<ItemTemplate>
								<asp:Label runat="server" ID="Label1" NAME="Label1"><%# DataBinder.Eval(Container, "DataItem.element_label") %>:</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn Visible="False">
							<ItemTemplate>
								<asp:Label runat="server" ID="PickListLabel" Visible="true"></asp:Label>
								<asp:Label runat="server" ID="TextValueLabel" Visible="true" />
							</ItemTemplate>
							<EditItemTemplate>
								<asp:DropDownList ID="DDLPickListValue" Runat="server" Visible="False" />
								<asp:TextBox Runat="server" ID="TextValue" Visible="False" />
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" CancelText="Cancel" EditText="Edit">
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:EditCommandColumn>
						<asp:ButtonColumn Text="Delete" CommandName="Delete">
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:ButtonColumn>
						<asp:BoundColumn Visible="False" DataField="element_id" ReadOnly="True"></asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="product_detail_id" ReadOnly="True"></asp:BoundColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" BackColor="Gainsboro" Mode="NumericPages"></PagerStyle>
				</cc1:FDAGrid>
				<asp:Label id="NoProductLabel" runat="server" Visible="False">There are no product details availible. Please add one below.</asp:Label>
			</asp:panel><asp:dropdownlist id="NewProductDetails" style="Z-INDEX: 104; LEFT: 33px; POSITION: absolute; TOP: 375px" runat="server"></asp:dropdownlist><asp:label id="Label2" style="Z-INDEX: 105; LEFT: 33px; POSITION: absolute; TOP: 346px" runat="server" Font-Size="Larger" Width="235px">Add a new product detail</asp:label><asp:button id="AddDetailsButton" style="Z-INDEX: 106; LEFT: 196px; POSITION: absolute; TOP: 374px" runat="server" Text="Add"></asp:button></form>
	</body>
</HTML>
