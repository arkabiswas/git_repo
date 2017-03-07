<%@ Register TagPrefix="cc1" Namespace="PPC.FDA.Controls" Assembly="PPC.FDA.Controls.FDAGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ModifyPickList.aspx.vb" Inherits="PPC.ModifyPickList" %>
<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Modify Pick List Values</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<link href="Styles.css" type="text/css" rel="stylesheet">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<uc1:trackingheader id="TrackingHeader1" title="Modify Pick List Values" runat="server" location="<a href='Settings.aspx' style='font-size: 14px; font-weight: bold;' alt='Click Here to return to the settings page.'>Return to Settings</a>"></uc1:trackingheader>
			<cc1:FDAGrid id="PickListGrid" style="Z-INDEX: 101; LEFT: 150px; POSITION: absolute; TOP: 154px" runat="server" ForeColor="Black" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellSpacing="2" IsSortedAscending="True" AllowSorting="True" CellPadding="2" PageSize="7" GridLines="Horizontal" Font-Size="120%" Font-Names="Verdana" AllowPaging="False" AutoGenerateColumns="False">
				<AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>
				<ItemStyle BackColor="White"></ItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
				<Columns>
					<asp:EditCommandColumn ButtonType="PushButton" UpdateText="Update" CancelText="Cancel" EditText="Edit"></asp:EditCommandColumn>
					<asp:TemplateColumn SortExpression="description" HeaderText="Description">
						<ItemTemplate>
							<asp:Label id="DescriptionLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "description") %>' />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:Label ID="lblListItem" runat="server" AssociatedControlID="Description" Text='<%# SetListItem(Eval("LIST_ITEM")) %>' />
							<asp:TextBox id="Description" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "description") %>' />						    
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Sequence" visible="false">
						<ItemTemplate>
							<asp:Label ID="SequenceLabel" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "PICK_LIST_SEQUENCE") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn Visible="false">
						<ItemTemplate>
							<asp:Label id="PickListId" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "pick_list_id") %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn Visible="false">
						<ItemTemplate>
							<asp:Label id="ListItem" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "list_item") %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" BackColor="Gainsboro" Mode="NumericPages"></PagerStyle>
			</cc1:FDAGrid>
			<asp:Label id="ErrorLabel" style="Z-INDEX: 102; LEFT: 150px; POSITION: absolute; TOP: 316px" runat="server" ForeColor="Red" Width="325px"></asp:Label>
			<asp:Label id="Label1" style="Z-INDEX: 103; LEFT: 150px; POSITION: absolute; TOP: 120px" runat="server" Width="220px">Modify the Level of Concern Picklist</asp:Label></form>
	</body>
</HTML>
