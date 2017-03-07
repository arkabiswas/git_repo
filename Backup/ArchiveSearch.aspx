<%@ Register TagPrefix="cc1" Namespace="PPC.FDA.Controls" Assembly="PPC.FDA.Controls.FDAGrid" %>
<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ArchiveSearch.aspx.vb" Inherits="PPC.ArchiveSearch" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ArchiveSearch</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<uc1:trackingheader id="TrackingHeader1" runat="server"></uc1:trackingheader>
			<asp:label id="ArchiveSearchDescrpt" style="Z-INDEX: 101; LEFT: 72px; POSITION: absolute; TOP: 114px" runat="server" Width="347px">Enter your search critieria to find an archived search.</asp:label>
			<asp:dropdownlist id="UsernameList" style="Z-INDEX: 102; LEFT: 164px; POSITION: absolute; TOP: 146px" runat="server" />
			<div style="margin-left: 40px">
                <asp:label id="Label1" 
                    style="Z-INDEX: 103; LEFT: 73px; POSITION: absolute; TOP: 146px" runat="server" 
                    AssociatedControlID="UsernameList">Username:</asp:label>
            </div>
			<asp:label id="Label2" 
                style="Z-INDEX: 104; LEFT: 73px; POSITION: absolute; TOP: 173px" runat="server" 
                Width="93px" AssociatedControlID="StartDate">Start Date:</asp:label>
			<asp:textbox id="StartDate" style="Z-INDEX: 105; LEFT: 164px; POSITION: absolute; TOP: 170px" runat="server"></asp:textbox>
			<asp:label id="Label3" 
                style="Z-INDEX: 106; LEFT: 77px; POSITION: absolute; TOP: 199px" runat="server" 
                Width="107px" AssociatedControlID="EndDate">End Date:</asp:label>
			<asp:textbox id="EndDate" style="Z-INDEX: 107; LEFT: 164px; POSITION: absolute; TOP: 195px" runat="server"></asp:textbox>
			<asp:label id="Label4" 
                style="Z-INDEX: 108; LEFT: 43px; POSITION: absolute; TOP: 224px" runat="server" 
                Width="108px" AssociatedControlID="SearchModuleList">Search Module:</asp:label>
			<asp:dropdownlist id="SearchModuleList" style="Z-INDEX: 109; LEFT: 165px; POSITION: absolute; TOP: 221px" runat="server"></asp:dropdownlist>
			<asp:label id="Label5" 
                style="Z-INDEX: 110; LEFT: 53px; POSITION: absolute; TOP: 247px" runat="server" 
                Width="93px" AssociatedControlID="SearchTerm">Search Term:</asp:label>
			<asp:textbox id="SearchTerm" style="Z-INDEX: 111; LEFT: 165px; POSITION: absolute; TOP: 246px" runat="server"></asp:textbox>
			<asp:button id="SearchSubmit" style="Z-INDEX: 112; LEFT: 262px; POSITION: absolute; TOP: 273px" runat="server" Text="Search"></asp:button>
			<cc1:fdagrid id="ResultsGrid" AllowPaging="False" style="Z-INDEX: 113; LEFT: 38px; POSITION: absolute; TOP: 336px" runat="server" Width="384px" Font-Size="120%" ForeColor="Black" Visible="False" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellSpacing="0" IsSortedAscending="True" AllowSorting="True" CellPadding="2" PageSize="7" GridLines="Horizontal" Font-Names="Verdana" AutoGenerateColumns="False">
				<AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>
				<ItemStyle BackColor="White"></ItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
				<Columns>
					<asp:BoundColumn DataField="search_term" HeaderText="Search Term"></asp:BoundColumn>
					<asp:BoundColumn DataField="username" HeaderText="Username"></asp:BoundColumn>
					<asp:BoundColumn DataField="search_modules" HeaderText="Search Modules"></asp:BoundColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" BackColor="Gainsboro" Mode="NumericPages"></PagerStyle>
			</cc1:fdagrid><asp:label id="ResultsLabel" style="Z-INDEX: 114; LEFT: 38px; POSITION: absolute; TOP: 313px" runat="server" Width="376px" Font-Size="Larger" Visible="False">Search Results:</asp:label><asp:label id="ErrorLabel" style="Z-INDEX: 115; LEFT: 38px; POSITION: absolute; TOP: 323px" runat="server" Width="379px" ForeColor="Red" Visible="False"></asp:label><asp:regularexpressionvalidator id="RegularExpressionValidator1" style="Z-INDEX: 116; LEFT: 409px; POSITION: absolute; TOP: 174px" runat="server" ErrorMessage="This date must match the following format: mm-dd-yyyy" ValidationExpression="([0-1][0-9](-))([0-3][0-9](-))([1-2][0-9]{3})" ControlToValidate="StartDate"></asp:regularexpressionvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator2" style="Z-INDEX: 117; LEFT: 409px; POSITION: absolute; TOP: 200px" runat="server" ErrorMessage="This date must match the following format: mm-dd-yyyy" ValidationExpression="([0-1][0-9](-))([0-3][0-9](-))([1-2][0-9]{3})" ControlToValidate="EndDate"></asp:regularexpressionvalidator></form>
	</body>
</HTML>
