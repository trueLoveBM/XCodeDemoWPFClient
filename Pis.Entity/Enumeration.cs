using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pis.Entity
{
    /// <summary>
    /// 程序相关的枚举
    /// </summary>
    public class Enumeration
    {
        /// <summary>
        /// 系统模块
        /// </summary>
        public enum EnSysModule
        {
            接收与登记 = 101,
            取材管理 = 102,
            技师管理 = 103,
            工作移交 = 104,
            病理诊断 = 105,
            报告发放 = 106,
            档案管理 = 107,
            病理质控 = 108,
            行政管理 = 109,
            病理查询 = 110,
            病理统计 = 111
        }
    }
}
