<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ManualAdd.ascx.vb" Inherits="PPC.ManualAdd" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<style>
		#ManualAddBox { WIDTH: 300px; PADDING-TOP: 15px; FONT-FAMILY: Verdana, Geneva, Arial, Helvetica, sans-serif }
		#ManualAddBox H1 { FONT-SIZE: 1.2em; BACKGROUND: navy; MARGIN-BOTTOM: 5px; PADDING-BOTTOM: 0px; COLOR: white }
		#ManualAddBox .errorMessage {
			display: block;
		}
</style>
<script>
	
	function verify(){
		var resp = confirm("Are you sure you want to add this name to the database?");
		if (resp)
			return true;
		return false;
	}
</script>
<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
<div id="ManualAddBox">
	<h1><asp:Label ID="manualAddLabel" runat="server" AssociatedControlID="manualAddName" Text="Manually Add Name to Database" /></h1>	
	<asp:textbox id="manualAddName" runat=server alt="Enter the name you would like to manually enter into the database." />
	<asp:button id="manualAddSubmit" runat=server 
        alt="Submit the name to the database." text="Add" 
        causesvalidation=False />
	<asp:label id="manualAddMessage" runat=server visible="False" cssclass="errorMessage"  />
</div>
