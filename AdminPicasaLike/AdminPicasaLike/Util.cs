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
                if (info.Extension == ".bmp")
                    list.Add(info.FullName);
            }

            return list;
        }

    }
}
