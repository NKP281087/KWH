//using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using KWH.DAL.Entities;

namespace KWH.DAL.DataContext
{
    public partial class KWHDBContext
    {
        /// <summary>
        /// The minimum parameters length
        /// </summary>
        private const int MINPARAMETERSLENGTH = 1;

        #region Public Methods

         DbContextOptions<KWHDBContext> options;
        

        /// <summary>
        /// Executes the bulk insert.
        /// </summary>
        /// <param name="routineName">Name of the routine.</param>
        /// <param name="dataTable">The data Table.</param>
        /// <param name="script">The script.</param>
        /// <returns>Inserted Value</returns>
        public async Task<string> ExecuteBulkInsert(string routineName, DataTable dataTable, string script)
        {
            int scriptresult = 0;
            scriptresult = ExecuteScriptNonQuery(script);
            using (KWHDBContext context = new KWHDBContext())
            {
                string connectionString = context.Database.Connection.ConnectionString;
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
                builder.ConnectTimeout = 100000;
                SqlConnection con = new SqlConnection(builder.ConnectionString);
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    using (SqlBulkCopy s = new SqlBulkCopy(con))
                    {
                        s.DestinationTableName = routineName;
                        foreach (var column in dataTable.Columns)
                        {
                            s.ColumnMappings.Add(column.ToString(), column.ToString());
                        }

                        s.WriteToServer(dataTable);
                    }

                    return await Task.FromResult(string.Empty);
                }
            }
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="routineName">Name of the routine.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Data table as object</returns>
        public async Task<DataTable> ExecuteQuery(string routineName, params object[] parameters)
        {
            DataTable dt = new DataTable();
            try
            {
                using (KWHDBContext context = new KWHDBContext())
                {
                    string connectionString = context.Database.Connection.ConnectionString;
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
                    builder.ConnectTimeout = 2500;
                    SqlConnection con = new SqlConnection(builder.ConnectionString);
                    System.Data.Common.DbDataReader sqlReader;
                    con.Open();

                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = routineName;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(parameters);
                        cmd.CommandTimeout = 300;
                        sqlReader = (System.Data.Common.DbDataReader)cmd.ExecuteReader();

                        while (!sqlReader.IsClosed)
                        {
                            dt = new DataTable();
                            dt.Load(sqlReader);
                        }

                        con.Close();
                        return await Task.FromResult(dt);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
                //Logging.LogManager.WriteInformational("Exception  Occured in " + routineName);
                //Logging.LogManager.Write(e);
                //return dt;

            }
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="routineName">Name of the routine.</param>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Return the Dataset</returns>
        public Task<DataSet> ExecuteQuery(string routineName, string datasetName, params object[] parameters)
        {
            return this.ExecuteQueryAsyncImpl(routineName, datasetName, parameters);
        }

         

        /// <summary>
        /// Executes the routine.
        /// </summary>
        /// <typeparam name="T">T Type</typeparam>
        /// <param name="routineName">Name of the routine.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>IEnumerable T type</returns>
        /// 


        public async Task<IEnumerable<T>> ExecuteRoutine<T>(string routineName, IDictionary<string, object> parameters)
        {
            var typeList = default(IEnumerable<T>);
            try
            {
                using (KWHDBContext context = new KWHDBContext(options))
                {
                    if (!string.IsNullOrEmpty(routineName))
                    {
                        var sqlParameters = new List<SqlParameter>();

                        if (parameters != default(IDictionary<string, object>) && parameters.Count >= MINPARAMETERSLENGTH)
                        {
                            foreach (var key in parameters.Keys)
                            {
                                sqlParameters.Add(new SqlParameter(key, parameters[key]));
                            }
                        }

                        Database.SetCommandTimeout(TimeSpan.FromMilliseconds(300));
                        typeList = context.Database.SqlQuery<T>().FromSqlRaw(routineName, sqlParameters.ToArray()).ToList();

                        var data = context.Set<Section>()
                                  .FromSqlRaw("SELECT * FROM Product WHERE 1=1")
    .Select(t => new ProductListEntry { Id = t.Id, Name = t.Name })
    .ToList();

                    }
                }

                return await Task.FromResult(typeList);
            }
            catch (Exception exe)
            {
                throw;
                //Logging.LogManager.WriteInformational("Exception  Occured in " + routineName);
                //Logging.LogManager.Write(exe);
                //return await Task.FromResult(typeList);
            }

        }

        /// <summary>
        /// Executes the routine by data table.
        /// </summary>
        /// <typeparam name="T">T Type</typeparam>
        /// <param name="routineName">Name of the routine.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>IEnumerable T type</returns>
        public Task<IEnumerable<T>> ExecuteRoutineByDataTable<T>(string routineName, SqlParameter[] parameters)
        {
            return this.ExecuteRoutinebydtAsync<T>(routineName, parameters);
        }

        public async Task<DataSet> SelectDataSet(string procedureName, SqlParameter[] parameters)
        {
            DataSet dsResults = new DataSet("dsResult");// null;
            try
            {
                using (KWHDBContext context = new KWHDBContext())
                {
                    string connectionString = context.Database.Connection.ConnectionString;
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
                    builder.ConnectTimeout = 2500;

                    using (SqlConnection con = new SqlConnection(builder.ConnectionString))
                    {
                        con.Open();
                        using (SqlCommand cmd = con.CreateCommand())
                        {
                            cmd.CommandText = procedureName;
                            cmd.CommandType = CommandType.StoredProcedure;
                            foreach (SqlParameter param in parameters)
                            {
                                cmd.Parameters.Add(param);
                            }
                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = cmd;

                            da.Fill(dsResults);
                            return await Task.FromResult(dsResults); //dsResults;
                        }
                    }
                }
            }
            catch (Exception exe)
            {
                throw;
                //Logging.LogManager.WriteInformational("Exception  Occured in " + procedureName);
                //Logging.LogManager.Write(exe);
                //return await Task.FromResult(dsResults); //dsResults;
            }

            // return this.ExecuteQueryAsyncImpl(procedureName, , parameters);
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <typeparam name="T">T Types</typeparam>
        /// <param name="mapEntities">The map entities.</param>
        /// <param name="exec">The execute.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>T Type</returns>
        protected virtual T ExecuteReader<T>(Func<DbDataReader, T> mapEntities, string exec, params object[] parameters)
        {
            KWHDBContext databaseContext = default(KWHDBContext);
            using (var conn = new SqlConnection(databaseContext.Database.Connection.ConnectionString))
            {
                using (var command = new SqlCommand(exec, conn))
                {
                    conn.Open();
                    command.Parameters.AddRange(parameters);
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        if (mapEntities != null)
                        {
                            T data = mapEntities(reader);
                            return data;
                        }
                    }
                }
            }

            return default(T);
        }

        #endregion Protected Methods

        #region Private Methods

        /// <summary>
        /// Executes the script non query.
        /// </summary>
        /// <param name="script">The script.</param>
        /// <returns>
        /// The Inserted Value
        /// </returns>
        private static int ExecuteScriptNonQuery(string script)
        {
            int result = 0;
            try
            {
                using (KWHDBContext context = new KWHDBContext())
                {
                    string connectionString = context.Database.Connection.ConnectionString;
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
                    builder.ConnectTimeout = 2500;

                    using (SqlConnection con = new SqlConnection(builder.ConnectionString))
                    {
                        con.Open();
                        using (SqlCommand cmd = con.CreateCommand())
                        {
                            cmd.CommandText = script;
                            result = cmd.ExecuteNonQuery();
                            return result;
                        }
                    }
                }
            }
            catch (Exception exe)
            {
                throw;
                //Logging.LogManager.WriteInformational("Exception  Occured in " + script);
                //Logging.LogManager.Write(exe);
                //return result;
            }

        }

        /// <summary>
        /// Executes the routine by data table asynchronous.
        /// </summary>
        /// <typeparam name="T">T Type</typeparam>
        /// <param name="routineName">Name of the routine.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>IEnumerable T type</returns>
        private async Task<IEnumerable<T>> ExecuteRoutinebydtAsync<T>(string routineName, SqlParameter[] parameters)
        {
            try
            {
                var typeList = default(IEnumerable<T>);

                if (!string.IsNullOrEmpty(routineName))
                {
                    var sqlParameters = new List<SqlParameter>();

                    if (parameters != default(SqlParameter[]) &&
                        parameters.Length >= MINPARAMETERSLENGTH)
                    {
                        foreach (SqlParameter p in parameters)
                        {
                            ////check for derived output value with no value assigned
                            if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
                            {
                                p.Value = DBNull.Value;
                            }

                            sqlParameters.Add(p);
                        }
                    }
                    Database.CommandTimeout = 300;
                    typeList = Database.SqlQuery<T>(routineName, sqlParameters.ToArray()).ToList();
                }

                return await Task.FromResult(typeList);
            }
            catch (Exception ex)
            {
                //Logging.LogManager.WriteInformational("Exception  Occured in " + routineName);
                //Logging.LogManager.Write(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Executes the query asynchronous implementation.
        /// </summary>
        /// <param name="routineName">Name of the routine.</param>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Data Set</returns>
        private async Task<DataSet> ExecuteQueryAsyncImpl(string routineName, string datasetName, object[] parameters) // Not ref or out!
        {
            DataSet ds = new DataSet(datasetName);
            try
            {
                using (KWHDBContext context = new KWHDBContext())
                {
                    string connectionString = context.Database.Connection.ConnectionString;
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
                    builder.ConnectTimeout = 2500;
                    SqlConnection con = new SqlConnection(builder.ConnectionString);
                    SqlDataAdapter da = new SqlDataAdapter();
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = routineName;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(parameters);
                        cmd.CommandTimeout = 300;
                        da.SelectCommand = cmd;

                        da.Fill(ds);
                        return await Task.FromResult(ds);
                    }
                }
            }
            catch (Exception exe)
            {
                //Logging.LogManager.WriteInformational("Exception  Occured in " + routineName);
                //Logging.LogManager.Write(exe);
                //return await Task.FromResult(ds);
                throw;
            }

        }

        #endregion Private Methods


        public DataSet ExecutesqlQueryList(string sql, string parameter, string parameterValue)
        {
            try
            {
                if (sql != null && sql.Length > 7)
                {
                    DbCommand cmd = Database.Connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    DbParameter param = cmd.CreateParameter();
                    param.ParameterName = parameter;
                    param.DbType = DbType.String;
                    param.Size = 8;
                    param.Direction = ParameterDirection.Input;
                    param.Value = parameterValue;
                    cmd.Parameters.Add(param);
                    IDbDataAdapter adp = new SqlDataAdapter();
                    adp.SelectCommand = cmd;
                    DataSet dataset = new DataSet();
                    adp.Fill(dataset);

                    return dataset;
                }
                return null;
            }
            catch (Exception exe)
            {
                throw;
                //Logging.LogManager.WriteInformational("Exception  Occured in " + sql);
                //Logging.LogManager.Write(exe);
                //return null;
            }

        }
    }
}
