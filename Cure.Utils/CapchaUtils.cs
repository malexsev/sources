using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.Utils
{
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Web;

    class CapchaUtils
    {
        public byte[] DrawByte()
        {
            byte[] returnByte = { };
            Bitmap bitmapImage = new Bitmap(150, 30, PixelFormat.Format32bppArgb);

            string key = getRandomString();

            HttpContext.Current.Session.Add("capcha", key);

            using (Graphics g = Graphics.FromImage(bitmapImage))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                Rectangle rect = new Rectangle(0, 0, 150, 30);
                HatchBrush hBrush = new HatchBrush(HatchStyle.SmallConfetti, Color.LightGray, Color.White);
                g.FillRectangle(hBrush, rect);
                hBrush = new HatchBrush(HatchStyle.LargeConfetti, Color.Red, Color.Black);
                float fontSize = 20;
                Font font = new Font(FontFamily.GenericSerif, fontSize, FontStyle.Strikeout);
                float x = 10;
                float y = 1;
                PointF fPoint = new PointF(x, y);
                g.DrawString(key, font, hBrush, fPoint);

                using (MemoryStream ms = new MemoryStream())
                {
                    bitmapImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    returnByte = ms.ToArray();
                }
            }
            return returnByte;
        }
        
        private string getRandomString()
        {

            string returnString = string.Empty;
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            Random rand = new Random();

            int length = rand.Next(5, 8);
            for (int i = 0; i < length; i++)
            {
                int pos = rand.Next(0, 62);
                returnString += letters[pos].ToString();
            }
            return returnString;
        }
    }
}
