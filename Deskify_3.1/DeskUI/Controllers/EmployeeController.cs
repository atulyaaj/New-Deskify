﻿
using DeskEntity.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using ZXing;

namespace DeskUI.Controllers
{
    public class EmployeeController : Controller
    {
        IConfiguration _configuration;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public static BookingSeat bookingSeat1 = new BookingSeat();
        public static BookingRoom bookingRoom1 = new BookingRoom();

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int EmployeeProfileId = Convert.ToInt32(TempData["ProfileID"]);
            TempData.Keep();
            Employee employee = new Employee();
            BookingSeat bookingSeat = new BookingSeat();
            using (HttpClient client = new HttpClient())
            {
                string endpoint = _configuration["WebApiBaseUrl"] + "BookingSeat/GetSeatBookingByEmployeeId?employeeid=" + EmployeeProfileId;
                using (var response = await client.GetAsync(endpoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        bookingSeat = JsonConvert.DeserializeObject<BookingSeat>(result);

                    }
                }
            }

            using (HttpClient client = new HttpClient())
            {
                string endpoint = _configuration["WebApiBaseUrl"] + "Employee/GetEmployeeById?employeeId=" + EmployeeProfileId;
                using (var response = await client.GetAsync(endpoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        employee = JsonConvert.DeserializeObject<Employee>(result);
                    }
                }
            }
            if (bookingSeat.Employee == null)
            {
                bookingSeat.SeatShiftTime = employee.EmployeeName;
            }
            return View(bookingSeat);

        }

		public async Task<IActionResult> Profile()
        {
            #region Employee profile
            //storing the profile Id
            int EmployeeProfileId = Convert.ToInt32(TempData["ProfileID"]);
            TempData.Keep();

            Employee employee = null;
            using (HttpClient client = new HttpClient())
            {
                string endpoint = _configuration["WebApiBaseUrl"] + "Employee/GetEmployeeById?employeeId=" + EmployeeProfileId;
                using (var response = await client.GetAsync(endpoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        employee = JsonConvert.DeserializeObject<Employee>(result);
                    }
                }
            }
            return View(employee);
            #endregion
        }

