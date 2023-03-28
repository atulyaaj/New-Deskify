using DeskData.Data;
using DeskEntity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeskData.Repository
{
    public class QRScannerRepository:IQRScannerRepository
    {
        DeskDbContext _db;
        public QRScannerRepository(DeskDbContext db)
        {
            _db = db;
        }
        #region AddQScanner

        public void AddQScanner(QRScanner qScanner)
        {
            _db.qrscanners.Add(qScanner);
            _db.SaveChanges();
        }
        #endregion AddQScanner

        #region DeleteQScanner

        public void DeleteQScanner(int qScannerId)
        {
            var qScanner = _db.qrscanners.Find(qScannerId);
            _db.qrscanners.Remove(qScanner);
            _db.SaveChanges();
        }
        #endregion DeleteQScanner

        #region GetQScanner

        public IEnumerable<QRScanner> GetQScanner()
        {
            return _db.qrscanners.ToList();
        }
        #endregion GetQScanner

        #region GetQScannerById

        public QRScanner GetQScannerById(int qScannerId)
        {
            return _db.qrscanners.Find(qScannerId);
        }
        #endregion GetQScannerById

        #region UpdateQScanner

        public void UpdateQScanner(QRScanner qScanner)
        {
            _db.Entry(qScanner).State = EntityState.Modified;
            _db.SaveChanges();
        }
        #endregion UpdateQScanner
    }
}
