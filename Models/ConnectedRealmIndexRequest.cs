using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlizzardFetcher.Models
{
    public class ConnectedRealmIndexRequest
    {
        public _Links _links { get; set; }
        public Connected_Realms[] connected_realms { get; set; }
        public class _Links
        {
            public Self self { get; set; }
        }

        public class Self
        {
            public string href { get; set; }
        }

        public class Connected_Realms
        {
            public string href { get; set; }
        }

    }
}
