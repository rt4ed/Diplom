using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text.RegularExpressions;

namespace TaskService.CommonTypes.Sql
{
    public static class SqlDapper
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private static string _connString = string.Empty;

        /// <summary>
        /// Timeout for command
        /// </summary>
        private static int _commandTimeout = 100;

        public static void InitDapper(string connString, string commandTimeout)
        {
            if (connString is null)
                throw new ArgumentNullException("Check appsettings, there is no connection string!");

            _connString = connString;

            if (int.TryParse(commandTimeout, out var timeout))
                _commandTimeout = timeout;
        }

        public static ICollection<T> ExecuteQuerySP<T>(string sp, DynamicParameters? param = null)
        {
            using (IDbConnection conn = new SqlConnection(_connString))
            {
                conn.Open();
                return conn.Query<T>(
                    sql: sp,
                    param: param,
                    commandType: CommandType.StoredProcedure,
                    commandTimeout: _commandTimeout).ToList();
            }
        }

        public static void ExecuteNonQuerySP(string sp, DynamicParameters? param = null)
        {
            using (IDbConnection conn = new SqlConnection(_connString))
            {
                conn.Open();
                conn.Query(
                    sql: sp,
                    param: param,
                    commandType: CommandType.StoredProcedure,
                    commandTimeout: _commandTimeout);
            }
        }

        public static void ExecuteSqlNonQuery(string sql, object? param = null)
        {
            using (IDbConnection conn = new SqlConnection(_connString))
            {
                conn.Open();
                conn.Execute(
                    sql: sql,
                    param: param,
                    commandType: CommandType.Text,
                    commandTimeout: _commandTimeout);
            }
        }

        public static void ClearTable(string tableName) => ExecuteSqlNonQuery($"DELETE FROM {tableName}");


        public static DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            dataTable.TableName = typeof(T).FullName;
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        public static List<SqlBulkCopyColumnMapping> PrepareColumnMapping(DataTable table)
        {
            if (table is null)
                throw new ArgumentNullException(nameof(table));

            var columnMap = new List<SqlBulkCopyColumnMapping>();

            foreach (DataColumn column in table.Columns)
                columnMap.Add(new SqlBulkCopyColumnMapping(column.ColumnName, column.ColumnName));

            return columnMap;
        }

        public static void BulkInsertIntoTable(DataTable data, string tableName, List<SqlBulkCopyColumnMapping> map)
        {
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();

                using (var bulk = new SqlBulkCopy(conn))
                {
                    try
                    {
                        bulk.DestinationTableName = tableName;
                        map.ForEach(x => bulk.ColumnMappings.Add(x));
                        bulk.WriteToServer(data);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("Received an invalid column length from the bcp client for colid"))
                        {
                            string pattern = @"\d+";
                            Match match = Regex.Match(ex.Message.ToString(), pattern);
                            var index = Convert.ToInt32(match.Value) - 1;

                            FieldInfo fi = typeof(SqlBulkCopy).GetField("_sortedColumnMappings", BindingFlags.NonPublic | BindingFlags.Instance);
                            var sortedColumns = fi.GetValue(bulk);
                            var items = (Object[])sortedColumns.GetType().GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(sortedColumns);

                            FieldInfo itemdata = items[index].GetType().GetField("_metadata", BindingFlags.NonPublic | BindingFlags.Instance);
                            var metadata = itemdata.GetValue(items[index]);

                            var column = metadata.GetType().GetField("column", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).GetValue(metadata);
                            var length = metadata.GetType().GetField("length", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).GetValue(metadata);
                            throw new DataException(string.Format("Column: {0} contains data with a length greater than: {1}", column, length));
                        }

                        throw;

                        //throw new DataException($"Error on BulkInsert to table - {tableName}");
                    }
                }
            }
        }
    }
}
