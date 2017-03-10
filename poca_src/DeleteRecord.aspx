<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DeleteRecord.aspx.vb" Inherits="PPC.DeleteRecord" %>
<%@ Register TagPrefix="poca" Namespace="MetaBuilders.WebControls" Assembly="MetaBuilders.WebControls.RowSelectorColumn" %>
<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>Delete A Record</title>
	<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
	<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		
	<link href="Styles.css" type="text/css" rel="stylesheet" />
	<style type="text/css">
		#selectDatasourceTable { MARGIN: 55px 100px 0px }
		#selectDatasourceTable TABLE TH { FONT-SIZE: 12px }
		.titleRow { FONT-WEIGHT: bold; TEXT-DECORATION: underline }
		#btnSearch { FLOAT: right; PADDING-TOP: 10px }
		.info { FONT-SIZE: 10px; FONT-STYLE: italic; POSITION: relative }
		.aster { PADDING-RIGHT: 2px; COLOR: blue; POSITION: relative }
    </style>
</head>
  <body>

    <form id="Form1" method="post" runat="server">
		<uc1:TrackingHeader id="TrackingHeader1" runat="server" title="Delete a Record" location="<a href='Settings.aspx' style='font-size: 14px; font-weight: bold; '>Return to Settings</a>"></uc1:TrackingHeader>

		<div id="selectDatasourceTable">
			<table>
				<thead>
					<tr>
						<th colspan="3" style="TEXT-ALIGN:left">
						Search for the record you would like to delete 
						using the search box below. Once you have found the record you are looking for, select 
						it by clicking the circle next to the name and then clicking the next button.
						</th>
					</tr>
				</thead>
				<tr>
					<td colspan="3">
						<asp:label id="Label1" runat="server" Text="Search:	" AssociatedControlID="searchTerm" /><asp:textbox id="searchTerm" runat="server" />&nbsp;<asp:button id="searchSubmitButton" runat="server" text="Search" />
						<br/>
						<span class="info">(
							Use % for wildcard searching or 
							_ for single character searching )
						</span>
						<asp:label id="errorMessage" visible="False" runat="server" cssclass="errorMessage" />
					</td>
				</tr>
			</table>
			<asp:datagrid id="searchResults" 
						  runat="server" 
						  datakeyfield="product_id" 
						  autogeneratecolumns="False"
					      gridlines="None" showfooter="True">
				<alternatingitemstyle backcolor="#ffffff" />
				<itemstyle backcolor="#eeeeee" />
				<headerstyle cssclass="titleRow" />
				
				<columns>				
					<%--<poca:RowSelectorColumn SelectionMode="Single" headertext=" " itemstyle-width="2%" />--%>
					<%--<asp:boundcolumn datafield="u_name" headertext="Name" itemstyle-width="49%" />--%>
					<asp:TemplateColumn ItemStyle-Width="2%" HeaderText=" ">
					    <ItemTemplate>
					    <asp:RadioButton ID="rdoData" runat="server" OnCheckedChanged="rdoDataCheckedChanged" AutoPostBack="true" />
					    </ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Name" ItemStyle-Width="49%" >
					    <ItemTemplate>
					    <asp:Label ID="lblDescription" runat="server" AssociatedControlID="rdoData" Text='<%# Eval("u_name") %>'/>
					    </ItemTemplate>
					</asp:TemplateColumn>			
			
					<asp:templatecolumn headertext="Datasource" itemstyle-width="49%" >
						<itemtemplate>
						<%# PPC.FDA.Data.OracleDataFactory.GetDataSourceName(DataBinder.Eval(Container.DataItem, "data_source")) %>
						</itemtemplate>
						<footertemplate>
			                <div style="FLOAT: right; PADDING-TOP: 10px">
				                <asp:button id="buttonDelete" runat="server" text="Delete" onclick="ConfirmDelete" />
			                </div>										
						</footertemplate>
					</asp:templatecolumn>					
				</columns>
			</asp:datagrid>			
		</div>		
    </form>

  </body>
</html>
