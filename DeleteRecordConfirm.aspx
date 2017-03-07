<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DeleteRecordConfirm.aspx.vb" Inherits="PPC.DeleteRecordConfirm" %>
<%@ Register TagPrefix="poca" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>Confirm Record Deletion</title>
		<link href="Styles.css" type="text/css" rel="stylesheet">    
		<meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
		<style>
		
			#datasourceWarningPage { MARGIN: 65px 100px 0px; WIDTH: 400px }		
			#datasourceWarningPage IMG { PADDING-RIGHT: 30px; FLOAT: left }		
			#confirmationButtons { MARGIN-TOP: 10px; TEXT-ALIGN: center }		
			#confirmationButtons INPUT { WIDTH: 50px; MARGIN-RIGHT: 5px }		
			#datasourceWarningPage TABLE { MARGIN-TOP: 10px }
			
		</style>
	</head>
	<body>
		<form id="confirmDeleteForm" method="post" runat="server">
			<poca:trackingheader id="TrackingMenuBar" 
								runat="server" 
								title="Delete a Record" 
								location="<a href='Settings.aspx' style='font-size: 14px; font-weight: bold; '>Return to Settings</a>" />
								 
			<div id="datasourceWarningPage">
				<img src="Images/exclamation.jpg" alt="Exclamation" height="66" width="62">
				<asp:label id="errorMessage" runat="server" visible="False" cssclass="errorMessage" />
				<asp:label id="datasourceWarning" runat="server" />
				<asp:repeater id="listOfWatchlists" runat="server" visible="False">					
					<headertemplate>
				<table>
					<thead>
						<td>
							List of watchlists that '<em><%= drugNameToDisplay %></em>' appears on.
						</td>					
					</thead>					
					</headertemplate>					
					<itemtemplate>
						<tr>
							<td><%# DataBinder.Eval(Container, "DataItem.name") %></td>
						</tr>
					</itemtemplate>
					<footertemplate>
				</table>
					</footertemplate>
				</asp:repeater>
				<div id="confirmationButtons">
					<asp:button id="confirmYesButton" runat="server" text="Yes" onclick="confirmYesButton_Click" />
					<asp:button id="confirmNoButton" runat="server" text="No" onclick="confirmNoButton_Click" />
				</div>											
			</div>	
		</form>
	</body>
</html>
