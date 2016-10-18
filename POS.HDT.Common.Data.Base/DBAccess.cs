using POS.HDT.Common.Data.Domain;

namespace POS.HDT.Common.Data.Base
{
    public class DBAccess
    {
        public Users users { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }
     
        public string errorString = "";

        public string ErrorString
        {
            get { return errorString; }
            set { errorString = value; }
        }

        public bool res = false;

        public bool Res
        {
            get { return res; }
            set { res = value; }
        }

        //Inject service
        public POSService.POSService PosService = new POSService.POSService();
    }
}
