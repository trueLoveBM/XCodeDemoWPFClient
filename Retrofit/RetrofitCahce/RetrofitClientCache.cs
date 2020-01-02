using RetrofitFrame.RetrofitException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrofitFrame.RetrofitCahce
{
    public class RetrofitClientCache
    {

        private RetrofitClientCache() {
            RetrofitCache = new Dictionary<Retrofit, List<int>>();
        }


        public Retrofit GetClient(int ProxyInterfaceHashCode)
        {
            Retrofit client = null;
            foreach (var item in RetrofitCache.Keys)
            {
                if (RetrofitCache[item].Contains(ProxyInterfaceHashCode))
                {
                    client = item;
                    break;
                }
            }
            if (client == null)
            {
                throw new ClientNotFoundException("未找到相关Client信息");
            }
            return client;

        }


        public void AddClient(Retrofit client)
        {
            if (!RetrofitCache.Keys.Contains(client)) {
                RetrofitCache.Add(client, new List<int>());
            }
        }

        public void AddProxy(Retrofit client, int ProxyInterfaceHashCode)
        {
            if (!RetrofitCache.Keys.Contains(client))
            {
                throw new UnKnownClientException("未知的Retrofit Client对象");
            }
            List<int> hashCodeList = RetrofitCache[client];
            hashCodeList.Add(ProxyInterfaceHashCode);
        }

        public static readonly RetrofitClientCache Instance = new RetrofitClientCache();

        public Dictionary<Retrofit, List<int>> RetrofitCache;
    }
}
