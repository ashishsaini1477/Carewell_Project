namespace CarwellAutoshop.Domain.DTOs.Request
{
    public class CreateJobCardDto
    {
        public int VehicleId { get; set; }
        public int GarageId { get; set; }
        public int JobCardStatusId { get; set; }
        public int? OdometerReading { get; set; }
    }
    public class EditJobCardDto
    {
        public int JobCardId { get; set; }
        public int JobCardStatusId { get; set; }
        public int? OdometerReading { get; set; }
    }

}
