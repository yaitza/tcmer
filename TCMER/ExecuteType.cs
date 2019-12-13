using System.ComponentModel;

namespace TCMER
{
    public enum ExecuteType
    {
        [Description("手动用例")]
        Manual,

        [Description("自动用例")]
        Auto,

        [Description("接口用例")]
        Interface,

        [Description("性能用例")]
        Performance,

        [Description("安全用例")]
        Security


    }
}