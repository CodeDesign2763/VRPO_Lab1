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

        public Employee(int par_id, String ln, String fn, String mn, String g, String bd, String wf )
        {
            id = par_id;
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

    }
}
