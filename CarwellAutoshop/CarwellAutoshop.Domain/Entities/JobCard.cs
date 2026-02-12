using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarwellAutoshop.Domain.Entities
{
    [Table("jobcard")]
    public class JobCard
    {
        [Key]
        [Column("jobcardid")]
        public int JobCardId { get; set; }

        [Column("vehicleid")]
        public int VehicleId { get; set; }

        [Column("garageid")]
        public int GarageId { get; set; }

        [Column("jobcardstatusid")]
        public int JobCardStatusId { get; set; }

        [Column("odometerreading")]
        public int? OdometerReading { get; set; }

        [Column("opendate")]
        public DateTime OpenDate { get; set; } = DateTime.UtcNow;

        [Column("closedate")]
        public DateTime? CloseDate { get; set; }

        public Vehicle Vehicle { get; set; }
        public Garage Garage { get; set; }
        public JobCardStatus JobCardStatus { get; set; }

        //public ICollection<JobCardRemark> Remarks { get; set; }
        //public ICollection<Invoice> Invoices { get; set; }
    }
}