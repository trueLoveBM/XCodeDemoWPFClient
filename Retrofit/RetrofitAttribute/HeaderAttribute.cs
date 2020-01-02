using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrofitFrame.RetrofitAttribute
{
    /// <summary>
    /// 这种Header用于方法上的多Header添加
    /// </summary>
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =true)]
    public class HeaderAttribute:Attribute
    {
        public string Header { get; set; }

        public HeaderAttribute(string header)
        {
            this.Header = header;
        }

    }
}
