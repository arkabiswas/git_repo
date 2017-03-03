<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Search.aspx.vb" Inherits="PPC.Search" %>
<%@ Register TagPrefix="lasa" Namespace="PPC.FDA.Controls" Assembly="PPC.FDA.Controls.FDAGrid" %>
<%@ Register TagPrefix="poca" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Register TagPrefix="poca" TagName="SearchBox" Src="SearchBox.ascx" %>
<%@ Register TagPrefix="mbrsc" Namespace="MetaBuilders.WebControls" Assembly="MetaBuilders.WebControls.RowSelectorColumn" %>
<HTML>
	<HEAD>
		<title>Search Results</title>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<style media="screen">
			#SearchMessage { BORDER-RIGHT: black 1px dashed; PADDING-RIGHT: 20px; BORDER-TOP: black 1px dashed; PADDING-LEFT: 20px; PADDING-BOTTOM: 20px; MARGIN: 0px 100px 50px; BORDER-LEFT: black 1px dashed; COLOR: black; PADDING-TOP: 20px; BORDER-BOTTOM: black 1px dashed; BACKGROUND-COLOR: #eee }
			#SearchConducted { PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 85px 100px 0px; PADDING-TOP: 0px }
		</style>		
		<script language="javascript" src="scripts.js" type="text/javascript"></script>
		<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
		<script language="javascript">
		<!--
		    if (document.images) {
		        var addwatch1 = new Image();
		        addwatch1.src = "Images/addtowatchlist.jpg";

		        var addwatch2 = new Image();
		        addwatch2.src = "Images/addtowatchlist2.jpg";
		    }		    
		-->
		</script>
		<script type="text/javascript" language="javascript">
		    function urlEncode(s) {
		        var i = s.indexOf("prdname=") + 8;
		        var urlTemp = s.substring(i, s.length);
		        var urlTemp2 = encodeURIComponent(urlTemp).replace(/\%20/g, '+').replace(/!/g, '%21').replace(/'/g, '%27').replace(/\(/g, '%28').replace(/\)/g, '%29').replace(/\*/g, '%2A').replace(/\~/g, '%7E');
		        return s.replace(urlTemp, urlTemp2);
		    }
		</script>
	</HEAD>
	<body>
		<form id="frmSearch" method="post" runat="server">
		<poca:TrackingHeader id="SearchHeader" 
										runat="server"
										location="<a href='StartSearch.aspx' title='New Search'>New Search</a>" />		
			<div id="searchResults" runat="server" Visible="False">
				<div>
					<span class="searchTitle">Search Results</span>
					<br>
					<span class="searchTerm">Search Term:</span>
					<em><asp:label id="lblSearchTerm" runat="server" /></em>
					<div class="searchImage">
						<asp:imagebutton id="addToWatchTop" 
										onmouseover="Swap(this, addwatch2)" 
										onmouseout="Swap(this, addwatch1)"
										causesvalidation="False" 
										AlternateText="Add selected words to a watchlist" 
										ImageUrl="~/images/addtowatchlist.jpg"
										BorderWidth="0" 
										Runat="server" 
										CssClass="searchImage" />
					</div>
				</div>
				<div id="gridMerged">
					<asp:label id="gridMergedHeader" runat="server" CssClass="gridHeader" Visible="False" />
					<lasa:fdagrid id="gridMergedSearch" Runat="server" Visible="False" Width="75%" 
                        UseAccessibleHeader="True" Font-Size="120%">
<PagerStyle Mode="NumericPages" HorizontalAlign="Right" BackColor="Gainsboro"></PagerStyle>

<AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>

<ItemStyle BackColor="White"></ItemStyle>
						<columns>
							<asp:templatecolumn HeaderStyle-HorizontalAlign="Left">
							    <HeaderTemplate>							       
							        <asp:CheckBox runat="server" onclick="ToggleSelectAll(this,'gridMergedSearch')" />
							    </HeaderTemplate>
									<itemtemplate>
									    <asp:CheckBox ID="chkSelect" runat="server" />									
									</itemtemplate>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
							</asp:templatecolumn>													
							
							<asp:templatecolumn							                   
						                    HeaderStyle-HorizontalAlign="Left"
											HeaderText="Name of Concern"
											SortExpression="name"
											HeaderStyle-Width="50%">											
									<itemtemplate>
									<asp:Label ID="Label1" runat="server" AssociatedControlID="chkSelect">								
									    <asp:HyperLink ID="HyperLink1" runat="server"  
                                            NavigateUrl='<%# Eval("name", "ProductDetail.aspx?prdname={0}") %>'									    
                                            OnClick="javascript:var prodDetailWin = window.open(urlEncode(this.href), 'ProductDetails', 'toolbars=no,status=no,scrollbars=yes,height=400,width=500');return false;"
									        text='<%#DataBinder.Eval(Container.DataItem, "name")%>'>	
									    </asp:HyperLink> 
									</asp:Label>	
									</itemtemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="50%"></HeaderStyle>
							</asp:templatecolumn>	
							<asp:boundcolumn HeaderStyle-HorizontalAlign="Left" 
											 HeaderText="Match Percentage" 
											 SortExpression="MergedScore" 
											 DataField="mergedscore" 
											 HeaderStyle-Width="25%" >
<HeaderStyle HorizontalAlign="Left" Width="25%"></HeaderStyle>
                            </asp:boundcolumn>
							<asp:templatecolumn HeaderStyle-HorizontalAlign="Left"
												HeaderText="Datasource"
												SortExpression="RecordSourceId"
												HeaderStyle-Width="25%">
									<itemtemplate>
										<%# PPC.FDA.Data.OracleDataFactory.GetDataSourceName(DataBinder.Eval(Container.DataItem, "RecordSourceId")) %>
									</itemtemplate>

<HeaderStyle HorizontalAlign="Left" Width="25%"></HeaderStyle>
							</asp:templatecolumn>
 						</columns>

<HeaderStyle HorizontalAlign="Center" BackColor="Blue" Font-Bold="True" ForeColor="White" 
                            Font-Size="Larger"></HeaderStyle>
					</lasa:fdagrid>
					<div class="errorMessage">
						<asp:label id="gridMergedMessage" Runat="server" Visible="False" />
					</div>
				</div>
				<div id="gridPhonetic">
					<asp:label id="gridPhoneticHeader" runat="server" CssClass="gridHeader" Visible="False" />
					<lasa:fdagrid id="gridPhoneticSearch" Runat="server" Visible="False" 
                        Width="75%" UseAccessibleHeader="True" Font-Size="120%">
<PagerStyle Mode="NumericPages" HorizontalAlign="Right" BackColor="Gainsboro"></PagerStyle>

<AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>

<ItemStyle BackColor="White"></ItemStyle>
						<columns>
														<asp:templatecolumn HeaderStyle-HorizontalAlign="Left">
							    <HeaderTemplate>							       
							        <asp:CheckBox runat="server" onclick="ToggleSelectAll(this,'gridPhoneticSearch')" />
							    </HeaderTemplate>
									<itemtemplate>
									    <asp:CheckBox ID="chkSelect" runat="server" />									
									</itemtemplate>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
							</asp:templatecolumn>													
							
							<asp:templatecolumn							                   
						                    HeaderStyle-HorizontalAlign="Left"
											HeaderText="Name of Concern"
											SortExpression="name"
											HeaderStyle-Width="50%">
									<itemtemplate>
									<asp:Label ID="Label2" runat="server" AssociatedControlID="chkSelect">								
									    <asp:HyperLink ID="HyperLink2" runat="server" 
                                            NavigateUrl='<%# Eval("name", "ProductDetail.aspx?prdname={0}") %>'									    
                                            OnClick="javascript:var prodDetailWin = window.open(urlEncode(this.href), 'ProductDetails', 'toolbars=no,status=no,scrollbars=yes,height=400,width=500');return false;"
									        text='<%#DataBinder.Eval(Container.DataItem, "name")%>'>	
									    </asp:HyperLink> 
									</asp:Label>	
									</itemtemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="50%"></HeaderStyle>
							</asp:templatecolumn>	
							<asp:boundcolumn HeaderStyle-HorizontalAlign="Left" 
											 HeaderText="Match Percentage" 
											 SortExpression="PhoneticScore" 
											 DataField="phoneticscore" 
											 HeaderStyle-Width="25%" >
<HeaderStyle HorizontalAlign="Left" Width="25%"></HeaderStyle>
                            </asp:boundcolumn>
							<asp:templatecolumn HeaderStyle-HorizontalAlign="Left"
												HeaderText="Datasource"
												SortExpression="RecordSourceId"
												HeaderStyle-Width="25%">
									<itemtemplate>
										<%# PPC.FDA.Data.OracleDataFactory.GetDataSourceName(DataBinder.Eval(Container.DataItem, "RecordSourceId")) %>
									</itemtemplate>

<HeaderStyle HorizontalAlign="Left" Width="25%"></HeaderStyle>
							</asp:templatecolumn>
																												
						</columns>

<HeaderStyle HorizontalAlign="Center" BackColor="Blue" Font-Bold="True" ForeColor="White" 
                            Font-Size="Larger"></HeaderStyle>
					</lasa:fdagrid>
					<div class="errorMessage">
						<asp:label id="gridPhoneticMessage" Runat="server" Visible="False" />
					</div>
				</div>
				<div id="gridOrthographic">
					<asp:label id="gridOrthographicHeader" runat="server" CssClass="gridHeader" Visible="False" />
					<lasa:fdagrid id="gridOrthographicSearch" Runat="server" Visible="False" 
                        Width="75%" UseAccessibleHeader="True" Font-Size="120%">
<PagerStyle Mode="NumericPages" HorizontalAlign="Right" BackColor="Gainsboro"></PagerStyle>

<AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>

<ItemStyle BackColor="White"></ItemStyle>
						<columns>
                           
							<asp:templatecolumn HeaderStyle-HorizontalAlign="Left">
							    <HeaderTemplate>							       
							        <asp:CheckBox runat="server" onclick="ToggleSelectAll(this,'gridOrthographic')" />
							    </HeaderTemplate>
									<itemtemplate>
									    <asp:CheckBox ID="chkSelect" runat="server" />									
									</itemtemplate>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
							</asp:templatecolumn>													
							
							<asp:templatecolumn							                   
						                    HeaderStyle-HorizontalAlign="Left"
											HeaderText="Name of Concern"
											SortExpression="name"
											HeaderStyle-Width="50%">
									<itemtemplate>
									<asp:Label runat="server" AssociatedControlID="chkSelect">								
									    <asp:HyperLink runat="server" 
                                            NavigateUrl='<%# Eval("name", "ProductDetail.aspx?prdname={0}") %>'									    
                                            OnClick="javascript:var prodDetailWin = window.open(urlEncode(this.href), 'ProductDetails', 'toolbars=no,status=no,scrollbars=yes,height=400,width=500');return false;"
									        text='<%#DataBinder.Eval(Container.DataItem, "name")%>'>	
									    </asp:HyperLink> 
									</asp:Label>	
									</itemtemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="50%"></HeaderStyle>
							</asp:templatecolumn>											
                                                
							<asp:boundcolumn HeaderStyle-HorizontalAlign="Left" 
											 HeaderText="Match Percentage" 
											 SortExpression="OrthographicScore" 
											 DataField="orthographicscore" 
											 HeaderStyle-Width="25%" >
<HeaderStyle HorizontalAlign="Left" Width="25%"></HeaderStyle>
                            </asp:boundcolumn>
							<asp:templatecolumn HeaderStyle-HorizontalAlign="Left"
												HeaderText="Datasource"
												SortExpression="RecordSourceId"
												HeaderStyle-Width="25%">
									<itemtemplate>
										<%# PPC.FDA.Data.OracleDataFactory.GetDataSourceName(DataBinder.Eval(Container.DataItem, "RecordSourceId")) %>
									</itemtemplate>

<HeaderStyle HorizontalAlign="Left" Width="25%"></HeaderStyle>
							</asp:templatecolumn>
						</columns>

<HeaderStyle HorizontalAlign="Center" BackColor="Blue" Font-Bold="True" ForeColor="White" 
                            Font-Size="Larger"></HeaderStyle>
					</lasa:fdagrid>
					<div class="errorMessage">
						<asp:label id="gridOrthographicMessage" Runat="server" Visible="False" />
					</div>
				</div>
				<div id="gridText">
					<asp:label id="gridTextHeader" runat="server" CssClass="gridHeader" Visible="False" />
					<lasa:fdagrid id="gridTextSearch" Runat="server" Visible="False" Width="75%" Font-Size="120%">
<PagerStyle Mode="NumericPages" HorizontalAlign="Right" BackColor="Gainsboro"></PagerStyle>

<AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>

<ItemStyle BackColor="White"></ItemStyle>
						<columns>
							<asp:templatecolumn HeaderStyle-HorizontalAlign="Left">
							    <HeaderTemplate>							       
							        <asp:CheckBox runat="server" onclick="ToggleSelectAll(this,'gridTextSearch')" />
							    </HeaderTemplate>
									<itemtemplate>
									    <asp:CheckBox ID="chkSelect" runat="server" />									
									</itemtemplate>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
							</asp:templatecolumn>													
							
							<asp:templatecolumn							                   
						                    HeaderStyle-HorizontalAlign="Left"
											HeaderText="Name of Concern"
											SortExpression="name"
											HeaderStyle-Width="50%">
									<itemtemplate>
									<asp:Label ID="Label2" runat="server" AssociatedControlID="chkSelect">								
									    <asp:HyperLink ID="HyperLink2" runat="server" 
                                            NavigateUrl='<%# Eval("name", "ProductDetail.aspx?prdname={0}") %>'									    
                                            OnClick="javascript:var prodDetailWin = window.open(urlEncode(this.href), 'ProductDetails', 'toolbars=no,status=no,scrollbars=yes,height=400,width=500');return false;"
									        text='<%#DataBinder.Eval(Container.DataItem, "name")%>'>	
									    </asp:HyperLink> 
									</asp:Label>	
									</itemtemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="50%"></HeaderStyle>
							</asp:templatecolumn>	
							<asp:templatecolumn HeaderStyle-HorizontalAlign="Left"
												HeaderText="Datasource"
												SortExpression="RecordSourceId"
												HeaderStyle-Width="50%">
									<itemtemplate>
										<%# PPC.FDA.Data.OracleDataFactory.GetDataSourceName(DataBinder.Eval(Container.DataItem, "RecordSourceId")) %>
									</itemtemplate>

<HeaderStyle HorizontalAlign="Left" Width="50%"></HeaderStyle>
							</asp:templatecolumn>												 
						</columns>

<HeaderStyle HorizontalAlign="Center" BackColor="Blue" Font-Bold="True" ForeColor="White"></HeaderStyle>
					</lasa:fdagrid>
					<div class="errorMessage">
						<asp:label id="gridTextMessage" Runat="server" Visible="False" />
					</div>
				</div>
				<div class="searchImage">
					<asp:imagebutton id="addToWatchBottom" 
									 onmouseover="Swap(this, addwatch2)" 
									 onmouseout="Swap(this, addwatch1)"
									 causesvalidation="False" 
									 AlternateText="Add selected words to a watchlist" 
									 ImageUrl="~/images/addtowatchlist.jpg"
									 BorderWidth="0" 
									 Runat="server" 
									 CssClass="searchImage" />
				</div>
				<poca:searchbox id="SearchResultsBox" runat="server" />
			</div>
			<asp:placeholder id="searchRunning" runat="server" visible="True">
				<h1 id="SearchConducted">Conducting your search.</h1>
				<div id="SearchMessage">
					<p>
					Please wait while your search is being conducted. Thank you for your patience.
					</p>
				</div>
			</asp:placeholder>
		</form>
	</body>
</HTML>
