using System;
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
using Microsoft.Win32;
using System.Drawing;

namespace PicasaLike
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            Bitmap bit;

           // OpenFileDialog open = new OpenFileDialog();
            //open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            bit = new Bitmap(@"C:\Users\user\Desktop\Audi_TT-RS N&B.bmp");

            DataBase db = new DataBase();
            ImageBDD imgdbb = new ImageBDD(db);

            //imgdbb.addImage("123", imgdbb.imageToByteArray(bit));
            Console.WriteLine(imgdbb.ImageToByte(bit).Length);
            Console.WriteLine("------ Ajout dans la base de donnée");
            //imgdbb.addImage("123", imgdbb.ImageToByte(bit));

            byte[] bimg = imgdbb.getImage("123");
            Console.WriteLine(bimg.Length);
            Bitmap img = imgdbb.BytesToBitmap(bimg);
            img.Save(@"D:\test.bmp");
            //image1.
        }

        public Bitmap openBMP(String path)
        {
            return new Bitmap(path);
        }
    }
}
