using AccountingProject.Entities;
using Microsoft.EntityFrameworkCore;
using Type = AccountingProject.Entities.Type;

namespace AccountingProject.Datas.Contexts
{
	public class AppDbContex : DbContext
	{
		public AppDbContex(DbContextOptions<AppDbContex> options) : base(options)
		{
		}
		public DbSet<Category> Categories { get; set; }
		public DbSet<Transaction> Transactions { get; set; }
		public DbSet<FixedExpense> FixedExpenses { get; set; }
		public DbSet<Debt> Debts { get; set; }
		public DbSet<Budget> Budgets { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>().Property(c => c.Type).HasConversion<string>().HasMaxLength(50);
			modelBuilder.Entity<Transaction>().Property(c => c.PaymentType).HasConversion<string>().HasMaxLength(10);

			//Category

			modelBuilder.Entity<Category>().HasData(
		   new Category { Id = 1, Name = "Maaş", Type = Type.Income },
		   new Category { Id = 2, Name = "Serbest iş", Type = Type.Income },
		   new Category { Id = 3, Name = "Yatırım", Type = Type.Income },
		   new Category { Id = 4, Name = "Diğer gelir", Type = Type.Income },
		   new Category { Id = 5, Name = "Kira", Type = Type.Expense },
		   new Category { Id = 6, Name = "Market", Type = Type.Expense },
		   new Category { Id = 7, Name = "Faturalar", Type = Type.Expense },
		   new Category { Id = 8, Name = "Ulaşım", Type = Type.Expense },
		   new Category { Id = 9, Name = "Eğlence", Type = Type.Expense },
		   new Category { Id = 10, Name = "Sağlık", Type = Type.Expense },
		   new Category { Id = 11, Name = "Diğer gider", Type = Type.Expense },
		   new Category { Id = 12, Name = "Kira geliri", Type = Type.Income },
			new Category { Id = 13, Name = "Hediye", Type = Type.Income },
			new Category { Id = 14, Name = "Abonelik", Type = Type.Expense },
			new Category { Id = 15, Name = "Giyim", Type = Type.Expense },
			new Category { Id = 16, Name = "Eğitim", Type = Type.Expense },
			new Category { Id = 17, Name = "Tatil", Type = Type.Expense },
			new Category { Id = 18, Name = "Borç ödemesi", Type = Type.Expense },
			new Category { Id = 19, Name = "Ev kirası", Type = Type.FixedExpense },
			new Category { Id = 20, Name = "Elektrik, su, internet", Type = Type.FixedExpense },
			new Category { Id = 21, Name = "Dijital abonelikler", Type = Type.FixedExpense }
			);
			//test
			//Transaction

			modelBuilder.Entity<Transaction>().HasData(TransactionSeedData.Items);
			modelBuilder.Entity<Transaction>().HasOne(t => t.Category).WithMany(t => t.Transactions).HasForeignKey(x => x.CategoryId)
				.OnDelete(DeleteBehavior.Restrict);

			//Budget

			modelBuilder.Entity<Budget>().Property(x => x.MonthlyLimit).IsRequired().HasPrecision(18, 2);
			modelBuilder.Entity<Budget>().Property(x => x.PoolAmount).IsRequired().HasPrecision(18, 2);

			modelBuilder.Entity<Budget>().HasData(new Budget { Id = 1, PoolAmount = 600000m, MonthlyLimit = 30000m });

			//Fixed Expense

			modelBuilder.Entity<FixedExpense>().Property(f => f.Amount).IsRequired().HasPrecision(18, 2);

			modelBuilder.Entity<FixedExpense>().HasData(
			new FixedExpense { Id = 1, CategoryId = 19, Amount = 16000m,  DayOfMonth = 3 },
			new FixedExpense { Id = 2, CategoryId = 20, Amount = 3200m, DayOfMonth = 10 },
			new FixedExpense { Id = 3, CategoryId = 21, Amount = 650m, DayOfMonth = 5 });

			//Debt

			modelBuilder.Entity<Debt>().Property(d => d.Amount).IsRequired().HasPrecision(18, 2);
			modelBuilder.Entity<Debt>().Property(d => d.DueDate).HasColumnType("date");
			modelBuilder.Entity<Debt>().HasData(new Debt { Id = 1, Creditor = "Banka kredisi", Amount = 150000m });

		}
	}

