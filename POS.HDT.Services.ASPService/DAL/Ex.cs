//using POS.HDT.Common.Data.Domain;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Drawing;
//using System.Drawing.Printing;
//using System.Linq;
//using System.Web;

//namespace POS.HDT.Services.ASPService.DAL
//{
//    /// <summary>
//    /// Temp place here
//    /// </summary>
//    public static partial class DataAccess
//    {
//        #region In Reciept
//        public static void Print_Receipt(string cnStr, string ReceiptId, string Lang, string PlaceOfPrinting, ref string pError)
//        {
//            pError = "";
//            bool print = DoPrintingReceipt(ReceiptId, Lang, PlaceOfPrinting, ref pError);
//        }
//        private static string Language;
//        private static string ReId;
//        public static bool DoPrintingReceipt(string ReceiptId, string Lang, string PlaceOfPrinting, ref string Err)
//        {
//            //PlaceOfPrinting : 1,2,3 
//            //PlaceOfPrinting : null , ""  có nghĩa là chọn máy in mặc định
//            Err = "";
//            try
//            {
//                Language = Lang;
//                ReId = ReceiptId;
//                PrintDocument pd = new PrintDocument();
//                string printerName = ConfigurationManager.AppSettings["Printer0" + PlaceOfPrinting].ToString();
//                if (!string.IsNullOrEmpty(PlaceOfPrinting))
//                {
//                    pd.PrinterSettings.PrinterName = printerName;
//                }
//                //pd.PrinterSettings.PrinterName = @"CutePDF Writer";
//                pd.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A7", 300, 10000);
//                pd.PrintPage += new PrintPageEventHandler(printDocumentReceipt_PrintPage);

//                pd.Print();


//                return true;
//            }
//            catch (Exception ex)
//            {
//                Err = ex.Message.ToString();
//                return false;
//            }
//        }

//        private static void printDocumentReceipt_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
//        {
//            // load data hoa don
//            ReceiptsAll rcAll = GetAllInfo(ReId);
//            int y_p = 0;

//            // Logo
//            string path = HttpContext.Current.Server.MapPath("/");
//            var imgLogo = System.Drawing.Image.FromFile(path + "Image//ShopImg//logoPrint.png");

//            Rectangle logo = new Rectangle(5, 10, 60, 40);
//            e.Graphics.DrawImage(imgLogo, logo);

//            /// 
//            Rectangle strHoaDon = new Rectangle(130, 5, 165, 20);
//            string data = Common.clsLanguages.GetResource("Receipt", Language);//"HÓA ĐƠN";
//            StringFormat strfmt_Left = new StringFormat();
//            StringFormat strfmt_Center = new StringFormat();
//            StringFormat strfmt_Right = new StringFormat();

//            strfmt_Left.LineAlignment = StringAlignment.Near;
//            strfmt_Left.Alignment = StringAlignment.Near;

//            strfmt_Center.LineAlignment = StringAlignment.Center;
//            strfmt_Center.Alignment = StringAlignment.Center;

//            strfmt_Right.LineAlignment = StringAlignment.Far;
//            strfmt_Right.Alignment = StringAlignment.Far;

//            e.Graphics.DrawString(data, new Font("Arial", 12, FontStyle.Bold), Brushes.DarkSlateBlue, strHoaDon, strfmt_Right);

//            //Ngày hóa đơn
//            Rectangle strRec = new Rectangle(120, 25, 175, 15);
//            data = Common.clsLanguages.GetResource("ReceiptDate", Language) + " : " + rcAll.receipt.CreatedDate; //"Ngày :"
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Regular), Brushes.DarkSlateBlue, strRec, strfmt_Right);


//            Rectangle RecLine1 = new Rectangle(115, 40, 180, 2);
//            var Line1 = System.Drawing.Image.FromFile(path + "Image//ShopImg//line1.png");
//            e.Graphics.DrawImage(Line1, RecLine1);

//            Rectangle strRec1 = new Rectangle(120, 42, 175, 18);
//            string strSoHD = rcAll.receipt.ReceiptId;
//            int soHD = int.Parse(strSoHD.Substring(10));
//            data = Common.clsLanguages.GetResource("ReceiptNumber", Language) + " : 10#" + soHD.ToString(); //"Số HĐ: "
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Italic), Brushes.DarkSlateBlue, strRec1, strfmt_Right);

//            //// Cty....
//            Rectangle strCty = new Rectangle(5, 50, 140, 20);
//            data = Common.clsLanguages.GetResource("MerchantName", Language);// "Công ty CP Việt Vang";           
//            e.Graphics.DrawString(data, new Font("Arial", 9, FontStyle.Bold), Brushes.DarkSlateBlue, strCty, strfmt_Left);
//            //Địa chỉ Cty
//            Rectangle strDCCty = new Rectangle(5, 70, 160, 50);
//            data = Common.clsLanguages.GetResource("MerchantAddress", Language) + "\n" + Common.clsLanguages.GetResource("MerchantTel", Language) + "\n" + Common.clsLanguages.GetResource("MerchantWeb", Language);
//            e.Graphics.DrawString(data, new Font("Arial", 7, FontStyle.Regular), Brushes.DarkSlateBlue, strDCCty, strfmt_Left);

//            //Headers
//            Rectangle strHD_STT = new Rectangle(3, 120, 30, 18);
//            data = Common.clsLanguages.GetResource("No", Language);// "STT";           
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, strHD_STT, strfmt_Center);
//            Rectangle strHD_SP = new Rectangle(33, 120, 97, 18);

//            data = Common.clsLanguages.GetResource("Product", Language);//"Sản phẩm ";           
//            e.Graphics.DrawString(data, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, strHD_SP, strfmt_Left);
//            Rectangle strHD_SL = new Rectangle(130, 120, 30, 18);

//            data = Common.clsLanguages.GetResource("ReceiptProQty", Language);//"SL";
//            e.Graphics.DrawString(data, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, strHD_SL, strfmt_Center);
//            Rectangle strHD_DG = new Rectangle(160, 120, 60, 18);

//            data = Common.clsLanguages.GetResource("ReceiptProPrice", Language);//"ĐG";
//            e.Graphics.DrawString(data, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, strHD_DG, strfmt_Center);
//            Rectangle strHD_TT = new Rectangle(220, 120, 70, 18);

//            data = Common.clsLanguages.GetResource("ReceiptProTotalAfterTax", Language);//"TT + Thuế";
//            e.Graphics.DrawString(data, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, strHD_TT, strfmt_Center);
//            Rectangle RecLine2 = new Rectangle(5, 138, 290, 2);

//            var Line2 = System.Drawing.Image.FromFile(path + "Image//ShopImg//line2.png");
//            e.Graphics.DrawImage(Line2, RecLine2);

//            if (rcAll.lst_Detail != null && rcAll.lst_Detail.Count > 0)
//            {
//                y_p = 140;
//                int i = 0;
//                foreach (ReceiptDetails dtl in rcAll.lst_Detail)
//                {
//                    i++;
//                    //Headers
//                    Rectangle strCT_STT = new Rectangle(5, y_p, 28, 22);
//                    data = i.ToString();
//                    e.Graphics.DrawString(data, new Font("Arial", 7, FontStyle.Regular), Brushes.Black, strCT_STT, strfmt_Center);
//                    Rectangle strCT_SP = new Rectangle(33, y_p, 97, 22);
//                    data = Common.clsLanguages.GetResource("ProductID", Language) + dtl.ProductId + " - " + dtl.ProductName;
//                    e.Graphics.DrawString(data, new Font("Arial", 7, FontStyle.Regular), Brushes.Black, strCT_SP, strfmt_Left);
//                    Rectangle strCT_SL = new Rectangle(130, y_p, 30, 22);

