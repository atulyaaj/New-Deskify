using Microsoft.EntityFrameworkCore;
using DeskData.Data;
using DeskEntity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskData.Repository
{
    public class BookingRoomRepository : IBookingRoomRepository
    {
        DeskDbContext _db;
        public BookingRoomRepository(DeskDbContext db)
        {
            _db = db;
        }
        #region ADD BookingRoom
        public void AddBookingRoom(BookingRoom bookingRoom)
        {
            _db.bookingRooms.Add(bookingRoom);
            _db.SaveChanges();
        }
        #endregion         

        #region GET BookingRoom
        public IEnumerable<BookingRoom> GetBookingRoom()
        {
            return _db.bookingRooms.ToList();
        }
        #endregion  

        #region Get BookingRoomById
        public BookingRoom GetBookingRoomById(int bookingRoomId)
        {
            return _db.bookingRooms.Find(bookingRoomId);
        }
        #endregion

        #region UPDATE BookingRoom
        public void UpdateBookingRoom(BookingRoom bookingRoom)
        {
            _db.Entry(bookingRoom).State = EntityState.Modified;
            _db.SaveChanges(); ;
        }
        #endregion

        #region DELETE BookingRoom
        public void DeleteBookingRoom(int bookingRoomId)
        {
            var rooms = _db.bookingRooms.Find(bookingRoomId);
            _db.bookingRooms.Remove(rooms);
            _db.SaveChanges();
        }
        #endregion
    }
}
