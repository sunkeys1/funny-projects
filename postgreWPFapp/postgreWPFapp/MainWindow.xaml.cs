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
            GetData(combo.SelectedIndex);
            var curr = combo.Items.Count;
            //var value = (string)curr;
            l1.Content = curr;

            
        }

        private void clearData_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            l1.Content = null;
        }
        public void GetData(int selected)
        {
            string table = "";
            string[] sqlQrrys = { "users", "games" };
            switch (selected)
            {
                case 0:
                    table = "users";
                    break;
                case 1:
                    table = "games";
                    break;


                default:
                    break;
            }
            if (sqlQrrys.Contains(table))
            {

                string connectionString = "Server=localhost;Database=pgDB;Port=5432;User Id=postgres;Password=postg";
                string sqlQuery = $"SELECT * FROM {table}";

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

        private void change_Click(object sender, RoutedEventArgs e)
        {
            var collaps = Visibility.Collapsed;
            var visible = Visibility.Visible;
            if (clearData.Visibility == visible)
            {
                clearData.Visibility = collaps;
                dataGrid.Visibility = collaps;
                combo.Visibility = collaps;
                getData.Visibility = collaps;
                l1.Visibility = collaps;
                img.Visibility = visible;
                change.Content = "To Data";

            }
            else
            {

                clearData.Visibility = visible;
                dataGrid.Visibility = visible;
                combo.Visibility = visible;
                getData.Visibility = visible;
                l1.Visibility = visible;
                img.Visibility = collaps;
                change.Content = "To Settings";
            }
            
        }
    }
}
