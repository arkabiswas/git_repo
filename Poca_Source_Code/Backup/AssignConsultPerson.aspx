<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AssignConsultPerson.aspx.vb" Inherits="PPC.AssignConsultPerson" %>
<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
		<title>Assign a Consult</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
  </head>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<uc1:trackingheader id="TrackingHeader1" title="Assign a Consult" runat="server" location="<a href='Settings.aspx' alt='Click here to return to the Settings page'>Return to Settings</a>" />
			<asp:dropdownlist id="UsernameList" style="Z-INDEX: 103; LEFT: 150px; POSITION: absolute; TOP: 141px" runat="server" />
			<asp:label id="Label1" 
                style="Z-INDEX: 102; LEFT: 73px; POSITION: absolute; TOP: 141px" runat="server" 
                AssociatedControlID="UsernameList">Username:</asp:label>
			<asp:label id="ArchiveSearchDescrpt" style="Z-INDEX: 101; LEFT: 72px; POSITION: absolute; TOP: 109px" runat="server" Width="347px">Select the user you would like to assign the consult to.</asp:label>
			<asp:label id="ConsultAssignLabel" style="Z-INDEX: 104; LEFT: 73px; POSITION: absolute; TOP: 169px" runat="server" Width="300px">Consult Selected: </asp:label>
			<asp:button id="AssignButton" style="Z-INDEX: 105; LEFT: 231px; POSITION: absolute; TOP: 195px" runat="server" Text="Assign" />
			<asp:label id="ErrorLabel" style="Z-INDEX: 106; LEFT: 75px; POSITION: absolute; TOP: 237px" runat="server" Width="312px" ForeColor="Red" />
		</form>
	</body>
</html>
