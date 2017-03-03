<%@ Register TagPrefix="uc1" TagName="NewsControl" Src="NewsControl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Home.aspx.vb" Inherits="PPC.Home" %>
<%@ Register TagPrefix="poca" TagName="SearchBox" Src="SearchBox.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Register TagPrefix="poca" TagName="ManualAdd" Src="~/Controls/ManualAdd.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Home</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<LINK href="Styles.css" type="text/css" rel="stylesheet" />
		<script language="javascript" src="scripts.js"></script>
		<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
	</HEAD>
	<body>	
		<form id="Form1" method="post" runat="server">
			<uc1:trackingheader id="TrackingHeader1" title="" runat="server" location="<a href='StartSearch.aspx' title='New Search'>New Search</a> | <a href='CalenderView.aspx' title='EPD Schedule'>EPD Schedule</a> | <a href='ArchiveSearch.aspx' title='Retrieve Archived Search'>Retrieve Archived Search</a> | <a href='CompletedConsults.aspx' title='View Completed Consults'>View Completed Consults</a>"></uc1:trackingheader>
			<table cellSpacing="0" cellPadding="0" width="100%" summary="This table is used for page layout.">
				<TBODY>
					<tr>
						<td vAlign="top" width="50%">
							<table class="tableFont" id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
								<TBODY>
									<tr>
										<td class="HomeTitles" colSpan="2">Pre-EPD&nbsp;Consults</td>
									</tr>
									<tr>
										<td id="PageContentSkip" colSpan="2"><asp:datagrid id="PreConsults" runat="server" 
                                                AutoGenerateColumns="False" HorizontalAlign="Left"
												CellPadding="3" GridLines="None" AllowSorting="True" UseAccessibleHeader="True">
												<Columns>
													<asp:BoundColumn DataField="consult_number" SortExpression="consult_number" HeaderText="Consult #">
														<HeaderStyle Width="20%" CssClass="HomeColumnHeaders"></HeaderStyle>
														<ItemStyle HorizontalAlign="Center"></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="APP_NUMBER" SortExpression="APP_NUMBER" HeaderText="NDA #">
														<HeaderStyle Width="15%" CssClass="HomeColumnHeaders"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="PRODUCT_NAME" SortExpression="PRODUCT_NAME" HeaderText="Consult Name(s)">
														<HeaderStyle CssClass="HomeColumnHeaders"></HeaderStyle>
													</asp:BoundColumn>
													<asp:HyperLinkColumn DataNavigateUrlField="DT_EPD" DataNavigateUrlFormatString="calenderview.aspx?date={0:d}"
														DataTextField="DT_EPD" SortExpression="DT_EPD" HeaderText="EPD Date" DataTextFormatString="{0:MM-dd-yyyy}">
														<HeaderStyle CssClass="HomeColumnHeaders"></HeaderStyle>
													</asp:HyperLinkColumn>
												</Columns>
											</asp:datagrid><asp:label id="lblPreErrors" Visible="False" Runat="server"></asp:label></td>
									</tr>
								</TBODY>
							</table>
							<p></p>
							<table class="tableFont" id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
								<tr>
									<td class="HomeTitles" colSpan="2">Pre-Marketing 
										Assigned&nbsp;Consults</td>
								</tr>
								<tr>
									<td id="" colSpan="2"><asp:datagrid id="PostGrid" runat="server" 
                                            AutoGenerateColumns="False" HorizontalAlign="Left"
											CellPadding="3" GridLines="None" AllowSorting="True" OnItemDataBound="PostGrid_ItemDataBound" 
                                            UseAccessibleHeader="True">
											<Columns>
												<asp:BoundColumn DataField="consult_number" SortExpression="consult_number" HeaderText="Consult #">
													<HeaderStyle Width="20%" CssClass="HomeColumnHeaders"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="APP_NUMBER" SortExpression="APP_NUMBER" HeaderText="NDA #">
													<HeaderStyle Width="15%" CssClass="HomeColumnHeaders"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="PRODUCT_NAME" SortExpression="PRODUCT_NAME" HeaderText="Consult Name(s)">
													<HeaderStyle CssClass="HomeColumnHeaders"></HeaderStyle>
												</asp:BoundColumn>
												<asp:HyperLinkColumn DataNavigateUrlField="DT_EPD" DataNavigateUrlFormatString="calenderview.aspx?date={0:d}"
													DataTextField="DT_EPD" SortExpression="DT_EPD" HeaderText="EPD Date" DataTextFormatString="{0:MM-dd-yyyy}">
													<HeaderStyle CssClass="HomeColumnHeaders"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:HyperLinkColumn>
												<asp:BoundColumn Visible="False" DataField="ASSIGNED_TO" SortExpression="ASSIGNED_TO" HeaderText="Assigned To">
													<HeaderStyle CssClass="HomeColumnHeaders"></HeaderStyle>
												</asp:BoundColumn>
											</Columns>
										</asp:datagrid><asp:label id="PostLabel" Visible="False" Runat="server"></asp:label></td>
								</tr>
							</table>
						</td>
						<td id="" vAlign="top"><uc1:newscontrol id="NewsControl1" runat="server"></uc1:newscontrol>
                            <poca:searchbox id="HomeSearch" runat="server" AdvancedSearch="True"></poca:searchbox><poca:manualadd id="ManualAdd" runat="server"></poca:manualadd></td>
					</tr>
				</TBODY>
			</table>
		</form>
	</body>
</HTML>
