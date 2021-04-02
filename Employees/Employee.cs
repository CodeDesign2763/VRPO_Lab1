using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Employees
{
    class Employee
    {
        private String lastName;
        private String firstName;
        private String middleName;
        private int id;
        private DateTime birthDate;
        private DateTime worksFrom;
        private Char gender;

        public Employee(int parID)
        {
            id = parID;
        }
        public Employee(int parID, String ln, String fn, String mn, String g, String bd, String wf )
        {
            id = parID;
            lastName = ln;
            firstName = fn;
            middleName = mn;
            
            gender = Convert.ToChar(g);
            birthDate = DateTime.ParseExact(bd, "dd.MM.yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);
            worksFrom = DateTime.ParseExact(wf, "dd.MM.yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);
        }
        public String getLastName()
        {
            return lastName;
        }
        public String getFirstName()
        {
            return firstName;
        }
        public String getMiddleName()
        {
            return middleName;
        }
        public int getID()
        {
            return id;
        }
        public DateTime getBirthDate()
        {
            return birthDate;
        }
        public DateTime getWorksFrom()
        {
            return worksFrom;
        }
        public Char getGender()
        {
            return gender;
        }


        /* Статистика по человеку */
        public String getStatMessage()
        {
            DateTime now = DateTime.Now;
            DateTime born = getBirthDate();
            DateTime from = getWorksFrom();


            /* Дата выхода на пенсию */
            DateTime toPens;
            if (getGender()=='М')
                toPens = getBirthDate().AddYears(60);
            else
                toPens = getBirthDate().AddYears(55);

            

            /* Возраст */
            String age = (Convert.ToInt32((now - born).TotalDays / 365)).ToString();

            /* Стаж работы */
            String lengthOfWork = (Convert.ToInt32((now - from).TotalDays / 365)).ToString();

            /* Все еще работает? */
            bool isWorking = now > toPens ? false : true;

            /* Статус */
            String status = isWorking ? "Работает" : "Вышел на пенсию";

            /* Осталось времени до пенсии */
            String timeLeftUntilRetirement = "";

            /* Сколько уже находится на пенсии? */
            String lengthOfWorkAfterRetirement = "";

            /* Формируем сообщение для пользователя */
            string message = "Возраст: " + age + "\n" +
                    "Дата выхода на пенсию: " + toPens.ToShortDateString() + "\n" +
                    "Стаж работы: (лет) " + lengthOfWork + "\n" +
                    "Статус: " + status + "\n";

            if (isWorking)
            {
                timeLeftUntilRetirement = (System.Math.Ceiling((toPens - now).TotalDays / 365)).ToString();
                message += "Осталось до пенсии: (лет) " + timeLeftUntilRetirement;
            }
            else
            {
                lengthOfWorkAfterRetirement = (now - toPens).ToString();
                message += "Получает пенсию: (лет) " + timeLeftUntilRetirement;
            }
            return message;
        }

    }
}
