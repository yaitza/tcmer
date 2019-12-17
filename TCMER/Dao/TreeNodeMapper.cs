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
    class TreeNodeMapper
    {
        private readonly MySqlHelper _mySqlHelper;

        public TreeNodeMapper()
        {
            _mySqlHelper = new MySqlHelper();
        }

        [Obsolete]
        public List<TreeNodeModel> GetAllNodes()
        {
            const String queryStr =
                @"SELECT tn.ID,tn.DATA_BODY,tn.UPDATED_BY,tn.UPDATED_TIME,tn.CREATED_BY,tn.CREATED_TIME FROM treenode tn LEFT JOIN treehierarchy th ON th.ANCESTOR = tn.ID AND th.DEPTH = 0;";
            DataSet ds = _mySqlHelper.Query(queryStr);
            List<TreeNodeModel> tnmList = new List<TreeNodeModel>();
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TreeNodeModel tnm = new TreeNodeModel();
                    tnm.Id = dr["ID"].ToString();
                    tnm.DataBody = dr["DATA_BODY"].ToString();
                    tnm.CreateBy = dr["CREATED_BY"].ToString();
                    tnm.CreateTime = DateTime.Parse(dr["CREATED_TIME"].ToString());
                    tnm.UpdateBy = dr["UPDATED_BY"].ToString();
                    tnm.UpdateTime = DateTime.Parse(dr["UPDATED_TIME"].ToString());
                    tnmList.Add(tnm);
                }
            }

            return tnmList;

        } 

        
    }
}
