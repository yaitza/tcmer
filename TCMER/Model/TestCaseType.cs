using System.ComponentModel;

namespace TCMER.Model
{
    public enum TestCaseType
    {
        [Description("手动用例")]
        Manual = 0,

        [Description("自动用例")]
        Auto = 1,

        [Description("接口用例")]
        Interface = 2,

        [Description("性能用例")]
        Performance = 3,

        [Description("安全用例")]
        Security = 4


    }
}