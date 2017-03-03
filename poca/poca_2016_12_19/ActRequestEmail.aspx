<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ActRequestEmail.aspx.vb" Inherits="PPC.ActRequestEmail" %>
<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>Update Email Address for Account Requests</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<script language="javascript" src="scripts.js" type="text/javascript"></script>
		<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
		<link href="Styles.css" type="text/css" rel="stylesheet" />
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<uc1:trackingheader id="TrackingHeader1" runat="server" location="<a title='Click here to return to settings' href='Settings.aspx' style='font-size: 14px; font-weight: bold;'>Return to Settings</a>" title="Update Email Address for Account Requests" />
			<table id="Table1" style="WIDTH: 620px; left:80px; position:absolute;" cellspacing="10" cellpadding="1" width="620" align="left" border="0">
                <tr>
                    <td>&nbsp;</td>
                </tr>
				<tr>
					<td valign="top" align="right"><asp:label id="Label3" AssociatedControlID="AcctRequestEmail" runat="server" Text="Enter the email address to send account requests to:" /></td>
					<td valign="top" align="left"><asp:textbox id="AcctRequestEmail" Columns="30" Runat="server"></asp:textbox></td>
				</tr>
				<tr>
                    <td>&nbsp;</td>
					<td align="left">
                        <asp:imagebutton id="btnImageButton" onmouseover="Swap(this,submit2);" onmouseout="Swap(this,submit1);" runat="server" alternatetext="Click here to update this setting." imageurl="Images/submit.jpg" CausesValidation="False"></asp:imagebutton>
                    </td>
				</tr>
				<tr>
					<td align="center" colspan="2">
                        <br/>
						<asp:label id="MessageLabel" runat="server" Width="100%" style="float:right;" ForeColor="Red"></asp:label>
                    </td>
				</tr>
			</table>
		</form>
	</body>
</html>
