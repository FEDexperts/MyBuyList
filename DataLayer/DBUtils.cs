using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.Common;
//using ProperServices.Common.Log;

namespace MyBuyList.DataLayer
{
    internal class DBUtils
    {
        public enum Database
        {
            MyBuyListDB
        }

        #region Factory
        /// <summary>
        /// Resolve connection settings name.
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        private static string GetDbFactory(Database database)
        {
            // if more than a single database is to be used, this logic will be replaced with a switch.
            const string ConnectionString = "MyBuyListDB";

            return ConnectionString;
        } 
        #endregion

        #region Connection
        /// <summary>
        /// Returns a connection.
        /// </summary>
        /// <returns></returns>
        public static DbConnection GetConnection()
        {
            // retrieve connection
            string cnName = GetDbFactory(Database.MyBuyListDB);

            // retrieve connection
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[cnName];
            DbProviderFactory factory = DbProviderFactories.GetFactory(settings.ProviderName);

            // create connection
            DbConnection cn = factory.CreateConnection();
            cn.ConnectionString = settings.ConnectionString;
            return cn;
        }
        /// <summary>
        /// Returns a connection string.
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionString()
        {
            // retrieve connection
            string cnName = GetDbFactory(Database.MyBuyListDB);

            // retrieve connection
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[cnName];
            return settings.ConnectionString;
        } 
        #endregion

