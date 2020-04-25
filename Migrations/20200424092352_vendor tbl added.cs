using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExcelImportWithColSelectionByAmit.Migrations
{
    public partial class vendortbladded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VendorName = table.Column<string>(nullable: true),
                    BankID = table.Column<int>(nullable: false),
                    BankName = table.Column<string>(nullable: true),
                    BranchID = table.Column<int>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    CurrencyID = table.Column<int>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    StateID = table.Column<int>(nullable: false),
                    StateName = table.Column<string>(nullable: true),
                    NatureID = table.Column<int>(nullable: false),
                    Nature = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vendors");
        }
    }
}
