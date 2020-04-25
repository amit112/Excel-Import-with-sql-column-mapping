using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelImportWithColSelectionByAmit.Models {
    public class ExcelImportDbContext:DbContext  {
        public ExcelImportDbContext(DbContextOptions<ExcelImportDbContext> options) : base(options)
        {
        }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
    }
}
