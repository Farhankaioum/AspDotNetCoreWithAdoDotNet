using LoginSystemWithRSA.Security;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LoginSystemWithRSA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string connectionString = string.Empty;

        private void metroLink1_Click(object sender, EventArgs e)
        {
            var registrationForm = new RegistrationForm();
            //this.Hide();
            registrationForm.ShowDialog();
           //this.Close();
        }

        private void SignInBtn_Click(object sender, EventArgs e)
        {
            var userName = UserNameTextbox.Text;
            var password = PasswordTextBox.Text;

            // For Encrypted password
            var rsaEnc = new RSAConfiguration();
            var encryptedPassword = rsaEnc.Decrypt(password);

            connectionString = ConfigurationManager.ConnectionStrings["LoginSystemWithRSA"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select * from tblUserRegistration where UserName = '"+userName+"' and Password='"+encryptedPassword+"'";
                
                SqlCommand cmd = new SqlCommand(query, con);

                try
                {
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        var homePage = new HomePage();
                        homePage.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password!");
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception occured: " + ex.Message);
                }
                finally
                {
                    con.Close();
                    
                }
            }

        }
    }
}
