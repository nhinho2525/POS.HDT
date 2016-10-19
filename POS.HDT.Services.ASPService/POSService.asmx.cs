using POS.HDT.Services.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace POS.HDT.Services.ASPService
{
    /// <summary>
    /// Summary description for POSService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class POSService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DataConnectionString"].ConnectionString;

            if (string.IsNullOrEmpty(connectionString))
                connectionString = "server=localhost;User Id=admin;password=123456789;database=pos_hdt;Allow Zero Datetime=True;Convert Zero Datetime=True";

            return connectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pError"></param>
        /// <returns></returns>
        public bool IsAccept(ref string pError)
        {
            //if (Session["ACCEPT"] != null)
            //{
            //    pError = "";
            //    return true;
            //}
            //else
            //{
            //    pError = "Không có quyền truy xuất";
            //    return false;
            //}

            return true;
        }

        /***********************************************************************
         *                  CONNECT & PUBLIC
         ***********************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public bool IsServiceReady()
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public bool IsDataReady()
        {
            return DataAccess.IsDatReady(GetConnectionString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pUsername"></param>
        /// <param name="pPassword"></param>
        /// <param name="pError"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public bool IsUser(string pUsername, string pPassword, ref string pError)
        {
            Session["USERNAME"] = pUsername;
            DataSet dataset = new DataSet();
            //Hung 09-12-2014
            //string strQuery = string.Format("SELECT * FROM users WHERE Status='1' And UserId = '{0}' AND Pwd = '{1}'", pUsername, pPassword);
            string strQuery = string.Format("SELECT * FROM user WHERE UserId = '{0}' AND Pwd = '{1}'", pUsername, pPassword);
            DataAccess.DataQuery(GetConnectionString(), strQuery, ref dataset, "x", ref pError);

            if (dataset.Tables["x"].Rows.Count > 0)
            {
                Session["ACCEPT"] = "OK";
                return true;
            }
            else
            {
                Session["ACCEPT"] = null;
                return false;
            }
        }

        /***********************************************************************
         *                  PRIVATE
         ***********************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pQueryString"></param>
        /// <param name="pDataset"></param>
        /// <param name="pTableName"></param>
        /// <param name="pError"></param>
        [WebMethod(EnableSession = true)]
        public void DataQuery(string pUsernameOrId, string password, string pQueryString, ref DataSet pDataset, string pTableName, ref string pError)
        {
            if (!IsAccept(ref pError))
                return;

            if (!IsUser(pUsernameOrId, password, ref pError))
                return;

            DataAccess.DataQuery(GetConnectionString(), pQueryString, ref pDataset, pTableName, ref pError);
        }

        [WebMethod(EnableSession = true)]
        public void DataStoreProcQuery(string pUsernameOrId, string password, string pQueryCmd, ref DataSet pDataset, ref string pError)
        {
            if (!IsAccept(ref pError))
                return;

            if (!IsUser(pUsernameOrId, password, ref pError))
                return;

            DataAccess.DataStoreProcQuery(GetConnectionString(), pQueryCmd, ref pDataset, ref pError);
        }

        [WebMethod(EnableSession = true)]
        public void DataStoreProcQuery_Param(string pUsernameOrId, string password, string pQueryCmd, ref DataSet pDataset, string[][] param, ref string pError)
        {
            if (!IsAccept(ref pError))
                return;

            if (!IsUser(pUsernameOrId, password, ref pError))
                return;

            DataAccess.DataStoreProcQuery_Param(GetConnectionString(), pQueryCmd, ref pDataset, param, ref pError);
        }

        [WebMethod(EnableSession = true)]
        public void DataStoreProcExecute(string pUsernameOrId, string password, string pQueryCmd, ref string pError, string[][] param, ref bool result)
        {
            if (!IsAccept(ref pError))
                return;

            if (!IsUser(pUsernameOrId, password, ref pError))
                return;

            DataAccess.DataStoreProcExecute(GetConnectionString(), pQueryCmd, ref pError, param, ref result);
        }

        [WebMethod(EnableSession = true)]
        public void DataStoreProceduceAndMultiQuery(string pUsernameOrId, string pPassword,
            string pQueryCmd, ref string pError, string[][] pParam,
            string[] pQueryOrder, ref bool pResult)
        {
            if (!IsAccept(ref pError))
                return;

            if (!IsUser(pUsernameOrId, pPassword, ref pError))
                return;

            DataAccess.DataStoreProceduceAndMultiQuery(GetConnectionString(), pQueryCmd, ref pError, pParam, pQueryOrder, ref pResult);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pQueryString"></param>
        /// <param name="pError"></param>
        [WebMethod(EnableSession = true)]
        public void DataExecute(string pUsernameOrId, string password, string pQueryString, ref string pError)
        {
            if (!IsAccept(ref pError))
                return;

            if (!IsUser(pUsernameOrId, password, ref pError))
                return;

            DataAccess.DataExecute(GetConnectionString(), pQueryString, ref pError);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pUsernameOrId"></param>
        /// <param name="password"></param>
        /// <param name="pQueryString"></param>
        /// <param name="pId"></param>
        /// <param name="pError"></param>
        [WebMethod(EnableSession = true)]
        public void DataExecuteId(string pUsernameOrId, string password, string pQueryString, ref Int64 pId, ref string pError)
        {
            if (!IsAccept(ref pError))
                return;

            if (!IsUser(pUsernameOrId, password, ref pError))
                return;

            DataAccess.DataExecuteId(GetConnectionString(), pQueryString, ref pId, ref pError);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pUsernameOrId"></param>
        /// <param name="password"></param>
        /// <param name="pQueryArrString"></param>
        /// <param name="pError"></param>
        [WebMethod(EnableSession = true)]
        public void DataMultiExecute(string pUsernameOrId, string password, string[] pQueryArrString, ref string pError)
        {
            if (!IsAccept(ref pError))
                return;

            if (!IsUser(pUsernameOrId, password, ref pError))
                return;

            DataAccess.DataMultiExecute(GetConnectionString(), pQueryArrString, ref pError);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pStringQuery"></param>
        /// <param name="pObject"></param>
        /// <param name="pError"></param>
        [WebMethod(EnableSession = true)]
        public void DataScalar(string pUsernameOrId, string password, string pStringQuery, ref object pObject, ref string pError)
        {
            if (!IsAccept(ref pError))
                return;

            if (!IsUser(pUsernameOrId, password, ref pError))
                return;

            DataAccess.DataScalar(GetConnectionString(), pStringQuery, ref pObject, ref pError);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pDataset"></param>
        /// <param name="pTableName"></param>
        /// <param name="pToTable"></param>
        /// <param name="pError"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public bool DataCommandBuilderUpdate(string pUsernameOrId, string password, DataSet pDataset, string pTableName, string pToTable, ref string pError)
        {
            if (!IsAccept(ref pError))
                return false;

            if (!IsUser(pUsernameOrId, password, ref pError))
                return false;

            return DataAccess.DataCommandBuilder(GetConnectionString(), pDataset, pTableName, pToTable, ref pError);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pImageBuffer"></param>
        /// <param name="pId"></param>
        /// <param name="pColumnIdName"></param>
        /// <param name="pColumnImageName"></param>
        /// <param name="pTableName"></param>
        /// <param name="pError"></param>
        [WebMethod(EnableSession = true)]
        public void DataSaveImage(string pUsernameOrId, string password, byte[] pImageBuffer, string pId, string pColumnIdName, string pColumnImageName, string pTableName, ref string pError)
        {
            if (!IsAccept(ref pError))
                return;

            if (!IsUser(pUsernameOrId, password, ref pError))
                return;

            DataAccess.DataSaveImage(GetConnectionString(), pImageBuffer, pId, pColumnIdName, pColumnImageName, pTableName, ref pError);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="pColumnIdName"></param>
        /// <param name="pColumnImageName"></param>
        /// <param name="pTableName"></param>
        /// <param name="pError"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public byte[] DataReadImage(string pUsernameOrId, string password, string pId, string pColumnIdName, string pColumnImageName, string pTableName, ref string pError)
        {
            if (!IsAccept(ref pError))
                return null;

            if (!IsUser(pUsernameOrId, password, ref pError))
                return null;

            return DataAccess.DataReadImage(GetConnectionString(), pId, pColumnIdName, pColumnImageName, pTableName, ref pError);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pQueryString"></param>
        /// <param name="pError"></param>
        [WebMethod(EnableSession = true)]
        public void DataExecuteForProduct(string pUsernameOrId, string password, string Name, string Name2, string Name3, string Unit, decimal UnitPrice, int IsNode, int IsPrint, string Notes, int ParentId, string CreateDate, string UpdateDate, byte[] Image, ref string pError)
        {
            if (!IsAccept(ref pError))
                return;

            if (!IsUser(pUsernameOrId, password, ref pError))
                return;

            DataAccess.DataExecuteForProduct(GetConnectionString(), Name, Name2, Name3, Unit, UnitPrice, IsNode, IsPrint, Notes, ParentId, CreateDate, UpdateDate, Image, ref pError);
        }

        [WebMethod(EnableSession = true)]
        public void DataExecuteEdit_ForProduct(string pUsernameOrId, string password, string Name, string Name2, string Name3, string Unit, decimal UnitPrice, int IsNode, int IsPrint, string Notes, int ParentId, string UpdateDate, byte[] Image, string ProductId, ref string pError)
        {
            if (!IsAccept(ref pError))
                return;

            if (!IsUser(pUsernameOrId, password, ref pError))
                return;

            DataAccess.DataExecuteEdit_ForProduct(GetConnectionString(), Name, Name2, Name3, Unit, UnitPrice, IsNode, IsPrint, Notes, ParentId, UpdateDate, Image, ProductId, ref pError);
        }

        [WebMethod(EnableSession = true)]
        public void Tran_Insert_for_Order(string pUsernameOrId, string password, string Lang, string pPrinterName, string[][] parram, List<string[][]> listParam,
            string[][] paramDesk, ref string pError, ref bool res, ref bool IsPrint, ref string pOrderId)
        {
            if (!IsAccept(ref pError))
                return;

            if (!IsUser(pUsernameOrId, password, ref pError))
                return;



            DataAccess.Tran_Insert_Order(GetConnectionString(), ref pError, Lang, pPrinterName, parram, listParam, paramDesk, ref res, ref IsPrint, ref pOrderId);
        }

        [WebMethod(EnableSession = true)]
        public void Tran_Insert_Receipt(string pUsernameOrId, string password, ref string pError,
            string[][] Receipt,
            List<string[][]> listReceiptDetail,
            List<string[][]> listReceiptCard,
            string[][] ReceiptMember,
            string[][] ReceiptInfo, ref bool res, ref string ReceiptID)
        {
            if (!IsAccept(ref pError))
                return;

            if (!IsUser(pUsernameOrId, password, ref pError))
                return;


            DataAccess.Tran_Insert_Receipt(GetConnectionString(), ref pError,
                Receipt, listReceiptDetail, listReceiptCard, ReceiptMember, ReceiptInfo, ref res, ref ReceiptID, pUsernameOrId);
        }

        [WebMethod(EnableSession = true)]
        public void SaveImage_IntoObject(byte[] pImageBuffer, string ObjectId, string OldImgName, ref string pError)
        {
            DataAccess.SaveImage_IntoObject(GetConnectionString(), pImageBuffer, ObjectId, OldImgName, ref pError);
        }

        [WebMethod(EnableSession = true)]
        public void SaveImage_IntoEmployee(byte[] pImageBuffer, string EmployeeId, ref string pError)
        {
            DataAccess.SaveImage_IntoEmployee(GetConnectionString(), pImageBuffer, EmployeeId, ref pError);
        }
        [WebMethod(EnableSession = true)]
        public void SaveImage_IntoProduct(byte[] pImageBuffer, string EmployeeId, string OldImgName, ref string pError)
        {
            DataAccess.SaveImage_IntoProduct(GetConnectionString(), pImageBuffer, EmployeeId, OldImgName, ref pError);
        }

        #region IN Orders
        [WebMethod(EnableSession = true)]
        public void Print_Order(string OrderId, string Lang, string PlaceOfPrinting, string StatusPrint, ref string pError)
        {

            DataAccess.Print_Order(GetConnectionString(), OrderId, Lang, PlaceOfPrinting, StatusPrint, ref pError);
        }
        #endregion

        [WebMethod(EnableSession = true)]
        public void Update_DeskId_For_Order(string pUsernameOrId, string password, string[] prm, string pDeskIdTo, string pDeskIdFrom, ref string pError, ref bool res)
        {
            if (!IsAccept(ref pError))
                return;

            if (!IsUser(pUsernameOrId, password, ref pError))
                return;

            DataAccess.Update_DeskId_For_Order(GetConnectionString(), prm, pDeskIdTo, pDeskIdFrom, ref pError, ref res);
        }

        [WebMethod(EnableSession = true)]
        public void Update_Order_Detail(string pUsernameOrId, string password, string pLang, string pPrinterName, string pOrderId, string[][] pPrm_O,
            List<string[][]> pLst_ODNew, bool IsPrint, bool pHaveDiscount, ref string pError, ref bool res)
        {
            if (!IsAccept(ref pError))
                return;

            if (!IsUser(pUsernameOrId, password, ref pError))
                return;

            DataAccess.Update_Order_Detail(GetConnectionString(), pUsernameOrId, pLang, pPrinterName, pOrderId, pPrm_O, pLst_ODNew, IsPrint, pHaveDiscount, ref pError, ref res);
        }

        [WebMethod(EnableSession = true)]
        public string GetValueScoreEx()
        {
            return DataAccess.GetValueScoreEx();
        }

        [WebMethod(EnableSession = true)]
        public string GetPriceScore()
        {
            return DataAccess.GetPriceScore();
        }

        [WebMethod(EnableSession = true)]
        public void Write_Score_Value(string pUsernameOrId, string password, string pScoreValue, string pPriceScore, ref string pError)
        {
            if (!IsAccept(ref pError))
                return;

            if (!IsUser(pUsernameOrId, password, ref pError))
                return;

            DataAccess.Write_Score_Value(GetConnectionString(), pUsernameOrId, pScoreValue, pPriceScore, ref pError);
        }
    }
}
