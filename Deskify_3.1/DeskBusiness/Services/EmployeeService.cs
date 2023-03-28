using DeskData.Repository;
using DeskEntity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeskBusiness.Services
{
    public  class EmployeeService
    {

        IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        //Update Employee
        public void UpdateEmployee(Employee employee)
        {
            _employeeRepository.UpdateEmployee(employee);
        }

        //Delete Employee
        public void DeleteEmployee(int employeeId)
        {
            _employeeRepository.DeleteEmployee(employeeId);
        }

        //Get EmployeeByID
        public Employee GetEmployeeById(int employeeId)
        {
            return _employeeRepository.GetEmployeeById(employeeId);
        }

        //Get Employees
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeRepository.GetAllEmployees();
        }

        //Registering Employee
        public void AddEmployee(Employee employeeInfo)
        {
            _employeeRepository.AddEmployee(employeeInfo);
        }

        
       
    }
}
