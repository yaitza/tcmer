using System;

namespace TCMER.Model
{
    public class UserModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public UserStatus Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 用户角色ID
        /// </summary>
        public string RoleID { get; set; }

        /// <summary>
        /// 用户电话号码
        /// </summary>
        public string TelePhone { get; set; }

        /// <summary>
        /// 用户邮件地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}