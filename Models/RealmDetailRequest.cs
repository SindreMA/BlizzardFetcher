using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlizzardFetcher.Models
{
    public class RealmDetailRequest
    {
        public _Links _links { get; set; }
        public int id { get; set; }
        public Region region { get; set; }
        public Connected_Realm connected_realm { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string locale { get; set; }
        public string timezone { get; set; }
        public Type type { get; set; }
        public bool is_tournament { get; set; }
        public string slug { get; set; }

        public class _Links
        {
            public Self self { get; set; }
        }

        public class Self
        {
            public string href { get; set; }
        }

        public class Region
        {
            public Key key { get; set; }
            public string name { get; set; }
            public int id { get; set; }
        }

        public class Key
        {
            public string href { get; set; }
        }

        public class Connected_Realm
        {
            public string href { get; set; }
        }

        public class Type
        {
            public string type { get; set; }
            public string name { get; set; }
        }

    }
}