//                    data = dtl.Qty;
//                    e.Graphics.DrawString(data, new Font("Arial", 7, FontStyle.Regular), Brushes.Black, strCT_SL, strfmt_Center);
//                    Rectangle strTT_DG = new Rectangle(160, y_p, 60, 22);

//                    data = double.Parse(dtl.Price.ToString()).ToString("0,0");
//                    e.Graphics.DrawString(data, new Font("Arial", 7, FontStyle.Regular), Brushes.Black, strTT_DG, strfmt_Right);
//                    Rectangle strTT_TT = new Rectangle(220, y_p, 70, 22);

//                    data = double.Parse(dtl.TotalAmount.ToString()).ToString("0,0");
//                    e.Graphics.DrawString(data, new Font("Arial", 7, FontStyle.Regular), Brushes.Black, strTT_TT, strfmt_Right);

//                    Rectangle RecLineCT = new Rectangle(5, y_p + 22, 290, 1);
//                    var LineCT = System.Drawing.Image.FromFile(path + "Image//ShopImg//linect.png");
//                    e.Graphics.DrawImage(LineCT, RecLineCT);
//                    y_p = y_p + 23;
//                }
//            }
//            ///Tổng tiền trước thuế
//            Rectangle RecTTTT = new Rectangle(90, y_p, 120, 15);
//            data = Common.clsLanguages.GetResource("TotalAmountBeforeTax", Language);//"Tổng tiền trước thuế ";
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, RecTTTT, strfmt_Right);

//            Rectangle RecTTTT_Val = new Rectangle(210, y_p, 85, 15);
//            data = rcAll.receipt.TotalAmountBeforeTax;
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, RecTTTT_Val, strfmt_Right);
//            y_p = y_p + 15;

//            ///Tổng tiền thuế
//            Rectangle RecTTT = new Rectangle(90, y_p, 120, 15);
//            data = Common.clsLanguages.GetResource("TotalTaxAmount", Language);// "Tổng tiền  thuế ";
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, RecTTT, strfmt_Right);

//            Rectangle RecTTT_Val = new Rectangle(210, y_p, 85, 15);
//            data = rcAll.receipt.TotalTax;
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, RecTTT_Val, strfmt_Right);
//            y_p = y_p + 15;

//            ///Tổng tiền sau thuế
//            Rectangle RecTTST = new Rectangle(90, y_p, 120, 15);
//            data = Common.clsLanguages.GetResource("TotalAmountAfterTax", Language);//"Tổng tiền sau thuế ";
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, RecTTST, strfmt_Right);

//            Rectangle RecTTST_Val = new Rectangle(210, y_p, 85, 15);
//            data = rcAll.receipt.TotalAmount;
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, RecTTST_Val, strfmt_Right);
//            y_p = y_p + 15;

//            ///Giảm giá
//            Rectangle RecDIS = new Rectangle(90, y_p, 120, 15);
//            data = Common.clsLanguages.GetResource("TotalDiscountAmount", Language);//"Giảm giá ";
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, RecDIS, strfmt_Right);

//            Rectangle RecDIS_Val = new Rectangle(210, y_p, 85, 15);
//            data = rcAll.receipt.DiscountAmount;
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, RecDIS_Val, strfmt_Right);
//            y_p = y_p + 15;

//            ///Tổng tiền thanh toán
//            Rectangle RecPAY = new Rectangle(90, y_p, 120, 15);
//            data = Common.clsLanguages.GetResource("TotalMoney", Language);//"TC Cần thanh toán";
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, RecPAY, strfmt_Right);

//            Rectangle RecPAY_Val = new Rectangle(210, y_p, 85, 15);
//            data = rcAll.receipt.TotalMoney;
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, RecPAY_Val, strfmt_Right);
//            y_p = y_p + 15;

//            ////
//            Rectangle RecLine3 = new Rectangle(90, y_p, 200, 1);
//            var Line3 = System.Drawing.Image.FromFile(path + "Image//ShopImg//line1.png");
//            e.Graphics.DrawImage(Line3, RecLine3);
//            y_p = y_p + 2;

//            ///Tổng tiền mặt
//            Rectangle RecCASH = new Rectangle(90, y_p, 120, 15);
//            data = Common.clsLanguages.GetResource("TotalCashCustomer", Language);//"Tiền mặt khách đưa";
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, RecCASH, strfmt_Right);

//            Rectangle RecCASH_Val = new Rectangle(210, y_p, 85, 15);
//            data = rcAll.receipt.CashPayAmt;
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, RecCASH_Val, strfmt_Right);
//            y_p = y_p + 15;

//            ///Tổng tiền thẻ
//            Rectangle RecCARD = new Rectangle(90, y_p, 120, 15);
//            data = Common.clsLanguages.GetResource("TotalCardAmount", Language);//"Tiền thẻ khách:";
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, RecCARD, strfmt_Right);

//            Rectangle RecCARD_Val = new Rectangle(210, y_p, 85, 15);
//            data = rcAll.receipt.CardPayAmt;
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, RecCARD_Val, strfmt_Right);
//            y_p = y_p + 15;

//            ////
//            Rectangle RecLine4 = new Rectangle(90, y_p, 200, 1);
//            var Line4 = System.Drawing.Image.FromFile(path + "Image//ShopImg//line1.png");
//            e.Graphics.DrawImage(Line4, RecLine4);
//            y_p = y_p + 2;


//            ///Tổng tiền thối lại
//            Rectangle RecRETU = new Rectangle(90, y_p, 120, 15);
//            data = Common.clsLanguages.GetResource("TotalReturnAmount", Language);//"Tiền thối:";
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, RecRETU, strfmt_Right);

//            Rectangle RecRETU_Val = new Rectangle(210, y_p, 85, 15);
//            data = rcAll.receipt.ReturnAmt;
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, RecRETU_Val, strfmt_Right);
//            y_p = y_p + 15;

//            if (rcAll.receiptMember != null && !string.IsNullOrEmpty(rcAll.receiptMember.MemberId))
//            {
//                ////
//                Rectangle RecLine5 = new Rectangle(5, y_p, 290, 2);
//                var Line5 = System.Drawing.Image.FromFile(path + "Image//ShopImg//line1.png");
//                e.Graphics.DrawImage(Line5, RecLine5);
//                y_p = y_p + 4;

//                Rectangle RecTTKH = new Rectangle(10, y_p, 160, 18);
//                data = Common.clsLanguages.GetResource("CustomerInformation", Language);//"Thông tin khách hàng";
//                e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, RecTTKH, strfmt_Left);
//                y_p = y_p + 18;

//                Rectangle RecTTKH1 = new Rectangle(10, y_p, 190, 15);
//                data = Common.clsLanguages.GetResource("FullName", Language) + " : " + rcAll.receiptMember.FullName;
//                e.Graphics.DrawString(data, new Font("Arial", 7, FontStyle.Regular), Brushes.Black, RecTTKH1, strfmt_Left);

//                Rectangle RecTTKH1A = new Rectangle(205, y_p, 90, 15);
//                data = Common.clsLanguages.GetResource("TotalScore", Language) + " : " + rcAll.receiptMember.MemberScore;
//                e.Graphics.DrawString(data, new Font("Arial", 7, FontStyle.Regular), Brushes.Black, RecTTKH1A, strfmt_Left);
//                y_p = y_p + 15;

