using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;

namespace ClientWeb
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

        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        /// <summary>
        /// Create a square thumbnail
        /// </summary>
        /// <param name="PassedImage">Image to reduce</param>
        /// <param name="LargestSide">Side size</param>
        /// <returns></returns>
        public static byte[] CreateThumbnailC(byte[] PassedImage, int LargestSide)
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