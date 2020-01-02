using RetrofitFrame.RetrofitAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrofitFrame
{
    public class ServiceRquestInfo
    {

        public bool  IsUploadFile { get; set; }

        public string FilePath { get; set; }

        public HttpMethod HttpMethod { get; set; }

        public List<String> Header { get; set; }

        public string Url { get; set; }

        public Dictionary<string, string> Param { get; set; }

        public string Body { get; set; }

        public ServiceRquestInfo()
        {
            Param = new Dictionary<string, string>();
            Header = new List<string>();
        }
    }
}
