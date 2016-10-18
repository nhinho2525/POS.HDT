using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.HDT.Common.Data.Base;
using POS.HDT.Common.Data.Domain;
using System.Data;

namespace POS.HDT.Kanri.Logic
{
    public class UserLogic
    {
        private UserDBAccess userDBAccess;
        public string errorString;
        public UserLogic()
        {
            userDBAccess = new UserDBAccess();
            errorString = "";
        }

        public DataTable LoadUsers()
        {
            return userDBAccess.GetUsers();
        }
    }
}
