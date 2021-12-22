using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlizzardFetcher.Models
{
    public class RealmIndexRequest
    {
        public _Links _links { get; set; }
        public Realm[] realms { get; set; }
        public class _Links
        {
            public Self self { get; set; }
        }

        public class Self
        {
            public string href { get; set; }
        }

        public class Realm
        {
            public Key key { get; set; }
            public string name { get; set; }
            public int id { get; set; }
            public string slug { get; set; }
        }

        public class Key
        {
            public string href { get; set; }
        }
    }
}
