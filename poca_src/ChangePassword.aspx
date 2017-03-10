<%@ Register TagPrefix="uc1" TagName="TrackingHeaderNoNav" Src="TrackingHeaderNoNav.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ChangePassword.aspx.vb" Inherits="PPC.ChangePassword" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
		<title>Change Your Password</title>
		<link href="Styles.css" type="text/css" rel="stylesheet" />		
		<script language="javascript" src="scripts.js" type="text/javascript"></script>
		<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
	</head>
	<body ms_positioning="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<uc1:trackingheadernonav id="TrackingHeader1NoNav" title="Change Password" runat="server" location="<a title='Return to Login Screen' href='default.aspx' style='font-size: 14px; font-weight: bold; '>Return to Login Screen</a>" />
			<asp:panel id="panelChangePassword" runat="server" Height="286px" Width="554px">
				<%--<asp:Label id="lblReturnText" Runat="server" />--%>
				<table  width="561px" 
						cellspacing="5"
						cellpadding="5" 
						align="center"
                        style="left:120px; position:absolute; "
						summary="This table is used for page layout." border="0">
					<tr>
						<td style="width: 177px; height: 28px">&nbsp;</td>
						<td style="width: 500px; height: 28px">
							<asp:label id="lblMustChangeMessage" 
										runat="server" 
										visible="False" 
										cssclass="ErrorMessage">
								You must change your password to sucessfully login to the system. Enter all the
								information into the form below and click Submit to change your password.			
							</asp:label>
							<asp:ValidationSummary id="valSum" 
													runat="server" 
													DisplayMode="BulletList" 
													HeaderText="Please correct the following:" />
							<asp:Label id="lblLoginValidate" 
										runat="server" 
										width="209px" 
										visible="False" 
										forecolor="red" />
						</td>
					</tr>
					<tr>
						<td style="width: 177px;" align="right">
							<p>
								<label for="txtLogin">User ID:</label>								 
							</p>
						</td>
						<td style="width: 500px">
							<asp:textbox id="txtLogin" runat="server" width="211px" />
							<asp:RequiredFieldValidator id="RequiredFieldValidator" 
														runat="server" 
														ControlToValidate="txtLogin" 
														ErrorMessage="Enter User ID" 
														Display="Static">
													*
							</asp:RequiredFieldValidator>
						</td>
					</tr>
					<tr>
						<td style="width: 177px" align="right">
							<label for="txtOldPassword">Old Password:</label>
						</td>
						<td style="width: 500px">
							<asp:textbox id="txtOldPassword" 
										runat="server" 
										width="211px" 
										textmode="Password" 
										height="24px" />
							<asp:RequiredFieldValidator id="Requiredfieldvalidator1" 
														runat="server" 
														ControlToValidate="txtOldPassword" 
														ErrorMessage="Enter Old Password " 
														Display="Static">
													*
							</asp:RequiredFieldValidator>
						</td>
					</tr>
					<tr>
						<td style="width: 177px" align="right">
							<label for="txtNewPassword">New Password:</label>
						</td>
						<td style="width: 500px">
							<asp:textbox id="txtNewPassword" 
										runat="server" 
										width="211px" 
										textmode="Password" 
										height="24px" />
							<asp:RequiredFieldValidator id="Requiredfieldvalidator2" 
														runat="server" 
														ControlToValidate="txtNewPassword" 
														ErrorMessage="Enter New Password " 
														Display="Static">
													*
							</asp:RequiredFieldValidator>
						</td>
					</tr>
					<tr>
						<td style="width: 177px" align="right">
							<label for="txtConfirmNewPassword">Confirm New Password:</label>
						</td>
						<td style="width: 500px">
							<asp:textbox id="txtConfirmNewPassword" runat="server" width="211px" textmode="Password" height="24px"></asp:textbox>
							<asp:RequiredFieldValidator id="Requiredfieldvalidator3" 
														runat="server" 
														ControlToValidate="txtConfirmNewPassword" 
														ErrorMessage="Confirm New Password" Display="Static">
										*
							</asp:RequiredFieldValidator>
							<asp:CompareValidator id="cpvPassword" 
													runat="server" 
													ControlToValidate="txtConfirmNewPassword"
													ControlToCompare="txtNewPassword"
													ErrorMessage="New Password and Confirm New Password must match" 
													Display="Static"  Type="String" Operator="Equal">*</asp:CompareValidator>
						</td>
					</tr>
					<tr>
						<td style="width: 300px">&nbsp;</td>
						<td style="width: 500px" align="right">
							<asp:imagebutton id="btnImageButton" 
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
		</form>
	</body>
</html>
