using DeskEntity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeskData.Repository
{
    public interface IEmployeeRepository
    {
        void AddEmployee(Employee employee);
        void DeleteEmployee(int employeeId);
        void UpdateEmployee(Employee employee);
        Employee GetEmployeeById(int employeeId);
        IEnumerable<Employee> GetAllEmployees();
    }
}
