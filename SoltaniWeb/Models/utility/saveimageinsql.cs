using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SoltaniWeb.Models.utility
{
    public static class saveimageinsql
    {
        public static byte[] perform(IFormFile image,bool resize=true, int X =128 , int Y=128)
        {
            var ms = new MemoryStream();


            image.CopyTo(ms);
            byte[] b = ms.ToArray();

            if (resize!=true)
            {
                return b;
            }
            else
            {

            System.Drawing.Image imgmem = System.Drawing.Image.FromStream(ms);
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(imgmem, X, Y);

            System.IO.MemoryStream memThumbnail = new System.IO.MemoryStream();
            bmp.Save(memThumbnail, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] bb = memThumbnail.ToArray();
            return bb;
            }
        }
    }
}
