using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.HDT.Common.Data.Domain
{
    public class OrderDetails
    {
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string OrderDetailQty { get; set; }
        public string OrderDetailPrice { get; set; }
        public string OrderDetailNote { get; set; }
        public string OrderDetailIsPrint { get; set; }
        public string OrderDetailStatus { get; set; }
        // 0: chưa thanh toán
        // 1: đã ra hóa đơn ( tất cả detail )
        // 2: Cancel
        // 3: Update
        public string OrderDetailMoney { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
