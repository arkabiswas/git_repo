<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Search.aspx.vb" Inherits="PPC.Search" %>
<%@ Register TagPrefix="lasa" Namespace="PPC.FDA.Controls" Assembly="PPC.FDA.Controls.FDAGrid" %>
<%@ Register TagPrefix="poca" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%--<%@ Register TagPrefix="poca" TagName="SearchBox" Src="SearchBox.ascx" %>--%>
<%@ Register TagPrefix="mbrsc" Namespace="MetaBuilders.WebControls" Assembly="MetaBuilders.WebControls.RowSelectorColumn" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"  %>
<html>
	<head runat="server">
		<title>Search Results</title>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<link href="Styles.css" type="text/css" rel="stylesheet" />
		<style media="screen" type="text/css">
			#SearchMessage { BORDER-RIGHT: black 1px dashed; PADDING-RIGHT: 20px; BORDER-TOP: black 1px dashed; PADDING-LEFT: 20px; PADDING-BOTTOM: 20px; MARGIN: 0px 100px 50px; BORDER-LEFT: black 1px dashed; COLOR: black; PADDING-TOP: 20px; BORDER-BOTTOM: black 1px dashed; BACKGROUND-COLOR: #eee }
			#SearchConducted { PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 85px 100px 0px; PADDING-TOP: 0px }
		</style>		
		<script language="javascript" src="scripts.js" type="text/javascript"></script>
		<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
		<script language="javascript" type="text/javascript">
		<!--
		    if (document.images) {
		        var addwatch1 = new Image();
		        addwatch1.src = "Images/addtowatchlist.jpg";

		        var addwatch2 = new Image();
		        addwatch2.src = "Images/addtowatchlist2.jpg";

		        var exporttoexcel1 = new Image();
		        exporttoexcel1.src = "Images/exporttoexcel.jpg";

		        var exporttoexcel2 = new Image();
		        exporttoexcel2.src = "Images/exporttoexcel2.jpg";
		    }		    
		-->
		</script>
		<script type="text/javascript" language="javascript">
		    function urlEncode(s) {
		        var i = s.indexOf("prdname=") + 8;
		        var urlTemp = s.substring(i, s.length);
		        var urlTemp2 = encodeURIComponent(urlTemp).replace(/\%20/g, '+').replace(/!/g, '%21').replace(/'/g, '%27').replace(/\(/g, '%28').replace(/\)/g, '%29').replace(/\*/g, '%2A').replace(/\~/g, '%7E').replace(/\%2520/g, '+');
		        return s.replace(urlTemp, urlTemp2);
		    }
		</script>

        <style type="text/css">
            .divTab
            {
                border-top-left-radius: 6px;
                border-top-right-radius: 6px;
                padding: 3px 10px 2px 10px;
            }

            .divGrid
            {
                padding-top: 6px;
            }

            .MyTabStyle .ajax__tab_header
            {
                cursor: pointer;
                font-weight: bold;
                font-size: 16px;
                display: block;
            }
            .MyTabStyle .ajax__tab_header .ajax__tab_outer
            {
                color: #1d0eff;
                margin-left: 1px;
                border: 1px solid black;
                background-color: #4597d1;
                border-top-left-radius: 6px;
                border-top-right-radius: 6px;
            }

            .MyTabStyle .ajax__tab_header .ajax__tab_outer1
            {
                border-color: #4597d1;
                color: #ffffff;
                padding-left: 2px;
                margin-right: 3px;
                background-color: white;
            }
            .MyTabStyle .ajax__tab_header .ajax__tab_inner
            {
                border-color: #666;
                color: #ffffff;
            }
            .MyTabStyle .ajax__tab_hover .ajax__tab_outer
            {
                background-color: #ffffff;
            }
            .MyTabStyle .ajax__tab_hover .ajax__tab_inner
            {
                color: #1d0eff;
            }
            .MyTabStyle .ajax__tab_active .ajax__tab_outer
            {
                border-bottom-color: #ffffff;
                background-color: #ffffff;
                color: #ffffff;
            }
            .MyTabStyle .ajax__tab_active .ajax__tab_inner
            {
                color: #000000;
            }
            .MyTabStyle .ajax__tab_body
            {
                font-family: verdana,tahoma,helvetica;
                font-size: 10pt;
                background-color: #fff;
                border-top-width: 0;
                border: solid 2px #d7d7d7;
                border-top-color: #d7d7d7;
            }            
        </style>
	</head>
	<body>
		<form id="frmSearch" method="post" runat="server">
            <asp:ScriptManager runat="server" EnablePartialRendering="True"  />
		    <poca:TrackingHeader id="SearchHeader" runat="server" location="<a href='StartSearch.aspx' title='New Search' style='font-size: 14px; font-weight: bold;' >New Search</a>" />
			<div id="searchResults" style="margin-left: 75px;" runat="server" Visible="False">
              <asp:UpdatePanel ID="searchUpdatePanel" runat="server" UpdateMode="Conditional">
              <ContentTemplate>
				<div>
					<span class="searchTitle">Search Results</span>
					<br>
					<span class="searchTerm">Search Term:</span>
					<em><asp:label id="lblSearchTerm" runat="server" /></em>
					<div class="searchImage">
						<%--<asp:imagebutton id="addToWatchTop" 
										onmouseover="Swap(this, addwatch2)" 
										onmouseout="Swap(this, addwatch1)"
										causesvalidation="False" 
										AlternateText="Add selected words to a watchlist" 
										ImageUrl="~/images/addtowatchlist.jpg"
										BorderWidth="0" 
										Runat="server" 
										CssClass="searchImage" />
                        &nbsp;&nbsp;&nbsp;&nbsp;--%>
						<asp:imagebutton id="exportToExcelTop" 
										onmouseover="Swap(this, exporttoexcel2)" 
										onmouseout="Swap(this, exporttoexcel1)"
										causesvalidation="False" 
										AlternateText="Export Results To Excel" 
										ImageUrl="~/images/exporttoexcel.jpg"
										BorderWidth="0" 
										Runat="server" 
										CssClass="searchImage" />
					</div>
				</div>


        <cc1:TabContainer ID="searchTabContainer" runat="server" CssClass="MyTabStyle" >
        <cc1:TabPanel ID="tabpanelMerged" runat="server"  HeaderText="Combined">
            <HeaderTemplate>
                <div class="divTab" >
                    <asp:Label ID="lblMerged" runat="server" Text="Combined"></asp:Label>
                </div>
            </HeaderTemplate>
            <ContentTemplate>
				<div id="gridMerged" class="divGrid">
					<asp:label id="gridMergedHeader" runat="server" CssClass="gridHeader" Visible="False" />
                    <br /><asp:Label ID="gridMergedLegend1" runat="server" Visible="false" Text="70% and Higher: " BackColor="LightBlue" />
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="gridMergedLegend2" runat="server" Visible="false" Text="Between 55% and 69%: " BackColor="LightGray" />
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="gridMergedLegend3" runat="server" Visible="false" Text="54% and Lower: " BackColor="#FFFF99" />
					<lasa:fdagrid id="gridMergedSearch" Runat="server" Visible="False" Width="75%" UseAccessibleHeader="True" Font-Size="120%">
                        <PagerStyle Mode="NumericPages" HorizontalAlign="Right" BackColor="Gainsboro"></PagerStyle>
                        <AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>
                        <ItemStyle BackColor="White"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center" BackColor="Blue" Font-Bold="True" ForeColor="White" Font-Size="Larger"></HeaderStyle>
						<columns>
							<%--<asp:templatecolumn HeaderStyle-HorizontalAlign="Left">
							    <HeaderTemplate>							       
							        <asp:CheckBox runat="server" onclick="ToggleSelectAll(this,'gridMergedSearch')" />
							    </HeaderTemplate>
									<itemtemplate>
									    <asp:CheckBox ID="chkSelect" runat="server" />									
									</itemtemplate>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
							</asp:templatecolumn>--%>													
							
							<asp:templatecolumn							                   
						                    HeaderStyle-HorizontalAlign="Left"
											HeaderText="Name of Concern"
											SortExpression="name"
											HeaderStyle-Width="20%">											
									<itemtemplate>
									<%--<asp:Label ID="Label1" runat="server" AssociatedControlID="chkSelect">--%>
									<asp:Label ID="Label1" runat="server">								
									    <asp:HyperLink ID="HyperLink1" runat="server"  
                                            NavigateUrl='<%# Eval("name", "ProductDetail.aspx?prdname={0}") %>'									    
                                            OnClick="javascript:var prodDetailWin = window.open(urlEncode(this.href), 'ProductDetails', 'toolbars=no,status=no,scrollbars=yes,resizable=yes,height=400,width=500');prodDetailWin.focus();return false;"
									        text='<%#DataBinder.Eval(Container.DataItem, "name")%>'>	
									    </asp:HyperLink>
                                        &nbsp;&nbsp;
									</asp:Label>
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/inactive.png" AlternateText="Inactive Drug" ToolTip="Inactive Drug"
                                               Visible='<%# ((Eval("IsDeleted") + String.Empty).Contains("Y")) %>' />
                                    </itemtemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="20%"></HeaderStyle>
							</asp:templatecolumn>

							<asp:boundcolumn HeaderStyle-HorizontalAlign="Left" 
											 HeaderText="Match %" 
											 SortExpression="MergedScore" 
											 DataField="mergedscore" 
											 HeaderStyle-Width="20%" >
                                    <HeaderStyle HorizontalAlign="Left" Width="20%"></HeaderStyle>
                            </asp:boundcolumn>

							<%--<asp:boundcolumn HeaderStyle-HorizontalAlign="Left" 
											 HeaderText="Reverse Match %" 
											 SortExpression="ReverseMergedScore" 
											 DataField="reversemergedscore" 
											 HeaderStyle-Width="20%" >
                                    <HeaderStyle HorizontalAlign="Left" Width="20%"></HeaderStyle>
                            </asp:boundcolumn>--%>

							<asp:templatecolumn HeaderStyle-HorizontalAlign="Left"
												HeaderText="Datasource"
												SortExpression="RecordSourceId"
												HeaderStyle-Width="40%">
									<itemtemplate>
										<%# PPC.FDA.Data.OracleDataFactory.GetDataSourceName(DataBinder.Eval(Container.DataItem, "RecordSourceId")) %>
									</itemtemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="40%"></HeaderStyle>
							</asp:templatecolumn>

							<%--<asp:boundcolumn HeaderStyle-HorizontalAlign="Left" 
											 HeaderText="Product Detail" 
											 SortExpression="ProductDetail" 
											 DataField="ProductDetail" 
											 HeaderStyle-Width="25%"
                                             Visible="false" >
                                    <HeaderStyle HorizontalAlign="Left" Width="1%"></HeaderStyle>
                            </asp:boundcolumn>--%>
 						</columns>
					</lasa:fdagrid>
					<div class="errorMessage">
						<asp:label id="gridMergedMessage" Runat="server" Visible="False" />
					</div>
				</div>
            </ContentTemplate>
        </cc1:TabPanel>

        <cc1:TabPanel ID="tabpanelPhonetic" runat="server"  HeaderText="Phonetic">
            <HeaderTemplate>
                <div class="divTab" >
                    <asp:Label ID="lblPhonetic" runat="server" Text="Phonetic"></asp:Label>
                </div>
            </HeaderTemplate>
            <ContentTemplate>
				<div id="gridPhonetic" class="divGrid">
					<asp:label id="gridPhoneticHeader" runat="server" CssClass="gridHeader" Visible="False" />
                    <br /><asp:Label ID="gridPhoneticLegend1" runat="server" Visible="false" Text="70% and Higher: " BackColor="LightBlue" />
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="gridPhoneticLegend2" runat="server" Visible="false" Text="Between 55% and 69%: " BackColor="LightGray" />
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="gridPhoneticLegend3" runat="server" Visible="false" Text="54% and Lower: " BackColor="#FFFF99" />
					<lasa:fdagrid id="gridPhoneticSearch" Runat="server" Visible="False" Width="75%" UseAccessibleHeader="True" Font-Size="120%">
                        <PagerStyle Mode="NumericPages" HorizontalAlign="Right" BackColor="Gainsboro"></PagerStyle>
                        <AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>
                        <ItemStyle BackColor="White"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center" BackColor="Blue" Font-Bold="True" ForeColor="White" Font-Size="Larger"></HeaderStyle>
						<columns>
							<%--<asp:templatecolumn HeaderStyle-HorizontalAlign="Left">
							    <HeaderTemplate>							       
							        <asp:CheckBox runat="server" onclick="ToggleSelectAll(this,'gridPhoneticSearch')" />
							    </HeaderTemplate>
									<itemtemplate>
									    <asp:CheckBox ID="chkSelect" runat="server" />									
									</itemtemplate>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
							</asp:templatecolumn>--%>													
							
							<asp:templatecolumn							                   
						                    HeaderStyle-HorizontalAlign="Left"
											HeaderText="Name of Concern"
											SortExpression="name"
											HeaderStyle-Width="20%">
									<itemtemplate>
									<%--<asp:Label ID="Label4" runat="server" AssociatedControlID="chkSelect">--%>
                                    <asp:Label ID="Label4" runat="server">								
									    <asp:HyperLink ID="HyperLink2" runat="server" 
                                            NavigateUrl='<%# Eval("name", "ProductDetail.aspx?prdname={0}") %>'									    
                                            OnClick="javascript:var prodDetailWin = window.open(urlEncode(this.href), 'ProductDetails', 'toolbars=no,status=no,scrollbars=yes,resizable=yes,height=400,width=500');prodDetailWin.focus();return false;"
									        text='<%#DataBinder.Eval(Container.DataItem, "name")%>'>	
									    </asp:HyperLink> 
                                        &nbsp;&nbsp;
									</asp:Label>
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/inactive.png" AlternateText="Inactive Drug" ToolTip="Inactive Drug"
                                               Visible='<%# ((Eval("IsDeleted") + String.Empty).Contains("Y")) %>' />
									</itemtemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="20%"></HeaderStyle>
							</asp:templatecolumn>

							<asp:boundcolumn HeaderStyle-HorizontalAlign="Left" 
											 HeaderText="Match %" 
											 SortExpression="PhoneticScore" 
											 DataField="phoneticscore" 
											 HeaderStyle-Width="20%" >
                                    <HeaderStyle HorizontalAlign="Left" Width="20%"></HeaderStyle>
                            </asp:boundcolumn>
							
							<%--<asp:boundcolumn HeaderStyle-HorizontalAlign="Left" 
											 HeaderText="Reverse Match %" 
											 SortExpression="ReversePhoneticScore" 
											 DataField="reversephoneticscore" 
											 HeaderStyle-Width="20%" >
                                    <HeaderStyle HorizontalAlign="Left" Width="20%"></HeaderStyle>
                            </asp:boundcolumn>--%>

                            <asp:templatecolumn HeaderStyle-HorizontalAlign="Left"
												HeaderText="Datasource"
												SortExpression="RecordSourceId"
												HeaderStyle-Width="40%">
									<itemtemplate>
										<%# PPC.FDA.Data.OracleDataFactory.GetDataSourceName(DataBinder.Eval(Container.DataItem, "RecordSourceId")) %>
									</itemtemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="40%"></HeaderStyle>
							</asp:templatecolumn>

							<%--<asp:boundcolumn HeaderStyle-HorizontalAlign="Left" 
											 HeaderText="Product Detail" 
											 SortExpression="ProductDetail" 
											 DataField="ProductDetail" 
											 HeaderStyle-Width="25%"
                                             Visible="false" >
                                    <HeaderStyle HorizontalAlign="Left" Width="1%"></HeaderStyle>
                            </asp:boundcolumn>--%>
						</columns>
					</lasa:fdagrid>
					<div class="errorMessage">
						<asp:label id="gridPhoneticMessage" Runat="server" Visible="False" />
					</div>
				</div>
            </ContentTemplate>
        </cc1:TabPanel>

        <cc1:TabPanel ID="tabpanelOrthographic" runat="server"  HeaderText="Orthographic">
            <HeaderTemplate>
                <div class="divTab">
                    <asp:Label ID="lblOrthographic" runat="server" Text="Orthographic"></asp:Label>
                </div>
            </HeaderTemplate>
            <ContentTemplate>
				<div id="gridOrthographic" class="divGrid">
					<asp:label id="gridOrthographicHeader" runat="server" CssClass="gridHeader" Visible="False" />
                    <br /><asp:Label ID="gridOrthographicLegend1" runat="server" Visible="false" Text="70% and Higher: " BackColor="LightBlue" />
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="gridOrthographicLegend2" runat="server" Visible="false" Text="Between 55% and 69%: " BackColor="LightGray" />
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="gridOrthographicLegend3" runat="server" Visible="false" Text="54% and Lower: " BackColor="#FFFF99" />
					<lasa:fdagrid id="gridOrthographicSearch" Runat="server" Visible="False" Width="75%" UseAccessibleHeader="True" Font-Size="120%" >
                        <PagerStyle HorizontalAlign="Right" BackColor="Gainsboro"></PagerStyle>
                        <AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>
                        <ItemStyle BackColor="White"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center" BackColor="Blue" Font-Bold="True" ForeColor="White" Font-Size="Larger"></HeaderStyle>
						<columns>
							<%--<asp:templatecolumn HeaderStyle-HorizontalAlign="Left">
							    <HeaderTemplate>							       
							        <asp:CheckBox runat="server" onclick="ToggleSelectAll(this,'gridOrthographic')" />
							    </HeaderTemplate>
									<itemtemplate>
									    <asp:CheckBox ID="chkSelect" runat="server" />									
									</itemtemplate>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
							</asp:templatecolumn>--%>													
							
							<asp:templatecolumn							                   
						                    HeaderStyle-HorizontalAlign="Left"
											HeaderText="Name of Concern"
											SortExpression="name"
											HeaderStyle-Width="20%">
									<itemtemplate>
									<%--<asp:Label runat="server" AssociatedControlID="chkSelect">--%>
                                    <asp:Label runat="server">
									    <asp:HyperLink runat="server" 
                                            NavigateUrl='<%# Eval("name", "ProductDetail.aspx?prdname={0}") %>'									    
                                            OnClick="javascript:var prodDetailWin = window.open(urlEncode(this.href), 'ProductDetails', 'toolbars=no,status=no,scrollbars=yes,resizable=yes,height=400,width=500');prodDetailWin.focus();return false;"
									        text='<%#DataBinder.Eval(Container.DataItem, "name")%>'>
									    </asp:HyperLink> 
                                        &nbsp;&nbsp;
									</asp:Label>
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/images/inactive.png" AlternateText="Inactive Drug" ToolTip="Inactive Drug"
                                               Visible='<%# ((Eval("IsDeleted") + String.Empty).Contains("Y")) %>' />
									</itemtemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="20%"></HeaderStyle>
							</asp:templatecolumn>											
                                                
							<asp:boundcolumn HeaderStyle-HorizontalAlign="Left" 
											 HeaderText="Match %" 
											 SortExpression="OrthographicScore" 
											 DataField="orthographicscore" 
											 HeaderStyle-Width="20%" >
                                    <HeaderStyle HorizontalAlign="Left" Width="20%"></HeaderStyle>
                            </asp:boundcolumn>

							<%--<asp:boundcolumn HeaderStyle-HorizontalAlign="Left" 
											 HeaderText="Reverse Match %" 
											 SortExpression="ReverseOrthographicScore" 
											 DataField="reverseorthographicscore" 
											 HeaderStyle-Width="20%" >
                                    <HeaderStyle HorizontalAlign="Left" Width="20%"></HeaderStyle>
                            </asp:boundcolumn>--%>

							<asp:templatecolumn HeaderStyle-HorizontalAlign="Left"
												HeaderText="Datasource"
												SortExpression="RecordSourceId"
												HeaderStyle-Width="40%">
									<itemtemplate>
										<%# PPC.FDA.Data.OracleDataFactory.GetDataSourceName(DataBinder.Eval(Container.DataItem, "RecordSourceId")) %>
									</itemtemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="40%"></HeaderStyle>
							</asp:templatecolumn>

							<%--<asp:boundcolumn HeaderStyle-HorizontalAlign="Left" 
											 HeaderText="Product Detail" 
											 SortExpression="ProductDetail" 
											 DataField="ProductDetail" 
											 HeaderStyle-Width="25%"
                                             Visible="false" >
                                    <HeaderStyle HorizontalAlign="Left" Width="1%"></HeaderStyle>
                            </asp:boundcolumn>--%>
						</columns>
					</lasa:fdagrid>
					<div class="errorMessage">
						<asp:label id="gridOrthographicMessage" Runat="server" Visible="False" />
					</div>
				</div>
            </ContentTemplate>
        </cc1:TabPanel>

        <cc1:TabPanel ID="tabpanelText" runat="server"  HeaderText="Text">
            <HeaderTemplate>
                <div class="divTab" >
                    <asp:Label ID="lblText" runat="server" Text="Text"></asp:Label>
                </div>
            </HeaderTemplate>
            <ContentTemplate>
				<div id="gridText" class="divGrid">
					<asp:label id="gridTextHeader" runat="server" CssClass="gridHeader" Visible="False" />
					<lasa:fdagrid id="gridTextSearch" Runat="server" Visible="False" Width="75%" Font-Size="120%">
                        <PagerStyle Mode="NumericPages" HorizontalAlign="Right" BackColor="Gainsboro"></PagerStyle>
                        <AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>
                        <ItemStyle BackColor="White"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center" BackColor="Blue" Font-Bold="True" ForeColor="White"></HeaderStyle>
						<columns>
							<%--<asp:templatecolumn HeaderStyle-HorizontalAlign="Left">
							    <HeaderTemplate>							       
							        <asp:CheckBox runat="server" onclick="ToggleSelectAll(this,'gridTextSearch')" />
							    </HeaderTemplate>
									<itemtemplate>
									    <asp:CheckBox ID="chkSelect" runat="server" />									
									</itemtemplate>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
							</asp:templatecolumn>--%>													
							
							<asp:templatecolumn							                   
						                    HeaderStyle-HorizontalAlign="Left"
											HeaderText="Name of Concern"
											SortExpression="name"
											HeaderStyle-Width="50%">
									<itemtemplate>
									<%--<asp:Label ID="Label2" runat="server" AssociatedControlID="chkSelect">--%>
                                    <asp:Label ID="Label2" runat="server">
									    <asp:HyperLink ID="HyperLink2" runat="server" 
                                            NavigateUrl='<%# Eval("name", "ProductDetail.aspx?prdname={0}") %>'									    
                                            OnClick="javascript:var prodDetailWin = window.open(urlEncode(this.href), 'ProductDetails', 'toolbars=no,status=no,scrollbars=yes,resizable=yes,height=400,width=500');prodDetailWin.focus();return false;"
									        text='<%#DataBinder.Eval(Container.DataItem, "name")%>'>	
									    </asp:HyperLink> 
                                        &nbsp;&nbsp;
									</asp:Label>
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/images/inactive.png" AlternateText="Inactive Drug" ToolTip="Inactive Drug"
                                               Visible='<%# ((Eval("IsDeleted") + String.Empty).Contains("Y")) %>' />
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

							<%--<asp:boundcolumn HeaderStyle-HorizontalAlign="Left" 
											 HeaderText="Product Detail" 
											 SortExpression="ProductDetail" 
											 DataField="ProductDetail" 
											 HeaderStyle-Width="25%"
                                             Visible="false" >
                                    <HeaderStyle HorizontalAlign="Left" Width="1%"></HeaderStyle>
                            </asp:boundcolumn>--%>
						</columns>
					</lasa:fdagrid>
					<div class="errorMessage">
						<asp:label id="gridTextMessage" Runat="server" Visible="False" />
					</div>
				</div>
            </ContentTemplate>
        </cc1:TabPanel>

        <%--
        <cc1:TabPanel ID="tabPanelSpelling" runat="server"  HeaderText="Spelling">
            <HeaderTemplate>
                <div class="divTab" >
                    <asp:Label ID="lblSpelling" runat="server" Text="Spelling"></asp:Label>
                </div>
            </HeaderTemplate>
            <ContentTemplate>
				<div id="gridSpelling" class="divGrid">
					<asp:label id="gridSpellingHeader" runat="server" CssClass="gridHeader" Visible="False" />
					<br /><asp:Label ID="gridSpellingLegend1" runat="server" Visible="false" Text="70% and Higher: " BackColor="LightBlue" />
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="gridSpellingLegend2" runat="server" Visible="false" Text="Between 55% and 69%: " BackColor="LightGray" />
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="gridSpellingLegend3" runat="server" Visible="false" Text="54% and Lower: " BackColor="#FFFF99" />
					<lasa:fdagrid id="gridSpellingSearch" Runat="server" Visible="False" Width="75%" UseAccessibleHeader="True" Font-Size="120%">
                        <PagerStyle HorizontalAlign="Right" BackColor="Gainsboro"></PagerStyle>
                        <AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>
                        <ItemStyle BackColor="White"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center" BackColor="Blue" Font-Bold="True" ForeColor="White" Font-Size="Larger"></HeaderStyle>
						<columns>
							
							<asp:templatecolumn							                   
						                    HeaderStyle-HorizontalAlign="Left"
											HeaderText="Name of Concern"
											SortExpression="name"
											HeaderStyle-Width="25%">
									<itemtemplate>
                                    <asp:Label ID="Label2" runat="server">
									    <asp:HyperLink ID="DrugNameHL" runat="server" 
                                            NavigateUrl='<%# Eval("name", "ProductDetail.aspx?prdname={0}") %>'									    
                                            OnClick="javascript:var prodDetailWin = window.open(urlEncode(this.href), 'ProductDetails', 'toolbars=no,status=no,scrollbars=yes,resizable=yes,height=400,width=500');prodDetailWin.focus();return false;"
									        text='<%#DataBinder.Eval(Container.DataItem, "name")%>'>	
									    </asp:HyperLink> 
									</asp:Label>	
									</itemtemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="25%"></HeaderStyle>
							</asp:templatecolumn>	
							
							<asp:boundcolumn HeaderStyle-HorizontalAlign="Left" 
											 HeaderText="Spelling Score (Orthographics Plus Weight)" 
											 SortExpression="SpellingScore" 
											 DataField="spellingscore" 
											 HeaderStyle-Width="25%" >
                                    <HeaderStyle HorizontalAlign="Left" Width="25%"></HeaderStyle>
                            </asp:boundcolumn>

							<asp:boundcolumn HeaderStyle-HorizontalAlign="Left" 
											 HeaderText="Orthographics Score" 
											 SortExpression="OrthographicScore" 
											 DataField="orthographicscore" 
											 HeaderStyle-Width="25%" >
                                    <HeaderStyle HorizontalAlign="Left" Width="25%"></HeaderStyle>
                            </asp:boundcolumn>

                            <asp:templatecolumn HeaderStyle-HorizontalAlign="Left"
												HeaderText="Datasource"
												SortExpression="RecordSourceId"
												HeaderStyle-Width="50%">
									<itemtemplate>
										<%# PPC.FDA.Data.OracleDataFactory.GetDataSourceName(DataBinder.Eval(Container.DataItem, "RecordSourceId")) %>
									</itemtemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="50%"></HeaderStyle>
							</asp:templatecolumn>												 

							<asp:boundcolumn HeaderStyle-HorizontalAlign="Left" 
											 HeaderText="Product Detail" 
											 SortExpression="ProductDetail" 
											 DataField="ProductDetail" 
											 HeaderStyle-Width="25%"
                                             Visible="false" >
                                    <HeaderStyle HorizontalAlign="Left" Width="1%"></HeaderStyle>
                            </asp:boundcolumn>
						</columns>
					</lasa:fdagrid>
					<div class="errorMessage">
						<asp:label id="gridSpellingMessage" Runat="server" Visible="False" />
					</div>
				</div>
            </ContentTemplate>
        </cc1:TabPanel>
        --%>

        <cc1:TabPanel ID="tabPanelAll" runat="server"  HeaderText="All">
            <HeaderTemplate>
                <div class="divTab" >
                    <asp:Label ID="lblAll" runat="server" Text="All "></asp:Label>
                </div>
            </HeaderTemplate>
            <ContentTemplate>
                <div id="gridAll">
				    <div id="gridMergedAll" runat="server" class="divGrid">
					    <asp:label id="gridMergedHeaderAll" runat="server" CssClass="gridHeader" Visible="False" />
                        <br /><asp:Label ID="gridMergedLegend1All" runat="server" Visible="false" Text="70% and Higher: " BackColor="LightBlue" />
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="gridMergedLegend2All" runat="server" Visible="false" Text="Between 55% and 69%: " BackColor="LightGray" />
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="gridMergedLegend3All" runat="server" Visible="false" Text="54% and Lower: " BackColor="#FFFF99" />
                        <asp:Panel id="gridMergedPanelAll"  runat="server" Height="500px" Width="77%" ScrollBars="Auto" BorderColor="LightBlue" BorderWidth="1px" Visible="false" >
					    <lasa:fdagrid id="gridMergedSearchAll" Runat="server" Visible="False" Width="100%" UseAccessibleHeader="True" Font-Size="120%">
                            <PagerStyle Mode="NumericPages" HorizontalAlign="Right" BackColor="Gainsboro"></PagerStyle>
                            <AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Center" BackColor="Blue" Font-Bold="True" ForeColor="White" Font-Size="Larger"></HeaderStyle>
						    <columns>
							    <%--<asp:templatecolumn HeaderStyle-HorizontalAlign="Left">
							        <HeaderTemplate>							       
							            <asp:CheckBox ID="CheckBox1" runat="server" onclick="ToggleSelectAll(this,'gridMergedSearchAll')" />
							        </HeaderTemplate>
									    <itemtemplate>
									        <asp:CheckBox ID="chkSelect" runat="server" />									
									    </itemtemplate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
							    </asp:templatecolumn>--%>													
							
							    <asp:templatecolumn							                   
						                        HeaderStyle-HorizontalAlign="Left"
											    HeaderText="Name of Concern"
											    SortExpression="name"
											    HeaderStyle-Width="25%">											
									    <itemtemplate>
									    <%--<asp:Label ID="Label1" runat="server" AssociatedControlID="chkSelect">--%>
                                        <asp:Label ID="Label1" runat="server">
									        <asp:HyperLink ID="HyperLink1" runat="server"  
                                                NavigateUrl='<%# Eval("name", "ProductDetail.aspx?prdname={0}") %>'									    
                                                OnClick="javascript:var prodDetailWin = window.open(urlEncode(this.href), 'ProductDetails', 'toolbars=no,status=no,scrollbars=yes,resizable=yes,height=400,width=500');prodDetailWin.focus();return false;"
									            text='<%#DataBinder.Eval(Container.DataItem, "name")%>'>	
									        </asp:HyperLink> 
                                            &nbsp;&nbsp;
									    </asp:Label>
                                        <asp:Image ID="Image5" runat="server" ImageUrl="~/images/inactive.png" AlternateText="Inactive Drug" ToolTip="Inactive Drug"
                                                   Visible='<%# ((Eval("IsDeleted") + String.Empty).Contains("Y")) %>' />
									    </itemtemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="25%"></HeaderStyle>
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
												    HeaderStyle-Width="50%">
									    <itemtemplate>
										    <%# PPC.FDA.Data.OracleDataFactory.GetDataSourceName(DataBinder.Eval(Container.DataItem, "RecordSourceId")) %>
									    </itemtemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="50%"></HeaderStyle>
							    </asp:templatecolumn>

							    <%--<asp:boundcolumn HeaderStyle-HorizontalAlign="Left" 
											     HeaderText="Product Detail" 
											     SortExpression="ProductDetail" 
											     DataField="ProductDetail" 
											     HeaderStyle-Width="25%"
                                                 Visible="false" >
                                        <HeaderStyle HorizontalAlign="Left" Width="1%"></HeaderStyle>
                                </asp:boundcolumn>--%>
 						    </columns>
					    </lasa:fdagrid>
                        </asp:Panel>
					    <div class="errorMessage">
						    <asp:label id="gridMergedMessageAll" Runat="server" Visible="False" />
					    </div>
				    </div>

				    <div id="gridPhoneticAll" runat="server" class="divGrid">
					    <asp:label id="gridPhoneticHeaderAll" runat="server" CssClass="gridHeader" Visible="False" />
                        <br /><asp:Label ID="gridPhoneticLegend1All" runat="server" Visible="false" Text="70% and Higher: " BackColor="LightBlue" />
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="gridPhoneticLegend2All" runat="server" Visible="false" Text="Between 55% and 69%: " BackColor="LightGray" />
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="gridPhoneticLegend3All" runat="server" Visible="false" Text="54% and Lower: " BackColor="#FFFF99" />
                        <asp:Panel id="gridPhoneticPanelAll" runat="server" Height="500px" Width="77%" ScrollBars="Auto" BorderColor="LightBlue" BorderWidth="1px" Visible="false" >
					    <lasa:fdagrid id="gridPhoneticSearchAll" Runat="server" Visible="False" Width="100%" UseAccessibleHeader="True" Font-Size="120%">
                            <PagerStyle Mode="NumericPages" HorizontalAlign="Right" BackColor="Gainsboro"></PagerStyle>
                            <AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Center" BackColor="Blue" Font-Bold="True" ForeColor="White" Font-Size="Larger"></HeaderStyle>
						    <columns>
								<%--<asp:templatecolumn HeaderStyle-HorizontalAlign="Left">
							        <HeaderTemplate>							       
							            <asp:CheckBox ID="CheckBox2" runat="server" onclick="ToggleSelectAll(this,'gridPhoneticSearchAll')" />
							        </HeaderTemplate>
									    <itemtemplate>
									        <asp:CheckBox ID="chkSelect" runat="server" />									
									    </itemtemplate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
							    </asp:templatecolumn>--%>													
							
							    <asp:templatecolumn							                   
						                        HeaderStyle-HorizontalAlign="Left"
											    HeaderText="Name of Concern"
											    SortExpression="name"
											    HeaderStyle-Width="25%">
									    <itemtemplate>
									    <%--<asp:Label ID="Label2" runat="server" AssociatedControlID="chkSelect">--%>
                                        <asp:Label ID="Label2" runat="server">
									        <asp:HyperLink ID="HyperLink2" runat="server" 
                                                NavigateUrl='<%# Eval("name", "ProductDetail.aspx?prdname={0}") %>'									    
                                                OnClick="javascript:var prodDetailWin = window.open(urlEncode(this.href), 'ProductDetails', 'toolbars=no,status=no,scrollbars=yes,resizable=yes,height=400,width=500');prodDetailWin.focus();return false;"
									            text='<%#DataBinder.Eval(Container.DataItem, "name")%>'>	
									        </asp:HyperLink> 
                                            &nbsp;&nbsp;
									    </asp:Label>
                                        <asp:Image ID="Image6" runat="server" ImageUrl="~/images/inactive.png" AlternateText="Inactive Drug" ToolTip="Inactive Drug"
                                                   Visible='<%# ((Eval("IsDeleted") + String.Empty).Contains("Y")) %>' />
									    </itemtemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="25%"></HeaderStyle>
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
												    HeaderStyle-Width="50%">
									    <itemtemplate>
										    <%# PPC.FDA.Data.OracleDataFactory.GetDataSourceName(DataBinder.Eval(Container.DataItem, "RecordSourceId")) %>
									    </itemtemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="50%"></HeaderStyle>
							    </asp:templatecolumn>

							    <%--<asp:boundcolumn HeaderStyle-HorizontalAlign="Left" 
											     HeaderText="Product Detail" 
											     SortExpression="ProductDetail" 
											     DataField="ProductDetail" 
											     HeaderStyle-Width="25%"
                                                 Visible="false" >
                                        <HeaderStyle HorizontalAlign="Left" Width="1%"></HeaderStyle>
                                </asp:boundcolumn>--%>
						    </columns>
					    </lasa:fdagrid>
                        </asp:Panel>
					    <div class="errorMessage">
						    <asp:label id="gridPhoneticMessageAll" Runat="server" Visible="False" />
					    </div>
				    </div>

				    <div id="gridOrthographicAll" runat="server" class="divGrid">
					    <asp:label id="gridOrthographicHeaderAll" runat="server" CssClass="gridHeader" Visible="False" />
                        <br /><asp:Label ID="gridOrthographicLegend1All" runat="server" Visible="false" Text="70% and Higher: " BackColor="LightBlue" />
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="gridOrthographicLegend2All" runat="server" Visible="false" Text="Between 55% and 69%: " BackColor="LightGray" />
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="gridOrthographicLegend3All" runat="server" Visible="false" Text="54% and Lower: " BackColor="#FFFF99" />
                        <asp:Panel id="gridOrthographicPanelAll" runat="server" Height="500px" Width="77%" ScrollBars="Auto" BorderColor="LightBlue" BorderWidth="1px" Visible="false" >
					    <lasa:fdagrid id="gridOrthographicSearchAll" Runat="server" Visible="False" Width="100%" UseAccessibleHeader="True" Font-Size="120%">
                            <PagerStyle Mode="NumericPages" HorizontalAlign="Right" BackColor="Gainsboro"></PagerStyle>
                            <AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Center" BackColor="Blue" Font-Bold="True" ForeColor="White" Font-Size="Larger"></HeaderStyle>
						    <columns>
							    <%--<asp:templatecolumn HeaderStyle-HorizontalAlign="Left">
							        <HeaderTemplate>							       
							            <asp:CheckBox ID="CheckBox3" runat="server" onclick="ToggleSelectAll(this,'gridOrthographicSearchAll')" />
							        </HeaderTemplate>
									    <itemtemplate>
									        <asp:CheckBox ID="chkSelect" runat="server" />									
									    </itemtemplate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
							    </asp:templatecolumn>--%>													
							
							    <asp:templatecolumn							                   
						                        HeaderStyle-HorizontalAlign="Left"
											    HeaderText="Name of Concern"
											    SortExpression="name"
											    HeaderStyle-Width="25%">
									    <itemtemplate>
									    <%--<asp:Label ID="Label3" runat="server" AssociatedControlID="chkSelect">--%>
                                        <asp:Label ID="Label3" runat="server">
									        <asp:HyperLink ID="HyperLink3" runat="server" 
                                                NavigateUrl='<%# Eval("name", "ProductDetail.aspx?prdname={0}") %>'									    
                                                OnClick="javascript:var prodDetailWin = window.open(urlEncode(this.href), 'ProductDetails', 'toolbars=no,status=no,scrollbars=yes,resizable=yes,height=400,width=500');prodDetailWin.focus();return false;"
									            text='<%#DataBinder.Eval(Container.DataItem, "name")%>'>	
									        </asp:HyperLink> 
                                            &nbsp;&nbsp;
									    </asp:Label>
                                        <asp:Image ID="Image7" runat="server" ImageUrl="~/images/inactive.png" AlternateText="Inactive Drug" ToolTip="Inactive Drug"
                                                   Visible='<%# ((Eval("IsDeleted") + String.Empty).Contains("Y")) %>' />
									    </itemtemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="25%"></HeaderStyle>
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
												    HeaderStyle-Width="50%">
									    <itemtemplate>
										    <%# PPC.FDA.Data.OracleDataFactory.GetDataSourceName(DataBinder.Eval(Container.DataItem, "RecordSourceId")) %>
									    </itemtemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="50%"></HeaderStyle>
							    </asp:templatecolumn>

							    <%--<asp:boundcolumn HeaderStyle-HorizontalAlign="Left" 
											     HeaderText="Product Detail" 
											     SortExpression="ProductDetail" 
											     DataField="ProductDetail" 
											     HeaderStyle-Width="25%"
                                                 Visible="false" >
                                        <HeaderStyle HorizontalAlign="Left" Width="1%"></HeaderStyle>
                                </asp:boundcolumn>--%>
						    </columns>
					    </lasa:fdagrid>
                        </asp:Panel>
					    <div class="errorMessage">
						    <asp:label id="gridOrthographicMessageAll" Runat="server" Visible="False" />
					    </div>
				    </div>

				    <div id="gridTextAll" runat="server" class="divGrid">
					    <asp:label id="gridTextHeaderAll" runat="server" CssClass="gridHeader" Visible="False" />
                        <asp:Panel id="gridTextPanelAll" runat="server" Height="500px" Width="77%" ScrollBars="Auto" BorderColor="LightBlue" BorderWidth="1px" Visible="false" >
					    <lasa:fdagrid id="gridTextSearchAll" Runat="server" Visible="False" Width="100%" Font-Size="120%">
                            <PagerStyle Mode="NumericPages" HorizontalAlign="Right" BackColor="Gainsboro"></PagerStyle>
                            <AlternatingItemStyle BackColor="#E0E0E0"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Center" BackColor="Blue" Font-Bold="True" ForeColor="White"></HeaderStyle>
						    <columns>
							    <%--<asp:templatecolumn HeaderStyle-HorizontalAlign="Left">
							        <HeaderTemplate>							       
							            <asp:CheckBox ID="CheckBox4" runat="server" onclick="ToggleSelectAll(this,'gridTextSearchAll')" />
							        </HeaderTemplate>
									    <itemtemplate>
									        <asp:CheckBox ID="chkSelect" runat="server" />									
									    </itemtemplate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
							    </asp:templatecolumn>--%>													
							
							    <asp:templatecolumn							                   
						                        HeaderStyle-HorizontalAlign="Left"
											    HeaderText="Name of Concern"
											    SortExpression="name"
											    HeaderStyle-Width="50%">
									    <itemtemplate>
									    <%--<asp:Label ID="Label2" runat="server" AssociatedControlID="chkSelect">--%>
                                        <asp:Label ID="Label5" runat="server">
									        <asp:HyperLink ID="HyperLink2" runat="server" 
                                                NavigateUrl='<%# Eval("name", "ProductDetail.aspx?prdname={0}") %>'									    
                                                OnClick="javascript:var prodDetailWin = window.open(urlEncode(this.href), 'ProductDetails', 'toolbars=no,status=no,scrollbars=yes,resizable=yes,height=400,width=500');prodDetailWin.focus();return false;"
									            text='<%#DataBinder.Eval(Container.DataItem, "name")%>'>	
									        </asp:HyperLink> 
                                            &nbsp;&nbsp;
									    </asp:Label>
                                        <asp:Image ID="Image8" runat="server" ImageUrl="~/images/inactive.png" AlternateText="Inactive Drug" ToolTip="Inactive Drug"
                                                   Visible='<%# ((Eval("IsDeleted") + String.Empty).Contains("Y")) %>' />
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

							    <%--<asp:boundcolumn HeaderStyle-HorizontalAlign="Left" 
											     HeaderText="Product Detail" 
											     SortExpression="ProductDetail" 
											     DataField="ProductDetail" 
											     HeaderStyle-Width="25%"
                                                 Visible="false" >
                                        <HeaderStyle HorizontalAlign="Left" Width="1%"></HeaderStyle>
                                </asp:boundcolumn>--%>
						    </columns>
					    </lasa:fdagrid>
                        </asp:Panel>
					    <div class="errorMessage">
						    <asp:label id="gridTextMessageAll" Runat="server" Visible="False" />
					    </div>
				    </div>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
        </cc1:TabContainer>


				<div class="searchImage">
					<%--<asp:imagebutton id="addToWatchBottom" 
									 onmouseover="Swap(this, addwatch2)" 
									 onmouseout="Swap(this, addwatch1)"
									 causesvalidation="False" 
									 AlternateText="Add selected words to a watchlist" 
									 ImageUrl="~/images/addtowatchlist.jpg"
									 BorderWidth="0" 
									 Runat="server" 
									 CssClass="searchImage" />
                        &nbsp;&nbsp;&nbsp;&nbsp;--%>
					<asp:imagebutton id="exportToExcelBottom" 
									onmouseover="Swap(this, exporttoexcel2)" 
									onmouseout="Swap(this, exporttoexcel1)"
									causesvalidation="False" 
									AlternateText="Export Results To Excel" 
									ImageUrl="~/images/exporttoexcel.jpg"
									BorderWidth="0" 
									Runat="server" 
									CssClass="searchImage" />
				</div>

				<%-- COMMENTED AS PER 3.0 Requirements
                <poca:searchbox id="SearchResultsBox" runat="server" />--%>

              </ContentTemplate>
              </asp:UpdatePanel>
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
</html>
