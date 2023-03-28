using DeskEntity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeskData.Repository
{
    public interface IRoomRepository
    {
        void AddRoom(Room room);
        void DeleteRoom(int roomId);
        void UpdateRoom(Room room);
        Room GetRoomsById(int roomId);
        IEnumerable<Room> GetAllRooms();
    }
}
