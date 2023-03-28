using DeskData.Data;
using DeskEntity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeskData.Repository
{
    public class SeatRepository:ISeatRepository
    {
        DeskDbContext _db;
        public SeatRepository(DeskDbContext db)
        {
            _db = db;
        }

        #region AddSeat
        public void AddSeat(Seat seat)
        {
            _db.seats.Add(seat);
            _db.SaveChanges();
        }
        #endregion AddSeat

        #region DeleteSeat
        public void DeleteSeat(int seatId)
        {
            var seats = _db.seats.Find(seatId);
            _db.seats.Remove(seats);
            _db.SaveChanges();

        }
        #endregion DeleteSeat

        #region  UpdateSeat
        public void UpdateSeat(Seat seat)
        {
            _db.Entry(seat).State = EntityState.Modified;
            _db.SaveChanges();

        }
        #endregion UpdateSeat

        #region GetSeatsById 
        public Seat GetSeatsById(int seatId)
        {
            return _db.seats.Find(seatId);
        }
        #endregion GetSeatsById

        #region GetAllSeats
        public IEnumerable<Seat> GetAllSeats()
        {
            return _db.seats.ToList();
        }
        #endregion GetAllSeats
    }
}
