export interface JobcardAndCustomerWithVehicles
  extends CustomerWithVehicles {

  jobCardId: number;
  vehicleId: number;
  garageId: number;
  jobCardStatusId: number;
  statusName: string;
  odometerReading: number | null;
  openDate: string; // ISO string → convert to Date if needed
}

export interface CustomerWithVehicles {
  customerId: number;
  name: string;
  mobile: number;
  vehicles: VehicleResponse[];
}

export interface VehicleResponse {
  vehicleId: number;
  registrationNo: string;
  model: string;
}

