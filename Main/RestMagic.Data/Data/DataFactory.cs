


using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;


namespace RestMagic.Lib.Data
{

    public class DataFactory : SchemaFactory, IDataFactory, IDisposable
    {
        /******************************************************************************************************/

        #region Fields
        public static string PrimaryConnectionString = string.Empty;
        public static string WebDirectory = "";
        private readonly bool keepOpen = false;
        private string transName = "";
        private bool hasTransaction = false;
        private SqlConnection connection; // changing from readonly to enable connection reset to new timeout
        private SqlCommand command;
        private SqlDataAdapter dataAdapter;
        private int sqlTimeOutInSeconds = 30;

        
        private int SqlTimeOutInSeconds
        {
            get { return sqlTimeOutInSeconds; }
            set
            {

                sqlTimeOutInSeconds = value;
                connection = InitializeSQLConnection();
            }
        }


        SqlTransaction transaction;

        #endregion

        /******************************************************************************************************/

        #region Constructors



        private SqlConnection InitializeSQLConnection()
        {
            // forced override of connString
            string connectionString = string.Empty;

            return new SqlConnection(PrimaryConnectionString);
        }


        public DataFactory()
        {

            connection = InitializeSQLConnection();
        }

        

       


        #endregion


        /******************************************************************************************************/

        #region Methods

        /// <summary>
        /// Opens the connection as appropriate per biz rules and status.  Also establishes and sets transaction if specified
        /// </summary>
        private SqlConnection OpenConnectionAsAppropriate()
        {
            if (connection.State == ConnectionState.Broken || connection.State == ConnectionState.Closed)
            {
                connection.ConnectionString = PrimaryConnectionString;
                connection.Open();

                command = new SqlCommand { Connection = connection };
                command.CommandTimeout = SqlTimeOutInSeconds;
                if (hasTransaction)
                {
                    // Start a local transaction.
                    transaction = connection.BeginTransaction(transName);
                    command.Transaction = transaction;
                }
            }
            return connection;
        }