        public IActionResult AddEmployee()
        {
            return View();

        }


        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            #region Adding employee Post Method
            try
            {
                ViewBag.status = "";

                //using grabage collection only for inbuilt classes
                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
                    string endPoint = _configuration["WebApiBaseUrl"] + "Employee/AddEmployee";//api controller name and its function

                    using (var response = await client.PostAsync(endPoint, content))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {   //dynamic viewbag we can create any variable name in run time
                            ViewBag.status = "Ok";
                            ViewBag.message = "Employee Added Successfull!!";
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

        public List<SelectListItem> GetGender()
        {
            #region Gender list
            List<SelectListItem> gender = new List<SelectListItem>()
            {
                new SelectListItem{Value="Select",Text="Select"},
                new SelectListItem{Value="M",Text="Male"},
                new SelectListItem{Value="F",Text="Female"},
                new SelectListItem{Value="O",Text="Others"},



           };
            return gender;
            #endregion
        }

        public async Task<IActionResult> EditEmployee(int employeeId)
        {
            #region Editing/Updating Employee Get Method to View
            //employeeId = 5;
            Employee employee = null;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string endPoint = _configuration["WebApiBaseUrl"] + "Employee/GetEmployeeById?employeeId=" + employeeId;
                    //EmployeeId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in Employeecontroller of api
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
            ViewBag.genderlist = GetGender();
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
                if (Request.Form.Files.Count > 0)
                {
                    MemoryStream ms = new MemoryStream();
                    Request.Form.Files[0].CopyTo(ms);
                    employee.Image = ms.ToArray();
                }
                //using grabage collection only for inbuilt classes
                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
                    string endPoint = _configuration["WebApiBaseUrl"] + "Employee/UpdateEmployee";
                    //api controller name and its function
                    using (var response = await client.PutAsync(endPoint, content))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {   //dynamic viewbag we can create any variable name in run time
                            ViewBag.status = "Ok";
                            ViewBag.message = "Employees Details Updated Successfully";
                            return RedirectToAction("Profile", "Employee");
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

        public List<SelectListItem> ShiftTiming()
        {
            List<SelectListItem> shiftTiming = new List<SelectListItem>()
            {
                new SelectListItem { Value="Shift time", Text="Select Shift Time"},
                new SelectListItem { Value = "09:00 AM - 06:00 PM", Text = "09:00 AM - 06:00 PM" },
                new SelectListItem { Value = "06:00 AM - 02:00 PM", Text = "06:00 AM - 02:00 PM" },
                new SelectListItem { Value = "02:00 PM - 10:00 PM", Text = "02:00 PM - 10:00 PM" },
                new SelectListItem { Value = "10:00 AM - 06:00 PM", Text = "10:00 AM - 06:00 PM" },
            };
            return shiftTiming;
        }


        #region BookingSeat
        public async Task<IActionResult> BookingSeat()
        {

            //Getlatestbookingbyemployeeid
            BookingSeat bookingseats = null;

            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "BookingSeat/GetSeatBookingByEmployeeId?employeeid=" + Convert.ToInt32(TempData["ProfileID"]);
                TempData.Keep();
                //EmployeeId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in Employeecontroller of api
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        bookingseats = JsonConvert.DeserializeObject<BookingSeat>(result);
                    }
                }
            }


            if (bookingseats != null)
            {
                if ((bookingseats.SeatStatus == 0 || bookingseats.SeatStatus == 1) && (bookingseats.FromDate <= DateTime.Today && bookingseats.ToDate >= DateTime.Today))
                {
                    return View(bookingseats);
                }
            }

            List<Floor> floors = new List<Floor>();
            using (HttpClient client = new HttpClient())
            {
                string endpoint = _configuration["WebApiBaseUrl"] + "Floor/GetFloor";
                using (var response = await client.GetAsync(endpoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        floors = JsonConvert.DeserializeObject<List<Floor>>(result);
                    }
                }
            }
            List<SelectListItem> floor = new List<SelectListItem>();

            //fetching the departments and adding to the Viewbag for selecting Floor


            floor.Add(new SelectListItem { Value = null, Text = "Select Floor" });
            foreach (var item in floors)
            {
                floor.Add(new SelectListItem { Value = item.FloorId.ToString(), Text = item.FloorName });
            }

            ViewBag.FloorList = floor;
            ViewBag.shiftTimings = ShiftTiming();
            return View();


        }


        [HttpPost]
        public async Task<IActionResult> BookingSeat(BookingSeat bookingSeat)
        {
            bookingSeat.EmployeeID = Convert.ToInt32(TempData["EmployeeID"]);
            TempData.Keep();
            bookingSeat.SeatId = 2;
            TempData["floorId"] = bookingSeat.Seat.FloorId;
            bookingSeat.Seat = null;
            int bookingSeatId = 0;
            ViewBag.status = "";
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(bookingSeat), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "BookingSeat/AddSeatBooking";
                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        bookingSeatId = JsonConvert.DeserializeObject<int>(result);
                        TempData["bookingSeatId"] = bookingSeatId;
                        TempData.Keep();
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
            bookingSeat1 = bookingSeat;
            return RedirectToAction("GetSeatsByFloorId");
        }

        public async Task<IActionResult> GetSeatsByFloorId(int SeatId)
        {


            if (SeatId != 0)
            {

                BookingSeat bookingSeat1 = new BookingSeat();
                Seat seat = new Seat();

                // Availabel and unavailable

                using (HttpClient client = new HttpClient())
                {
                    string endPoint = _configuration["WebApiBaseUrl"] + "Seat/GetSeatsById?seatId=" + SeatId;
                    //EmployeeId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in Employeecontroller of api
                    using (var response = await client.GetAsync(endPoint))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {   //dynamic viewbag we can create any variable name in run time
                            var result = await response.Content.ReadAsStringAsync();
                            seat = JsonConvert.DeserializeObject<Seat>(result);
                        }
                    }
                }

                seat.Status = true;

                // Updating in seat table


                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(seat), Encoding.UTF8, "application/json");
                    string endPoint = _configuration["WebApiBaseUrl"] + "Seat/UpdateSeat";
                    using (var response = await client.PutAsync(endPoint, content))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            ViewBag.status = "Ok";

                        }
                        else
                        {
                            ViewBag.status = "Error";
                            ViewBag.message = "Wrong Entries";
                        }
                    }
                }




                bookingSeat1.SeatStatus = 0;

                using (HttpClient client = new HttpClient())
                {
                    string endPoint = _configuration["WebApiBaseUrl"] + "BookingSeat/GetSeatBookingById?bookingseatId=" + Convert.ToInt32(TempData["bookingSeatId"]);
                    //EmployeeId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in Employeecontroller of api
                    using (var response = await client.GetAsync(endPoint))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {   //dynamic viewbag we can create any variable name in run time
                            var result = await response.Content.ReadAsStringAsync();
                            bookingSeat1 = JsonConvert.DeserializeObject<BookingSeat>(result);
                        }
                    }
                }

                bookingSeat1.SeatId = SeatId;
                bookingSeat1.SeatStatus = 1;




                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(bookingSeat1), Encoding.UTF8, "application/json");
                    string endPoint = _configuration["WebApiBaseUrl"] + "BookingSeat/UpdateSeatBooking";
                    using (var response = await client.PutAsync(endPoint, content))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            ViewBag.status = "Ok";
                            ViewBag.message = "Booking Seat Added!!";
                            TempData["SeatStatus"] = 1;
                            TempData.Keep();
                            return RedirectToAction("AddChoices", "Employee");
                        }
                        else
                        {
                            ViewBag.status = "Error";
                            ViewBag.message = "Wrong Entries";
                        }
                    }

                    return View();
                }
            }

            int floorId = Convert.ToInt32(TempData["floorId"]);
            List<Seat> seats = new List<Seat>();

            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Seat/GetAllSeatsByFloorId?floorId=" + floorId;
                //EmployeeId is apicontroller passing argument name//api controller name and httppost name given inside httppost in Employeecontroller of api
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        seats = JsonConvert.DeserializeObject<List<Seat>>(result);
                    }
                }
                List<SelectListItem> seats1 = new List<SelectListItem>();
                return View(seats);

            }

        }

        public async Task<IActionResult> CancelBooking(int BookSeatId)
        {
			BookingSeat seat = new BookingSeat();
			using (HttpClient client = new HttpClient())
			{
				string endPoint = _configuration["WebApiBaseUrl"] + "BookingSeat/GetSeatBookingByEmployeeId?employeeid=" + Convert.ToInt32(TempData["ProfileID"]);
				TempData.Keep();

				using (var response = await client.GetAsync(endPoint))
				{
					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						var result = await response.Content.ReadAsStringAsync();
						seat = JsonConvert.DeserializeObject<BookingSeat>(result);
					}
				}
			}
			TempData["seatId"] = seat.SeatId;
			TempData.Keep();
			// Cancelling and making seat available
			Seat seats = new Seat();
			using (HttpClient client = new HttpClient())
			{
				string endPoint = _configuration["WebApiBaseUrl"] + "Seat/GetSeatsById?seatId=" + Convert.ToInt32(TempData["seatId"]); ;
				//EmployeeId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in Employeecontroller of api
				using (var response = await client.GetAsync(endPoint))
				{
					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{   //dynamic viewbag we can create any variable name in run time
						var result = await response.Content.ReadAsStringAsync();
						seats = JsonConvert.DeserializeObject<Seat>(result);
					}
				}
			}
			seats.Status = false;
			// Updating in seat table

			using (HttpClient client = new HttpClient())
			{
				StringContent content = new StringContent(JsonConvert.SerializeObject(seats), Encoding.UTF8, "application/json");
				string endPoint = _configuration["WebApiBaseUrl"] + "Seat/UpdateSeat";
				using (var response = await client.PutAsync(endPoint, content))
				{
					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						ViewBag.status = "Ok";
					}
					else
					{
						ViewBag.status = "Error";
						ViewBag.message = "Wrong Entries";
					}
				}
			}

			//Update booking
			seat.SeatStatus = 2;
			using (HttpClient client = new HttpClient())
			{
				StringContent content = new StringContent(JsonConvert.SerializeObject(seat), Encoding.UTF8, "application/json");
				string endPoint = _configuration["WebApiBaseUrl"] + "BookingSeat/UpdateSeatBooking";
				using (var response = await client.PutAsync(endPoint, content))
				{
					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						ViewBag.status = "Ok";
						TempData["SeatStatus"] = seat.SeatStatus;
						TempData.Keep();
						TempData["Count"] = Convert.ToInt32(TempData["Count"]) + 1;
						TempData.Keep();
						return RedirectToAction("Index", "Employee");
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


        [HttpGet]
        public async Task<IActionResult> UpdateBooking(int BookSeatId)
        {
            BookingSeat seat = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "BookingSeat/GetSeatBookingById?bookingseatId=" + BookSeatId;

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        seat = JsonConvert.DeserializeObject<BookingSeat>(result);
                    }

                }
            }

            ViewBag.shiftTimings = ShiftTiming();
            return View(seat);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBooking(BookingSeat bookseat)

        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(bookseat), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "BookingSeat/UpdateSeatBooking";
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

        #endregion BookingSeat

        public List<SelectListItem> MeetingHours()
        {
            List<SelectListItem> meetingHours = new List<SelectListItem>()
            {
                new SelectListItem { Value="Shift time", Text="Select Meeting Hours"},
                new SelectListItem { Value = "1 Hour", Text = "1 Hour" },
                new SelectListItem { Value = "2 Hours", Text = "2 Hours" },
                new SelectListItem { Value = "3 Hours", Text = "3 Hours" },
                new SelectListItem { Value = "4 Hours", Text = "4 Hours" },
            };
            return meetingHours;
        }



        #region BookingRoom
        public async Task<IActionResult> BookingRooms()
        {
            List<Floor> floors = new List<Floor>();
            using (HttpClient client = new HttpClient())
            {
                string endpoint = _configuration["WebApiBaseUrl"] + "Floor/GetFloor";
                using (var response = await client.GetAsync(endpoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        floors = JsonConvert.DeserializeObject<List<Floor>>(result);
                    }
                }
            }

            List<SelectListItem> floor = new List<SelectListItem>();

            //fetching the departments and adding to the Viewbag for selecting appointment
            floor.Add(new SelectListItem { Value = null, Text = "Select Floor" });
            foreach (var item in floors)
            {
                floor.Add(new SelectListItem { Value = item.FloorId.ToString(), Text = item.FloorName });
            }

            ViewBag.FloorList = floor;
            ViewBag.meetingHours = MeetingHours();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> BookingRooms(BookingRoom bookingRoom)
        {
            bookingRoom.EmployeeID = Convert.ToInt32(TempData["EmployeeID"]);
            TempData.Keep();
            bookingRoom.RoomId = 1;
            TempData["floorId"] = bookingRoom.Room.FloorId;
            bookingRoom.Room = null;
            int bookingRoomId = 0;
            ViewBag.status = "";
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(bookingRoom), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "BookingRoom/AddBookingRoom";
                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        bookingRoomId = JsonConvert.DeserializeObject<int>(result);
                        TempData["bookingRoomId"] = bookingRoomId;
                        TempData.Keep();
                        ViewBag.status = "Ok";
                        ViewBag.message = "Booking Room Added!!";
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong entries!";
                    }
                }
            }
            bookingRoom1 = bookingRoom;
            return RedirectToAction("GetSeatsByFloorId1");
        }



        public async Task<IActionResult> GetSeatsByFloorId1(int RoomId)
        {
            if (RoomId != 0)
            {
                BookingRoom bookingRoom1 = new BookingRoom();
                Room room = new Room();

                using (HttpClient client = new HttpClient())
                {
                    string endPoint = _configuration["WebApiBaseUrl"] + "Room/GetRoomsById?roomId=" + RoomId;
                    //EmployeeId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in Employeecontroller of api
                    using (var response = await client.GetAsync(endPoint))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {   //dynamic viewbag we can create any variable name in run time
                            var result = await response.Content.ReadAsStringAsync();
                            room = JsonConvert.DeserializeObject<Room>(result);
                        }
                    }
                }
                room.RStatus = true;

                // Updating in room table

                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(room), Encoding.UTF8, "application/json");
                    string endPoint = _configuration["WebApiBaseUrl"] + "Room/UpdateRoom";
                    using (var response = await client.PutAsync(endPoint, content))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            ViewBag.status = "Ok";

                        }
                        else
                        {
                            ViewBag.status = "Error";
                            ViewBag.message = "Wrong Entries";
                        }
                    }
                }


                bookingRoom1.RoomStatus = 0;

                using (HttpClient client = new HttpClient())
                {
                    string endPoint = _configuration["WebApiBaseUrl"] + "BookingRoom/GetBookingRoomById?bookingRoomId=" + Convert.ToInt32(TempData["bookingRoomId"]);
                    TempData.Keep();
                    //EmployeeId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in Employeecontroller of api
                    using (var response = await client.GetAsync(endPoint))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {   //dynamic viewbag we can create any variable name in run time
                            var result = await response.Content.ReadAsStringAsync();
                            bookingRoom1 = JsonConvert.DeserializeObject<BookingRoom>(result);
                        }
                    }
                }

                bookingRoom1.RoomId = RoomId;
                bookingRoom1.RoomStatus = 1;
                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(bookingRoom1), Encoding.UTF8, "application/json");
                    string endPoint = _configuration["WebApiBaseUrl"] + "BookingRoom/UpdateBookingRoom";
                    using (var response = await client.PutAsync(endPoint, content))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            ViewBag.status = "Ok";
                            ViewBag.message = "Booking Room Added!!";
                            return RedirectToAction("UserRoomBookHistory", "Employee");
                        }
                        else
                        {
                            ViewBag.status = "Error";
                            ViewBag.message = "Wrong Entries";
                        }
                    }

                    /* return View();*/
                }
            }

            int floorId = Convert.ToInt32(TempData["floorId"]);
            List<Room> rooms = new List<Room>();

            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Room/GetAllSeatsByFloorId1?floorId=" + floorId;
                //EmployeeId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in Employeecontroller of api
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        rooms = JsonConvert.DeserializeObject<List<Room>>(result);
                    }
                }
                List<SelectListItem> rooms1 = new List<SelectListItem>();
                return View(rooms);
            }

        }

        [HttpGet]
        public async Task<IActionResult> BookingRoomHistory()
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
            ViewBag.meetingHours = MeetingHours();
            return View(bookingroomresult);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBookingRooms(int RoomId)
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
            ViewBag.meetingHours = MeetingHours();
            return View(bookingRoom);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateBookingRooms(BookingRoom bookingRoom)
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

        [HttpGet]
        public async Task<IActionResult> CancelBookingRooms(int RoomId)
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
        public async Task<IActionResult> CancelBookingRooms(BookingRoom bookingRoom)
        {

            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "BookingRoom/DeleteBookingRoom?bookingRoomId=" + bookingRoom.BookingRoomId;
                using (var response = await client.DeleteAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Booked Room cancelled sucessfully!!";
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
        #endregion BookingRegion

        public async Task<IActionResult> SelectingFloor()
        {
            #region Selecting the floor
            List<Floor> floors = new List<Floor>();
            using (HttpClient client = new HttpClient())
            {
                string endpoint = _configuration["WebApiBaseUrl"] + "Floor/GetFloor";
                using (var response = await client.GetAsync(endpoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        floors = JsonConvert.DeserializeObject<List<Floor>>(result);
                    }
                }
            }
            List<SelectListItem> floor = new List<SelectListItem>();

            //fetching the departments and adding to the Viewbag for selecting appointment
            floor.Add(new SelectListItem { Value = null, Text = "Select Floor" });
            foreach (var item in floors)
            {
                floor.Add(new SelectListItem { Value = item.FloorId.ToString(), Text = item.FloorName });
            }

            ViewBag.FloorList = floor;
            return View();
            #endregion
        }




        public IActionResult ChoicesHealth()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> UserSeatBookHstory()
        {
            #region Employee can see his Booking history

            int BookSeatId = Convert.ToInt32(TempData["bookingSeatId"]);
            TempData.Keep();
            BookingSeat bookingSeats = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "BookingSeat/GetSeatBookingById?bookingseatId=" + BookSeatId;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        bookingSeats = JsonConvert.DeserializeObject<BookingSeat>(result);
                        TempData["Count"] = Convert.ToInt32(TempData["Count"]) + 1;
                        TempData.Keep();
                    }
                }
            }

            return View(bookingSeats);
            #endregion

        }

        [HttpGet]
        public async Task<IActionResult> UserRoomBookHistory()
        {
            #region Employee can see his Booking Room history
            int BookingRoomId = Convert.ToInt32(TempData["bookingRoomId"]);
            TempData.Keep();
            BookingRoom bookingRooms = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "BookingRoom/GetBookingRoomById?bookingRoomId=" + BookingRoomId;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        bookingRooms = JsonConvert.DeserializeObject<BookingRoom>(result);
                    }
                }
            }
            /*List<BookingRoom> EmployeeRooms = new List<BookingRoom>();
           foreach (var item in bookingRooms)
            {

                //In User Sear book History User Can view His History when he Booked the seats
                if (BookingRoomId == item.EmployeeID && item.RoomStatus == 1)
                {
                    EmployeeRooms.Add(item);
                }
            }*/
            return View(bookingRooms);
            #endregion

        }

        public IActionResult AddChoices()
        {
            ViewBag.choices = Choices();
            return View();
        }

        public List<SelectListItem> Choices()
        {
            List<SelectListItem> choices = new List<SelectListItem>()
            {
             new SelectListItem{Value="Select",Text="Select"},
             new SelectListItem{Value=true.ToString(),Text="Yes"},
             new SelectListItem{Value=false.ToString(),Text="No"},
            };
            return choices;
        }

        [HttpPost]
        public async Task<IActionResult> AddChoices(Choices choices)
        {

            ViewBag.status = "";
            choices.BookingSeatId = Convert.ToInt32(TempData["bookingSeatId"]);
            TempData.Keep();
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(choices), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Choices/AddChoice";
                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Choices details saved successfully!!";
                        return RedirectToAction("UserSeatBookHstory", "Employee");
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

        [HttpGet]
        public async Task<IActionResult> GetChoices()
        {
            IEnumerable<Choices> Choicesresult = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Choices/GetAllChoices";
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        Choicesresult = JsonConvert.DeserializeObject<IEnumerable<Choices>>(result);
                    }
                }
            }
            return View(Choicesresult);
        }

        public async Task<IActionResult> GenerateQR()
        {
            #region Generating Displaying QR

            Random rnd = new Random();
            int length = 5;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var result2 = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result2.Append(chars[rnd.Next(chars.Length)]);
            }
            string randomNum = result2.ToString();
            int employeeId = Convert.ToInt32(TempData["EmployeeID"]);
            TempData.Keep();
            SecretKey secretKey = new SecretKey();
            secretKey.SecretKeyType = randomNum.ToString();
            secretKey.EmployeeID = employeeId;
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            var bitmap = barcodeWriter.Write(secretKey.SecretKeyType);
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Bmp);
                secretKey.QRCode = ms.ToArray();
            }
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(secretKey), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "SecretKey/AddSecretKey";
                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Secret key details saved sucessfully!!";
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong entries!";
                    }
                }
            }

            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "SecretKey/GetSecretKeyByEmployeeId?employeeId=" + employeeId;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        secretKey = JsonConvert.DeserializeObject<SecretKey>(result);
                        TempData["SecretIdForQr"] = Convert.ToInt32(secretKey.SecretId);
                        TempData.Keep();
                    }
                }
            }

            SecretKey secretKey2 = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "SecretKey/GetSecretKeyById?secretId=" + Convert.ToInt32(TempData["SecretIdForQr"]);
                TempData.Keep();
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        secretKey2 = JsonConvert.DeserializeObject<SecretKey>(result);
                    }
                }
            }
            #endregion

            return View(secretKey2);
        }

        public IActionResult Notification()
        {
            //return View(bookingSeat);
            bookingSeat1.SeatStatus = Convert.ToInt32(TempData["SeatStatus"]);
            TempData.Keep();
            return View(bookingSeat1);
        }

        public async Task<IActionResult> ViewPassSeat()
        {
            int EmployeeProfileId = Convert.ToInt32(TempData["ProfileID"]);
            TempData.Keep();

            BookingSeat bookingSeat = null;
            using (HttpClient client = new HttpClient())
            {
                string endpoint = _configuration["WebApiBaseUrl"] + "BookingSeat/GetSeatBookingByEmployeeId?employeeId=" + EmployeeProfileId;
                using (var response = await client.GetAsync(endpoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        bookingSeat = JsonConvert.DeserializeObject<BookingSeat>(result);
                    }
                }
            }
            return View(bookingSeat);

        }

        public async Task<IActionResult> ViewPassRoom()
        {
            int EmployeeProfileId = Convert.ToInt32(TempData["ProfileID"]);
            TempData.Keep();

            BookingRoom bookingRoom = null;
            using (HttpClient client = new HttpClient())
            {
                string endpoint = _configuration["WebApiBaseUrl"] + "BookingRoom/GetBookingRoomByEmployeeId?employeeId=" + EmployeeProfileId;
                using (var response = await client.GetAsync(endpoint))
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

        [HttpGet]
        public async Task<IActionResult> AllBookingSeats()
        {
            BookingSeat bookingSeat = new BookingSeat();
            IEnumerable<BookingSeat> bookingseatresult = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "BookingSeat/GetAllBookingSeats";
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        bookingseatresult = JsonConvert.DeserializeObject<IEnumerable<BookingSeat>>(result);
                    }
                }
            }
            foreach (var item in bookingseatresult)
            {
                if (item.SeatStatus == 1 && (item.FromDate <= DateTime.Today && item.ToDate >= DateTime.Today))
                {
                    DateTime dt = DateTime.Today;
                    dt.ToString("HH:mm");
                    DateTime p = DateTime.Now;
                    string l = p.ToString("HH");
                    if (l == "12")
                    {
                        BookingSeat seat = new BookingSeat();
                        using (HttpClient client = new HttpClient())
                        {
                            string endPoint = _configuration["WebApiBaseUrl"] + "BookingSeat/GetSeatBookingById?bookingseatId=" + item.BookingSeatId;
                            using (var response = await client.GetAsync(endPoint))
                            {
                                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    var result = await response.Content.ReadAsStringAsync();
                                    seat = JsonConvert.DeserializeObject<BookingSeat>(result);
                                }
                            }
                        }
                        //Update booking
                        seat.SeatStatus = 3;
                        using (HttpClient client = new HttpClient())
                        {
                            StringContent content = new StringContent(JsonConvert.SerializeObject(seat), Encoding.UTF8, "application/json");
                            string endPoint = _configuration["WebApiBaseUrl"] + "BookingSeat/UpdateSeatBooking";
                            using (var response = await client.PutAsync(endPoint, content))
                            {
                                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    ViewBag.status = "Ok";
                                    TempData["SeatStatus"] = seat.SeatStatus;
                                    TempData.Keep();
                                    TempData["Count"] = Convert.ToInt32(TempData["Count"]) + 1;
                                    TempData.Keep();
                                    return RedirectToAction("Index", "BookingRoom");
                                }
                                else
                                {
                                    ViewBag.status = "Error";
                                    ViewBag.message = "Wrong Entries";
                                }
                            }
                        }

                    }


                }

            }
            return View();

        }
    }
}