//                Rectangle RecTTKH2 = new Rectangle(10, y_p, 190, 15);
//                data = Common.clsLanguages.GetResource("MemberID", Language) + " : " + rcAll.receiptMember.MemberId;
//                e.Graphics.DrawString(data, new Font("Arial", 7, FontStyle.Regular), Brushes.Black, RecTTKH2, strfmt_Left);
//                y_p = y_p + 15;
//            }

//            ////
//            Rectangle RecLine6 = new Rectangle(5, y_p, 290, 2);
//            var Line6 = System.Drawing.Image.FromFile(path + "Image//ShopImg//line1.png");
//            e.Graphics.DrawImage(Line6, RecLine6);
//            y_p = y_p + 4;

//            Rectangle RecTK = new Rectangle(5, y_p, 190, 18);
//            data = Common.clsLanguages.GetResource("ReceiptFooterText", Language); //"Cám ơn & hẹn gặp lại quý khách !";
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Italic), Brushes.Black, RecTK, strfmt_Left);

//            Rectangle RecPower = new Rectangle(200, y_p, 95, 47);
//            var LineP = System.Drawing.Image.FromFile(path + "Image//VVImg//powered by vv.png");
//            e.Graphics.DrawImage(LineP, RecPower);
//            y_p = y_p + 47;

//            // show các PromoText
//            DataTable dtPromo = GetValidPromotionToPrint();
//            if (dtPromo != null && dtPromo.Rows.Count > 0)
//            {
//                ////
//                Rectangle RecLine7 = new Rectangle(5, y_p, 290, 1);
//                var Line7 = System.Drawing.Image.FromFile(path + "Image//ShopImg//line1.png");
//                e.Graphics.DrawImage(Line7, RecLine7);
//                y_p = y_p + 4;
//                //////
//                foreach (DataRow dr in dtPromo.Rows)
//                {
//                    Rectangle RecPromo = new Rectangle(5, y_p, 290, 130);
//                    data = "\n---------------\n" + dr["PromoText"].ToString() + "\n---------------\n";
//                    e.Graphics.DrawString(data, new Font("Arial", 7, FontStyle.Italic), Brushes.Black, RecPromo, strfmt_Center);
//                    y_p = y_p + 130;
//                }
//            }

//        }

//        #region Lấy tất cả thông tin của 1 HD
//        public static ReceiptsAll GetAllInfo(string ReceiptID)
//        {
//            ReceiptsAll res = new ReceiptsAll();

//            DataSet ds = new DataSet();
//            string[][] param ={
//                new string[] {"p_ReceiptId", ReceiptID},
//            };
//            string errorString = "";
//            string cnStr = GetConnectionString();
//            DataStoreProcQuery_Param(cnStr, "spSelect_Receipt_AllInfo", ref ds, param, ref errorString);

//            if (string.IsNullOrEmpty(errorString) && ds.Tables.Count > 0)
//            {
//                if (ds.Tables[0].Rows.Count > 0)
//                {
//                    DataTable dt = ds.Tables[0];
//                    DataRow dr = dt.Rows[0];
//                    Receipts rc = new Receipts();
//                    rc.ReceiptId = dr["ReceiptId"].ToString();
//                    rc.CreatedBy = dr["CreatedBy"].ToString();
//                    rc.CreatedDate = dr["CreatedDate"].ToString();
//                    rc.TotalAmountBeforeTax = double.Parse(dr["TotalAmountBeforeTax"].ToString()).ToString("0,0");
//                    rc.TotalTax = double.Parse(dr["TotalTax"].ToString()).ToString("0,0");
//                    rc.TotalAmount = double.Parse(dr["TotalAmount"].ToString()).ToString("0,0");
//                    rc.DiscountAmount = double.Parse(dr["DiscountAmount"].ToString()).ToString("0,0");
//                    rc.TotalMoney = double.Parse(dr["TotalMoney"].ToString()).ToString("0,0");
//                    rc.CashPayAmt = double.Parse(dr["CashPayAmt"].ToString()).ToString("0,0");
//                    rc.CardPayAmt = double.Parse(dr["CardPayAmt"].ToString()).ToString("0,0");
//                    rc.ReturnAmt = double.Parse(dr["ReturnAmt"].ToString()).ToString("0,0");
//                    res.receipt = rc;
//                }
//                /////
//                if (ds.Tables[1].Rows.Count > 0)
//                {
//                    DataTable dt = ds.Tables[1];
//                    List<ReceiptDetails> lst = new List<ReceiptDetails>();
//                    foreach (DataRow dr in dt.Rows)
//                    {
//                        ReceiptDetails rc = new ReceiptDetails();
//                        rc.ReceiptId = dr["ReceiptId"].ToString();
//                        rc.ProductId = dr["ProductId"].ToString();
//                        rc.ProductName = GetProductName(dr["ProductId"].ToString());
//                        rc.Qty = double.Parse(dr["Qty"].ToString()).ToString("0,0");
//                        rc.Price = double.Parse(dr["Price"].ToString()).ToString("0,0");
//                        rc.TotalAmountBeforeTax = double.Parse(dr["TotalAmountBeforeTax"].ToString()).ToString("0,0");
//                        rc.TaxAmount = double.Parse(dr["TaxAmount"].ToString()).ToString("0,0");
//                        rc.TotalAmount = double.Parse(dr["TotalAmount"].ToString()).ToString("0,0");

//                        lst.Add(rc);
//                    }
//                    res.lst_Detail = lst;
//                }
//                ///thẻ
//                if (ds.Tables[2].Rows.Count > 0)
//                {
//                    DataTable dt = ds.Tables[2];
//                    List<ReceiptsCard> lst = new List<ReceiptsCard>();
//                    foreach (DataRow dr in dt.Rows)
//                    {
//                        ReceiptsCard rc = new ReceiptsCard();
//                        rc.ReceiptId = dr["ReceiptId"].ToString();
//                        rc.CardNo = dr["CardNo"].ToString();
//                        rc.CardHolderName = dr["CardHolderName"].ToString();
//                        rc.ExpiredDate = dr["ExpiredDate"].ToString();
//                        rc.CardType = dr["CardType"].ToString();
//                        rc.Bank = dr["Bank"].ToString();
//                        rc.TotalAmount = double.Parse(dr["TotalAmount"].ToString()).ToString("0,0");
//                        lst.Add(rc);
//                    }
//                    res.lst_card = lst;
//                }

//                // ReceiptInfo
//                if (ds.Tables[3].Rows.Count > 0)
//                {
//                    DataTable dt = ds.Tables[3];
//                    DataRow dr = dt.Rows[0];
//                    ReceiptInfo rc = new ReceiptInfo();
//                    rc.ReceiptId = dr["ReceiptId"].ToString();
//                    rc.CustomerName = dr["CustomerName"].ToString();
//                    rc.Address = dr["Address"].ToString();
//                    rc.Phone = dr["Phone"].ToString();
//                    rc.Note = dr["Note"].ToString();
//                    res.receiptInfo = rc;
//                }

//                // ReceiptMember
//                if (ds.Tables[4].Rows.Count > 0)
//                {
//                    DataTable dt = ds.Tables[4];
//                    DataRow dr = dt.Rows[0];
//                    ReceiptMember rc = new ReceiptMember();
//                    rc.ReceiptId = dr["ReceiptId"].ToString();
//                    rc.MemberId = dr["MemberId"].ToString();
//                    rc.CreatedBy = dr["CreatedBy"].ToString();
//                    DataTable dte = GetMemberInfo(dr["MemberId"].ToString());
//                    if (dte != null)
//                    {
//                        DataRow drr = dte.Rows[0];
//                        rc.MemberCode = drr["MemberCode"].ToString();
//                        rc.ObjectId = drr["ObjectId"].ToString();
//                        rc.NumberOfVissits = drr["NumberOfVissits"].ToString();
//                        rc.LastestDate = drr["LastestDate"].ToString();
//                        rc.MemberType = drr["MemberType"].ToString();

