using System;
using System.Collections.Generic;
using System.Text;

namespace Pis.Entity.SystemManager
{
    public class LoginOutputDto
    {
        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }

        /// <summary>主键</summary>
        public string EmpId { get; set; }

        /// <summary>员工工号</summary>
        public string EmployeeCode { get; set; }

        /// <summary>员工姓名</summary>
        public string EmployeeName { get; set; }

        /// <summary>快捷PIN码</summary>
        public string PinCode { get; set; }

        /// <summary>所属机构</summary>
        public string OrganizationName { get; set; }

        /// <summary>所属机构Id</summary>
        public string OrganizationId { get; set; }

        /// <summary>工作科室</summary>
        public string WorkDeptId { get; set; }

        /// <summary>职务</summary>
        public string WorkDuty { get; set; }

        /// <summary>专业职称编码</summary>
        public string CertificateCode { get; set; }
    }
}
