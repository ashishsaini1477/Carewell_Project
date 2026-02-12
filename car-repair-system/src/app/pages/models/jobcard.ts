export class JobCardReq {
    vehicleId: number = 0;
    garageId: number = 0;
    jobCardStatusId: number = 0;
    odometerReading: number = 0;
}
export class JobCard {
    jobCardId: number = 0;
    garageId: number = 0;
    jobCardStatusId: number = 0;
    odometerReading: number = 0;
    vehicle: Vehicle;
    jobCardStatus: JobCardStatus;
    customerId: number = 0;      // 🔥 REQUIRED
    fuelTypeId: number = 0;      // 🔥 REQUIRED
    registrationNo: string = '';
    model = '';
    openDate= Date;
}
export class Vehicle {
    vehicleId: number = 0;
    registrationNo: string = '';
    model: string = '';
}
export class JobCardStatus {
    jobCardStatusId: number;
    statusName: string;
    displayOrder: number;
    isFinal: boolean;
    isActive: boolean;

}