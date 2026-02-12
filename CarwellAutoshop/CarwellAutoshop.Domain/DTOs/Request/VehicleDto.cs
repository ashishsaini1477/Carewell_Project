namespace CarwellAutoshop.Domain.DTOs.Request
{
    public class VehicleDto : VehicleProps
    {
        public int CustomerId { get; set; }

    }
    public class EditVehicleDto : VehicleProps
    {
        public int VehicleId { get; set; }
    }
    public class VehicleProps
    {
        public string RegistrationNo { get; set; }
        public string Model { get; set; }
        public int FuelTypeId { get; set; }
    }
}