//                        rc.MemberScore = drr["MemberScore"].ToString();
//                        rc.ObjectGroup = drr["ObjectGroup"].ToString();
//                        rc.ObjectType = drr["ObjectType"].ToString();
//                        rc.Tel = drr["Tel"].ToString();
//                        rc.Email = drr["Email"].ToString();
//                        rc.MemberCode = drr["Gender"].ToString();
//                        rc.FullName = drr["FullName"].ToString();
//                        rc.TemAdd = drr["TemAdd"].ToString();
//                    }
//                    res.receiptMember = rc;
//                }

//            }
//            else
//            {
//                res = null;
//            }

//            return res;
//        }
//        public static string GetProductName(string ProID)
//        {
//            string res = "";
//            DataTable dt = new DataTable();
//            string query = @"SELECT *
//                            FROM `products`
//                            WHERE  `ProductId` ='" + ProID + "'";
//            DataSet dataset = new DataSet();
//            string errorString = "";
//            DataQuery(GetConnectionString(), query, ref dataset, "Products", ref errorString);

//            if (string.IsNullOrEmpty(errorString) && dataset.Tables["Products"].Rows.Count > 0)
//            {
//                dt = dataset.Tables["Products"];
//                res = dt.Rows[0]["name"].ToString();
//            }
//            else
//            {
//                res = null;
//            }
//            return res;
//        }

//        public static string GetDeskNO(string DeskId)
//        {
//            string res = "";
//            DataTable dt = new DataTable();
//            string query = @"SELECT *
//                            FROM `desk`
//                            WHERE  `DeskId` ='" + DeskId + "'";
//            DataSet dataset = new DataSet();
//            string errorString = "";
//            DataQuery(GetConnectionString(), query, ref dataset, "Desk", ref errorString);

//            if (string.IsNullOrEmpty(errorString) && dataset.Tables["Desk"].Rows.Count > 0)
//            {
//                dt = dataset.Tables["Desk"];
//                res = dt.Rows[0]["DeskNo"].ToString();
//            }
//            else
//            {
//                res = null;
//            }
//            return res;
//        }

//        public static DataTable GetMemberInfo(string MemberID)
//        {
//            DataTable dt = new DataTable();
//            string query = @"SELECT `members`.`MemberId`,`members`.`MemberCode`,`members`.`ObjectId`,`members`.`NumberOfVissits`,`members`.`LastestDate`,`members`.`MemberType`,`members`.`MemberScore`,
//                            `objects`.`ObjectGroup`,`objects`.`ObjectType`,`objects`.`Tel`,`objects`.`Email`,`objects`.`Gender`,`objects`.`FullName`,`objects`.`TemAdd`
//                            FROM `members` INNER JOIN `objects` ON `members`.`ObjectId`=`objects`.`ObjectId`
//                            WHERE  `members`.`MemberId` ='" + MemberID + "'";
//            DataSet dataset = new DataSet();
//            string errorString = "";
//            DataQuery(GetConnectionString(), query, ref dataset, "X", ref errorString);

//            if (string.IsNullOrEmpty(errorString) && dataset.Tables[0].Rows.Count > 0)
//            {
//                dt = dataset.Tables["X"];
//            }
//            else
//            {
//                dt = null;
//            }
//            return dt;
//        }
//        #endregion

//        #region
//        public static DataTable GetValidPromotionToPrint()
//        {
//            DataTable dt = new DataTable();
//            string query = @"SELECT *
//                            FROM `promotions`
//                            WHERE NOW() BETWEEN `BeginDateTime` AND `EndDateTime`
//                            AND `PrintToBill`='1'";
//            DataSet dataset = new DataSet();
//            string errorString = "";
//            DataQuery(GetConnectionString(), query, ref dataset, "X", ref errorString);

//            if (string.IsNullOrEmpty(errorString) && dataset.Tables[0].Rows.Count > 0)
//            {
//                dt = dataset.Tables["X"];
//            }
//            else
//            {
//                dt = null;
//            }
//            return dt;
//        }

//        #endregion

//        #endregion


//        #region In Order
//        public static void Print_Order(string cnStr, string OrderId, string Lang, string PlaceOfPrinting, string StatusPrint, ref string pError)
//        {
//            pError = "";
//            bool print = DoPrintingOrder(OrderId, Lang, PlaceOfPrinting, StatusPrint, ref pError);
//        }
//        //private static string Language;
//        private static string OrId;
//        public static bool DoPrintingOrder(string OrderId, string Lang, string PlaceOfPrinting, string StatusPrint, ref string Err)
//        {
//            //PlaceOfPrinting : 1,2,3 
//            //PlaceOfPrinting : null , ""  có nghĩa là chọn máy in mặc định
//            Err = "";
//            try
//            {
//                Language = Lang;
//                OrId = OrderId;
//                PrintDocument pd = new PrintDocument();
//                string printerName = PlaceOfPrinting;//ConfigurationManager.AppSettings["Printer0" + PlaceOfPrinting].ToString();
//                if (!string.IsNullOrEmpty(PlaceOfPrinting))
//                {
//                    pd.PrinterSettings.PrinterName = printerName;
//                }
//                //pd.PrinterSettings.PrinterName = @"CutePDF Writer";
//                pd.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A7", 300, 10000);
//                if (StatusPrint == "0")
//                    pd.PrintPage += new PrintPageEventHandler(printDocumentOrderKitchen_PrintPage);
//                else if (StatusPrint == "3")
//                    pd.PrintPage += new PrintPageEventHandler(printDocumentOrderKitchen_Update_PrintPage);
//                else if (StatusPrint == "2")
//                    pd.PrintPage += new PrintPageEventHandler(printDocumentOrderKitchen_Cancel_PrintPage);

//                pd.Print();


//                return true;
//            }
//            catch (Exception ex)
//            {
//                Err = ex.Message.ToString();
//                return false;
//            }
//        }

//        private static void printDocumentOrder_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
//        {
//            // load data hoa don
//            OrderAll rcAll = new OrderAll();
//            rcAll = GetOrderInfo(OrId);
//            int y_p = 0;

//            // Logo
//            string path = HttpContext.Current.Server.MapPath("/");
//            var imgLogo = System.Drawing.Image.FromFile(path + "Image//ShopImg//logoPrint.png");

//            Rectangle logo = new Rectangle(5, 10, 60, 40);
//            e.Graphics.DrawImage(imgLogo, logo);

//            /// 
//            Rectangle strHoaDon = new Rectangle(130, 5, 165, 20);
//            string data = Common.clsLanguages.GetResource("Order", Language);//"ORDER";
//            StringFormat strfmt_Left = new StringFormat();
//            StringFormat strfmt_Center = new StringFormat();
//            StringFormat strfmt_Right = new StringFormat();

//            strfmt_Left.LineAlignment = StringAlignment.Near;
//            strfmt_Left.Alignment = StringAlignment.Near;

//            strfmt_Center.LineAlignment = StringAlignment.Center;
//            strfmt_Center.Alignment = StringAlignment.Center;

//            strfmt_Right.LineAlignment = StringAlignment.Far;
//            strfmt_Right.Alignment = StringAlignment.Far;

