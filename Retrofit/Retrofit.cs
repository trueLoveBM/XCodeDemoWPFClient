using Castle.DynamicProxy;
using RetrofitFrame.RetrofitCahce;
using RetrofitFrame.RetrofitException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RetrofitFrame
{
    public class Retrofit
    {

        public string Url { set; get; }


        public List<String> Header { set; get; }


        public string Accept { get; set; } = "text/html, application/xhtml+xml, */*";

        public string ContentType { get; set; } = "application/json";

        public T create<T>() where T : class
        {
            ProxyGenerator generator = new ProxyGenerator();
            //将当前所产生的接口代理，存入Cache中
            T t = generator.CreateInterfaceProxyWithoutTarget<T>(new ServiceInterceptor());
            RetrofitClientCache.Instance.AddProxy(this, t.GetHashCode());
            return t;
        }

        public static Builder builder()
        {
            return new Builder();
        }


        public sealed class Builder
        {
            private string _url;
            private List<string> _header;

            public Builder()
            {
                _header = new List<string>();
            }

            public Builder setUrl(string url)
            {
                this._url = url;
                return this;
            }

            public Builder AddHeader(string headerStr)
            {
                _header.Add(headerStr);
                return this;
            }

            public Retrofit build()
            {
                Retrofit retrofit = new Retrofit();
                if (string.IsNullOrEmpty(_url))
                    throw new UrlNotFoundException();
                retrofit.Url = _url;
                retrofit.Header = _header;
                //保存当前的retrofit对象
                RetrofitClientCache.Instance.AddClient(retrofit);
                return retrofit;
            }
        }


    }

}
