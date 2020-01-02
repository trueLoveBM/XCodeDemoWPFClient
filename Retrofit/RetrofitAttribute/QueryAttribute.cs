using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrofitFrame
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class QueryAttribute:System.Attribute
    {
        public string ParamName { get; set; }

        public QueryAttribute(string ParamName)
        {
            this.ParamName = ParamName;
        }

        public QueryAttribute() {
        }
    }
}
