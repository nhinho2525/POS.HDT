﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.HDT.Common.Data.Domain
{
    public class Inputs
    {
        public string InputId { get; set; }
        public string Ballot { get; set; }
        public string BallotDate { get; set; }
        public string RecieptId { get; set; }
        public string RecieptDate { get; set; }
        public string SupplierId { get; set; }
        public string StoreId { get; set; }
        public string Paid { get; set; }
        public string InputDiscount { get; set; }
        public string InputNote { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
    }
}
