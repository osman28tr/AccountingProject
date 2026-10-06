using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AccountingProject.Migrations
{
    /// <inheritdoc />
    public partial class v2_entities_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "DebtId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FixedExpenseId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentType",
                table: "Transactions",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.InsertData(
                table: "Budgets",
                columns: new[] { "Id", "MonthlyLimit", "PoolAmount" },
                values: new object[] { 1, 30000m, 600000m });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Type" },
                values: new object[,]
                {
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

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DebtId", "FixedExpenseId", "PaymentType" },
                values: new object[] { null, null, "0" });

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
                    { 42, 10000m, 18, 1, null, "Borç ödemesi: Banka kredisi", "Transfer", new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 43, 10000m, 18, 1, null, "Borç ödemesi: Banka kredisi", "Transfer", new DateTime(2026, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 44, 10000m, 18, 1, null, "Borç ödemesi: Banka kredisi", "Transfer", new DateTime(2026, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DebtId",
                table: "Transactions",
                column: "DebtId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FixedExpenseId",
                table: "Transactions",
                column: "FixedExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedExpenses_CategoryId",
                table: "FixedExpenses",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                table: "Transactions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Debts_DebtId",
                table: "Transactions",
                column: "DebtId",
                principalTable: "Debts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_FixedExpenses_FixedExpenseId",
                table: "Transactions",
                column: "FixedExpenseId",
                principalTable: "FixedExpenses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Debts_DebtId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_FixedExpenses_FixedExpenseId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "Debts");

            migrationBuilder.DropTable(
                name: "FixedExpenses");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_DebtId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_FixedExpenseId",
                table: "Transactions");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DropColumn(
                name: "DebtId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "FixedExpenseId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                table: "Transactions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
