using Microsoft.EntityFrameworkCore;
using OnyxScheduling.Models;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options) {}

    public DbSet<Category> Category { get; set; }
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Invoice_Items> Invoice_Items { get; set; }
    public DbSet<Invoices> Invoices { get; set; }
    public DbSet<OfficeStaff> OfficeStaff { get; set; }
    public DbSet<Technicians> Technicians { get; set; }
    public DbSet<InvoiceInvoice_Item> InvoiceInvoice_Item { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InvoiceInvoice_Item>()
           .HasKey(ii => new { ii.InvoiceId, ii.InvoiceItemId });

        modelBuilder.Entity<InvoiceInvoice_Item>()
            .HasOne(ii => ii.Invoice)
            .WithMany(i => i.InvoiceInvoice_Items)
            .HasForeignKey(ii => ii.InvoiceId);

        modelBuilder.Entity<InvoiceInvoice_Item>()
            .HasOne(ii => ii.InvoiceItem)
            .WithMany(ii => ii.InvoiceInvoiceItems)
            .HasForeignKey(ii => ii.InvoiceItemId);


        modelBuilder.Entity<Invoices>().HasData(
            new Invoices { Id= 1,
                InvoiceNumber = "INV001",
                Assigned_Customer_Id = 1,
                Assigned_Technician_Id = 2,
                CreatedDateTime = DateTime.Now
            },

            new Invoices { Id= 2,
                InvoiceNumber = "INV002",
                Assigned_Customer_Id = 1,
                Assigned_Technician_Id = 2,
                CreatedDateTime = DateTime.Now
            }
        );

        modelBuilder.Entity<Invoice_Items>().HasData(
            new Invoice_Items { Id = 123, Item_Name= "Test", Price = 50.00 },
            new Invoice_Items { Id = 234, Item_Name = "Item", Price= 75.00 }
        );

        modelBuilder.Entity<InvoiceInvoice_Item>().HasData(
            new InvoiceInvoice_Item { InvoiceId = 1, InvoiceItemId = 123 },
            new InvoiceInvoice_Item { InvoiceId = 2, InvoiceItemId = 234 }
        );
    }
}
