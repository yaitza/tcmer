using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCMER.Model
{
    public class TestStepModel
    {
        /// <summary>
        /// 测试步骤ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 测试步骤顺序
        /// </summary>
        public int StepOrder { get; set; }

        /// <summary>
        /// 测试步骤动作
        /// </summary>
        public string StepActions { get; set; }

        /// <summary>
        /// 测试步骤执行结果
        /// </summary>
        public string StepResults { get; set; }

        /// <summary>
        /// 测试步骤创建人
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 测试步骤创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 测试步骤更新者
        /// </summary>
        public string UpdateBy { get; set; }

        /// <summary>
        /// 测试步骤更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

    }
}
