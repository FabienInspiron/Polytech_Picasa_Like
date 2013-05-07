﻿using System;
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

        private ClientWPF.Util.ImageCollection imageCollection1;
        private ClientWPF.Util.ImageCollection imageCollection2;
        private ClientWPF.Util.AlbumCollection imageCollectionAlbum;

        int IDUser = 170;

        int idAlbumSelected = 0;

        public MainWindow(int idUser)
        {
            InitializeComponent();

            this.IDUser = idUser;
            webService = new ServiceClient();

            imageCollection2 = new ClientWPF.Util.ImageCollection();
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
                image.Album = idAlbumSelected;
                webService.AddPhoto(image);
            }
            else // recupération d'image en local
            {
                //Console.WriteLine("Envoi de {0} en local", image.Nom);
                String imagePath = textDirectory.Text + "\\" + image.Nom;
                Util.ByteArrayToFile(imagePath, image.Image);
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
            idAlbumSelected = a.Id;
            Console.WriteLine("album : " + idAlbumSelected);
            miseAJourPhoto(idAlbumSelected);
        }

        private void miseAjourAlbum()
        {
            // Recuperation des albums de l'utilisateur
            imageCollectionAlbum = new ClientWPF.Util.AlbumCollection();
            Album[] albums = webService.GetAlbumCollection(IDUser);
            foreach (Album a in albums)
            {
                Console.WriteLine("Album Nom:{0}", a.Nom);
                imageCollectionAlbum.Add(a);
            }

            ObjectDataProvider imageSourceAlbum = (ObjectDataProvider)FindResource("ImageCollectionAlbum");
            imageSourceAlbum.ObjectInstance = imageCollectionAlbum;
        }

        public void miseAJourPhoto(int idAlb)
        {
            // On crée notre collection d'image et on y ajoute deux images
            imageCollection1 = new ClientWPF.Util.ImageCollection();
            Photo[] photos = webService.GetPhotoAlbum(IDUser, idAlb);
            foreach (Photo p in photos)
                imageCollection1.Add(p);

            // On lie la collectionau ObjectDataProvider déclaré dans le fichier XAML
            ObjectDataProvider imageSource = (ObjectDataProvider)FindResource("ImageCollection1");
            imageSource.ObjectInstance = imageCollection1;
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd1 = new System.Windows.Forms.FolderBrowserDialog();
            fbd1.ShowDialog();
            string myFolder = fbd1.SelectedPath;

            textDirectory.Text = myFolder;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            List<string> lis = Util.listDir(@textDirectory.Text);
            imageCollection2.Clear();

            foreach (string img in lis)
            {
                string[] words = img.Split('\\');
                string name = words[words.Length - 1];

                Photo p = new Photo();
                p.Image = Util.lireFichier(@img);
                p.Nom = name;
                imageCollection2.Add(p);
            }

            ObjectDataProvider imageSource2 = (ObjectDataProvider)FindResource("ImageCollection2");
            imageSource2.ObjectInstance = imageCollection2;
        }

        /// <summary>
        /// Ajouter un nouvel album
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Album a = new Album();
            a.Nom = textAlbum.Text;
            a.UserId = IDUser;
            a = webService.AddAlbum(a);
            Console.WriteLine("Album Ajouté {0}({1})", a.Nom, a.Id);
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            miseAjourAlbum();
        }
    }
}