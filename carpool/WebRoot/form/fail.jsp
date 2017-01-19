<%@ page language="java" import="java.util.*" pageEncoding="GBK"%>

<%@ taglib uri="http://struts.apache.org/tags-bean" prefix="bean" %>
<%@ taglib uri="http://struts.apache.org/tags-html" prefix="html" %>
<%@ taglib uri="http://struts.apache.org/tags-logic" prefix="logic" %>
<%@ taglib uri="http://struts.apache.org/tags-tiles" prefix="tiles" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html:html lang="true">
  <head>
    <html:base />
    
    <title>Error.jsp</title>

	<meta http-equiv="pragma" content="no-cache">
	<meta http-equiv="cache-control" content="no-cache">
	<meta http-equiv="expires" content="0">    
	<meta http-equiv="keywords" content="keyword1,keyword2,keyword3">
	<meta http-equiv="description" content="This is my page">
	<!--
	<link rel="stylesheet" type="text/css" href="styles.css">
	-->

  </head>
  
<body bgColor=#ffffff text=#000000>
<div align=center>
<TABLE border=0 cellSpacing=0 cellPadding=0 width=500><!-- fwtable fwsrc="error.png" fwbase="error.jpg" fwstyle="Dreamweaver" fwdocid = "742308039" fwnested="0" -->
  <TBODY>
  <TR>
    <TD><IMG border=0 src="../images/spacer.gif" width=342 height=1></TD>
    <TD><IMG border=0 src="../images/spacer.gif" width=158 height=1></TD>
    <TD><IMG border=0 src="../images/spacer.gif" width=1 height=1></TD></TR>
  <TR>
    <TD rowSpan=2><IMG border=0 name=error_r1_c1 
      src="../images/error_r1_c1.jpg" width=342 height=280></TD>
    <TD><IMG border=0 name=error_r1_c2 src="../images/error_r1_c2.jpg" 
      width=158 height=238></TD>
    <TD><IMG border=0 src="../images/spacer.gif" width=1 height=238></TD></TR>
  <TR>
    <TD bgColor=#ffffff vAlign=top>
      <DIV align=center><A href="javascript:window.history.go(-1)"><IMG border=0 
      src="../images/back.gif" width=77 height=19></A></DIV></TD>
    <TD><IMG border=0 src="../images/spacer.gif" width=1 
  height=42></TD></TR></TBODY></TABLE>
<TABLE border=0 width="100%">
  <TBODY>
  <TR>
    <TD class=font height=21 align=middle>¥ÌŒÛ‘≠“Ú£∫${error}£°£°£°</TD></TR></TBODY></TABLE>
<p>&nbsp;</p></div>
</body>

</html:html>
