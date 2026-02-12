using System.ComponentModel.DataAnnotations;

namespace CarwellAutoshop.Domain.Entities
{
    public class JobCard
    {
        [Key]
        public int JobCardId { get; set; }

        public int VehicleId { get; set; }
        public int GarageId { get; set; }
        public int JobCardStatusId { get; set; }

        public int? OdometerReading { get; set; }

        public DateTime OpenDate { get; set; } = DateTime.Now;
        public DateTime? CloseDate { get; set; }

        public Vehicle Vehicle { get; set; }
        public Garage Garage { get; set; }
        public JobCardStatus JobCardStatus { get; set; }

        //public ICollection<JobCardRemark> Remarks { get; set; }
        //public ICollection<Invoice> Invoices { get; set; }
    }
}
