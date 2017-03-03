<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="UserAdmin.aspx.vb" Inherits="PPC.UserAdmin" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>User Administration</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<link href="Styles.css" type="text/css" rel="stylesheet" />
	</head>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:button id="EditDisabledUser" style="Z-INDEX: 112; LEFT: 550px; POSITION: absolute; TOP: 192px"
				runat="server" Width="75px" Text="Edit User" CausesValidation="False"></asp:button>
			<asp:label id="Label2" AssociatedControlID="UserList" style="Z-INDEX: 115; LEFT: 180px; POSITION: absolute; TOP: 160px" runat="server"
				Width="96px" Height="8px">Enabled Users:</asp:label>
			<asp:label id="Label1" AssociatedControlID="DisabledUserList"  style="Z-INDEX: 114; LEFT: 180px; POSITION: absolute; TOP: 192px" runat="server"
				Width="96px">Disabled Users:</asp:label>
			<%--<asp:Button id="ResetDisabledPwd" style="Z-INDEX: 113; LEFT: 488px; POSITION: absolute; TOP: 192px"
				runat="server" Text="Reset Password" CausesValidation="False" Visible="False"></asp:Button>--%>
			<uc1:trackingheader id="TrackingHeader1" title="User Administration" runat="server" location="<a href='Settings.aspx' style='font-size: 14px; font-weight: bold; '>Return to Settings</a>" />
			<asp:label id="EditLabel" style="Z-INDEX: 100; LEFT: 120px; POSITION: absolute; TOP: 107px; font-weight:bold;"
				runat="server" Font-Size="120%" Width="291px">Edit User:</asp:label>
            <asp:label id="EditInfoLabel" style="Z-INDEX: 102; LEFT: 180px; POSITION: absolute; TOP: 136px"
				runat="server" Width="350px">Select a user from the list below to edit their details.</asp:label>
			<asp:dropdownlist id="UserList" style="Z-INDEX: 103; LEFT: 280px; POSITION: absolute; TOP: 159px"
				runat="server" Width="260px"></asp:dropdownlist>
			<asp:dropdownlist id="DisabledUserList" style="Z-INDEX: 111; LEFT: 280px; POSITION: absolute; TOP: 192px"
				runat="server" Width="260px"></asp:dropdownlist>

			<asp:label id="AddUserLabel" style="Z-INDEX: 104; LEFT: 120px; POSITION: absolute; TOP: 270px; font-weight:bold;"
				runat="server" Font-Size="120%" Width="256px">Add a new user:</asp:label>
			<table id="Table1" style="Z-INDEX: 105; LEFT: 180px; WIDTH: 529px; POSITION: absolute; TOP: 300px"
				cellspacing="0" cellpadding="2" width="529" border="0">
				<tr>
					<td style="height: 10px" valign="top"><asp:label id="Label3" AssociatedControlID="Username" runat="server" Text="User Name:" /></td>
					<td style="height: 10px" valign="top"><asp:textbox id="Username" runat="server"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="Firstname" ErrorMessage="You must enter a Username.">*</asp:requiredfieldvalidator></td>
				</tr>
				<tr>
					<td style="height: 17px" valign="top"><asp:label id="Label4" AssociatedControlID="Firstname" runat="server" Text="First Name:" /></td>
					<td style="height: 17px" valign="top"><asp:textbox id="Firstname" runat="server"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ControlToValidate="Lastname" ErrorMessage="You must enter a First Name.">*</asp:requiredfieldvalidator></td>
				</tr>
				<tr>
					<td style="height: 5px" valign="top"><asp:label id="Label5" AssociatedControlID="Lastname" runat="server" Text="Last Name:" /></td>
					<td style="height: 5px" valign="top"><asp:textbox id="Lastname" runat="server"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ControlToValidate="Lastname" ErrorMessage="You must enter a Last Name.">*</asp:requiredfieldvalidator></td>
				</tr>
				<%--<tr>
					<td style="height: 1px" valign="top"><asp:label id="Label6" AssociatedControlID="Password" runat="server" Text="Password:" /></td>
					<td style="height: 1px" valign="top"><asp:textbox id="Password" runat="server"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator4" runat="server" ControlToValidate="Password" ErrorMessage="You must enter a Password.">*</asp:requiredfieldvalidator></td>
				</tr>
				<tr>
					<td style="height: 9px" valign="top"><asp:label id="Label7" AssociatedControlID="Verifypassword" runat="server" Text="Verify Password:" /></td>
					<td style="height: 9px" valign="top"><asp:textbox id="Verifypassword" runat="server"></asp:textbox><asp:comparevalidator id="CompareValidator1" runat="server" ControlToValidate="Verifypassword" ErrorMessage="Passwords do not match."
							ControlToCompare="Password">*</asp:comparevalidator></td>
				</tr>--%>
				<tr>
					<td style="height: 9px" valign="top"><asp:label id="Label8" AssociatedControlID="Email" runat="server" Text="Email:" /></td>
					<td style="height: 9px" valign="top"><asp:textbox id="Email" runat="server"></asp:textbox><asp:requiredfieldvalidator id="EmailRequired" ControlToValidate="Email" ErrorMessage="You must enter an email address."
							Runat="server">*</asp:requiredfieldvalidator></td>
				</tr>
				<tr>
					<td style="height: 97px" valign="top"><asp:label id="Label9" AssociatedControlID="Usergrouplist" runat="server" Text="User Group:" /></td>
					<td style="height: 97px" valign="top"><asp:dropdownlist id="Usergrouplist" runat="server"></asp:dropdownlist></td>
				</tr>
			</table>
			<asp:button id="AddUser" style="Z-INDEX: 106; LEFT: 305px; POSITION: absolute; TOP: 456px" runat="server"
				Text="Add User" />
			<asp:button id="EditUser" style="Z-INDEX: 107; LEFT: 550px; POSITION: absolute; TOP: 160px"
				runat="server" Width="75px" Text="Edit User" CausesValidation="False" />
			<asp:validationsummary id="ValidationSummary1" style="Z-INDEX: 108; LEFT: 305px; POSITION: absolute; TOP: 480px"
				runat="server" Width="312px" />
			<asp:label id="ErrorLabel" style="Z-INDEX: 109; LEFT: 305px; POSITION: absolute; TOP: 270px"
				runat="server" Width="424px" ForeColor="Red" />
			<%--<asp:Button id="ResetPwd" style="Z-INDEX: 110; LEFT: 488px; POSITION: absolute; TOP: 160px"
				runat="server" Text="Reset Password" CausesValidation="False" />--%>
		</form>
	</body>
</html>
