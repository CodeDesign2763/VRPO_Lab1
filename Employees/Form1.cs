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
    public partial class MainWindow : Form
    {
        private DBI database;
        private void initEmployeesDGV()
        {
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
        private void showTable(DataSet ds)
        {
            employeesDGV.DataSource = ds.Tables[0];
        }
        public MainWindow()
        {
            InitializeComponent();
            /* Инициализация БД */
            database = new DB("Data Source=(localDB)\\MSSQLLocalDB;Initial Catalog=employees_db;Integrated Security=True;Pooling=False");
            /* Подключение к БД */
            database.connect();


            /* Вывод таблицы на экран */
            showTable(database.getEmployeesTable());

            /* Настройка employeesDGV */
            initEmployeesDGV();
        }

        /* Проверка корректности введеных дат */
        private bool checkDate(DateTimePicker wFDTP,DateTimePicker bDDTP)
        {
            bool f = true;
            TimeSpan ts = wFDTP.Value - bDDTP.Value;
            if ((ts.TotalDays / 365) < 14 || bDDTP.Value.Year < 1900
                || wFDTP.Value > DateTime.Now)
            {
                f = false;
            }
            return f;
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            // Проверка правильности дат
            if (checkDate(worksFromTimePicker, birthDateTimePicker))
            {
                // Если даты верные

                database.addEmployee(
                        new Employee(0, lastNameTextBox.Text,
                        firstNameTextBox.Text,
                        middleNameTextBox.Text,
                        genderComboBox.Text,
                        birthDateTimePicker.Value.ToShortDateString(),
                        worksFromTimePicker.Value.ToShortDateString()
                        ));

                /* Вывод обновленной таблицы */
                showTable(database.getEmployeesTable());
                             
            }
            /* Если даты неверные, то вывести сообщения */
            else MessageBox.Show("Неправильно введены даты !");
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(employeesDGV.CurrentRow.Cells[0].Value.ToString());
            /* Удаление сотрудника из базы */
            database.deleteEmployee(new Employee(id));

            /* Вывод обновленной информации */
            showTable(database.getEmployeesTable());
                    
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (checkDate(worksFromTimePicker, birthDateTimePicker))
            {
                int id = Convert.ToInt32(employeesDGV.CurrentRow.Cells[0].Value.ToString());

                database.updateEmployee(
                        new Employee(id, lastNameTextBox.Text,
                        firstNameTextBox.Text,
                        middleNameTextBox.Text,
                        genderComboBox.Text,
                        birthDateTimePicker.Value.ToShortDateString(),
                        worksFromTimePicker.Value.ToShortDateString()
                        ));

                showTable(database.getEmployeesTable());
            }
            else MessageBox.Show("Неправильно введены даты !");
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
            /* Получение данных из таблицы */
            String born = DateTime.Parse(employeesDGV.CurrentRow.Cells[5].Value.ToString()).ToShortDateString();
            String from = DateTime.Parse(employeesDGV.CurrentRow.Cells[6].Value.ToString()).ToShortDateString();
            String gender = employeesDGV.CurrentRow.Cells[4].Value.ToString();
            
            /* Создание на их основе экземпляра класса Employee */
            Employee empl = new Employee(0, null, null, null, gender, born, from);    
            
            /* Вывод сообщения на экран */
            MessageBox.Show(empl.getStatMessage());

        }

        private void statButton_Click(object sender, EventArgs e)
        {
            //List<Employee> el = database.getEmployeesList();
            StatWindow frm = new StatWindow((DataTable)employeesDGV.DataSource,database.getEmployeesList());
            
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

        private void mainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            database.disconnect();
        }

        private void employeesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