//            e.Graphics.DrawString(data, new Font("Arial", 12, FontStyle.Bold), Brushes.DarkSlateBlue, strHoaDon, strfmt_Right);

//            //Ngày hóa đơn
//            Rectangle strRec = new Rectangle(120, 25, 175, 15);
//            data = Common.clsLanguages.GetResource("ReceiptDate", Language) + " : " + rcAll.order.CreateDate; //"Ngày :"
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Regular), Brushes.DarkSlateBlue, strRec, strfmt_Right);


//            Rectangle RecLine1 = new Rectangle(115, 40, 180, 2);
//            var Line1 = System.Drawing.Image.FromFile(path + "Image//ShopImg//line1.png");
//            e.Graphics.DrawImage(Line1, RecLine1);

//            Rectangle strRec1 = new Rectangle(120, 42, 175, 18);
//            string strSoHD = rcAll.order.OrderId;
//            int soHD = int.Parse(strSoHD.Substring(10));
//            data = Common.clsLanguages.GetResource("OrderId", Language) + " : 10#" + soHD.ToString(); //"Số HĐ: "
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Italic), Brushes.DarkSlateBlue, strRec1, strfmt_Right);

//            //// Cty....
//            Rectangle strCty = new Rectangle(5, 50, 140, 20);
//            data = Common.clsLanguages.GetResource("MerchantName", Language);// "Công ty CP Việt Vang";           
//            e.Graphics.DrawString(data, new Font("Arial", 9, FontStyle.Bold), Brushes.DarkSlateBlue, strCty, strfmt_Left);
//            //Địa chỉ Cty
//            Rectangle strDCCty = new Rectangle(5, 70, 160, 50);
//            data = Common.clsLanguages.GetResource("MerchantAddress", Language) + "\n" + Common.clsLanguages.GetResource("MerchantTel", Language) + "\n" + Common.clsLanguages.GetResource("MerchantWeb", Language);
//            e.Graphics.DrawString(data, new Font("Arial", 7, FontStyle.Regular), Brushes.DarkSlateBlue, strDCCty, strfmt_Left);

//            //Headers
//            Rectangle strHD_STT = new Rectangle(3, 120, 30, 18);
//            data = Common.clsLanguages.GetResource("No", Language);// "STT";           
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, strHD_STT, strfmt_Center);
//            Rectangle strHD_SP = new Rectangle(33, 120, 97, 18);

//            data = Common.clsLanguages.GetResource("Product", Language);//"Sản phẩm ";           
//            e.Graphics.DrawString(data, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, strHD_SP, strfmt_Left);
//            Rectangle strHD_SL = new Rectangle(130, 120, 30, 18);

//            data = Common.clsLanguages.GetResource("ReceiptProQty", Language);//"SL";
//            e.Graphics.DrawString(data, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, strHD_SL, strfmt_Center);
//            Rectangle strHD_DG = new Rectangle(160, 120, 60, 18);

//            data = Common.clsLanguages.GetResource("ReceiptProPrice", Language);//"ĐG";
//            e.Graphics.DrawString(data, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, strHD_DG, strfmt_Center);
//            Rectangle strHD_TT = new Rectangle(220, 120, 70, 18);

//            data = Common.clsLanguages.GetResource("ReceiptProTotalAfterTax", Language);//"TT + Thuế";
//            e.Graphics.DrawString(data, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, strHD_TT, strfmt_Center);
//            Rectangle RecLine2 = new Rectangle(5, 138, 290, 2);

//            var Line2 = System.Drawing.Image.FromFile(path + "Image//ShopImg//line2.png");
//            e.Graphics.DrawImage(Line2, RecLine2);

//            if (rcAll.lst_Detail != null && rcAll.lst_Detail.Count > 0)
//            {
//                y_p = 140;
//                int i = 0;
//                foreach (OrderDetails dtl in rcAll.lst_Detail)
//                {
//                    i++;
//                    //Headers
//                    Rectangle strCT_STT = new Rectangle(5, y_p, 28, 22);
//                    data = i.ToString();
//                    e.Graphics.DrawString(data, new Font("Arial", 7, FontStyle.Regular), Brushes.Black, strCT_STT, strfmt_Center);
//                    Rectangle strCT_SP = new Rectangle(33, y_p, 97, 22);
//                    data = Common.clsLanguages.GetResource("ProductID", Language) + dtl.ProductId + " - " + dtl.ProductName;
//                    e.Graphics.DrawString(data, new Font("Arial", 7, FontStyle.Regular), Brushes.Black, strCT_SP, strfmt_Left);
//                    Rectangle strCT_SL = new Rectangle(130, y_p, 30, 22);

//                    data = dtl.Qty;
//                    e.Graphics.DrawString(data, new Font("Arial", 7, FontStyle.Regular), Brushes.Black, strCT_SL, strfmt_Center);
//                    Rectangle strTT_DG = new Rectangle(160, y_p, 60, 22);

//                    data = double.Parse(dtl.Price.ToString()).ToString("0,0");
//                    e.Graphics.DrawString(data, new Font("Arial", 7, FontStyle.Regular), Brushes.Black, strTT_DG, strfmt_Right);
//                    Rectangle strTT_TT = new Rectangle(220, y_p, 70, 22);

//                    data = double.Parse(dtl.TotalAmount.ToString()).ToString("0,0");
//                    e.Graphics.DrawString(data, new Font("Arial", 7, FontStyle.Regular), Brushes.Black, strTT_TT, strfmt_Right);

//                    Rectangle RecLineCT = new Rectangle(5, y_p + 22, 290, 1);
//                    var LineCT = System.Drawing.Image.FromFile(path + "Image//ShopImg//linect.png");
//                    e.Graphics.DrawImage(LineCT, RecLineCT);
//                    y_p = y_p + 23;
//                }
//            }
//            ///Tổng tiền trước thuế
//            Rectangle RecTTTT = new Rectangle(90, y_p, 120, 15);
//            data = Common.clsLanguages.GetResource("TotalAmountBeforeTax", Language);//"Tổng tiền trước thuế ";
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, RecTTTT, strfmt_Right);

//            Rectangle RecTTTT_Val = new Rectangle(210, y_p, 85, 15);
//            data = rcAll.order.TotalAmountBeforeTax;
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, RecTTTT_Val, strfmt_Right);
//            y_p = y_p + 15;

//            ///Tổng tiền thuế
//            Rectangle RecTTT = new Rectangle(90, y_p, 120, 15);
//            data = Common.clsLanguages.GetResource("TotalTaxAmount", Language);// "Tổng tiền  thuế ";
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, RecTTT, strfmt_Right);

//            Rectangle RecTTT_Val = new Rectangle(210, y_p, 85, 15);
//            data = rcAll.order.TotalTax;
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, RecTTT_Val, strfmt_Right);
//            y_p = y_p + 15;

//            ///Tổng tiền sau thuế
//            Rectangle RecTTST = new Rectangle(90, y_p, 120, 15);
//            data = Common.clsLanguages.GetResource("TotalAmountAfterTax", Language);//"Tổng tiền sau thuế ";
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, RecTTST, strfmt_Right);

//            Rectangle RecTTST_Val = new Rectangle(210, y_p, 85, 15);
//            data = rcAll.order.TotalAmmount;
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, RecTTST_Val, strfmt_Right);
//            y_p = y_p + 15;

//            ///Giảm giá
//            Rectangle RecDIS = new Rectangle(90, y_p, 120, 15);
//            data = Common.clsLanguages.GetResource("TotalDiscountAmount", Language);//"Giảm giá ";
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, RecDIS, strfmt_Right);

