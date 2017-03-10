<%@ Control Language="vb" AutoEventWireup="false" Codebehind="SearchBox.ascx.vb" Inherits="PPC.SearchBox" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<script language="javascript" src="scripts.js" type="text/javascript"></script>
<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
<style>.SearchBox {
	FONT: 10px Verdana, Geneva, Arial, Helvetica, sans-serif; WIDTH: 310px
}
#SearchTable {
	WIDTH: 300px
}
#SearchTable TD {
	VERTICAL-ALIGN: top
}
#SearchTable TH {
	BACKGROUND: navy; COLOR: white; TEXT-ALIGN: left
}
#SearchAdvanced {
	PADDING-BOTTOM: 5px
}
#SearchAdvanced H1 {
	FONT-SIZE: 12px; MARGIN-BOTTOM: 5px
}
#SearchAdvanced DIV {
	PADDING-LEFT: 10px
}
#LanguageText {
	PADDING-RIGHT: 2em; FLOAT: left; POSITION: relative
}
#LanguageSelect {
	FLOAT: left; POSITION: relative
}
.info {
	FLOAT: left; FONT-STYLE: italic; POSITION: relative
}
.aster {
	PADDING-RIGHT: 2px; FLOAT: left; COLOR: blue; POSITION: relative
}
.DatasourcesTable {
	BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px
}
.DatasourcesTable TD {
	PADDING-LEFT: 10px;
	PADDING-RIGHT: 10px;
}
.DatasourcesTable TH {
	PADDING-LEFT: 10px;
	PADDING-RIGHT: 10px;
	Font-Size:12px;
	font-weight: bold;
	text-align: left;
	background-color: navy;
	color: white;
}
</style>


				
				
				
<div class="SearchBox">
	<table id="SearchTable">
		<tr>
			<th colSpan="2">
				<asp:label id="lblTitle" Runat="server">Search</asp:label></th></tr>
		<tr>
			<td colSpan="2"><asp:requiredfieldvalidator id="rfvSearchText" controltovalidate="SearchText" runat="server" ErrorMessage="Please enter a value in the text box."
					display="Dynamic"></asp:requiredfieldvalidator><asp:label id="lblValidate" runat="server" ForeColor="Red" Visible="False">
					Please check at least one search type.							
				</asp:label></td>
		</tr>
		<%----------------------------%>
		<tr>
			<td></td>
			<td>			
				<div class="aster">*</div>
				<div class="info">% For wildcard searching
					<br>
					_ For single character searching
				</div>				
			</td>
		</tr>		
		<%-----------------------------%>
		<tr>
			<td id="PageContentSkip"><!--Changed to server control for 508 <label title="Search Text" for="SearchText">Search Text:</label> -->
			<asp:Label ID="lblSearch" runat="server" AssociatedControlID="SearchText" Text="Search Text"/></td>
			<td><asp:textbox id="SearchText" runat="server" MaxLength="100" Columns="20"></asp:textbox></td>
		</tr>
		<tr>
			<td>Search Type:
			</td>			
			<td>
		            <asp:checkbox id="chkOrthographic" runat="server" Enabled="True" Checked="True"></asp:checkbox><asp:Label ID="Label1" runat="server" AssociatedControlID="chkOrthographic" Text="Orthographic" /><br>
			        <asp:checkbox id="chkPhonetic" runat="server"></asp:checkbox><asp:Label ID="Label2" runat="server" AssociatedControlID="chkPhonetic" Text="Phonetic" /><br>
			        <asp:checkbox id="chkText" runat="server"></asp:checkbox><asp:Label ID="Label3" runat="server" AssociatedControlID="chkText" Text="Text" /><span style="COLOR: blue">*</span><br>
			</td>			
		</tr>
		<tr>
			<td align="right" colSpan="2"><IMG onmouseover="Swap(this, reset2);" onclick="document.forms[0].reset();" onmouseout="Swap(this, reset1);"
					alt="Clear Search" src="images/reset.jpg" border="0">
				<asp:imagebutton id="SearchSubmit" onmouseover="Swap(this, submit2);" onmouseout="Swap(this, submit1);"
					runat="server" imageurl="images/submit.jpg" alternatetext="Submit search"></asp:imagebutton></td>
		</tr>
	</table>
	<asp:panel id="PNLAdvancedDetails" runat="server" Visible="False">
		<DIV id="SearchAdvanced">
			<H1>Other Datasources:</H1>
			<DIV>&nbsp;</DIV>
			<DIV>
				<asp:datagrid id="dgDatasources" runat="server" Visible="True" AutoGenerateColumns="False" UseAccessibleHeader="True"
					CssClass="DatasourcesTable" GridLines="None">
					<ItemStyle BorderStyle="None"></ItemStyle>
					<HeaderStyle Font-Bold="True"></HeaderStyle>
					<Columns>
						<asp:TemplateColumn>
							<HeaderStyle Wrap="False"></HeaderStyle>
							<ItemStyle Wrap="False"></ItemStyle>
							<ItemTemplate>
								<asp:CheckBox id="cbSearch" runat="server"></asp:CheckBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn Visible="False" DataField="PICK_LIST_ITEM_ID" HeaderText="Pick List ID">
							<HeaderStyle Wrap="False"></HeaderStyle>
							<ItemStyle Wrap="False"></ItemStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn>
							<HeaderStyle Wrap="False"></HeaderStyle>
							<ItemStyle Wrap="False"></ItemStyle>
							<HeaderTemplate>
							    <asp:label id="lblDesc" Text="Datasource" runat="server"></asp:label>
							</HeaderTemplate>
							<ItemTemplate>
								<asp:label id="lblDesc" AssociatedControlID="cbSearch" Text='<%# DataBinder.Eval(Container.DataItem, "DESCRIPTION") %>' runat="server"></asp:label>
							</ItemTemplate>
						</asp:TemplateColumn>						
