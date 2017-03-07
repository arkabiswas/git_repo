<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="PPC.FDA.Controls" Assembly="PPC.FDA.Controls.Number" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DynamicWeights.aspx.vb" Inherits="PPC.DynamicWeights" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Dynamic Weights Settings.</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="scripts.js" type="text/javascript"></script>
		<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
		<script language="javascript">
		if(document.images)
		{
			var submit1 = new Image();
			submit1.src = "Images/submit.jpg";
			
			var submit2 = new Image();
			submit2.src = "Images/submit2.jpg";
		}
		</script>
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<uc1:trackingheader id="TrackingHeader1" runat="server" location="<A title='Click here to return to settings' href='Settings.aspx'>Return to Settings</A>" title="Dynamic Weights Settings" />
			<TABLE id="Table1" style="WIDTH: 542px; HEIGHT: 151px" cellSpacing="10" cellPadding="1" width="542" align="center" border="0">
				<TR>
					<TD align="middle" colSpan="2"><BR>
						<asp:label id="MessageLabel" runat="server" Width="227px"></asp:label></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 455px" vAlign="top" align="right"><asp:label id="Label1" AssociatedControlID="PWeight" runat="server" Text="Enter the Phonetic Dynamic Weight:" /></TD>
					<TD vAlign="top" align="left">
						<asp:textbox id="PWeight" Columns="5" Runat="server" MaxLength="4" />%
						<asp:regularexpressionvalidator id="RegPhonetic" Width="155px" Runat="server" Height="6px" ValidationExpression="^\d{0,2}(\.\d{1,3})? *%?$" ControlToValidate="PWeight">Must be between 0 and 100.</asp:regularexpressionvalidator>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 455px" vAlign="top" align="right"><asp:label id="Label2" AssociatedControlID="Oweight" runat="server" Text="Enter the Orthographic Dynamic Weight:" /></TD>
					<TD valign="top" align="left"><asp:textbox id="Oweight" Columns="5" Runat="server" MaxLength="4" />%
						<asp:regularexpressionvalidator id="RegOrthographic" Width="155px" Runat="server" Height="6px" ValidationExpression="^\d{0,2}(\.\d{1,3})? *%?$" ControlToValidate="Oweight">Must be between 0 and 100.</asp:regularexpressionvalidator>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 455px" vAlign="top" align="right"><asp:label id="Label3" AssociatedControlID="Aweight" runat="server" Text="Enter the Additional Factors Dynamic Weight:" /></TD>
					<TD vAlign="top" align="left"><asp:textbox id="Aweight" Columns="5" Runat="server" MaxLength="4"></asp:textbox>%<asp:regularexpressionvalidator id="RegAdditionalFax" Width="155px" Runat="server" Height="6px" ValidationExpression="^\d{0,2}(\.\d{1,3})? *%?$" ControlToValidate="Aweight">Must be between 0 and 100.</asp:regularexpressionvalidator></TD>
				</TR>
				<TR>
					<TD align="right" colSpan="2"><asp:imagebutton id="btnImageButton" onmouseover="Swap(this,submit2);" onmouseout="Swap(this,submit1);" runat="server" alternatetext="Click here to update this setting." imageurl="Images/submit.jpg" CausesValidation="true"></asp:imagebutton></TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
			<!--

			function ValidatorOnSubmit() {
			 if (DynamicWeights(this.Pweight.value, this.Oweight.value, this.Aweight.value)) {
				if (Page_ValidationActive) {
					ValidatorCommonOnSubmit();
				}
			 }
			}
            function __doPostBack(eventTarget, eventArgument) {
                        var theform = document.WebForm1;
                        theform.__EVENTTARGET.value = eventTarget;
                        theform.__EVENTARGUMENT.value = eventArgument;
                       
            }
			// -->
</script>

	</body>
</HTML>
