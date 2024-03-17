using MyTrivia.ViewModel;
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
using System.Data.SQLite;


namespace MyTrivia.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        

        public Login()
        {
            InitializeComponent();
          
        }


        private void UsrTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
   
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the entered email and password
                string enteredEmail = UsrTxtBox.Text;
                string enteredPassword = PwdBox.Password;

                // Updated connection string with your database path
                string connectionString = @"Data Source=C:\Users\User\source\repos\MyTrivia\DBAllUsers.db;Version=3;";
                bool userFound = false; // Flag to indicate if user was found

                using (var connection = new System.Data.SQLite.SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // SQL command to search for user
                    string sql = "SELECT * FROM Users WHERE Email = @Email AND Password = @Password LIMIT 1";
                    using (var command = new System.Data.SQLite.SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Email", enteredEmail);
                        command.Parameters.AddWithValue("@Password", enteredPassword);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // If a user is found
                            {
                                userFound = true; // Set the flag to true
                            }
                        }
                    }
                }

                // Check if a match was found and load the main page
                if (userFound)
                {
                    // User found, create and show the MainWithFrame window
                    MainWithFrame mainWithFrame = new MainWithFrame();
                    mainWithFrame.Show();

                    // Now, navigate the Frame within MainWithFrame to MainPage
                    MainPage mainPage = new MainPage(); // Assuming _sharedViewModel is already defined
                    mainWithFrame.mainFrame.Navigate(mainPage);
                }

                else
                {
                    // No match found, display an error message
                    MessageBox.Show("Invalid email or password. Please try again." + enteredEmail);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            RegisterUser registerUser = new RegisterUser();
            registerUser.Show();
        }

        private void ShowRow_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=C:\Users\User\source\repos\פרויקט 5 יחידות\MVVMWithAdd\myfirstsql.db;Version=3;";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open(); // פתיחת החיבור לבסיס הנתונים

                string sql = "SELECT * FROM Users LIMIT 1"; // שאילתה לקבלת השורה הראשונה מהטבלה
                using (var command = new SQLiteCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // אם קיימת שורה לקריאה
                        {
                            // שמירת ערכי העמודות במשתנים
                            int userId = Convert.ToInt32(reader["userId"]);
                            string firstName = Convert.ToString(reader["firstName"]);
                            string lastName = Convert.ToString(reader["lastName"]);
                            string city = Convert.ToString(reader["city"]);
                            string state = Convert.ToString(reader["state"]);
                            string country = Convert.ToString(reader["country"]);
                            string email = Convert.ToString(reader["eMail"]);
                            string password = Convert.ToString(reader["password"]);

                            // הצגת הנתונים לדוגמא
                            MessageBox.Show($"UserID: {userId}, Name: {firstName} {lastName}, Email: {email}");
                        }
                    }
                }
            }
        }

 
    }
}
