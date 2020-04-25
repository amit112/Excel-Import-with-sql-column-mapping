using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExcelImportWithColSelectionByAmit.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.IO;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;
using System.Text;
using System.Data.SqlClient;
using ExcelImportWithColSelectionByAmit.Interfaces;

namespace ExcelImportWithColSelectionByAmit.Controllers {
    public class HomeController : Controller {

        private ISqlHelper sqlHelper;
        public HomeController(ISqlHelper _sqlHelper)
        {
            sqlHelper = _sqlHelper;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TableNames = (await sqlHelper.GetSqlTableslist()).Select(tableName => new SelectListItem()
            {
                Text = tableName,
                Value = tableName
            }).ToList();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<JsonResult> PostExcelData()
        {
            ExcelDataViewModal result;
            string message = "";
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var response = await reader.ReadToEndAsync();
                result = JsonConvert.DeserializeObject<ExcelDataViewModal>(response);

            }

            DataTable dt = new DataTable();
            for(int i=0; i < result.ExcelData.Count; i++) {
                if (i == 0)
                {
                    var primaryKeys = await sqlHelper.GetPrimaryKeys(result.TableName);
                    if (primaryKeys.Count > 0)
                    {
                        if (!dt.Columns.Contains(primaryKeys[0]))
                        {
                            dt.Columns.Add(primaryKeys[0], typeof(int));
                        }
                    }
                    foreach (var col in result.ExcelToSqlMapping)
                    {
                        if (!dt.Columns.Contains(col.sqlColumn))
                        {
                            Type type = GetDataType(result.ExcelData[i][col.excelColumn].Value);
                            dt.Columns.Add(col.sqlColumn, type);
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                for(int j =0; j <result.ExcelToSqlMapping.Count; j++) 
                {
                    if (result.ExcelData[i][result.ExcelToSqlMapping[j].excelColumn] != null)
                    {
                        dr[result.ExcelToSqlMapping[j].sqlColumn] = result.ExcelData[i][result.ExcelToSqlMapping[j].excelColumn].Value;
                    }
                };
                dt.Rows.Add(dr);

            }

            if (dt.Rows.Count > 0)
            {
                message = await sqlHelper.InsertBulk(result.TableName, dt);
            }

            return Json(message);

        }

        public async Task<List<string>> GetTableColNames(string tableName)
        {
            return await sqlHelper.GetTableColNames(tableName);
        }


        private Type GetDataType(string input)
        {
            Type type = typeof(string);
            if (Int32.TryParse(input, out _))
            {
                type = typeof(int);
            }
            else if (double.TryParse(input, out _))
            {
                type = typeof(double);

            }
            else if (bool.TryParse(input, out _))
            {
                type = typeof(bool);

            }
            else if (DateTime.TryParse(input, out _))
            {
                type = typeof(DateTime);

            }
            return type;
        }
    }


}