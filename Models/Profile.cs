using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelImportWithColSelectionByAmit.Models {
    public class Profile {
        public int  Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
        public string AlternativeId { get; set; }
        public string DrivingLicense { get; set; }
        public string Education { get; set; }
        public string MaritalStatus { get; set; }
        public string Children { get; set; }
    }
}
