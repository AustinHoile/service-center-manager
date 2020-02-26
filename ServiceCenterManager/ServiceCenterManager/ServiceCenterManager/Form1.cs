/*
Austin Hoile
IS318 Final Project
Service Manager Version 1.0
11/25/2018
 */
using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace ServiceCenterManager
{
    public partial class frm_login : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public frm_login()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\EmployeeInfo.mdb; 
                                            Persist Security Info=False;"; //Connection to the Access database with a relative path 
        }
        private void frm_login_Load(object sender, EventArgs e)
        {
        }
        private void btn_login_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "select * from Logins where Username = '" + txt_username.Text + "' and Password = '" + txt_password.Text + "'"; //Pulls the username and password from the "Logins" table with the users' login information. 
            OleDbDataReader reader = command.ExecuteReader();

            int count = 0;
            while (reader.Read()) //If the counter counts to 1, the login info matches and the service manager window will open.
            {
                count = count + 1;
            }
            if (count == 1)
            {
                connection.Close();
                connection.Dispose();
                this.Hide();
                ServiceManager sm = new ServiceManager();
                sm.ShowDialog();
            }
            else
            {
                lbl_loginUnsuccessful.Text = "Login Unsuccessful";
            }

            connection.Close();
        }
    }
}