<%--						<asp:BoundColumn DataField="DESCRIPTION" HeaderText="Datasource">
							<HeaderStyle Wrap="False"></HeaderStyle>
							<ItemStyle Wrap="False"></ItemStyle>
						</asp:BoundColumn>--%>
						<asp:BoundColumn DataField="DATA_SOURCE_UPDATED" HeaderText="Date Updated" DataFormatString="{0:MM-dd-yyyy}">
							<HeaderStyle Wrap="False"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="DATA_SOURCE_RECORDS" HeaderText="Product Count" DataFormatString="{0:#,#}">
							<HeaderStyle Wrap="False"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn>
							<HeaderStyle Wrap="False"></HeaderStyle>
							<ItemStyle Wrap="False"></ItemStyle>
							<ItemTemplate>
                                <a href="#" onclick="window.open('<%#String.Format("DatasourceDetail.aspx?DSName={0}&DSDesc={1}",DataBinder.Eval(Container,"DataItem.description"),DataBinder.Eval(Container,"DataItem.detail_description"))%>', 'DatasourceDetails', 'scrollbars=yes,resizable=yes,toolbars=no,status=no,height=300,width=500')">
                                    <img border="0" alt"More Information" src="Images/Information_20.png" /></a>
                                <%--<a href="#" onclick="window.open('<%# DataBinder.Eval(Container, "DataItem.name_of_concern", "ProductDetail.aspx?PrdName={0}") %>', 'ProductDetails', 'toolbars=no,status=no,height=400,width=500')">More Info</a>--%>
							</ItemTemplate>
						</asp:TemplateColumn>						
					</Columns>
				</asp:datagrid></DIV>
		</DIV> <!---
		<DIV id="LanguageText" runat="server"><LABEL title="Proposed Name" for="ProposedNameLanguage">Proposed 
				Name:</LABEL>
			<asp:DropDownList id="ProposedNameLanguage" Runat="server">
				<asp:ListItem Selected="False" Value="English">Standard English</asp:ListItem>
				<asp:ListItem Value="Spanish">Spanish Accent</asp:ListItem>
			</asp:DropDownList></DIV>
		<DIV id="LanguageSelect"><LABEL title="Database Name" for="database">Database Name:</LABEL>
			<BR>
			<asp:DropDownList id="DatabaseNameLanguage" Runat="server">
				<asp:ListItem Selected="False" Value="English">Standard English</asp:ListItem>
				<asp:ListItem Value="Spanish">Spanish Accent</asp:ListItem>
			</asp:DropDownList>
		</DIV>
		---></asp:panel></div>
<DIV>&nbsp;</DIV>
