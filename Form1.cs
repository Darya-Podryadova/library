using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "database1DataSet.Students". При необходимости она может быть перемещена или удалена.
            this.studentsTableAdapter.Fill(this.database1DataSet.Students);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "database1DataSet.Books". При необходимости она может быть перемещена или удалена.
            this.booksTableAdapter.Fill(this.database1DataSet.Books);
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Книги"].ConnectionString);

            sqlConnection.Open();

            if (sqlConnection.State == ConnectionState.Open)
            {
                MessageBox.Show("Подключение установлено!");
            }
        }
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    SqlCommand command = new SqlCommand(
        //        $"INSERT INTO [Books] (Name_book, Name_author, Name_publish, Year__, Count__) VALUES (@Name_book, @Name_author, @Name_publish, @Year__, @Count__)", sqlConnection);

        //    command.Parameters.AddWithValue("Name_book", textBox1.Text);
        //    command.Parameters.AddWithValue("Name_author", textBox2.Text);
        //    command.Parameters.AddWithValue("Name_publish", textBox3.Text);
        //    command.Parameters.AddWithValue("Year__", textBox4.Text);
        //    command.Parameters.AddWithValue("Count__", textBox5.Text);

        //    MessageBox.Show(command.ExecuteNonQuery().ToString());
        //}
        private void button1_Click_1(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand(
                $"INSERT INTO [Books] (Name_book, Name_author, Name_publish, Year__, Count__) VALUES (@Name_book, @Name_author, @Name_publish, @Year__, @Count__)", sqlConnection);

            command.Parameters.AddWithValue("Name_book", textBox1.Text);
            command.Parameters.AddWithValue("Name_author", textBox2.Text);
            command.Parameters.AddWithValue("Name_publish", textBox3.Text);
            command.Parameters.AddWithValue("Year__", textBox4.Text);
            command.Parameters.AddWithValue("Count__", textBox5.Text);

            MessageBox.Show(command.ExecuteNonQuery().ToString());
        }
        //private void button2_Click(object sender, EventArgs e)
        //{
        //    SqlCommand command = new SqlCommand(
        //       $"INSERT INTO [Students] (Name, Surname) VALUES (@Name, @Surname)", sqlConnection);

        //    command.Parameters.AddWithValue("Name", textBox6.Text);
        //    command.Parameters.AddWithValue("Surname", textBox7.Text);


        //    MessageBox.Show(command.ExecuteNonQuery().ToString());
        //}
        private void button2_Click_1(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand(
               $"INSERT INTO [Students] (Name, Surname) VALUES (@Name, @Surname)", sqlConnection);

            command.Parameters.AddWithValue("Name", textBox6.Text);
            command.Parameters.AddWithValue("Surname", textBox7.Text);


            MessageBox.Show(command.ExecuteNonQuery().ToString());
        }
        //private void button3_Click(object sender, EventArgs e)
        //{
        //    SqlCommand command = new SqlCommand(
        //       $"INSERT INTO [Borrow] (BookId, StudentId, Borrow, Back) VALUES (@BookId, @StudentId, @Borrow, @Back)", sqlConnection);


        //    DateTime dateB = DateTime.Parse(textBox8.Text);
        //    DateTime dateR = DateTime.Parse(textBox9.Text);

        //    string book = comboBox1.SelectedValue.ToString();
        //    string student = comboBox2.SelectedValue.ToString();

        //    command.Parameters.AddWithValue("BookId", book);
        //    // MessageBox.Show(comboBox1.SelectedValue.ToString());
        //    command.Parameters.AddWithValue("StudentId", student);
        //    // MessageBox.Show(comboBox2.SelectedValue.ToString());


        //    SqlCommand choose = sqlConnection.CreateCommand();
        //    choose.CommandText = "SELECT IF(BookID = {book} AND StudentId = {student}, Borrow) FROM Borrow;";

        //    DateTime result = ((DateTime)choose.ExecuteScalar());

        //    MessageBox.Show(result.ToString());

        //    command.Parameters.AddWithValue("Borrow", $"{dateB.Month}/{dateB.Day}/{dateB.Year}");
        //    command.Parameters.AddWithValue("Back", $"{dateR.Month}/{dateR.Day}/{dateR.Year}");

        //    MessageBox.Show(command.ExecuteNonQuery().ToString());

        //}

        private void button3_Click_1(object sender, EventArgs e)
        {
            SqlCommand commandB = new SqlCommand(
               $"INSERT INTO [Borrow] (BookId, StudentId, Borrow) VALUES (@BookId, @StudentId, @Borrow)", sqlConnection);
            SqlCommand commandR = new SqlCommand(
               $"INSERT INTO [Borrow] (BookId, StudentId, Borrow, Back) VALUES (@BookId, @StudentId, @Borrow,  @Back)", sqlConnection);
            SqlCommand command = new SqlCommand(
               $"INSERT INTO [Borrow] (BookId, StudentId) VALUES (@BookId, @StudentId)", sqlConnection);



            string book = comboBox1.SelectedValue.ToString();
            string student = comboBox2.SelectedValue.ToString();

            command.Parameters.AddWithValue("BookId", book);
            command.Parameters.AddWithValue("StudentId", student);
           


            SqlCommand choose = sqlConnection.CreateCommand();
            choose.CommandText = $"SELECT Borrow FROM Borrow WHERE (BookId = {book} AND StudentId = {student});";
            try
            {
                DateTime result = ((DateTime)choose.ExecuteScalar());
                MessageBox.Show(result.ToString());
                commandR.Parameters.AddWithValue("BookId", book);
                commandR.Parameters.AddWithValue("StudentId", student);
                DateTime dateR = DateTime.Parse(textBox9.Text);
                commandR.Parameters.AddWithValue("Borrow", result);
                commandR.Parameters.AddWithValue("Back", $"{dateR.Month}/{dateR.Day}/{dateR.Year}");
                

            }
            catch
            {

                commandB.Parameters.AddWithValue("BookId", book);
                commandB.Parameters.AddWithValue("StudentId", student);
                DateTime dateB = DateTime.Parse(textBox8.Text);
                commandB.Parameters.AddWithValue("Borrow", $"{dateB.Month}/{dateB.Day}/{dateB.Year}");
                MessageBox.Show(commandB.ExecuteNonQuery().ToString());

            }
            finally
            {
                MessageBox.Show(commandR.ExecuteNonQuery().ToString());
            }


        }


    }


}
