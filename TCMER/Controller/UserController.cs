using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCMER.Model;
using TCMER.Dao;

namespace TCMER.Controller
{
    public class UserController
    {
        private UserMapper um;
        public UserController()
        {
            um = new UserMapper();
        }

        [Obsolete]
        public UserModel GetUserInfo(string userID)
        {
            return um.GetUserInfo(userID);
        }

        [Obsolete]
        public UserModel Login(string userName, string password)
        {
            UserModel user = um.ValidateUser(userName, password);

            return user;
        }

        [Obsolete]
        public int Register(UserModel umInfo)
        {
            return um.AddUser(umInfo);
        }
    }
}
