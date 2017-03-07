<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="PPC.FDA.Controls" Assembly="PPC.FDA.Controls.FDAGrid" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="WatchList.aspx.vb" Inherits="PPC.WatchList" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Home</title>
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<style>
		
			#WatchListComments {
				width: 500px;
				padding: 10px 0 0 0;
				margin: 10px;
				}
			
			#WatchListComments .row {
				clear: both;
			}
			
			#WatchListComments .formr{
				float: left;	
				width: 150px;		
			}
			#WatchListComments .formw {
				float: right;
			}
			#WatchListComments .errorMessage {
				display: block;
				float: left;
			}
		</style>
	</HEAD>
	<body>
		<form id="WatchListForm" method="post" runat="server">
			<uc1:trackingheader id="Trackingheader2" title="" runat="server" location="<a href='StartSearch.aspx' title='New Search'>New Search</a> | <a href='CalenderView.aspx' title='EPD Schedule'>EPD Schedule</a> | <a href='ArchiveSearch.aspx' title='Retrieve Archived Search'>Retrieve Archived Search</a>"></uc1:trackingheader>
			<input id="custom_action" type="hidden" name="custom_action"> <input id="delete_narrative" type="hidden" name="delete_narrative">
			<table cellSpacing="0" cellPadding="0" width="100%" summary="This table is used for page layout.">
				<TBODY>
					<tr>
						<td vAlign="top"><asp:label id="WatchListTitle" runat="server"></asp:label><br>
							<asp:dropdownlist id="DDLWatchLists" AutoPostBack="True" Runat="server"></asp:dropdownlist><br>
							<br />
							<asp:label id="ErrorLabel" runat="server" Visible="False" />
							<br />
							<asp:LinkButton ID="PrintScreen" Runat=server>Print</asp:LinkButton>
							<asp:Label ID="SeperatorPipe" Runat=server> | </asp:Label>
							<asp:LinkButton ID="DownloadScreen" Runat=server>Download</asp:LinkButton>
							<asp:Label Visible=False ID="WatchlistType" Runat=server Text="Individual" />
							<cc1:fdagrid id="FDAGrid1" runat="server" BorderColor="Black" ShowFooter="True" BorderStyle="None" Width="60%" BorderWidth="1px" ForeColor="Black" IsSortedAscending="True" AllowSorting="True" CellPadding="2" OnEditCommand="Grid_Edit" OnUpdateCommand="Grid_Update" AutoGenerateColumns="False" GridLines="Horizontal" Font-Size="120%" Font-Names="Verdana" AllowPaging="False" PageSize="7" CellSpacing="0">
								<AlternatingItemStyle VerticalAlign="Top" BackColor="#E0E0E0"></AlternatingItemStyle>
								<ItemStyle VerticalAlign="Top" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Left" ForeColor="White" BackColor="Blue"></HeaderStyle>
								<FooterStyle ForeColor="White" VerticalAlign="Top" BackColor="Blue"></FooterStyle>
								<Columns>
									<asp:EditCommandColumn ButtonType="PushButton" UpdateText="Update" CancelText="Cancel" EditText="Edit"></asp:EditCommandColumn>
									<asp:TemplateColumn SortExpression="name_of_concern" HeaderText="Name of Concern">
										<ItemTemplate>
											<asp:Label visible=False id=NameOfConcern runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.name_of_concern") %>' />								
											<a href="#" onclick="window.open('<%# DataBinder.Eval(Container, "DataItem.name_of_concern", "ProductDetail.aspx?PrdName={0}") %>', 'ProductDetails', 'toolbars=no,status=no,height=400,width=500')"><%# DataBinder.Eval(Container, "DataItem.name_of_concern") %></a>											
										</ItemTemplate>
										<FooterTemplate>
											<asp:TextBox ID="NewNameofConcern" MaxLength="100" Runat="server" />
										</FooterTemplate>
										<EditItemTemplate>
											<asp:TextBox id="NameOfConcern" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.name_of_concern") %>' />
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="type_of_concern" HeaderText="Type of Concern">
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.type_of_concern") %>' ID="TypeConcern">
											</asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:DropDownList ID="NewDDLTypeofconcern" Runat="server" DataTextField="Description" DataValueField="List_Item" />
										</FooterTemplate>
										<EditItemTemplate>
											<asp:DropDownList ID="DDLTypeofconcern" Runat="server" DataTextField="Description" DataValueField="List_Item" />
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="level_of_concern" HeaderText="Level of Concern">
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.level_of_concern") %>' ID="LevelConcern">
											</asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:DropDownList ID="NewDDLLevelofconcern" Runat="server" DataTextField="Description" DataValueField="List_Item" />
										</FooterTemplate>
										<EditItemTemplate>
											<asp:DropDownList ID="DDLLevelofconcern" Runat="server" DataTextField="Description" DataValueField="List_Item" />
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:templatecolumn SortExpression="data_source" HeaderText="Datasource">
										<itemtemplate>
											<%# PPC.FDA.Data.OracleDataFactory.GetDataSourceName(DataBinder.Eval(Container, "DataItem.data_source")) %>											
											<input type=hidden runat=server id="ProductNameId" value='<%# DataBinder.Eval(Container, "DataItem.product_name_id") %>' />
										</itemtemplate>
									</asp:templatecolumn>								
									<asp:TemplateColumn SortExpression="narrative" HeaderText="Narrative">
										<ItemTemplate>
											<asp:Label ID="Narrative" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.narrative") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:TextBox ID="NewNarrative" Runat="server" TextMode="MultiLine" Columns="20" Rows="6" />
											<asp:Button ID="BtnAddNew" CommandName="Add" Text="Add" Runat="server" />
										</FooterTemplate>
										<EditItemTemplate>
											<asp:TextBox ID="Narrative" runat="server" TextMode=MultiLine Columns="20" Rows="6" Text='<%# DataBinder.Eval(Container, "DataItem.narrative") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False">
										<ItemTemplate>
											<input type="button" value="Delete" id="Delete_Item" onclick="javascript:window.open('DeleteConfirm.aspx', 'Confirm_Delete', 'status=no,menubar=no,scrollbars=yes,resizable=no,width=300,height=200');">
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" BackColor="Gainsboro" Mode="NumericPages"></PagerStyle>
							</cc1:fdagrid>
						</td>
					</tr>
					<tr>
						<td>						
							<div id="WatchListComments" runat=server>
								<div class="row">
									<span class="formr">
										<label for="comment">Watchlist Comment:</label>
									</span>
									<span class="formw">
										<asp:textbox cssclass="ta" textmode=MultiLine columns="40" rows="6" id="DDMACComments" runat=server />
									</span>
								</div>
								<div class="row">
									<span class="formr">
										&nbsp;
									</span>
									<span class="formw">
										<asp:button id="DDMACAdd" runat=server text="Add" cssclass="btn" />
									</span>
								</div>
								<asp:label id="DDMacError" cssclass="errorMessage" runat="server" />
							</div>										
							<div id="WatchListCommentsLabel" runat=server>
								<div class="row">
									<span class="formw">
										Watchlist Comment: <asp:label id="DDMacCommentsLabel" runat="server" visible="True" />
									</span>
								</div>
							</div>										
						</td>
					</tr>
					<tr>
						<td>
							<br>
							<br>
							<asp:Label id="WatchListHistory" runat="server">WatchList History:</asp:Label>
							<asp:Label id="HistoryErrorLabel" runat="server" ForeColor="Red"></asp:Label>
							<cc1:FDAGrid id="FDAGrid2" runat="server" Font-Names="Verdana" cellspacing="0" Font-Size="120%" GridLines="Horizontal" AutoGenerateColumns="False" CellPadding="2" AllowSorting="True" IsSortedAscending="True" ForeColor="Black" BorderWidth="1px" BorderStyle="Solid" BorderColor="Black" PageSize="7" AllowPaging="False">
								<AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="action" HeaderText="Action"></asp:BoundColumn>
									<asp:BoundColumn DataField="action_desc" HeaderText="Description"></asp:BoundColumn>
									<asp:BoundColumn DataField="action_date" HeaderText="Date" DataFormatString="{0:MM-dd-yyyy}"></asp:BoundColumn>
									<asp:BoundColumn DataField="action_by" HeaderText="Action By"></asp:BoundColumn>
									<asp:BoundColumn DataField="narrative" HeaderText="Narrative"></asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" BackColor="Gainsboro" Mode="NumericPages"></PagerStyle>
							</cc1:FDAGrid>
						</td>
					</tr>
				</TBODY>
			</table>
		</form>
	</body>
</HTML>
