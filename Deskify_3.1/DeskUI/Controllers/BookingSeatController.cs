using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text;
using System.Threading.Tasks;
using DeskEntity.Model;
using Newtonsoft.Json;

namespace DeskUI.Controllers
{
    public class BookingSeatController : Controller
    {
        private IConfiguration _configuration;

        public BookingSeatController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> AllBookingSeats()
        {
            IEnumerable<BookingSeat> bookingseatresult = null;
            using (HttpClient client = new HttpClient())
            {

                string endPoint = _configuration["WebApiBasedUrl"] + "BookingSeat/GetAllBookingSeats";
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        bookingseatresult = JsonConvert.DeserializeObject<IEnumerable<BookingSeat>>(result);
                    }
                }
            }
            return View(bookingseatresult);
        }


        public IActionResult AddBookingSeat()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBookingSeat(BookingSeat bookseat)
        {

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(bookseat), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBasedUrl"] + "BookingSeat/AddSeatBooking";
                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Booking Seat Added!!";
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

        public async Task<IActionResult> DeleteBookingSeat(int BookSeatId)
        {
            BookingSeat seat = new BookingSeat();
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBasedUrl"] + "BookingSeat/GetSeatBookingById?bookingseatId=" + BookSeatId;

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        seat = JsonConvert.DeserializeObject<BookingSeat>(result);
                    }

                }
            }
            return View(seat);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBookingSeat(BookingSeat bookseat)

        {
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBasedUrl"] + "BookingSeat/DeleteSeatBooking?bookingseatId=" + bookseat.BookingSeatId;
                using (var response = await client.DeleteAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "BookingSeat details deleted successfully";
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



        public async Task<IActionResult> EditBookingSeat(int BookSeatId)
        {
            BookingSeat seat = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBasedUrl"] + "BookingSeat/GetSeatBookingById?bookingseatId=" + BookSeatId;

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        seat = JsonConvert.DeserializeObject<BookingSeat>(result);
                    }

                }
            }
            return View(seat);
        }
        [HttpPost]
        public async Task<IActionResult> EditBookingSeat(BookingSeat bookseat)

        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(bookseat), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBasedUrl"] + "BookingSeat/UpdateSeatBooking";
                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "BookingSeat Updated!!";
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

