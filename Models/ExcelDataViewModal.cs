using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelImportWithColSelectionByAmit.Models {
    public class ExcelDataViewModal {
        public string TableName { get; set; }
        public List<ExcelToSqlMap> ExcelToSqlMapping { get; set; }
        public List<dynamic> ExcelData {get;set;}
    }
    public class ExcelToSqlMap {
          public string excelColumn { get; set; }
          public string sqlColumn { get; set; }
    }
}
