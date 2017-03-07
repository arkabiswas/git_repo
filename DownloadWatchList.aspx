<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DownloadWatchList.aspx.vb" Inherits="PPC.DownloadWL" EnableViewStateMac="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>DownloadWatchList</title>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link type="text/css" href="styles.css" media="all" >
  </HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="DownloadWL" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" summary="This table is used for page layout.">
				<TBODY>
					<tr>
						<td vAlign="top"><asp:label id="WatchListTitle" runat="server"></asp:label><br>
							<asp:label id="ErrorLabel" runat="server" Visible="False" ForeColor="Red"></asp:label><br>
							<ASP:DATAGRID id="DownloadGrid" 
											runat="server" 
											CellSpacing="0" 
											PageSize="7" 
											Font-Names="Verdana" 
											Font-Size="100%" 
											AutoGenerateColumns="False" 
											CellPadding="2" 
											ForeColor="Black" 
											BorderWidth="1px" 
											Width="580px" 
											BorderStyle="None" ShowFooter="False">
								<columns>
									<asp:TemplateColumn HeaderText="Name of Concern">
										<itemtemplate>
											<%# DataBinder.Eval(Container, "DataItem.name_of_concern") %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Type of Concern">
										<itemtemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.type_of_concern") %>' ID="TypeConcern">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Level of Concern">
										<itemtemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.level_of_concern") %>' ID="LevelConcern">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Datasource">
										<itemtemplate>
											<asp:Label runat="server" Text='<%# PPC.FDA.Data.OracleDataFactory.GetDataSourceName(DataBinder.Eval(Container, "DataItem.data_source")) %>' ID="Datasource">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Narrative">
										<itemtemplate>
											<asp:Label ID="Narrative" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.narrative") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</ASP:DATAGRID>
						</td>
					</tr>
					<tr>
						<td>
							<div id="WatchListComments" runat="server">
								<div class="row">
									<span class="formr">
										<label for="comment">Watchlist Comment:</label> 
										<asp:Label id=WatchListCommentsLabel runat="server"></asp:Label>
									</span>
									<span class="formw">
									</span>
								</div>
							</div>																
						</td>
					</tr>
				</TBODY>
			</table>
		</form>
	</body>
</HTML>
