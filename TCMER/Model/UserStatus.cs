namespace TCMER.Model
{
    public enum UserStatus
    {
        /// <summary>
        /// 失效中
        /// </summary>
        DISABLE = 0,

        /// <summary>
        /// 生效中
        /// </summary>
        ENABLE = 1,

        /// <summary>
        /// 冻结中
        /// </summary>
        FREEZE = 2,

        /// <summary>
        /// 已删除
        /// </summary>
        DELETED = 3
    }
}