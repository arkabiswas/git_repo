<%@ Control Language="vb" AutoEventWireup="false" Codebehind="NewsControl.ascx.vb" Inherits="PPC.NewsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<link href="NewsControl.css" rel="stylesheet" type="text/css" />
<div class="NewsBox">
	<asp:DataList id="DataList1" runat="server" width="340px">
		<HeaderTemplate>
			<%--<table class="trimmer"><tr><th class="NewsHeader">--%>
			<table><tr><th class="NewsHeader">			
			            News
			        </th>
			    </tr>
			</table>
		</HeaderTemplate>
		<ItemTemplate>
			<table width="100%">
				<tr class="Headline">				    				    	
				    <td>			  
					    <a href="javascript:void(0);" onclick="window.open('news.aspx?newsid=<%# DataBinder.Eval(Container.DataItem, "News_ID") %>','News_Window', 'width=245, height=245, toolbar=no, scrollbars=yes');"><%# DataBinder.Eval(Container.DataItem, "HeadLine") %></a>					
				    </td>					
				</tr>
				<tr class="Teaser">
					<td>
						<%# DataBinder.Eval(Container.DataItem, "Teaser") %>
					</td>
				</tr>
			</table>
		</ItemTemplate>
        <FooterTemplate>
            <asp:Label  Visible='<%# Boolean.Parse((DataList1.Items.Count=0).ToString())%>'
                        runat="server" ID="lblNoRecord" Text="No News Available.">
            </asp:Label>
        </FooterTemplate>
        <FooterStyle Font-Size="12px" Height="30px" />
		<HeaderStyle CssClass="NewsHeader"></HeaderStyle>
	</asp:DataList>
	<br/>
	<asp:Label ID="NewsError" Runat="server" ForeColor="Red" />
</div>