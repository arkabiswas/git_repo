<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SetThreshold.aspx.vb" Inherits="PPC.SetThreshold" %>
<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>SetThreshold</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="scripts.js" type="text/javascript"></script>
		<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
	</HEAD>
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
			<uc1:TrackingHeader id="TrackingHeader1" runat="server"></uc1:TrackingHeader>
	<a href="Settings.aspx" title="Click here to return to personal settings">Return 
				to Settings</a>
			<TABLE id="Table1" style="WIDTH: 542px; HEIGHT: 151px" cellSpacing="10" cellPadding="1" width="542" align="center" border="0">
				<TR>
					<TD align="middle" colSpan="2">
						<asp:RangeValidator id="RangeValidator1" runat="server" ErrorMessage="Please enter a threshold between 1 and 99." MaximumValue="99" MinimumValue="1" Type="Integer" ControlToValidate="txtThreshold" Display="Dynamic"></asp:RangeValidator>
						<asp:RequiredFieldValidator id="Requiredfieldvalidator1" runat="server" ErrorMessage="Please enter a threshold between 1 and 99." ControlToValidate="txtThreshold" Display="Dynamic"></asp:RequiredFieldValidator><BR>
						<asp:Label id="MessageLabel" runat="server" Width="250px" Visible="False"></asp:Label></TD>
				</TR>
				<TR>
					<TD align="right"><asp:Label id="Label1" AssociatedControlID="txtThreshold" runat="server" Text="Enter a threshold for returned results per module:"/></TD>
					<TD align="left">
						<asp:TextBox id="txtThreshold" runat="server" Width="27px"></asp:TextBox>%
					</TD>
				</TR>
				<TR>
					<TD colspan="2" align="right"><asp:imagebutton id="btnImageButton" onmouseover="Swap(this,submit2);" onmouseout="Swap(this,submit1);" runat="server" imageurl="Images/submit.jpg" alternatetext="Click here to Change your password."></asp:imagebutton></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
