using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.Linq;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ProperControls.Pages;

public partial class SelectRecipePicture : BasePage
{
    Binary RecipePicture
    {
        get { return (Binary)Session["RecipePicture"]; }
        set { Session["RecipePicture"] = value; }
    }
    Binary TempPicture
    {
        get { return (Binary)ViewState["TempPicture"]; }
        set { ViewState["TempPicture"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (this.RecipePicture != null)
            {
                string filename = Path.GetTempFileName();
                MemoryStream stream = new MemoryStream(this.RecipePicture.ToArray());
                Bitmap bitmap = ImageHelper.ResizeImage(new Bitmap(stream, false), 200, 200);
                bitmap.Save(filename);
                this.imgContainer.ImageUrl = "~/ShowPicture.ashx?picture=" + new FileInfo(filename).Name;
                this.imgContainer.Visible = true;
                this.TempPicture = this.RecipePicture;
            }
        }
    }
    protected void btnHidden_Click(object sender, EventArgs e)
    {
        if (this.pictureFile.HasFile && this.pictureFile.PostedFile != null)
        {
            if (!ImageHelper.IsImage(pictureFile.PostedFile.FileName))
            {
                return;
            }

            string filename = Path.GetTempFileName();
            Bitmap bitmap = ImageHelper.ResizeImage(new Bitmap(pictureFile.PostedFile.InputStream, false), 200, 200);
            bitmap.Save(filename);
            this.imgContainer.ImageUrl = "~/ShowPicture.ashx?picture=" + new FileInfo(filename).Name;
            this.imgContainer.Visible = true;
            this.TempPicture  = ImageHelper.GetBitmapBytes(bitmap);
            this.btnClear.Enabled = true;
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.imgContainer.ImageUrl= "";
        this.imgContainer.Visible = false;
        this.TempPicture = null;
        this.btnClear.Enabled = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.RecipePicture = this.TempPicture;
    }
}
