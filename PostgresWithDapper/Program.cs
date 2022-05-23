using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgresWithDapper
{
    class Program
    {
        static void Main(string[] args)
        {


            var connectionString = "host=172.16.0.96;port=5432;database=VMS;user id=postgres;password=12345";
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            var transaction = connection.BeginTransaction();
            try
            {
                string commandText = $"Call sp_searcheventsbycriteriaORcondition('<VMS><SearchData><EventTypeID>100</EventTypeID><ResourceID>fe2ea23d-5502-4b00-a225-96e9b4cfe8c1</ResourceID><MinTime></MinTime><ExtraData></ExtraData><OwnerId></OwnerId><Title></Title><Description></Description><AdvancedFilter>{"IsAnd":false,\"Conditions\":[]}</AdvancedFilter></SearchData></VMS>','30',null,null,null,'OR','result');";
                var result = connection.Query<string>(commandText);

                string fetchCommandText = "fetch all in \"result\";";
                var employees = connection.Query<Employee>(fetchCommandText);

            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                transaction.Commit();
                connection.Close();
            }
        }
    }

    [Table("employees")]
    public class Employee
    {
        [Key]
        [Column("emp_id")]
        public int Emp_id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("dept")]
        public string Dept { get; set; }

        [Column("salary")]
        public string Salary { get; set; }

        [Column("fiforefdate")]
        public string Fiforefdate { get; set; }
    }
}
