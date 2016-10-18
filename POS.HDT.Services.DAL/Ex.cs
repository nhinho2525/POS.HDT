using MySql.Data.MySqlClient;
using POS.HDT.Common.Core.Logic.Classes;
using POS.HDT.Common.Data.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Xml;

namespace POS.HDT.Services.DAL
{
    /// <summary>
    /// Temp place here
    /// </summary>
    public static partial class DataAccess
    {

        private static string Language;
        private static string ReId;
        private static string pConnectionStr;

        #region In Order
        public static void Print_Order(string cnStr, string OrderId, string Lang, string PlaceOfPrinting, string StatusPrint, ref string pError)
        {
            pError = "";
            pConnectionStr = cnStr;
            bool print = DoPrintingOrder(OrderId, Lang, PlaceOfPrinting, StatusPrint, ref pError);
        }
        //private static string Language;
        private static string OrId;
        public static bool DoPrintingOrder(string OrderId, string Lang, string PlaceOfPrinting, string StatusPrint, ref string Err)
        {
            //PlaceOfPrinting : 1,2,3 
            //PlaceOfPrinting : null , ""  có nghĩa là chọn máy in mặc định
            Err = "";
            try
            {
                Language = Lang;
                OrId = OrderId;
                PrintDocument pd = new PrintDocument();
                string printerName = PlaceOfPrinting;//ConfigurationManager.AppSettings["Printer0" + PlaceOfPrinting].ToString();
                if (!string.IsNullOrEmpty(PlaceOfPrinting))
                {
                    pd.PrinterSettings.PrinterName = printerName;
                }
                //pd.PrinterSettings.PrinterName = @"CutePDF Writer";
                pd.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A7", 300, 10000);
                if (StatusPrint == "0")
                    pd.PrintPage += new PrintPageEventHandler(printDocumentOrderKitchen_PrintPage);
                else if (StatusPrint == "3")
                    pd.PrintPage += new PrintPageEventHandler(printDocumentOrderKitchen_Update_PrintPage);
                else if (StatusPrint == "2")
                    pd.PrintPage += new PrintPageEventHandler(printDocumentOrderKitchen_Cancel_PrintPage);

                pd.Print();


                return true;
            }
            catch (Exception ex)
            {
                Err = ex.Message.ToString();
                return false;
            }
        }

        private static void printDocumentOrderKitchen_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // load data hoa don
            OrderAll rcAll = new OrderAll();
            rcAll = GetOrderInfo(OrId);
            int y_p = 0;

            string[][] list = new string[rcAll.lst_Detail.Count][];
            int j = 0;
            foreach (OrderDetails dtl in rcAll.lst_Detail)
            {
                string[] arr = new string[4];
                arr[0] = j.ToString();
                //arr[1] = dtl.ProductId + " - " + dtl.ProductName;
                arr[1] = dtl.ProductName;

                //TODO: TEMP Comment
                //arr[2] = dtl.Qty;
                //arr[3] = dtl.Note;//(dtl.Notes == null) ? "" : dtl.Notes;
                list[j] = arr;
                j++;
            }
            string data;
            StringFormat strfmt_Left = new StringFormat();
            StringFormat strfmt_Center = new StringFormat();
            StringFormat strfmt_Right = new StringFormat();

            strfmt_Left.LineAlignment = StringAlignment.Near;
            strfmt_Left.Alignment = StringAlignment.Near;

            strfmt_Center.LineAlignment = StringAlignment.Center;
            strfmt_Center.Alignment = StringAlignment.Center;

            strfmt_Right.LineAlignment = StringAlignment.Far;
            strfmt_Right.Alignment = StringAlignment.Far;

            y_p += 20;
            Rectangle strDesk = new Rectangle(5, y_p, 250, 22);
            data = Languages.GetResource("Desk_", Language) + " " + rcAll.order.TableId;
            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strDesk, strfmt_Left);

