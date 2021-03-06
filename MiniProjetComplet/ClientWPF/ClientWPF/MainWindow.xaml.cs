﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.IO;
using System.IO.IsolatedStorage;
using ClientWPF.WebService;

namespace ClientWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ServiceClient webService;

        private ImageCollection remotePictureCollection;
        private ImageCollection localPictureCollection;
        private AlbumCollection albumCollection;

        int userID;

        int idSelectedAlbum = 0;

        String LocalDirectory = null;

        public MainWindow(int userID)
        {
            InitializeComponent();

            this.userID = userID;
            webService = new ServiceClient();

            localPictureCollection = new ImageCollection();
            miseAjourAlbum();
        }

        ListBox dragSource = null;

        // On initie le Drag and Drop
        private void ImageDragEvent(object sender, MouseButtonEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            dragSource = parent;
            object data = GetDataFromListBox(dragSource, e.GetPosition(parent));
            if (data != null)
            {
                DragDrop.DoDragDrop(parent, data, DragDropEffects.Move);
            }
        }

        // On ajoute l'objet dans la nouvelle ListBox et on le supprime de l'ancienne
        private void ImageDropEvent(object sender, DragEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            Photo image = (Photo)e.Data.GetData(typeof(Photo));

            // Si il y a deplacement depuis les albums
            if (parent.Name.Equals(dragSource.Name))
                return;

            // envoi de données vers le serveur
            if (parent.Name == "ListBox1")
            {
                //Console.WriteLine("Envoi de " + image.Nom + " vers le serveur");
                ImageInfo i = new ImageInfo();
                i.Name = image.Nom;
                i.Album = idSelectedAlbum;

                webService.AddPicture(i, new FileStream(@LocalDirectory + "\\" + image.Nom, FileMode.Open, FileAccess.Read));
            }
            else // recupération d'image en local
            {
                //Console.WriteLine("Envoi de {0} en local", image.Nom);
                String imagePath = LocalDirectory + "\\" + image.Nom;
                ImageInfo img = new ImageInfo();
                img.Album = image.Album;
                img.Id = image.Id;
                img.Name = image.Nom;
                Util.StreamToFile(imagePath, webService.GetPicture(img));
            }

            try
            {
                ((IList)parent.ItemsSource).Add(image);
                //((IList)dragSource.ItemsSource).Remove(data);
            }
            catch
            {
                MessageBoxResult result = MessageBox.Show("Veuillez choisir un dossier pour l'importation local");
            }
        }


        private void ViewPhotoEvent(object sender, MouseButtonEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            Album a = (Album)lb.SelectedItem;
            idSelectedAlbum = a.Id;
            Console.WriteLine("album : " + idSelectedAlbum);
            miseAJourPhoto(idSelectedAlbum);
        }

        private void miseAjourAlbum()
        {
            // Recuperation des albums de l'utilisateur
            albumCollection = new AlbumCollection();
            Album[] albums = webService.GetAlbumCollection(userID);
            foreach (Album a in albums)
            {
                Console.WriteLine("Album Nom:{0}", a.Nom);
                albumCollection.Add(a);
            }

            ObjectDataProvider imageSourceAlbum = (ObjectDataProvider)FindResource("ImageCollectionAlbum");
            imageSourceAlbum.ObjectInstance = albumCollection;
        }

        public void miseAJourPhoto(int idAlb)
        {
            // On crée notre collection d'image et on y ajoute deux images
            remotePictureCollection = new ImageCollection();
            ImageInfo[] photos = webService.GetPicturesFromAlbum(idAlb);
            foreach (ImageInfo p in photos)
            {
                Photo ph = new Photo(p.Id, p.Name, p.Album);
                ph.Image = Util.StreamToByte(webService.GetPictureThumbnail(p, 100));
                remotePictureCollection.Add(ph);
            }

            // On lie la collectionau ObjectDataProvider déclaré dans le fichier XAML
            ObjectDataProvider imageSource = (ObjectDataProvider)FindResource("ImageCollection1");
            imageSource.ObjectInstance = remotePictureCollection;
        }

        // On récupére l'objet que que l'on a dropé
        private static object GetDataFromListBox(ListBox source, Point point)
        {
            UIElement element = (UIElement)source.InputHitTest(point);
            if (element != null)
            {
                object data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    data =
                    source.ItemContainerGenerator.ItemFromContainer(element);
                    if (data == DependencyProperty.UnsetValue)
                    {
                        element = (UIElement)VisualTreeHelper.GetParent(element);
                    }
                    if (element == source)
                    {
                        return null;
                    }
                }
                if (data != DependencyProperty.UnsetValue)
                {
                    return data;
                }
            }
            return null;
        }

        private void selectDirButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd1 = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = fbd1.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                LocalDirectory = fbd1.SelectedPath;

                List<string> lis = Util.listDir(@LocalDirectory);
                localPictureCollection.Clear();

                foreach (string img in lis)
                {
                    string[] words = img.Split('\\');
                    string name = words[words.Length - 1];

                    Photo p = new Photo(name);
                    p.Image = Util.CreateThumbnailC(Util.lireFichier(@img), 100);
                    localPictureCollection.Add(p);

                    //Photo p = new Photo(name);
                    //System.Drawing.Image image = System.Drawing.Image.FromFile(@img);
                    //p.Image = Util.imageToByteArray(Util.FixedSize(image, 100, 100));
                    //imageCollection2.Add(p);
                }

                ObjectDataProvider imageSource2 = (ObjectDataProvider)FindResource("ImageCollection2");
                imageSource2.ObjectInstance = localPictureCollection;
            }
        }

        /// <summary>
        /// Ajouter un nouvel album
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddAlbum_Click(object sender, RoutedEventArgs e)
        {
            Album a = new Album();
            a.Nom = textAlbum.Text;
            a.UserId = userID;
            a = webService.AddAlbum(a);
            Console.WriteLine("Album Ajouté {0}({1})", a.Nom, a.Id);
            miseAjourAlbum();
        }

        private void ListBoxAlbum_KeyUp(object sender, KeyEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            if (e.Key == Key.Delete && lb.SelectedItem != null)
            {
                // Delete confirmation
                if (MessageBox.Show("Do you want to delete this folder?",
                    "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {   
                    int albumId = ((Album)lb.SelectedItem).Id;
                    if (webService.RemoveAlbum(userID, albumId) == 1)
                        albumCollection.RemoveAt(lb.SelectedIndex);
                    else
                        MessageBox.Show("Error on delete"); 
                }
            }
        }

        private void ListBox1_KeyUp(object sender, KeyEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            if (e.Key == Key.Delete && lb.SelectedItem != null)
            {
                // Delete confirmation
                if (MessageBox.Show("Do you want to delete this picture?",
                    "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    int pictureId = ((Photo)lb.SelectedItem).Id;
                    if (webService.RemovePicture(userID, ((Album)ListBoxAlbum.SelectedItem).Id, pictureId) == 1)
                        remotePictureCollection.RemoveAt(lb.SelectedIndex);
                    else
                        MessageBox.Show("Error on delete");
                }
            }
        }
    }
}