using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Employees
{
    interface DBI
    {
        DataSet getPersonsTable();
        void connect();
        void disconnect();
        void addEmployee(Employee empl);
        void deleteEmployee(Employee empl);
        void updateEmployee(Employee empl);


    }
}
