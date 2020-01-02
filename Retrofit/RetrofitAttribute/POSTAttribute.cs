using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrofitFrame
{
    [AttributeUsage(AttributeTargets.Method)]
    public class POSTAttribute:System.Attribute
    {
        public string Url { get; set; }

        public POSTAttribute(string _url)
        {
            this.Url = _url;
        }

    }
}
