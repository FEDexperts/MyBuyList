using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using ProperControls.General;
using ProperControls.Pages;
using MyBuyList.Shared.Entities;
using MyBuyList.BusinessLayer;
using MyBuyList.Shared;

public partial class UC_HeaderControl : System.Web.UI.UserControl
{
    public const int USER_ADMIN = 1;

    public string SearchFor { get; set; }
    public string SearchIn { get; set; }
     
    protected void Page_Load(object sender, EventArgs e)
    {
        PlaceHolderJoin.Visible = ((BasePage)Page).CurrUser == null;
    }
}
