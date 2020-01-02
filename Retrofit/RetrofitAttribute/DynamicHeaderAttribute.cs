using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrofitFrame.RetrofitAttribute
{
    /// <summary>
    /// 这种Header用于参数上的多Header添加
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter,AllowMultiple =true)]
    public class DynamicHeaderAttribute : Attribute
    {
        public string Header { get; set; }

        public DynamicHeaderAttribute(string header)
        {
            this.Header = header;
        }

        public DynamicHeaderAttribute()
        {

        }

    }
}
