using CarwellAutoshop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarwellAutoshop.Infrastructure.Data
{
    public class GarageDbContext : DbContext
    {
        public GarageDbContext(DbContextOptions<GarageDbContext> options)
            : base(options) { }

        public DbSet<Domain.Entities.Customer> Customer => base.Set<Domain.Entities.Customer>();
        public DbSet<Vehicle> Vehicle => Set<Vehicle>();
        public DbSet<JobCard> JobCard => Set<JobCard>();
        public DbSet<Invoice> Invoice => Set<Invoice>();
        public DbSet<Garage> Garage => Set<Garage>();
        public DbSet<InvoiceLineItem> InvoiceLineItem => Set<InvoiceLineItem>();
        public DbSet<LabourWork> LabourWork => Set<LabourWork>();
        public DbSet<Payment> Payment => Set<Payment>();
        public DbSet<FuelType> FuelType => Set<FuelType>();
        public DbSet<JobCardStatus> JobCardStatus => Set<JobCardStatus>();
        public DbSet<PaymentMode> PaymentMode => Set<PaymentMode>();
        public DbSet<JobCardRemark> JobCardRemark => Set<JobCardRemark>();
        
    }
}
