using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.HDT.Common.Data.Domain
{
    public class OrderDetails
    {
        public string ODID { get; set; }
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Qty { get; set; }
        public string Price { get; set; }
        public string CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string IsPrint { get; set; }
        public string Status { get; set; }
        // 0: chưa thanh toán
        // 1: đã ra hóa đơn ( tất cả detail )
        // 2: Cancel
        // 3: Update
        public string Note { get; set; }

        public string AmmountBeforeTax { get; set; }
        public string TaxAmmount { get; set; }
        public string TotalAmount { get; set; }

        public string IsBuffet { get; set; }
    }
}
