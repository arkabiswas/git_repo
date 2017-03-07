<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ActRequestEmail.aspx.vb" Inherits="PPC.ActRequestEmail" %>
<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Update Email Address for Account Requests</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="scripts.js" type="text/javascript"></script>
		<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<uc1:trackingheader id="TrackingHeader1" runat="server" location="<A title='Click here to return to settings' href='Settings.aspx'>Return to Settings</A>" title="Update Email Address for Account Requests" />
			<TABLE id="Table1" style="WIDTH: 620px;" cellSpacing="10" cellPadding="1" width="620" align="center" border="0">
				<TR>
					<TD align="middle" colSpan="2"><BR>
						<asp:label id="MessageLabel" runat="server" Width="382px" ForeColor=Red></asp:label></TD>
				</TR>
				<TR>
					<TD  vAlign="top" align="right"><asp:label id="Label3" AssociatedControlID="AcctRequestEmail" runat="server" Text="Enter the email address to send account requests to:" /></TD>
					<TD vAlign="top" align="left"><asp:textbox id="AcctRequestEmail" Columns="20" Runat="server"></asp:textbox></TD>
				</TR>
				<TR>
					<TD align="right" colSpan="2"><asp:imagebutton id="btnImageButton" onmouseover="Swap(this,submit2);" onmouseout="Swap(this,submit1);" runat="server" alternatetext="Click here to update this setting." imageurl="Images/submit.jpg" CausesValidation="False"></asp:imagebutton></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
