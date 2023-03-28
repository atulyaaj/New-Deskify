using DeskBusiness.Services;
using DeskEntity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DeskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private RoomService _roomService;

        public RoomController(RoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpPost("AddRoom")]
        public IActionResult AddRoom(Room room)
        {
            _roomService.AddRoom(room);
            return Ok("Room Added Successfully");

        }

        [HttpDelete("DeleteRoom")]
        public IActionResult DeleteRoom(int roomId)
        {
            _roomService.DeleteRoom(roomId);
            return Ok("Room Deleted Successfully");
        }

        [HttpPut("UpdateRoom")]
        public IActionResult UpdateRoom(Room room)
        {
            _roomService.UpdateRoom(room);
            return Ok("Room Updated Successfully");
        }

        [HttpGet("GetAllRooms")]

        public IEnumerable<Room> GetAllRooms()
        {
            return _roomService.GetAllRooms();
        }
        [HttpGet("GetRoomsById")]
        public Room GetRoomById(int roomId)
        {
            return _roomService.GetRoomsById(roomId);
        }
    }
}
