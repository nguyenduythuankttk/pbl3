//:qa
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Backend.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<User> User { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<UserAddress> UserAddress { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<Shift> Shift { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductVarient> ProductVarient { get; set; }
        public DbSet<Combo> Combo { get; set; }
        public DbSet<ComboProduct> ComboProduct { get; set; }
        public DbSet<Receipe> Receipe { get; set; }
        public DbSet<Bill> Bill { get; set; }
        public DbSet<BillDetail> BillDetail { get; set; }
        public DbSet<BillChange> BillChange { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<DiningTable> DiningTable { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<BookingChange> BookingChange {get; set; }
        public DbSet<DeliveryInfo> DeliveryInfo { get; set; }
        public DbSet<DeliveryLog> DeliveryLog { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public DbSet<PODetail> PODetail { get; set; }
        public DbSet<POApproval> POApproval { get; set; }
        public DbSet<Receipt> Receipt { get; set; }
        public DbSet<ReceiptDetail> ReceiptDetail { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<InventoryBatch> InventoryBatch { get; set; }
        public DbSet<StockMovement> StockMovement { get; set; }
        public DbSet<ReceiptChange> ReceiptChange {get; set;}
        public DbSet<TicketCombo> TicketCombo {get; set;}
        public DbSet<TicketProduct> TicketProduct {get; set;}
        public DbSet<BlacklistedToken> BlackListedToken {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            // many to many
            modelBuilder.Entity<UserAddress>()
                .HasKey(x => new { x.UserID, x.AddressID });

            modelBuilder.Entity<BillDetail>()
                .HasKey(x => new { x.BillID, x.ProductVarientID });

            modelBuilder.Entity<ComboProduct>()
                .HasKey(x => new { x.ComboID, x.ProductVarientID });

            modelBuilder.Entity<Receipe>()
                .HasKey(x => new { x.IngredientID, x.ProductVarientID });

            modelBuilder.Entity<PODetail>()
                .HasKey(x => new { x.POID, x.IngredientID });
            modelBuilder.Entity<ReceiptDetail>()
                .HasKey(x => new { x.GoodsReceiptID, x.IngredientID });

            modelBuilder.Entity<TicketCombo>()
                .HasKey(x => new {x.TicketID, x.ComboID});

            modelBuilder.Entity<TicketProduct>()
                .HasKey(x => new {x.TicketID, x.ProductVarientID});


            //one to one
            modelBuilder.Entity<Store>()
                .HasOne(s => s.Address)
                .WithOne(a => a.Store)
                .HasForeignKey<Store>(s => s.AddressID);

            modelBuilder.Entity<Supplier>()
                .HasOne(s => s.Address)
                .WithOne(a => a.Supplier)
                .HasForeignKey<Supplier>(s => s.AddressID);
            modelBuilder.Entity<Receipt>()
                .HasOne(r => r.PurchaseOrder)
                .WithOne(p => p.Receipt)
                .HasForeignKey<Receipt> (r => r.POID);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.BookingChange)
                .WithOne(a => a.Booking)
                .HasForeignKey<BookingChange>("BookingID");


            // convert string
            modelBuilder.Entity<User>()
                .Property(x=> x.Gender)
                .HasConversion<string>().HasMaxLength(10).IsRequired();
            modelBuilder.Entity<Employee>()
                .Property(x => x.Role)
                .HasConversion<string>().HasMaxLength(20).IsRequired();

            modelBuilder.Entity<DiningTable>()
                .Property(x=> x.Status)
                .HasConversion<string>().HasMaxLength(30).IsRequired();

            modelBuilder.Entity<Bill>()
                .Property(x => x.PaymentMethods)
                .HasConversion<string>().HasMaxLength(20).IsRequired();
            
            modelBuilder.Entity<BookingChange>()
                .Property(x => x.BookingStatus)
                .HasConversion<string>().HasMaxLength(20).IsRequired();

            modelBuilder.Entity<Product>()
                .Property(x => x.ProductType)
                .HasConversion<string>().HasMaxLength(20).IsRequired();

            modelBuilder.Entity<ProductVarient>()
                .Property(x => x.Size)
                .HasConversion<string>().HasMaxLength(10).IsRequired();

            modelBuilder.Entity<ReceiptChange>()
                .Property(x => x.Status)
                .HasConversion<string>().HasMaxLength(20).IsRequired();

            modelBuilder.Entity<InventoryBatch>()
                .Property(x => x.Status)
                .HasConversion<string>().HasMaxLength(20).IsRequired();

            modelBuilder.Entity<StockMovement>()
                .Property(x => x.MovementType)
                .HasConversion<string>().HasMaxLength(30).IsRequired();

            modelBuilder.Entity<StockMovement>()
                .Property(x => x.ReferenceType)
                .HasConversion<string>().HasMaxLength(20).IsRequired();

            modelBuilder.Entity<POApproval>()
                .Property(x => x.Status)
                .HasConversion<string>().HasMaxLength(20).IsRequired();

            modelBuilder.Entity<Ingredient>()
                .Property(x => x.IngredientUnit)
                .HasConversion<string>().HasMaxLength(20).IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.Gender)
                .HasConversion<string>().HasMaxLength(10);

            modelBuilder.Entity<BillChange>()
                .Property(x => x.Status)
                .HasConversion<string>().HasMaxLength(20).IsRequired();

            modelBuilder.Entity<DeliveryLog>()
                .Property(x => x.Status)
                .HasConversion<string>().HasMaxLength(20).IsRequired();
        }
    }
}
