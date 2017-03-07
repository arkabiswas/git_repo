<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="UserEdit.aspx.vb" Inherits="PPC.UserEdit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>UserEdit</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="Styles.css" type="text/css" rel="stylesheet" />
	</head>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<uc1:trackingheader id="TrackingHeader1" title="Edit User" runat="server" location="<a href='UserAdmin.aspx' style='font-size: 14px; font-weight: bold; '>Return to User Administration</a>" />

			<asp:label id="ErrorLabel" style="z-index: 104; left: 205px; position: absolute; TOP: 111px"
				runat="server" Width="424px" ForeColor="Red"></asp:label>
			
			<asp:label id="EditUserLabel" style="z-index: 100; left: 120px; position: absolute; TOP: 107px"
				runat="server" Width="256px" Font-Size="Larger" Font-Bold="true" Font-Underline="true">Edit User</asp:label>
			<table id="Table1" style="z-index: 101; left: 180px; width: 529px; position: absolute; TOP: 140px"
				cellspacing="0" cellpadding="2" width="529" border="0">
				<tr>
					<td valign="top">User Name:</td>
					<td valign="top">
                        <asp:Label id="Username" runat="server"></asp:Label>
                    </td>
				</tr>
				<tr>
					<td valign="top">First Name:</td>
					<td valign="top">
                        <asp:textbox id="Firstname" runat="server"></asp:textbox>
                        <asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ControlToValidate="Firstname" ErrorMessage="You must enter a First Name.">*</asp:requiredfieldvalidator>
                     </td>
				</tr>
				<tr>
					<td valign="top">Last Name:</td>
					<td valign="top">
                        <asp:textbox id="Lastname" runat="server"></asp:textbox>
                        <asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ControlToValidate="Lastname" ErrorMessage="You must enter a Last Name.">*</asp:requiredfieldvalidator>
                    </td>
				</tr>
				<tr>
					<td valign="top">Email:</td>
					<td valign="top">
                        <asp:textbox id="Email" runat="server" Width="234px"></asp:textbox>
                        <asp:requiredfieldvalidator id="EmailRequired" ControlToValidate="Email" ErrorMessage="You must enter an email address." runat="server">*</asp:requiredfieldvalidator>
                    </td>
				</tr>
				<tr>
					<td valign="top">User Group:</td>
					<td valign="top">
                        <asp:dropdownlist id="Usergrouplist" runat="server"></asp:dropdownlist>
                    </td>
				</tr>
			</table>
			<asp:button id="UpdateUser" style="z-index: 102; left: 323px; position: absolute; top: 344px"
				runat="server" Text="Update User"></asp:button>
            <asp:validationsummary id="ValidationSummary1" style="z-index: 103; left: 74px; position: absolute; top: 376px"
				runat="server" Width="312px"></asp:validationsummary>
			<asp:Button id="DisableUser" style="z-index: 106; left: 436px; position: absolute; top: 344px"
				runat="server" Text="Disable User"></asp:Button>
			<asp:Button id="EnableUser" style="z-index: 106; left: 436px; position: absolute; top: 344px"
				runat="server" Text="Enable User" Visible="False"></asp:Button></form>
	</body>
</html>
