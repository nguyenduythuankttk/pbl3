using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        // Dbset
        public DbSet<User> User {get; set;}
        public DbSet<BillDetail> BillDetail {get; set;}
        public DbSet<Bill> Bill {get; set;}
        public DbSet<Employee> Employee  {get; set;}
        public DbSet<Receipt> GoodsReceipt  {get; set;}
        public DbSet<Ingredient> Ingredient {get; set;}
        public DbSet<PurchaseOrder> PurchaseOrder  {get; set;}
        public DbSet<POApproval> POApproval {get; set;}
        public DbSet<PODetail> PODetail {get; set;}
        public DbSet<Product> Product {get; set;}
        public DbSet<ReceiptDetail>  ReceiptDetail {get; set;}
        public DbSet<Receipe> Receipe {get; set;}
        public DbSet<Shift> Shift {get; set;}
        public DbSet<StockMovement>  StockMovement {get; set;}
        public DbSet<Store> Store {get; set;}
        public DbSet<Supplier> Supplier {get; set;}
        public DbSet<Combo> Combo {get; set;}
        public DbSet<ComboProduct> ComboProduct {get; set;}
        public DbSet<ProductVarient> ProductVarient {get; set;}

        public DbSet<Ticket> Ticket  {get; set;}
        public DbSet<UserAddress> UserAddress {get; set;}
        public DbSet<Warehouse> Warehouse {get; set;}

        // Configure Dbset
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            // ── Composite Keys ───────────────────────────────────────────────
            modelBuilder.Entity<UserAddress>()
                .HasKey(x => new { x.UserID, x.AddressID });

            modelBuilder.Entity<BillDetail>()
            .HasKey(ma => new {ma.BillID,ma.ProductVarient});
            modelBuilder.Entity<PODetail>()
                .HasKey(x => new { x.POID, x.IngredientID });

            modelBuilder.Entity<POApproval>()
                .HasKey(x => new { x.POID, x.EmployeeID });

            modelBuilder.Entity<ReceiptDetail>()
                .HasKey(x => new { x.GoodsReceiptID, x.IngredientID });

            modelBuilder.Entity<InspectionDetail>()
                .HasKey(x => new { x.InspectionID, x.IngredientID });

            modelBuilder.Entity<Reservation>()
                .HasKey(x => new { x.UserID, x.TableID });

            // ── 1:1 ─────────────────────────────────────────────────────────
            modelBuilder.Entity<Store>()
                .HasOne(s => s.Address)
                .WithOne(a => a.Store)
                .HasForeignKey<Store>(s => s.AddressID);

            modelBuilder.Entity<Supplier>()
                .HasOne(s => s.Address)
                .WithOne(a => a.Supplier)
                .HasForeignKey<Supplier>(s => s.AddressID);

            modelBuilder.Entity<Bill>()
                .HasOne(b => b.DeliveryInfo)
                .WithOne(d => d.Bill)
                .HasForeignKey<DeliveryInfo>(d => d.BillID);

            // ── 1:N — Multiple FK cùng bảng (cần Restrict tránh cascade) ────
            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Employee)
                .WithMany()
                .HasForeignKey(b => b.EmployeeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bill>()
                .HasOne(b => b.DeletedByEmployee)
                .WithMany()
                .HasForeignKey(b => b.DeletedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Receipt>()
                .HasOne(gr => gr.Employee)
                .WithMany(e => e.GoodsReceipts)
                .HasForeignKey(gr => gr.EmployeeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Receipt>()
                .HasOne(gr => gr.Deleted)
                .WithMany()
                .HasForeignKey(gr => gr.DeletedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GoodsInspection>()
                .HasOne(gi => gi.Employee)
                .WithMany(e => e.GoodsInspections)
                .HasForeignKey(gi => gi.InspectedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DeliveryLog>()
                .HasOne(dl => dl.Employee)
                .WithMany(e => e.DeliveryLogs)
                .HasForeignKey(dl => dl.EmployeeID)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Composite FK: InventoryBatch → ReceiptDetail ─────────────────
            modelBuilder.Entity<InventoryBatch>()
                .HasOne(ib => ib.ReceiptDetail)
                .WithMany(rd => rd.InventoryBatches)
                .HasForeignKey(ib => new { ib.GoodsReceiptID, ib.IngredientID })
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InventoryBatch>()
                .HasOne(ib => ib.Ingredient)
                .WithMany(i => i.InventoryBatches)
                .HasForeignKey(ib => ib.IngredientID)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Enum → String ────────────────────────────────────────────────
            modelBuilder.Entity<Employee>()
            .Property(b => b.Role)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();
            modelBuilder.Entity<Receipt>()
            .Property(b => b.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();
            modelBuilder.Entity<Ingredient>()
                .Property(x => x.IngredientUnit)
                .HasConversion<string>().HasMaxLength(20).IsRequired();
        }
    }
}
