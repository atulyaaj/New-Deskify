using DeskBusiness.Services;
using DeskEntity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace DeskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private EmployeeService _employeeService;
        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("GetAllEmployees")]
        public IEnumerable<Employee> GetAllEmployees()
        {
            #region Function for getting  the Employee

            return _employeeService.GetAllEmployees();
            #endregion
        }

        [HttpDelete("DeleteEmployee")]
        public IActionResult DeleteEmployee(int employeeId)
        {
            #region Function for deleting the employee by its employeeId.
            try
            {
                _employeeService.DeleteEmployee(employeeId);
                return Ok("Employee deleted Successfully");
            }
            catch
            {
                return BadRequest(400);
            }
            #endregion
        }

        [HttpPut("UpdateEmployee")]
        public IActionResult UpdateEmployee([FromBody] Employee employee)
        {
            #region Function for updating  the employee its object
            try
            {
                _employeeService.UpdateEmployee(employee);
                return Ok("Employee Updated Successfully");
            }
            catch
            {
                return BadRequest(400);
            }
            #endregion
        }

        [HttpGet("GetEmployeeById")]
        public Employee GetEmployeeById(int employeeId)
        {
            #region Function for getting it all details of the   the employee by  its employeeId

            return _employeeService.GetEmployeeById(employeeId);

            #endregion
        }

        [HttpPost("AddEmployee")]
        public IActionResult AddEmployee([FromBody] Employee employeeInfo)
        {
            #region Function for updating  the employee  by its object
            try
            {
                _employeeService.AddEmployee(employeeInfo);
                return Ok("Register successfully!!");
            }
            catch
            {
                return BadRequest(400);
            }
            #endregion
        }
    }
}
