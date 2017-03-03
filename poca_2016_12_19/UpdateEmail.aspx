<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="UpdateEmail.aspx.vb" Inherits="PPC.UpdateEmail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
		<meta name=vs_showGrid content="False">
        <title>UpdateEmail</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="Styles.css" type="text/css" rel="stylesheet" />
		<script language="javascript" src="scripts.js" type="text/javascript"></script>
		<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
    </head>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<uc1:trackingheader id="TrackingHeader1" runat="server" location="<a href='Settings.aspx' title='Click here to return to settings' style='font-size: 14px; font-weight: bold; '>Return to Settings</a>" />
			<table cellspacing="10" cellpadding="0" style="width: 629px; height: 126px; left:120px; position:absolute; ">
				<tr>
					<td colspan="2" align="left">
						<asp:Label id="MessageLabel" runat="server" Visible="False" Width="518px"></asp:Label>
                     </td>
				</tr>
				<tr>
					<td>
						The email address currently stored for this account is:
					</td>
					<td>
                        <asp:Label Runat="server" ID="lblCurrentEmail"></asp:Label>
					</td>
				</tr>
				<tr>
					<td>
    					<asp:Label Runat="server" AssociatedControlID="txtNewEmail" ID="Label1">Please enter updated email address:</asp:Label>
					</td>
					<td>
						<asp:TextBox Runat="server" ID="txtNewEmail" Width="246px"></asp:TextBox>
					</td>
				</tr>
				<tr>
                    <td>&nbsp;</td>
					<td align="left">
						<asp:imagebutton id="btnImageButton" onmouseover="Swap(this,submit2);" onmouseout="Swap(this,submit1);" runat="server" imageurl="Images/submit.jpg" alternatetext="Click here to Update Email Address."></asp:imagebutton>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
