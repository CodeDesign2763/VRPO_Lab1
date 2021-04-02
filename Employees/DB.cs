using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Employees
{
    class DB : DBI
    {
        SqlConnection cnn;
        public DB(String conStr)
        {
            cnn = new SqlConnection(conStr);
           
        }
        public void connect()
        {
            cnn.Open();
        }
        public void disconnect()
        {
            cnn.Close();
        }

        public DataSet getPersonsTable()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from Persons", cnn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public void addEmployee(Employee empl)
        {
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

            parLastName.Value = empl.getLastName();
            parFirstName.Value = empl.getFirstName();
            parMiddleName.Value = empl.getMiddleName();
            parBirthDate.Value = empl.getBirthDate().ToShortDateString();
            parWorksFrom.Value = empl.getWorksFrom().ToShortDateString();
            parGender.Value = empl.getGender();

            cmd.Parameters.Add(parLastName);
            cmd.Parameters.Add(parFirstName);
            cmd.Parameters.Add(parMiddleName);
            cmd.Parameters.Add(parBirthDate);
            cmd.Parameters.Add(parWorksFrom);
            cmd.Parameters.Add(parGender);

            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }

        public void deleteEmployee(Employee empl)
        {
            /* Переписано с использованием PreparedStatements */
            SqlCommand cmd = new SqlCommand(null, cnn);

            cmd.CommandText = "DELETE FROM Persons WHERE id_person=@id;";

            SqlParameter parID = new SqlParameter("@id", SqlDbType.Int);

            parID.Value = empl.getID();

            cmd.Parameters.Add(parID);

            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }

        public void updateEmployee(Employee empl)
        {
            /* Обновление */
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

            parID.Value = empl.getID();
            parLastName.Value = empl.getLastName();
            parFirstName.Value = empl.getFirstName();
            parMiddleName.Value = empl.getMiddleName();
            parBirthDate.Value = empl.getBirthDate().ToShortDateString();
            parWorksFrom.Value = empl.getWorksFrom().ToShortDateString();
            parGender.Value = empl.getGender();

            cmd.Parameters.Add(parID);
            cmd.Parameters.Add(parLastName);
            cmd.Parameters.Add(parFirstName);
            cmd.Parameters.Add(parMiddleName);
            cmd.Parameters.Add(parBirthDate);
            cmd.Parameters.Add(parWorksFrom);
            cmd.Parameters.Add(parGender);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

        }
    }
}
