using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCMER.Model
{
    public enum NodeType
    {
        [Description("测试套")]
        TestSuite,

        [Description("测试用例")]
        TestCase
    }
}
