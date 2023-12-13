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
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM VegetablesAndFruits WHERE Type = 'Vegetable'", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                names.Add("Count of vegetables: " + reader[0].ToString());
            }
            reader.Close();
            command = new SqlCommand("SELECT COUNT(*) FROM VegetablesAndFruits WHERE Type = 'Fruit'", connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                names.Add("Count of fruits: " + reader[0].ToString());
            }
            reader.Close();
            string color = "Red";
            command = new SqlCommand($"SELECT COUNT(*) FROM VegetablesAndFruits WHERE Color = '{color}'", connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                colors.Add($"Count of vegetables and fruits of {color} color: " + reader[0].ToString());
            }
            reader.Close();
            command = new SqlCommand("SELECT Color, COUNT(*) FROM VegetablesAndFruits GROUP BY Color", connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                colors.Add($"Count of vegetables and fruits of {reader[0]} color: " + reader[1].ToString());
            }
            reader.Close();
            int maxCaloricity = 100;
            command = new SqlCommand($"SELECT Name FROM VegetablesAndFruits WHERE Caloricity < {maxCaloricity}", connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                caloricities.Add($"Vegetables and fruits with caloricity below {maxCaloricity}: " + reader[0].ToString());
            }
            reader.Close();
            int minCaloricity = 50;
            command = new SqlCommand($"SELECT Name FROM VegetablesAndFruits WHERE Caloricity > {minCaloricity}", connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                caloricities.Add($"Vegetables and fruits with caloricity above {minCaloricity}: " + reader[0].ToString());
            }
            reader.Close();
            int lowerBound = 50;
            int upperBound = 100;
            command = new SqlCommand($"SELECT Name FROM VegetablesAndFruits WHERE Caloricity BETWEEN {lowerBound} AND {upperBound}", connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                caloricities.Add($"Vegetables and fruits with caloricity between {lowerBound} and {upperBound}: " + reader[0].ToString());
            }
            reader.Close();
            command = new SqlCommand("SELECT Name FROM VegetablesAndFruits WHERE Color IN ('Yellow', 'Red')", connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                colors.Add("Vegetables and fruits of yellow or red color: " + reader[0].ToString());
            }
            reader.Close();
        }
    }
}