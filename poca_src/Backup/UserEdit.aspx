<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="UserEdit.aspx.vb" Inherits="PPC.UserEdit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>UserEdit</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:label id="ErrorLabel" style="Z-INDEX: 104; LEFT: 205px; POSITION: absolute; TOP: 111px"
				runat="server" Width="424px" ForeColor="Red"></asp:label>
			<uc1:trackingheader id="TrackingHeader1" runat="server" location="<a href='UserAdmin.aspx'>Return to User Administration</a>"
				title="Edit User" />
			<asp:label id="EditUserLabel" style="Z-INDEX: 100; LEFT: 40px; POSITION: absolute; TOP: 107px"
				runat="server" Width="256px" Font-Size="Larger">Edit User:</asp:label>
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 80px; WIDTH: 529px; POSITION: absolute; TOP: 140px"
				cellSpacing="0" cellPadding="2" width="529" border="0">
				<TR>
					<TD style="HEIGHT: 10px" vAlign="top">User Name:</TD>
					<TD style="HEIGHT: 10px" vAlign="top">
						<asp:Label id="Username" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 17px" vAlign="top">First Name:</TD>
					<TD style="HEIGHT: 17px" vAlign="top"><asp:textbox id="Firstname" runat="server"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ControlToValidate="Firstname" ErrorMessage="You must enter a First Name.">*</asp:requiredfieldvalidator></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 5px" vAlign="top">Last Name:</TD>
					<TD style="HEIGHT: 5px" vAlign="top"><asp:textbox id="Lastname" runat="server"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ControlToValidate="Lastname" ErrorMessage="You must enter a Last Name.">*</asp:requiredfieldvalidator></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 9px" vAlign="top">Email:</TD>
					<TD style="HEIGHT: 9px" vAlign="top"><asp:textbox id="Email" runat="server"></asp:textbox><asp:requiredfieldvalidator id="EmailRequired" ControlToValidate="Email" ErrorMessage="You must enter an email address."
							Runat="server">*</asp:requiredfieldvalidator></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 97px" vAlign="top">User Group:</TD>
					<TD style="HEIGHT: 97px" vAlign="top"><asp:dropdownlist id="Usergrouplist" runat="server"></asp:dropdownlist></TD>
				</TR>
			</TABLE>
			<asp:button id="UpdateUser" style="Z-INDEX: 102; LEFT: 323px; POSITION: absolute; TOP: 344px"
				runat="server" Text="Update User"></asp:button><asp:validationsummary id="ValidationSummary1" style="Z-INDEX: 103; LEFT: 74px; POSITION: absolute; TOP: 376px"
				runat="server" Width="312px"></asp:validationsummary>
			<asp:Button id="DisableUser" style="Z-INDEX: 106; LEFT: 436px; POSITION: absolute; TOP: 344px"
				runat="server" Text="Disable User"></asp:Button>
			<asp:Button id="EnableUser" style="Z-INDEX: 106; LEFT: 436px; POSITION: absolute; TOP: 344px"
				runat="server" Text="Enable User" Visible="False"></asp:Button></form>
	</body>
</HTML>
