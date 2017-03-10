<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Comments.aspx.vb" Inherits="PPC.Comments" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
		<title>Comments and Feedback Form</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="scripts.js" type="text/javascript"></script>
		<noscript  style="color:Red;font-weight: bold; font-size:medium;">Your browser does not support JavaScript!</noscript>
    </head>
	<body MS_POSITIONING="FlowLayout">
		<script language="javascript" type="text/javascript">
		<!--
			if(document.images)
			{
				var submit1 = new Image();
				submit1.src = "Images/submit.jpg";
				
				var submit2 = new Image();
				submit2.src = "Images/submit2.jpg";
			}
		-->
		</script>
		<form id="frmComments" method="post" runat="server">
			<uc1:trackingheader id="TrackingHeader1" runat="server" title="Comments and Feedback" location="<A title='Return to Login Screen' href='Home.aspx' style='font-size: 14px; font-weight: bold; '>Return To Home Page</A>" />
			<asp:panel id="panelEnterComments" runat="server" style=" left:120px; position:absolute;">
                <table style="WIDTH: 681px; HEIGHT: 293px;" cellspacing="5px" cellpadding="5px" summary="This table is used for formatting" border="0">
                    <tr>
                        <td>
                            <asp:Label id="lblFrom" runat="server" AssociatedControlID="txtComments"></asp:Label>,&nbsp;please enter your comments below:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox id="txtComments" runat="server" TextMode="MultiLine" BorderColor="Black" name="txtComments" rows="14" cols="45" Height="277px" Width="776px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label id="MessageLabel" runat="server" Width="250px" Visible="False"></asp:Label>
                            &nbsp;&nbsp;
                            <asp:imagebutton id="btnImageButton" onmouseover="Swap(this,submit2);" onmouseout="Swap(this,submit1);" runat="server" alternatetext="Click here to send your feedback." imageurl="Images/submit.jpg"></asp:imagebutton>
                        </td>
                    </tr>
                </table>
			</asp:panel>
			<asp:panel id="panelCommentsSent" runat="server" Visible="False" style="left:120px; position:absolute; ">
                <asp:label id="CommentsSent" runat="server">
					Your comments have been sent to the System Administrator, please <a title="Click here to return Home" href="Home.aspx">click here</a> to return Home.
				</asp:label>
			</asp:panel>			
		</form>
	</body>
</html>
