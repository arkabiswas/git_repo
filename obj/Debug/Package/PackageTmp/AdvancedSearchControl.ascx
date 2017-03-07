<%@ Control Language="vb" AutoEventWireup="false" Codebehind="AdvancedSearchControl.ascx.vb" Inherits="PPC.AdvancedSearchControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<script language="javascript">
	if(document.images)
	{
		var submit1 = new Image();
		submit1.src = "Images/submit.jpg";
		
		var submit2 = new Image();
		submit2.src = "Images/submit2.jpg";
	}
</script>
<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
<script language="javascript" src="scripts.js" type="text/javascript"></script>
<LINK href="SimpleSearchControl.css" type="text/css" rel="stylesheet">
<P>
	<table class="gContentSection" cellSpacing="0" cellPadding="0" border="0">
		<tr class="SearchHeader">
			<td colSpan="2">Advanced Search</td>
		</tr>
		<tr>
			<td colSpan="2"><asp:RequiredFieldValidator controltovalidate="SearchText" id="rfvSearchText" runat="server" ErrorMessage="Please enter a value in the text box." display="Dynamic"></asp:RequiredFieldValidator><asp:Label id="lblValidate" runat="server" Visible="False" ForeColor="Red">Please check at least one search type.</asp:Label></td>
		</tr>
		<tr>
			<td style="WIDTH: 80px" noWrap>Search Text:&nbsp;</td>
			<td><asp:TextBox id="SearchText" runat="server" /></td>
		</tr>
		<tr>
		<tr>
			<td style="WIDTH: 80px" noWrap>Search Type:&nbsp;</td>
			<td><asp:CheckBox id="chkOrthographic" runat="server" Enabled="True" />Orthographic<br>
				<asp:CheckBox id="chkPhonetic" runat="server" Checked="True" />Phonetic<br>
				<asp:CheckBox id="chkText" runat="server" Enabled="False" />Text</td>
		</tr>
		<tr>
			<td colspan="2" align="right"><asp:ImageButton imageurl="images/submit.jpg" id="ImageButton1" runat="server" alternatetext="Submit search" onmouseover="Swap(this, submit2);" onmouseout="Swap(this, submit1);"></asp:ImageButton></td>
		</tr>
	</table>
</P>
