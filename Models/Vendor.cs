using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelImportWithColSelectionByAmit.Models {
    public class Vendor {
        public int Id { get; set; }
        public string VendorName { get; set; }
        public int? BankID { get; set; }
        public string BankName { get; set; }
        public int? BranchID { get; set; }
        public string BranchName { get; set; }
        public int? CurrencyID { get; set; }
        public string CurrencyName { get; set; }
        public int? StateID { get; set; }
        public string StateName { get; set; }
        public int? NatureID { get; set; }
        public string Nature { get; set; }

    }
}
