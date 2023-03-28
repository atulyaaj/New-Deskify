using DeskEntity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeskData.Repository
{
    public interface IReservedRoomRepository
    {
        void AddReservedRoom(ReservedRoom reservedRoom);
        void UpdateReservedRoom(ReservedRoom reservedRoom);
        void DeleteReservedRoom(int reservedRoomId);
        ReservedRoom GetReservedRoomById(int reservedRoomId);
        IEnumerable<ReservedRoom> GetAllReservedRooms();
    }
}
