using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.HDT.Common.Data.Domain
{
    public class Outputdetails
    {
        public string OutputId { get; set; }
        public string IngredientId { get; set; }
        public string IngredientQty { get; set; }
        public string IngredientPrice { get; set; }
        public string IngredientVat { get; set; }
    }
}
