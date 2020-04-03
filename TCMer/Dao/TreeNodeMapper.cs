﻿using System;
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
        private const string SqlStr = @"SELECT tn.ID,tn.DATA_BODY,tn.UPDATED_BY,tn.UPDATED_TIME,tn.CREATED_BY,tn.CREATED_TIME,th.DEPTH 
                                FROM treenode tn LEFT JOIN treehierarchy th ON th.ANCESTOR = tn.ID AND th.DEPTH = {0}";

        private const string SqlStr2 = @"SELECT tn.ID,tn.DATA_BODY,tn.UPDATED_BY,tn.UPDATED_TIME,tn.CREATED_BY,tn.CREATED_TIME,th.DEPTH
                                FROM treenode tn LEFT JOIN treehierarchy th ON th.DESCENDANT = tn.ID WHERE th.ANCESTOR = {0} AND th.DESCENDANT != {0}";

        private const string SqlStr3 = @"SELECT tc.ID,tc.NAME,tc.UPDATED_BY,tc.UPDATED_TIME,tc.CREATED_BY,tc.CREATED_TIME,0 AS `DEPTH` 
                                FROM testcase tc LEFT JOIN node_case_map nc ON nc.TESTCASE_ID = tc.ID WHERE nc.TREENODE_ID = {0}";

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
            const String queryStr =
                @"SELECT tn.ID,tn.DATA_BODY,tn.UPDATED_BY,tn.UPDATED_TIME,tn.CREATED_BY,tn.CREATED_TIME,th.DEPTH FROM treenode tn LEFT JOIN treehierarchy th ON th.ANCESTOR = tn.ID AND th.DEPTH = 0;";
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
                    tnm.NodeType = NodeType.TestSuite;
                    GetNodesByAncestor(tnm.Nodes,tnm.Id);

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
        [Obsolete]
        public void GetNodesByAncestor(ObservableCollection<TreeNodeModel> tnmList, String id)
        {
            bool checkTestCase = false;
            string queryStr = string.Format(SqlStr2, id);
            DataSet ds = _mySqlHelper.Query(queryStr);
            foreach (DataTable dt in ds.Tables)
            {
                if (dt.Rows.Count == 0)
                {
                    checkTestCase = true;
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
                    tnmList.Add(tnm);
                }
            }

            if (checkTestCase)
            {
                string queryStr3 = string.Format(SqlStr3, id);
                DataSet ds3 = _mySqlHelper.Query(queryStr3);
                foreach (DataTable dt in ds3.Tables)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        TreeNodeModel tnm = new TreeNodeModel();
                        tnm.Id = dr["ID"].ToString();
                        tnm.DataBody = dr["NAME"].ToString();
                        tnm.CreateBy = dr["CREATED_BY"].ToString();
                        tnm.CreateTime = DateTime.Parse(dr["CREATED_TIME"].ToString());
                        tnm.UpdateBy = dr["UPDATED_BY"].ToString();
                        tnm.Depth = int.Parse(dr["DEPTH"].ToString());
                        tnm.UpdateTime = DateTime.Parse(dr["UPDATED_TIME"].ToString());
                        tnm.NodeType = NodeType.TestCase;
                        tnmList.Add(tnm);
                    }
                }
            }
            
        }



    }
}
