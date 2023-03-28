using DeskData.Data;
using DeskEntity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeskData.Repository
{
    public class LoginTableRepository:ILoginTableRepository
    {
        DeskDbContext _db;
        public LoginTableRepository(DeskDbContext db)
        {
            _db = db;
        }

        public int AddLoginTable(LoginTable loginTable)
        {
            #region Adding LoginTable
            _db.LoginTables.Add(loginTable);
            _db.SaveChanges();
            List<LoginTable> list = new List<LoginTable>();
            list = _db.LoginTables.ToList();
            var loginTable1 = (from list1 in list
                               select list1).Last();
            return loginTable1.ID;
            #endregion
        }
        public int[] Login(LoginTable loginTable)
        {
            #region Admin and Employee login
            int[] arr = { -1, 4 };
            List<LoginTable> LoginLists = _db.LoginTables.ToList();
            var TotalList = from v in LoginLists select v;
            if (loginTable.Email == "Desk@gmail.com" && loginTable.Password == "desk@123")
            {
                arr[1] = 3;
                return arr;
            }
            else
            {
                foreach (var i in TotalList)
                {
                    if (i.Email == loginTable.Email && i.Password == loginTable.Password)
                    {
                        arr[0] = i.ID;
                        arr[1] = i.Type;
                    }
                }
            }
            return arr;
            #endregion
        }
        public LoginTable ForgetPassword(LoginTable loginTable)
        {
            #region Forget Password
            List<LoginTable> LoginLists = _db.LoginTables.ToList();
            var TotalList = from v in LoginLists select v;
            LoginTable loginTable1 = new LoginTable();
            foreach (var i in TotalList)
            {
                if (i.Email == loginTable.Email)
                {
                    return i;
                }
            }
            return loginTable1;
            #endregion
        }
        public void UpdatePassword(LoginTable login)
        {
            #region Updating Employee Details in Database
            _db.Entry(login).State = EntityState.Modified;
            _db.SaveChanges();
            #endregion
        }
    }
}
