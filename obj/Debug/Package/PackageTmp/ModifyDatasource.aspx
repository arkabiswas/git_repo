<%@ Register TagPrefix="poca" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ModifyDatasource.aspx.vb" Inherits="PPC.ModifyDatasource" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>ModifyDatasource</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="Styles.css" type="text/css" rel="stylesheet" />
		<style type="text/css">
		        #ModifyDatasourceList { FONT-SIZE: 12px; MARGIN: 55px 100px 0px; FONT-FAMILY: verdana }
	            .addDatasource { MARGIN-LEFT: 10px }
	            #ModifyDatasourceList TFOOT { COLOR: white; BACKGROUND-COLOR: blue }
	            #ModifyDatasourceList THEAD { COLOR: white; BACKGROUND-COLOR: blue }
		</style>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<poca:trackingheader id="TrackingHeader1" runat="server" location="<a href='Settings.aspx' title='Settings' style='font-size: 14px; font-weight: bold;'>Return To Settings</a>" title="Modify Datasource Name" />		
			<table border="0" style="width: 447px; height: 50px; margin-left: 100px;">
                <tr>
	                <td>						
		                <p>					                    
		                Select the "Edit" button to the left of the Datasource Name that you wish to edit. Once the name change is complete, make sure
		                to select the "Update" button to successfully update the data.</p>
	                </td>
                </tr>
            </table>
			<div id="xx">			
				<asp:repeater id="rpDatasource" runat="server">
					<HeaderTemplate>			
						<table id="ModifyDatasourceList" cellpadding="2" cellspacing="0" rules="rows" bordercolor="black" border="1">
							<thead>
								<tr>
									<th>
										&nbsp;
									</th>
									<th style="FONT-SIZE: 14px; COLOR: white; TEXT-ALIGN: left">
										Datasource Name
									</th>
								</tr>
							</thead>
							<tbody>
					</HeaderTemplate>
					<ItemTemplate>
						<tr>
							<td>
								<asp:Button id="btnEdit" runat="server" Text="Edit" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LIST_ITEM") %>' Visible="True">
								</asp:Button>
								<asp:Button id="btnUpdate" runat="server" Text="Update" Visible="False" CommandName="Update" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LIST_ITEM") & ";" & SetPickListId( DataBinder.Eval(Container.DataItem, "PICK_LIST_ID") ) & ";" & DataBinder.Eval(Container.DataItem, "PICK_LIST_ITEM_ID") %>'>
								</asp:Button>
								<asp:Button id="btnCancel" runat="server" Text="Cancel" Visible="False" CommandName="Cancel" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LIST_ITEM") %>'>
								</asp:Button>
							</td>
							<td style="vertical-align:top;">
								<asp:Label id="lblDatasource" runat="server" Visible="True" >
									<%# DataBinder.Eval(Container.DataItem, "DESCRIPTION") %>
								</asp:Label>
								<asp:Label id="Label1" runat="server" Visible="False" Text="Name: " AssociatedControlID="txtDatasource" />
								<asp:TextBox id="txtDatasource" runat="server" style="width:300px" Text='<%# DataBinder.Eval(Container.DataItem, "DESCRIPTION") %>' Visible="False">
								</asp:TextBox>
								<asp:Label id="Label2" runat="server" Visible="False" Text="Description: " AssociatedControlID="txtDatasourceDesc" />
								<asp:TextBox id="txtDatasourceDesc" TextMode="MultiLine" runat="server" style="width:300px; height:100px" Text='<%# DataBinder.Eval(Container.DataItem, "DETAIL_DESCRIPTION") %>' Visible="False">
								</asp:TextBox>
								<asp:Button id="btnDelete" runat="server" Text="Delete" Visible="False" CommandName="Delete" CommandArgument='<%# SetPickListId( DataBinder.Eval(Container.DataItem, "PICK_LIST_ID") ) & ";" & DataBinder.Eval(Container.DataItem, "PICK_LIST_ITEM_ID") %>'>
								</asp:Button>
							</td>
						</tr>
					</ItemTemplate>
					<FooterTemplate>
						</tbody>
						<tfoot>
							<tr>
								<td colspan="2">
								<asp:Label id="lblDatasource" runat="server" AssociatedControlID="txtDatasourceAdd">Add a new datasource:</asp:Label>									
									<br>
									<asp:TextBox id="txtDatasourceAdd" CssClass="addDatasource" runat="server" Visible="True" Width="200"></asp:TextBox>
									<asp:Button id="btnAdd" runat="server" Text="Add" CommandName="Add" CommandArgument='<%# _pickListId %>'>
									</asp:Button>
								</td>
							</tr>
						</tfoot>
						</table>
					</FooterTemplate>
				</asp:repeater>
			</div>
		</form>
	</body>
</html>
