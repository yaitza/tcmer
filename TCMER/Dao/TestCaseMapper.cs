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
    class TestCaseMapper
    {
        public const string SqlStr1 = @"SELECT tc.ID, tc.ORDERID, tc.ORDERID, tc.`NAME`,tc.SUMMARY, tc.PRECONDITION, tc.IMPORTANCE, tc.TYPE, tc.UPDATED_BY, tc.UPDATED_TIME, tc.CREATED_BY, tc.CREATED_TIME 
                                        FROM testcase tc WHERE tc.ID = '{0}' AND tc.DELETED = 0 ORDER BY tc.ORDERID";

        public const string SqlStr2 = @"SELECT ts.ID, ts.STEP_ORDER, ts.STEP_ACTIONS, ts.STEP_RESULTS, ts.CREATED_BY, ts.CREATED_TIME, ts.UPDATED_BY, ts.UPDATED_TIME FROM teststeps ts WHERE ts.TESTCASE_ID = '{0}'";


        public const string SqlStr4 = @"INSERT INTO `TCMer`.`node_case_map`(`ID`, `TREENODE_ID`, `TESTCASE_ID`, `VERSIONID`) VALUES ('{0}', '{1}', '{2}', '{3}')";

        public const string SqlStr5 = @"INSERT INTO `TCMer`.`testcase`(`ID`, `ORDERID`, `NAME`, `SUMMARY`, `PRECONDITION`, `IMPORTANCE`, `TYPE`, `CREATED_BY`, `CREATED_TIME`, `UPDATED_BY`, `UPDATED_TIME`) VALUES ('{0}', '{1}', '{2}', NULL, NULL, 2, 0, '{3}', NOW(), '{4}', NOW());
";
        private const string SqlStr6 = @"UPDATE `TCMer`.`testcase` SET `{0}` = '{1}', `UPDATED_BY` = '{2}'  WHERE `ID` = '{3}'";

        private const string SqlStr7 = @"UPDATE `TCMer`.`testcase` SET `DELETED` = 1 WHERE `ID` = '{0}'";

        private const string SqlStr8 = @"SELECT RESULT,CREATED_BY,CREATED_TIME FROM TCMer.case_result WHERE CASE_ID = '{0}' AND VERSIONID = '{1}' ORDER BY ID DESC LIMIT 1";

        private const string SqlStr9 = @"INSERT INTO `TCMer`.`case_result`(`CASE_ID`, `RESULT`, `CREATED_BY`, `VERSIONID`) VALUES  ('{0}', {1}, '{2}', '{3}')";

        private readonly MySqlHelper _mySqlHelper;

        public TestCaseMapper()
        {
            _mySqlHelper = new MySqlHelper();
        }

        [Obsolete]
        public TestCaseModel QueryTestCaseById(string testcaseId)
        {
            DataSet ds = _mySqlHelper.Query(string.Format(SqlStr1, testcaseId));
            TestCaseModel tcm = new TestCaseModel();
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    tcm.Id = dr["ID"].ToString();
                    tcm.OrderId = dr["ORDERID"].ToString();
                    tcm.Name = dr["NAME"].ToString();
                    tcm.Summary = dr["SUMMARY"].ToString();
                    tcm.Precondition = dr["PRECONDITION"].ToString();
                    tcm.Importance = (ImportanceLevel) int.Parse(dr["IMPORTANCE"].ToString());
                    tcm.TestCaseType = (TestCaseType) int.Parse(dr["TYPE"].ToString());
                    tcm.CreateBy = dr["CREATED_BY"].ToString();
                    tcm.CreateTime = DateTime.Parse(dr["CREATED_TIME"].ToString());
                    tcm.UpdateBy = dr["UPDATED_BY"].ToString();
                    tcm.UpdateTime = DateTime.Parse(dr["UPDATED_TIME"].ToString());
                    tcm.TestSteps = this.QueryTestStepsById(testcaseId);
                }
            }

            return tcm;
        }

        [Obsolete]
        private ObservableCollection<TestStepModel> QueryTestStepsById(string testcaseId)
        {
            ObservableCollection<TestStepModel> tsmArrayList = new ObservableCollection<TestStepModel>();

            DataSet ds = _mySqlHelper.Query(string.Format(SqlStr2, testcaseId));
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TestStepModel tsm = new TestStepModel();
                    tsm.Id = dr["ID"].ToString();
                    tsm.StepOrder = int.Parse(dr["STEP_ORDER"].ToString());
                    tsm.StepActions = dr["STEP_ACTIONS"].ToString();
                    tsm.StepResults = dr["STEP_RESULTS"].ToString();
                    tsm.CreateBy = dr["CREATED_BY"].ToString();
                    tsm.CreateTime = DateTime.Parse(dr["CREATED_TIME"].ToString());
                    tsm.UpdateBy = dr["UPDATED_BY"].ToString();
                    tsm.UpdateTime = DateTime.Parse(dr["UPDATED_TIME"].ToString());

                    tsmArrayList.Add(tsm);
                }
            }

            return tsmArrayList;
        }

        [Obsolete]
        public void InsertTestCase(TestCaseModel tcm, TreeNodeModel stnm, UserModel um)
        {
            string guid = System.Guid.NewGuid().ToString();
            string SqlStr4tmp = string.Format(SqlStr4, guid, stnm.Id, tcm.Id, stnm.RootId);
            string SqlStr5tmp = string.Format(SqlStr5, tcm.Id, tcm.OrderId, tcm.Id, um.Name, um.Name);
            
            _mySqlHelper.ExecuteSql(SqlStr5tmp);
            _mySqlHelper.ExecuteSql(SqlStr4tmp);
        }

        [Obsolete]
        public void UpdateTestCaseProperty(string property, string value, string userName, string id)
        {
            string sqlStr6Tmp = string.Format(SqlStr6, property, value, userName, id);
            _mySqlHelper.ExecuteSql(sqlStr6Tmp);
        }

        [Obsolete]
        public void DeleteTestCase(string id)
        {
            string sqlStr7Tmp = string.Format(SqlStr7, id);
            _mySqlHelper.ExecuteSql(sqlStr7Tmp);
        }

        [Obsolete]
        public ExecuteResult QueryResultById(string testcaseId, string versionid)
        {
            DataSet ds = _mySqlHelper.Query(string.Format(SqlStr8, testcaseId, versionid));
            ExecuteResult er = new ExecuteResult();
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    er.result = (ExecuteResultType)int.Parse(dr["RESULT"].ToString());                    
                    er.CreateBy = dr["CREATED_BY"].ToString();
                    er.CreateTime = DateTime.Parse(dr["CREATED_TIME"].ToString());
                }
            }

            return er;
        }

        [Obsolete]
        public void InsertTestCaseExecuteResult(TreeNodeModel tnModel ,ExecuteResult erModel)
        {
            string SqlStr9tmp = string.Format(SqlStr9, tnModel.Id, (int)erModel.result, erModel.CreateBy, tnModel.RootId);

            _mySqlHelper.ExecuteSql(SqlStr9tmp);
        }

    }
}
