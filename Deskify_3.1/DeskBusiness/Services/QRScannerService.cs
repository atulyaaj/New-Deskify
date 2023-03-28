using DeskData.Repository;
using DeskEntity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeskBusiness.Services
{
    public class QRScannerService
    {
        IQRScannerRepository _qScannerRepository;
        public QRScannerService(IQRScannerRepository qScannerRepository)
        {
            _qScannerRepository = qScannerRepository;
        }

        #region Serice for AddQScanner

        public void AddQScanner(QRScanner qScanner)
        {
            _qScannerRepository.AddQScanner(qScanner);
        }
        #endregion Serice for AddQScanner

        #region Serice for UpdateQScanner

        public void UpdateQScanner(QRScanner qScanner)
        {
            _qScannerRepository.UpdateQScanner(qScanner);
        }
        #endregion Serice for UpdateQScanner

        #region Serice for DeleteQScanner

        public void DeleteQScanner(int qScannerId)
        {
            _qScannerRepository.DeleteQScanner(qScannerId);
        }
        #endregion Serice for AddQScanner

        #region Serice for GetQScannerById

        public QRScanner GetQScannerById(int qScannerId)
        {
            return _qScannerRepository.GetQScannerById(qScannerId);
        }
        #endregion Serice for GetQScannerById

        #region Serice for GetQScanner

        public IEnumerable<QRScanner> GetQScanner()
        {
            return _qScannerRepository.GetQScanner();
        }
        #endregion Serice for GetQScanner
    }
}
