using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWPF.DB
{
    public class UserController : IUserController
    {
        public void AddUser(User user)
        {
            using MyContext myContext = new MyContext();
            try
            {
                if (IsLogIn(user.Login) == true)
                {
                    throw new Exception($"Ошибка при добавлении в  ДБ логин {user.Login} уже существует");
                }

                myContext.Add(new User
                {
                    Login = user.Login,
                    Password = user.Password,                    
                });
                myContext.SaveChanges(); // сохранение
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при добавлении в  ДБ логин {user.Login}");
            }
        }

        public User GetUser(string login)
        {
            using MyContext myContext = new MyContext();
            try
            {
                var usDb = myContext.Users.Single(u => u.Login == login);
                return new User
                {
                    Login = usDb.Login,
                    Password = usDb.Password
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при выборки  в  ДБ логин {login}");
            }
        }

        public List<User> GetUsers()
        {
            using MyContext myContext = new MyContext();
            try
            {
                List<User> users = new List<User>();

                foreach (var item in myContext.Users.ToList())
                {
                    users.Add(GetUser(item.Login));
                }
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при выборки  в  ДБ");
            }
        }

        public bool IsLogIn(string login)
        {
            using MyContext myContext = new MyContext();
            try
            {
                return myContext.Users.Any(x => x.Login == login);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при выборки  в  ДБ - логин {login}");
            }
        }
    }
    public interface IUserController
    {
        void AddUser(User user);
        User GetUser(string login);
        List<User> GetUsers();
        bool IsLogIn(string login);
    }
}
