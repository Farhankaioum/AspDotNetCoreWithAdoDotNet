﻿using LoginSystemWithRSA.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginSystemWithRSA
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        string connectionString = string.Empty;
        private void RegiterBtn_Click(object sender, EventArgs e)
        {
            connectionString = ConfigurationManager.ConnectionStrings["LoginSystemWithRSA"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string userName = UserNameTextbox.Text;
                string password = UserNameTextbox.Text;
                string email = EmailTextbox.Text;

                #region for first RSA // For Encrypted password
                // var rsaEnc = new RSAConfiguration();
                // var encryptedPassword = rsaEnc.Encrypt(password); // 


                // var encryptedPassword = AESConfiguration.Encrypt(password); // For AES

                #endregion

                #region For msdn RSACSPSample
                byte[] dataToEncrypt = Encoding.ASCII.GetBytes(password);
                string hashPassword = string.Empty;
                using(RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    var encryptedData = RSACSPSample.RSAEncrypt(dataToEncrypt, RSA.ExportParameters(false), false);
                    hashPassword = BitConverter.ToString(encryptedData);
                }

                #endregion



                string query = "insert into tblUserRegistration values('"+userName+"', '"+ hashPassword + "', '"+email+"')";
                SqlCommand cmd = new SqlCommand(query, con);

                try
                {
                    con.Open();
                   var count = cmd.ExecuteNonQuery();
                    if (count > 0)
                    {
                        MessageBox.Show("Insert Successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Unsuccessfully operation!");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception thrown: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }

        }
    }
}
