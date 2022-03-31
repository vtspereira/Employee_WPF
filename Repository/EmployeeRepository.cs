using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlServerCe;
using Funcionários.Models;

namespace Funcionários.Repository
{
    public  class EmployeeRepository
    {
        private static SqlCeConnection connection = new SqlCeConnection(@"Data Source=V:\Programação\Visual Studio\Funcionários\Database\Database.sdf");
        public static DataTable GetEmployee()
        {
            SqlCeDataAdapter data = new SqlCeDataAdapter("SELECT * FROM Funcionario", connection);

            DataSet ds = new DataSet();

            data.Fill(ds);

            return ds.Tables[0];
        }

        public static Models.Employee Get(int id)
        {
            Models.Employee employee = new Models.Employee();
            string sql = "SELECT * FROM [Funcionario] WHERE Id = @id;";

            SqlCeCommand cmd = new SqlCeCommand(sql, connection);
            cmd.Parameters.Add("@Id", id);

            connection.Open();

            SqlCeDataReader result = cmd.ExecuteReader();

            while (result.Read())
            {
                employee.Id = result.GetInt32(0);
                employee.Name = result.GetString(1);
                employee.Email = result.GetString(2);
                employee.Wage = result.GetDecimal(3);
                employee.Gender = result.GetString(4);
                employee.ContractType = result.GetString(5);
                employee.CreatedAt = result.GetDateTime(6);

                if (!result.IsDBNull(7))
                    employee.UpdatedAt = result.GetDateTime(7);
            }

            connection.Close();

            return employee;
        }

        public static bool Create(Models.Employee employee)
        {
            string sql = "INSERT INTO [Funcionario](Name, Email, Wage, Gender, ContractType, CreatedAt)" +
                "VALUES " +
                "(" +
                    "@Name, @Email, @Wage, @Gender, @ContractType, @CreatedAt" +
                ");";

            SqlCeCommand cmd = new SqlCeCommand(sql, connection);
            cmd.Parameters.Add("@Name", employee.Name);
            cmd.Parameters.Add("@Email", employee.Email);
            cmd.Parameters.Add("@Wage", employee.Wage);
            cmd.Parameters.Add("@Gender", employee.Gender);
            cmd.Parameters.Add("@ContractType", employee.ContractType);
            cmd.Parameters.Add("@CreatedAt", employee.CreatedAt);

            connection.Open();

            if (cmd.ExecuteNonQuery() > 0)
            {
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }
        }

        public static bool Update(Models.Employee employee)
        {
            string sql = "UPDATE [Funcionario] SET Name = @Name, Email = @Email, Wage = @Wage, Gender = @Gender, ContractType = @ContractType, UpdatedAt = @UpdatedAt WHERE Id = @Id;";

            SqlCeCommand cmd = new SqlCeCommand(sql, connection);
            cmd.Parameters.AddWithValue("@Id", employee.Id);
            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Email", employee.Email);
            cmd.Parameters.AddWithValue("@Wage", employee.Wage);
            cmd.Parameters.AddWithValue("@Gender", employee.Gender);
            cmd.Parameters.AddWithValue("@ContractType", employee.ContractType);
            cmd.Parameters.AddWithValue("@UpdatedAt", employee.UpdatedAt);

            connection.Open();

            if (cmd.ExecuteNonQuery() > 0)
            {
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }
        }

        public static bool Delete(int id)
        {
            Models.Employee employee = new Models.Employee();
            string sql = "DELETE FROM [Funcionario] WHERE Id = @id;";

            SqlCeCommand cmd = new SqlCeCommand(sql, connection);
            cmd.Parameters.Add("@Id", id);

            connection.Open();

            if (cmd.ExecuteNonQuery() > 0)
            {
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }
        }
    }
}
