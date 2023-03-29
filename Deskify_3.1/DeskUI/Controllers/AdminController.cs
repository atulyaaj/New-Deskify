using DeskEntity.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeskUI.Controllers
{
    public class AdminController : Controller
    {
        IConfiguration _configuration;

        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditEmployee(int employeeId)
        {
            #region Editing/Updating Employee Get Mthod to View

            Employee employee = null;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string endPoint = _configuration["WebApiBaseUrl"] + "Employee/GetEmployeeById?employeeId=" + employeeId;//EmployeeId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in Employeecontroller of api
                    using (var response = await client.GetAsync(endPoint))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {   //dynamic viewbag we can create any variable name in run time
                            var result = await response.Content.ReadAsStringAsync();
                            employee = JsonConvert.DeserializeObject<Employee>(result);
                        }
                    }
                }
            }
            catch (System.NullReferenceException ex)
            {
                Console.WriteLine(ex.ToString());

            }
            catch (Exception ex1)
            {
                Console.WriteLine(ex1.ToString());

            }
            return View(employee);
            #endregion
        }
        [HttpPost]
        public async Task<IActionResult> EditEmployee(Employee employee)
        {
            #region Editing Employee Post method
            ViewBag.status = "";
            try
            {
                //using grabage collection only for inbuilt classes
                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
                    string endPoint = _configuration["WebApiBaseUrl"] + "Employee/UpdateEmployee";//api controller name and its function

                    using (var response = await client.PutAsync(endPoint, content))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {   //dynamic viewbag we can create any variable name in run time
                            ViewBag.status = "Ok";
                            ViewBag.message = "Employees Details Updated Successfull!!";
                        }

                        else
                        {
                            ViewBag.status = "Error";
                            ViewBag.message = "Wrong Entries";
                        }

                    }
                }
            }
            catch (System.NullReferenceException ex)
            {
                Console.WriteLine(ex.ToString());

            }
            catch (Exception ex1)
            {
                Console.WriteLine(ex1.ToString());

            }
            return View();
            #endregion
        }

        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            #region Deleting Employee with id from database
            ViewBag.status = "";
            //using grabage collection only for inbuilt classes
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    string endPoint = _configuration["WebApiBaseUrl"] + "Employee/DeleteEmployee?employeeId=" + employeeId;  //api controller name and its function

                    using (var response = await client.DeleteAsync(endPoint))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {   //dynamic viewbag we can create any variable name in run time
                            ViewBag.status = "Ok";
                            ViewBag.message = "Employees Details Deleted Successfull!!";

                        }

                        else
                        {
                            ViewBag.status = "Error";
                            ViewBag.message = "Wrong Entries";
                        }

                    }
                }
            }
            catch (System.NullReferenceException ex)
            {
                Console.WriteLine(ex.ToString());

            }
            catch (Exception ex1)
            {
                Console.WriteLine(ex1.ToString());

            }
            return View();
            #endregion
        }
        public IActionResult GetAllEmployees()
        {
            #region  Index of all  the employee present
            return View();
            #endregion
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees(Employee employee)
        {
            #region Getting all Employees List from database

            IEnumerable<Employee> employeeresult = null;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string endPoint = _configuration["WebApiBaseUrl"] + "Employee/GetAllEmployees";//api controller name and httppost name given inside httppost in moviecontroller of api

                    using (var response = await client.GetAsync(endPoint))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {   //dynamic viewbag we can create any variable name in run time
                            var result = await response.Content.ReadAsStringAsync();
                            employeeresult = JsonConvert.DeserializeObject<IEnumerable<Employee>>(result);
                        }
                    }
                }
            }
            catch (System.NullReferenceException ex)
            {
                Console.WriteLine(ex.ToString());

            }
            catch (Exception ex1)
            {
                Console.WriteLine(ex1.ToString());

            }
            return View(employeeresult);
            #endregion


        }
    }
}
