using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text;
using System.Threading.Tasks;
using DeskEntity.Model;
using Newtonsoft.Json;

namespace DeskUI.Controllers
{
    public class FloorController : Controller
    {
        private IConfiguration _configuration;

        public FloorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Floor> floorresult = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Floor/GetFloor";
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        floorresult = JsonConvert.DeserializeObject<IEnumerable<Floor>>(result);
                    }
                }
            }
            return View(floorresult);
        }



        public IActionResult AddFloors()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> AddFloors(Floor floor)
        {
            ViewBag.status = "";
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(floor), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Floor/AddFloor";
                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Floor details saved sucessfully!!";
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong entries!";
                    }
                }
            }
            return View();
        }

        public async Task<IActionResult> EditFloor(int floorId)
        {
            Floor floor = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Floor/GetFloorById?Id=" + floorId;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        floor = JsonConvert.DeserializeObject<Floor>(result);
                    }
                }
            }
            return View(floor);
        }

        [HttpPost]
        public async Task<IActionResult> EditFloor(Floor floor)
        {
            ViewBag.status = "";
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(floor), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Floor/UpdateFloor";
                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Floor details updated sucessfully!!";
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong entries!";
                    }
                }
            }
            return View();
        }
        public IActionResult DeleteFloors()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteFloors(int floorID)
        {
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Floor/DeleteFloor?floorId=" + floorID;

                using (var response = await client.DeleteAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Floor details deleted sucessfully!!";
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong entries!";
                    }
                }
            }
            return View();
        }
    }
}
