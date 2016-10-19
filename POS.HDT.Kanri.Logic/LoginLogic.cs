using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.HDT.Common.Data.Base;
using POS.HDT.Common.Data.Domain;

namespace POS.HDT.Kanri.Logic
{
    public class LoginLogic
    {
        private UserDBAccess userDBAccess;
        public Users user;
        public string Username { get; set; }
        public string Password
        {
            get; set;
        }
        public string ErrorString { get; set; }
        public LoginLogic()
        {
            userDBAccess = new UserDBAccess();
            user = new Users();
            Username = "";
            Password = "";
            ErrorString = "";
        }
        //TODO: Code Logic here
        // Commit thu
        /// <summary>
        /// Check Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckLogin(string username, string password)
        {
            bool result = userDBAccess.CheckLogin(username, password);
            user = userDBAccess.users;
            Username = userDBAccess.Username;
            Password = userDBAccess.Password;
            ErrorString = userDBAccess.errorString;
            return result;
        }
    }
}
