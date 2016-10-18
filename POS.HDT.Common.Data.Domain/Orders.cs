using System.Collections.Generic;

namespace POS.HDT.Common.Data.Domain
{
    public class Orders
    {
        public string OrderId { get; set; }
        public string TableId { get; set; }
        public string OrderNote { get; set; }
        public string OrderStatus { get; set; }
        // 0: chưa thanh toán
        // 1: đã ra hóa đơn ( tất cả detail )
        public string OrderTax { get; set; }
        public string OrderMoney { get; set; }
        public string OrderDiscount { get; set; }
        public string OrderPercentDiscount { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }

    public class OrderAll
    {
        public Orders order { get; set; }
        public List<OrderDetails> lst_Detail { get; set; }
    }
}
