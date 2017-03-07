<%@ Register TagPrefix="uc1" TagName="TrackingHeaderNoNav" Src="TrackingHeaderNoNav.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Disclaimer.aspx.vb" Inherits="PPC.Disclaimer" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Disclaimer</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="Styles.css" type="text/css" rel="stylesheet">
		<script language="javascript" type="text/javascript" src="scripts.js"></script>
		<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
		<script language="javascript">
		if(document.images)
		{
			var logout1 = new Image();
			logout1.src = "Images/logout.jpg";
			
			var logout2 = new Image();
			logout2.src = "Images/logout2.jpg";

			var agree1 = new Image();
			agree1.src = "Images/agree.jpg";
			
			var agree2 = new Image();
			agree2.src = "Images/agree2.jpg";
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<uc1:TrackingHeaderNoNav id="TrackingHeaderNoNav1" runat="server"></uc1:TrackingHeaderNoNav>
			<table class="tableFont">
				<tr>
					<td><b>Disclaimer</b>&nbsp;&nbsp;&nbsp;<a href="default.aspx" title="Return to Login Screen">Return 
							to Login Screen</a></td>
				</tr>
				<tr>
					<td>This is a Federal computer system and is the property of the United States 
						Government. It is for authorized use only. Users (authorized or unauthorized) 
						have no explicit or implicit expectation of privacy.
						<br>
						<br>
						Any or all use of this system and all files on this system may be intercepted, 
						monitored, recorded, copied, audited, inspected, and disclosed to authorized 
						site, Food and Drug Administration, and law enforcement personnel, as well as 
						authorized officials of other agencies, both domestic and foreign. By using 
						this system, the user consents to such interception, monitoring, recording, 
						copying, auditing, inspection, and disclosure at the discretion of authorized 
						site or Food and Drug Administration personnel.
						<br>
						<br>
						Unauthorized or improper use of this system may result in administrative 
						disciplinary action and civil and criminal penalties. <u>By continuing to use this 
							system you indicate your awareness of and consent to these terms and conditions 
							of use.</u> <u>LOG OUT IMMEDIATELY if you do not agree to the conditions stated 
							in this warning.</u>
					</td>
				</tr>
			</table>
			<br>
			<asp:ImageButton id="ibtnAgree" runat="server" alternatetext="Agree" imageurl="images/agree.jpg" onmouseover="Swap(this, agree2);" onmouseout="Swap(this, agree1);"></asp:ImageButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:ImageButton id="ibtnLogout" runat="server" alternatetext="Logout" imageurl="images/logout.jpg" onmouseover="Swap(this, logout2);" onmouseout="Swap(this, logout1);"></asp:ImageButton>&nbsp;
		</form>
	</body>
</HTML>
