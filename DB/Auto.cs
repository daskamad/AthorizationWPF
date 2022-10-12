using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWPF.DB
{
    public class Auto : IAuto
    {
        private IUserController _userContoroller;

        public Auto(IUserController userContoroller) //проброс зависимости
        {
            _userContoroller = userContoroller;
        }

        public User GetUser(string login, string password)
        {
            return _userContoroller.GetUser(login);
        }

        public bool IsLogIn(string login, string password)
        {
            if (_userContoroller.IsLogIn(login) == true)
            {
                var us = _userContoroller.GetUser(login);
                if (us.Password == password)
                    return true;
                else
                    return false;
            }

            return false;
        }
    }
    public interface IAuto
    {
        bool IsLogIn(string login, string password);
        User GetUser(string login, string password);

    }
    public class MyBuild
    {
        internal static IAuto GetAuto()
        {
            return new Auto(GetUserContoroller()); // проброс  объекта  базы данных
        }

        internal static IUserController GetUserContoroller()
        {
            return new UserController(); //  база данных 
        }
    }

}
