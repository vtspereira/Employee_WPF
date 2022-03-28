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

        public static bool Create(Employee employee)
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

            //var result = cmd.ExecuteNonQuery();
            

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
