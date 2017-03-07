<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Help.aspx.vb" Inherits="PPC.Help" %>
<%@ Register TagPrefix="uc1" TagName="TrackingHeader" Src="TrackingHeader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>Help</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="Styles.css" type=text/css rel=stylesheet ></link>
  </head>
  <body MS_POSITIONING="GridLayout">

    <form id="Form1" method="post" runat="server">
		<uc1:TrackingHeader id=TrackingHeader1 
							runat="server" 
							Title="Help" 
							Location="<a href='Home.aspx' title='Home'>Home</a> | Help" />

    </form>

  </body>
</html>
