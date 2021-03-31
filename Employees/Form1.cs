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
            /* Не работает автоинкремент ??? */
            SqlCommand cmd = new SqlCommand(
                "insert into Persons (lastname, firstname, middlename," +
                "birthdate, worksfrom, gender) values ('" + 
                textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" +
                dateTimePicker1.Value.ToShortDateString() + "','" +
                dateTimePicker2.Value.ToShortDateString() + "','" + 
                comboBox1.Text + "')", cnn);
            MessageBox.Show("insert into Persons (lastname, firstname, middlename," +
                "birthdate, worksfrom, gender) values ('" +
                textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" +
                dateTimePicker1.Value.ToShortDateString() + "','" +
                dateTimePicker2.Value.ToShortDateString() + "','" +
                comboBox1.Text + "')");
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
            SqlCommand cmd = new SqlCommand(
                "delete from Persons where id_person = " + id, cnn);
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
            SqlCommand cmd = new SqlCommand(
                "update Persons set lastname = '" + textBox1.Text + "'," + 
                               "firstname = '" + textBox2.Text + "'," +
                               "middlename = '" + textBox3.Text + "'," +
                               "birthdate = '" + dateTimePicker1.Value.ToShortDateString() + "'," +
                               "worksfrom = '" + dateTimePicker2.Value.ToShortDateString() + "', " +
                               "gender = '" + comboBox1.Text + "' " +
                               "where id_person = " + id, cnn);
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
    }
}
