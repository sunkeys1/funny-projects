using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
        private string connection = "Data Source=localhost;integrated security=sspi;initial catalog=123";
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

        private void toDB_Click(object sender, RoutedEventArgs e)
        {
            //byte[] byte_img = File.ReadAllBytes(imgToDB.);
            var some_img = imgToDB.Source;
            if (imgToDB.Source != null)
            {
                byte[] barr = File.ReadAllBytes(testLabel.Content.ToString());
                var img = imgToDB.Source;
            }


            // рабочая тема но до конца не понял


            //SqlConnection cn = new SqlConnection(connection);
            //SqlCommand cmd = new SqlCommand("INSERT INTO imgTest (img) VALUES (@igm)", cn);
            //String strFilePath = testLabel.Content.ToString();

            ////Read jpg into file stream, and from there into Byte array.
            //FileStream fsBLOBFile = new FileStream(strFilePath, FileMode.Open, FileAccess.Read);
            //Byte[] bytBLOBData = new Byte[fsBLOBFile.Length];
            //fsBLOBFile.Read(bytBLOBData, 0, bytBLOBData.Length);
            //fsBLOBFile.Close();

            ////Create parameter for insert command and add to SqlCommand object.
            //SqlParameter prm = new SqlParameter("@igm", SqlDbType.VarBinary, bytBLOBData.Length, ParameterDirection.Input, false,
            //0, 0, null, DataRowVersion.Current, bytBLOBData);
            //cmd.Parameters.Add(prm);

            ////Open connection, execute query, and close connection.
            //cn.Open();
            //cmd.ExecuteNonQuery();
            //cn.Close();

        }

        private void fromDB_Click(object sender, RoutedEventArgs e)
        {
            // для выгрузки из бд но пикчурбокс

            //SqlConnection cn = new SqlConnection(strCn);
            //cn.Open();

            ////Retrieve BLOB from database into DataSet.
            //SqlCommand cmd = new SqlCommand("SELECT BLOBID, BLOBData FROM BLOBTest ORDER BY BLOBID", cn);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet();
            //da.Fill(ds, "BLOBTest");
            //int c = ds.Tables["BLOBTest"].Rows.Count;

            //if (c > 0)
            //{
            //    //BLOB is read into Byte array, then used to construct MemoryStream,
            //    //then passed to PictureBox.
            //    Byte[] byteBLOBData = new Byte[0];
            //    byteBLOBData = (Byte[])(ds.Tables["BLOBTest"].Rows[c - 1]["BLOBData"]);
            //    MemoryStream stmBLOBData = new MemoryStream(byteBLOBData);
            //    pictureBox1.Image = Image.FromStream(stmBLOBData);
            //}
            //cn.Close();
        }
    }
}
