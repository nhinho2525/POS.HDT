using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.HDT.Common.Data.Base;
using System.Data;

namespace POS.HDT.Kanri.Logic
{
    public class RoleLogic
    {
        private RoleDBAccess roleDBAccess;

        public RoleLogic()
        {
            roleDBAccess = new RoleDBAccess();
        }

       
    }
}
