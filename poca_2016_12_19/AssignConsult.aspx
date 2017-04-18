<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AssignConsult.aspx.vb" Inherits="PPC.AssignConsult" %>
<%@ Register TagPrefix="cc1" Namespace="PPC.FDA.Controls" Assembly="PPC.FDA.Controls.FDAGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Assign a Consult</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<uc1:TrackingHeader id="TrackingHeader1" runat="server" title="Assign a consult" location="<a href='Settings.aspx' alt='Click here to return to the Settings page'>Return to Settings</a>" />
			<cc1:FDAGrid id="FDAGrid1" style="Z-INDEX: 101; LEFT: 33px; POSITION: absolute; TOP: 132px" AllowPaging="False" runat="server" Width="349px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellSpacing="0" ForeColor="Black" IsSortedAscending="True" AllowSorting="False" CellPadding="2" PageSize="7" GridLines="Horizontal" Font-Size="120%" Font-Names="Verdana" AutoGenerateColumns="False">
				<AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>
				<ItemStyle BackColor="White"></ItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Left" ForeColor="White" BackColor="Blue"></HeaderStyle>
				<Columns>
					<asp:templatecolumn headertext="Consult Name" sortexpression="product_name">
						<itemtemplate>
							<asp:hyperlink runat="server" text='<%# DataBinder.Eval(Container, "DataItem.product_name") %>' navigateurl='<%# "AssignConsultPerson.aspx?clt=" + DataBinder.Eval(Container, "DataItem.product_name") + "&cid=" + Databinder.Eval(Container, "DataItem.consult_number") %>' id="AssignConsultLink"></asp:hyperlink>
						</itemtemplate>
					</asp:templatecolumn>					
					<asp:BoundColumn DataField="CONSULT_NUMBER" HeaderText="Consult Number"></asp:BoundColumn>
					<asp:BoundColumn DataField="ASSIGNED_TO" HeaderText="Assigned To" />
				</Columns>
				<PagerStyle HorizontalAlign="Right" BackColor="Gainsboro" Mode="NumericPages"></PagerStyle>
			</cc1:FDAGrid>
			<asp:Label id="Label1" style="Z-INDEX: 102; LEFT: 35px; POSITION: absolute; TOP: 99px" runat="server" Width="375px"> Select the Consult you would like to assign.</asp:Label>
		</form>
	</body>
</HTML>
