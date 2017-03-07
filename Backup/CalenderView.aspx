<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CalenderView.aspx.vb" Inherits="PPC.CalenderView" %>
<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>EPD Calender</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="CalenderView" method="post" runat="server">
			<uc1:TrackingHeader id="TrackingHeader1" runat="server" location="<a href='Home.aspx' alt='Click here to return to the home screen'>Return Home</a>" title="EPD Calendar" />
			<table cellspacing="0" cellpadding="0" width="100%" summary="This table is used for page layout.">
				<tr>
					<td valign="top">
						<table class="tableFont" id="Table1" cellspacing="1" cellpadding="1" width="75%" border="0" >
							<tr>                                
								<td>
									<asp:Calendar id="EPDCalender" runat="server" BorderColor="#3366CC" Width="616px" Height="424px" CellPadding="1" BorderWidth="1px" BackColor="White" DayNameFormat="Full" ForeColor="#003399" Font-Size="150%" Font-Names="Verdana" SelectionMode="None">
										<TodayDayStyle ForeColor="White" BackColor="#99CCCC"></TodayDayStyle>
										<SelectorStyle ForeColor="#336666" BackColor="#99CCCC"></SelectorStyle>
										<DayStyle Font-Size="100%" HorizontalAlign="Left" VerticalAlign="Top"></DayStyle>
										<NextPrevStyle Font-Size="100%" ForeColor="#CCCCFF"></NextPrevStyle>
										<DayHeaderStyle Height="1px" ForeColor="#336666" BackColor="#99CCCC"></DayHeaderStyle>
										<SelectedDayStyle Font-Bold="True" ForeColor="#CCFF99" BackColor="#009999"></SelectedDayStyle>
										<TitleStyle Font-Size="150%" Font-Bold="True" Height="25px" BorderWidth="1px" ForeColor="#CCCCFF" BorderStyle="Solid" BorderColor="#3366CC" BackColor="#003399"></TitleStyle>
										<WeekendDayStyle BackColor="#CCCCFF"></WeekendDayStyle>
										<OtherMonthDayStyle ForeColor="#999999"></OtherMonthDayStyle>
									</asp:Calendar>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
