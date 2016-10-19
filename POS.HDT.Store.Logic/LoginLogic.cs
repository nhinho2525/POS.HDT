using POS.HDT.Common.Data.Base;

namespace POS.HDT.Store.Logic
{
    public class LoginLogic
    {
        public UserDBAccess userDBAccess = new UserDBAccess();

        public bool OnLogin(string username, string pwd)
        {
            string password = Common.Core.Logic.Utils.Utilities.ConvertStringToMD5(pwd);
            bool ok = userDBAccess.CheckLogin(username, password);

            return ok;
        }

        public void Dispose()
        {
            userDBAccess = null;
        }
    }
}
