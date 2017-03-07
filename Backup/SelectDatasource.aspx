<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SelectDatasource.aspx.vb" Inherits="PPC.SelectDatasource" %>
<%@ Register TagPrefix="poca" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<HTML>
	<HEAD>
		<title>Datasource Refresh started.</title>
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
			<style>
			#selectDatasourceTable { MARGIN: 55px 100px 0px }
			#selectDatasourceTable TABLE { WIDTH: 80% }
			#selectDatasourceTable THEAD { BORDER-RIGHT: medium none; BORDER-TOP: medium none; BORDER-LEFT: medium none; BORDER-BOTTOM: medium none }
			#selectDatasourceTable TH { PADDING-RIGHT: 10px; PADDING-LEFT: 10px; PADDING-BOTTOM: 10px; PADDING-TOP: 10px }
			.titleRow { FONT-WEIGHT: bold; TEXT-DECORATION: underline }
			</style>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<poca:trackingheader id="TrackingHeader1" runat="server" location="<a href='Settings.aspx' title='Settings'>Return To Settings</a>"
				title="Datasource Maintenance" />
			<div id="selectDatasourceTable">
				<table>
					<asp:repeater id="rpFileList" runat="server">
						<HeaderTemplate>
							<thead>
								<tr>
									<th colspan="3">
										Select the file that will be used to refresh/load the datasource.</th>
								</tr>
							</thead>
							<tr class="titleRow">
								<td>File Name</td>
								<td>File Size</td>
								<td>File Date</td>
							</tr>
						</HeaderTemplate>
						<ItemTemplate>
							<tr style="background-color: #eee;">
								<td>
									<asp:LinkButton id="lbtnFileNameOdd" runat="server" CommandName="loadFile" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "filename") %>'>
										<%# DataBinder.Eval(Container.DataItem, "filename") %>
									</asp:LinkButton>
								</td>
								<td>
									<%# String.Format("{0:#,#}", Math.Ceiling(CDbl(DataBinder.Eval(Container.DataItem, "filesize")) / 1024)) & " KB"%>
								</td>
								<td>
									<%# DataBinder.Eval(Container.DataItem, "filedate", "{0:MM-dd-yyyy}") %>
								</td>
							</tr>
						</ItemTemplate>
						<AlternatingItemTemplate>
							<tr style="background-color: #fff;">
								<td>
									<asp:LinkButton id="lbtnFileNameEven" runat="server" CommandName="loadFile" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "filename") %>'>
										<%# DataBinder.Eval(Container.DataItem, "filename") %>
									</asp:LinkButton>
								</td>
								<td>
									<%# String.Format("{0:#,#}", Math.Ceiling(CDbl(DataBinder.Eval(Container.DataItem, "filesize")) / 1024)) & " KB"%>
								</td>
								<td>
									<%# DataBinder.Eval(Container.DataItem, "filedate", "{0:MM-dd-yyyy}") %>
								</td>
							</tr>
						</AlternatingItemTemplate>
					</asp:repeater>
				</table>
			</div>
		</form>
	</body>
</HTML>
