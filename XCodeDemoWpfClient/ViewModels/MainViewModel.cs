using Pis.Entity;
using RetrofitFrame;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using XCodeDemoWpfClient.Utils;

namespace XCodeDemoWpfClient.ViewModels
{
    [Export(typeof(MainViewModel))]
    public class MainViewModel : Caliburn.Micro.Screen
    {
        #region 界面调用方法 
        /// <summary>
        /// 测试token
        /// </summary>
        public void TestToken()
        {
            Call call = RetrofitHelper.SystemManagerService.TestJWT(ClientCache.Instance.GetClientInfo().Token);
            Respone<CommonResponse<string>> resp = call.Execute<CommonResponse<string>>();
            if (resp.IsSuccess)
            {
                MessageBox.Show("登录成功");
            }
            else
                MessageBox.Show(resp.Msg);
        }
        #endregion
    }
}
