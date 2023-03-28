using DeskEntity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeskData.Repository
{
    public interface IQRScannerRepository
    {
        #region QR Scanner Interface
        void AddQScanner(QRScanner qScanner);
        void DeleteQScanner(int qScannerId);
        IEnumerable<QRScanner> GetQScanner();
        QRScanner GetQScannerById(int qScannerId);
        void UpdateQScanner(QRScanner qScanner);
        #endregion
    }
}
