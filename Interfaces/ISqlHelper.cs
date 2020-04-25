using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelImportWithColSelectionByAmit.Interfaces {
    public interface ISqlHelper {
        Task<List<string>> GetPrimaryKeys(string tableName);
        Task<List<string>> GetSqlTableslist();
        Task<List<string>> GetTableColNames(string tableName);
        Task<string> InsertBulk(string tableName, DataTable dt);
    }
}
