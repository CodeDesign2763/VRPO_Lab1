using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Employees
{
    class EmployeeReport
    {
        private List<Employee> emplList;
        public EmployeeReport(List<Employee> el)
        {
            emplList = el;
        }
        public List<RepItem> getReport()
        {
            int total=0;
            int menTotal=0;
            int womanTotal=0;
            int retTotal = 0;
            int retMTotal=0;
            int retWTotal=0;
            double avgAge=0;
            double avgTimeToRet = 0;
            foreach (Employee empl in emplList)
            {
                total++;
                avgAge = avgAge + empl.getAge();
                avgTimeToRet += empl.getTimeToRet();
                if (empl.isRet()) retTotal++;

                if (empl.getGender()=='М')
                {
                    menTotal++;
                    if (empl.isRet()) retMTotal++;
                }
            }
            womanTotal = total - menTotal;
            retWTotal = retTotal - retMTotal;
            avgAge = avgAge / total;
            avgTimeToRet = avgTimeToRet / total;

            /* Формирование отчета */
            List<RepItem> report = new List<RepItem>();
            report.Add(new RepItem("Сотрудников всего", Convert.ToString(total)));
            report.Add(new RepItem("Мужчин", Convert.ToString(menTotal)));
            report.Add(new RepItem("Женщин", Convert.ToString(womanTotal)));
            report.Add(new RepItem("Пенсионеров всего", Convert.ToString(retTotal)));
            report.Add(new RepItem("Пенсионеров мужчин", Convert.ToString(retMTotal)));
            report.Add(new RepItem("Пенсионеров женщин", Convert.ToString(retWTotal)));
            report.Add(new RepItem("Средний возраст", Convert.ToString(avgAge)));
            report.Add(new RepItem("Среднее время до пенсии", Convert.ToString(avgTimeToRet)));




            return report;
        }
    }
}
