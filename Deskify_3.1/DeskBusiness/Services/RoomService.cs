using DeskData.Repository;
using DeskEntity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeskBusiness.Services
{
    public class RoomService
    {
        IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public void AddRoom(Room room)
        {
            _roomRepository.AddRoom(room);
        }

        public void DeleteRoom(int roomId)
        {
            _roomRepository.DeleteRoom(roomId);
        }

        public void UpdateRoom(Room room)
        {
            _roomRepository.UpdateRoom(room);
        }

        public Room GetRoomsById(int roomId)
        {
            return _roomRepository.GetRoomsById(roomId);
        }

        public IEnumerable<Room> GetAllRooms()
        {
            return _roomRepository.GetAllRooms();
        }
    }
}
