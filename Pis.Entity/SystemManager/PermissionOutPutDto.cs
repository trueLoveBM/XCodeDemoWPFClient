using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pis.Entity.SystemManager
{
    /// <summary>
    /// 当前用户的权限数据实体
    /// </summary>
    public class PermissionOutPutDto
    {
        /// <summary>权限ID</summary>
        public string PermissionId { get; set; }

        /// <summary>权限名</summary>
        public string PermissionName { get; set; }

        /// <summary>权限编码</summary>
        public string PermissionCode { get; set; }

        /// <summary>权限类型(1:流程控制权限)</summary>
        public int PermissionType { get; set; }

        /// <summary>父节点ID</summary>
        public string ParentId { get; set; }

        /// <summary>排序序号</summary
        public int SortNo { get; set; }
    }
}
