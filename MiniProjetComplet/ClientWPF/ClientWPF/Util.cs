using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ClientWPF.WebService;
using System.Collections.ObjectModel;
using System.Drawing;

namespace ClientWPF
{
    class Util
    {
        private static List<string> Acceptedextension = new List<string>(new String[] { ".bmp", ".jpg", ".jpeg", ".png" });
        public static List<string> listDir(String dirPath)
        {
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            List<string> list = new List<string>();
            IEnumerable<FileInfo> f = null;

            if (dir.Exists)
            {
                f = dir.EnumerateFiles();
            }
            else
            {
                Console.WriteLine("Repertoire " + dirPath + " inexistant");
            }

            foreach (FileInfo info in f)
            {
                if (Acceptedextension.Contains(info.Extension.ToLower()))
                    list.Add(info.FullName);
            }

            return list;
        }

        public static bool ByteArrayToFile(string _FileName, byte[] _ByteArray)
        {
            try
            {
                // Open file for reading
                System.IO.FileStream _FileStream =
                   new System.IO.FileStream(_FileName, System.IO.FileMode.Create,
                                            System.IO.FileAccess.Write);
                // Writes a block of bytes to this stream using data from
                // a byte array.
                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

                // close file stream
                _FileStream.Close();

                return true;
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}",
                                  _Exception.ToString());
            }

            // error occured, return false
            return false;
        }

        public static bool StreamToFile(string _FileName, Stream _stream)
        {
            FileStream file = File.Create(_FileName);
            _stream.CopyTo(file);
            file.Close();
            return true;
        }

        /// <summary>
        /// Lit et retourne le contenu du fichier sous la forme de tableau de byte
        /// </summary>
        /// <param name="chemin">chemin du fichier</param>
        /// <returns></returns>
        public static byte[] lireFichier(string chemin)
        {
            byte[] data = null;
            try
            {
                FileInfo fileInfo = new FileInfo(chemin);
                int nbBytes = (int)fileInfo.Length;
                FileStream fileStream = new FileStream(chemin, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fileStream);
                data = br.ReadBytes(nbBytes);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return data;
        }

        public static byte[] StreamToByte(Stream str)
        {
            int b, i = 0;
            List<byte> bytes = new List<byte>();
            do
            {
                b = str.ReadByte();
                bytes.Add((byte)b); //read next byte from stream  
                i++;
            } while (b != -1);
            return bytes.ToArray();
        }

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

                // set thumbnail height and width proportional to the original image.  
                int newHeight;
                int newWidth;
                double HW_ratio;
                if (startBitmap.Height > startBitmap.Width)
                {
                    newHeight = LargestSide;
                    HW_ratio = (double)((double)LargestSide / (double)startBitmap.Height);
                    newWidth = (int)(HW_ratio * (double)startBitmap.Width);
                }
                else
                {
                    newWidth = LargestSide;
                    HW_ratio = (double)((double)LargestSide / (double)startBitmap.Width);
                    newHeight = (int)(HW_ratio * (double)startBitmap.Height);
                }

                // create a new Bitmap with dimensions for the thumbnail.  
                Bitmap newBitmap = new Bitmap(newWidth, newHeight);

                // Copy the image from the START Bitmap into the NEW Bitmap.  
                // This will create a thumnail size of the same image.  
                newBitmap = ResizeImage(startBitmap, newWidth, newHeight);

                // Save this image to the specified stream in the specified format.  
                newBitmap.Save(NewMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);

                // Fill the byte[] for the thumbnail from the new MemoryStream.  
                ReturnedThumbnail = NewMemoryStream.ToArray();
            }

            // return the resized image as a string of bytes.  
            return ReturnedThumbnail;
        }

        // Resize a Bitmap  
        private static Bitmap ResizeImage(Bitmap image, int width, int height)
        {
            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics gfx = Graphics.FromImage(resizedImage))
            {
                gfx.DrawImage(image, new Rectangle(0, 0, width, height),
                    new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            }
            return resizedImage;
        }

    }
}