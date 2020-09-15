using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCMER.Model;

namespace TCMER.Utils
{
    public class LoginInfo
    {
        private static UserModel mLoginInfo = null;

        private LoginInfo()
        {

        }

        public static UserModel getInstance()
        {
            if(mLoginInfo == null)
            {
                mLoginInfo = new UserModel()
                {
                    Name = "NoBody"
                };
            }

            return mLoginInfo;
        }

        public static void setInstance(UserModel um)
        {
            if (um != null)
            {
                mLoginInfo = um;
            }
        }
    }
}
