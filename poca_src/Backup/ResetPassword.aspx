<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TrackingHeaderNoNav" Src="TrackingHeaderNoNav.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ResetPassword.aspx.vb" Inherits="PPC.ResetPassword" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
		<title>Reset Password</title>
		<link href="Styles.css" type="text/css" rel="stylesheet">
		<script language="javascript" type="text/javascript" src="scripts.js"></script>
		<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
</head>
	<body ms_positioning="FlowLayout">
		<FORM id="Form1" method="post" runat="server">
			<uc1:TrackingHeaderNoNav id="TrackingHeaderNoNav1" runat="server" />
			<a title="Return to Login Screen" href="default.aspx">Return to Login Screen</a>
			<asp:panel id="panelSendEmail" runat="server">
<table cellspacing=5 cellpadding=5 width=750 border=0>
  <tr>
    <td colspan=3>Enter your username below and hit submit to recieve a 
      temporary password. You will use that password to login where afterwards 
      you will be asked to change that password.</td></tr>
  <tr>
    <td align=right width=250><label for=txtResetAccountName>Please enter your 
      username:</label> </td>
    <td align=left width=250>
<asp:TextBox id=txtResetAccountName Runat="server" Width="200"></asp:TextBox></td>
    <td width=250>
<asp:RequiredFieldValidator id=RequiredFieldValidator runat="server" Display="Dynamic" ErrorMessage="Please enter your name!" ControlToValidate="txtResetAccountName"></asp:RequiredFieldValidator></td></tr>
  <tr>
    <td align=right colSpan=2>
<asp:imagebutton id=btnImageButton onmouseover=Swap(this,login2); onmouseout=Swap(this,login1); runat="server" alternatetext="Click here to request an account." imageurl="Images/submit.jpg"></asp:imagebutton></td></tr></table>
			</asp:panel>
			<div id="Messages">
				<asp:label id="MailSent" runat=server visible="false">
					Your password has successfully been reset. You should receive an email in several minutes. 
					If you do not receive an email within an hour please contact your system administrator.
				</asp:label>
			</div>
		</FORM>
	</body>
</html>
