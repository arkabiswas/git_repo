<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="NewsActionPage.aspx.vb" Inherits="PPC.NewsActionPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<TITLE>Add/Edit News Items</TITLE>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script src="scripts.js" type="text/javascript" language="javascript"></script>
		<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<uc1:TrackingHeader id="TrackingHeader1" runat="server" Title="Add/Edit News Items"></uc1:TrackingHeader>
			<asp:TextBox id="TxtHeadline" style="Z-INDEX: 101; LEFT: 256px; POSITION: absolute; TOP: 187px" runat="server" Width="298px"></asp:TextBox>
			<asp:Label id="Label1" AssociatedControlID="TxtHeadline" style="Z-INDEX: 102; LEFT: 97px; POSITION: absolute; TOP: 187px" runat="server" Width="142px">Headline</asp:Label>
			<asp:Label id="Label2" AssociatedControlID="TxtTeaser" style="Z-INDEX: 103; LEFT: 97px; POSITION: absolute; TOP: 215px" runat="server" Width="142px">News Teaser</asp:Label>
			<asp:TextBox id="TxtTeaser" style="Z-INDEX: 104; LEFT: 256px; POSITION: absolute; TOP: 215px" runat="server" Width="298px"></asp:TextBox>
			<asp:Label id="Label3" AssociatedControlID="TANews" style="Z-INDEX: 105; LEFT: 97px; POSITION: absolute; TOP: 245px" runat="server" Width="142px">News Item</asp:Label>
			<asp:TextBox id="TANews" style="Z-INDEX: 106; LEFT: 257px; POSITION: absolute; TOP: 245px" runat="server" Width="298px" TextMode="MultiLine" Height="128px"></asp:TextBox>
			<asp:Label id="Label4" AssociatedControlID="DDLCategory" style="Z-INDEX: 107; LEFT: 97px; POSITION: absolute; TOP: 394px" runat="server" Width="142px" Visible="False">Category</asp:Label>
			<asp:DropDownList id="DDLCategory" style="Z-INDEX: 108; LEFT: 258px; POSITION: absolute; TOP: 394px" runat="server" Visible="False"></asp:DropDownList>
			<asp:Label id="Label5" AssociatedControlID="DDLPriority" style="Z-INDEX: 109; LEFT: 97px; POSITION: absolute; TOP: 421px" runat="server" Width="142px" Visible="False">Priority</asp:Label>
			<asp:DropDownList id="DDLPriority" style="Z-INDEX: 110; LEFT: 259px; POSITION: absolute; TOP: 421px" runat="server" Visible="False"></asp:DropDownList>
			<asp:TextBox id="TXTExpire" style="Z-INDEX: 111; LEFT: 258px; POSITION: absolute; TOP: 448px" runat="server"></asp:TextBox>
			<asp:Label id="Label6" AssociatedControlID="TXTExpire" style="Z-INDEX: 112; LEFT: 97px; POSITION: absolute; TOP: 448px" runat="server" Width="142px" Height="1px">Expire Date</asp:Label>
			<asp:Button id="BTNSubmit" style="Z-INDEX: 113; LEFT: 433px; POSITION: absolute; TOP: 477px" runat="server" Text="Submit/Update"></asp:Button>
			<asp:Button CausesValidation="False" Runat="server" ID="BTNReset" name="BTNReset" style="Z-INDEX: 126; LEFT: 565px; POSITION: absolute; TOP: 477px" Text="Reset" />
			<asp:TextBox id="TXTNewsid" style="Z-INDEX: 114; LEFT: 395px; POSITION: absolute; TOP: 156px" runat="server" Visible="False"></asp:TextBox>
			<asp:Label id="Label7" AssociatedControlID="DDLNewsItems" style="Z-INDEX: 115; LEFT: 99px; POSITION: absolute; TOP: 103px" runat="server" Width="442px">Select a news item from the list to edit it.</asp:Label>
			<asp:DropDownList id="DDLNewsItems" style="Z-INDEX: 116; LEFT: 172px; POSITION: absolute; TOP: 130px" runat="server"></asp:DropDownList>
			<asp:RegularExpressionValidator id="RegularExpressionValidator1" style="Z-INDEX: 117; LEFT: 426px; POSITION: absolute; TOP: 452px" runat="server" ErrorMessage="Date must be in this form mm-dd-yyyy" ControlToValidate="TXTExpire" ValidationExpression="([0-1][0-9](-))([0-3][0-9](-))([1-2][0-9]{3})"></asp:RegularExpressionValidator>
			<asp:RequiredFieldValidator id="RequiredFieldValidator1" style="Z-INDEX: 118; LEFT: 563px; POSITION: absolute; TOP: 191px" runat="server" ErrorMessage="You must enter a headline" ControlToValidate="TxtHeadline">*</asp:RequiredFieldValidator>
			<asp:RequiredFieldValidator id="RequiredFieldValidator2" style="Z-INDEX: 119; LEFT: 564px; POSITION: absolute; TOP: 219px" runat="server" ErrorMessage="You must enter a teaser item" ControlToValidate="TxtTeaser">*</asp:RequiredFieldValidator>
			<asp:RequiredFieldValidator id="RequiredFieldValidator3" style="Z-INDEX: 120; LEFT: 565px; POSITION: absolute; TOP: 247px" runat="server" ErrorMessage="You must enter news text" ControlToValidate="TANews">*</asp:RequiredFieldValidator>
			<asp:RequiredFieldValidator id="RequiredFieldValidator4" style="Z-INDEX: 121; LEFT: 351px; POSITION: absolute; TOP: 395px" runat="server" ErrorMessage="You must select a category" ControlToValidate="DDLCategory" Visible="False">*</asp:RequiredFieldValidator>
			<asp:RequiredFieldValidator id="RequiredFieldValidator5" style="Z-INDEX: 122; LEFT: 352px; POSITION: absolute; TOP: 422px" runat="server" ErrorMessage="You must select a priorty" ControlToValidate="DDLPriority" Visible="False">*</asp:RequiredFieldValidator>
			<asp:ValidationSummary id="ValidationSummary1" style="Z-INDEX: 123; LEFT: 101px; POSITION: absolute; TOP: 504px" runat="server" Width="319px" Height="72px" HeaderText="You must enter in the following items to proceed."></asp:ValidationSummary>
			<asp:Label id="CurrentNewsLabel" AssociatedControlID="BTNGetNews" style="Z-INDEX: 124; LEFT: 102px; POSITION: absolute; TOP: 159px" runat="server" Width="704px" Visible="False" ForeColor="Red"></asp:Label>
			<asp:Button id="BTNGetNews" style="Z-INDEX: 125; LEFT: 425px; POSITION: absolute; TOP: 130px" runat="server" Text="Get News Item" CausesValidation="False"></asp:Button>
		</form>
	</body>
</HTML>
