using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ServiceWCF
{
    /// <summary>
    /// Symbolyse la classe en dll
    /// </summary>
    class AccesDonnees
    {
        public Photo getImage(int imageId)
        {
            return null;
        }

        /// <summary>
        /// Ajoute une image dans la base de données
        /// </summary>
        /// <param name="p"> Photo à ajouter</param>
        /// <returns>Le nouvel Id de la photo</returns>
        public int uploadImage(Photo p)
        {
            // transfrme image en byte
            byte[] imageBytes = null;
            MemoryStream imageStreamEnMemoire = new MemoryStream();
            p.Blob.CopyTo(imageStreamEnMemoire);
            imageBytes = imageStreamEnMemoire.ToArray();

            // Ajout de l'image en base de données

            // Retour du nouvel Id
            return 0;
        }
    }
}
