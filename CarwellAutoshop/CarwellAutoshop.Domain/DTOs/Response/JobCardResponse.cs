namespace CarwellAutoshop.Domain.DTOs.Response
{
    public class JobCardResponse
    {
        public int JobCardId { get; set; }
        public int VehicleId { get; set; }
        public int GarageId { get; set; }
        public int JobCardStatusId { get; set; }
        public int? OdometerReading { get; set; }
        public int CustomerId { get; set; }
        public int FuelTypeId { get; set; }
        public string RegistrationNo { get; set; }
        public string Model { get; set; }
        public DateTime OpenDate { get; set; }
        public VehicleResponse Vehicle { get; set; }
        public JobCardStatusResponse JobCardStatus { get; set; }


    }
    public class JobCardStatusResponse
    {
        public int JobCardStatusId { get; set; }

        public string StatusName { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsFinal { get; set; }

        public bool IsActive { get; set; }
    }
}
