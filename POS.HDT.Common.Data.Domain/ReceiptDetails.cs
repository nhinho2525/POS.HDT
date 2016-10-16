using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.HDT.Common.Data.Domain
{
    public class ReceiptDetails
    {
        public string ReceiptId { get; set; }
        public string ProductId { get; set; }
        public string PromotionId { get; set; }
        public string ProductName { get; set; }
        public string Qty { get; set; }
        public string Price { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string Status { get; set; }
        public string TotalAmountBeforeTax { get; set; }
        public string TaxAmount { get; set; }
        public string TotalAmount { get; set; }
        public string RefOrderId { get; set; }
    }
}
