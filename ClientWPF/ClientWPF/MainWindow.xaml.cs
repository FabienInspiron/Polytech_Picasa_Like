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

namespace ClientWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ImageCollection imageCollection1; 
        private ImageCollection imageCollection2;

        public MainWindow()
        {
            
            InitializeComponent();

            // On crée notre collection d'image et on y ajoute deux images
            imageCollection1 = new ImageCollection();
            imageCollection1.Add(new ImageObjet("1", GestionBDD.lireFichier(@"d:\photo\0.jpg")));
            imageCollection1.Add(new ImageObjet("A", GestionBDD.lireFichier(@"d:\photo\1.jpg")));
            imageCollection1.Add(new ImageObjet("E", GestionBDD.lireFichier(@"d:\photo\2.jpg")));

            imageCollection2 = new ImageCollection();
            imageCollection2.Add(new ImageObjet("1", GestionBDD.lireFichier(@"d:\photo\3.jpg")));
            imageCollection2.Add(new ImageObjet("A", GestionBDD.lireFichier(@"d:\photo\4.jpg")));
            imageCollection2.Add(new ImageObjet("E", GestionBDD.lireFichier(@"d:\photo\5.jpg")));

            // On lie la collectionau ObjectDataProvider déclaré dans le fichier XAML
            ObjectDataProvider imageSource = (ObjectDataProvider)FindResource("ImageCollection1");
            imageSource.ObjectInstance = imageCollection1;

            ObjectDataProvider imageSource2 = (ObjectDataProvider)FindResource("ImageCollection2");
            imageSource2.ObjectInstance = imageCollection2;
        }

        private void NotreListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
    }
}
