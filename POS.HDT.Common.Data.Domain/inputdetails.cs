using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.HDT.Common.Data.Domain
{
    public class inputdetails
    {
        public string InputId { get; set; }
        public string IngredientId { get; set; }
        public string IngredientExpDate { get; set; }
        public string IngredientQty { get; set; }
        public string IngredientPrice { get; set; }
        public string IngredientVat { get; set; }
        public string IngredientDiscount { get; set; }
    }
}
