<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SetThreshold.aspx.vb" Inherits="PPC.SetThreshold" %>
<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>SetThreshold</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0" />
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0" />
		<meta name="vs_defaultClientScript" content="JavaScript" />
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
		<link href="Styles.css" type="text/css" rel="stylesheet" />
		<script language="javascript" src="scripts.js" type="text/javascript"></script>
		<noscript style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
	</head>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<uc1:trackingheader id="TrackingHeader1" runat="server" location="<a href='Settings.aspx' title='Click here to return to settings' style='font-size: 14px; font-weight: bold; '>Return to Settings</a>" />
			<table id="Table1" style="WIDTH: 542px; HEIGHT: 151px; left:80px; position:absolute;" cellspacing="10" cellpadding="1" width="542" align="left" border="0">
				<tr>
					<td align="center" colspan="2">
						<%--<asp:RangeValidator id="RangeValidator1" runat="server" ErrorMessage="Please enter a threshold between 1 and 99." MaximumValue="99" MinimumValue="1" Type="Integer" ControlToValidate="txtThreshold" Display="Dynamic"></asp:RangeValidator>
						<asp:RequiredFieldValidator id="Requiredfieldvalidator1" runat="server" ErrorMessage="Please enter a threshold between 1 and 99." ControlToValidate="txtThreshold" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                        <br/>
						<asp:Label id="MessageLabel" runat="server" Width="250px" Visible="True"></asp:Label>
                    </td>
				</tr>
				<tr>
					<td align="right"><asp:Label id="Label1" AssociatedControlID="txtThreshold" runat="server" Text="Enter a threshold for returned results per module:"/></td>
					<td align="left">
						<asp:TextBox id="txtThreshold" runat="server" Width="27px" maxlength="2"></asp:TextBox>%
					</td>
				</tr>
				<tr>
					<td colspan="2" align="right"><asp:imagebutton id="btnImageButton" onmouseover="Swap(this,submit2);" onmouseout="Swap(this,submit1);" runat="server" imageurl="Images/submit.jpg" alternatetext="Click here to Change your password."></asp:imagebutton></td>
				</tr>
			</table>
		</form>
	</body>
</html>