//            Rectangle RecDIS_Val = new Rectangle(210, y_p, 85, 15);
//            data = rcAll.order.DisCountAmount;
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, RecDIS_Val, strfmt_Right);
//            y_p = y_p + 15;

//            ///Tổng tiền thanh toán
//            Rectangle RecPAY = new Rectangle(90, y_p, 120, 15);
//            data = Common.clsLanguages.GetResource("TotalMoney", Language);//"TC Cần thanh toán";
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, RecPAY, strfmt_Right);

//            Rectangle RecPAY_Val = new Rectangle(210, y_p, 85, 15);
//            data = rcAll.order.TotalMoney;
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, RecPAY_Val, strfmt_Right);
//            y_p = y_p + 15;

//            ////
//            Rectangle RecLine6 = new Rectangle(5, y_p, 290, 2);
//            var Line6 = System.Drawing.Image.FromFile(path + "Image//ShopImg//line1.png");
//            e.Graphics.DrawImage(Line6, RecLine6);
//            y_p = y_p + 4;

//            Rectangle RecTK = new Rectangle(5, y_p, 190, 18);
//            data = Common.clsLanguages.GetResource("ReceiptFooterText", Language); //"Cám ơn & hẹn gặp lại quý khách !";
//            e.Graphics.DrawString(data, new Font("Arial", 8, FontStyle.Italic), Brushes.Black, RecTK, strfmt_Left);

//            Rectangle RecPower = new Rectangle(200, y_p, 95, 47);
//            var LineP = System.Drawing.Image.FromFile(path + "Image//VVImg//powered by vv.png");
//            e.Graphics.DrawImage(LineP, RecPower);
//            y_p = y_p + 47;

//        }

//        private static void printDocumentOrderKitchen_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
//        {
//            // load data hoa don
//            OrderAll rcAll = new OrderAll();
//            rcAll = GetOrderInfo(OrId);
//            int y_p = 0;

//            string[][] list = new string[rcAll.lst_Detail.Count][];
//            int j = 0;
//            foreach (OrderDetails dtl in rcAll.lst_Detail)
//            {
//                string[] arr = new string[4];
//                arr[0] = j.ToString();
//                //arr[1] = dtl.ProductId + " - " + dtl.ProductName;
//                arr[1] = dtl.ProductName;
//                arr[2] = dtl.Qty;
//                arr[3] = dtl.Note;//(dtl.Notes == null) ? "" : dtl.Notes;
//                list[j] = arr;
//                j++;
//            }
//            string data;
//            StringFormat strfmt_Left = new StringFormat();
//            StringFormat strfmt_Center = new StringFormat();
//            StringFormat strfmt_Right = new StringFormat();

//            strfmt_Left.LineAlignment = StringAlignment.Near;
//            strfmt_Left.Alignment = StringAlignment.Near;

//            strfmt_Center.LineAlignment = StringAlignment.Center;
//            strfmt_Center.Alignment = StringAlignment.Center;

//            strfmt_Right.LineAlignment = StringAlignment.Far;
//            strfmt_Right.Alignment = StringAlignment.Far;

//            y_p += 20;
//            Rectangle strDesk = new Rectangle(5, y_p, 250, 22);
//            data = Common.clsLanguages.GetResource("Desk_", Language) + " " + rcAll.order.DeskId;
//            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strDesk, strfmt_Left);

//            Rectangle strDate = new Rectangle(5, y_p + 22, 250, 22);
//            data = Common.clsLanguages.GetResource("ReceiptDate", Language) + " " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
//            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strDate, strfmt_Left);

//            Rectangle strReceiptId1 = new Rectangle(180, y_p, 100, 30);
//            data = Int32.Parse(rcAll.order.OrderId.Substring(10)).ToString();
//            e.Graphics.DrawString(data, new Font("Arial", 16, FontStyle.Bold), Brushes.Black, strReceiptId1, strfmt_Right);

//            y_p += 45;
//            Rectangle strReceiptId = new Rectangle(5, y_p, 250, 22);
//            data = Common.clsLanguages.GetResource("OrderIDNo", Language) + " : 10#" + rcAll.order.OrderId.Substring(10);
//            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strReceiptId, strfmt_Left);

//            y_p += 23;
//            Rectangle strHD_STT = new Rectangle(5, y_p, 63, 22); // đổi , 30-28
//            data = Common.clsLanguages.GetResource("No", Language);
//            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strHD_STT, strfmt_Left);

//            Rectangle strHD_SP = new Rectangle(35, y_p, 220, 22);
//            data = Common.clsLanguages.GetResource("Product", Language);//"Sản phẩm ";  
//            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strHD_SP, strfmt_Left);

//            Rectangle strHD_SL = new Rectangle(260, y_p, 45, 22);
//            data = Common.clsLanguages.GetResource("ReceiptProQty", Language);//"SL";
//            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strHD_SL, strfmt_Left);
//            y_p += 30;
//            for (int i = 0; i < list.Length; i++)
//            {
//                //Headers
//                Rectangle strCT_STT = new Rectangle(5, y_p, 30, 22);
//                data = (i + 1).ToString();
//                e.Graphics.DrawString(data, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, strCT_STT, strfmt_Left);

//                data = list[i][1];
//                int heightl = Int32.Parse(Math.Ceiling((double)data.Length / 17).ToString()) * 18 + 5;
//                Rectangle strCT_SP = new Rectangle(35, y_p, 220, heightl);
//                e.Graphics.DrawString(data, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, strCT_SP, strfmt_Left);

//                Rectangle strCT_SL = new Rectangle(260, y_p, 45, 22);

//                data = list[i][2];
//                e.Graphics.DrawString(data, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, strCT_SL, strfmt_Left);


//                data = list[i][3];
//                int height = 0;
//                if (!string.IsNullOrEmpty(data))
//                {
//                    height = Int32.Parse(Math.Ceiling((double)data.Length / 17).ToString()) * 18 + 4;
//                    Rectangle notes = new Rectangle(5, y_p + heightl, 280, height);
//                    e.Graphics.DrawString(data, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, notes, strfmt_Left);
//                }

//                Rectangle RecLineCT = new Rectangle(5, y_p + heightl + height, 280, 22);
//                data = "-----------------------------------------------------------------------";
//                e.Graphics.DrawString(data, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, RecLineCT, strfmt_Left);


//                y_p = y_p + height + heightl + 20;
//            }
//        }
//        private static void printDocumentOrderKitchen_Update_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
//        {
//            // load data hoa don
//            OrderAll rcAll = new OrderAll();
//            rcAll = GetOrderInfo(OrId);
//            int y_p = 0;

//            string[][] list = new string[rcAll.lst_Detail.Count][];
//            int j = 0;
//            foreach (OrderDetails dtl in rcAll.lst_Detail)
//            {
//                string[] arr = new string[5];
//                arr[0] = j.ToString();
//                //arr[1] = dtl.ProductId + " - " + dtl.ProductName;
//                arr[1] = dtl.ProductName;
//                arr[2] = dtl.Qty;
//                arr[3] = dtl.Note;//(dtl.Notes == null) ? "" : dtl.Notes;
//                arr[4] = dtl.Status;
//                list[j] = arr;
//                j++;
//            }
//            string data;
//            StringFormat strfmt_Left = new StringFormat();
//            StringFormat strfmt_Center = new StringFormat();
//            StringFormat strfmt_Right = new StringFormat();

//            strfmt_Left.LineAlignment = StringAlignment.Near;
//            strfmt_Left.Alignment = StringAlignment.Near;

//            strfmt_Center.LineAlignment = StringAlignment.Center;
//            strfmt_Center.Alignment = StringAlignment.Center;

