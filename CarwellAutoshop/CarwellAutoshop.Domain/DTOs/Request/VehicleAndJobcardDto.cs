namespace CarwellAutoshop.Domain.DTOs.Request
{
    public class VehicleAndJobcardDto
    {
        public int? VehicleId { get; set; }
        public int? GarageId { get; set; }
        public int CustomerId { get; set; }
        public string RegistrationNo { get; set; }
        public string Model { get; set; }
        public int FuelTypeId { get; set; }
        public int JobCardStatusId { get; set; }
        public int? OdometerReading { get; set; }
    }

    public class EditVehicleAndJobcardDto : VehicleAndJobcardDto
    {
        public int JobCardId { get; set; }
    }

}
