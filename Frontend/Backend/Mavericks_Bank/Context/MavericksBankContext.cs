using Mavericks_Bank.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Mavericks_Bank.Context
{
    public class MavericksBankContext : DbContext
    {
        public DbSet<Validation> Validation { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<BankEmployees> BankEmployees { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Banks> Banks { get; set; }
        public DbSet<Branches> Branches { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Beneficiaries> Beneficiaries { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<Loans> Loans { get; set; }
        public DbSet<AppliedLoans> AppliedLoans { get; set; }

        public MavericksBankContext(DbContextOptions options) : base(options)
        {

        }
    }
}
