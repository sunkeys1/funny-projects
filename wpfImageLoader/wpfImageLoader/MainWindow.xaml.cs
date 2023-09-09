using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpfImageLoader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void selectIMG_Click(object sender, RoutedEventArgs e)
        {
            string imgName = string.Empty;
            string imgPath = string.Empty;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.JPEG;*.JPG;*.PNG;*BMP)|*.JPEG;*.JPG;*.PNG;*BMP";
            bool? success = ofd.ShowDialog();
            if (success == true)
            {
                imgName = ofd.SafeFileName;
                imgPath = ofd.FileName;
                testLabel.Content = imgPath;
                imgToDB.Source = new BitmapImage(new Uri(imgPath)); // хз почему в первый раз не сработало

            }
            
        }
    }
}