//            strfmt_Right.LineAlignment = StringAlignment.Far;
//            strfmt_Right.Alignment = StringAlignment.Far;


//            y_p += 20;
//            Rectangle strDesk = new Rectangle(5, y_p, 250, 22);
//            data = Common.clsLanguages.GetResource("Desk_", Language) + " " + rcAll.order.DeskId;
//            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strDesk, strfmt_Left);

//            Rectangle strDate = new Rectangle(5, y_p + 22, 250, 22);
//            data = Common.clsLanguages.GetResource("ReceiptDate", Language) + " " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
//            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strDate, strfmt_Left);

//            Rectangle strReceiptId1 = new Rectangle(180, y_p, 100, 30);
//            data = Int32.Parse(rcAll.order.OrderId.Substring(10)).ToString();
//            e.Graphics.DrawString(data, new Font("Arial", 16, FontStyle.Bold), Brushes.Black, strReceiptId1, strfmt_Right);

//            y_p += 45;
//            Rectangle strReceiptId = new Rectangle(5, y_p, 250, 22);
//            data = Common.clsLanguages.GetResource("UpdateOrder_1", Language) + " : 10#" + rcAll.order.OrderId.Substring(10);
//            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strReceiptId, strfmt_Left);

//            y_p += 23;
//            Rectangle strHD_STT = new Rectangle(5, y_p, 63, 22); // đổi , 30-28
//            data = Common.clsLanguages.GetResource("No", Language);
//            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strHD_STT, strfmt_Left);

//            Rectangle strHD_SP = new Rectangle(35, y_p, 220, 22);
//            data = Common.clsLanguages.GetResource("Product", Language);//"Sản phẩm ";  
//            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strHD_SP, strfmt_Left);

//            Rectangle strHD_SL = new Rectangle(260, y_p, 45, 22);
//            data = Common.clsLanguages.GetResource("ReceiptProQty", Language);//"SL";
//            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strHD_SL, strfmt_Left);
//            y_p += 30;
//            FontStyle ft = new FontStyle();
//            for (int i = 0; i < list.Length; i++)
//            {
//                if (list[i][4] == "0")
//                {
//                    ft = FontStyle.Italic;
//                }
//                else if (list[i][4] == "2") ft = FontStyle.Strikeout;
//                //Headers

//                Rectangle strCT_STT = new Rectangle(5, y_p, 30, 22);
//                data = (i + 1).ToString();
//                e.Graphics.DrawString(data, new Font("Arial", 14, ft), Brushes.Black, strCT_STT, strfmt_Left);

//                data = list[i][1];
//                int heightl = Int32.Parse(Math.Ceiling((double)data.Length / 17).ToString()) * 18 + 5;
//                Rectangle strCT_SP = new Rectangle(35, y_p, 220, heightl);
//                e.Graphics.DrawString(data, new Font("Arial", 14, ft), Brushes.Black, strCT_SP, strfmt_Left);

//                Rectangle strCT_SL = new Rectangle(260, y_p, 45, 22);
//                data = list[i][2];
//                e.Graphics.DrawString(data, new Font("Arial", 14, ft), Brushes.Black, strCT_SL, strfmt_Left);


//                data = list[i][3];
//                int height = 0;
//                if (!string.IsNullOrEmpty(data))
//                {
//                    height = Int32.Parse(Math.Ceiling((double)data.Length / 17).ToString()) * 18 + 4;
//                    Rectangle notes = new Rectangle(5, y_p + heightl, 280, height);
//                    e.Graphics.DrawString(data, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, notes, strfmt_Left);
//                }

//                Rectangle RecLineCT = new Rectangle(5, y_p + heightl + height, 280, 22);
//                data = "-----------------------------------------------------------------------";
//                e.Graphics.DrawString(data, new Font("Arial", 14, ft), Brushes.Black, RecLineCT, strfmt_Left);


//                y_p = y_p + height + heightl + 20;
//            }
//        }
//        private static void printDocumentOrderKitchen_Cancel_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
//        {
//            // load data hoa don
//            OrderAll rcAll = new OrderAll();
//            rcAll = GetOrderInfo(OrId);
//            int y_p = 0;

//            string[][] list = new string[rcAll.lst_Detail.Count][];
//            int j = 0;
//            foreach (OrderDetails dtl in rcAll.lst_Detail)
//            {
//                string[] arr = new string[5];
//                arr[0] = j.ToString();
//                //arr[1] = dtl.ProductId + " - " + dtl.ProductName;
//                arr[1] = dtl.ProductName;
//                arr[2] = dtl.Qty;
//                arr[3] = dtl.Note;//(dtl.Notes == null) ? "" : dtl.Notes;
//                arr[4] = dtl.Status;
//                list[j] = arr;
//                j++;
//            }
//            string data;
//            StringFormat strfmt_Left = new StringFormat();
//            StringFormat strfmt_Center = new StringFormat();
//            StringFormat strfmt_Right = new StringFormat();

//            strfmt_Left.LineAlignment = StringAlignment.Near;
//            strfmt_Left.Alignment = StringAlignment.Near;

//            strfmt_Center.LineAlignment = StringAlignment.Center;
//            strfmt_Center.Alignment = StringAlignment.Center;

//            strfmt_Right.LineAlignment = StringAlignment.Far;
//            strfmt_Right.Alignment = StringAlignment.Far;

//            y_p += 20;
//            Rectangle strDesk = new Rectangle(5, y_p, 250, 22);
//            data = Common.clsLanguages.GetResource("Desk_", Language) + " " + rcAll.order.DeskId;
//            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strDesk, strfmt_Left);

//            Rectangle strDate = new Rectangle(5, y_p + 22, 250, 22);
//            data = Common.clsLanguages.GetResource("ReceiptDate", Language) + " " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
//            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strDate, strfmt_Left);

//            Rectangle strReceiptId1 = new Rectangle(180, y_p, 100, 30);
//            data = Int32.Parse(rcAll.order.OrderId.Substring(10)).ToString();
//            e.Graphics.DrawString(data, new Font("Arial", 16, FontStyle.Bold), Brushes.Black, strReceiptId1, strfmt_Right);

//            y_p += 45;
//            Rectangle strReceiptId = new Rectangle(5, y_p, 250, 22);
//            data = Common.clsLanguages.GetResource("CancelOrder", Language) + " : 10#" + rcAll.order.OrderId.Substring(10);
//            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strReceiptId, strfmt_Left);

//            y_p += 23;
//            Rectangle strHD_STT = new Rectangle(5, y_p, 63, 22); // đổi , 30-28
//            data = Common.clsLanguages.GetResource("No", Language);
//            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strHD_STT, strfmt_Left);

//            Rectangle strHD_SP = new Rectangle(35, y_p, 220, 22);
//            data = Common.clsLanguages.GetResource("Product", Language);//"Sản phẩm ";  
//            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strHD_SP, strfmt_Left);

//            Rectangle strHD_SL = new Rectangle(260, y_p, 45, 22);
//            data = Common.clsLanguages.GetResource("ReceiptProQty", Language);//"SL";
//            e.Graphics.DrawString(data, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, strHD_SL, strfmt_Left);
//            y_p += 30;
//            FontStyle ft = FontStyle.Strikeout;
//            for (int i = 0; i < list.Length; i++)
//            {
//                //Headers
//                Rectangle strCT_STT = new Rectangle(5, y_p, 30, 22);
//                data = (i + 1).ToString();
//                e.Graphics.DrawString(data, new Font("Arial", 14, ft), Brushes.Black, strCT_STT, strfmt_Left);

