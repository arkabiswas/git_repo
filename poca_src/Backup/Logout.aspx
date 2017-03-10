<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Logout.aspx.vb" Inherits="PPC.Logout" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<%@ Register TagPrefix="uc1" TagName="LoginNewsControl" Src="LoginNewsControl.ascx" %> 
<html>
  <head>
    <title>Logout</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <LINK href="Styles.css" type="text/css" rel="stylesheet" />	
  </head>
  <body >
		<script language="javascript" type="text/javascript">
		<!--
			if(document.images)
			{
				var login1 = new Image();
				login1.src = "Images/login.jpg";
				
				var login2 = new Image();
				login2.src = "Images/login2.jpg";
			}
		-->
		</script>
		<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
		<form id="Form2" method="post" runat="server">
			<p>&nbsp;</p>
			<p>&nbsp;</p>
			<table style="border-color:Black" cellspacing="0" cellpadding="0" align="center" summary="This table is used for page layout." border="1">
				<tbody>
					<tr>
						<td>
							<div align="center"><asp:image id="Image1" runat="server" alternatetext="FDA Automated Method of Minimizing Medication Errors Due to Similar Proprietary and Established Names" imageurl="Images\fdasystemlogo.gif"></asp:image></div>
							<div align="left">&nbsp;</div>
							<div align="left"><uc1:loginnewscontrol id="LoginNewsControl1" runat="server"></uc1:loginnewscontrol></DIV>
							<table style="FONT-SIZE: 100%" align="center" summary="This table is used for page layout.">
								<tbody> 
									<tr>
										<td style="WIDTH:auto;" align="center"> 
										<asp:label id="lblMessage" runat="server" cssclass="errorMessage" 
                                                Font-Size="Larger"></asp:label></td>
									</tr> 								
								</tbody>
							</table>
							<table style="FONT-SIZE: 100%" align="center" summary="This table is used for page layout.">
								<tbody>
									<tr>
										<td style="WIDTH: 211px" align="center">										
										<asp:LinkButton runat="server" ID="lnkLogin" CssClass="formr" 
                                            Text="Begin POCA Session" Font-Size="Larger"></asp:LinkButton> 							
										</td>
									</tr> 								
								</tbody>
							</table>
						</td>
					</tr>
				</tbody>
			</table>
			<br />
			<br />
			<table cellspacing="0" cellpadding="0" align="center" width="58%">
				<tr>
					<td><b>Disclaimer</b></td>
				</tr>
				<tr>
					<td>
						<p>
						This is a Federal computer system and is the property of the United States 
Government. It is for authorized use only. Users (authorized or unauthorized) 
have no explicit or implicit expectation of privacy.
						</p>
						<p>
						Any or all use of this system and all files on this system may be intercepted, 
monitored, recorded, copied, audited, inspected, and disclosed to authorized 
site, Food and Drug Administration, and law enforcement personnel, as well as 
authorized officials of other agencies, both domestic and foreign. By using this 
system, the user consents to such interception, monitoring, recording, copying, 
auditing, inspection, and disclosure at the discretion of authorized site or 
Food and Drug Administration personnel.
						</p>
						<p>
						Unauthorized or improper use of this system may result in administrative 
disciplinary action and civil and criminal penalties. By using this system you 
indicate your awareness of and consent to these terms and conditions of use. 
						</p>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
