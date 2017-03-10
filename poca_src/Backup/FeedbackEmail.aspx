<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FeedbackEmail.aspx.vb" Inherits="PPC.FeedbackEmail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Update Email For Feedback Emails.</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="scripts.js" type="text/javascript"></script>
		<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<uc1:trackingheader id="TrackingHeader1" runat="server" />
			<A title="Click here to return to personal settings" href="Settings.aspx">Return 
				to Settings</A>
			<TABLE id="Table1" style="WIDTH: 620px; HEIGHT: 151px" cellSpacing="10" cellPadding="1" width="620" align="center" border="0">
				<TR>
					<TD align="middle" colSpan="2"><BR>
						<asp:label id="MessageLabel" runat="server" Width="382px" ForeColor="Red"></asp:label></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 455px" vAlign="top" align="right"><asp:label id="Label3" AssociatedControlID="TxtFeedbackEmail" runat="server" Text="Enter the email address to feedback comments to:" /></TD>
					<TD vAlign="top" align="left">
						<asp:textbox id="TxtFeedbackEmail" Columns="20" Runat="server" MaxLength="35" /></TD>
				</TR>
				<TR>
					<TD align="right" colSpan="2"><asp:imagebutton id="btnImageButton" onmouseover="Swap(this,submit2);" onmouseout="Swap(this,submit1);" runat="server" alternatetext="Click here to update this setting." imageurl="Images/submit.jpg" CausesValidation="False"></asp:imagebutton></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
