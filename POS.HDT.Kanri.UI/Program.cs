using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.HDT.Common.Data.Domain;

namespace POS.HDT.Kanri.UI
{
    static class Program
    {
        public static Users curUser= new Users();
        public static string username = "";
        public static string password = "";

        public static string ConvertStringToMD5(string pString)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(pString);

            Byte[] hashedBytes = new MD5CryptoServiceProvider().ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
        }
    }
}
