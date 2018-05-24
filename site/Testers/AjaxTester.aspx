<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AjaxTester.aspx.cs" Inherits="Testers_AjaxTester" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <script type="text/javascript">

    function genError(){
        PageMethods.GenerateError(onSuccess, onFailure);
    }
        
    function go(){
        PageMethods.GetDateTime($get("Text1").value, onSuccess, onFailure);
    }

    function onSuccess(result, context, methodName) { 
        $get("result").innerHTML = result.Date + "<BR />" + result.Name;
    } 

    function onFailure(ex, context, methodName) { 
        alert(ex.get_exceptionType()); 
        alert(ex.get_message());
        alert(ex.get_stackTrace());
        alert(ex.get_statusCode());
        alert(ex.get_timedOut());
    } 

    </script>
    <form id="form1" runat="server">
    <%--<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" />--%>
    <div>
        <input id="Text1" type="text" />
        <input id="Button1" type="button" value="Go" onclick="go();" />
        <input id="Button2" type="button" value="Generate Error" onclick="genError();" />
        <br />
        <div id="result">
        
        </div>
    </div>
    </form>
</body>
</html>
