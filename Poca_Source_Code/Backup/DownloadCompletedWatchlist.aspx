<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DownloadCompletedWatchlist.aspx.vb" Inherits="PPC.DownloadCompletedWatchlist" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>DownloadCompletedWatchlist</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <body>

    <form id="Form1" method="post" runat="server">			
   
		<table cellSpacing="0" cellPadding="0" width="100%" summary="This table is used for page layout.">
			<TBODY>
				<td vAlign="top">
				<tr>
				<table cellspacing="0" cellpadding="2">
					<tr>
						<td align="right"><asp:label id="Label1" runat="server">Proprietary Name</asp:label>&nbsp;</td>
						<td><asp:label id="lblProductName" tabIndex="1" runat="server"></asp:label></td>
					</tr>
					<tr>
						<td align="right"><asp:Label id="Label4" runat="server" Width="130px">Alternative Name</asp:Label>&nbsp;</td>
						<td><asp:label id="lblAlternativeName" runat="server" tabIndex="1" /></td>
					</tr>
					<tr>
						<td align="right"><asp:Label id="Label6" runat="server" Width="115px">Established Name</asp:Label>&nbsp;</td>
						<td><asp:label id="lblEstablishedName" runat="server" tabIndex="2" /></td>
					</tr>
					<tr>
						<td align="right"><asp:label id="Label2" runat="server">Product Modifier</asp:label>&nbsp;</td>
						<td><asp:label id="lblProductModifier" tabIndex="3" runat="server"></asp:label></td>
					</tr>
					<tr>
						<td align="right"><asp:label id="Label5" runat="server">Date Received</asp:label>&nbsp;</td>
						<td><asp:label id="lblDTReceived" tabIndex="4" runat="server"></asp:label></td>
					</tr>
					<tr>
						<td align="right"><asp:label id="EPDDateLabel" runat="server">EPD Date</asp:label>&nbsp;</td>
						<td><asp:label id="lblEPDDate" runat="server" tabIndex="5" /></td>
					</tr>
					<tr>
						<td align="right"><asp:label id="Label7" runat="server">Consult Number</asp:label>&nbsp;</td>
						<td><asp:label id="lblConsultNumber" tabIndex="6" runat="server"></asp:label></td>
					</tr>
					<tr>
						<td align="right"><asp:label id="Label8" runat="server">Application Number</asp:label>&nbsp;</td>
						<td><asp:label id="lblAppNumber" tabIndex="7" runat="server"></asp:label></td>
					</tr>
					<tr>
						<td align="right"><asp:label id="Label9" runat="server">Application Type</asp:label>&nbsp;</td>
						<td><asp:label id="lblAppType" tabIndex="8" runat="server"></asp:label></td>
					</tr>
					<tr>
						<td align="right"><asp:Label id="Label10" runat="server" Width="98px">Comments</asp:Label>&nbsp;</td>
						<td><asp:label id="lblComments" runat="server" tabIndex="9"></asp:label></td>
					</tr>
				</table>
    </form>

  </body>
</html>
