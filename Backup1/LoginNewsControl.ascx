<%@ Control Language="vb" AutoEventWireup="false" Codebehind="LoginNewsControl.ascx.vb" Inherits="PPC.LoginNewsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<LINK href="LoginNewsControl.css" type="text/css" rel="stylesheet" />
<asp:datalist id="DataList1" runat="server" RepeatColumns="1">
	<ItemTemplate>
		<table width="100%">
			<tr class="Headline">
				<td>
					<%# DataBinder.Eval(Container.DataItem, "HeadLine") %>
				</td>
			</tr>
			<tr class="Teaser">
				<td>
					<%# DataBinder.Eval(Container.DataItem, "Teaser") %>
				</td>
			</tr>
		</table>
	</ItemTemplate>
</asp:datalist><br>
<asp:label id="NewsError" Visible="False" ForeColor="Red" Runat="server"></asp:label>
