using DeskData.Data;
using DeskEntity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeskData.Repository
{
    public class RoomRepository : IRoomRepository
    {
        DeskDbContext _db;
        public RoomRepository(DeskDbContext db)
        {
            _db = db;
        }
        #region AddRoom
        public void AddRoom(Room room)
        {
            _db.rooms.Add(room);
            _db.SaveChanges();
        }
        #endregion AddRoom

        #region DeleteRoom
        public void DeleteRoom(int roomId)
        {
            var rooms = _db.rooms.Find(roomId);
            _db.rooms.Remove(rooms);
            _db.SaveChanges();

        }
        #endregion DeleteRoom

        #region  UpdateRoom
        public void UpdateRoom(Room room)
        {
            _db.Entry(room).State = EntityState.Modified;
            _db.SaveChanges();

        }
        #endregion UpdateRoom

        #region GetRoomsById 
        public Room GetRoomsById(int roomId)
        {
            return _db.rooms.Find(roomId);
        }
        #endregion GetRoomsById

        #region GetAllRooms
        public IEnumerable<Room> GetAllRooms()
        {
            return _db.rooms.ToList();
        }
        #endregion GetAllRooms
    }
}
