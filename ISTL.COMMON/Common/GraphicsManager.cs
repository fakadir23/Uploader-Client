using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using NLog;
using System.Drawing.Text;
using System.Windows.Forms;

namespace ISTL.COMMON.Common
{
    public class GraphicsManager
    {
        private static GraphicsManager graphicsManager = null;
        private Logger logger = LogManager.GetCurrentClassLogger();

        public static GraphicsManager GetInstance()
        {
            if (graphicsManager == null)
            {
                graphicsManager = new GraphicsManager();
            }

            return graphicsManager;
        }

        public Image ResizeImage(Image image, Size size, bool preserveAspectRatio)
        {
            if (image == null) return null;

            int newWidth;
            int newHeight;

            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = (float)size.Width / (float)originalWidth;
                float percentHeight = (float)size.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }

            Rectangle recSrc = new Rectangle(0, 0, image.Width, image.Height);
            Rectangle recDest = new Rectangle(0, 0, newWidth, newHeight);
            Image newImage = new Bitmap(newWidth, newHeight);

            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.DrawImage(image, recDest, recSrc, GraphicsUnit.Pixel);
            }
            return newImage;
        }

        public byte[] ImageToByteArray(Image imageIn)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                imageIn.Save(ms, ImageFormat.Jpeg);
                byte[] imageB = ms.ToArray();
                ms.Close();
                return imageB;
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
            }

            return null;
        }

        public Image ByteArrayToImage(byte[] byteArrayIn)
        {
            try
            {
                if (byteArrayIn == null) return null;

                MemoryStream ms = new MemoryStream(byteArrayIn);
                Image returnImage = Image.FromStream(ms);
                ms.Close();
                return returnImage;
            }
            catch (Exception x)
            {
                logger.Error(x.ToString());
            }
            return null;
        }

        public byte[] GetByteArrayFromTransparentImage(Image pic)
        {
            MemoryStream ms = new MemoryStream();
            byte[] ret = null;


            try
            {
                if ((pic != null))
                {

                    pic.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteData = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(byteData, 0, (int)ms.Length);
                    ret = byteData;
                    ms.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error is found while converting image from bitmap to png." + ex.ToString());
            }

            return ret;
        }

        public byte[] GetBanglaImage(string str, FontStyle style)
        {
            System.Drawing.Font banglaFont = new System.Drawing.Font("Shonar Bangla", 50, style);
            Bitmap b = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(b);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            SizeF size = g.MeasureString(str, banglaFont);
            b = new Bitmap((int)size.Width, (int)size.Height);
            g = Graphics.FromImage(b);
            g.Clear(Color.White);

            TextRenderer.DrawText(g, str, banglaFont, new Point(0, 20), SystemColors.ControlText);
            MemoryStream ms = new MemoryStream();
            b.Save(ms, ImageFormat.Png);
            byte[] imageB = ms.ToArray();
            ms.Close();

            return imageB;
        }
    }
}
