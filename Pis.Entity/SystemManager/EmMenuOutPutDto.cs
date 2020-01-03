using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pis.Entity.SystemManager
{
    public class EmMenuOutPutDto
    {
        /// <summary>主键</summary>
        public string MenuId { get; set; }

        /// <summary>窗体配置Id，空时不加载界面</summary>
        public string ConfigId { get; set; }

        /// <summary>菜单名称</summary>
        public string MenuName { get; set; }

        /// <summary>所属模块编码</summary>
        public string Module { get; set; }

        /// <summary>显示类型 Hide-1\Show0\ShowDialog1</summary>
        public int ShowType { get; set; }

        /// <summary>序号</summary>
        public int SortNO { get; set; }

        /// <summary>上级菜单Id</summary>
        public string ParentId { get; set; }

        /// <summary>创建人</summary>
        public string Creater { get; set; }

        /// <summary>创建时间</summary>
        public string CreateTime { get; set; }

        /// <summary>合法标记(正常0/删除-1)</summary>
        public int FlagValid { get; set; }

        /// <summary>检查类型Id</summary>
        public string TbId { get; set; }
    }
}
