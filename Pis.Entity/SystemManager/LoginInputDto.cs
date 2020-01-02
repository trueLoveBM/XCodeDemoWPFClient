using System;
using System.Collections.Generic;
using System.Text;

namespace Pis.Entity.SystemManager
{
    /// <summary>
    /// 登录的实体
    /// </summary>
    public class LoginInputDto
    {
        /// <summary>员工工号</summary>
        public string EmployeeCode { get; set; }

        /// <summary>员工姓名</summary>
        public string EmployeeName { get; set; }

        /// <summary>登录密码</summary>
        public string Password { get; set; }
    }
}
