using DeskBusiness.Services;
using DeskEntity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DeskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservedRoomController : ControllerBase
    {
        ReservedRoomService _reservedRoomService;
        public ReservedRoomController(ReservedRoomService reservedRoomService)
        {
            _reservedRoomService = reservedRoomService;
        }

        [HttpPost("AddReservedRoom")]
        public IActionResult AddReservedRoom(ReservedRoom reservedRoom)
        {
            _reservedRoomService.AddReservedRoom(reservedRoom);
            return Ok("Reserved Room Added Successfully");

        }

        [HttpDelete("DeleteReservedRoom")]
        public IActionResult DeleteReservedRoom(int reservedRoomId)
        {
            _reservedRoomService.DeleteReservedRoom(reservedRoomId);
            return Ok("Reserved Room Deleted Successfully");
        }

        [HttpPut("UpdateReservedRoom")]
        public IActionResult UpdateReservedRoom(ReservedRoom reservedRoom)
        {
            _reservedRoomService.UpdateReservedRoom(reservedRoom);
            return Ok("Reserved Room Updated Successfully");
        }

        [HttpGet("GetAllReservedRooms")]
        public IEnumerable<ReservedRoom> GetAllReservedRooms()
        {
            return _reservedRoomService.GetAllReservedRooms();
        }

        [HttpGet("GetReservedRoomById")]
        public ReservedRoom GetReservedRoomById(int reservedRoomId)
        {
            return _reservedRoomService.GetReservedRoomById(reservedRoomId);
        }
    }
}
