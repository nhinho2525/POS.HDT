using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.HDT.Store.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //public static DataTableCollection LangRes = VVPosS.Common.clsLanguages.ReadExcelFromURL(@"../../Res/LangResource.xls");
        public static DataTableCollection LangRes = Common.Core.Logic.Classes.Languages.ReadExcelFromURL(@"LangResource.xls");
        public static string urlImage = "";
        public static string Username = "";
        public static string Password = "";
        public static string RoleId = "";
        public static string ImageUser = "";
        public static string FullName = "";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
