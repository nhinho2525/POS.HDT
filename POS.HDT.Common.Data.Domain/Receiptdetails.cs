namespace POS.HDT.Common.Data.Domain
{
    public class ReceiptDetails
    {
        public string ReceiptId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ReceiptDetailQty { get; set; }
        public string ReceiptDetailPrice { get; set; }
        public string ReceiptDetailNote { get; set; }
        public string ReceiptDetailIsPrint { get; set; }
        public string ReceiptDetailStatus { get; set; }
        // 0: chưa thanh toán
        // 1: đã ra hóa đơn ( tất cả detail )
        // 2: Cancel
        // 3: Update
        public string ReceiptDetailMoney { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
