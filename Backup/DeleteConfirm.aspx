<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DeleteConfirm.aspx.vb" Inherits="PPC.DeleteConfirm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Delete Confirmation</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<script language="javascript">
		
			function ConfirmDelete() {
				
				window.opener.document.forms.WatchListForm.custom_action.value = 'delete';
				window.opener.document.forms.WatchListForm.delete_narrative.value = document.forms.Form1.narrative.value;
				window.opener.document.forms[0].submit();
				window.close();
			}
		</script>
		<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
		<form id="Form1" method="post" runat="server">
			<asp:Label id="Label1" style="Z-INDEX: 101; LEFT: 23px; POSITION: absolute; TOP: 20px" runat="server" Width="173px" Height="10px">Please enter a reason for deleting this item.</asp:Label>
			<textarea id="narrative" name="narrative" style="Z-INDEX: 102; LEFT: 21px; WIDTH: 199px; POSITION: absolute; TOP: 58px; HEIGHT: 73px" rows="4" cols="22"></textarea>
			<input type="button" id="BtnSubmit" name="BtnSubmit" value="Delete" onclick="ConfirmDelete()" style="Z-INDEX: 103; LEFT: 22px; POSITION: absolute; TOP: 134px">
		</form>
	</body>
</HTML>
