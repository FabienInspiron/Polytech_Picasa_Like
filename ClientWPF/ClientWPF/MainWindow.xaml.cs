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
using AdminPicasaLike;
using System.Collections;
using System.IO;

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
            remplirListeFromServeur();

            imageCollection2 = new ImageCollection();

            // On lie la collectionau ObjectDataProvider déclaré dans le fichier XAML
            ObjectDataProvider imageSource = (ObjectDataProvider)FindResource("ImageCollection1");
            imageSource.ObjectInstance = imageCollection1;
        }

        public void remplirListeFromServeur()
        {
            List<byte[]> list = gestBDD.getImagesUserByte(170);
            foreach (byte[] b in list)
                imageCollection1.Add(new ImageObjet("E", b));
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
            ((IList)dragSource.ItemsSource).Remove(data);
            ((IList)parent.ItemsSource).Add(data);
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
                imageCollection2.Add(new ImageObjet(img, GestionBDD.lireFichier(@img)));
            }

            ObjectDataProvider imageSource2 = (ObjectDataProvider)FindResource("ImageCollection2");
            imageSource2.ObjectInstance = imageCollection2;
        }
    }
}
