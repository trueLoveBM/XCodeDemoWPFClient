using Caliburn.Micro;
using Pis.Contract;
using Pis.Entity;
using Pis.Entity.SystemManager;
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
    [Export(typeof(LoginViewModel))]
    public class LoginViewModel : Caliburn.Micro.Screen
    {

        IWindowManager _WindowManager;

        [ImportingConstructor]
        public LoginViewModel(IWindowManager WindowManager, IEventAggregator eventAggregator)
        {
            _WindowManager = WindowManager;
        }

        #region 用户名
        private string _userName = "admin";

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyOfPropertyChange(nameof(UserName));
            }
        }
        #endregion

        #region 密码
        private string _passWord = "123456";

        public string PassWord
        {
            get { return _passWord; }
            set
            {
                _passWord = value;
                NotifyOfPropertyChange(nameof(PassWord));
            }
        }

        #endregion

        /// <summary>
        /// 开始登陆
        /// </summary>
        public void StartLogin(string UserName, string PassWord)
        {
            PassWord = "123456";

            LoginInputDto dto = new LoginInputDto();
            dto.EmployeeCode = UserName;
            dto.EmployeeName = UserName;
            dto.Password = PassWord;

            Call call = RetrofitHelper.SystemManagerService.SignIn(dto);

            Respone<CommonResponse<LoginOutputDto>> resp = call.Execute<CommonResponse<LoginOutputDto>>();
            if (resp.IsSuccess)
            {
                CommonResponse<LoginOutputDto> data = resp.Data;

                //登录成功，将Token保存至Retrofit的header中，以后每次调用进行添加
                RetrofitHelper.AddHeader($"Authenticate:{data.Data.Token}");

                //将用户信息保存至缓存中
                ClientCache.Instance.SaveClientInfo(data.Data);

                //启动主页面
                _WindowManager.ShowWindow(new MainViewModel());

                //关闭此页面
                this.TryClose();
            }
            else
            {
                MessageBox.Show(resp.Msg);
            }
        }

    }
}
