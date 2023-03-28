using DeskData.Data;
using DeskEntity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeskData.Repository
{
    public class FloorRepository:IFloorRepository
    {
        DeskDbContext _db;
        public FloorRepository(DeskDbContext db)
        {
            _db = db;
        }

        #region ADD FLOOR

        public void AddFloor(Floor floor)
        {
            _db.floors.Add(floor);
            _db.SaveChanges();
        }

        #endregion

        #region DELETE FLOOR

        public void DeleteFloor(int floorId)
        {
            var floor = _db.floors.Find(floorId);
            _db.floors.Remove(floor);
            _db.SaveChanges();
        }

        #endregion


        #region GET FLOOR

        public IEnumerable<Floor> GetFloor()
        {
            return _db.floors.ToList();
        }

        #endregion

        #region GETFLOORBYID

        public Floor GetFloorById(int floorId)
        {
            return _db.floors.Find(floorId);
        }

        #endregion

        #region UPDATE FLOOR

        public void UpdateFloor(Floor floor)
        {
            _db.Entry(floor).State = EntityState.Modified;
            _db.SaveChanges();
        }

        #endregion
    }
}