        #region Update
        /// <summary>
        /// Opens a connection and transaction.
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        public static DbTransaction BeginTransaction(Database database)
        {
            DbConnection cn = GetConnection();
            cn.Open();
            return cn.BeginTransaction();
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="ds"></param>
        public static void Update(DataSet ds)
        {
            DbTransaction transaction = BeginTransaction(Database.MyBuyListDB);
            try
            {
                Update(ds, transaction);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                //throw Logger.PublishException(ex, Logger.Level.Error);
            }
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="ds"></param>
        public static void Update(DataSet ds, DbTransaction transaction)
        {
            if (ds == null)
                throw new ArgumentNullException("ds");

            if (ds.Tables.Count == 0)
                return;

            Dictionary<string, DataTable> map = new Dictionary<string, DataTable>();
            foreach (DataTable table in ds.Tables)
                map.Add(table.TableName, table);

            List<string> tableNames = new List<string>(map.Keys);
            List<DataTable> tables = new List<DataTable>(map.Values);
            Update(Database.MyBuyListDB, tables.ToArray(), tableNames.ToArray(), null, transaction);
        }

        /// <summary>
        /// Update a datatable.
        /// </summary>
        /// <param name="table"></param>
        public static void Update(DataTable table)
        {
            Update(table, null);
        }

        /// <summary>
        /// Update a datatable.
        /// </summary>
        /// <param name="table"></param>
        public static void Update(DataTable table, string identity)
        {
            DbTransaction transaction = BeginTransaction(Database.MyBuyListDB);
            try
            {
                Update(table, identity, transaction);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                //throw Logger.PublishException(ex, Logger.Level.Error);
            }
        }

        /// <summary>
        /// Update a datatable.
        /// </summary>
        /// <param name="table"></param>
        public static void Update(DataTable table, string identity, DbTransaction transaction)
        {
            Update(Database.MyBuyListDB, new DataTable[] { table }, new string[] { table.TableName }, identity, transaction);
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="database"></param>
        /// <param name="tables"></param>
        /// <param name="tableNames"></param>
        /// <param name="identity"></param>
        /// <param name="transaction"></param>
        private static void Update(Database database, DataTable[] tables, string[] tableNames, string identity, DbTransaction transaction)
        {
            #region Validations
            if (transaction == null)
                throw new ArgumentNullException("transaction");

            if (tables == null)
                throw new ArgumentNullException("ds");

            if (tableNames == null)
                throw new ArgumentNullException("tableNames");

            if (tables.Length != tableNames.Length)
                throw new ArgumentException("tables and tableNames parameters cannot differ in size");
            #endregion

            // retrieve connection
            string cnName = GetDbFactory(database);

            // retrieve connection
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[cnName];
            DbProviderFactory factory = DbProviderFactories.GetFactory(settings.ProviderName);

            // create adapter
            using (DbDataAdapter adapter = factory.CreateDataAdapter())
            {
                // create commands
                for (int i = 0; i < tableNames.Length; ++i)
                {
                    using (DbCommand select = factory.CreateCommand())
                    {
                        string tableName = tableNames[i];
                        select.CommandText = string.Format("SELECT * FROM {0}", tableName);
                        select.Connection = transaction.Connection;
                        select.Transaction = transaction;

                        using (DbCommandBuilder builder = factory.CreateCommandBuilder())
                        {
                            // get table
                            DataTable table = tables[i];

                            adapter.SelectCommand = select;

                            builder.DataAdapter = adapter;

                            adapter.UpdateCommand = builder.GetUpdateCommand();
                            adapter.DeleteCommand = builder.GetDeleteCommand();
                            adapter.InsertCommand = builder.GetInsertCommand();

                            if (!string.IsNullOrEmpty(identity))
                                GenerateIdentityUpdate(factory, table, adapter, identity);

                            // update
                            adapter.Update(table);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Generates an alternate insert command for retrieving the identity value after insert.
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="table"></param>
        /// <param name="adapter"></param>
        /// <param name="identity"></param>
        private static void GenerateIdentityUpdate(DbProviderFactory factory, DataTable table, DbDataAdapter adapter, string identity)
        {
            if (!table.Columns.Contains(identity))
                return;

            DbCommand newInsertCommand = factory.CreateCommand();
            newInsertCommand.CommandType = CommandType.Text;
            newInsertCommand.CommandText = string.Format("{0}; SET @{1}=@@Identity;", adapter.InsertCommand.CommandText, identity);
            newInsertCommand.Connection = adapter.InsertCommand.Connection;
            newInsertCommand.Transaction = adapter.InsertCommand.Transaction;
            foreach (DbParameter p in adapter.InsertCommand.Parameters)
            {
                DbParameter newParameter = factory.CreateParameter();
                newParameter.DbType = p.DbType;
                newParameter.Direction = p.Direction;
                newParameter.ParameterName = p.ParameterName;
                newParameter.Size = p.Size;
                newParameter.SourceColumn = p.SourceColumn;
                newParameter.SourceVersion = p.SourceVersion;

                newInsertCommand.Parameters.Add(newParameter);
            }

            // create identity parameter
            DbParameter id = factory.CreateParameter();
            id.DbType = DbType.Int32;
            id.SourceColumn = identity;
            id.Direction = ParameterDirection.Output;
            id.ParameterName = "@" + identity;

            newInsertCommand.Parameters.Add(id);

            adapter.InsertCommand = newInsertCommand;
        } 
        #endregion

        #region Execute
        /// <summary>
        /// Executes a query which has no return value.
        /// </summary>
        /// <param name="database"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        public static void ExecuteNonQuery(Database database, string commandText, CommandType commandType)
        {
            // retrieve connection
            string cnName = GetDbFactory(database);

            // retrieve connection
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[cnName];
            DbProviderFactory factory = DbProviderFactories.GetFactory(settings.ProviderName);

            // create connection
            using (DbConnection cn = factory.CreateConnection())
            {
                cn.ConnectionString = settings.ConnectionString;
                cn.Open();

                using (DbCommand command = factory.CreateCommand())
                {
                    command.Connection = cn;
                    command.CommandText = commandText;
                    command.CommandType = commandType;

                    command.ExecuteNonQuery();
                }
            }
        } 
        #endregion

        #region Load data
        /// <summary>
        /// Loads a dataset.
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="query"></param>
        /// <param name="tableName"></param>
        public static void LoadDataSet(DataSet ds, string query, string tableName)
        {
            LoadDataSet(Database.MyBuyListDB, ds, new string[] { query }, new string[] { tableName });
        }

        /// <summary>
        /// Loads a dataset.
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="queries"></param>
        /// <param name="tableNames"></param>
        public static void LoadDataSet(DataSet ds, string[] queries, string[] tableNames)
        {
            LoadDataSet(Database.MyBuyListDB, ds, queries, tableNames);
        }

        /// <summary>
        /// Loads a dataset.
        /// </summary>
        /// <param name="cnName"></param>
        /// <param name="ds"></param>
        /// <param name="queries"></param>
        /// <param name="tableNames"></param>
        private static void LoadDataSet(Database database, DataSet ds, string[] queries, string[] tableNames)
        {
            #region Validations
            if (ds == null)
                throw new ArgumentNullException("ds");

            if (queries == null || queries.Length == 0)
                return;

            if (tableNames == null || tableNames.Length != queries.Length)
                throw new ArgumentNullException("tableNames", "tableNames cannot be different in number than the queries parameter");
            #endregion

            // retrieve connection
            string cnName = GetDbFactory(database);

            // retrieve connection
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[cnName];
            DbProviderFactory factory = DbProviderFactories.GetFactory(settings.ProviderName);

            // create connection
            using (DbConnection cn = factory.CreateConnection())
            {
                cn.ConnectionString = settings.ConnectionString;

                // create adapter
                using (DbDataAdapter adapter = factory.CreateDataAdapter())
                {
                    // create commands
                    for (int i = 0; i < queries.Length; ++i)
                    {
                        string select = queries[i];
                        using (DbCommand command = factory.CreateCommand())
                        {
                            command.CommandText = select;
                            command.Connection = cn;

                            // fill table
                            DataTable table = new DataTable();
                            table.TableName = tableNames[i];

                            adapter.SelectCommand = command;
                            adapter.Fill(table);

                            // add to dataset
                            ds.Tables.Add(table);
                        }
                    }
                }
            }
        } 
        #endregion

        #region EnsureNotDbNull
        /// <summary>
        /// Returns the item or the specified default.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static T EnsureNotDbNull<T>(object item, T defaultValue)
        {
            if (item == DBNull.Value)
                return defaultValue;

            return (T)item;
        }

        /// <summary>
        /// Returns the item or the default for the given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static T EnsureNotDbNull<T>(object item)
        {
            if (item == DBNull.Value)
                return default(T);

            return (T)item;
        } 
        #endregion
    }
}
