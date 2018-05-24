<%@ Page Language="C#" %>
<%@ Import Namespace="System.Web.Services" %>
<%@ Import Namespace="MyBuyList.BusinessLayer" %>
<%@ Import Namespace="MyBuyList.Shared" %>
<%@ Import Namespace="MyBuyList.Shared.Entities" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    [WebMethod(EnableSession = true)]
    public static bool IsLoggedIn()
    {
        SRL_User user = (SRL_User)HttpContext.Current.Session[AppConstants.CURR_USER];
        bool loggedIn = user != null;
        return loggedIn;
    }

    [WebMethod(EnableSession = true)]
    public static bool Login(string userName, string password)
    {
        //SRL_User user = BusinessFacade.Instance.GetUser(userName, password);
        User user = BusinessFacade.Instance.GetUser(userName, password);
        HttpContext.Current.Session[AppConstants.CURR_USER] = user;
        return user != null;
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
