using DeskData.Data;
using DeskEntity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeskData.Repository
{
    public  class EmployeeRepository:IEmployeeRepository
    {
        DeskDbContext _db;
        public EmployeeRepository(DeskDbContext db)
        {
            _db = db;
        }
        public void AddEmployee(Employee employee)
        {
            _db.employees.Add(employee);
            _db.SaveChanges();
        }

        public void DeleteEmployee(int employeeId)
        {
            var employee = _db.employees.Find(employeeId);
            _db.employees.Remove(employee);
            _db.SaveChanges();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _db.employees.ToList();
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return _db.employees.Find(employeeId);
        }

      

        public void UpdateEmployee(Employee employee)
        {
            _db.Entry(employee).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}

   
