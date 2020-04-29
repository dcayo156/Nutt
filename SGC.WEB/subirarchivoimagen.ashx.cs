using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace NUT.WEB
{
    /// <summary>
    /// Descripción breve de subirarchivoimagen
    /// </summary>
    public class subirarchivoimagen : Utils.HandlerBase
    {

        public override void ProcessRequest(HttpContext context)
        {
            validarSesion = false;
            base.ProcessRequest(context);

            if (context.Request.Files.Count == 0)
            {
                return;
            }

            try
            {
                HttpPostedFile archivo = context.Request.Files[0];
                string extension = Path.GetExtension(archivo.FileName);
                string nombre = Guid.NewGuid().ToString();

                int widthIcono = 1024;
                Image image = Image.FromStream(archivo.InputStream);// .FromFile(archivo.FileName);
                ImageFormat formato;
                switch (extension)
                {
                    case ".jpg":
                        formato = ImageFormat.Jpeg;
                        break;
                    case ".jpeg":
                        formato = ImageFormat.Jpeg;
                        break;
                    case ".png":
                        formato = ImageFormat.Png;
                        break;
                    case ".gif":
                        formato = ImageFormat.Gif;
                        break;
                    case ".bmp":
                        formato = ImageFormat.Bmp;
                        break;
                    default:
                        formato = ImageFormat.Png;
                        break;
                }
                if (image.Width > widthIcono)
                {
                    int nuevoAlto = (widthIcono * image.Height) / image.Width;
                    //eUsuario.Fotografia = FixedSize(image, widthFotografia, nuevoAlto, formato);
                    File.WriteAllBytes(Path.Combine(context.Server.MapPath("~/temp"), nombre + extension), FixedSize(image, widthIcono, nuevoAlto, formato));
                }
                else
                {
                    archivo.SaveAs(Path.Combine(context.Server.MapPath("~/temp"), nombre + extension));
                }
                context.Response.Clear();
                if (context.Request.Browser.Browser == "IE" && context.Request.Browser.MajorVersion <= 6)
                {
                    context.Response.ContentType = "text/html";
                }

                context.Response.Write(JObject.FromObject(new
                {
                    success = true,
                    archivo = nombre + extension,
                    status = 200
                }
                ).ToString(Newtonsoft.Json.Formatting.None));
            }
            catch (Exception ex)
            {
                context.Response.Write(JObject.FromObject(new
                {
                    success = false,
                    error = ex.Message,
                    status = 400
                }
               ).ToString(Newtonsoft.Json.Formatting.None));
            }
        }

        static byte[] FixedSize(Image oldImage, int Width, int Height, ImageFormat formato)
        {//modificado para preservar la transparencia de imagenes .png
            int sourceWidth = oldImage.Width;
            int sourceHeight = oldImage.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((Width - (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((Height - (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            using (Bitmap newImage = new Bitmap(Width, Height))
            {
                using (Graphics g = Graphics.FromImage(newImage))
                {
                    g.Clear(Color.Transparent);
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.DrawImage(oldImage,
                        new Rectangle(destX, destY, destWidth, destHeight),
                        new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                    GraphicsUnit.Pixel);
                    g.Dispose();
                }
                using (MemoryStream msImagen = new MemoryStream())
                {
                    newImage.Save(msImagen, formato);
                    return msImagen.GetBuffer();
                }
            }
        }
    }
}