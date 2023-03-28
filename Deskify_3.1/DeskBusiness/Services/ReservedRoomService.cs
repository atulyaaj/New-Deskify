using DeskData.Repository;
using DeskEntity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeskBusiness.Services
{
    public class ReservedRoomService
    {
        IReservedRoomRepository _reservedRoomRepository;

        public ReservedRoomService(IReservedRoomRepository reservedRoomRepository)
        {
            _reservedRoomRepository = reservedRoomRepository;
        }

        public void AddReservedRoom(ReservedRoom reservedRoom)
        {
            _reservedRoomRepository.AddReservedRoom(reservedRoom);
        }

        public void DeleteReservedRoom(int reservedRoomId)
        {
            _reservedRoomRepository.DeleteReservedRoom(reservedRoomId);
        }

        public void UpdateReservedRoom(ReservedRoom reservedRoom)
        {
            _reservedRoomRepository.UpdateReservedRoom(reservedRoom);
        }

        public ReservedRoom GetReservedRoomById(int reservedRoomId)
        {
            return _reservedRoomRepository.GetReservedRoomById(reservedRoomId);
        }

        public IEnumerable<ReservedRoom> GetAllReservedRooms()
        {
            return _reservedRoomRepository.GetAllReservedRooms();
        }
    }
}
