using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Devin
{
    /// <summary>
    /// 数据库的通用访问代码 
    /// 此类为抽象类，不允许实例化，在应用时直接调用即可
    /// 创建人：沈金龙
    /// 创建时间：2015-01-18
    /// 修改时间：2015-07-30
    /// </summary>
    public abstract class SQLHelper
    {
        #region 全局变量

        /// <summary>
        /// 默认数据库连接字符串
        /// </summary>
        private static string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnStr"].ToString().Trim();
        
        /// <summary>
        /// Hashtable to store cached parameters
        /// 用于存储缓存的参数信息
        /// </summary>
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        #endregion

        #region 公用函数

        /// <summary>
        /// 设置连接字符串值
        /// </summary>
        /// <param name="connectionStringSign">连接字符串的标识</param>
        /// <returns></returns>
        private static void SetConStr(string connectionStringSign)
        {
            connectionString = System.Configuration.ConfigurationManager.AppSettings[connectionStringSign].ToString().Trim();
        }

        /// <summary>
        /// add parameter array to the cache
        /// 将参数数组存入缓存中
        /// </summary>
        /// <param name="cacheKey">Key to the parameter cache(缓存参数的键)</param>
        /// <param name="cmdParms">an array of SqlParamters to be cached(要缓存的参数数组)</param>
        private static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// Retrieve cached parameters
        /// 根据键值返回缓存的参数数组
        /// </summary>
        /// <param name="cacheKey">key used to lookup parameters(用来查找的键)</param>
        /// <returns>Cached SqlParamters array(缓存的参数数组)</returns>
        private static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];
            if (cachedParms == null)
                return null;
            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];
            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();
            return clonedParms;
        }

        /// <summary>
        /// 为执行命令准备参数
        /// </summary>
        /// <param name="cmd">SqlCommand命令</param>
        /// <param name="conn">已经存在的数据库连接</param>
        /// <param name="trans">数据库事物处理</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">Command text，T-SQL语句 例如 Select * from Products</param>
        /// <param name="cmdParms">返回带参数的命令</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            //判断数据库连接状态
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            //判断是否需要事物处理
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        #endregion

        #region ExecteNonQuery

        /// <summary>
        /// ExecteNonQuery无参数方法
        /// 返回执行影响的行数
        /// </summary>
        /// <param name="cmdText">T-SQL语句</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        public static int ExecteNonQuery(string cmdText)
        {
            return ExecteNonQuery("", cmdText, null);
        }

        /// <summary>
        /// ExecteNonQuery有参数方法
        /// 返回执行影响的行数
        /// </summary>
        /// <param name="cmdText">T-SQL语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        public static int ExecteNonQuery(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecteNonQuery("", cmdText, commandParameters);
        }

        /// <summary>
        /// ExecteNonQuery基方法，使用连接字符串标识
        /// 返回执行影响的行数
        /// </summary>
        /// <param name="connectionStringSign">一个有效的数据库连接字符串标识</param>
        /// <param name="cmdText">T-SQL语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        public static int ExecteNonQuery(string connectionStringSign, string cmdText, params SqlParameter[] commandParameters)
        {
            //自定义连接字符串
            if (connectionStringSign != "")
                SetConStr(connectionStringSign);

            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //通过PrePareCommand方法将参数逐个加入到SqlCommand的参数集合中
                PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                //清空SqlCommand中的参数列表
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// ExecteNonQuery基方法，使用数据库连接对象
        /// 返回执行影响的行数
        /// </summary>
        /// <param name="sqlConnection">一个有效的数据库连接对象</param>
        /// <param name="cmdText">T-SQL语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        public static int ExecteNonQuery(SqlConnection sqlConnection , string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (sqlConnection)
            {
                //通过PrePareCommand方法将参数逐个加入到SqlCommand的参数集合中
                PrepareCommand(cmd, sqlConnection, null, CommandType.Text, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                //清空SqlCommand中的参数列表
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// ExecteNonQuerySP无参数方法
        /// 返回执行影响的行数
        /// </summary>
        /// <param name="cmdText">存储过程名称</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        public static int ExecteNonQuerySP(string cmdText)
        {
            return ExecteNonQuery("", cmdText, null);
        }

        /// <summary>
        /// ExecteNonQuerySP有参数方法
        /// 返回执行影响的行数
        /// </summary>
        /// <param name="cmdText">存储过程名称</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        public static int ExecteNonQuerySP(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecteNonQuery("", cmdText, commandParameters);
        }

        /// <summary>
        /// ExecteNonQuerySP基方法，使用连接字符串标识
        /// 返回执行影响的行数
        /// </summary>
        /// <param name="connectionStringSign">一个有效的数据库连接字符串标识</param>
        /// <param name="cmdText">存储过程名称</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        public static int ExecteNonQuerySP(string connectionStringSign, string cmdText, params SqlParameter[] commandParameters)
        {
            //自定义连接字符串
            if (connectionStringSign != "")
                SetConStr(connectionStringSign);
            
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //通过PrePareCommand方法将参数逐个加入到SqlCommand的参数集合中
                PrepareCommand(cmd, conn, null, CommandType.StoredProcedure, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                //清空SqlCommand中的参数列表
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// ExecteNonQuerySP基方法，使用数据库连接对象
        /// 返回执行影响的行数
        /// </summary>
        /// <param name="sqlConnection">一个有效的数据库连接对象</param>
        /// <param name="cmdText">存储过程名称</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        public static int ExecteNonQuerySP(SqlConnection sqlConnection, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (sqlConnection)
            {
                //通过PrePareCommand方法将参数逐个加入到SqlCommand的参数集合中
                PrepareCommand(cmd, sqlConnection, null, CommandType.StoredProcedure, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                //清空SqlCommand中的参数列表
                cmd.Parameters.Clear();
                return val;
            }
        }

        #endregion

        #region ExecuteScalar

        /// <summary>
        /// ExecuteScalar无参数方法
        /// 返回结果第一行第一列的值，类型为object
        /// </summary>
        /// <param name="cmdText">T-SQL语句</param>
        /// <returns>返回一个对象</returns>
        public static object ExecuteScalar(string cmdText)
        {
            return ExecuteScalar("", cmdText, null);
        }

        /// <summary>
        /// ExecuteScalar有参数方法
        /// 返回结果第一行第一列的值，类型为object
        /// </summary>
        /// <param name="cmdText">T-SQL语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个对象</returns>
        public static object ExecuteScalar(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar("", cmdText, commandParameters);
        }

        /// <summary>
        /// ExecuteScalar基方法，使用连接字符串标识
        /// 返回结果第一行第一列的值，类型为object
        /// Execute a SqlCommand that returns the first column of the first record against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <param name="connectionStringSign">一个有效的数据库连接字符串标识</param>
        /// <param name="cmdText">T-SQL语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(string connectionStringSign , string cmdText , params SqlParameter[] commandParameters)
        {
            //自定义连接字符串
            if (connectionStringSign != "")
                SetConStr(connectionStringSign);

            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, CommandType.Text, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// ExecuteScalar基方法，使用数据库连接对象
        /// 返回结果第一行第一列的值，类型为object
        /// Execute a SqlCommand that returns the first column of the first record against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <param name="sqlConnection">一个有效的数据库连接对象</param>
        /// <param name="cmdText">T-SQL语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(SqlConnection sqlConnection , string cmdText , params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, sqlConnection, null, CommandType.Text, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// ExecuteScalarSP无参数方法
        /// 返回结果第一行第一列的值，类型为object
        /// </summary>
        /// <param name="cmdText">存储过程名称</param>
        /// <returns>返回一个对象</returns>
        public static object ExecuteScalarSP(string cmdText)
        {
            return ExecuteScalarSP("", cmdText, null);
        }

        /// <summary>
        /// ExecuteScalarSP有参数方法
        /// 返回结果第一行第一列的值，类型为object
        /// </summary>
        /// <param name="cmdText">存储过程名称</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个对象</returns>
        public static object ExecuteScalarSP(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalarSP("", cmdText, commandParameters);
        }

        /// <summary>
        /// ExecuteScalarSP基方法，使用连接字符串标识
        /// 返回结果第一行第一列的值，类型为object
        /// Execute a SqlCommand that returns the first column of the first record against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <param name="connectionStringSign">一个有效的数据库连接字符串标识</param>
        /// <param name="cmdText">存储过程名称</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalarSP(string connectionStringSign, string cmdText, params SqlParameter[] commandParameters)
        {
            //自定义连接字符串
            if (connectionStringSign != "")
                SetConStr(connectionStringSign);

            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, CommandType.Text, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// ExecuteScalarSP基方法，使用数据库连接对象
        /// 返回结果第一行第一列的值，类型为object
        /// Execute a SqlCommand that returns the first column of the first record against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <param name="sqlConnection">一个有效的数据库连接对象</param>
        /// <param name="cmdText">存储过程名称</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalarSP(SqlConnection sqlConnection, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, sqlConnection, null, CommandType.Text, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }


        #endregion

        #region ExecuteReader

        /// <summary>
        /// ExecuteReader无参数方法
        /// 返回一个结果集，类型为SqlDataReader
        /// </summary>
        /// <param name="cmdText">T-SQL语句</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReader(string cmdText)
        {
            return ExecuteReader("", cmdText, null);
        }

        /// <summary>
        /// ExecuteReader有参数方法
        /// 返回一个结果集，类型为SqlDataReader
        /// </summary>
        /// <param name="cmdText">T-SQL语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReader(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteReader("", cmdText, commandParameters);
        }

        /// <summary>
        /// ExecuteReader基方法，使用连接字符串标识
        /// 返回一个结果集，类型为SqlDataReader
        /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <param name="connectionStringSign">一个有效的数据库连接字符串标识</param>
        /// <param name="cmdText">T-SQL语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReader(string connectionStringSign, string cmdText, params SqlParameter[] commandParameters)
        {
            //自定义连接字符串
            if (connectionStringSign != "")
                SetConStr(connectionStringSign);

            SqlCommand cmd = new SqlCommand();

            SqlConnection conn = new SqlConnection(connectionString);

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work

            try
            {
                PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// ExecuteReader基方法，使用数据库连接对象
        /// 返回一个结果集，类型为SqlDataReader
        /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <param name="sqlConnection">一个有效的数据库连接对象</param>
        /// <param name="cmdText">T-SQL语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReader(SqlConnection sqlConnection, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work

            try
            {
                PrepareCommand(cmd, sqlConnection, null, CommandType.Text, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                sqlConnection.Close();
                throw;
            }
        }

        /// <summary>
        /// ExecuteReaderSP无参数方法
        /// 返回一个结果集，类型为SqlDataReader
        /// </summary>
        /// <param name="cmdText">存储过程名称</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReaderSP(string cmdText)
        {
            return ExecuteReaderSP("", cmdText, null);
        }

        /// <summary>
        /// ExecuteReaderSP有参数方法
        /// 返回一个结果集，类型为SqlDataReader
        /// </summary>
        /// <param name="cmdText">存储过程名称</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReaderSP(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteReaderSP("", cmdText, commandParameters);
        }

        /// <summary>
        /// ExecuteReaderSP基方法，使用连接字符串标识
        /// 返回一个结果集，类型为SqlDataReader
        /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <param name="connectionStringSign">一个有效的数据库连接字符串标识</param>
        /// <param name="cmdText">存储过程名称</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReaderSP(string connectionStringSign, string cmdText, params SqlParameter[] commandParameters)
        {
            //自定义连接字符串
            if (connectionStringSign != "")
                SetConStr(connectionStringSign);

            SqlCommand cmd = new SqlCommand();

            SqlConnection conn = new SqlConnection(connectionString);

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work

            try
            {
                PrepareCommand(cmd, conn, null, CommandType.StoredProcedure, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// ExecuteReaderSP基方法，使用数据库连接对象
        /// 返回一个结果集，类型为SqlDataReader
        /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <param name="sqlConnection">一个有效的数据库连接对象</param>
        /// <param name="cmdText">存储过程名称</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReaderSP(SqlConnection sqlConnection, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work

            try
            {
                PrepareCommand(cmd, sqlConnection, null, CommandType.StoredProcedure, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                sqlConnection.Close();
                throw;
            }
        }

        #endregion

        #region ExecuteDataSet

        /// <summary>
        /// ExecuteDataSet无参数方法
        /// 返回一个结果集，类型为DataSet
        /// </summary>
        /// <param name="cmdText">T-SQL语句</param>
        /// <returns>return a dataset</returns>
        public static DataSet ExecuteDataSet(string cmdText)
        {
            return ExecuteDataSet("", cmdText, null);
        }

        /// <summary>
        /// ExecuteDataSet有参数方法
        /// 返回一个结果集，类型为DataSet
        /// </summary>
        /// <param name="cmdText">T-SQL语句</param>
        /// <param name="commandParameters"></param>
        /// <returns>return a dataset</returns>
        public static DataSet ExecuteDataSet(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataSet("", cmdText, commandParameters);
        }

        /// <summary>
        /// ExecuteDataSet基方法，使用连接字符串标识
        /// 返回一个结果集，类型为DataSet
        /// </summary>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdText">T-SQL语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>return a dataset</returns>
        public static DataSet ExecuteDataSet(string connectionStringSign, string cmdText, params SqlParameter[] commandParameters)
        {
            //自定义连接字符串
            if (connectionStringSign != "")
                SetConStr(connectionStringSign);

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, commandParameters);
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                da.SelectCommand = cmd;
                da.Fill(ds);
                return ds;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// ExecuteDataSet基方法，使用数据库连接对象
        /// 返回一个结果集，类型为DataSet
        /// </summary>
        /// <param name="connectionString">一个有效的数据库连接对象</param>
        /// <param name="cmdText">T-SQL语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>return a dataset</returns>
        public static DataSet ExecuteDataSet(SqlConnection sqlConnection, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, sqlConnection, null, CommandType.Text, cmdText, commandParameters);
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                da.SelectCommand = cmd;
                da.Fill(ds);
                return ds;
            }
            catch
            {
                sqlConnection.Close();
                throw;
            }
        }

        /// <summary>
        /// ExecuteDataSetSP无参数方法
        /// 返回一个结果集，类型为DataSet
        /// </summary>
        /// <param name="cmdText">存储过程名称</param>
        /// <returns>return a dataset</returns>
        public static DataSet ExecuteDataSetSP(string cmdText)
        {
            return ExecuteDataSetSP("", cmdText, null);
        }

        /// <summary>
        /// ExecuteDataSetSP有参数方法
        /// 返回一个结果集，类型为DataSet
        /// </summary>
        /// <param name="cmdText">存储过程名称</param>
        /// <param name="commandParameters"></param>
        /// <returns>return a dataset</returns>
        public static DataSet ExecuteDataSetSP(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataSetSP("", cmdText, commandParameters);
        }

        /// <summary>
        /// ExecuteDataSetSP基方法，使用连接字符串标识
        /// 返回一个结果集，类型为DataSet
        /// </summary>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdText">存储过程名称</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>return a dataset</returns>
        public static DataSet ExecuteDataSetSP(string connectionStringSign, string cmdText, params SqlParameter[] commandParameters)
        {
            //自定义连接字符串
            if (connectionStringSign != "")
                SetConStr(connectionStringSign);

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, conn, null, CommandType.StoredProcedure, cmdText, commandParameters);
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                da.SelectCommand = cmd;
                da.Fill(ds);
                return ds;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// ExecuteDataSetSP基方法，使用数据库连接对象
        /// 返回一个结果集，类型为DataSet
        /// </summary>
        /// <param name="connectionString">一个有效的数据库连接对象</param>
        /// <param name="cmdText">存储过程名称</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>return a dataset</returns>
        public static DataSet ExecuteDataSetSP(SqlConnection sqlConnection, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, sqlConnection, null, CommandType.Text, cmdText, commandParameters);
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                da.SelectCommand = cmd;
                da.Fill(ds);
                return ds;
            }
            catch
            {
                sqlConnection.Close();
                throw;
            }
        }

        /// <summary>
        /// ExecuteDataSet基方法，使用连接字符串标识
        /// 返回一个结果集，类型为DataView
        /// </summary>
        /// <param name="connectionStringSign">一个有效的数据库连接字符串</param>
        /// <param name="sortExpression"></param>
        /// <param name="direction"></param>
        /// <param name="cmdType">存储过程或者T-SQL语句</param>
        /// <param name="cmdText">存储过程的名称或者T-SQL语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns></returns>
        public static DataView ExecuteDataSet(string connectionStringSign, string sortExpression, string direction, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            //自定义连接字符串
            if (connectionStringSign != "")
                SetConStr(connectionStringSign);

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                da.SelectCommand = cmd;
                da.Fill(ds);
                DataView dv = ds.Tables[0].DefaultView;
                dv.Sort = sortExpression + " " + direction;
                return dv;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// ExecuteDataSet基方法，使用数据库连接对象
        /// 返回一个结果集，类型为DataView
        /// </summary>
        /// <param name="connectionStringSign">一个有效的数据库连接对象</param>
        /// <param name="sortExpression"></param>
        /// <param name="direction"></param>
        /// <param name="cmdType">存储过程或者T-SQL语句</param>
        /// <param name="cmdText">存储过程的名称或者T-SQL语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns></returns>
        public static DataView ExecuteDataSet(SqlConnection sqlConnection, string sortExpression, string direction, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, sqlConnection, null, cmdType, cmdText, commandParameters);
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                da.SelectCommand = cmd;
                da.Fill(ds);
                DataView dv = ds.Tables[0].DefaultView;
                dv.Sort = sortExpression + " " + direction;
                return dv;
            }
            catch
            {
                sqlConnection.Close();
                throw;
            }
        }

        #endregion

        #region ExecuteTables

        /// <summary>
        /// ExecuteTables无参数方法
        /// 返回一个表集合(DataTableCollection)表示查询得到的数据集
        /// </summary>
        /// <param name="cmdText">T-SQL语句</param>
        /// <returns>返回一个表集合(DataTableCollection)表示查询得到的数据集</returns>
        public static DataTableCollection ExecuteTables(string cmdText)
        {
            return ExecuteTables("", cmdText, null);
        }

        /// <summary>
        /// ExecuteTables有参数方法
        /// 返回一个表集合(DataTableCollection)表示查询得到的数据集
        /// </summary>
        /// <param name="cmdText">T-SQL语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个表集合(DataTableCollection)表示查询得到的数据集</returns>
        public static DataTableCollection ExecuteTables(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteTables("", cmdText, commandParameters);
        }

        /// <summary>
        /// ExecuteTables基方法，使用连接字符串标识
        /// 返回一个表集合(DataTableCollection)表示查询得到的数据集
        /// </summary>
        /// <param name="connecttionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdText">T-SQL语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个表集合(DataTableCollection)表示查询得到的数据集</returns>
        public static DataTableCollection ExecuteTables(string connectionStringSign, string cmdText, SqlParameter[] commandParameters)
        {
            //自定义连接字符串
            if (connectionStringSign != "")
                SetConStr(connectionStringSign);

            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, commandParameters);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
            }
            DataTableCollection table = ds.Tables;
            return table;
        }

        /// <summary>
        /// ExecuteTables基方法，使用数据库连接对象
        /// 返回一个表集合(DataTableCollection)表示查询得到的数据集
        /// </summary>
        /// <param name="connecttionString">一个有效的数据库连接对象</param>
        /// <param name="cmdText">T-SQL语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个表集合(DataTableCollection)表示查询得到的数据集</returns>
        public static DataTableCollection ExecuteTables(SqlConnection sqlConnection, string cmdText, SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            using (sqlConnection)
            {
                PrepareCommand(cmd, sqlConnection, null, CommandType.Text, cmdText, commandParameters);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
            }
            DataTableCollection table = ds.Tables;
            return table;
        }

        /// <summary>
        /// ExecuteTablesSP无参数方法
        /// 返回一个表集合(DataTableCollection)表示查询得到的数据集
        /// </summary>
        /// <param name="cmdText">存储过程</param>
        /// <returns>返回一个表集合(DataTableCollection)表示查询得到的数据集</returns>
        public static DataTableCollection ExecuteTablesSP(string cmdText)
        {
            return ExecuteTablesSP("", cmdText, null);
        }

        /// <summary>
        /// ExecuteTablesSP有参数方法
        /// 返回一个表集合(DataTableCollection)表示查询得到的数据集
        /// </summary>
        /// <param name="cmdText">存储过程</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个表集合(DataTableCollection)表示查询得到的数据集</returns>
        public static DataTableCollection ExecuteTablesSP(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteTablesSP("", cmdText, commandParameters);
        }

        /// <summary>
        /// ExecuteTablesSP基方法，使用连接字符串标识
        /// 返回一个表集合(DataTableCollection)表示查询得到的数据集
        /// </summary>
        /// <param name="connecttionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdText">存储过程</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个表集合(DataTableCollection)表示查询得到的数据集</returns>
        public static DataTableCollection ExecuteTablesSP(string connectionStringSign, string cmdText, SqlParameter[] commandParameters)
        {
            //自定义连接字符串
            if (connectionStringSign != "")
                SetConStr(connectionStringSign);

            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, CommandType.StoredProcedure, cmdText, commandParameters);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
            }
            DataTableCollection table = ds.Tables;
            return table;
        }

        /// <summary>
        /// ExecuteTablesSP基方法，使用数据库连接对象
        /// 返回一个表集合(DataTableCollection)表示查询得到的数据集
        /// </summary>
        /// <param name="connecttionString">一个有效的数据库连接对象</param>
        /// <param name="cmdText">存储过程</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个表集合(DataTableCollection)表示查询得到的数据集</returns>
        public static DataTableCollection ExecuteTablesSP(SqlConnection sqlConnection, string cmdText, SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            using (sqlConnection)
            {
                PrepareCommand(cmd, sqlConnection, null, CommandType.StoredProcedure, cmdText, commandParameters);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
            }
            DataTableCollection table = ds.Tables;
            return table;
        }

        #endregion

        #region IsExists

        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns>bool结果</returns>
        public static bool Exists(string strSql)
        {
            int cmdresult = Convert.ToInt32(ExecuteScalar(strSql));
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>bool结果</returns>
        public static bool Exists(string strSql, params SqlParameter[] cmdParms)
        {
            int cmdresult = Convert.ToInt32(ExecuteScalar(strSql, cmdParms));
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="connectionStringSign">一个有效的数据库连接字符串标识</param>
        /// <param name="strSql">Sql语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>bool结果</returns>
        public static bool Exists(string connectionStringSign, string strSql, params SqlParameter[] cmdParms)
        {
            int cmdresult = Convert.ToInt32(ExecuteScalar(connectionStringSign, strSql, cmdParms));
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="sqlConnection">一个有效的数据库连接对象</param>
        /// <param name="strSql">Sql语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>bool结果</returns>
        public static bool Exists(SqlConnection sqlConnection, string strSql, params SqlParameter[] cmdParms)
        {
            int cmdresult = Convert.ToInt32(ExecuteScalar(sqlConnection, strSql, cmdParms));
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

    }

}