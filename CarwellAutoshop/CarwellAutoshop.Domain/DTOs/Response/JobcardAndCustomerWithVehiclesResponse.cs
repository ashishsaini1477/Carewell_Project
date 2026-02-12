namespace CarwellAutoshop.Domain.DTOs.Response
{
    public class JobcardAndCustomerWithVehiclesResponse : CustomerWithVehiclesResponse
    {
        public int JobCardId { get; set; }
        public int VehicleId { get; set; }
        public int GarageId { get; set; }
        public int JobCardStatusId { get; set; }
        public string StatusName { get; set; }
        public int? OdometerReading { get; set; }
        public DateTime OpenDate { get; set; }
    }
}
