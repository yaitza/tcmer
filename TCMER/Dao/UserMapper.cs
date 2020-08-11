using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCMER.Model;
using TCMER.Utils;

namespace TCMER.Dao
{
    public class UserMapper
    {
        private const string SqlStr1 = "SELECT ID,NAME,CREATE_TIME,UPDATE_TIME,STATUS,ROLE_ID,TELEPHONE,EMAIL,REMARK FROM user WHERE ID = '{0}'";

        private const string SqlStr2 = "SELECT ID,NAME,CREATE_TIME,UPDATE_TIME,STATUS,ROLE_ID,TELEPHONE,EMAIL,REMARK FROM user WHERE NAME = '{0}' AND PASSWORD = '{1}'";

        private const string SqlStr3 = "INSERT INTO `user`(`ID`, `NAME`, `PASSWORD`, `CREATE_TIME`, `STATUS`, `UPDATE_TIME`,  `TELEPHONE`, `EMAIL`, `REMARK`) VALUES('{0}', '{1}', '{2}', NOW(), {3}, NOW(), '{4}', '{5}', '{6}')";


        private readonly MySqlHelper _mySqlHelper;

        public UserMapper()
        {
            _mySqlHelper = new MySqlHelper();
        }

        [Obsolete]
        public UserModel GetUserInfo(string userId)
        {
            DataSet ds = _mySqlHelper.Query(string.Format(SqlStr1, userId));
            UserModel um = new UserModel();
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    um.ID = dr["ID"].ToString();
                    um.Name = dr["NAME"].ToString();
                    um.CreateTime = DateTime.Parse(dr["CREATE_TIME"].ToString());
                    um.UpdateTime = DateTime.Parse(dr["UPDATE_TIME"].ToString());
                    um.Status = (UserStatus)int.Parse(dr["STATUS"].ToString());
                    um.RoleID = dr["ROLE_ID"].ToString();
                    um.TelePhone = dr["TELEPHONE"].ToString();
                    um.Email = dr["EMAIL"].ToString();
                    um.remark = dr["REMARK"].ToString();
                }
            }

            return um;
        }

        [Obsolete]
        public UserModel ValidateUser(string name, string password)
        {
            DataSet ds = _mySqlHelper.Query(string.Format(SqlStr2, name, password));
            UserModel um = new UserModel();
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    um.ID = dr["ID"].ToString();
                    um.Name = dr["NAME"].ToString();
                    um.CreateTime = DateTime.Parse(dr["CREATE_TIME"].ToString());
                    um.UpdateTime = DateTime.Parse(dr["UPDATE_TIME"].ToString());
                    um.Status = (UserStatus)int.Parse(dr["STATUS"].ToString());
                    um.RoleID = dr["ROLE_ID"].ToString();
                    um.TelePhone = dr["TELEPHONE"].ToString();
                    um.Email = dr["EMAIL"].ToString();
                    um.remark = dr["REMARK"].ToString();
                }
            }

            return um;
        }

        [Obsolete]
        public int AddUser(UserModel um)
        {
            string guid = System.Guid.NewGuid().ToString();
            string SqlStr4tmp = string.Format(SqlStr3, guid, um.Name, um.PassWord, (int)UserStatus.ENABLE, um.TelePhone, um.Email, um.remark);

            return _mySqlHelper.ExecuteSql(SqlStr4tmp);
        }

    }
}
