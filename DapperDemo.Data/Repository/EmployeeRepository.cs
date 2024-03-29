﻿using Dapper;
using DapperDemo.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemo.Data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private IDbConnection db;

        public EmployeeRepository(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }




        public Employee Add(Employee employee)
        {
            var sql = "INSERT INTO Employees (Name, Title, Email, Phone, CompanyId) "
                + "VALUES(@Name, @Title, @Email, @Phone, @CompanyId) "
                + "SELECT CAST (SCOPE_IDENTITY() AS int)";

            var id = db.Query<int>(sql, employee).Single();

            employee.EmployeeId = id;
            return employee;
        }
        public async Task<Employee> AddAsync(Employee employee)
        {
            var sql = "INSERT INTO Employees (Name, Title, Email, Phone, CompanyId) "
                + "VALUES(@Name, @Title, @Email, @Phone, @CompanyId) "
                + "SELECT CAST (SCOPE_IDENTITY() AS int)";

            var id = await db.QueryAsync<int>(sql, employee);
            employee.EmployeeId = id.Single();

            return employee;
        }

        public Employee Find(int id)
        {
            var sql = "select * from Employees where EmployeeId = @Id";
            return db.Query<Employee>(sql, new { @Id = id }).Single();
        }

        public async Task<List<Employee>> GetAll()
        {
            var sql = "select * from Employees";
            var result = await db.QueryAsync<Employee>(sql);
            return result.ToList();
        }

        public async Task Remove(int id)
        {
            var sql = "DELETE  FROM Employees WHERE EmployeeId = @Id";

            await db.ExecuteAsync(sql, new { id });

            return;
        }

        public async Task<Employee> Update(Employee employee)
        {
            var sql = "UPDATE Employees SET Name = @Name, Title = @Title, Email = @Email, Phone = @Phone, CompanyId = @CompanyId "
                + "WHERE EmployeeId = @EmployeeId";

            await db.ExecuteAsync(sql, employee);

            return null;
        }
    }
}
