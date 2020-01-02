using NewLife.Caching;
using Pis.Entity.SystemManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCodeDemoWpfClient.Utils
{
    class ClientCache
    {

        #region 缓存
        private ICache _Cache;
        #endregion

        #region 单例实现
        private static Lazy<ClientCache> lazy = new Lazy<ClientCache>(() => { return new ClientCache(); });

        public static ClientCache Instance { get { return lazy.Value; } }

        private ClientCache()
        {
            _Cache = Cache.Default;
            _Cache.Expire = 0;//永不过期
        }
        #endregion



        #region 公开方法

        /// <summary>
        /// 保存客户端
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool SaveClientInfo(LoginOutputDto dto)
        {
            if (_Cache.Keys.Contains("ClientInfo"))
                _Cache.Remove("ClientInfo");
            return _Cache.Add("ClientInfo", dto);
        }

        public LoginOutputDto GetClientInfo()
        {
            if (!_Cache.Keys.Contains("ClientInfo"))
                return null;
            return _Cache.Get<LoginOutputDto>("ClientInfo");
        }

        #endregion
    }
}
