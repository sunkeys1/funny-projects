using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace postgreWPFapp
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

        private void getData_Click(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        private void clearData_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
        }
        public void GetData()
        {
            string connectionString = "Server=localhost;Database=pgDB;Port=5432;User Id=postgres;Password=postg";
            string sqlQuery = "SELECT * FROM users";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                {
                    NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    dataGrid.ItemsSource = dataTable.DefaultView;

                }
                connection.Close();
            }
        }
    }
}
