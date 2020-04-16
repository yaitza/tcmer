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
        public const string SqlStr1 = @"SELECT tc.ID, tc.`NAME`,tc.SUMMARY, tc.PRECONDITION, tc.IMPORTANCE, tc.TYPE, tc.UPDATED_BY, tc.UPDATED_TIME, tc.CREATED_BY, tc.CREATED_TIME 
                                        FROM testcase tc WHERE tc.ID = '{0}'";

        public const string SqlStr2 = @"SELECT ts.ID, ts.STEP_ORDER, ts.STEP_ACTIONS, ts.STEP_RESULTS, ts.CREATED_BY, ts.CREATED_TIME, ts.UPDATED_BY, ts.UPDATED_TIME FROM teststeps ts WHERE ts.TESTCASE_ID = '{0}'";


        public const string SqlStr4 = @"INSERT INTO `TCMer`.`node_case_map`(`ID`, `TREENODE_ID`, `TESTCASE_ID`, `VERSION`) VALUES ('{0}', '{1}', '{2}', '{3}')";

        public const string SqlStr5 = @"INSERT INTO `TCMer`.`testcase`(`ID`, `NAME`, `SUMMARY`, `PRECONDITION`, `IMPORTANCE`, `TYPE`, `CREATED_BY`, `CREATED_TIME`, `UPDATED_BY`, `UPDATED_TIME`) VALUES ('{0}', '{1}', NULL, NULL, 2, 0, 'muyi', NOW(), 'muyi', NOW());
";
        private const string SqlStr6 = @"UPDATE `TCMer`.`testcase` SET `{0}` = '{1}' WHERE `ID` = '{2}'";

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
        public void InsertTestCase(TestCaseModel tcm, TreeNodeModel stnm)
        {
            string guid = System.Guid.NewGuid().ToString();
            string SqlStr4tmp = string.Format(SqlStr4, guid, stnm.Id, tcm.Id, stnm.RootId);
            string SqlStr5tmp = string.Format(SqlStr5, tcm.Id, tcm.Id);
            
            _mySqlHelper.ExecuteSql(SqlStr5tmp);
            _mySqlHelper.ExecuteSql(SqlStr4tmp);
        }

        [Obsolete]
        public void UpdateTestCaseProperty(string property, string value, string id)
        {
            string sqlStr6Tmp = string.Format(SqlStr6, property, value, id);
            _mySqlHelper.ExecuteSql(sqlStr6Tmp);
        }
    }
}
