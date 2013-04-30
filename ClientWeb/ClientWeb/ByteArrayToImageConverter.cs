using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media.Imaging;
using System.IO;

namespace ClientWPF
{
    public class ByteArrayToImageConverter : IValueConverter
    {
        // byte[] to BitmapImage
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // crée un BitmapImage à partir d'un byte[]
            BitmapImage imageSource = null;

            byte[] array = (byte[])value;
            if (array != null)
            {
                imageSource = new BitmapImage();
                imageSource.BeginInit();
                imageSource.StreamSource = new MemoryStream(array);
                imageSource.EndInit();
            }
            return imageSource;
        }

        // BitmapImage to Byte[]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BitmapImage imageSource = new BitmapImage();
            using (MemoryStream stream = new MemoryStream((Byte[]) value))
            {
                stream.Seek(0, SeekOrigin.Begin);
                imageSource.BeginInit();
                imageSource.StreamSource = stream;
                imageSource.CacheOption = BitmapCacheOption.OnLoad;
                imageSource.EndInit();
            }

            return imageSource;
        }
    }
}
