using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.DAO;
using GameServer.Model;
using GameServer.Servers;

namespace GameServer.Controller
{
    class UserController:BaseController
    {
        private UserDAO userDAO = new UserDAO();
        public UserController()
        {
            requestCode = RequestCode.User;
        }

        public string Login(string data, Client
            client, Server serve)
        {
            string[] strs = data.Split(',');
           User user =  userDAO.VerifyUser(client.MySQLConn, strs[0], strs[1]);
            if (user == null)
            {
                //Enum.GetName(typeof(RequestCode), ReturnCode.Fail);
                return ((int) ReturnCode.Fail).ToString();
            }
            else
            {
                return ((int) ReturnCode.Success).ToString();
            }
        }

        public string Register(string data, Client
            client, Server serve)
        {
            string[] strs = data.Split(',');
            string username = strs[0];
            string password = strs[1];
            bool res = userDAO.GetUserByUsername(client.MySQLConn, username);
            if (res)
            {
                return ((int)ReturnCode.Fail).ToString();
            }
            userDAO.AddUser(client.MySQLConn,username,password);
            return ((int)ReturnCode.Success).ToString();
            return null;
        }
    }
}
