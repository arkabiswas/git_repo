<%@ Page Language="vb" AutoEventWireup="false" Codebehind="UARReport.aspx.vb" Inherits="PPC.UARReport" %>
<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"  %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head runat="server">
		<title>User Access Report</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="Styles.css" type="text/css" rel="stylesheet" />
		<script language="javascript" src="scripts.js" type="text/javascript"></script>
		<style type="text/css">
			#uarPageContainer { MARGIN-TOP: 20px; MARGIN-LEFT: 30px }
			#uarPageContainer H2 { PADDING-RIGHT: 5px; PADDING-LEFT: 5px; FONT-SIZE: 100%; PADDING-BOTTOM: 5px; PADDING-TOP: 5px; BACKGROUND-COLOR: #eee }
			#uarPageContainer #leftcolumn { PADDING-RIGHT: 15px; FLOAT: left; TOP: -10px }
			#uarPageContainer #rightcolumn { FLOAT: left; TOP: -10px }
		</style>
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
                border: solid 1px #d7d7d7;
                border-top-color: #d7d7d7;
            }            
        </style>
	</head>

	<body MS_POSITIONING="GridLayout">
		<form id="UARReportForm" method="post" runat="server">
			<uc1:trackingheader id="TrackingHeader1" title="User Administration" runat="server" location="<a href='Settings.aspx' style='font-size: 14px; font-weight: bold; '>Return to Settings</a>" />

            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="True"  />

			<table cellspacing="0" cellpadding="0" width="100%" summary="This table is used for page layout.">
				<tbody>
					<tr>
                        <td style="width:55px; ">
                            &nbsp;
                        </td>
                        <td>
                            <div id="uarPageContainer">
				                <h2>
					                User Access Report
				                </h2>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" Text="Enter Begin Date:" Font-Bold="true"></asp:Label>
                                            &nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtBeginDate" Width="120px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="calBeginDate" runat="server" TargetControlID="txtBeginDate"></cc1:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;(mm/dd/yyyy)"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" Text="Enter End Date:" Font-Bold="true"></asp:Label>
                                            &nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtEndDate" Width="120px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="calEndDate" runat="server" TargetControlID="txtEndDate"></cc1:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;(mm/dd/yyyy)"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td colspan="2">
                                            <%--<asp:Button runat="server" ID="btnSubmit" Text="Submit" />
                                            <asp:Button runat="server" ID="btnExportToExcel" Text="Export To Excel" />--%>
                                            <asp:imagebutton id="btnSubmit" 
                                                             onmouseover="Swap(this, submit2);" 
                                                             onmouseout="Swap(this, submit1);" 
					                                         runat="server" 
                                                             imageurl="images/submit.jpg" 
                                                             alternatetext="Submit Search" />
                                            &nbsp;&nbsp;
						                    <asp:imagebutton id="btnExportToExcel" 
										                    onmouseover="Swap(this, exporttoexcel2)" 
										                    onmouseout="Swap(this, exporttoexcel1)"
										                    causesvalidation="False" 
										                    AlternateText="Export Results To Excel" 
										                    ImageUrl="~/images/exporttoexcel.jpg"
										                    Runat="server" />
                                        </td>
                                    </tr>
				                    <tr>
                                        <td>
                                        </td>
					                    <td colspan="2">
						                    <asp:label id="MessageLabel" runat="server" Width="227px" ForeColor="Red"></asp:label>
                                        </td>
				                    </tr>
                                    <tr style="height:20px;">
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>

                                <table>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="uarUpdatePanel" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <cc1:TabContainer ID="uarTabContainer" runat="server" CssClass="MyTabStyle" >

                                                    <cc1:TabPanel ID="tabpanelAll" runat="server"  HeaderText="All">
                                                        <HeaderTemplate>
                                                            <div class="divTab" >
                                                                <asp:Label ID="Label3" runat="server" Text="All"></asp:Label>
                                                            </div>
                                                        </HeaderTemplate>
                                                        <ContentTemplate>
                                                            <div class="divGrid">
                                        <asp:label id="gridHeaderAll" runat="server" CssClass="gridHeader" />
                                        <asp:Panel id="panelAll"  runat="server" Height="250px" Width="800px" ScrollBars="Auto" BorderColor="LightBlue" BorderWidth="1px" Visible="true" >
					    					<asp:GridView id="gridAll" Runat="server" Visible="True" Width="100%" UseAccessibleHeader="True" Font-Size="120%" AllowPaging="false" AutoGenerateColumns="false" AllowSorting="false">
                                                <PagerStyle HorizontalAlign="Right" BackColor="Gainsboro"></PagerStyle>
                                                <AlternatingRowStyle BackColor="#E0E0E0"></AlternatingRowStyle>
                                                <RowStyle BackColor="White"></RowStyle>
                                                <HeaderStyle HorizontalAlign="Center" BackColor="Blue" Font-Bold="True" ForeColor="White" Font-Size="Larger"></HeaderStyle>
						                        <columns>
							                        <asp:BoundField  HeaderStyle-HorizontalAlign="Left" 
											                         HeaderText="User Name" 
											                         SortExpression="U_USERNAME" 
											                         DataField="U_USERNAME" >
                                                    </asp:BoundField>
						                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" 
											                           HeaderText="Full Name">
							                            <HeaderStyle Wrap="False"></HeaderStyle>
							                            <ItemStyle Wrap="False"></ItemStyle>
							                            <ItemTemplate>
								                            <asp:Label ID="lblFirstName" Text='<%#Eval("U_FIRST_NAME") %>' runat="server"></asp:Label>
								                            <asp:Label ID="lblLastName" Text='<%#Eval("U_LAST_NAME") %>' runat="server"></asp:Label>
							                            </ItemTemplate>
						                            </asp:TemplateField>
							                        <asp:BoundField  HeaderStyle-HorizontalAlign="Left" 
											                         HeaderText="Email" 
											                         SortExpression="U_EMAIL" 
											                         DataField="U_EMAIL" >
                                                    </asp:BoundField>
							                        <asp:BoundField HeaderStyle-HorizontalAlign="Left" 
											                         HeaderText="User Group" 
											                         SortExpression="U_USER_GROUP_NAME" 
											                         DataField="U_USER_GROUP_NAME" >
                                                    </asp:BoundField>
						                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left"
											                           HeaderText="Status"
                                                                       SortExpression="STATUS">
							                            <HeaderStyle Wrap="False"></HeaderStyle>
							                            <ItemStyle Wrap="False"></ItemStyle>
							                            <ItemTemplate>
								                            <asp:Label ID="lblStatus" Text='<%# IIf(Eval("STATUS").ToString() = "1", "Enabled", "Disabled")%>' runat="server"></asp:Label>
							                            </ItemTemplate>
						                            </asp:TemplateField>
							                        <%--<asp:BoundField HeaderStyle-HorizontalAlign="Left" 
											                         HeaderText="Create/Update Date" 
											                         SortExpression="CREATE_DISABLED_DT" 
											                         DataField="CREATE_DISABLED_DT" >
                                                    </asp:BoundField>--%>
						                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left"
											                           HeaderText="Create/Update Date"
                                                                       SortExpression="CREATE_DISABLED_DT">
							                            <HeaderStyle Wrap="False"></HeaderStyle>
							                            <ItemStyle Wrap="False"></ItemStyle>
							                            <ItemTemplate>
								                            <asp:Label ID="lblStatusDate" Text='<%# Bind("CREATE_DISABLED_DT", "{0:MM/dd/yyyy}") %>' runat="server"></asp:Label>
							                            </ItemTemplate>
						                            </asp:TemplateField>
 						                        </columns>
		                                    </asp:GridView>
                                        </asp:Panel>
                                                            </div>
                                                        </ContentTemplate>
                                                    </cc1:TabPanel>

                                                    <cc1:TabPanel ID="tabpanelEnabled" runat="server"  HeaderText="Enabled">
                                                        <HeaderTemplate>
                                                            <div class="divTab" >
                                                                <asp:Label ID="lblEnabled" runat="server" Text="Enabled"></asp:Label>
                                                            </div>
                                                        </HeaderTemplate>
                                                        <ContentTemplate>
                                                            <div class="divGrid">
                                       <asp:label id="gridHeaderEnabled" runat="server" CssClass="gridHeader" />
                                       <asp:Panel id="panelEnabled"  runat="server" Height="250px" Width="800px" ScrollBars="Auto" BorderColor="LightBlue" BorderWidth="1px" Visible="true" >
					    					<asp:GridView id="gridEnabled" Runat="server" Visible="True" Width="100%" UseAccessibleHeader="True" Font-Size="120%" AllowPaging="false" AutoGenerateColumns="false" AllowSorting="false">
                                                <PagerStyle HorizontalAlign="Right" BackColor="Gainsboro"></PagerStyle>
                                                <AlternatingRowStyle BackColor="#E0E0E0"></AlternatingRowStyle>
                                                <RowStyle BackColor="White"></RowStyle>
                                                <HeaderStyle HorizontalAlign="Center" BackColor="Blue" Font-Bold="True" ForeColor="White" Font-Size="Larger"></HeaderStyle>
						                        <columns>
							                        <asp:BoundField  HeaderStyle-HorizontalAlign="Left" 
											                         HeaderText="User Name" 
											                         SortExpression="U_USERNAME" 
											                         DataField="U_USERNAME" >
                                                    </asp:BoundField>
						                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" 
											                           HeaderText="Full Name">
							                            <HeaderStyle Wrap="False"></HeaderStyle>
							                            <ItemStyle Wrap="False"></ItemStyle>
							                            <ItemTemplate>
								                            <asp:Label ID="lblFirstName" Text='<%#Eval("U_FIRST_NAME") %>' runat="server"></asp:Label>
								                            <asp:Label ID="lblLastName" Text='<%#Eval("U_LAST_NAME") %>' runat="server"></asp:Label>
							                            </ItemTemplate>
						                            </asp:TemplateField>
							                        <asp:BoundField  HeaderStyle-HorizontalAlign="Left" 
											                         HeaderText="Email" 
											                         SortExpression="U_EMAIL" 
											                         DataField="U_EMAIL" >
                                                    </asp:BoundField>
							                        <asp:BoundField HeaderStyle-HorizontalAlign="Left" 
											                         HeaderText="User Group" 
											                         SortExpression="U_USER_GROUP_NAME" 
											                         DataField="U_USER_GROUP_NAME" >
                                                    </asp:BoundField>
						                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left"
											                           HeaderText="Status"
                                                                       SortExpression="STATUS">
							                            <HeaderStyle Wrap="False"></HeaderStyle>
							                            <ItemStyle Wrap="False"></ItemStyle>
							                            <ItemTemplate>
								                            <asp:Label ID="lblStatus" Text='<%# IIf(Eval("STATUS").ToString() = "1", "Enabled", "Disabled")%>' runat="server"></asp:Label>
							                            </ItemTemplate>
						                            </asp:TemplateField>
							                        <%--<asp:BoundField HeaderStyle-HorizontalAlign="Left" 
											                         HeaderText="Create/Update Date" 
											                         SortExpression="CREATE_DISABLED_DT" 
											                         DataField="CREATE_DISABLED_DT" >
                                                    </asp:BoundField>--%>
						                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left"
											                           HeaderText="Create/Update Date"
                                                                       SortExpression="CREATE_DISABLED_DT">
							                            <HeaderStyle Wrap="False"></HeaderStyle>
							                            <ItemStyle Wrap="False"></ItemStyle>
							                            <ItemTemplate>
								                            <asp:Label ID="lblStatusDate" Text='<%# Bind("CREATE_DISABLED_DT", "{0:MM/dd/yyyy}") %>' runat="server"></asp:Label>
							                            </ItemTemplate>
						                            </asp:TemplateField>
 						                        </columns>
		                                    </asp:GridView>
                                        </asp:Panel>
                                                            </div>
                                                        </ContentTemplate>
                                                    </cc1:TabPanel>

                                                    <cc1:TabPanel ID= "tabpanelDisabled" runat="server"  HeaderText="Disabled">
                                                        <HeaderTemplate>
                                                            <div class="divTab" >
                                                                <asp:Label ID="lblDisabled" runat="server" Text="Disabled"></asp:Label>
                                                            </div>
                                                        </HeaderTemplate>
                                                        <ContentTemplate>
                                                            <div class="divGrid">
                                       <asp:label id="gridHeaderDisabled" runat="server" CssClass="gridHeader" />
                                        <asp:Panel id="pnlDisabled"  runat="server" Height="250px" Width="800px" ScrollBars="Auto" BorderColor="LightBlue" BorderWidth="1px" Visible="true" >
					    					<asp:GridView id="gridDisabled" Runat="server" Visible="True" Width="100%" UseAccessibleHeader="True" Font-Size="120%" AllowPaging="false" AutoGenerateColumns="false" AllowSorting="false">
                                                <PagerStyle HorizontalAlign="Right" BackColor="Gainsboro"></PagerStyle>
                                                <AlternatingRowStyle BackColor="#E0E0E0"></AlternatingRowStyle>
                                                <RowStyle BackColor="White"></RowStyle>
                                                <HeaderStyle HorizontalAlign="Center" BackColor="Blue" Font-Bold="True" ForeColor="White" Font-Size="Larger"></HeaderStyle>
						                        <columns>
							                        <asp:BoundField HeaderStyle-HorizontalAlign="Left" 
											                         HeaderText="User Name" 
											                         SortExpression="U_USERNAME" 
											                         DataField="U_USERNAME" >
                                                    </asp:BoundField>
						                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" 
											                           HeaderText="Full Name">
							                            <HeaderStyle Wrap="False"></HeaderStyle>
							                            <ItemStyle Wrap="False"></ItemStyle>
							                            <ItemTemplate>
								                            <asp:Label ID="lblFirstName" Text='<%#Eval("U_FIRST_NAME") %>' runat="server"></asp:Label>
								                            <asp:Label ID="lblLastName" Text='<%#Eval("U_LAST_NAME") %>' runat="server"></asp:Label>
							                            </ItemTemplate>
						                            </asp:TemplateField>
							                        <asp:BoundField HeaderStyle-HorizontalAlign="Left" 
											                         HeaderText="Email" 
											                         SortExpression="U_EMAIL" 
											                         DataField="U_EMAIL" >
                                                    </asp:BoundField>
							                        <asp:BoundField HeaderStyle-HorizontalAlign="Left" 
											                         HeaderText="User Group" 
											                         SortExpression="U_USER_GROUP_NAME" 
											                         DataField="U_USER_GROUP_NAME" >
                                                    </asp:BoundField>
						                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" 
											                           HeaderText="Status"
                                                                       SortExpression="STATUS">
							                            <HeaderStyle Wrap="False"></HeaderStyle>
							                            <ItemStyle Wrap="False"></ItemStyle>
							                            <ItemTemplate>
								                            <asp:Label ID="lblStatus" Text='<%# IIf(Eval("STATUS").ToString() = "1", "Enabled", "Disabled")%>' runat="server"></asp:Label>
							                            </ItemTemplate>
						                            </asp:TemplateField>
							                        <%--<asp:BoundField HeaderStyle-HorizontalAlign="Left" 
											                         HeaderText="Create/Update Date" 
											                         SortExpression="CREATE_DISABLED_DT" 
											                         DataField="CREATE_DISABLED_DT" >
                                                    </asp:BoundField>--%>
						                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left"
											                           HeaderText="Create/Update Date"
                                                                       SortExpression="CREATE_DISABLED_DT">
							                            <HeaderStyle Wrap="False"></HeaderStyle>
							                            <ItemStyle Wrap="False"></ItemStyle>
							                            <ItemTemplate>
								                            <asp:Label ID="lblStatusDate" Text='<%# Bind("CREATE_DISABLED_DT", "{0:MM/dd/yyyy}") %>' runat="server"></asp:Label>
							                            </ItemTemplate>
						                            </asp:TemplateField>
 						                        </columns>
		                                    </asp:GridView>
                                        </asp:Panel>
                                                            </div>
                                                        </ContentTemplate>
                                                    </cc1:TabPanel>
                                                </cc1:TabContainer>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>            
					</tr>
				</tbody>
			</table>

		</form>
	</body>
</html>
