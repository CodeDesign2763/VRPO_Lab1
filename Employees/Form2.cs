using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Employees
{
    public partial class statWindow : Form
    {
        private DataTable dt;
        private List<Employee> emplList;
        //private List<RepItem> report;
        

        public statWindow(DataTable dataTable, List<Employee> el)
        {
            dt = dataTable;
            emplList = el;
            InitializeComponent();
        }
        public statWindow()
        {
            InitializeComponent();
        }

        private void statWindow_Load(object sender, EventArgs e)
        {
            EmployeeReport er = new EmployeeReport(emplList);
            showReport(er.getReport());
        }

       private void showReport(List<RepItem> rep)
       {
            /* Предварительная очиска */
            statDGV.DataSource = null;
            statDGV.ColumnCount = 2;

            statDGV.Columns[0].Width = 200;
            statDGV.Columns[1].Width = 60;

            for (int i=0;i<=rep.Count-1;i++)
            {
                statDGV.Rows.Add();
                statDGV.Rows[i].Cells[0].Value = rep[i].getItemName();
                statDGV.Rows[i].Cells[1].Value = rep[i].getItemValue();
            }
       }
        private void statTimePicker_ValueChanged(object sender, EventArgs e)
        {
            /* Функция отключена */

            //int tot = 0;
            //int men = 0;
            //int women = 0;
            //int pens = 0;
            //int menp = 0;
            //int womenp = 0;
            //double toPens = 0;
            //double midAge = 0;

            //DateTime now = statTimePicker.Value;

            //// Статистика по работникам
            //foreach (DataRow dr in dt.Rows)
            //{
            //    try
            //    {
            //        if (dr["id_person"].ToString() == "") continue;

            //        if (dr["gender"].ToString() == "М")
            //            men++;
            //        else
            //            women++;
            //        DateTime toPens1;
            //        DateTime born = DateTime.Parse(dr["birthdate"].ToString());
            //        if (dr["gender"].ToString() == "М")
            //        {
            //            toPens1 = born.AddYears(60);
            //            if (toPens1 < now)
            //                menp++;
            //        }
            //        else
            //        {
            //            toPens1 = born.AddYears(55);
            //            if (toPens1 < now)
            //                womenp++;
            //        }
            //        if (toPens1 < now) pens++;
            //        toPens += (toPens1 - now).TotalDays / 365;
            //        midAge += (now - born).TotalDays / 365;
            //        tot++;
            //    }
            //    catch { }
            //}
            //if (tot > 0)
            //{
            //    toPens = toPens / tot;
            //    midAge = midAge / tot;
            //}

            //statDGV.Rows[0].Cells[0].Value = "Всего сотрудников";
            //statDGV.Rows[0].Cells[1].Value = tot.ToString();
            //statDGV.Rows[1].Cells[0].Value = "Мужчин";
            //statDGV.Rows[1].Cells[1].Value = men.ToString();
            //statDGV.Rows[2].Cells[0].Value = "Женщин";
            //statDGV.Rows[2].Cells[1].Value = women.ToString();
            //statDGV.Rows[3].Cells[0].Value = "Пенсионеров";
            //statDGV.Rows[3].Cells[1].Value = pens.ToString();
            //statDGV.Rows[4].Cells[0].Value = "Пенсионеров мужчин";
            //statDGV.Rows[4].Cells[1].Value = menp.ToString();
            //statDGV.Rows[5].Cells[0].Value = "Пенсионеров женщин";
            //statDGV.Rows[5].Cells[1].Value = menp.ToString();
            //statDGV.Rows[6].Cells[0].Value = "Средний возраст";
            //statDGV.Rows[6].Cells[1].Value = midAge.ToString();
            //statDGV.Rows[7].Cells[0].Value = "До пенсии лет (ср.)";
            //statDGV.Rows[7].Cells[1].Value = toPens.ToString();

        }

    }
}
