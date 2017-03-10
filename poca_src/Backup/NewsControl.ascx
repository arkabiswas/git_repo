<%@ Control Language="vb" AutoEventWireup="false" Codebehind="NewsControl.ascx.vb" Inherits="PPC.NewsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<link href="NewsControl.css" rel="stylesheet" type="text/css">
<div class="NewsBox">
	<asp:DataList id="DataList1" runat="server" width="300px">
		<HeaderTemplate>
			<table class="trimmer"><tr><th class="NewsHeader">			
			            News
			        </th>
			    </tr>
			</table>
		</HeaderTemplate>
		<ItemTemplate>
			<table width="100%">
				<tr class="Headline">				    				    	
				    <th>			  
					    <a href="javascript:void(0);" onclick="window.open('news.aspx?newsid=<%# DataBinder.Eval(Container.DataItem, "News_ID") %>','News_Window', 'width=245, height=245, toolbar=no, scrollbars=yes');"><%# DataBinder.Eval(Container.DataItem, "HeadLine") %></a>					
				    </th>					
				</tr>
				<tr class="Teaser">
					<td>
						<%# DataBinder.Eval(Container.DataItem, "Teaser") %>
					</td>
				</tr>
			</table>
		</ItemTemplate>
		<HeaderStyle CssClass="NewsHeader"></HeaderStyle>
	</asp:DataList>
	<br>
	<asp:Label ID="NewsError" Runat="server" ForeColor="Red" />
</div>