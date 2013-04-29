using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AdminPicasaLike
{
    class Util
    {
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
                if (info.Extension == ".bmp" || info.Extension == ".jpg")
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

    }
}
