<%@ Register TagPrefix="poca" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PasswordPolicies.aspx.vb" Inherits="PPC.PasswordPolicies" %>
<!doctype html public "-//w3c//dtd html 4.0 transitional//en">
<html>
  <head>
		<title>Password Policies</title>
		<LINK href="Styles.css" type="text/css" rel="stylesheet" />
		<script language=javascript src="scripts.js" />
		<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
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
		<style>
			#passwordpolicies { MARGIN-TOP: 50px; LEFT: 50%; MARGIN-LEFT: -166px; WIDTH: 450px; POSITION: absolute }
			.updatebutton { FLOAT: right }
			#passwordpolicies UL { LIST-STYLE-TYPE: none }
			#passwordpolicies UL LI INPUT { MARGIN-RIGHT: 10px }
			</style>
</head>
	<body>
		<form id=frmPassword method=post runat="server">
			<poca:trackingheader id=TrackingHeader1 
								 title="Adjust Password Policies" 
								 runat="server" 
								 location="<a href='Settings.aspx' alt='Click here to return to the Settings page'>Return to Settings</a>" />								 
			<div id=passwordpolicies>
				<asp:validationsummary id="vldPassword" runat=server displaymode=BulletList showsummary=True />
				<asp:label id="lblErrorMessages" runat=server visible=False cssclass="errorMessage" />										
				<ul>
					<li>
						<asp:textbox id=pwdSpecialCharacters runat="server" columns="2" maxlength="2" /><asp:label id="Label1" AssociatedControlID="pwdSpecialCharacters" runat=server text=" Number of required special characters (!@#$%^&amp;*)." />
						<asp:requiredfieldvalidator runat=server id="vldSpecial" controltovalidate="pwdSpecialCharacters" errormessage="You must enter a number of required special characters." display=dynamic>*</asp:requiredfieldvalidator>
						<asp:rangevalidator runat=server id="vldSpecialCount" controltovalidate="pwdSpecialCharacters" errormessage="You can only have between 0 to 8 special characters." minimumvalue="0" maximumvalue="7" display="Dynamic">*</asp:rangevalidator>
					<li>
						<asp:textbox id=pwdMinimumLength runat="server" columns="2" maxlength="2" /><asp:label id="Label2" AssociatedControlID="pwdMinimumLength" runat=server text=" Minimum length of password." />
						<asp:requiredfieldvalidator runat=server id="vldMinLength" controltovalidate="pwdMinimumLength" errormessage="You must enter a minimum length of passwords." display=dynamic>*</asp:requiredfieldvalidator>
					<li>
						<asp:textbox id=pwdExpireDays runat="server" columns="2" maxlength="4" /><asp:label id="Label3" AssociatedControlID="pwdExpireDays" runat=server text=" Number of days until passwords are required to change." />
						<asp:requiredfieldvalidator runat=server id="vldExpire" controltovalidate="pwdExpireDays" errormessage="You must enter a number of days until passwords are required to change." display=dynamic>*</asp:requiredfieldvalidator>
					<li>
						<asp:textbox id=pwdMinExpireDays runat="server" columns="2" maxlength="2" /><asp:label id="Label4" AssociatedControlID="pwdMinExpireDays" runat=server text=" Minimum number of days before password can be changed." />
						<asp:requiredfieldvalidator runat=server id="vldMinExpire" controltovalidate="pwdMinExpireDays" errormessage="You must enter a minimum of number of days before the password can be changed." display=dynamic>*</asp:requiredfieldvalidator>
					<li>
						<asp:textbox id=pwdLoginAttempts runat="server" columns="2" maxlength="2" /><asp:label id="Label5" AssociatedControlID="pwdLoginAttempts" runat=server text=" Number of allowed login attempts." />
						<asp:requiredfieldvalidator runat=server id="vldLoginAttempts" controltovalidate="pwdLoginAttempts" errormessage="You must enter a number of allowed login attempts." display=dynamic>*</asp:requiredfieldvalidator>
					<li>
						<asp:textbox id=pwdHistoryLength runat="server" columns="2" maxlength="2" /><asp:label id="Label6" AssociatedControlID="pwdHistoryLength" runat=server text=" Number of changed passwords to be kept in history." />
						<asp:requiredfieldvalidator runat=server id="vldHistoryLength" controltovalidate="pwdHistoryLength" errormessage="You must enter the number of passwords kept in history." display=dynamic>*</asp:requiredfieldvalidator>
					</li>
				</ul>
				<asp:imagebutton id=btnImageButton 
								 onmouseover=Swap(this,submit2); 
								 onmouseout=Swap(this,submit1); 
								 runat="server" 
								 alternatetext="Click here to update the password policies." 
								 imageurl="Images/submit.jpg" 
								 cssclass="updatebutton"
								 causesvalidation=True />
			</div>
		</form>
	</body>
</html>
