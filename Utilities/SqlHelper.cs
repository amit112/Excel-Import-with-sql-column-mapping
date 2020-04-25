using ExcelImportWithColSelectionByAmit.Interfaces;
using ExcelImportWithColSelectionByAmit.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelImportWithColSelectionByAmit.Utilities {
   

    public class SqlHelper : ISqlHelper {

        private ExcelImportDbContext _db;
        public SqlHelper(ExcelImportDbContext db)
        {
            _db = db;
        }
        public async Task<List<string>> GetSqlTableslist()
        {
            List<string> tableNames = new List<string>();
            try
            {
                using (var command = _db.Database.GetDbConnection().CreateCommand())
                {
                    string dbName = _db.Database.GetDbConnection().Database;
                    command.CommandText = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' and TABLE_NAME not in ('__EFMigrationsHistory') AND TABLE_CATALOG = '" + dbName + "'";
                    _db.Database.OpenConnection();
                    using (var rdr = command.ExecuteReader())
                    {
                        while (await rdr.ReadAsync())
                        {
                            tableNames.Add(rdr["TABLE_NAME"].ToString());
                        }
                    }
                }

                return tableNames;
            }
            catch
            {
                return tableNames;

            }
        }

        public async Task<List<string>> GetTableColNames(string tableName)
        {
            List<string> columns = new List<string>();
            using (var command = _db.Database.GetDbConnection().CreateCommand())
            {
                string dbName = _db.Database.GetDbConnection().Database;
                command.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + tableName + "' ORDER BY ORDINAL_POSITION";
                _db.Database.OpenConnection();
                using (var rdr = command.ExecuteReader())
                {
                    while (await rdr.ReadAsync())
                    {
                        columns.Add(rdr["COLUMN_NAME"].ToString());
                    }
                }
            }
            return columns;
        }

        public async Task<List<string>> GetPrimaryKeys(string tableName)
        {
            List<string> primaryKeys = new List<string>();
            using (var command = _db.Database.GetDbConnection().CreateCommand())
            {
                string dbName = _db.Database.GetDbConnection().Database;
                command.CommandText = "SELECT KU.table_name as TABLENAME, column_name as PRIMARYKEYCOLUMN FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC"
                + " INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KU ON TC.CONSTRAINT_TYPE = 'PRIMARY KEY' AND TC.CONSTRAINT_NAME = KU.CONSTRAINT_NAME AND" +
             " KU.table_name = '" + tableName + "'ORDER BY KU.TABLE_NAME, KU.ORDINAL_POSITION";
                _db.Database.OpenConnection();
                using (var rdr = command.ExecuteReader())
                {
                    while (await rdr.ReadAsync())
                    {
                        primaryKeys.Add(rdr["PRIMARYKEYCOLUMN"].ToString());
                    }
                }
            }
            return primaryKeys;
        }

        public async Task<string> InsertBulk(string tableName, DataTable dt)
        {
            string message = "";
            using (var connection = new SqlConnection(_db.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();

                using (var tran = connection.BeginTransaction(IsolationLevel.ReadCommitted))
                {

                    var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, tran)
                    {
                        DestinationTableName = "[dbo].[" + tableName + "]"
                    };
                    try
                    {
                        await bulkCopy.WriteToServerAsync(dt);
                        tran.Commit();
                        message = dt.Rows.Count + " records inserted!";
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        message = ex.Message;
                    }
                }
            }
            return message;

        }

    }
}
