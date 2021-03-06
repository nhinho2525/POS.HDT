﻿using System.Collections.Generic;

namespace POS.HDT.Common.Data.Domain
{
    public class Receipts
    {
        public string ReceiptId { get; set; }
        public string TableId { get; set; }
        public string ReceiptNote { get; set; }
        public string ReceiptStatus { get; set; }
        // 0: chưa thanh toán
        // 1: đã ra hóa đơn ( tất cả detail )
        public string ReceiptTax { get; set; }
        public string ReceiptMoney { get; set; }
        public string ReceiptDiscount { get; set; }
        public string ReceiptPercentDiscount { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }

    public class ReceiptsCard
    {
        public string id { get; set; }
        public string ReceiptId { get; set; }
        public string CardNo { get; set; }
        public string CardHolderName { get; set; }
        public string ExpiredDate { get; set; }

        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CardType { get; set; }
        public string Bank { get; set; }
        public string TotalAmount { get; set; }

        public string TerminalId { get; set; }
        public string MerchantId { get; set; }
        public string TransType { get; set; }
        public string BatchNo { get; set; }
        public string TraceNo { get; set; }
        public string RefNo { get; set; }
        public string AppCode { get; set; }


    }

    public class ReceiptsAll
    {
        public Receipts receipt { get; set; }
        public List<ReceiptDetails> lst_Detail { get; set; }
        public List<ReceiptsCard> lst_card { get; set; }
    }
}
