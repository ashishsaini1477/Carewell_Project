import { Injectable } from '@angular/core';
import { HttpService } from '../../shared/services/http.service';

@Injectable()
export class JobcardService {

  private path: string = 'Jobcard';
  private vehiclePath: string = 'Vehicle';

  constructor(
    private http: HttpService
  ) { }


  addJobCard(data) {
    let url = this.path;
    return this.http.add(data, this.path);
  }
  addJobcardWithVehicle(data) {
    return this.http.add(data, this.vehiclePath + "/add-jobcard-vehicle");
  }

   getalljobcords<T>(pageNumber: number, pageSize: number) {
    return this.http.getWithParam(
      this.path + '/all-jobcards',
      {
        PageNumber: pageNumber,
        PageSize: pageSize
      }
    );
  }

  editJobcardWithVehicle(data) {
    return this.http.add(data, this.vehiclePath + "/edit-jobcard-vehicle");
  }
}
