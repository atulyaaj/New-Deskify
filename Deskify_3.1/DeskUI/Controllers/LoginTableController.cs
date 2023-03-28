using DeskEntity.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DeskUI.Controllers
{
    public class LoginTableController : Controller
    {
        private IConfiguration _configuration;
        public LoginTableController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginTable loginTable)
        {
            #region Admin and employee can login
            ViewBag.status = "";
            if (loginTable.Email == null && loginTable.Password == null)
            {
                ViewBag.status = "Error";
                ViewBag.message = "Email and Password is required";

            }
            else
            {
                int[] arr = new int[2];
                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(loginTable), Encoding.UTF8, "application/json");
                    string endpoint = _configuration["WebApiBaseUrl"] + "LoginTable/Login";
                    using (var response = await client.PostAsync(endpoint, content))
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        arr = JsonConvert.DeserializeObject<int[]>(result);

                        TempData.Keep();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            TempData["ProfileId"] = arr[0].ToString();
                            TempData.Keep();
                            ViewBag.status = "Ok";
                            ViewBag.message = "Login Succesfully";
                            if (arr[1] == 0)
                            {
                                TempData["EmployeeID"] = arr[0].ToString();
                                TempData.Keep();

                                return RedirectToAction("Index", "Employee");
                            }
                            else if (arr[1] == 3)
                            {
                                return RedirectToAction("Index", "Admin");
                            }

                        }
                        else
                        {
                            ViewBag.status = "Error";
                            ViewBag.message = "Invalid Email Address/Password";
                        }
                    }
                }
            }
            return View();
            #endregion
        }
    }
}
