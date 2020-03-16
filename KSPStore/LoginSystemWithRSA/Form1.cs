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
            

            connectionString = ConfigurationManager.ConnectionStrings["LoginSystemWithRSA"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select * from tblUserRegistration where UserName = '"+userName+"'";
                string cypher = string.Empty;
                
                SqlCommand cmd = new SqlCommand(query, con);

                try
                {
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader != null)
                    {
                        int count = 0;
                        while (reader.Read())
                        {
                            string tempPass = reader["Password"].ToString();
                            cypher = rsaEnc.Decrypt(tempPass);
                            if (tempPass == password)
                            {
                                count++;
                                break;
                            }

                        }
                        if (count > 0)
                        {
                            var homePage = new HomePage();
                            homePage.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password!");
                        }
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception occured: " + ex);
                }
                finally
                {
                    con.Close();
                    
                }
            }

        }

        // For Login with AES password hash
        private void BtnLoginAES_Click(object sender, EventArgs e)
        {
            var userName = UserNameTextbox.Text;
            var password = PasswordTextBox.Text;
            var encPassword = AESConfiguration.Encrypt(password);
           


            connectionString = ConfigurationManager.ConnectionStrings["LoginSystemWithRSA"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {


                string query = "select * from tblUserRegistration where UserName = '" + userName + "' and Password='"+encPassword+"'";
                string cypher = string.Empty;

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
                        MessageBox.Show("Failed to login");
                    }

                    reader.Close();




                    //con.Open();
                    //var reader = cmd.ExecuteReader();
                    //if (reader != null)
                    //{
                    //    int count = 0;
                    //    while (reader.Read())
                    //    {
                    //        string tempPass = reader["Password"].ToString();
                    //        cypher = AESConfiguration.Decrypt(tempPass);
                    //        if (cypher == password)
                    //        {
                    //            count++;
                    //            break;
                    //        }

                    //    }
                    //    if (count > 0)
                    //    {
                    //        var homePage = new HomePage();
                    //        homePage.ShowDialog();
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("Invalid username or password!");
                    //    }
                    //}

                    //reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception occured: " + ex);
                }
                finally
                {
                    con.Close();

                }
            }
        }
    }
}
