using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using DeskEntity.Model;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace DeskUI.Controllers
{
    public class BookingRoomController : Controller
    {
        private IConfiguration _configuration;


        public BookingRoomController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<BookingRoom> bookingroomresult = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "BookingRoom/GetBookingRoom";
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        bookingroomresult = JsonConvert.DeserializeObject<IEnumerable<BookingRoom>>(result);
                    }
                }

            }
            return View(bookingroomresult);
        }

        public IActionResult AddBookingRooms()
        {
            return View();  
        }


        [HttpPost]
        public async Task<IActionResult> AddBookingRooms(BookingRoom bookingRoom)
        {
            ViewBag.status = "";
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(bookingRoom), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "BookingRoom/AddBookingRoom";
                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "BookingRoom details saved sucessfully!!";
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
        public async Task<IActionResult> EditBookingRooms(int RoomId)
        {
            BookingRoom bookingRoom = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "BookingRoom/GetBookingRoomById?bookingRoomId=" + RoomId;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        bookingRoom = JsonConvert.DeserializeObject<BookingRoom>(result);
                    }
                }
            }
            return View(bookingRoom);

        }

        [HttpPost]

        public async Task<IActionResult> EditBookingRooms(BookingRoom bookingRoom)
        {
            ViewBag.status = "";
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(bookingRoom), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "BookingRoom/UpdateBookingRoom";
                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "BookingRoom details updated sucessfully!!";
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

        public IActionResult DeleteBookingRooms()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBookingRooms(int RoomId)
        {
            BookingRoom bookingRoom = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "BookingRoom/DeleteBookingRoom?bookingRoomId=" + RoomId;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "BookingRoom details deleted sucessfully!!";
                        return RedirectToAction("Index", "BookingRoom");
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
