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
        public string Id;

        /// <summary>
        /// 测试用例名称
        /// </summary>
        public string Name;

        /// <summary>
        /// 测试用例摘要
        /// </summary>
        public string Summary;

        /// <summary>
        /// 测试用例前提
        /// </summary>
        public string Precondition;

        /// <summary>
        /// 测试用例重要性
        /// </summary>
        public ImportanceLevel Importance;

        /// <summary>
        /// 测试用例类型
        /// </summary>
        public TestCaseType TestCaseType;

        /// <summary>
        /// 测试用例步骤
        /// </summary>
        public ObservableCollection<TestStepModel> TestSteps;

        /// <summary>
        /// 测试用例创建人
        /// </summary>
        public string CreateBy;

        /// <summary>
        /// 测试用例创建时间
        /// </summary>
        public DateTime CreateTime;

        /// <summary>
        /// 测试用例更新者
        /// </summary>
        public string UpdateBy;

        /// <summary>
        /// 测试用例更新时间
        /// </summary>
        public DateTime UpdateTime;


    }


}
