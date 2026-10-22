using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AccountingProject.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PoolAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MonthlyLimit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Debts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Creditor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DueDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Debts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FixedExpenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DayOfMonth = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixedExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FixedExpenses_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    FixedExpenseId = table.Column<int>(type: "int", nullable: true),
                    DebtId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Debts_DebtId",
                        column: x => x.DebtId,
                        principalTable: "Debts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_FixedExpenses_FixedExpenseId",
                        column: x => x.FixedExpenseId,
                        principalTable: "FixedExpenses",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Budgets",
                columns: new[] { "Id", "MonthlyLimit", "PoolAmount" },
                values: new object[] { 1, 30000m, 600000m });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "Maaş", "Income" },
                    { 2, "Serbest iş", "Income" },
                    { 3, "Yatırım", "Income" },
                    { 4, "Diğer gelir", "Income" },
                    { 5, "Kira", "Expense" },
                    { 6, "Market", "Expense" },
                    { 7, "Faturalar", "Expense" },
                    { 8, "Ulaşım", "Expense" },
                    { 9, "Eğlence", "Expense" },
                    { 10, "Sağlık", "Expense" },
                    { 11, "Diğer gider", "Expense" },
                    { 12, "Kira geliri", "Income" },
                    { 13, "Hediye", "Income" },
                    { 14, "Abonelik", "Expense" },
                    { 15, "Giyim", "Expense" },
                    { 16, "Eğitim", "Expense" },
                    { 17, "Tatil", "Expense" },
                    { 18, "Borç ödemesi", "Expense" },
                    { 19, "Ev kirası", "FixedExpense" },
                    { 20, "Elektrik, su, internet", "FixedExpense" },
                    { 21, "Dijital abonelikler", "FixedExpense" }
                });

            migrationBuilder.InsertData(
                table: "Debts",
                columns: new[] { "Id", "Amount", "Creditor", "DueDate" },
                values: new object[] { 1, 150000m, "Banka kredisi", null });

            migrationBuilder.InsertData(
                table: "FixedExpenses",
                columns: new[] { "Id", "Amount", "CategoryId", "DayOfMonth" },
                values: new object[,]
                {
                    { 1, 16000m, 19, 3 },
                    { 2, 3200m, 20, 10 },
                    { 3, 650m, 21, 5 }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "CategoryId", "DebtId", "FixedExpenseId", "Note", "PaymentType", "TransactionDate" },
                values: new object[,]
                {
                    { 1, 48000m, 1, null, null, "Aylık maaş", "0", new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 11047m, 2, null, null, "Proje ödemesi", "0", new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 16000m, 5, null, null, "Ev kirası", "0", new DateTime(2026, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 2939m, 7, null, null, "Elektrik, su, internet", "0", new DateTime(2026, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 7732m, 6, null, null, "Aylık market", "0", new DateTime(2026, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 1855m, 8, null, null, "Ulaşım kartı ve yakıt", "0", new DateTime(2026, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 2719m, 9, null, null, "Sinema, dışarıda yemek", "0", new DateTime(2026, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 48000m, 1, null, null, "Aylık maaş", "0", new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 11305m, 2, null, null, "Proje ödemesi", "0", new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 16000m, 5, null, null, "Ev kirası", "0", new DateTime(2026, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 3293m, 7, null, null, "Elektrik, su, internet", "0", new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 7853m, 6, null, null, "Aylık market", "0", new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, 2125m, 8, null, null, "Ulaşım kartı ve yakıt", "0", new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, 3172m, 9, null, null, "Sinema, dışarıda yemek", "0", new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, 48000m, 1, null, null, "Aylık maaş", "0", new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, 8116m, 2, null, null, "Proje ödemesi", "0", new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, 16000m, 5, null, null, "Ev kirası", "0", new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, 3606m, 7, null, null, "Elektrik, su, internet", "0", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, 7474m, 6, null, null, "Aylık market", "0", new DateTime(2026, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, 1948m, 8, null, null, "Ulaşım kartı ve yakıt", "0", new DateTime(2026, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, 2662m, 9, null, null, "Sinema, dışarıda yemek", "0", new DateTime(2026, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, 48000m, 1, null, null, "Aylık maaş", "0", new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, 16000m, 5, null, null, "Ev kirası", "0", new DateTime(2026, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, 3927m, 7, null, null, "Elektrik, su, internet", "0", new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, 9910m, 6, null, null, "Aylık market", "0", new DateTime(2026, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 26, 1597m, 8, null, null, "Ulaşım kartı ve yakıt", "0", new DateTime(2026, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 27, 2533m, 9, null, null, "Sinema, dışarıda yemek", "0", new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 28, 1084m, 10, null, null, "Eczane", "0", new DateTime(2026, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 29, 48000m, 1, null, null, "Aylık maaş", "0", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 30, 10953m, 2, null, null, "Proje ödemesi", "0", new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 31, 16000m, 5, null, null, "Ev kirası", "0", new DateTime(2026, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 32, 3606m, 7, null, null, "Elektrik, su, internet", "0", new DateTime(2026, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 33, 7842m, 6, null, null, "Aylık market", "0", new DateTime(2026, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 34, 1636m, 8, null, null, "Ulaşım kartı ve yakıt", "0", new DateTime(2026, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 35, 4157m, 9, null, null, "Sinema, dışarıda yemek", "0", new DateTime(2026, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 36, 48000m, 1, null, null, "Aylık maaş", "0", new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 37, 16000m, 5, null, null, "Ev kirası", "0", new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 38, 3456m, 7, null, null, "Elektrik, su, internet", "0", new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 39, 7991m, 6, null, null, "Aylık market", "0", new DateTime(2026, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 40, 1623m, 8, null, null, "Ulaşım kartı ve yakıt", "0", new DateTime(2026, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 41, 3519m, 9, null, null, "Sinema, dışarıda yemek", "0", new DateTime(2026, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 42, 10000m, 18, 1, null, "Borç ödemesi: Banka kredisi", "Transfer", new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 43, 10000m, 18, 1, null, "Borç ödemesi: Banka kredisi", "Transfer", new DateTime(2026, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 44, 10000m, 18, 1, null, "Borç ödemesi: Banka kredisi", "Transfer", new DateTime(2026, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FixedExpenses_CategoryId",
                table: "FixedExpenses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryId",
                table: "Transactions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DebtId",
                table: "Transactions",
                column: "DebtId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FixedExpenseId",
                table: "Transactions",
                column: "FixedExpenseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Debts");

            migrationBuilder.DropTable(
                name: "FixedExpenses");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
