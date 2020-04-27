using System.ComponentModel;

namespace TCMER.Model
{
    public enum ExecuteResultType
    {
        [Description("未执行")]
        None = 0,

        [Description("通过")]
        Passed = 1,

        [Description("失败")]
        Failed = 2,

        [Description("阻断")]
        Blocked = 3
    }
}