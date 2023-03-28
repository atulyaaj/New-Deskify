using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Numerics;
using System.Text.Json.Serialization;
using System.Text;
using System.Threading.Tasks;
using System;
using DeskData.Data;
using DeskEntity.Model;
using Newtonsoft.Json;

namespace DeskUI.Controllers
{
    public class RoomController : Controller
    {
        //Private member
        private IConfiguration _configuration;

        //Constructor
        public RoomController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        DeskDbContext db = new DeskDbContext();

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Room> roomresult = null;
            using (HttpClient client = new HttpClient())
            {
                // LocalHost Adress in endpoint
                string endPoint = _configuration["WebApiBaseUrl"] + "Room/GetAllRooms";
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        //It will deserilize the object in the form of JSON
                        roomresult = JsonConvert.DeserializeObject<IEnumerable<Room>>(result);
                    }
                }
            }
            return View(roomresult);
        }

        #region Room View-Edit-Delete Actions

        public IActionResult AddRoom()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRoom(Room room)
        {
            ViewBag.status = "";
            //it will update the room details after Admin Changes
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(room), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Room/AddRoom";
                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Room Details Added Successfully!";
                        //return RedirectToAction("Index", "Room");
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries!";
                    }
                }
            }
            return View();
        }

        public async Task<IActionResult> UpdateRoom(int roomId)
        {
            if (roomId != 0)
            {
                //We are Storing Doctor Id  temporary to avoid the error. Now it will show the room details after the update also
                TempData["UpdateroomId"] = roomId;
                TempData.Keep();
            }
            Room room = null;
            //it will fetch the room Details by using DoctorID
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Room/GetRoomsById?roomId=" + Convert.ToInt32(TempData["EditroomId"]);
                TempData.Keep();
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        //It will deserilize the object in the form of JSON
                        room = JsonConvert.DeserializeObject<Room>(result);
                    }
                }
            }
            return View(room);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRoom(Room room)
        {
            ViewBag.status = "";
            //it will update the room details after Admin Changes
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(room), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Room/UpdateRoom";
                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Room Details Updated Successfully!";
                        //return RedirectToAction("GetAllRooms", "Room");
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries!";
                    }
                }
            }
            return View();

        }

        public async Task<IActionResult> DeleteRoom(int roomId)
        {
            ViewBag.status = "";
            //it will Delete the doctor Details by using doctor Id
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Room/DeleteRoom?roomId=" + roomId;
                using (var response = await client.DeleteAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Room Details Deleted Successfully!";
                        return RedirectToAction("Index", "Room");
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries!";
                    }
                }
            }
            return View();

        }

        #endregion
    }
}
