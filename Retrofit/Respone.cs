using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RetrofitFrame
{
    public class Respone<T>
    {
        public HttpStatusCode Code { get; set; }

        public bool IsSuccess { get; set; }

        public string Msg { get; set; }

        public T  Data { get; set; }

    }
}
