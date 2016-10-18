using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.HDT.Common.Data.Domain;
using POS.HDT.Common.Data.Tables;
using System.Data;

namespace POS.HDT.Common.Data.Base
{
    public class RoleDBAccess : DBAccess
    {
        /// <summary>
        /// Edit name
        /// </summary>
        /// <param name="obj"></param>
        public void Edit(Roles obj)
        {
            string queryStr = "UPDATE role SET";
            queryStr += string.Format(" " + RoleColumn.RoleName + " = '{0}'", obj.RoleName);
            queryStr += string.Format(" WHERE " + RoleColumn.RoleId + " = '{0}'", obj.RoleId);

            PosService.DataExecute(Username, Password, queryStr, ref errorString);
        }

        public DataTable GetRoles()
        {
            DataTable res = new DataTable();
            DataSet ds = new DataSet();
            string queryStr = "UPDATE role SET";
            PosService.DataQuery(Username, Password, queryStr, ref ds, "roles", ref errorString);
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

        public DataTable GetRoleOfUser(string roleId)
        {
            string queryStr = String.Format("SELECT * FROM role WHERE " + RoleColumn.RoleId + " = '{0}'", roleId);
            DataSet dataset = new DataSet();
            PosService.DataQuery(Username, Password, queryStr, ref dataset, "x", ref errorString);

            if (string.IsNullOrEmpty(errorString))
                return dataset.Tables["x"];
            else
                return null;
        }
    }
}
