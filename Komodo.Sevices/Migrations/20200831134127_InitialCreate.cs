using Microsoft.EntityFrameworkCore.Migrations;

namespace Komodo.Sevices.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommodityGroups",
                columns: table => new
                {
                    CommodityGroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommodityGroupCode = table.Column<string>(nullable: true),
                    CommodityGroupDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommodityGroups", x => x.CommodityGroupId);
                });

            migrationBuilder.CreateTable(
                name: "Commodities",
                columns: table => new
                {
                    CommodityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommodityCode = table.Column<string>(nullable: true),
                    CommodityDescription = table.Column<string>(nullable: true),
                    CommodityGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commodities", x => x.CommodityId);
                    table.ForeignKey(
                        name: "FK_Commodities_CommodityGroups_CommodityGroupId",
                        column: x => x.CommodityGroupId,
                        principalTable: "CommodityGroups",
                        principalColumn: "CommodityGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CommodityGroups",
                columns: new[] { "CommodityGroupId", "CommodityGroupCode", "CommodityGroupDescription" },
                values: new object[,]
                {
                    { 1, "0001", "CommodityGroup1" },
                    { 2, "0002", "CommodityGroup2" },
                    { 3, "0003", "CommodityGroup3" },
                    { 4, "0004", "CommodityGroup4" },
                    { 5, "0005", "CommodityGroup5" }
                });

            migrationBuilder.InsertData(
                table: "Commodities",
                columns: new[] { "CommodityId", "CommodityCode", "CommodityDescription", "CommodityGroupId" },
                values: new object[,]
                {
                    { 1, "0001", "Commodity1", 1 },
                    { 2, "0001", "Commodity2", 1 },
                    { 3, "0001", "Commodity3", 1 },
                    { 4, "0001", "Commodity4", 2 },
                    { 5, "0001", "Commodity5", 2 },
                    { 6, "0001", "Commodity6", 2 },
                    { 7, "0001", "Commodity7", 3 },
                    { 8, "0001", "Commodity8", 3 },
                    { 9, "0001", "Commodity9", 3 },
                    { 10, "0001", "Commodity10", 4 },
                    { 11, "0001", "Commodity11", 4 },
                    { 12, "0001", "Commodity12", 4 },
                    { 13, "0001", "Commodity13", 5 },
                    { 14, "0001", "Commodity14", 5 },
                    { 15, "0001", "Commodity15", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commodities_CommodityGroupId",
                table: "Commodities",
                column: "CommodityGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commodities");

            migrationBuilder.DropTable(
                name: "CommodityGroups");
        }
    }
}
