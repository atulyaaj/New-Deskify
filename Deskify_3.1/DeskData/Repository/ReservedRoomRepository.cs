using DeskData.Data;
using DeskEntity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeskData.Repository
{
    public class ReservedRoomRepository : IReservedRoomRepository
    {
        DeskDbContext _db;
        public ReservedRoomRepository(DeskDbContext db)
        {
            _db = db;
        }
        public void AddReservedRoom(ReservedRoom reservedRoom)
        {
            _db.reservedRooms.Add(reservedRoom);
            _db.SaveChanges();
        }

        public void DeleteReservedRoom(int reservedRoomId)
        {
            var reservedRoom = _db.reservedRooms.Find(reservedRoomId);
            _db.reservedRooms.Remove(reservedRoom);
            _db.SaveChanges();
        }

        public IEnumerable<ReservedRoom> GetAllReservedRooms()
        {
            return _db.reservedRooms.ToList();
        }

        public ReservedRoom GetReservedRoomById(int reservedRoomId)
        {
            return _db.reservedRooms.Find(reservedRoomId);
        }

        public void UpdateReservedRoom(ReservedRoom reservedRoom)
        {
            _db.Entry(reservedRoom).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
