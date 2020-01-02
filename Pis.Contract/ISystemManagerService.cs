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

        [POST("/api/SystemManager/TestJWT")]
        Call TestJWT([Query]string Token);
    }
}
