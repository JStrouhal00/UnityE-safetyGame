using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OleDb;

namespace Main_Menu_System_form
{

    public partial class Form2 : Form
    {
        private string ConnectionString = "Integrated Security=SSPI;" +
"Initial Catalog=;" +
"Data Source=localhost;";
        private SqlDataReader reader = null;
        private SqlConnection conn = null;
        private SqlCommand cmd = null;
        private System.Windows.Forms.Button AlterTableBtn;
        private string sql = null;
        private System.Windows.Forms.Button CreateOthersBtn;

        public Form2()
        {
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
        int count { get; set; }
        String Player1 { get;set; }
        String Player2 { get; set; }
        String Player3 { get; set; }
        String Player4 { get; set; }
        String Player5 { get; set; }
        String Player6 { get; set; }


        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void TextBox6_TextChanged(object sender, EventArgs e)
        {
        }
        /*public void ExecuteSQLStmt(string sql)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            ConnectionString = "Integrated Security=SSPI;" +
            "Initial Catalog=mydb;" +
            "Data Source=localhost;";
            conn.ConnectionString = ConnectionString;
           /* var connectionx = new OdbcConnection();
            connectionx.ConnectionString =
                          "Dsn=boardgame;" +
                          "Uid=Group6;" +
                          "Pwd=Group6;";*//*
            conn.Open();
            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ae)
            {
                MessageBox.Show(ae.Message.ToString());
            }
        }

        public void CreateDBBtn_Click()
        {
            // Create a connection  
            conn = new SqlConnection(ConnectionString);
            // Open the connection  
            if (conn.State != ConnectionState.Open)
                conn.Open();
            string sql = "CREATE DATABASE mydb ON PRIMARY"
            + "(Name=test_data, filename = 'C:\\mysql\\mydb_data.mdf', size=3,"
            + "maxsize=5, filegrowth=10%)log on"
            + "(name=mydbb_log, filename='C:\\mysql\\mydb_log.ldf',size=3,"
            + "maxsize=20,filegrowth=1)";
            ExecuteSQLStmt(sql);
        }

        public void CreateTableBtn_Click()
        {
            // Open the connection  
            if (conn.State == ConnectionState.Open)
                conn.Close();
            ConnectionString = "Integrated Security=SSPI;" +
            "Initial Catalog=mydb;" +
            "Data Source=localhost;";
            conn.ConnectionString = ConnectionString;
            conn.Open();
            sql = "CREATE TABLE myTable" +
            "(myId INTEGER CONSTRAINT PKeyMyId PRIMARY KEY," +
            "myName CHAR(50), myAddress CHAR(255), myBalance FLOAT)";
            cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.ExecuteNonQuery();
                // Adding records the table  
                sql = "INSERT INTO myTable(myId, myName, myAddress, myBalance) " +
                "VALUES (1001, 'Puneet Nehra', 'A 449 Sect 19, DELHI', 23.98 ) ";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                sql = "INSERT INTO myTable(myId, myName, myAddress, myBalance) " +
                "VALUES (1002, 'Anoop Singh', 'Lodi Road, DELHI', 353.64) ";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                sql = "INSERT INTO myTable(myId, myName, myAddress, myBalance) " +
                "VALUES (1003, 'Rakesh M', 'Nag Chowk, Jabalpur M.P.', 43.43) ";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                sql = "INSERT INTO myTable(myId, myName, myAddress, myBalance) " +
                "VALUES (1004, 'Madan Kesh', '4th Street, Lane 3, DELHI', 23.00) ";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ae)
            {
                MessageBox.Show(ae.Message.ToString());
            }
        }*/

        public void Open_DB(int count)
        {
            /*Access.Application oAccess = null;

            // Start a new instance of Access for Automation:
            oAccess = new Access.ApplicationClass();

            // Open a database in exclusive mode:
            oAccess.OpenCurrentDatabase(
               "c:\\mydb.mdb", //filepath
               true //Exclusive
               );*/

            string connectionString =
    @"Provider=Microsoft.ACE.OLEDB.12.0;" +
    @"Data Source=C:\Users\avmis\Documents\unity e safety game\BoardGame-Database1.accdb;" +
    @"User Id=Admin;Password=;";
            string queryString = "SELECT * FROM BoardGame";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            using (OleDbCommand command = new OleDbCommand(queryString, connection))
            {
                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        //Console.WriteLine(reader[0].ToString());

                        String DeleteRows = "DELETE FROM BoardGame where ([User_ID]) = @Value";
                        using (OleDbConnection connection2 = new OleDbConnection(connectionString))
                        using (OleDbCommand cmd = new OleDbCommand(DeleteRows, connection2))
                        {
                            connection2.Open();
                            cmd.Parameters.AddWithValue("@Value", reader[0]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    reader.Close();
                    String ResetSeed = "ALTER TABLE BoardGame ALTER COLUMN User_ID COUNTER(1,1)";
                    using (OleDbConnection connection3 = new OleDbConnection(connectionString))
                    using (OleDbCommand cmd = new OleDbCommand(ResetSeed, connection3))
                    {
                        connection3.Open();
                        cmd.ExecuteNonQuery();
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            

            
            
            string Value_Input = "";
            Console.WriteLine(count);
            for (int i = 1; i <= count; i++)
            {
                string UserData = "INSERT INTO BoardGame ([Player_Number],[Username],[Password]) VALUES (@Player_Number,@Username,@Password)";
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand cmd = new OleDbCommand(UserData, connection))
                {


                    //Console.WriteLine(i);
                    if (i == 1)
                    {
                        Value_Input = this.textBox1.Text;
                    }
                    else if (i == 2)
                    {
                        Value_Input = this.textBox2.Text;
                    }
                    else if (i == 3)
                    {
                        Value_Input = this.textBox3.Text;
                    }
                    else if (i == 4)
                    {
                        Value_Input = this.textBox4.Text;
                    }
                    else if (i == 5)
                    {
                        Value_Input = this.textBox5.Text;
                    }
                    else if (i == 6)
                    {
                        Value_Input = this.textBox6.Text;
                    }
                    //connection.Open();
                    Console.WriteLine(Value_Input);
                    connection.Open();
                    cmd.Parameters.AddWithValue("@Player_Number", i);
                    cmd.Parameters.AddWithValue("@Username", Value_Input);
                    cmd.Parameters.AddWithValue("@Password", Value_Input);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            
            
            

        }
        private void Button1_Click(object sender, EventArgs e)
        {
            String Player1 = textBox1.Text; String Player2 = textBox2.Text; String Player3 = textBox3.Text; String Player4 = textBox4.Text; String Player5 = textBox5.Text; String Player6 = textBox6.Text;
            int count = Convert.ToInt32(comboBox1.SelectedItem);
            File.WriteAllText(@"C:\temp\Players.txt", "");
            using (StreamWriter writer = new StreamWriter(@"C:\temp\Players.txt"))
            {
                writer.WriteLine(count);
                writer.WriteLine(Player1);
                writer.WriteLine(Player2);
                writer.WriteLine(Player3);
                writer.WriteLine(Player4);
                writer.WriteLine(Player5);
                writer.WriteLine(Player6);
            }
            //Form2 p = new Form2();
            //p.CreateDBBtn_Click();
            //p.CreateTableBtn_Click();
            Open_DB(count);


            //Process.Start(@"C:\Users\avmis\Documents\unity e safety game\UnityE-safetyGame\BoardgamePrototype\BoardGame\test build\BoardGame.exe");
            //Process.Start(@"C:\temp\Players.txt");
            Process.Start(@"C:\Users\avmis\Documents\unity e safety game\BoardGame-Database1.accdb");
            Application.Exit();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 frm = new Form1();
            frm.Show();
            
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = Convert.ToInt32(comboBox1.SelectedItem);
            if (count < 3)
            {
                textBox3.BackColor = Color.LightGray;
                textBox3.Enabled = false;
            }
            else if (count >= 3)
            {
                textBox3.BackColor = Color.White;
                textBox3.Enabled = true;
            }
            if (count < 4)
            {
                textBox4.BackColor = Color.LightGray;
                textBox4.Enabled = false;
            }
            else if (count >= 4)
            {
                textBox4.BackColor = Color.White;
                textBox4.Enabled = true;
            }
            if (count < 5)
            {
                textBox5.BackColor = Color.LightGray;
                textBox5.Enabled = false;
            }
            else if (count >= 5)
            {
                textBox5.BackColor = Color.White;
                textBox5.Enabled = true;
            }
            if (count < 6)
            {
                textBox6.BackColor = Color.LightGray;
                textBox6.Enabled = false;
            }
            else if (count >= 6)
            {
                textBox6.BackColor = Color.White;
                textBox6.Enabled = true;
            }
        }
    }
}
