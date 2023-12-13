using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using Microsoft.Data.SqlClient;

namespace Module_1_HW
{
    public partial class MainWindow : Window
    {
        ObservableCollection<string> names = new ObservableCollection<string>();
        ObservableCollection<string> colors = new ObservableCollection<string>();
        ObservableCollection<string> caloricities = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();
            list.ItemsSource = names;
            list1.ItemsSource = colors;
            list2.ItemsSource = caloricities;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=LAPTOP-2\\SQLLAPTOP;Initial Catalog=fuck2;User ID=dimavoronkov2222;Password=G_8289/00/5654_G;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                MessageBox.Show("Connection successful!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                Loaddata(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Loaddata(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT Name FROM VegetablesAndFruits ORDER BY Name", connection);
            SqlDataReader reader = command.ExecuteReader();
            // Display all the names of vegetables and fruits
            while (reader.Read())
            {
                names.Add(reader[0].ToString());
            }
            reader.Close();

            command = new SqlCommand("SELECT Color FROM VegetablesAndFruits ORDER BY Color", connection);
            reader = command.ExecuteReader();
            // Display all the colors
            while (reader.Read())
            {
                colors.Add(reader[0].ToString());
            }
            reader.Close();

            command = new SqlCommand("SELECT MAX(Caloricity) FROM VegetablesAndFruits", connection);
            reader = command.ExecuteReader();
            // Display the maximum calories
            while (reader.Read())
            {
                caloricities.Add(reader[0].ToString());
            }
            reader.Close();

            command = new SqlCommand("SELECT MIN(Caloricity) FROM VegetablesAndFruits", connection);
            reader = command.ExecuteReader();
            // Display the minimum calories
            while (reader.Read())
            {
                caloricities.Add(reader[0].ToString());
            }
            reader.Close();

            command = new SqlCommand("SELECT AVG(Caloricity) FROM VegetablesAndFruits", connection);
            reader = command.ExecuteReader();
            // Display the average calories
            while (reader.Read())
            {
                caloricities.Add(reader[0].ToString());
            }
            reader.Close();
        }
    }
}