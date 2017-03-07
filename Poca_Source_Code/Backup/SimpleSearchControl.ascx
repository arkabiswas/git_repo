<%@ Control Language="vb" AutoEventWireup="false" Codebehind="SimpleSearchControl.ascx.vb" Inherits="PPC.SimpleSearchControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<LINK href="SimpleSearchControl.css" type="text/css" rel="stylesheet">
<script language="javascript">
	if(document.images)
	{
		var submit1 = new Image();
		submit1.src = "Images/submit.jpg";
		
		var submit2 = new Image();
		submit2.src = "Images/submit2.jpg";
		
		var reset1 = new Image();
		reset1.src = "Images/reset.jpg";
		
		var reset2 = new Image();
		reset2.src = "Images/reset2.jpg";
	}
	
	function ClearForm()
	{
		document.forms[0]['SimpleSearchControl1:SearchText'].value = '';
		document.forms[0]['SimpleSearchControl1:chkOrthographic'].checked = true;
		document.forms[0]['SimpleSearchControl1:chkPhonetic'].checked = false;
		document.forms[0]['SimpleSearchControl1:chkText'].checked = false;
	
	}
</script>
<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
<script language="javascript" src="scripts.js" type="text/javascript"></script>
<table class="Border" width="300">
	<tr>
		<td>
			<table class="gContentSection" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr class="SearchHeader">
					<td vAlign="top" colSpan="2"><asp:label id="lblTitle" Runat="server">Quick Search</asp:label></td>
				</tr>
				<tr>
					<td colSpan="2"><asp:requiredfieldvalidator id="rfvSearchText" controltovalidate="SearchText" runat="server" ErrorMessage="Please enter a value in the text box." display="Dynamic"></asp:requiredfieldvalidator><asp:label id="lblValidate" runat="server" Visible="False" ForeColor="Red">
							Please check at least one search type.							
						</asp:label></td>
				</tr>
				<tr>
					<td style="WIDTH: 80px" vAlign="top">Search Text yada:&nbsp;</td>
					<td><asp:textbox id="SearchText" runat="server"></asp:textbox></td>
				</tr>
				<tr>
				<tr>
					<td style="WIDTH: 80px" vAlign="top">Search Type:&nbsp;</td>
					<td><asp:checkbox id="chkOrthographic" runat="server" Enabled="True" Checked="True"></asp:checkbox>Orthographic<br>
						<asp:checkbox id="chkPhonetic" runat="server"></asp:checkbox>Phonetic<br>
						<asp:checkbox id="chkText" runat="server"></asp:checkbox>Text <FONT color="blue">*</FONT>&nbsp;
						<BR>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <FONT color="blue">*</FONT><EM><FONT size="1">% 
								For wildcard searching<BR>
								&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
								_ For single character searching<BR>
							</FONT></EM>
					</td>
				</tr>
				<tr>
					<td align="right" colSpan="2"><BR>
						<A onclick="ClearForm();" href="#"><IMG onmouseover="Swap(this, reset2);" onmouseout="Swap(this, reset1);" alt="Clear Search" src="Images/reset.jpg" border="0"></A>
						<asp:imagebutton id="ImageButton1" onmouseover="Swap(this, submit2);" onmouseout="Swap(this, submit1);" runat="server" imageurl="images/submit.jpg" alternatetext="Submit search"></asp:imagebutton></td>
				</tr>
			</table>
			<asp:panel id="PNLAdvancedDetails" runat="server" Visible="False" Width="293px" Height="65px">Other Datasources: <BR>
<asp:CheckBoxList id="OtherDatasources" Runat="server"></asp:CheckBoxList>
</asp:panel>
		</td>
	</tr>
</table>
<br>
