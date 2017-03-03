<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DefaultSearchView.aspx.vb" Inherits="PPC.DefaultSearchView" %>
<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
		<title>Set your default search view.</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="scripts.js" type="text/javascript"></script>
		<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
</head>
	<body MS_POSITIONING="FlowLayout">
		<script language="javascript">
		<!--
			if(document.images)
			{
				var submit1 = new Image();
				submit1.src = "Images/submit.jpg";
				
				var submit2 = new Image();
				submit2.src = "Images/submit2.jpg";
			}
		-->
		</script>
		<form id="Form1" method="post" runat="server">
			<uc1:TrackingHeader id="TrackingHeader1" runat="server"></uc1:TrackingHeader><a href="Settings.aspx" title="Click here to Return to Settings">Return 
				to Settings</a>
			<TABLE id="Table1" style="WIDTH: 422px; HEIGHT: 257px" cellSpacing="5" cellPadding="5" width="422" border="0" align="center">
				<tr>
					<td colspan="2">
						<asp:Label Runat="server" ID="MessageLabel" Width="332px"></asp:Label>
					</td>
				</tr>
				<TR>
					<TD colSpan="2">
						<P>Select a default search view:</P>
						<P>The selected view will be the initial start point for every search.&nbsp; The 
							non-selected view can be opened from a link while in the default search view.</P>
					</TD>
				</TR>
				<TR>
					<TD align="right">
						<asp:RadioButton id="SimpleSearch" runat="server" GroupName="radioSearchView" Checked></asp:RadioButton></TD>
					<TD style="WIDTH: 166px"><asp:Label AssociatedControlID="SimpleSearch" Runat="server" ID="Label1" Text="Simple Search View" /></TD>
				</TR>
				<TR>
					<TD align="right">
						<asp:RadioButton id="AdvancedView" runat="server" GroupName="radioSearchView"></asp:RadioButton></TD>
					<TD style="WIDTH: 166px" align="left"><asp:Label AssociatedControlID="AdvancedView" Runat="server" ID="Label2" Text="Advanced Search View" /></TD>
				</TR>
				<TR>
					<TD align="right"></TD>
					<TD align="right"><asp:imagebutton id="btnImageButton" onmouseover="Swap(this,submit2);" onmouseout="Swap(this,submit1);" runat="server" imageurl="Images/submit.jpg" alternatetext="Click here to Change your password."></asp:imagebutton></TD>
				</TR>
			</TABLE>
			<P></P>
		</form>
	</body>
</html>
