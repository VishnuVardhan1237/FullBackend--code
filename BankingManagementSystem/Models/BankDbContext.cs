using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class BankDbContext : DbContext
{
    public BankDbContext()
    {
    }

    public BankDbContext(DbContextOptions<BankDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Beneficiary> Beneficiaries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Loan> Loans { get; set; }

    public virtual DbSet<Manager> Managers { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSqlLocaldb;Database=BankDb;Integrated Security =true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountNumber).HasName("PK__Accounts__BE2ACD6E200956A3");

            entity.Property(e => e.AccountNumber).HasMaxLength(20);
            entity.Property(e => e.AccountType).HasMaxLength(20);
            entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.IsLocked).HasDefaultValue(false);

            entity.HasOne(d => d.Customer).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Accounts__Custom__412EB0B6");
        });

        modelBuilder.Entity<Beneficiary>(entity =>
        {
            entity.HasKey(e => e.BeneficiaryId).HasName("PK__Benefici__3FBA95D57125D210");

            entity.Property(e => e.BeneficiaryId).HasColumnName("BeneficiaryID");
            entity.Property(e => e.BeneficiaryAccountNumber).HasMaxLength(20);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

            entity.HasOne(d => d.BeneficiaryAccountNumberNavigation).WithMany(p => p.Beneficiaries)
                .HasForeignKey(d => d.BeneficiaryAccountNumber)
                .HasConstraintName("FK__Beneficia__Benef__4CA06362");

            entity.HasOne(d => d.Customer).WithMany(p => p.Beneficiaries)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Beneficia__Custo__4BAC3F29");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B801357B45");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Customers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Customers__UserI__38996AB5");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF15B5F759B");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Designation).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Employees)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Employees__UserI__3B75D760");
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(e => e.LoanId).HasName("PK__Loans__4F5AD43740CC18E0");

            entity.Property(e => e.LoanId).HasColumnName("LoanID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.Customer).WithMany(p => p.Loans)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Loans__CustomerI__48CFD27E");
        });

        modelBuilder.Entity<Manager>(entity =>
        {
            entity.HasKey(e => e.ManagerId).HasName("PK__Managers__3BA2AA818603C4EA");

            entity.Property(e => e.ManagerId).HasColumnName("ManagerID");
            entity.Property(e => e.Department).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Managers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Managers__UserID__3E52440B");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A4B69DE5843");

            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            entity.Property(e => e.AccountNumber).HasMaxLength(20);
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Type).HasMaxLength(20);

            entity.HasOne(d => d.AccountNumberNavigation).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.AccountNumber)
                .HasConstraintName("FK__Transacti__Accou__44FF419A");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACE1895E6F");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
