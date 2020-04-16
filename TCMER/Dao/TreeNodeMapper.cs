using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private const string SqlStr =
            @"SELECT tn.ID,tn.DATA_BODY,tn.UPDATED_BY,tn.UPDATED_TIME,tn.CREATED_BY,tn.CREATED_TIME,th.DEPTH 
                                FROM treenode tn LEFT JOIN treehierarchy th ON th.ANCESTOR = tn.ID AND th.DEPTH = '{0}' AND tn.DELETED = 0";

        private const string SqlStr2 =
            @"SELECT tn.ID,tn.DATA_BODY,tn.UPDATED_BY,tn.UPDATED_TIME,tn.CREATED_BY,tn.CREATED_TIME,th.DEPTH
                                FROM treenode tn LEFT JOIN treehierarchy th ON th.DESCENDANT = tn.ID WHERE th.ANCESTOR = '{0}' AND th.DESCENDANT != '{0}'";

        private const string SqlStr2Ex =
            @"SELECT tn.ID,tn.DATA_BODY,tn.UPDATED_BY,tn.UPDATED_TIME,tn.CREATED_BY,tn.CREATED_TIME,th.DEPTH
                                FROM treenode tn LEFT JOIN treehierarchy th ON th.DESCENDANT = tn.ID WHERE th.ANCESTOR = '{0}' AND th.DESCENDANT != '{0}' AND th.VERSIONID = '{1}'";

        private const string SqlStr3 =
            @"SELECT tc.ID,tc.NAME,tc.UPDATED_BY,tc.UPDATED_TIME,tc.CREATED_BY,tc.CREATED_TIME,0 AS `DEPTH` 
                                FROM testcase tc LEFT JOIN node_case_map nc ON nc.TESTCASE_ID = tc.ID WHERE nc.TREENODE_ID = '{0}'";

        private const string SqlStr3Ex =
            @"SELECT tc.ID,tc.NAME,tc.UPDATED_BY,tc.UPDATED_TIME,tc.CREATED_BY,tc.CREATED_TIME,0 AS `DEPTH` 
                                FROM testcase tc LEFT JOIN node_case_map nc ON nc.TESTCASE_ID = tc.ID WHERE nc.TREENODE_ID = '{0}' AND nc.VERSION = '{1}'";

        private const string SqlStr4 =
            @"INSERT INTO `TCMer`.`treenode`(`ID`, `DATA_BODY`, `CREATED_BY`, `CREATED_TIME`, `UPDATED_BY`, `UPDATED_TIME`, `DELETED`, `VFLAG`) VALUES ('{0}', '{1}', 'muyi', NOW(), 'muyi', NOW(), 0, {2})";

        private const string SqlStr5 =
            @"INSERT INTO `TCMer`.`treehierarchy`(`ANCESTOR`, `DESCENDANT`, `DEPTH`, `VERSIONID`) VALUES ('{0}', '{1}', {2}, 'root')";

        private const string SqlStr5Ex =
            @"INSERT INTO `TCMer`.`treehierarchy`(`ANCESTOR`, `DESCENDANT`, `DEPTH`, `VERSIONID`) VALUES ('{0}', '{1}', {2}, '{3}')";

        private const string SqlStr6 =
            @"SELECT `DESCENDANT` FROM `TCMer`.`treehierarchy` WHERE `DEPTH` = 1 AND ANCESTOR = '01'";

        private const string SqlStr7 = @"SELECT `VFLAG` FROM `TCMer`.`treenode` WHERE ID = '{0}'";

        private const string SqlStr8 = @"UPDATE `TCMer`.`treenode` SET `{0}` = '{1}' WHERE `ID` = '{2}'";

        private readonly MySqlHelper _mySqlHelper;

        public TreeNodeMapper()
        {
            _mySqlHelper = new MySqlHelper();
        }

        /// <summary>
        /// 获取当前所有测试套以及测试用例数据，以树的形式提供
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public List<TreeNodeModel> GetAllNodes()
        {
            List<TreeNodeModel> tnmList = new List<TreeNodeModel>();

            const string queryStr =
                @"SELECT tn.ID,tn.DATA_BODY,tn.UPDATED_BY,tn.UPDATED_TIME,tn.CREATED_BY,tn.CREATED_TIME,th.DEPTH FROM treenode tn LEFT JOIN treehierarchy th ON th.ANCESTOR = tn.ID WHERE th.DEPTH = 0 AND tn.VFLAG = 0 AND tn.DELETED = 0";
            DataSet ds = _mySqlHelper.Query(queryStr);
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
                    tnm.NodeType = NodeType.TestSuite;
                    tnm.RootId = dr["ID"].ToString();
                    GetNodesByAncestor(tnm.Nodes, tnm.Id);

                    tnmList.Add(tnm);
                }
            }

            const string queryStrEx =
                @"SELECT tn.ID,tn.DATA_BODY,tn.UPDATED_BY,tn.UPDATED_TIME,tn.CREATED_BY,tn.CREATED_TIME,th.DEPTH FROM treenode tn LEFT JOIN treehierarchy th ON th.ANCESTOR = tn.ID WHERE th.DEPTH = 0 AND tn.VFLAG = 1 AND tn.DELETED = 0";
            DataSet dsEx = _mySqlHelper.Query(queryStrEx);
            foreach (DataTable dt in dsEx.Tables)
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
                    tnm.NodeType = NodeType.TestSuite;
                    tnm.RootId = dr["ID"].ToString();
                    GetNodesByAncestor(tnm.Nodes, tnm.Id, tnm.RootId);

                    tnmList.Add(tnm);
                }
            }

            return tnmList;
        }

        [Obsolete]
        public List<TreeNodeModel> GetAllNodes(int depth)
        {
            List<TreeNodeModel> tnmList = new List<TreeNodeModel>();

            string queryStr = string.Format(SqlStr, depth);
            DataSet ds = _mySqlHelper.Query(queryStr);

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
                    tnm.Depth = int.Parse(dr["DEPTH"].ToString());
                    tnm.UpdateTime = DateTime.Parse(dr["UPDATED_TIME"].ToString());

                    tnmList.Add(tnm);
                }
            }

            return tnmList;
        }

        /// <summary>
        /// 根据父节点id获取其子节点所有信息
        /// </summary>
        /// <param name="tnmList">返回子节点信息</param>
        /// <param name="id">父节点id</param>
        /// <param name="rootId">根节点id</param>
        [Obsolete]
        public void GetNodesByAncestor(ObservableCollection<TreeNodeModel> tnmList, string id, string rootId = null)
        {
            bool checkTestSuite = false;
            bool checkTestCase = false;

            string queryStr = string.IsNullOrEmpty(rootId)
                ? string.Format(SqlStr2, id)
                : string.Format(SqlStr2Ex, id, rootId);
            //string queryStr = string.Format(SqlStr2, id);
            DataSet ds = _mySqlHelper.Query(queryStr);
            foreach (DataTable dt in ds.Tables)
            {
                if (dt.Rows.Count == 0)
                {
                    checkTestSuite = true;
                    break;
                }

                foreach (DataRow dr in dt.Rows)
                {
                    TreeNodeModel tnm = new TreeNodeModel();
                    tnm.Id = dr["ID"].ToString();
                    tnm.DataBody = dr["DATA_BODY"].ToString();
                    tnm.CreateBy = dr["CREATED_BY"].ToString();
                    tnm.CreateTime = DateTime.Parse(dr["CREATED_TIME"].ToString());
                    tnm.UpdateBy = dr["UPDATED_BY"].ToString();
                    tnm.Depth = int.Parse(dr["DEPTH"].ToString());
                    tnm.UpdateTime = DateTime.Parse(dr["UPDATED_TIME"].ToString());
                    tnm.NodeType = NodeType.TestSuite;
                    tnm.RootId = rootId;
                    this.GetNodesByAncestor(tnm.Nodes, tnm.Id, rootId);
                    tnmList.Add(tnm);
                }
            }

            string queryStr3 = string.IsNullOrEmpty(rootId)
                ? string.Format(SqlStr3, id)
                : string.Format(SqlStr3Ex, id, rootId);
            DataSet ds3 = _mySqlHelper.Query(queryStr3);
            foreach (DataTable dt in ds3.Tables)
            {
                if (dt.Rows.Count == 0)
                {
                    checkTestCase = true;
                    break;
                }

                foreach (DataRow dr in dt.Rows)
                {
                    TestCaseNodeModel tcnm = new TestCaseNodeModel();
                    tcnm.Id = dr["ID"].ToString();
                    tcnm.DataBody = dr["NAME"].ToString();
                    tcnm.CreateBy = dr["CREATED_BY"].ToString();
                    tcnm.CreateTime = DateTime.Parse(dr["CREATED_TIME"].ToString());
                    tcnm.UpdateBy = dr["UPDATED_BY"].ToString();
                    tcnm.Depth = int.Parse(dr["DEPTH"].ToString());
                    tcnm.UpdateTime = DateTime.Parse(dr["UPDATED_TIME"].ToString());
                    tcnm.NodeType = NodeType.TestCase;
                    tcnm.RootId = rootId;
                    tnmList.Add(tcnm);
                }
            }

            if (checkTestSuite && checkTestCase)
            {
                return;
            }
        }

        [Obsolete]
        public void InsertTreeNode(TreeNodeModel tnm, TreeNodeModel stnm)
        {
            string sqlStr4Tmp = string.Format(SqlStr4, tnm.Id, tnm.DataBody, 0);
            string sqlStr5Tmp = string.Format(SqlStr5, stnm.Id, tnm.Id, tnm.Depth);

            _mySqlHelper.ExecuteSql(sqlStr4Tmp);
            _mySqlHelper.ExecuteSql(sqlStr5Tmp);

            if (this.IsVersionTreeNode(stnm.RootId))
            {
                string sqlStr5ExTmp = string.Format(SqlStr5Ex, stnm.Id, tnm.Id, tnm.Depth, stnm.RootId);
                _mySqlHelper.ExecuteSql(sqlStr5ExTmp);
            }

            // 当在版本里添加第一层级的测试套，需要添加至测试集中
            if (tnm.Depth == 1)
            {
                sqlStr5Tmp = string.Format(SqlStr5, "01", tnm.Id, tnm.Depth);
                _mySqlHelper.ExecuteSql(sqlStr5Tmp);
            }
        }

        [Obsolete]
        public void InsertTreeNode(TreeNodeModel tnm)
        {
            string sqlStr4Tmp = string.Format(SqlStr4, tnm.Id, tnm.DataBody, 1);
            string sqlStr5Tmp = string.Format(SqlStr5, tnm.Id, tnm.Id, tnm.Depth);
            _mySqlHelper.ExecuteSql(sqlStr4Tmp);
            _mySqlHelper.ExecuteSql(sqlStr5Tmp);

            DataSet ds = _mySqlHelper.Query(SqlStr6);

            foreach (DataTable dt in ds.Tables)
            {
                string id = dt.Rows[0]["DESCENDANT"].ToString();

                string sqlStr5TmpEx = string.Format(SqlStr5, tnm.Id, id, tnm.Depth + 1);
                _mySqlHelper.ExecuteSql(sqlStr5TmpEx);
            }
        }

        [Obsolete]
        public bool IsVersionTreeNode(string id)
        {
            int vFlag = 0;
            string sqlStr7Tmp = string.Format(SqlStr7, id);
            DataSet ds = _mySqlHelper.Query(sqlStr7Tmp);
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    vFlag = int.Parse(dr["VFLAG"].ToString());
                }
            }

            if (vFlag == 1)
            {
                return true;
            }

            return false;
        }

        [Obsolete]
        public void UpdateTreeNodeProperty(string property, string value, string id)
        {
            string sqlStr8Tmp = string.Format(SqlStr8, property, value, id);
            _mySqlHelper.ExecuteSql(sqlStr8Tmp);
        }
    }
}
