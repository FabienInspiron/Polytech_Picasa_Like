using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TestServices
{
    class Program
    {
        static void Main(string[] args)
        {
            ImageTransfertServiceReference.ImageTransfertServiceClient client = new ImageTransfertServiceReference.ImageTransfertServiceClient();
            ImageTransfertServiceReference.PhotoInfo photoInfo = new ImageTransfertServiceReference.PhotoInfo();
            photoInfo.AlbumId = 0;
            photoInfo.Name = "fichier";
            MemoryStream blob = new MemoryStream(lireFichier(@"c:\fichier.jpg"));
            // Appel de notre web method
            client.Upload(photoInfo, blob);
            Console.Out.WriteLine("Transfert Terminé");
            Console.ReadLine();
        }

        /// <summary>
        /// Lit et retourne le contenu du fichier sous la forme de tableau de byte
        /// </summary>
        /// <param name="chemin">chemin du fichier</param>
        /// <returns></returns>
        private static byte[] lireFichier(string chemin)
        {
            byte[] data = null;
            FileInfo fileInfo = new FileInfo(chemin);
            int nbBytes = (int)fileInfo.Length;
            FileStream fileStream = new FileStream(chemin, FileMode.Open,
            FileAccess.Read);
            BinaryReader br = new BinaryReader(fileStream);
            data = br.ReadBytes(nbBytes);
            return data;
        }
    }
}
