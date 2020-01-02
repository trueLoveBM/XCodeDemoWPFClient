using Pis.Contract;
using RetrofitFrame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCodeDemoWpfClient.Utils
{
    public class RetrofitHelper
    {
        private static Retrofit _Retrofit;

        static RetrofitHelper()
        {
            _Retrofit = new Retrofit.Builder()
           .setUrl("https://localhost:44322")
           .build();
        }


        /// <summary>
        /// 给Retrofit添加全局Header，添加后之后的接口一直会被调用
        /// </summary>
        public static void AddHeader(string header)
        {
            _Retrofit.Header.Add(header);
        }

        private static ISystemManagerService _systemManagerService;
        public static ISystemManagerService SystemManagerService
        {
            get
            {
                if (_systemManagerService == null)
                {
                    _systemManagerService = _Retrofit.create<ISystemManagerService>();
                }
                return _systemManagerService;
            }
        }

    }
}
