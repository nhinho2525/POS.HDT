using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.HDT.Common.Data.Domain
{
    public class ReceiptMember
    {
        public string ReceiptId { get; set; }
        public string MemberId { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        //extend
        public string MemberCode { get; set; }
        public string ObjectId { get; set; }
        public string NumberOfVissits { get; set; }
        public string LastestDate { get; set; }
        public string MemberType { get; set; }
        public string MemberScore { get; set; }

        // object
        public string ObjectGroup { get; set; }
        public string ObjectType { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string FullName { get; set; }
        public string TemAdd { get; set; }
    }
}
