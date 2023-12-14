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
            combo.Items.Clear();
            string connectionString = "Server=localhost;Database=pgDB;Port=5432;User Id=postgres;Password=postg";
            string sqlQuery = "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public'";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                {
                    var reader = command.ExecuteReader();
                    List<string> tables = new List<string>();
                    while (reader.Read())
                    {
                        string tableName = reader.GetString(0);
                        tables.Add(tableName);
                    }

                    foreach (var ta in tables)
                    {
                        combo.Items.Add(ta);
                    }
                }
                connection.Close();
            }


        }

        private void getData_Click(object sender, RoutedEventArgs e)
        {
            GetData(combo.SelectedIndex);
            var curr = combo.Items.Count;
            l1.Content = curr;
            //var value = (string)curr;
            
            //l1.Content = combo.SelectedValuePath.ToString();

            
        }

        private void clearData_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            l1.Content = null;
            
        }
        public void GetData(int selected)
        {
            var selectedValue = combo.SelectedValue;
            if (selectedValue != null)
            {
                string selectedName = selectedValue.ToString();
                string connectionString = "Server=localhost;Database=pgDB;Port=5432;User Id=postgres;Password=postg";
                string sqlQuery = $"SELECT * FROM {selectedName}";
                //string sqlQuery = $"SELECT *, to_char(four, 'DD.MM.YYYY') AS four FROM {selectedName}";
                //string sqlQuery = "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public'";

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                    {
                        NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);  // crashing here if table name have a numbers or other symbols

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
                loadTables.Visibility = collaps;
                img.Visibility = visible;
                change.Content = "To Data";

            }
            else
            {
                loadTables.Visibility = visible;
                clearData.Visibility = visible;
                dataGrid.Visibility = visible;
                combo.Visibility = visible;
                getData.Visibility = visible;
                l1.Visibility = visible;
                img.Visibility = collaps;
                change.Content = "To Settings";
            }
            
        }

        private void loadTables_Click(object sender, RoutedEventArgs e)
        {
            //combo.Items.Clear();
            //string connectionString = "Server=localhost;Database=pgDB;Port=5432;User Id=postgres;Password=postg";
            //string sqlQuery = "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public'";

            //using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            //{
            //    connection.Open();

            //    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
            //    {
            //        var reader = command.ExecuteReader();
            //        List<string> tables = new List<string>();
            //        while (reader.Read())
            //        {
            //            string tableName = reader.GetString(0);
            //            tables.Add(tableName);
            //        }
                    
            //        foreach(var ta in tables)
            //        {
            //            combo.Items.Add(ta);
            //        }
            //    }
            //    connection.Close();
            //}



            //combo.Items.Add("Bebra");
        }
    }
}
