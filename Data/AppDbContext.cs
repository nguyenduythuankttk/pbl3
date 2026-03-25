using Microsoft.EntityFrameworkCore;
using Backend.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        // Dbset
        public DbSet<User> User {get; set;}
        public DbSet<BillDetail> BillDetail {get; set;}
        public DbSet<Bill> Bill {get; set;}
        public DbSet<BillModify> BillModify  {get; set;}
        public DbSet<Employee> Employee  {get; set;}
        public DbSet<GoodsReceipt> GoodsReceipt  {get; set;}
        public DbSet<Ingredient> Ingredient {get; set;}
        public DbSet<PurchaseOrder> PurchaseOrder  {get; set;}
        public DbSet<POApproval> POApproval {get; set;}
        public DbSet<PODetail> PODetail {get; set;}
        public DbSet<Product> Product {get; set;}
        public DbSet<ReceiptDetail>  ReceiptDetail {get; set;}
        public DbSet<Receipe> Receipe {get; set;}
        public DbSet<Shift> Shift {get; set;}
        public DbSet<StockAudit> StockAudit {get; set;}
        public DbSet<StockAuditDetail> StockAuditDetail {get; set;}
        public DbSet<StockMovement>  StockMovement {get; set;}
        public DbSet<Store> Store {get; set;}
        public DbSet<Supplier> Supplier {get; set;}

        public DbSet<Ticket> Ticket  {get; set;}
        public DbSet<UserAddress> UserAddress {get; set;}
        public DbSet<Utility> Utility  {get; set;}
        public DbSet<Warehouse> Warehouse {get; set;}

        // Configure Dbset
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            // many to many
            modelBuilder.Entity<BillDetail>()
            .HasKey(ma => new {ma.BillID,ma.ProductID});
            modelBuilder.Entity<PODetail>()
            .HasKey (ma => new {ma.POID,ma.IngredientID});
            modelBuilder.Entity<POApproval>()
            .HasKey ( ma => new {ma.POID,ma.EmployeeID});
            modelBuilder.Entity<ReceiptDetail>()
            .HasKey (ma => new {ma.GoodsReceiptID, ma.IngredientID});
            modelBuilder.Entity<Receipe>()
            .HasKey (ma => new {ma.IngredientID,ma.ProductID});
            modelBuilder.Entity<StockAuditDetail>()
            .HasKey (ma => new {ma.StockAuditID,ma.BatchID});
            modelBuilder.Entity<StoreUtility>()
            .HasKey(ma => new {ma.StoreID,ma.UtilityID});
            modelBuilder.Entity<UserAddress>()
            .HasKey(ma => new {ma.UserID,ma.AddressID});

            // one to one
            modelBuilder.Entity<Store>()
            .HasOne (s => s.Address)
            .WithOne (a => a.Store)
            .HasForeignKey<Store>(s => s.AddressID);

            modelBuilder.Entity<Supplier>()
            .HasOne (s => s.Address)
            .WithOne (a => a.Supplier)
            .HasForeignKey <Supplier> (s => s.AddressID);

            modelBuilder.Entity<Bill>()
            .Property(b => b.PaymentMethods)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();
            modelBuilder.Entity<Employee>()
            .Property(b => b.Role)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();
            modelBuilder.Entity<GoodsReceipt>()
            .Property(b => b.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();
            modelBuilder.Entity<Ingredient>()
            .Property(b => b.IngredientUnit)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();
            modelBuilder.Entity <InventoryBatch>()
            .Property(b => b.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();
            modelBuilder.Entity <POApproval> ()
            .Property(b => b.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();
            modelBuilder.Entity <Product>()
            .Property(b => b.ProductType)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();
            modelBuilder.Entity <StockAudit>()
            .Property(b => b.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();
            modelBuilder.Entity <StockMovement>()
            .Property(b => b.MovementType)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();
            modelBuilder.Entity <StockMovement>()
            .Property(b => b.ReferenceType)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        }
    }
}
