using System.ComponentModel;

namespace TCMER.Model
{
    public enum ImportanceLevel
    {
        [Description("高")]
        High = 2,

        [Description("中")]
        Medium = 1,

        [Description("低")]
        Low = 0
    }
}