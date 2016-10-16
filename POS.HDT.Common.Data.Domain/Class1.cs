using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.HDT.Common.Data.Domain
{
    // ECTD_METADATA
    public class EctdMetadata
    {
        public long MetadataKey { get; set; } // MetadataKey (Primary key)
        public long NodeGroupKey { get; set; } // NodeGroupKey
        public string MetadataName { get; set; } // MetadataName (length: 200)
        public string MetadataValue { get; set; } // MetadataValue (length: 4000)
        public System.Nullable<double> SortOrder { get; set; } // SortOrder
        public System.Nullable<int> IsHidden { get; set; } // IsHidden
        public System.Nullable<System.DateTime> LastUpdate { get; set; } // LastUpdate

        // Foreign keys
        //public virtual EctdNodegroup EctdNodegroup { get; set; } // ECTD_METADATA_FK

        public EctdMetadata()
        {
            IsHidden = 0;
            LastUpdate = System.DateTime.Now;
        }
    }
}
