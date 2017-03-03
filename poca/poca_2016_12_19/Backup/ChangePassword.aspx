<%@ Register TagPrefix="uc1" TagName="TrackingHeaderNoNav" Src="TrackingHeaderNoNav.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ChangePassword.aspx.vb" Inherits="PPC.ChangePassword" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
		<title>Change Your Password</title>
		<LINK href="Styles.css" type="text/css" rel="stylesheet" />		
		<script language="javascript" src="scripts.js" type="text/javascript"></script>
		<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
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
	</head>
	<body ms_positioning="FlowLayout">
		<FORM id="Form1" method="post" runat="server">
			<uc1:trackingheadernonav id="TrackingHeader1NoNav" title="Change Password" runat="server" />
			<asp:panel id="panelChangePassword" runat="server" Height="286px" Width="554px">
				<asp:Label id=lblReturnText Runat="server" />
				<table style="WIDTH: 561px" 
						cellspacing="0"
						cellpadding="0" 
						align="center"
						summary="This table is used for page layout." border="0">
					<tr>
						<td style="WIDTH: 177px; HEIGHT: 28px">&nbsp;</td>
						<td style="WIDTH: 500px; HEIGHT: 28px">
							<asp:label id=lblMustChangeMessage 
										runat="server" 
										visible="False" 
										cssclass="ErrorMessage">
								You must change your password to sucessfully login to the system. Enter all the
								information into the form below and click Submit to change your password.			
							</asp:label>
							<asp:ValidationSummary id=valSum 
													runat="server" 
													DisplayMode="BulletList" 
													HeaderText="Please correct the following:" />
							<asp:Label id=lblLoginValidate 
										runat="server" 
										Width="209px" 
										visible="False" 
										forecolor="red" />
						</td>
					</tr>
					<tr>
						<td style="WIDTH: 177px; align: right" align=right>
							<p>
								<label for=txtLogin>User ID:</label>								 
							</p>
						</td>
						<td style="WIDTH: 500px">
							<asp:textbox id="txtLogin" runat="server" width="211px" />
							<asp:RequiredFieldValidator id=RequiredFieldValidator 
														runat="server" 
														ControlToValidate="txtLogin" 
														ErrorMessage="Enter User ID" 
														Display="Static">
													*
							</asp:RequiredFieldValidator>
						</td>
					</tr>
					<tr>
						<td style="WIDTH: 177px" align=right>
							<label for="txtOldPassword">Old Password:</label>
						</td>
						<td style="WIDTH: 500px">
							<asp:textbox id="txtOldPassword" 
										runat="server" 
										width="211px" 
										textmode="Password" 
										height="24px" />
							<asp:RequiredFieldValidator id=Requiredfieldvalidator1 
														runat="server" 
														ControlToValidate="txtOldPassword" 
														ErrorMessage="Enter Old Password " 
														Display="Static">
													*
							</asp:RequiredFieldValidator>
						</td>
					</tr>
					<tr>
						<td style="WIDTH: 177px" align=right>
							<label for=txtNewPassword>New Password:</label>
						</td>
						<td style="WIDTH: 500px">
							<asp:textbox id=txtNewPassword 
										runat="server" 
										width="211px" 
										textmode="Password" 
										height="24px" />
							<asp:RequiredFieldValidator id=Requiredfieldvalidator2 
														runat="server" 
														ControlToValidate="txtNewPassword" 
														ErrorMessage="Enter New Password " 
														Display="Static">
													*
							</asp:RequiredFieldValidator>
						</td>
					</tr>
					<tr>
						<td style="WIDTH: 177px" align=right>
							<label for=txtConfirmNewPassword>Confirm New Password:</label>
						</td>
						<td style="WIDTH: 500px">
							<asp:textbox id=txtConfirmNewPassword runat="server" width="211px" textmode="Password" height="24px"></asp:textbox>
							<asp:RequiredFieldValidator id=Requiredfieldvalidator3 
														runat="server" 
														ControlToValidate="txtConfirmNewPassword" 
														ErrorMessage="Confirm New Password" Display="Static">
										*
							</asp:RequiredFieldValidator>
							<asp:CompareValidator id=cpvPassword 
													runat="server" 
													ControlToValidate="txtConfirmNewPassword"
													ControlToCompare="txtNewPassword"
													ErrorMessage="Password and Confirm Password must match" 
													Display="Static"  Type="String" Operator="Equal">*</asp:comparevalidator>
						</td>
					</tr>
					<tr>
						<td style="WIDTH: 300px">&nbsp;</td>
						<td style="WIDTH: 500px" align=right>
							<asp:imagebutton id=btnImageButton 
											onmouseover="Swap(this,login2);"
											onmouseout="Swap(this,login1);"
											runat="server" 
											imageurl="Images/submit.jpg" 
											alternatetext="Click here to Change your password." />
						</td>
					</tr>
				</table>
			</asp:panel>
			<asp:label id="lblPasswordChanged" runat="server" Visible="False" />
		</FORM>
	</body>
</html>
