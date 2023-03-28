using DeskData.Repository;
using DeskEntity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeskBusiness.Services
{
    public class LoginTableService
    {
        ILoginTableRepository _logintableRepository;
        public LoginTableService(ILoginTableRepository logintableRepository)
        {
            _logintableRepository = logintableRepository;
        }

        public int[] Login(LoginTable loginTable)
        {
            return _logintableRepository.Login(loginTable);
        }
        public int AddLogin(LoginTable login)
        {
            return _logintableRepository.AddLoginTable(login);
        }
        public LoginTable ForgetPassword(LoginTable login)
        {
            return _logintableRepository.ForgetPassword(login);
        }
        public void UpdatePassword(LoginTable login)
        {
            _logintableRepository.UpdatePassword(login);
        }
    }
}
