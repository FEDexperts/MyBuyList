using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

/// <summary>
/// Summary description for ImageHelper
/// </summary>
public class ImageHelper
{
    public static bool IsImage(string filename)
    {
        string extension = Path.GetExtension(filename).ToLower();
        switch (extension)
        {
            case ".gif":
            case ".jpg":
            case ".jpeg":
            case ".jpe":
            case ".png":
            case ".bmp":
                return true;
            default:
                return false;
        }
    }
    public static Bitmap ResizeImage(Bitmap postedFile, int width, int height)
    {

        System.Drawing.Bitmap bmpOut;
        ImageFormat Format = postedFile.RawFormat;
        decimal Ratio;
        int NewWidth;
        int NewHeight;

        //*** If the image is smaller than a thumbnail just return it 
        if (postedFile.Width < width && postedFile.Height < height)
        {
            return postedFile;
        }

        if ((postedFile.Width > postedFile.Height))
        {
            Ratio = Convert.ToDecimal((float)postedFile.Height / (float)postedFile.Width);
            NewWidth = width;

            decimal Temp = NewWidth * Ratio;
            NewHeight = Convert.ToInt32(Temp);
        }
        else
        {
            Ratio = Convert.ToDecimal((float)postedFile.Width / (float)postedFile.Height);
            NewHeight = height;

            decimal Temp = NewHeight * Ratio;
            NewWidth = Convert.ToInt32(Temp);
        }

        bmpOut = new Bitmap(NewWidth, NewHeight);
        Graphics g = Graphics.FromImage(bmpOut);
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        g.FillRectangle(Brushes.White, 0, 0, NewWidth, NewHeight);
        g.DrawImage(postedFile, 0, 0, NewWidth, NewHeight);
        postedFile.Dispose();
        return bmpOut;
    }

    public static byte[] GetBitmapBytes(Bitmap Bitmap)
    {
        MemoryStream memStream = new MemoryStream();
        byte[] bytes;

        try
        {
            // Save the bitmap to the MemoryStream.
            Bitmap.Save(memStream, ImageFormat.Jpeg);

            // Create the byte array.
            bytes = new byte[memStream.Length];

            // Rewind.
            memStream.Seek(0, SeekOrigin.Begin);

            // Read the MemoryStream to get the bitmap's bytes.
            memStream.Read(bytes, 0, bytes.Length);

            // Return the byte array.
            return bytes;
        }
        finally
        {
            // Cleanup.
            memStream.Close();
        }
    }
}
