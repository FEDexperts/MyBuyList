using System;
using System.Web.UI.Design;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Compilation;
using System.CodeDom;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.IO;

// changes in expression require closing the solution/project and reopening it.

#region ImageExpression class and Image inner class
/// <summary>
/// Prefix for using this expression.
/// </summary>
/// <remarks>
/// Notice the web.config file section.
/// </remarks>
[ExpressionPrefix("ImageExpression")]
[ExpressionEditor("ImageExpressionEditor")]
public sealed class ImageExpression : ExpressionBuilder
{
    #region Image inner class
    /// <summary>
    /// Provides a structure for containing Image information.
    /// </summary>
    public sealed class Image
    {
        private const string ErrorMessage = "Error in expression: '{0}'";

        public Image(string imageName, string folder)
        {
            this.imageName = imageName;
            this.folder = folder;
        }

        private string imageName;
        public string ImageName
        {
            get { return this.imageName; }
            set { this.imageName = value; }
        }

        private string folder;
        public string Folder
        {
            get { return this.folder; }
            set { this.folder = value; }
        }

        public static Image Parse(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                throw new ApplicationException(string.Format(ErrorMessage, expression));

            string[] parts = expression.Split(',');
            if (parts.Length != 2)
                throw new ApplicationException(string.Format(ErrorMessage, expression));

            string folder = parts[0].Trim();
            string img = parts[1].Trim();

            Image image = new Image(img, folder);
            return image;
        }

        /// <summary>
        /// Returns the string which will be used in the markup file.
        /// </summary>
        /// <returns></returns>
        public string GetExpression()
        {
            return string.Format("{0}, {1}", this.folder, this.imageName);
        }

        /// <summary>
        /// Returns the actual image url.
        /// </summary>
        /// <returns></returns>
        public string GetImageUrl()
        {
            string url = string.Format("~/{0}/{1}/{2}", this.Folder, Thread.CurrentThread.CurrentUICulture.Name, this.ImageName);
            string file = HttpContext.Current.Server.MapPath(url);
            if (!File.Exists(file))
                url = string.Format("~/{0}/en-US/{1}", this.Folder, this.ImageName);

            return url;
        }
    }
    #endregion

    /// <summary>
    /// Returns the url for the image.
    /// </summary>
    /// <param name="expression">Full expression as written in the markup file.</param>
    /// <returns></returns>
    public static object GetImageUrl(string expression)
    {
        Image image = Image.Parse(expression);
        return image.GetImageUrl();
    }

    public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
    {
        CodePrimitiveExpression argument = new CodePrimitiveExpression(entry.Expression);
        CodeTypeReferenceExpression targetObject = new CodeTypeReferenceExpression(typeof(ImageExpression));
        return new CodeMethodInvokeExpression(targetObject, "GetImageUrl", argument);
    }
} 
#endregion

#region ImageExpressionEditor class
public sealed class ImageExpressionEditor : ExpressionEditor
{
    // return the sheet
    public override ExpressionEditorSheet GetExpressionEditorSheet(string expression, IServiceProvider serviceProvider)
    {
        return new ImageExpressionEditorSheet(expression, this, serviceProvider);
    }

    public override object EvaluateExpression(string expression, object parseTimeData, Type propertyType, IServiceProvider serviceProvider)
    {
        return ImageExpression.GetImageUrl(expression);
    }
} 
#endregion

#region ImageExpressionEditorSheet class
// this sheet will appear in the Expressions properties of an item (within the properties designer window)
public class ImageExpressionEditorSheet : ExpressionEditorSheet
{
    private ImageExpressionEditor owner;
    private ImageExpression.Image image;

    public ImageExpressionEditorSheet(string expression, ImageExpressionEditor owner, IServiceProvider provider)
        : base(provider)
    {
        this.owner = owner;
        this.image = ImageExpression.Image.Parse(expression);
    }

    #region Properties for Expression sheet
    public string ImageFileName
    {
        get { return this.image.ImageName; }
        set { this.image.ImageName = value; }
    }

    public string RootFolder
    {
        get { return this.image.Folder; }
        set { this.image.Folder = value; }
    }
    #endregion

    /// <summary>
    /// Returns the expression which will be used in the markup.
    /// </summary>
    /// <returns></returns>
    public override string GetExpression()
    {
        return this.image.GetExpression();
    }
} 
#endregion