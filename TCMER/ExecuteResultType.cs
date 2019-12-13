using System.ComponentModel;

namespace TCMER
{
    public enum ExecuteResultType
    {
        [Description("通过")]
        Passed,

        [Description("失败")]
        Failed,

        [Description("阻断")]
        Blocked
    }
}