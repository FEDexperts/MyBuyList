<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoggerTester.aspx.cs" Inherits="Testers_LoggerTester" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Logger tester</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <a target="_blank" href="http://logging.apache.org/log4net/release/config-examples.html">Log4Net configuration
            examples</a>
        <pre>
CREATE TABLE [dbo].[Log] (
    [Id] [int] IDENTITY (1, 1) NOT NULL,
    [Date] [datetime] NOT NULL,
    [Thread] [varchar] (255) NOT NULL,
    [Level] [varchar] (50) NOT NULL,
    [Logger] [varchar] (255) NOT NULL,
    [Message] [varchar] (4000) NOT NULL,
    [Exception] [varchar] (2000) NULL
)
        </pre>
    </div>
    </form>
</body>
</html>
