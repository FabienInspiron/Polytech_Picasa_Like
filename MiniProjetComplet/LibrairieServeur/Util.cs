using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;

namespace LibrairieServeur
{
    public class Util
    {
        /// <summary>
        /// Create a square thumbnail
        /// </summary>
        /// <param name="PassedImage">Image to reduce</param>
        /// <param name="LargestSide">Side size</param>
        /// <returns></returns>
        public static byte[] CreateThumbnail(byte[] PassedImage, int LargestSide)
        {
            byte[] ReturnedThumbnail;

            using (MemoryStream StartMemoryStream = new MemoryStream(),
                                NewMemoryStream = new MemoryStream())
            {
                // write the string to the stream  
                StartMemoryStream.Write(PassedImage, 0, PassedImage.Length);

                // create the start Bitmap from the MemoryStream that contains the image  
                Bitmap startBitmap = new Bitmap(StartMemoryStream);

                Bitmap resizedImage = new Bitmap(LargestSide, LargestSide);
                if (startBitmap.Height > startBitmap.Width)
                {
                    int offset = (startBitmap.Height - startBitmap.Width) / 2;

                    Console.WriteLine("offset : " + offset * LargestSide / startBitmap.Height);

                    using (Graphics gfx = Graphics.FromImage(resizedImage))
                    {
                        gfx.DrawImage(startBitmap,
                            new Rectangle(0, 0, LargestSide, LargestSide),
                            new Rectangle(0, offset, startBitmap.Width, startBitmap.Width),
                            GraphicsUnit.Pixel);
                    }
                }
                else // width > height
                {
                    int offset = (startBitmap.Width - startBitmap.Height) / 2;
                    Console.WriteLine("offset : " + offset * LargestSide / startBitmap.Width);

                    using (Graphics gfx = Graphics.FromImage(resizedImage))
                    {
                        gfx.DrawImage(startBitmap,
                            new Rectangle(0, 0, LargestSide, LargestSide),
                            new Rectangle(offset, 0, startBitmap.Height, startBitmap.Height),
                            GraphicsUnit.Pixel);
                    }
                }

                // Save this image to the specified stream in the specified format.  
                resizedImage.Save(NewMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);

                // Fill the byte[] for the thumbnail from the new MemoryStream.  
                ReturnedThumbnail = NewMemoryStream.ToArray();
            }

            // return the resized image as a string of bytes.  
            return ReturnedThumbnail;
        }

    }
}