//                data = list[i][1];
//                int heightl = Int32.Parse(Math.Ceiling((double)data.Length / 17).ToString()) * 18 + 5;
//                Rectangle strCT_SP = new Rectangle(35, y_p, 220, heightl);
//                e.Graphics.DrawString(data, new Font("Arial", 14, ft), Brushes.Black, strCT_SP, strfmt_Left);

//                Rectangle strCT_SL = new Rectangle(260, y_p, 45, 22);

//                data = list[i][2];
//                e.Graphics.DrawString(data, new Font("Arial", 14, ft), Brushes.Black, strCT_SL, strfmt_Left);


//                data = list[i][3];
//                int height = 0;
//                if (!string.IsNullOrEmpty(data))
//                {
//                    height = Int32.Parse(Math.Ceiling((double)data.Length / 17).ToString()) * 18 + 4;
//                    Rectangle notes = new Rectangle(5, y_p + heightl, 280, height);
//                    e.Graphics.DrawString(data, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, notes, strfmt_Left);
//                }

//                Rectangle RecLineCT = new Rectangle(5, y_p + heightl + height, 280, 22);
//                data = "-----------------------------------------------------------------------";
//                e.Graphics.DrawString(data, new Font("Arial", 14, ft), Brushes.Black, RecLineCT, strfmt_Left);


//                y_p = y_p + height + heightl + 20;
//            }
//        }

//        #region Lấy tất cả thông tin của 1 HD
//        public static OrderAll GetOrderInfo(string OrderID)
//        {
//            OrderAll res = new OrderAll();

//            DataSet ds = new DataSet();
//            string[][] param ={
//                new string[] {"p_OrderId", OrderID},
//            };
//            string errorString = "";
//            string cnStr = GetConnectionString();
//            DataStoreProcQuery_Param(cnStr, "spSelect_Order_AllInfo", ref ds, param, ref errorString);

//            if (string.IsNullOrEmpty(errorString) && ds.Tables.Count > 0)
//            {
//                if (ds.Tables[0].Rows.Count > 0)
//                {
//                    DataTable dt = ds.Tables[0];
//                    DataRow dr = dt.Rows[0];
//                    Orders rc = new Orders();
//                    rc.OrderId = dr["OrderId"].ToString();
//                    rc.CreatedBy = dr["CreatedBy"].ToString();
//                    rc.CreateDate = dr["CreatedDate"].ToString();
//                    rc.TotalAmountBeforeTax = double.Parse(dr["TotalAmountBeforeTax"].ToString()).ToString("0,0");
//                    rc.TotalTax = double.Parse(dr["TotalTax"].ToString()).ToString("0,0");
//                    rc.TotalMoney = double.Parse(dr["TotalMoney"].ToString()).ToString("0,0");
//                    rc.DisCountAmount = double.Parse(dr["DisCountAmount"].ToString()).ToString("0,0");

//                    rc.DeskId = dr["DeskId"].ToString();

//                    res.order = rc;
//                }
//                /////
//                if (ds.Tables[1].Rows.Count > 0)
//                {
//                    DataTable dt = ds.Tables[1];
//                    List<OrderDetails> lst = new List<OrderDetails>();
//                    foreach (DataRow dr in dt.Rows)
//                    {
//                        OrderDetails rc = new OrderDetails();
//                        rc.OrderId = dr["OrderId"].ToString();
//                        rc.ProductId = dr["ProductId"].ToString();
//                        rc.ProductName = GetProductName(dr["ProductId"].ToString());
//                        rc.Qty = double.Parse(dr["Qty"].ToString()).ToString("0,0");
//                        rc.Price = double.Parse(dr["Price"].ToString()).ToString("0,0");
//                        rc.AmmountBeforeTax = double.Parse(dr["AmmountBeforeTax"].ToString()).ToString("0,0");
//                        rc.TaxAmmount = double.Parse(dr["TaxAmmount"].ToString()).ToString("0,0");
//                        rc.TotalAmount = double.Parse(dr["TotalAmmount"].ToString()).ToString("0,0");
//                        rc.Status = dr["Status"].ToString();
//                        rc.Note = dr["Note"].ToString();

//                        lst.Add(rc);
//                    }
//                    res.lst_Detail = lst;
//                }



//            }
//            else
//            {
//                res = null;
//            }

//            return res;
//        }

//        #endregion

//        #endregion

//        #region Utility

//        public static string GetInfoFromXml(string sql, string file)
//        {
//            string path = HttpContext.Current.Server.MapPath("~");
//            XmlDocument doc = new XmlDocument();
//            //doc.Load("..\\..\\xml\\" + file + ".xml");
//            doc.Load(path + "\\bin\\" + file + ".xml");
//            XmlNodeList nodeLst = doc.GetElementsByTagName(sql);
//            return nodeLst.Item(0).InnerText;
//        }

//        public static string sql = "SELECT DATE_FORMAT(NOW(),'%d/%m/%Y')";
//        public static string Ngayhienhanh_Server
//        {
//            get
//            {
//                return gets(sql).Rows[0][0].ToString();
//            }
//        }

//        public static void GetCurrentDateServer(ref string s)
//        {
//            s = Ngayhienhanh_Server;
//        }

//        //Get CURDATE()
//        public static string sql1 = "SELECT CURDATE()";
//        public static string Ngayhienhanh_Server1
//        {
//            get
//            {
//                return gets(sql1).Rows[0][0].ToString();
//            }
//        }

//        //get datatable
//        public static DataTable gets(string sql)
//        {
//            DataSet ds;
//            MySqlConnection con = new MySqlConnection(GetConnectionString());
//            ds = new DataSet();
//            MySqlDataAdapter cmd = new MySqlDataAdapter(sql, con);
//            cmd.Fill(ds, "table");
//            return ds.Tables["table"];
//        }

//        /// <summary>
//        /// Convert 1 chuổi ngày kiểu String thành kiểu Date
//        /// </summary>
//        public static System.DateTime StringToDateTime(string s)
//        {
//            string[] format1 = { "dd/MM/yyyy" },
//                format2 = { "yyyy-MM-dd HH:mm:ss" };
//            return System.DateTime.ParseExact(s.ToString(), (s.Length == 10) ? format1 : format2, System.Globalization.DateTimeFormatInfo.CurrentInfo, System.Globalization.DateTimeStyles.None);
//        }
//        public static System.DateTime StringToDateTime1(string s)
//        {
//            string[] format1 = { System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern },
//                format2 = { System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern };

//            return System.DateTime.ParseExact(s.ToString(), (s.Length == 10) ? format1 : format2, System.Globalization.DateTimeFormatInfo.CurrentInfo, System.Globalization.DateTimeStyles.None);
//        }

//        /// <summary>
//        /// Convert 1 chuổi ngày kiểu Date thành kiểu String
//        /// </summary>
//        public static string DateToString(string format, System.DateTime date)
//        {
//            if (date.Equals(null)) return "";
//            else return date.ToString(format, System.Globalization.DateTimeFormatInfo.CurrentInfo);
//        }

//        //Score
//        public static string GetValueScoreEx()
//        {
//            string sFile = "settingcode";
//            if (GetInfoFromXml("ScoreValue", sFile) != "") return GetInfoFromXml("ScoreValue", sFile);
//            else return "0";
//        }

//        public static string GetPriceScore()
//        {
//            string sFile = "settingcode";
//            if (GetInfoFromXml("PriceScore", sFile) != "") return GetInfoFromXml("PriceScore", sFile);
//            else return "0";
//        }

//        #endregion
//    }
//}