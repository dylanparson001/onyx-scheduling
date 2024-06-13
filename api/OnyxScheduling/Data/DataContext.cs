using Microsoft.EntityFrameworkCore;
using OnyxScheduling.Models;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options) {}

    public DbSet<Category> Category { get; set; }
    public DbSet<Invoice_Items> Invoice_Items { get; set; }
    public DbSet<Invoices> Invoices { get; set; }

    public DbSet<InvoiceInvoice_Item> InvoiceInvoice_Item { get; set; }
    public DbSet<Jobs> Jobs { get; set; }
    public DbSet<JobInvoice_Item> JobInvoice_Item { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<InvoiceInvoice_Item>()
           .HasKey(ii => new { ii.InvoiceId, ii.InvoiceItemId });

        modelBuilder.Entity<InvoiceInvoice_Item>()
            .HasOne(ii => ii.Invoice)
            .WithMany(i => i.Invoice_Items)
            .HasForeignKey(ii => ii.InvoiceId);

        modelBuilder.Entity<InvoiceInvoice_Item>()
            .HasOne(ii => ii.InvoiceItem)
            .WithMany(ii => ii.InvoiceInvoiceItems)
            .HasForeignKey(ii => ii.InvoiceItemId);

        // JobInvoice_Item
        modelBuilder.Entity<JobInvoice_Item>()
            .HasKey(item => new { item.JobId, item.InvoiceItemId });

        modelBuilder.Entity<JobInvoice_Item>()
            .HasOne(ii => ii.Job)
            .WithMany(i => i.JobInvoiceItems)
            .HasForeignKey(ii => ii.JobId);

        modelBuilder.Entity<JobInvoice_Item>()
            .HasOne(ii => ii.InvoiceItems)
            .WithMany(ii => ii.JobInvoiceItems)
            .HasForeignKey(ii => ii.InvoiceItemId);
    }
}