            Rectangle strDate = new Rectangle(5, y_p + 22, 250, 22);
            data = Languages.GetResource("ReceiptDate", Language) + " " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strDate, strfmt_Left);

            Rectangle strReceiptId1 = new Rectangle(180, y_p, 100, 30);
            data = Int32.Parse(rcAll.order.OrderId.Substring(10)).ToString();
            e.Graphics.DrawString(data, new Font("Arial", 16, FontStyle.Bold), Brushes.Black, strReceiptId1, strfmt_Right);

            y_p += 45;
            Rectangle strReceiptId = new Rectangle(5, y_p, 250, 22);
            data = Languages.GetResource("OrderIDNo", Language) + " : 10#" + rcAll.order.OrderId.Substring(10);
            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strReceiptId, strfmt_Left);

            y_p += 23;
            Rectangle strHD_STT = new Rectangle(5, y_p, 63, 22); // đổi , 30-28
            data = Languages.GetResource("No", Language);
            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strHD_STT, strfmt_Left);

            Rectangle strHD_SP = new Rectangle(35, y_p, 220, 22);
            data = Languages.GetResource("Product", Language);//"Sản phẩm ";  
            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strHD_SP, strfmt_Left);

            Rectangle strHD_SL = new Rectangle(260, y_p, 45, 22);
            data = Languages.GetResource("ReceiptProQty", Language);//"SL";
            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strHD_SL, strfmt_Left);
            y_p += 30;
            for (int i = 0; i < list.Length; i++)
            {
                //Headers
                Rectangle strCT_STT = new Rectangle(5, y_p, 30, 22);
                data = (i + 1).ToString();
                e.Graphics.DrawString(data, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, strCT_STT, strfmt_Left);

                data = list[i][1];
                int heightl = Int32.Parse(Math.Ceiling((double)data.Length / 17).ToString()) * 18 + 5;
                Rectangle strCT_SP = new Rectangle(35, y_p, 220, heightl);
                e.Graphics.DrawString(data, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, strCT_SP, strfmt_Left);

                Rectangle strCT_SL = new Rectangle(260, y_p, 45, 22);

                data = list[i][2];
                e.Graphics.DrawString(data, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, strCT_SL, strfmt_Left);


                data = list[i][3];
                int height = 0;
                if (!string.IsNullOrEmpty(data))
                {
                    height = Int32.Parse(Math.Ceiling((double)data.Length / 17).ToString()) * 18 + 4;
                    Rectangle notes = new Rectangle(5, y_p + heightl, 280, height);
                    e.Graphics.DrawString(data, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, notes, strfmt_Left);
                }

                Rectangle RecLineCT = new Rectangle(5, y_p + heightl + height, 280, 22);
                data = "-----------------------------------------------------------------------";
                e.Graphics.DrawString(data, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, RecLineCT, strfmt_Left);


                y_p = y_p + height + heightl + 20;
            }
        }
        private static void printDocumentOrderKitchen_Update_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // load data hoa don
            OrderAll rcAll = new OrderAll();
            rcAll = GetOrderInfo(OrId);
            int y_p = 0;

            string[][] list = new string[rcAll.lst_Detail.Count][];
            int j = 0;
            foreach (OrderDetails dtl in rcAll.lst_Detail)
            {
                string[] arr = new string[5];
                arr[0] = j.ToString();
                //arr[1] = dtl.ProductId + " - " + dtl.ProductName;
                arr[1] = dtl.ProductName;

                //TODO: TEMP Comment
                //arr[2] = dtl.Qty;
                //arr[3] = dtl.Note;//(dtl.Notes == null) ? "" : dtl.Notes;
                //arr[4] = dtl.Status;
                list[j] = arr;
                j++;
            }
            string data;
            StringFormat strfmt_Left = new StringFormat();
            StringFormat strfmt_Center = new StringFormat();
            StringFormat strfmt_Right = new StringFormat();

            strfmt_Left.LineAlignment = StringAlignment.Near;
            strfmt_Left.Alignment = StringAlignment.Near;

            strfmt_Center.LineAlignment = StringAlignment.Center;
            strfmt_Center.Alignment = StringAlignment.Center;

            strfmt_Right.LineAlignment = StringAlignment.Far;
            strfmt_Right.Alignment = StringAlignment.Far;


            y_p += 20;
            Rectangle strDesk = new Rectangle(5, y_p, 250, 22);
            data = Languages.GetResource("Desk_", Language) + " " + rcAll.order.TableId;
            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strDesk, strfmt_Left);

            Rectangle strDate = new Rectangle(5, y_p + 22, 250, 22);
            data = Languages.GetResource("ReceiptDate", Language) + " " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strDate, strfmt_Left);

            Rectangle strReceiptId1 = new Rectangle(180, y_p, 100, 30);
            data = Int32.Parse(rcAll.order.OrderId.Substring(10)).ToString();
            e.Graphics.DrawString(data, new Font("Arial", 16, FontStyle.Bold), Brushes.Black, strReceiptId1, strfmt_Right);

            y_p += 45;
            Rectangle strReceiptId = new Rectangle(5, y_p, 250, 22);
            data = Languages.GetResource("UpdateOrder_1", Language) + " : 10#" + rcAll.order.OrderId.Substring(10);
            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strReceiptId, strfmt_Left);

            y_p += 23;
            Rectangle strHD_STT = new Rectangle(5, y_p, 63, 22); // đổi , 30-28
            data = Languages.GetResource("No", Language);
            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strHD_STT, strfmt_Left);

            Rectangle strHD_SP = new Rectangle(35, y_p, 220, 22);
            data = Languages.GetResource("Product", Language);//"Sản phẩm ";  
            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strHD_SP, strfmt_Left);

            Rectangle strHD_SL = new Rectangle(260, y_p, 45, 22);
            data = Languages.GetResource("ReceiptProQty", Language);//"SL";
            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strHD_SL, strfmt_Left);
            y_p += 30;
            FontStyle ft = new FontStyle();
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i][4] == "0")
                {
                    ft = FontStyle.Italic;
                }
                else if (list[i][4] == "2") ft = FontStyle.Strikeout;
                //Headers

                Rectangle strCT_STT = new Rectangle(5, y_p, 30, 22);
                data = (i + 1).ToString();
                e.Graphics.DrawString(data, new Font("Arial", 14, ft), Brushes.Black, strCT_STT, strfmt_Left);

                data = list[i][1];
                int heightl = Int32.Parse(Math.Ceiling((double)data.Length / 17).ToString()) * 18 + 5;
                Rectangle strCT_SP = new Rectangle(35, y_p, 220, heightl);
                e.Graphics.DrawString(data, new Font("Arial", 14, ft), Brushes.Black, strCT_SP, strfmt_Left);

                Rectangle strCT_SL = new Rectangle(260, y_p, 45, 22);
                data = list[i][2];
                e.Graphics.DrawString(data, new Font("Arial", 14, ft), Brushes.Black, strCT_SL, strfmt_Left);


                data = list[i][3];
                int height = 0;
                if (!string.IsNullOrEmpty(data))
                {
                    height = Int32.Parse(Math.Ceiling((double)data.Length / 17).ToString()) * 18 + 4;
                    Rectangle notes = new Rectangle(5, y_p + heightl, 280, height);
                    e.Graphics.DrawString(data, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, notes, strfmt_Left);
                }

                Rectangle RecLineCT = new Rectangle(5, y_p + heightl + height, 280, 22);
                data = "-----------------------------------------------------------------------";
                e.Graphics.DrawString(data, new Font("Arial", 14, ft), Brushes.Black, RecLineCT, strfmt_Left);


                y_p = y_p + height + heightl + 20;
            }
        }
        private static void printDocumentOrderKitchen_Cancel_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // load data hoa don
            OrderAll rcAll = new OrderAll();
            rcAll = GetOrderInfo(OrId);
            int y_p = 0;

            string[][] list = new string[rcAll.lst_Detail.Count][];
            int j = 0;
            foreach (OrderDetails dtl in rcAll.lst_Detail)
            {
                string[] arr = new string[5];
                arr[0] = j.ToString();
                //arr[1] = dtl.ProductId + " - " + dtl.ProductName;
                arr[1] = dtl.ProductName;

                //TODO: TEMP Comment
                //arr[2] = dtl.Qty;
                //arr[3] = dtl.Note;//(dtl.Notes == null) ? "" : dtl.Notes;
                //arr[4] = dtl.Status;
                list[j] = arr;
                j++;
            }
            string data;
            StringFormat strfmt_Left = new StringFormat();
            StringFormat strfmt_Center = new StringFormat();
            StringFormat strfmt_Right = new StringFormat();

            strfmt_Left.LineAlignment = StringAlignment.Near;
            strfmt_Left.Alignment = StringAlignment.Near;

            strfmt_Center.LineAlignment = StringAlignment.Center;
            strfmt_Center.Alignment = StringAlignment.Center;

            strfmt_Right.LineAlignment = StringAlignment.Far;
            strfmt_Right.Alignment = StringAlignment.Far;

            y_p += 20;
            Rectangle strDesk = new Rectangle(5, y_p, 250, 22);
            data = Languages.GetResource("Desk_", Language) + " " + rcAll.order.TableId;
            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strDesk, strfmt_Left);

            Rectangle strDate = new Rectangle(5, y_p + 22, 250, 22);
            data = Languages.GetResource("ReceiptDate", Language) + " " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strDate, strfmt_Left);

            Rectangle strReceiptId1 = new Rectangle(180, y_p, 100, 30);
            data = Int32.Parse(rcAll.order.OrderId.Substring(10)).ToString();
            e.Graphics.DrawString(data, new Font("Arial", 16, FontStyle.Bold), Brushes.Black, strReceiptId1, strfmt_Right);

            y_p += 45;
            Rectangle strReceiptId = new Rectangle(5, y_p, 250, 22);
            data = Languages.GetResource("CancelOrder", Language) + " : 10#" + rcAll.order.OrderId.Substring(10);
            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strReceiptId, strfmt_Left);

            y_p += 23;
            Rectangle strHD_STT = new Rectangle(5, y_p, 63, 22); // đổi , 30-28
            data = Languages.GetResource("No", Language);
            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strHD_STT, strfmt_Left);

            Rectangle strHD_SP = new Rectangle(35, y_p, 220, 22);
            data = Languages.GetResource("Product", Language);//"Sản phẩm ";  
            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strHD_SP, strfmt_Left);

            Rectangle strHD_SL = new Rectangle(260, y_p, 45, 22);
            data = Languages.GetResource("ReceiptProQty", Language);//"SL";
            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strHD_SL, strfmt_Left);
            y_p += 30;
            FontStyle ft = FontStyle.Strikeout;
            for (int i = 0; i < list.Length; i++)
            {
                //Headers
                Rectangle strCT_STT = new Rectangle(5, y_p, 30, 22);
                data = (i + 1).ToString();
                e.Graphics.DrawString(data, new Font("Arial", 14, ft), Brushes.Black, strCT_STT, strfmt_Left);

                data = list[i][1];
                int heightl = Int32.Parse(Math.Ceiling((double)data.Length / 17).ToString()) * 18 + 5;
                Rectangle strCT_SP = new Rectangle(35, y_p, 220, heightl);
                e.Graphics.DrawString(data, new Font("Arial", 14, ft), Brushes.Black, strCT_SP, strfmt_Left);

                Rectangle strCT_SL = new Rectangle(260, y_p, 45, 22);

                data = list[i][2];
                e.Graphics.DrawString(data, new Font("Arial", 14, ft), Brushes.Black, strCT_SL, strfmt_Left);


                data = list[i][3];
                int height = 0;
                if (!string.IsNullOrEmpty(data))
                {
                    height = Int32.Parse(Math.Ceiling((double)data.Length / 17).ToString()) * 18 + 4;
                    Rectangle notes = new Rectangle(5, y_p + heightl, 280, height);
                    e.Graphics.DrawString(data, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, notes, strfmt_Left);
                }

                Rectangle RecLineCT = new Rectangle(5, y_p + heightl + height, 280, 22);
                data = "-----------------------------------------------------------------------";
                e.Graphics.DrawString(data, new Font("Arial", 14, ft), Brushes.Black, RecLineCT, strfmt_Left);


                y_p = y_p + height + heightl + 20;
            }
        }

        #region Lấy tất cả thông tin của 1 HD
        public static OrderAll GetOrderInfo(string OrderID)
        {
            OrderAll res = new OrderAll();

            DataSet ds = new DataSet();
            string[][] param ={
                new string[] {"p_OrderId", OrderID},
            };
            string errorString = "";
            string cnStr = pConnectionStr;
            DataStoreProcQuery_Param(cnStr, "spSelect_Order_AllInfo", ref ds, param, ref errorString);

            if (string.IsNullOrEmpty(errorString) && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.Rows[0];
                    Orders rc = new Orders();
                    rc.OrderId = dr["OrderId"].ToString();
                    rc.CreatedBy = dr["CreatedBy"].ToString();
                    rc.CreatedDate = dr["CreatedDate"].ToString();
                    rc.OrderTax = double.Parse(dr["OrderTax"].ToString()).ToString("0,0");
                    rc.OrderMoney = double.Parse(dr["OrderMoney"].ToString()).ToString("0,0");
                    rc.OrderDiscount = double.Parse(dr["OrderDiscount"].ToString()).ToString("0,0");

                    rc.TableId = dr["TableId"].ToString();

                    res.order = rc;
                }
                /////
                if (ds.Tables[1].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[1];
                    List<OrderDetails> lst = new List<OrderDetails>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        OrderDetails rc = new OrderDetails();
                        rc.OrderId = dr["OrderId"].ToString();
                        rc.ProductId = dr["ProductId"].ToString();
                        rc.ProductName = GetProductName(dr["ProductId"].ToString());
                        //TODO: TEMP Comment
                        //rc.Qty = double.Parse(dr["Qty"].ToString()).ToString("0,0");
                        //rc.Price = double.Parse(dr["Price"].ToString()).ToString("0,0");
                        //rc.AmmountBeforeTax = double.Parse(dr["AmmountBeforeTax"].ToString()).ToString("0,0");
                        //rc.TaxAmmount = double.Parse(dr["TaxAmmount"].ToString()).ToString("0,0");
                        //rc.TotalAmount = double.Parse(dr["TotalAmmount"].ToString()).ToString("0,0");
                        //rc.Status = dr["Status"].ToString();
                        //rc.Note = dr["Note"].ToString();

                        lst.Add(rc);
                    }
                    res.lst_Detail = lst;
                }



            }
            else
            {
                res = null;
            }

            return res;
        }

        #endregion

        #endregion

        public static string GetProductName(string ProID)
        {
            string res = "";
            DataTable dt = new DataTable();
            string query = @"SELECT *
                            FROM `products`
                            WHERE  `ProductId` ='" + ProID + "'";
            DataSet dataset = new DataSet();
            string errorString = "";
            DataQuery(pConnectionStr, query, ref dataset, "Products", ref errorString);

            if (string.IsNullOrEmpty(errorString) && dataset.Tables["Products"].Rows.Count > 0)
            {
                dt = dataset.Tables["Products"];
                res = dt.Rows[0]["name"].ToString();
            }
            else
            {
                res = null;
            }
            return res;
        }

        public static string GetDeskNO(string DeskId)
        {
            string res = "";
            DataTable dt = new DataTable();
            string query = @"SELECT *
                            FROM `desk`
                            WHERE  `DeskId` ='" + DeskId + "'";
            DataSet dataset = new DataSet();
            string errorString = "";
            DataQuery(pConnectionStr, query, ref dataset, "Desk", ref errorString);

            if (string.IsNullOrEmpty(errorString) && dataset.Tables["Desk"].Rows.Count > 0)
            {
                dt = dataset.Tables["Desk"];
                res = dt.Rows[0]["DeskNo"].ToString();
            }
            else
            {
                res = null;
            }
            return res;
        }

        #region Utility

        public static string GetInfoFromXml(string sql, string file)
        {
            //TODO: TEMP
            string path = "PATH";//HttpContext.Current.Server.MapPath("~");
            XmlDocument doc = new XmlDocument();
            //doc.Load("..\\..\\xml\\" + file + ".xml");
            doc.Load(path + "\\bin\\" + file + ".xml");
            XmlNodeList nodeLst = doc.GetElementsByTagName(sql);
            return nodeLst.Item(0).InnerText;
        }

        public static string sql = "SELECT DATE_FORMAT(NOW(),'%d/%m/%Y')";
        public static string Ngayhienhanh_Server
        {
            get
            {
                return gets(sql).Rows[0][0].ToString();
            }
        }

        public static void GetCurrentDateServer(ref string s)
        {
            s = Ngayhienhanh_Server;
        }

        //Get CURDATE()
        public static string sql1 = "SELECT CURDATE()";
        public static string Ngayhienhanh_Server1
        {
            get
            {
                return gets(sql1).Rows[0][0].ToString();
            }
        }

        //get datatable
        public static DataTable gets(string sql)
        {
            DataSet ds;
            MySqlConnection con = new MySqlConnection(pConnectionStr);
            ds = new DataSet();
            MySqlDataAdapter cmd = new MySqlDataAdapter(sql, con);
            cmd.Fill(ds, "table");
            return ds.Tables["table"];
        }

        /// <summary>
        /// Convert 1 chuổi ngày kiểu String thành kiểu Date
        /// </summary>
        public static System.DateTime StringToDateTime(string s)
        {
            string[] format1 = { "dd/MM/yyyy" },
                format2 = { "yyyy-MM-dd HH:mm:ss" };
            return System.DateTime.ParseExact(s.ToString(), (s.Length == 10) ? format1 : format2, System.Globalization.DateTimeFormatInfo.CurrentInfo, System.Globalization.DateTimeStyles.None);
        }
        public static System.DateTime StringToDateTime1(string s)
        {
            string[] format1 = { System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern },
                format2 = { System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern };

            return System.DateTime.ParseExact(s.ToString(), (s.Length == 10) ? format1 : format2, System.Globalization.DateTimeFormatInfo.CurrentInfo, System.Globalization.DateTimeStyles.None);
        }

        /// <summary>
        /// Convert 1 chuổi ngày kiểu Date thành kiểu String
        /// </summary>
        public static string DateToString(string format, System.DateTime date)
        {
            if (date.Equals(null)) return "";
            else return date.ToString(format, System.Globalization.DateTimeFormatInfo.CurrentInfo);
        }

        //Score
        public static string GetValueScoreEx()
        {
            string sFile = "settingcode";
            if (GetInfoFromXml("ScoreValue", sFile) != "") return GetInfoFromXml("ScoreValue", sFile);
            else return "0";
        }

        public static string GetPriceScore()
        {
            string sFile = "settingcode";
            if (GetInfoFromXml("PriceScore", sFile) != "") return GetInfoFromXml("PriceScore", sFile);
            else return "0";
        }

        #endregion
    }
}