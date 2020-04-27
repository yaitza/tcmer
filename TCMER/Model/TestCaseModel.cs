using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCMER.Model
{
    class TestCaseModel
    {
        /// <summary>
        /// 测试用例ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 排序ID
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 测试用例名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 测试用例摘要
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 测试用例前提
        /// </summary>
        public string Precondition { get; set; }

        /// <summary>
        /// 测试用例重要性
        /// </summary>
        public ImportanceLevel Importance { get; set; }

        /// <summary>
        /// 测试用例类型
        /// </summary>
        public TestCaseType TestCaseType { get; set; }

        /// <summary>
        /// 测试用例执行结果
        /// </summary>
        public ExecuteResultType Result { get; set; }

        /// <summary>
        /// 测试用例步骤
        /// </summary>
        public ObservableCollection<TestStepModel> TestSteps { get; set; }

        /// <summary>
        /// 测试用例创建人
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 测试用例创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 测试用例更新者
        /// </summary>
        public string UpdateBy { get; set; }

        /// <summary>
        /// 测试用例更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }


    }


}
