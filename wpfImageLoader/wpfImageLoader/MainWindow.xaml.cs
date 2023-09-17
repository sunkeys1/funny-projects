using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
        private string connectionString = "Data Source=localhost;integrated security=sspi;initial catalog=123";
        private string command = $"INSERT INTO base64Image (image) VALUES ('')";
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

        private async void toDB_Click(object sender, RoutedEventArgs e)
        {
            //byte[] byte_img = File.ReadAllBytes(imgToDB.);
            //var some_img = imgToDB.Source;
            //if (imgToDB.Source != null)
            //{
            //    byte[] barr = File.ReadAllBytes(testLabel.Content.ToString());
            //    var img = imgToDB.Source;
            //}


            // последняя версия с base64 версией
            //SqlConnection connect = new SqlConnection(connectionString);
            //string base64 = Convert.ToBase64String(File.ReadAllBytes(testLabel.Content.ToString()));
            //string command = $"INSERT INTO base64Image (image) VALUES ('{base64}')";
            //SqlCommand sqlCmd = new SqlCommand(command, connect);
            //connect.Open();
            //sqlCmd.ExecuteNonQuery();
            //connect.Close();



            //рабочая тема но до конца не понял
            SqlConnection connect = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO imgTest (img) VALUES (@igm)", connect);
            String? strFilePath = testLabel.Content.ToString();

            //Read jpg into file stream, and from there into Byte array.
            FileStream fsBLOBFile = new FileStream(strFilePath, FileMode.Open, FileAccess.Read);
            Byte[] bytBLOBData = new Byte[fsBLOBFile.Length];
            fsBLOBFile.Read(bytBLOBData, 0, bytBLOBData.Length);
            fsBLOBFile.Close();

            //Create parameter for insert command and add to SqlCommand object.
            SqlParameter prm = new SqlParameter("@igm", SqlDbType.VarBinary, bytBLOBData.Length, ParameterDirection.Input, false,
            0, 0, null, DataRowVersion.Current, bytBLOBData);
            cmd.Parameters.Add(prm);

            //Open connection, execute query, and close connection.
            connect.Open();
            cmd.ExecuteNonQuery();
            connect.Close();

        }

        private void fromDB_Click(object sender, RoutedEventArgs e)
        {
            //string command = $"SELECT img FROM imgTest WHERE Id = 4)";
            //SqlConnection sqlConnect = new SqlConnection(connectionString);
            //SqlCommand sqlCmd = new SqlCommand(command, sqlConnect);
            //string base64FrobDb = sqlCmd.ExecuteScalar().ToString();
            //sqlConnect.Open();
            //sqlCmd.ExecuteNonQuery();
            //sqlConnect.Close();
            //var mem = new MemoryStream(Convert.ToByte(base64FrobDb));
            //BitmapImage bim = new BitmapImage();
            //bim.StreamSource = mem;

            //using (MemoryStream stream = new MemoryStream(byteArray))
            //{
            //    imgFromDB.Source = BitmapFrame.Create(stream,
            //                                      BitmapCreateOptions.None,
            //                                      BitmapCacheOption.OnLoad);
            //}
            //using (var stream = new MemoryStream(base64FrobDb))
            //{
            //    var bi = BitmapFrame.Create(stream, BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.OnLoad);
            //}










            //для выгрузки из бд но пикчурбокс

            SqlConnection connect = new SqlConnection(connectionString);
            connect.Open();

            //Retrieve BLOB from database into DataSet.
            SqlCommand sqlCmd = new SqlCommand($"SELECT img FROM imgTest WHERE Id = {Convert.ToInt32(tbId.Text)}", connect);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "imgTest");
            int c = dataSet.Tables["imgTest"].Rows.Count;

            if (c > 0)
            {
                //BLOB is read into Byte array, then used to construct MemoryStream,
                //then passed to PictureBox.
                Byte[] byteBLOBData = new Byte[0];
                byteBLOBData = (Byte[])(dataSet.Tables["imgTest"].Rows[c - 1]["img"]);
                MemoryStream stmBLOBData = new MemoryStream(byteBLOBData);
                imgFromDB.Source = BitmapFrame.Create(stmBLOBData,
                                                  BitmapCreateOptions.None,
                                                  BitmapCacheOption.OnLoad);
            }
            connect.Close();


        }
    }
}