	public static class TransactionSeedData
	{
		public static readonly Transaction[] Items =
	{
		new() { Id = 1, CategoryId = 1, Amount = 48000m, TransactionDate = new DateTime(2026, 4, 1), Note = "Aylık maaş" },
		new() { Id = 2, CategoryId = 2, Amount = 11047m, TransactionDate = new DateTime(2026, 4, 15), Note = "Proje ödemesi" },
		new() { Id = 3, CategoryId = 5, Amount = 16000m, TransactionDate = new DateTime(2026, 4, 3), Note = "Ev kirası" },
		new() { Id = 4, CategoryId = 7, Amount = 2939m, TransactionDate = new DateTime(2026, 4, 10), Note = "Elektrik, su, internet" },
		new() { Id = 5, CategoryId = 6, Amount = 7732m, TransactionDate = new DateTime(2026, 4, 12), Note = "Aylık market" },
		new() { Id = 6, CategoryId = 8, Amount = 1855m, TransactionDate = new DateTime(2026, 4, 8), Note = "Ulaşım kartı ve yakıt" },
		new() { Id = 7, CategoryId = 9, Amount = 2719m, TransactionDate = new DateTime(2026, 4, 20), Note = "Sinema, dışarıda yemek" },
		new() { Id = 8, CategoryId = 1, Amount = 48000m, TransactionDate = new DateTime(2026, 5, 1), Note = "Aylık maaş" },
		new() { Id = 9, CategoryId = 2, Amount = 11305m, TransactionDate = new DateTime(2026, 5, 15), Note = "Proje ödemesi" },
		new() { Id = 10, CategoryId = 5, Amount = 16000m, TransactionDate = new DateTime(2026, 5, 3), Note = "Ev kirası" },
		new() { Id = 11, CategoryId = 7, Amount = 3293m, TransactionDate = new DateTime(2026, 5, 10), Note = "Elektrik, su, internet" },
		new() { Id = 12, CategoryId = 6, Amount = 7853m, TransactionDate = new DateTime(2026, 5, 12), Note = "Aylık market" },
		new() { Id = 13, CategoryId = 8, Amount = 2125m, TransactionDate = new DateTime(2026, 5, 8), Note = "Ulaşım kartı ve yakıt" },
		new() { Id = 14, CategoryId = 9, Amount = 3172m, TransactionDate = new DateTime(2026, 5, 20), Note = "Sinema, dışarıda yemek" },
		new() { Id = 15, CategoryId = 1, Amount = 48000m, TransactionDate = new DateTime(2026, 6, 1), Note = "Aylık maaş" },
		new() { Id = 16, CategoryId = 2, Amount = 8116m, TransactionDate = new DateTime(2026, 6, 15), Note = "Proje ödemesi" },
		new() { Id = 17, CategoryId = 5, Amount = 16000m, TransactionDate = new DateTime(2026, 6, 3), Note = "Ev kirası" },
		new() { Id = 18, CategoryId = 7, Amount = 3606m, TransactionDate = new DateTime(2026, 6, 10), Note = "Elektrik, su, internet" },
		new() { Id = 19, CategoryId = 6, Amount = 7474m, TransactionDate = new DateTime(2026, 6, 12), Note = "Aylık market" },
		new() { Id = 20, CategoryId = 8, Amount = 1948m, TransactionDate = new DateTime(2026, 6, 8), Note = "Ulaşım kartı ve yakıt" },
		new() { Id = 21, CategoryId = 9, Amount = 2662m, TransactionDate = new DateTime(2026, 6, 20), Note = "Sinema, dışarıda yemek" },
		new() { Id = 22, CategoryId = 1, Amount = 48000m, TransactionDate = new DateTime(2026, 7, 1), Note = "Aylık maaş" },
		new() { Id = 23, CategoryId = 5, Amount = 16000m, TransactionDate = new DateTime(2026, 7, 3), Note = "Ev kirası" },
		new() { Id = 24, CategoryId = 7, Amount = 3927m, TransactionDate = new DateTime(2026, 7, 10), Note = "Elektrik, su, internet" },
		new() { Id = 25, CategoryId = 6, Amount = 9910m, TransactionDate = new DateTime(2026, 7, 12), Note = "Aylık market" },
		new() { Id = 26, CategoryId = 8, Amount = 1597m, TransactionDate = new DateTime(2026, 7, 8), Note = "Ulaşım kartı ve yakıt" },
		new() { Id = 27, CategoryId = 9, Amount = 2533m, TransactionDate = new DateTime(2026, 7, 20), Note = "Sinema, dışarıda yemek" },
		new() { Id = 28, CategoryId = 10, Amount = 1084m, TransactionDate = new DateTime(2026, 7, 18), Note = "Eczane" },
		new() { Id = 29, CategoryId = 1, Amount = 48000m, TransactionDate = new DateTime(2026, 8, 1), Note = "Aylık maaş" },
		new() { Id = 30, CategoryId = 2, Amount = 10953m, TransactionDate = new DateTime(2026, 8, 15), Note = "Proje ödemesi" },
		new() { Id = 31, CategoryId = 5, Amount = 16000m, TransactionDate = new DateTime(2026, 8, 3), Note = "Ev kirası" },
		new() { Id = 32, CategoryId = 7, Amount = 3606m, TransactionDate = new DateTime(2026, 8, 10), Note = "Elektrik, su, internet" },
		new() { Id = 33, CategoryId = 6, Amount = 7842m, TransactionDate = new DateTime(2026, 8, 12), Note = "Aylık market" },
		new() { Id = 34, CategoryId = 8, Amount = 1636m, TransactionDate = new DateTime(2026, 8, 8), Note = "Ulaşım kartı ve yakıt" },
		new() { Id = 35, CategoryId = 9, Amount = 4157m, TransactionDate = new DateTime(2026, 8, 20), Note = "Sinema, dışarıda yemek" },
		new() { Id = 36, CategoryId = 1, Amount = 48000m, TransactionDate = new DateTime(2026, 9, 1), Note = "Aylık maaş" },
		new() { Id = 37, CategoryId = 5, Amount = 16000m, TransactionDate = new DateTime(2026, 9, 3), Note = "Ev kirası" },
		new() { Id = 38, CategoryId = 7, Amount = 3456m, TransactionDate = new DateTime(2026, 9, 10), Note = "Elektrik, su, internet" },
		new() { Id = 39, CategoryId = 6, Amount = 7991m, TransactionDate = new DateTime(2026, 9, 12), Note = "Aylık market" },
		new() { Id = 40, CategoryId = 8, Amount = 1623m, TransactionDate = new DateTime(2026, 9, 8), Note = "Ulaşım kartı ve yakıt" },
		new() { Id = 41, CategoryId = 9, Amount = 3519m, TransactionDate = new DateTime(2026, 9, 20), Note = "Sinema, dışarıda yemek" },
		new() { Id = 42, CategoryId = 18, Amount = 10000m, TransactionDate = new DateTime(2026, 7, 20), PaymentType = PaymentMethodType.Transfer, DebtId = 1, Note = "Borç ödemesi: Banka kredisi" },
		new() { Id = 43, CategoryId = 18, Amount = 10000m, TransactionDate = new DateTime(2026, 8, 20), PaymentType = PaymentMethodType.Transfer, DebtId = 1, Note = "Borç ödemesi: Banka kredisi" },
		new() { Id = 44, CategoryId = 18, Amount = 10000m, TransactionDate = new DateTime(2026, 9, 20), PaymentType = PaymentMethodType.Transfer, DebtId = 1, Note = "Borç ödemesi: Banka kredisi" },
	};
	}
}
