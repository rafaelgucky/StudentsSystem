using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellersManager.Migrations
{
    public partial class InsertingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Students (Name, Password) VALUES ('Rafael', '1212')");
            migrationBuilder.Sql("INSERT INTO Days (DateTime, StudentId) VALUES ('2024-02-23', 1)");
            migrationBuilder.Sql("INSERT INTO Days (DateTime, StudentId) VALUES ('2024-02-23', 1)");
            migrationBuilder.Sql("INSERT INTO Days (DateTime, StudentId) VALUES ('2024-02-23', 1)");
            migrationBuilder.Sql("INSERT INTO Days (DateTime, StudentId) VALUES ('2024-02-23', 1)");
            migrationBuilder.Sql("INSERT INTO Days (DateTime, StudentId) VALUES ('2024-02-23', 1)");
            migrationBuilder.Sql("INSERT INTO Lessons (Name, DayId) VALUES ('Math', 1)");
            migrationBuilder.Sql("INSERT INTO Lessons (Name, DayId) VALUES ('Math', 1)");
            migrationBuilder.Sql("INSERT INTO Lessons (Name, DayId) VALUES ('Math', 1)");
            migrationBuilder.Sql("INSERT INTO Lessons (Name, DayId) VALUES ('Math', 1)");
            migrationBuilder.Sql("INSERT INTO Lessons (Name, DayId) VALUES ('Math', 1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE * FROM Students");
            migrationBuilder.Sql("DELETE * FROM Days");
            migrationBuilder.Sql("DELETE * FROM Lessons");
        }
    }
}
