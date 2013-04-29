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
using AdminPicasaLike;
using System.Collections;
using System.IO;
using System.IO.IsolatedStorage;

namespace ClientWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DataBase db;
        GestionBDD gestBDD;

        private ImageCollection imageCollection1;
        private ImageCollection imageCollection2;

        public MainWindow()
        {

            InitializeComponent();
            db = new DataBase();
            gestBDD =  new GestionBDD(db);

            // On crée notre collection d'image et on y ajoute deux images
            imageCollection1 = new ImageCollection();
            imageCollection1 = gestBDD.getPhotoUser(170);

            imageCollection2 = new ImageCollection();

            // On lie la collectionau ObjectDataProvider déclaré dans le fichier XAML
            ObjectDataProvider imageSource = (ObjectDataProvider)FindResource("ImageCollection1");
            imageSource.ObjectInstance = imageCollection1;
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
            ImageObjet data = (ImageObjet)e.Data.GetData(typeof(ImageObjet));

            // envoi de données vers le serveur
            if (parent.Name == "ListBox1")
            {
                Console.WriteLine("Envoi de " + data.Nom + " vers le serveur");
                String nameImageComplet = textDirectory.Text + "\\"+ data.Nom;
                Console.WriteLine(nameImageComplet);
                gestBDD.addImage(data.Nom,  GestionBDD.lireFichier(nameImageComplet), 85);
            }
            else
            {
                Console.WriteLine("Envoi de " + data.Nom + " en local");
                byte[] imageToDownload = gestBDD.getImageByte(data.id);
                String imagePath = textDirectory.Text + "\\" + data.Nom;
                Util.ByteArrayToFile(imagePath, imageToDownload);
                
            }

            try
            {
                ((IList)parent.ItemsSource).Add(data);
                ((IList)dragSource.ItemsSource).Remove(data);
            }
            catch
            {
                MessageBoxResult result = MessageBox.Show("Veuillez choisir un dossier pour l'importation local");   
            }
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

            foreach (string img in lis)
            {
                string[] words = img.Split('\\');
                string name = words[words.Length-1];

                imageCollection2.Add(new ImageObjet(name, GestionBDD.lireFichier(@img)));
            }

            ObjectDataProvider imageSource2 = (ObjectDataProvider)FindResource("ImageCollection2");
            imageSource2.ObjectInstance = imageCollection2;
        }
    }
}
