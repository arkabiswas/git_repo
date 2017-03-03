<%@ Register TagPrefix="poca" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DeleteRecordFinish.aspx.vb" Inherits="PPC.DeleteRecordFinish" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Delete Record</title>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="Styles.css" type="text/css" rel="stylesheet">
		<style>
		
			#datasourceWarning { MARGIN: 65px 100px 0px; WIDTH: 400px }
		
			#datasourceWarning IMG { PADDING-RIGHT: 20px; FLOAT: left }
		
			#confirmationButtons { MARGIN-TOP: 10px; TEXT-ALIGN: center }
		
			#confirmationButtons INPUT { WIDTH: 50px; MARGIN-RIGHT: 5px }
		
			#datasourceWarning TABLE { MARGIN-TOP: 10px }
		
		</style>
	</HEAD>
	<body>
		<form id="recordDeletedFinished" method="post" runat="server">
			<poca:trackingheader id="TrackingMenuBar" runat="server" title="Delete a Record" location="<a href='Settings.aspx'>Return to Settings</a>" />
			<div id="datasourceWarning" runat="server">
				The product name '<em><%= drugNameToDisplay %></em>' from the datasource '<em><%= drugDatasourceToDisplay %></em>' 
				has successfully been removed.
				
				<br /><br />
				<asp:Label ID="moveBack" runat="server" text="" />
			</div>
			<asp:label id="errorMessage" cssclass="errorMessage" visible="False" runat="server" />
		</form>
	</body>
</HTML>
