using CarwellAutoshop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().ToTable("customer");
            modelBuilder.Entity<Vehicle>().ToTable("vehicle");
            modelBuilder.Entity<JobCard>().ToTable("jobcard");
            modelBuilder.Entity<Invoice>().ToTable("invoice");
            modelBuilder.Entity<Garage>().ToTable("garage");
            modelBuilder.Entity<InvoiceLineItem>().ToTable("invoicelineitem");
            modelBuilder.Entity<LabourWork>().ToTable("labourwork");
            modelBuilder.Entity<Payment>().ToTable("payment");
            modelBuilder.Entity<FuelType>().ToTable("fueltype");
            modelBuilder.Entity<JobCardStatus>().ToTable("jobcardstatus");
            modelBuilder.Entity<PaymentMode>().ToTable("paymentmode");
            modelBuilder.Entity<JobCardRemark>().ToTable("jobcardremark");

            // Bool -> smallint conversion (existing)
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;
                if (clrType == null)
                    continue;

                var boolProperties = entityType.GetProperties()
                                              .Where(p => p.ClrType == typeof(bool) || p.ClrType == typeof(bool?))
                                              .ToList();

                if (!boolProperties.Any())
                    continue;

                var entity = modelBuilder.Entity(clrType);
                foreach (var prop in boolProperties)
                {
                    if (prop.ClrType == typeof(bool))
                    {
                        entity.Property(prop.Name)
                              .HasConversion(new BoolToZeroOneConverter<short>())
                              .HasColumnType("smallint");
                    }
                    else // bool?
                    {
                        var nullableBoolConverter = new ValueConverter<bool?, short?>(
                            v => v.HasValue ? (short?)(v.Value ? (short)1 : (short)0) : null,
                            v => v.HasValue ? (bool?)(v.Value == 1) : null);

                        entity.Property(prop.Name)
                              .HasConversion(nullableBoolConverter)
                              .HasColumnType("smallint");
                    }
                }
            }

            // DateTime Kind handling: ensure values are written/read as UTC for timestamptz columns.
            // This prevents "Cannot write DateTime with Kind=Unspecified to PostgreSQL type 'timestamp with time zone'".
            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
                v => v.Kind == DateTimeKind.Utc ? v : DateTime.SpecifyKind(v, DateTimeKind.Utc),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
                v => v.HasValue ? (v.Value.Kind == DateTimeKind.Utc ? v : DateTime.SpecifyKind(v.Value, DateTimeKind.Utc)) : v,
                v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;
                if (clrType == null)
                    continue;

                var dtProperties = entityType.GetProperties()
                                            .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?))
                                            .ToList();

                if (!dtProperties.Any())
                    continue;

                var entity = modelBuilder.Entity(clrType);
                foreach (var prop in dtProperties)
                {
                    if (prop.ClrType == typeof(DateTime))
                    {
                        entity.Property(prop.Name)
                              .HasConversion(dateTimeConverter);
                    }
                    else
                    {
                        entity.Property(prop.Name)
                              .HasConversion(nullableDateTimeConverter);
                    }
                }
            }
        }
    }
}