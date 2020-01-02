using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrofitFrame
{
    [AttributeUsage(AttributeTargets.Method)]
    public class GETAttribute:System.Attribute
    {
        public string Url { get; set; }

        public GETAttribute(string _url)
        {
            this.Url = _url;
        }

    }
}
