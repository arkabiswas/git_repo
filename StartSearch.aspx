<%@ Register TagPrefix="poca" TagName="SearchBox" Src="SearchBox.ascx" %>
<%@ Register TagPrefix="poca" TagName="DirectSearch" Src="DirectSearch.ascx" %>
<%@ Register TagPrefix="poca" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="StartSearch.aspx.vb" Inherits="PPC.StartSearch" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>Search Database</title>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
		<link href="Styles.css" type="text/css" rel="stylesheet" />
		<script language="javascript" src="scripts.js" type="text/javascript"></script>
		<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
	</head>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server" style="width:100%; min-width:1100px; height:100%;">
			<poca:TrackingHeader id="SearchStartHeader" runat="server" location="<a href='ArchiveSearch.aspx' title='Retrieve Archived Search' style='font-size: 14px; font-weight: bold; '>Retrieve Archived Search</a>"
				title="Search Database" />
			<table cellspacing="0" cellpadding="0" width="100%" summary="This table is used for page layout.">
				<tbody>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
					<tr valign="top">
                        <td style="width:85px; ">
                            &nbsp;
                        </td>
                        <td style="width:600px; ">
                            <poca:SearchBox id="StartSearchBox" runat="server" />
                        </td>

                        <td style="width:80px; ">
                            &nbsp;
                        </td>
                        <td>
                            <poca:DirectSearch id="DirectSearch" runat="server" />
                        </td>
                        <td style="width:130px; ">
                            &nbsp;
                        </td>
					</tr>
				</tbody>
			</table>
			<br/>
			<%--<asp:linkbutton id="ToggleSearchType" runat="server" causesvalidation="False">Switch to advanced search view</asp:linkbutton>--%>
		</form>
	</body>
</html>
