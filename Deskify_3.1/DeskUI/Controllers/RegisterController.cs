using DeskEntity.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;

namespace DeskUI.Controllers
{
    public class RegisterController : Controller
    {
        private IConfiguration _configuration;
        public RegisterController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> EmployeeRegister1()
        {
            List<SelectListItem> gender = new List<SelectListItem>()
            {
                new SelectListItem{Value="Gender",Text="Gender"},
                new SelectListItem{Value="M",Text="Male"},
                new SelectListItem{Value="F",Text="Female"},
                new SelectListItem{Value="O",Text="Others"},
            };
            ViewBag.genderlist = gender;
            /* #region Select list before registration
             TempData["Notification"] = 0;
             TempData.Keep();
            */
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> EmployeeRegister1(Employee employee, int Type)
        {
            #region Adding Employee data in the LoginTable
            LoginTable loginTable = new LoginTable();
            if (Type == 0)
            {
                loginTable.Email = employee.LoginTable.Email;
                loginTable.Password = employee.LoginTable.Password;
                loginTable.Type = Type;
            }

            #endregion
            #region Employee can register here only by email and password
            ViewBag.Status = "";
            int loginID = 0;
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(loginTable), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "LoginTable/AddLogin";
                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        loginID = JsonConvert.DeserializeObject<int>(result);
                        if (loginID != 0)
                        {
                            TempData["LoginID"] = loginID.ToString();
                            TempData.Keep();

                        }

                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries";
                    }
                }
            }

            if (Type == 0)
            {
                ViewBag.Status = "";
                Employee employee1 = new Employee();
                employee1.EmployeeID = loginID;
                employee1.EmployeeName = employee.EmployeeName;
                employee1.EmployeeNumber = employee.EmployeeNumber;
                employee1.Role = employee.Role;
                employee1.Gender = employee.Gender;
                employee1.PhoneNumber = employee.PhoneNumber;
                employee1.Image = employee.Image;
                employee1.SecurityQuestion = employee.SecurityQuestion;
                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(employee1), Encoding.UTF8, "application/json");
                    string endPoint = _configuration["WebApiBaseUrl"] + "Employee/AddEmployee";
                    using (var response = await client.PostAsync(endPoint, content))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            ViewBag.status = "Ok";
                            ViewBag.message = "Employee Registered Successfully";
                            return RedirectToAction("Index", "LoginTable");
                        }
                        else
                        {
                            ViewBag.status = "Error";
                            ViewBag.message = "Wrong Entries";
                        }
                    }
                }
            }
            return View();
            #endregion
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(LoginTable loginTable)
        {
            #region Forget Password
            loginTable.Password = "hell";
            LoginTable loginTable1 = new LoginTable();
            ViewBag.status = "";
            int[] arr = new int[2];
            TempData["EmailAddress"] = loginTable.Email;
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(loginTable), Encoding.UTF8, "application/json");
                string endpoint = _configuration["WebApiBaseUrl"] + "LoginTable/ForgetPassword";
                using (var response = await client.PostAsync(endpoint, content))
                {
                    var result = await response.Content.ReadAsStringAsync();
                    loginTable1 = JsonConvert.DeserializeObject<LoginTable>(result);


                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        TempData["ForgetId"] = loginTable1.ID;
                        TempData.Keep();
                        TempData["TypeId"] = loginTable1.Type;
                        TempData.Keep();
                        ViewBag.status = "Ok";
                        ViewBag.message = "Login Succesfully";
                        if (loginTable1.Type == 0)
                        {
                            return RedirectToAction("UpdatePassword", "Register");
                        }
                        else if (loginTable1.Type == 1)
                        {
                            TempData.Keep();
                            return RedirectToAction("UpdatePassword", "Register");
                        }
                    }
                    else
                    {
                        ViewBag.staus = "Error";
                        ViewBag.message = "Wrong Entries";
                    }
                }
            }

            return View();
            #endregion
        }

        public IActionResult UpdatePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePassword(Employee employee)
        {
            #region Update password
            LoginTable loginTable = new LoginTable();
            loginTable.ID = Convert.ToInt32(TempData["ForgetId"]);
            loginTable.Type = Convert.ToInt32(TempData["TypeId"]);
            if (Convert.ToInt32(TempData["TypeId"]) == 1)
            {
                Employee employee1 = null;
                using (HttpClient client = new HttpClient())
                {
                    string endpoint = _configuration["WebApiBaseUrl"] + "Employee/GetEmployeeById?employeeId=" + Convert.ToInt32(TempData["ForgetId"]);
                    using (var response = await client.GetAsync(endpoint))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            employee1 = JsonConvert.DeserializeObject<Employee>(result);
                        }
                    }
                }
                //Checking security code to update password
                if (employee1.SecurityQuestion == employee.SecurityQuestion)
                {
                    loginTable.Password = employee.LoginTable.Password;
                }
            }

            loginTable.Email = TempData["EmailAddress"].ToString();

            ViewBag.status = "";
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(loginTable), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "LoginTable/UpdatePassword";
                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Password Updated Successfully";
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries!";
                    }
                }
            }
            return View();
            #endregion
        }
    }
}
