using Microsoft.EntityFrameworkCore.Migrations;

namespace Sport_Retail.Migrations
{
    public partial class PopulateCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Category (Name) VALUES ('Sporty wodne')");
            migrationBuilder.Sql("INSERT INTO Category (Name) VALUES ('Piłka nożna')");
            migrationBuilder.Sql("INSERT INTO Category (Name) VALUES ('Akcesoria')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
