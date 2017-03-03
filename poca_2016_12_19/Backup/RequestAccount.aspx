<%@ Page Language="vb" AutoEventWireup="false" Codebehind="RequestAccount.aspx.vb" Inherits="PPC.RequestAccount" %>
<%@ Register TagPrefix="uc1" TagName="TrackingHeaderNoNav" Src="TrackingHeaderNoNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
		<title>Request an Account</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="Styles.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="scripts.js" type="text/javascript"></script>
		<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
</head>
	<body ms_positioning="FlowLayout">
		<script language="javascript">
		<!--
			if(document.images)
			{
				var login1 = new Image();
				login1.src = "Images/submit.jpg";
				
				var login2 = new Image();
				login2.src = "Images/submit2.jpg";
			}
		-->
		</script>
		<FORM id="Form1" method="post" runat="server">
			<uc1:TrackingHeaderNoNav id="TrackingHeaderNoNav1" runat="server" title="Request an Account" location="<A title='Return to Login Screen' href='default.aspx'>Return to Login Screen</A>" />
			<asp:panel id="panelSendEmail" runat="server">
<table cellspacing=5 cellpadding=5 width=750 border=0>
  <tr>
    <td colspan=3>If you would like to request an account, enter your name and 
      email address. The system administrator will contact you shortly with 
      login information.</td></tr>
  <tr>
    <td align=right width=250><label for=txtRequestAccountName>Please enter 
      your name:</label> </td>
    <td align=left width=250><asp:TextBox id=txtRequestAccountName Runat="server" Width="200"></asp:TextBox></td>
    <td width=250><asp:RequiredFieldValidator id=RequiredFieldValidator runat="server" Display="Dynamic" ErrorMessage="Please enter your name!" ControlToValidate="txtRequestAccountName"></asp:RequiredFieldValidator></td></tr>
  <tr>
    <td align=right width=250><label for=txtRequestAccountEmail>Please enter 
      your email address:</label> </td>
    <td align=left width=250><asp:TextBox id=txtRequestAccountEmail Runat="server" Width="200"></asp:TextBox></td>
    <td width=250><asp:RequiredFieldValidator id=Requiredfieldvalidator1 runat="server" Display="Dynamic" ErrorMessage="Please enter your email address!" ControlToValidate="txtRequestAccountEmail"></asp:RequiredFieldValidator></td></tr>
  <tr>
    <td align=right colSpan=2><asp:imagebutton id=btnImageButton onmouseover=Swap(this,login2); onmouseout=Swap(this,login1); runat="server" alternatetext="Click here to request an account." imageurl="Images/submit.jpg"></asp:imagebutton></td></tr></table>
			</asp:panel>
				<asp:label id="MailSent" runat=server visible="false">
					Your request for an account has been sent to the System Administrator. 
					Please <a href="default.aspx">click here</a> to return to the login screen.
				</asp:label>		
		</FORM>
	</body>
</html>
