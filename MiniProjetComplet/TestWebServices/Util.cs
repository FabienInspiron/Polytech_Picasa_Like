using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TestWebServices
{
    class Util
    {
        /// <summary>
        /// Enregistre image
        /// </summary>
        /// <param name="_FileName">Chemin de l'image</param>
        /// <param name="_ByteArray">Données brute représentant l'image</param>
        /// <returns></returns>
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

        public static byte[] ImageByte(Stream Image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Image.CopyTo(ms);
                return ms.ToArray();
            }
        }

    }
}
