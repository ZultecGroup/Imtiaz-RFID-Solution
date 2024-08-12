using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZulLabel
{
    class RequestParameter
    {
        public class bulkPrint
        {
            public string client { get; set; }
            public string reference { get; set; }
            public Supplier supplier { get; set; }
            public Dictionary<string, Article> articles { get; set; }
        }
        public class Supplier
        {
            public string id { get; set; }
            public string site_id { get; set; }
        }
        public class Article
        {
            public int count { get; set; }
        }


    }
}
