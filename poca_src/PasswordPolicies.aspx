<%@ Register TagPrefix="poca" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PasswordPolicies.aspx.vb" Inherits="PPC.PasswordPolicies" %>
<!doctype html public "-//w3c//dtd html 4.0 transitional//en">
<html>
 <head>
		<title>Password Policies</title>
		<link href="Styles.css" type="text/css" rel="stylesheet" />
		<script language="javascript" src="scripts.js" />
		<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
		</script>
		<style type="text/css">
			#passwordpolicies { MARGIN-TOP: 50px; LEFT: 120px; MARGIN-LEFT: 0px; WIDTH: 700px; POSITION: absolute }
			.updatebutton { FLOAT: right; margin-right:250px; }
			#passwordpoliciesUL { LIST-STYLE-TYPE: none }
			#passwordpolicies UL LI INPUT { MARGIN-RIGHT: 10px }
		</style>
</head>
	<body>
		<form id="frmPassword" method="post" runat="server">
			<poca:trackingheader id="TrackingHeader1" 
								 title="Adjust Password Policies" 
								 runat="server" 
								 location="<a href='Settings.aspx' alt='Click here to return to the Settings page' style='font-size: 14px; font-weight: bold;'>Return to Settings</a>" />
			<div id="passwordpolicies">
				<ul id="passwordpoliciesUL">
					<li>
						<asp:Textbox id="pwdSpecialCharacters" runat="server" Columns="2" MaxLength="2" />
                        <asp:Label id="Label1" AssociatedControlID="pwdSpecialCharacters" runat="server" Text=" Number of required special characters (!@#$%^&amp;*)." />
						<asp:Requiredfieldvalidator runat="server" id="vldSpecial" ControlToValidate="pwdSpecialCharacters" ErrorMessage="You must enter a number of required special characters." Display="Dynamic">*</asp:Requiredfieldvalidator>
						<asp:Rangevalidator runat="server" id="vldSpecialCount" ControlToValidate="pwdSpecialCharacters" ErrorMessage="You can only have between 0 to 8 special characters." MinimumValue="0" MaximumValue="7" Type="Integer" Display="Dynamic">*</asp:Rangevalidator>
                    </li>
					<li>
						<asp:TextBox id="pwdMinimumLength" runat="server" Columns="2" MaxLength="2" />
                        <asp:Label id="Label2" AssociatedControlID="pwdMinimumLength" runat="server" Text=" Minimum length of password." />
						<asp:Requiredfieldvalidator runat="server" id="vldMinLength" ControlToValidate="pwdMinimumLength" ErrorMessage="You must enter a minimum length of passwords." Display="Dynamic">*</asp:Requiredfieldvalidator>
						<asp:Rangevalidator runat="server" id="vldMinLengthCount" ControlToValidate="pwdMinimumLength" ErrorMessage="You can only have between 6 to 12 minimum length of passwords." MinimumValue="6" MaximumValue="12" Type="Integer" Display="Dynamic">*</asp:Rangevalidator>
					</li>
                    <li>
						<asp:TextBox id="pwdExpireDays" runat="server" Columns="2" MaxLength="4" />
                        <asp:Label id="Label3" AssociatedControlID="pwdExpireDays" runat="server" Text=" Number of days until passwords are required to change." />
						<asp:Requiredfieldvalidator runat="server" id="vldExpire" ControlToValidate="pwdExpireDays" ErrorMessage="You must enter a number of days until passwords are required to change." Display="Dynamic">*</asp:Requiredfieldvalidator>
						<asp:Rangevalidator runat="server" id="vldExpireCount" ControlToValidate="pwdExpireDays" ErrorMessage="You can only have between 90 to 365 number of days until passwords are required to change." MinimumValue="90" MaximumValue="365" Type="Integer" Display="Dynamic">*</asp:Rangevalidator>
					</li>
                    <li>
						<asp:TextBox id="pwdMinExpireDays" runat="server" Columns="2" MaxLength="2" />
                        <asp:Label id="Label4" AssociatedControlID="pwdMinExpireDays" runat="server" Text=" Minimum number of days before password can be changed." />
						<asp:Requiredfieldvalidator runat="server" id="vldMinExpire" ControlToValidate="pwdMinExpireDays" ErrorMessage="You must enter a minimum number of days before password can be changed." Display="Dynamic">*</asp:Requiredfieldvalidator>
						<asp:Rangevalidator runat="server" id="vldMinExpireCount" ControlToValidate="pwdMinExpireDays" ErrorMessage="You can only have between 0 to 99 minimum number of days before password can be changed." MinimumValue="0" MaximumValue="99" Type="Integer" Display="Dynamic">*</asp:Rangevalidator>
					</li>
                    <li>
						<asp:TextBox id="pwdLoginAttempts" runat="server" Columns="2" MaxLength="2" />
                        <asp:label id="Label5" AssociatedControlID="pwdLoginAttempts" runat="server" Text=" Number of allowed login attempts." />
						<asp:Requiredfieldvalidator runat="server" id="vldLoginAttempts" ControlToValidate="pwdLoginAttempts" ErrorMessage="You must enter a number of allowed login attempts." Display="Dynamic">*</asp:Requiredfieldvalidator>
						<asp:Rangevalidator runat="server" id="vldLoginAttemptsCount" ControlToValidate="pwdLoginAttempts" ErrorMessage="You can only have between 3 to 10 number of allowed login attempts." MinimumValue="3" MaximumValue="10" Type="Integer" Display="Dynamic">*</asp:Rangevalidator>
					</li>
                    <li>
						<asp:TextBox id="pwdHistoryLength" runat="server" Columns="2" MaxLength="2" />
                        <asp:Label id="Label6" AssociatedControlID="pwdHistoryLength" runat="server" Text=" Number of changed passwords to be kept in history." />
						<asp:Requiredfieldvalidator runat="server" id="vldHistoryLength" ControlToValidate="pwdHistoryLength" ErrorMessage="You must enter a number of changed passwords to be kept in history." Display="Dynamic">*</asp:Requiredfieldvalidator>
						<asp:Rangevalidator runat="server" id="vldHistoryLengthCheck" ControlToValidate="pwdHistoryLength" ErrorMessage="You can only have between 3 to 10 number of changed passwords to be kept in history." MinimumValue="3" MaximumValue="10" Type="Integer" Display="Dynamic">*</asp:Rangevalidator>
					</li>
				</ul>
                <br />
				<asp:imagebutton id="btnImageButton" 
								 onmouseover="Swap(this,submit2);" 
								 onmouseout="Swap(this,submit1);" 
								 runat="server" 
								 alternatetext="Click here to update the password policies." 
								 imageurl="Images/submit.jpg" 
								 cssclass="updatebutton"
								 causesvalidation="true" />

				<asp:label id="lblErrorMessages" runat="server" visible="false" cssclass="errorMessage" />										
				<asp:validationsummary id="vldPasswordPolicies" runat="server" 
                                       HeaderText="You must enter in the following items to proceed." 
                                       style="margin-top: 30px; margin-left: 40px;" />
			</div>
		</form>
	</body>
</html>
