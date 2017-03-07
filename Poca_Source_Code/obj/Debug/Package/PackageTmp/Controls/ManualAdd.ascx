<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ManualAdd.ascx.vb" Inherits="PPC.ManualAdd" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<style type="text/css">
		#ManualAddBox { WIDTH: 340px; PADDING-TOP: 15px; font-size: 14px; FONT-FAMILY: Verdana, Geneva, Arial, Helvetica, sans-serif }
		#ManualAddBox H1 { FONT-SIZE: 1.2em; BACKGROUND: navy; MARGIN-BOTTOM: 15px; PADDING-BOTTOM: 0px; COLOR: white }
		#ManualAddBox .errorMessage {
			display: block;
		}
</style>
<script type="text/javascript">
    if (typeof String.prototype.trim !== 'function') {
        String.prototype.trim = function () {
            return this.replace(/^\s+|\s+$/g, '');
        }
    }

    function verify() {
        var nameToAdd = document.getElementById("ManualAdd_manualAddName").value;
        //alert(nameToAdd);
        var nameToAddStr = nameToAdd.trim();
        //alert(nameToAddStr);
        //if (nameToAdd.trim().length == 0) {
        if (nameToAddStr == "") {
            document.getElementById("ManualAdd_manualAddMessage").innerText = "Please add drug name to add.";
            return false;
        }
		var resp = confirm("Are you sure you want to add this name to the database?");
		if (resp)
			return true;
		return false;
	}
</script>
<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
<div id="ManualAddBox">
	<h1><asp:Label ID="manualAddLabel" runat="server" AssociatedControlID="manualAddName" Text="Manually Add Name to Database" /></h1>	
	<asp:textbox id="manualAddName" runat="server" alt="Enter the name you would like to manually enter into the database." />
    &nbsp;&nbsp;
	<asp:button id="manualAddSubmit" runat="server" 
        alt="Submit the name to the database." text="Add" 
        causesvalidation="false" style="padding: 2px 15px;"/>
	<asp:label id="manualAddMessage" runat="server" visible="True" cssclass="errorMessage"  />
   	<br/>
</div>
