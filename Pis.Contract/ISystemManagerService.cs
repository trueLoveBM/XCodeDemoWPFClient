using Pis.Entity;
using Pis.Entity.SystemManager;
using RetrofitFrame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pis.Contract
{
    public interface ISystemManagerService
    {
        /// <summary>
        /// 用户登录接口
        /// </summary>
        /// <param name="dto">用户登录信息</param>
        /// <returns>用户登录成功后返回实体</returns>
        [POST("/api/SystemManager/SignIn")]
        Call SignIn([Body]LoginInputDto dto);

        /// <summary>
        /// 测试Token
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [POST("/api/SystemManager/TestJWT")]
        Call TestJWT([Query]string Token);


        /// <summary>
        /// 获取指定雇员的权限
        /// </summary>
        /// <param name="Empid">指定雇员id</param>
        /// <returns>用户权限列表</returns>
        [POST("/api/SystemManager/GetEmployeePermissionList")]
        Call GetEmployeePermissionList([Query]string Empid);


        /// <summary>
        /// 获取指定雇员可见的菜单
        /// </summary>
        /// <param name="Empid">指定雇员id</param>
        /// <returns>用户可见列表菜单</returns>
        [POST("/api/SystemManager/GetEmployeeMenuList")]
        Call GetEmployeeMenuList([Query]string Empid);
    }
}
