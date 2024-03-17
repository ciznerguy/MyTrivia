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
using System.Windows.Shapes;
using System.Data.SQLite;

namespace MyTrivia.View
{
    /// <summary>
    /// Interaction logic for RegisterUser.xaml
    /// </summary>
    public partial class RegisterUser : Window
    {
        public RegisterUser()
        {
            InitializeComponent();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)

            // יש להוסיף לוגיקה המוודאה שהמייל לא קיים בדאטה בייס
            // משאיר לכם לעבוד על כך
        {
            if (!AreAllValuesFilled())
            {
                MessageBox.Show("Please fill all the fields & make sure passwords are equal");
                return;
            }
            else
            {
                // Write to users table with the data
                string email = txtEmail.Text;
                string firstName = txtFname.Text;
                string lastName = txtLname.Text;
                string password = txtPassword1.Text;

                // TODO: Write code to insert the data into the users table
                string connectionString = @"Data Source=C:\Users\User\source\repos\MyTrivia\DBAllUsers.db;Version=3;";

                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO users (email, firstName, lastName, password) VALUES (@Email, @FirstName, @LastName, @Password)";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@Password", password);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("User registered successfully!");
                this.Close();
            }
        }

        private bool AreAllValuesFilled()
        {
            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtFname.Text)
                || string.IsNullOrEmpty(txtLname.Text) || string.IsNullOrEmpty(txtPassword1.Text)
                || string.IsNullOrEmpty(txtPassword2.Text) || txtPassword1.Text != txtPassword2.Text)
            {
                return false;
            }
            return true;
        }
    }
}