        /// <summary>
        /// Closes the connection as appropriate per keepOpen specification for transactions.
        /// </summary>
        private void CloseConnectionAsAppropriate()
        {
            if (!keepOpen)
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Commits the transaction.
        /// </summary>
        public void CommitTransaction()
        {
            transaction.Commit();
        }

        // TODO: Remove Dead Code
        public List<SqlParameter> GetParameterListForUpsert(DataModel objectToUse)
        {
            List<SqlParameter> result = new List<SqlParameter>();
            try
            {
                foreach (PropertyInfo propertyInfo in objectToUse.GetType().GetProperties())
                {
                    if (!Attribute.IsDefined(propertyInfo, typeof(ExcludeAsSqlParam)))
                    {
                        object paramValue = propertyInfo.GetValue(objectToUse);


                        if (objectToUse.RuleSet.ContainsKey(propertyInfo.PropertyType))
                        {
                            var ruleType = objectToUse.RuleSet[propertyInfo.PropertyType];

                            if (ruleType.GetType() == typeof(RulesCaster))
                            {
                                paramValue = Convert.ChangeType(paramValue, (ruleType as RulesCaster).CastTypeParameter);
                            }
                            else { }
                        }

                        result.Add(new SqlParameter("@" + propertyInfo.Name, paramValue));
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return result;
        }




        /// <summary>
        /// Rollbacks the transaction.
        /// </summary>
        public void RollbackTransaction()
        {
            //if (transaction != null && connection.State == ConnectionState.Open)
            //{
            //    transaction.Rollback();
            //}
            //else
            //{
            //    //TODO: log error couldn't rollback transaction
            //}
            try
            {
                transaction.Rollback();
            }
            catch (Exception e)
            {
                //TODO: log error couldn't rollback transaction
                throw (e);
            }
        }

        /// <summary>
        /// Sets the transaction.
        /// </summary>
        /// <param name="transactionName">Name of the transaction.</param>
        public void SetTransaction(string transactionName)
        {
            //command.Transaction = new SqlTransaction();
            transName = transactionName;
            hasTransaction = true;

            // Start a local transaction
            if (connection.State == ConnectionState.Open)
            {
                transaction = connection.BeginTransaction(transName);
                command.Transaction = transaction;
            }

        }



        /// <summary>
        /// Clears the transaction string and bool setting.  Does not end existing transactions.
        /// </summary>
        public void ClearTransaction()
        {
            transName = "";
            hasTransaction = false;
        }


        public DataSet GetDataSet(string sqlQueryString, List<SqlParameter> parameters)
        {
            int returnValue = -1; // don't get return value
            return GetDataSet(sqlQueryString, parameters, ref returnValue);
        }

        public static DataSet GetDataSetStatic(string sqlQueryString, List<SqlParameter> parameters)
        {
            int returnValue = -1; // don't get return value
            return GetDataSetStatic(sqlQueryString, parameters, ref returnValue);
        }
        public static DataSet GetDataSetStatic(string sqlQueryString, List<SqlParameter> parameters, ref int returnValue)
        {

            DataFactory dbLayer = new DataFactory();
            DataSet result = dbLayer.GetDataSet(sqlQueryString, parameters, ref returnValue);
            dbLayer.Dispose();
            return result;
        }


        /// <summary>
        /// Gets a data set.
        /// </summary>
        /// <param name="sqlQueryString">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The representing DataSet</returns>
        public DataSet GetDataSet(string sqlQueryString, List<SqlParameter> parameters, ref int returnValue)
        {
            DataSet ds = new DataSet();
            int tempReturn = returnValue;
            try
            {

                using (var conn = OpenConnectionAsAppropriate())
                {

                    // TODO: below determination is fragile
                    command.CommandType = !sqlQueryString.Contains(" ") ? CommandType.StoredProcedure : CommandType.Text;
                    command.CommandText = sqlQueryString;
                    command.Parameters.Clear();

                    if (parameters != null)
                    {

                        foreach (SqlParameter p in parameters)
                        {

                            command.Parameters.Add(p);

                        }
                        if (tempReturn >= 0)
                        {
                            var param = new SqlParameter("ReturnValue", DBNull.Value);
                            param.Direction = ParameterDirection.ReturnValue;
                            command.Parameters.Add(param);
                        }
                    }

                    dataAdapter = new SqlDataAdapter(command);

                    ShowSQLCall(command);

                    dataAdapter.Fill(ds);
                    // need to think through no return value
                    if (tempReturn >= 0)
                        tempReturn = (dataAdapter.SelectCommand.Parameters["ReturnValue"] == null ? 0 : Convert.ToInt32(dataAdapter.SelectCommand.Parameters["ReturnValue"].Value));


                }
            }

            catch (Exception e)
            {
                throw (e);
            }

            command.Parameters.Clear();
            returnValue = tempReturn;

            return ds;
        }

        public string ShowSQLCall(SqlCommand command)
        {
            string result = "\r\n" + command.CommandText + " ";
            // this was not picking the params off so am sending them through as a separate param for now.
            //foreach (SqlParameter param in command.Parameters)
            foreach (SqlParameter param in command.Parameters)
            {
                result += param.ParameterName + " = " + param.Value + ",";
            }
            result = result.Substring(0, result.Length - 1);
            result += "\r\n";
#if DEBUG
            Debug.WriteLine(result);
#endif
            return result;
        }


        private DataTable CreateDataTableFromReader(SqlDataReader reader)
        {
            DataTable dt = new DataTable();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                dt.Columns.Add(new DataColumn(((IDataRecord)reader).GetName(i), ((IDataRecord)reader).GetFieldType(i)));
            }
            return dt;
        }

        /// <summary>
        /// Gets the data set.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <returns>The representing DataSet</returns>
        public DataSet GetDataSet(string sql)
        {
            DataSet ds = new DataSet();
            try
            {
                using (var conn = OpenConnectionAsAppropriate())
                {

                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.Parameters.Clear();

                    dataAdapter = new SqlDataAdapter(command);


                    dataAdapter.Fill(ds);


                    command.Parameters.Clear();

                }



            }
            catch (Exception e)
            {
                throw (e);
            }
            CloseConnectionAsAppropriate();
            return ds;
        }

        public void ExecuteNonQuery(string sqlQueryString, string PassthroughParams)
        {
            try
            {

                using (var conn = OpenConnectionAsAppropriate())
                {

                    command.CommandType = CommandType.Text;

                    command.CommandText = "EXEC " + sqlQueryString + " " + PassthroughParams;
                    command.Parameters.Clear();

                    ShowSQLCall(command);
                    command.ExecuteNonQuery();



                }
                CloseConnectionAsAppropriate();
            }


            catch (Exception e)
            {
                throw (e);
            }

        }


        public void ExecuteNonQuery(string sqlQueryString, Dictionary<string, object> parms)
        {
            List<SqlParameter> newParams = new List<SqlParameter>();
            foreach (var o in parms)
            {
                newParams.Add(new SqlParameter(o.Key.ToString(), o.Value));
            }

            ExecuteNonQuery(sqlQueryString, newParams);
        }

        public void ExecuteNonQuery(string sqlQueryString, List<SqlParameter> parameters)
        {
            long returnValue = -1;
            ExecuteNonQuery(sqlQueryString, parameters, ref returnValue);
        }

        /// <summary>
        /// Executes a SQL non query.
        /// </summary>
        /// <param name="sqlQueryString">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        public void ExecuteNonQuery(string sqlQueryString, List<SqlParameter> parameters, ref long returnValue)
        {
            long tempResult = returnValue;
            try
            {

                using (var conn = OpenConnectionAsAppropriate())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = sqlQueryString;
                    command.Parameters.Clear();

                    if (parameters != null)
                    {
                        //command.Parameters.Add(parameters);
                        foreach (SqlParameter p in parameters)
                        {
                            command.Parameters.Add(p);
                        }
                        if (tempResult >= 0)
                        {
                            var param = new SqlParameter("ReturnValue", DBNull.Value);
                            param.Direction = ParameterDirection.ReturnValue;
                            command.Parameters.Add(param);
                        }
                    }



                    ShowSQLCall(command);
                    command.ExecuteNonQuery();
                    if (tempResult >= 0)
                        tempResult = (command.Parameters["ReturnValue"] == null ? 0 : Convert.ToInt32(command.Parameters["ReturnValue"].Value));

                    command.Parameters.Clear();
                }
            }

            catch (Exception ex)
            {
                if (!sqlQueryString.ToUpper().Contains("spInsertLog".ToUpper()))
                    throw (ex);
            }
            CloseConnectionAsAppropriate();
        }

        /// <summary>
        /// Executes a SQL non query.
        /// </summary>
        /// <param name="sqlCommand">The SQL command.</param>
        public void ExecuteNonQuery(string sqlCommand)
        {
            try
            {

                using (var conn = OpenConnectionAsAppropriate())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = sqlCommand;
                    command.Parameters.Clear();
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }

                CloseConnectionAsAppropriate();
            }
            catch (Exception ex)
            {

                throw (ex);
            }

        }

        /// <summary>
        /// Executes the scalar SQL call.
        /// </summary>
        /// <param name="sqlQueryString">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Scalar value</returns>
        public object ExecuteScalar(string sqlQueryString, List<SqlParameter> parameters)
        {
            object retVal = null;

            try
            {
                
                    using (var conn = OpenConnectionAsAppropriate())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = sqlQueryString;
                        command.Parameters.Clear();

                        if (parameters != null)
                        {
                            //command.Parameters.Add(parameters);
                            foreach (SqlParameter p in parameters)
                            {
                                command.Parameters.Add(p);
                            }
                        }

                        retVal = command.ExecuteScalar();
                        command.Parameters.Clear();
                    }
                 
                CloseConnectionAsAppropriate();
            }
            catch (Exception ex)
            {

                throw (ex);
            }

            return retVal;
        }


        /// <summary>
        /// Forces connection close, and kills all other resources.  WARNING: This does NOT do anything with transactions.
        /// </summary>
        public void Dispose()
        {
            CloseConnection();

            if (command != null)
            {
                command.Connection = null;
                command = null;
            }
        }

        /// <summary>
        /// Closes the connection.
        /// </summary>
        public void CloseConnection()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }


        #endregion

        /******************************************************************************************************/



     

        public static void ExecuteNonquery(string v, List<SqlParameter> list)
        {

            DataFactory dbLayer = new DataFactory();
            dbLayer.ExecuteNonQuery(v, list);
            dbLayer.Dispose();
        }
        public static void ExecuteNonquery(string sql)
        {
            DataFactory dbLayer = new DataFactory();
            dbLayer.ExecuteNonQuery(sql);
            dbLayer.Dispose();

        }
    }

}