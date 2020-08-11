using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MySql.Data.MySqlClient;

namespace TCMER.Utils
{
    class MySqlHelper
    {
        private readonly MySqlConnection _mySqlConnection;

        public MySqlHelper()
        {
            String connectionString = ConfigHelper.GetSingleConfigData("MySqlConnectionString");
            try
            {
                this._mySqlConnection = new MySqlConnection(connectionString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sqlString">查询语句</param>
        /// <returns>DataSet</returns>
        [Obsolete]
        public DataSet Query(string sqlString)
        {
            DataSet ds = new DataSet();
            try
            {
                _mySqlConnection.Open();
                MySqlDataAdapter command = new MySqlDataAdapter(sqlString, _mySqlConnection);
                command.Fill(ds);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                DisplayHelper.ShowMessage(ex.Message, Color.FromRgb(255, 0, 0));
                throw new Exception(ex.Message);
            }
            finally
            {
                _mySqlConnection.Close();                
            }
            return ds;
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        [Obsolete]
        public int ExecuteSql(string sqlString)
        {
            using (MySqlCommand cmd = new MySqlCommand(sqlString, _mySqlConnection))
            {
                try
                {
                    _mySqlConnection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    DisplayHelper.ShowMessage(e.Message, Color.FromRgb(255, 0, 0));
                    _mySqlConnection.Close();
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    _mySqlConnection.Close();
                }
            }
        }

        ~MySqlHelper()
        {
            _mySqlConnection.Close();
        }
    }
}
