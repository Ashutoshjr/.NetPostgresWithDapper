using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
                string commandText = $"Call sp_eventsearch(2,'result');";
                connection.Query<string>(commandText);

                string fetchCommandText = "fetch all in \"result\";";
                var employees = connection.Query<Employee>(fetchCommandText);

                // Display fetched employee records
                foreach (var employee in employees)
                {
                    Console.WriteLine($"ID: {employee.Emp_id}, Name: {employee.Name}, Dept: {employee.Dept}, Salary: {employee.Salary}, Date: {employee.Fiforefdate}");
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
            finally
            {
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
