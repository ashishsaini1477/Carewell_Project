namespace CarwellAutoshop.Domain.DTOs.Response
{
    public class CustomerWithVehiclesResponse
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public long Mobile { get; set; }

        public List<VehicleResponse> Vehicles { get; set; }
    }

}
