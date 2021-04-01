using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
//using System.Data.SqlServerCe;

namespace Employees
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SqlConnection cnn = new SqlConnection("Data Source=(localDB)\\MSSQLLocalDB;Initial Catalog=employees_db;Integrated Security=True;Pooling=False");
            cnn.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from Persons", cnn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cnn.Close();
         
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Width = 30;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[1].HeaderText = "Фамилия";
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[2].HeaderText = "Имя";
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[3].HeaderText = "Отчество";
            dataGridView1.Columns[4].Width = 40;
            dataGridView1.Columns[4].HeaderText = "Пол";
            dataGridView1.Columns[5].Width = 100;
            dataGridView1.Columns[5].HeaderText = "Д.р.";
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[6].HeaderText = "С";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Проверка правильности дат
            TimeSpan ts = dateTimePicker2.Value - dateTimePicker1.Value;
            if ((ts.TotalDays / 365) < 14 || dateTimePicker1.Value.Year < 1900
                || dateTimePicker2.Value > DateTime.Now)
            {
                MessageBox.Show("Неправильно введены даты !");
                return;
            }
            SqlConnection cnn = new SqlConnection("Data Source = (localDB)\\MSSQLLocalDB; Initial Catalog = employees_db; Integrated Security = True; Pooling = False");
            cnn.Open();
            /* Эта команда дает ошибку */
            /* datetime заменен на date в таблице */
            
            /* Запрос с использованием PreparedStatements */
            SqlCommand cmd = new SqlCommand(null, cnn);

            cmd.CommandText = "INSERT INTO Persons " +
                    "(lastname, firstname, middlename, birthdate, worksfrom, gender) " +
                    "VALUES (@parLastName, @parFirstName, @parMiddleName, @parBirthDate, " +
                    "@parWorksFrom, @parGender);";

            SqlParameter parLastName = new SqlParameter("@parLastName", SqlDbType.NChar, 20);
            SqlParameter parFirstName = new SqlParameter("@parFirstName", SqlDbType.NChar, 20);
            SqlParameter parMiddleName = new SqlParameter("@parMiddleName", SqlDbType.NChar, 20);
            /* Это решило вопрос с проблемой позиции даты и месяца */
            SqlParameter parBirthDate = new SqlParameter("@parBirthDate", SqlDbType.Date);
            SqlParameter parWorksFrom = new SqlParameter("@parWorksFrom", SqlDbType.Date);
            SqlParameter parGender = new SqlParameter("@parGender", SqlDbType.NChar, 1);

            parLastName.Value = textBox1.Text;
            parFirstName.Value = textBox2.Text;
            parMiddleName.Value = textBox3.Text;
            parBirthDate.Value = dateTimePicker1.Value.ToShortDateString();
            parWorksFrom.Value = dateTimePicker2.Value.ToShortDateString();
            parGender.Value = comboBox1.Text;

            cmd.Parameters.Add(parLastName);
            cmd.Parameters.Add(parFirstName);
            cmd.Parameters.Add(parMiddleName);
            cmd.Parameters.Add(parBirthDate);
            cmd.Parameters.Add(parWorksFrom);
            cmd.Parameters.Add(parGender);

            cmd.Prepare();
                     
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter("select * from Persons", cnn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cnn.Close();
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            SqlConnection cnn = new SqlConnection("Data Source = (localDB)\\MSSQLLocalDB; Initial Catalog = employees_db; Integrated Security = True; Pooling = False");
            cnn.Open();

            /* Переписано с использованием PreparedStatements */
            SqlCommand cmd = new SqlCommand(null, cnn);
            
            cmd.CommandText = "DELETE FROM Persons WHERE id_person=@id;";

            SqlParameter parID = new SqlParameter("@id", SqlDbType.Int);

            parID.Value = id;

            cmd.Parameters.Add(parID);

            cmd.Prepare();

            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter("select * from Persons", cnn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cnn.Close();
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Проверка правильности дат
            TimeSpan ts = dateTimePicker2.Value - dateTimePicker1.Value;
            if ((ts.TotalDays / 365) < 14 || dateTimePicker1.Value.Year < 1900
                || dateTimePicker2.Value > DateTime.Now)
            {
                MessageBox.Show("Неправильно введены даты !");
                return;
            }
            string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            SqlConnection cnn = new SqlConnection("Data Source=(localDB)\\MSSQLLocalDB;Initial Catalog=employees_db;Integrated Security=True;Pooling=False");
            cnn.Open();
            
            /* Переписано с использованием PreparedStatements */
            SqlCommand cmd = new SqlCommand(null, cnn);

            cmd.CommandText = "UPDATE Persons SET lastname=@parLastName, firstname=@parFirstName, " +
                        "middlename=@parMiddleName, birthdate=@parBirthDate, worksfrom=@parWorksFrom, " +
                        "gender=@parGender WHERE id_person=@parID";

            SqlParameter parLastName = new SqlParameter("@parLastName", SqlDbType.NChar, 20);
            SqlParameter parFirstName = new SqlParameter("@parFirstName", SqlDbType.NChar, 20);
            SqlParameter parMiddleName = new SqlParameter("@parMiddleName", SqlDbType.NChar, 20);
            SqlParameter parBirthDate = new SqlParameter("@parBirthDate", SqlDbType.Date);
            SqlParameter parWorksFrom = new SqlParameter("@parWorksFrom", SqlDbType.Date);
            SqlParameter parGender = new SqlParameter("@parGender", SqlDbType.NChar, 1);
            SqlParameter parID = new SqlParameter("@parID", SqlDbType.Int);

            parID.Value = id;
            parLastName.Value = textBox1.Text;
            parFirstName.Value = textBox2.Text;
            parMiddleName.Value = textBox3.Text;
            parBirthDate.Value = dateTimePicker1.Value.ToShortDateString();
            parWorksFrom.Value = dateTimePicker2.Value.ToShortDateString();
            parGender.Value = comboBox1.Text;

            cmd.Parameters.Add(parID);
            cmd.Parameters.Add(parLastName);
            cmd.Parameters.Add(parFirstName);
            cmd.Parameters.Add(parMiddleName);
            cmd.Parameters.Add(parBirthDate);
            cmd.Parameters.Add(parWorksFrom);
            cmd.Parameters.Add(parGender);

            cmd.Prepare();
                      
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter("select * from Persons", cnn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cnn.Close();
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                comboBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                dateTimePicker2.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            }
            catch { };
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            DateTime now = DateTime.Now;
            DateTime born = DateTime.Parse(dataGridView1.CurrentRow.Cells[5].Value.ToString());
            DateTime from = DateTime.Parse(dataGridView1.CurrentRow.Cells[6].Value.ToString());
            Boolean man = dataGridView1.CurrentRow.Cells[4].Value.ToString() == "М";
            DateTime toPens;
            if (man)
                toPens = born.AddYears(60);
            else
                toPens = born.AddYears(55);
            MessageBox.Show("Возраст : " + 
                (Convert.ToInt32((now - born).TotalDays / 365)).ToString() +
                "\nВыход на пенсию : " + toPens.ToShortDateString() +
                "\nСтаж работы (лет) : " + 
                (Convert.ToInt32((now - from).TotalDays / 365)).ToString() + 
                "\n" + (now > toPens ? "На" : "До") + " пенсии (лет) : " + 
                (Convert.ToInt32((now > toPens ? (now - toPens) :
                    (toPens - now)).TotalDays / 365)).ToString(), 
                dataGridView1.CurrentRow.Cells[1].Value.ToString() + " " +
                dataGridView1.CurrentRow.Cells[2].Value.ToString().Substring(0,1)+"." +
                dataGridView1.CurrentRow.Cells[3].Value.ToString().Substring(0,1)+".");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.dt = (DataTable)dataGridView1.DataSource;
            frm.Show();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
