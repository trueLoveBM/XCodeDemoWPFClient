using NewLife.Log;
using Pis.Entity;
using Pis.Entity.SystemManager;
using RetrofitFrame;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using XCodeDemoWpfClient.Utils;
using static Pis.Entity.Enumeration;

namespace XCodeDemoWpfClient.ViewModels
{
    [Export(typeof(MainViewModel))]
    public class MainViewModel : Caliburn.Micro.Screen
    {
        public MainViewModel()
        {


        }

        #region 界面绑定属性

        #region 界面菜单

        private ObservableCollection<EmMenuOutPutDto> _MenuList;

        /// <summary>
        /// 菜单列表
        /// </summary>
        public ObservableCollection<EmMenuOutPutDto> MenuList
        {
            get { return _MenuList; }
            set
            {
                _MenuList = value;
                NotifyOfPropertyChange(nameof(MenuList));
            }
        }


        #region 接收登记模块

        private ObservableCollection<EmMenuOutPutDto> _SignMenuList;

        /// <summary>
        /// 接收登记模块菜单列表
        /// </summary>
        public ObservableCollection<EmMenuOutPutDto> SignMenuList
        {
            get { return _SignMenuList; }
            set
            {
                _SignMenuList = value;
                NotifyOfPropertyChange(nameof(SignMenuList));
            }
        }

        #endregion
        #endregion



        #endregion


        #region View的生命周期
        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            //加载菜单列表
            LoadMenu();
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 加载菜单列表
        /// </summary>
        public void LoadMenu()
        {
            Call call = RetrofitHelper.SystemManagerService.GetEmployeeMenuList(ClientCache.Instance.GetClientInfo().EmpId);
            Respone<CommonResponse<List<EmMenuOutPutDto>>> resp = call.Execute<CommonResponse<List<EmMenuOutPutDto>>>();
            if (resp.IsSuccess)
            {
                MenuList = new ObservableCollection<EmMenuOutPutDto>(resp.Data.Data);
                //接收登记模块菜单
                SignMenuList = new ObservableCollection<EmMenuOutPutDto>(MenuList.FindAll(m => m.Module == EnSysModule.接收与登记.ToInt().ToString()));

            }
            else
                XTrace.Log.Write(LogLevel.Error, "加载菜单列表失败", null);
        }





        #endregion

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
