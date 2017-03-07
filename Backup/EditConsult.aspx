<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EditConsult.aspx.vb" Inherits="PPC.EditConsult" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Edit a Consult</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="scripts.js"></script>
		<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<uc1:trackingheader id="TrackingHeader1" title="Edit Consult" runat="server" />
			<asp:label id="Label1" 
                style="Z-INDEX: 101; LEFT: 86px; POSITION: absolute; TOP: 123px" runat="server" 
                AssociatedControlID="TxtProductName">Proprietary Name</asp:label>
			<asp:textbox id="TxtProductName" style="Z-INDEX: 102; LEFT: 198px; POSITION: absolute; TOP: 120px"
				runat="server" tabIndex="1" />
			<asp:label id="Label2" 
                style="Z-INDEX: 103; LEFT: 96px; POSITION: absolute; TOP: 214px" runat="server" 
                AssociatedControlID="TxtProductModifier">Product Modifier</asp:label>
			<asp:textbox id="TxtProductModifier" style="Z-INDEX: 104; LEFT: 198px; POSITION: absolute; TOP: 212px"
				runat="server" tabIndex="3" />
			<asp:label id="Label5" 
                style="Z-INDEX: 107; LEFT: 96px; POSITION: absolute; TOP: 265px" runat="server"
				Width="236px" Height="15px" AssociatedControlID="TxtDTReceived">Date Received</asp:label>
			<asp:textbox id="TxtDTReceived" style="Z-INDEX: 108; LEFT: 198px; POSITION: absolute; TOP: 261px"
				runat="server" tabIndex="4" />
			<asp:label id="Label7" style="Z-INDEX: 109; LEFT: 96px; POSITION: absolute; TOP: 326px" runat="server">Consult Number</asp:label>
			<asp:label id="Label8" 
                style="Z-INDEX: 110; LEFT: 77px; POSITION: absolute; TOP: 352px" runat="server" 
                AssociatedControlID="TxtAppNumber">Application Number</asp:label>
			<asp:label id="Label9" 
                style="Z-INDEX: 111; LEFT: 94px; POSITION: absolute; TOP: 378px" runat="server" 
                AssociatedControlID="DDLAppType">Application Type</asp:label>
			<asp:label id="TxtConsultNumber" style="Z-INDEX: 112; LEFT: 197px; POSITION: absolute; TOP: 326px"
				runat="server" tabIndex="6" />
			<asp:textbox id="TxtAppNumber" style="Z-INDEX: 114; LEFT: 197px; POSITION: absolute; TOP: 350px"
				runat="server" tabIndex="7"></asp:textbox>
			<INPUT id="BtnReset" style="Z-INDEX: 115; LEFT: 299px; POSITION: absolute; TOP: 492px"
				accessKey="R" type="reset" value="Reset" name="BtnReset" tabIndex="11">
			<asp:requiredfieldvalidator id="RequiredFieldValidator1" style="Z-INDEX: 116; LEFT: 421px; POSITION: absolute; TOP: 123px"
				runat="server" Width="92px" ControlToValidate="TxtProductName" ErrorMessage="Enter a Proprietary Name">* required info</asp:requiredfieldvalidator>
			<asp:regularexpressionvalidator id="ValidDateRecieved" style="Z-INDEX: 117; LEFT: 421px; POSITION: absolute; TOP: 264px"
				runat="server" ControlToValidate="TxtDTReceived" ErrorMessage="Date must be in this form mm-dd-yyyy" ValidationExpression="([0-1][0-9](-))([0-3][0-9](-))([1-2][0-9]{3})"></asp:regularexpressionvalidator>
			<asp:dropdownlist id="DDLAppType" style="Z-INDEX: 120; LEFT: 197px; POSITION: absolute; TOP: 374px"
				runat="server" tabIndex="8" />
			<asp:label id="ErrorLabel" style="Z-INDEX: 121; LEFT: 96px; POSITION: absolute; TOP: 521px"
				runat="server" Width="326px" Height="15px" Visible="False" />
			<asp:label id="EPDDateLabel" style="Z-INDEX: 105; LEFT: 96px; POSITION: absolute; TOP: 291px"
				runat="server" AssociatedControlID="EPDDate">EPD Date</asp:label>
			<asp:textbox id="EPDDate" style="Z-INDEX: 106; LEFT: 198px; POSITION: absolute; TOP: 285px" runat="server"
				tabIndex="5" />
			<asp:regularexpressionvalidator id="ValidEPDDate" style="Z-INDEX: 118; LEFT: 421px; POSITION: absolute; TOP: 291px"
				runat="server" ControlToValidate="EPDDate" ErrorMessage="Date must be in this form mm-dd-yyyy" ValidationExpression="([0-1][0-9](-))([0-3][0-9](-))([1-2][0-9]{3})"></asp:regularexpressionvalidator>
			<asp:Label id="Label4" 
                style="Z-INDEX: 124; LEFT: 88px; POSITION: absolute; TOP: 147px" runat="server"
				Width="130px" AssociatedControlID="AlternativeName">Alternative Name</asp:Label>
			<asp:TextBox id="AlternativeName" style="Z-INDEX: 125; LEFT: 198px; POSITION: absolute; TOP: 144px"
				runat="server" tabIndex="1" />
			<asp:Label id="Label6" 
                style="Z-INDEX: 126; LEFT: 85px; POSITION: absolute; TOP: 170px" runat="server"
				Width="115px" AssociatedControlID="EstablishedName">Established Name</asp:Label>
			<asp:TextBox id="EstablishedName" style="Z-INDEX: 127; LEFT: 198px; POSITION: absolute; TOP: 168px"
				runat="server" tabIndex="2" />
			<asp:Label id="Label3" 
                style="Z-INDEX: 128; LEFT: 95px; POSITION: absolute; TOP: 402px" runat="server"
				Width="98px" AssociatedControlID="Comments">Comments</asp:Label>
			<asp:TextBox id="Comments" style="Z-INDEX: 129; LEFT: 197px; POSITION: absolute; TOP: 399px"
				runat="server" Columns="25" Rows="5" TextMode="MultiLine" tabIndex="9"></asp:TextBox>
			<!-- UID's for updating all 3 name types -->
			<asp:textbox id="ProductNameUID" runat="server" Visible="False" />
			<asp:TextBox ID="EstablishedNameUID" Runat="server" Visible="False" />
			<asp:TextBox ID="AlternateNameUID" Runat="server" Visible="False" />
			<asp:hyperlink id="ProductDetailsLink" style="Z-INDEX: 125; LEFT: 53px; POSITION: absolute; TOP: 492px"
				runat="server" NavigateUrl="AddProductDetails.aspx" tabIndex="25">Add Product Details</asp:hyperlink>
			<asp:button id="BTNDelete" style="Z-INDEX: 123; LEFT: 353px; POSITION: absolute; TOP: 492px"
				runat="server" Text="Mark as Complete" CausesValidation="False" tabIndex="12" />
			<asp:button id="BtnADD" style="Z-INDEX: 112; LEFT: 233px; POSITION: absolute; TOP: 492px" runat="server"
				Text="Submit" tabIndex="10" />
		</form>
	</body>
</HTML>
