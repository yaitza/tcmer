using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCMER.Model
{
    class ExecuteResult
    {
        /// <summary>
        /// 执行结果
        /// </summary>
        public ExecuteResultType result { get; set; }

        /// <summary>
        /// 执行人
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
