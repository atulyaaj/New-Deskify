using DeskEntity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeskData.Repository
{
    public interface ILoginTableRepository
    {
        #region LoginTable Interface
        public int[] Login(LoginTable loginTable);
        public int AddLoginTable(LoginTable loginTable);
        public LoginTable ForgetPassword(LoginTable loginTable);
        public void UpdatePassword(LoginTable login);
        #endregion
    }
}
