using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.HDT.Common.Data.Domain
{
    public class ReceiptDetails
    {
        public string RecieptId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string RecieptDetailQty { get; set; }
        public string RecieptDetailPrice { get; set; }
        public string RecieptDetailNote { get; set; }
        public string RecieptDetailIsPrint { get; set; }
        public string RecieptDetailStatus { get; set; }
        // 0: chưa thanh toán
        // 1: đã ra hóa đơn ( tất cả detail )
        // 2: Cancel
        // 3: Update
        public string RecieptDetailMoney { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
