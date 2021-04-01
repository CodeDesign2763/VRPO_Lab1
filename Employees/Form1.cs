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
    public partial class mainWindow : Form
    {
        public mainWindow()
        {
            InitializeComponent();
            SqlConnection cnn = new SqlConnection("Data Source=(localDB)\\MSSQLLocalDB;Initial Catalog=employees_db;Integrated Security=True;Pooling=False");
            cnn.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from Persons", cnn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cnn.Close();
         
            employeesDGV.DataSource = ds.Tables[0];
            
            /* Настройка employeesDGV */
            employeesDGV.Columns[0].Width = 30;
            employeesDGV.Columns[1].Width = 100;
            employeesDGV.Columns[1].HeaderText = "Фамилия";
            employeesDGV.Columns[2].Width = 100;
            employeesDGV.Columns[2].HeaderText = "Имя";
            employeesDGV.Columns[3].Width = 100;
            employeesDGV.Columns[3].HeaderText = "Отчество";
            employeesDGV.Columns[4].Width = 40;
            employeesDGV.Columns[4].HeaderText = "Пол";
            employeesDGV.Columns[5].Width = 100;
            employeesDGV.Columns[5].HeaderText = "Д.р.";
            employeesDGV.Columns[6].Width = 100;
            employeesDGV.Columns[6].HeaderText = "С";
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            // Проверка правильности дат
            TimeSpan ts = worksFromTimePicker.Value - birthDateTimePicker.Value;
            if ((ts.TotalDays / 365) < 14 || birthDateTimePicker.Value.Year < 1900
                || worksFromTimePicker.Value > DateTime.Now)
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

            parLastName.Value = lastNameTextBox.Text;
            parFirstName.Value = firstNameTextBox.Text;
            parMiddleName.Value = middleNameTextBox.Text;
            parBirthDate.Value = birthDateTimePicker.Value.ToShortDateString();
            parWorksFrom.Value = worksFromTimePicker.Value.ToShortDateString();
            parGender.Value = genderComboBox.Text;

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

            /* Вывод таблицы на экран */
            employeesDGV.DataSource = ds.Tables[0];
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            string id = employeesDGV.CurrentRow.Cells[0].Value.ToString();
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

            /* Вывод обновленной информации на экран */
            employeesDGV.DataSource = ds.Tables[0];
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            // Проверка правильности дат
            TimeSpan ts = worksFromTimePicker.Value - birthDateTimePicker.Value;
            if ((ts.TotalDays / 365) < 14 || birthDateTimePicker.Value.Year < 1900
                || worksFromTimePicker.Value > DateTime.Now)
            {
                MessageBox.Show("Неправильно введены даты !");
                return;
            }
            string id = employeesDGV.CurrentRow.Cells[0].Value.ToString();
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
            parLastName.Value = lastNameTextBox.Text;
            parFirstName.Value = firstNameTextBox.Text;
            parMiddleName.Value = middleNameTextBox.Text;
            parBirthDate.Value = birthDateTimePicker.Value.ToShortDateString();
            parWorksFrom.Value = worksFromTimePicker.Value.ToShortDateString();
            parGender.Value = genderComboBox.Text;

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

            /* Вывод обновленной информации на экран */
            employeesDGV.DataSource = ds.Tables[0];
        }

        private void employeesDGV_SelectionChanged(object sender, EventArgs e)
        {
            /* Передача в текстовые поля соотв. знач. для тек. выбранного сотрудника */
            try
            {
                lastNameTextBox.Text = employeesDGV.CurrentRow.Cells[1].Value.ToString();
                firstNameTextBox.Text = employeesDGV.CurrentRow.Cells[2].Value.ToString();
                middleNameTextBox.Text = employeesDGV.CurrentRow.Cells[3].Value.ToString();
                genderComboBox.Text = employeesDGV.CurrentRow.Cells[4].Value.ToString();
                birthDateTimePicker.Text = employeesDGV.CurrentRow.Cells[5].Value.ToString();
                worksFromTimePicker.Text = employeesDGV.CurrentRow.Cells[6].Value.ToString();
            }
            catch { };
        }

        private void employeesDGV_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string id = employeesDGV.CurrentRow.Cells[0].Value.ToString();
            DateTime now = DateTime.Now;
            DateTime born = DateTime.Parse(employeesDGV.CurrentRow.Cells[5].Value.ToString());
            DateTime from = DateTime.Parse(employeesDGV.CurrentRow.Cells[6].Value.ToString());
            Boolean man = employeesDGV.CurrentRow.Cells[4].Value.ToString() == "М";
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
                employeesDGV.CurrentRow.Cells[1].Value.ToString() + " " +
                employeesDGV.CurrentRow.Cells[2].Value.ToString().Substring(0,1)+"." +
                employeesDGV.CurrentRow.Cells[3].Value.ToString().Substring(0,1)+".");
        }

        private void statButton_Click(object sender, EventArgs e)
        {
            statWindow frm = new statWindow();
            frm.dt = (DataTable)employeesDGV.DataSource;
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
