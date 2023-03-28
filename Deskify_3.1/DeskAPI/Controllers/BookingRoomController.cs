using DeskBusiness.Services;
using DeskEntity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DeskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingRoomController : ControllerBase
    {

        private BookingRoomService _bookingRoomService;
        public BookingRoomController(BookingRoomService bookingRoomService)
        {
            _bookingRoomService = bookingRoomService;
        }
        #region GET ROOMS

        [HttpGet("GetBookingRoom")]
        public IEnumerable<BookingRoom> GetBookingRoom()
        {
            return _bookingRoomService.GetBookingRoom();
        }
        #endregion

        #region ROOMBYID

        [HttpGet("GetBookingRoomById")]
        public BookingRoom GetBookingRoomById(int bookingRoomId)
        {
            return _bookingRoomService.GetBookingRoomById(bookingRoomId);
        }
        #endregion

        #region ADD ROOM

        [HttpPost("AddBookingRoom")]
        public IActionResult AddBookingRoom([FromBody] BookingRoom bookingRoom)
        {
            _bookingRoomService.AddBookingRoom(bookingRoom);
            return Ok("BookingRoom added successfully");
        }
        #endregion

        #region DELETE ROOM

        [HttpDelete("DeleteBookingRoom")]
        public IActionResult DeleteBookingRoom(int bookingRoomId)
        {
            _bookingRoomService.DeleteBookingRoom(bookingRoomId);
            return Ok("BookingRoom deleted successfully");
        }
        #endregion

        #region UPDATE ROOM

        [HttpPut("UpdateBookingRoom")]
        public IActionResult UpdateBookingRoom([FromBody] BookingRoom bookingRoom)
        {
            _bookingRoomService.UpdateBookingRoom(bookingRoom);
            return Ok("BookingRoom updated successfully");
        }
        #endregion

    }
}



