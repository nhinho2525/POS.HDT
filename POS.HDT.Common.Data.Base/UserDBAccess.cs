using POS.HDT.Common.Data.Domain;
using POS.HDT.Common.Data.Tables;
using System.Data;

namespace POS.HDT.Common.Data.Base
{
    public class UserDBAccess : DBAccess
    {
        
        public void Add(Users obj, string username)
        {
            long id = 0;
            string queryStr = "INSERT INTO";
            queryStr += " users(UserId, Pwd, LastLogin, Status, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, RoleId)";
            queryStr += " VALUES(";
            queryStr += string.Format("'{0}'", obj.UserId);
            queryStr += string.Format(", '{0}'", obj.Pwd);
            queryStr += string.Format(", '{0}'", obj.LastLogin);
            queryStr += string.Format(", '{0}'", obj.Status);
            queryStr += string.Format(", '{0}'", obj.CreatedBy);
            queryStr += ", NOW()";
            queryStr += string.Format(", '{0}'", obj.ModifiedBy);
            queryStr += ", NOW()";
            queryStr += string.Format(", '{0})'", obj.RoleId);
            //PosService.DataExecute(Username, Password, queryStr, ref errorString);
            PosService.DataExecuteId(Username, Password, queryStr, ref id, ref errorString);
            //MessageBox.Show(id + "");
        }

        public void Edit(Users obj)
        {
            string queryStr = "UPDATE users SET";
            queryStr += string.Format(" Pwd = '{0}'", obj.Pwd);
            queryStr += string.Format(" ,LastLogin = '{0}'", obj.LastLogin);
            queryStr += string.Format(" ,Status = '{0}'", obj.Status);
            queryStr += string.Format(" ,ModifiedBy = '{0}'", obj.ModifiedBy);
            queryStr += " ,ModifiedBy = NOW()";
            queryStr += string.Format(" ,ModifiedBy = '{0}'", obj.ModifiedBy);
            queryStr += string.Format(", '{0})'", obj.RoleId);
            queryStr += string.Format(" WHERE UserId = '{0}'", obj.UserId);

            PosService.DataExecute(Username, Password, queryStr, ref errorString);
        }

        public bool CheckLogin(string pIdOrUsername, string pPassword)
        {
            bool ok = PosService.IsUser(pIdOrUsername, pPassword, ref errorString);

            if (ok == true)
            {
                Username = pIdOrUsername;
                Password = pPassword;
                users = GetByIdOrUsername(pIdOrUsername);
            }

            return ok;
        }

        public DataTable GetObjectIdByUserId(string userid)
        {
            DataTable res = new DataTable();
            DataSet ds = new DataSet();
            string[][] param ={
                new string[] {"p_UserId",userid},
            };
            PosService.DataStoreProcQuery_Param(Username, Password, "spSelect_ObjectId_By_UserId", ref ds, param, ref errorString);

            if (string.IsNullOrEmpty(errorString))
            {
                res = ds.Tables[0];
            }
            else
            {
                res = null;
            }
            return res;
        }

        public void ChangePassword(Users obj)
        {
            string[][] param ={
                new string[] {"p_UserId", obj.UserId},
                new string[] {"p_Pwd",obj.Pwd},
            };
            PosService.DataStoreProcExecute(Username, Password, "spChangePassword_User", ref errorString, param, ref res);
        }


        public void Delete(string id)
        {
            //
        }
        public void Delete(string id, string stt)
        {
            string queryStr = string.Format("UPDATE users SET Status = '{0}' WHERE  UserId = '{1}'", stt, id);
            PosService.DataExecute(Username, Password, queryStr, ref errorString);
        }

        public Users GetByIdOrUsername(string pIdOrUsername)
        {
            Users user = new Users();
            DataSet dataset = new DataSet();
            string queryStr = string.Format("SELECT * FROM users WHERE  UserId = '{0}'", pIdOrUsername);
            PosService.DataQuery(Username, Password, queryStr, ref dataset, "x", ref errorString);

            if (!string.IsNullOrEmpty(errorString))
                return null;

            if (dataset.Tables["x"].Rows.Count > 0)
            {

                user.LastLogin = dataset.Tables["x"].Rows[0][UserColumn.LastLogin].ToString();
                user.Status = dataset.Tables["x"].Rows[0][UserColumn.Status].ToString();
                user.CreatedBy = dataset.Tables["x"].Rows[0][UserColumn.CreatedBy].ToString();
                user.CreatedDate = dataset.Tables["x"].Rows[0][UserColumn.CreatedDate].ToString();
                user.ModifiedBy = dataset.Tables["x"].Rows[0][UserColumn.ModifiedBy].ToString();
                user.ModifiedDate = dataset.Tables["x"].Rows[0][UserColumn.ModifiedDate].ToString();
                user.RoleId = dataset.Tables["x"].Rows[0][UserColumn.RoleId].ToString();
            }
            else
                user = null;

            return user;
        }
    }
}
