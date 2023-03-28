using DeskEntity.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DeskUI.Controllers
{
    public class SeatController : Controller
    {
        private IConfiguration _configuration;

        public SeatController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> AllSeats()
        {
            IEnumerable<Seat> seatresult = null;
            using (HttpClient client = new HttpClient())
            {

                string endPoint = _configuration["WebApiBasedUrl"] + "Seat/GetAllSeats";
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        seatresult = JsonConvert.DeserializeObject<IEnumerable<Seat>>(result);
                    }
                }
            }
            return View(seatresult);
        }


        public IActionResult AddSeats()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSeats(Seat seat)

        {

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(seat), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBasedUrl"] + "Seat/AddSeat";
                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Seat Booked!!";
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries";
                    }
                }
            }
            return View();
        }

        public async Task<IActionResult> DeleteSeats(int SeatId)
        {
            Seat seat = new Seat();
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBasedUrl"] + "Seat/GetSeatsById?seatId=" + SeatId;

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        seat = JsonConvert.DeserializeObject<Seat>(result);
                    }

                }
            }
            return View(seat);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSeats(Seat seat)

        {
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBasedUrl"] + "Seat/DeleteSeat?seatId=" + seat.SeatId;
                using (var response = await client.DeleteAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Seat details deleted successfully";
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries";
                    }
                }
            }
            return View();
        }



        public async Task<IActionResult> EditSeats(int SeatId)
        {
            Seat seat = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBasedUrl"] + "Seat/GetSeatsById?seatId=" + SeatId;

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        seat = JsonConvert.DeserializeObject<Seat>(result);
                    }

                }
            }
            return View(seat);
        }


        [HttpPost]
        public async Task<IActionResult> EditSeats(Seat seat)

        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(seat), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBasedUrl"] + "Seat/UpdateSeat";
                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Seat Updated!!";
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries";
                    }
                }
            }
            return View();
        }

    }

}
