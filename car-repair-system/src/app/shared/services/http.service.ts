import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders  } from '@angular/common/http';
import 'rxjs/add/operator/map';
import { HttpParams } from '@angular/common/http';
import { environment } from '../../../environments/environment';


@Injectable()
export class HttpService {
  // private baseUrl = 'https://localhost:7234/api/';
  private baseUrl = environment.apiBaseUrl + "/api/";

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private http: HttpClient) { }

  add(data, urlPart) {
    let url = this.baseUrl + urlPart;
    return this.http.post(url, data, this.httpOptions).map(response => response);
  }

  update(data, urlPart) {
    let url = this.baseUrl + urlPart;
    return this.http.put(url, data, this.httpOptions).map(response => response);
  }

  getWithParam<T>(
    urlPart: string,
    params: { PageNumber: number; PageSize: number }
  ) {
    const url = this.baseUrl + urlPart;

    const httpParams = new HttpParams()
      .set('PageNumber', params.PageNumber.toString())
      .set('PageSize', params.PageSize.toString());

    return this.http.get<T>(url, { params: httpParams });
  }

  get(urlPart) {
    let url = this.baseUrl + urlPart;
    return this.http.get(url, this.httpOptions).map(response => response);
  }

  archive(urlPart) {
    let url = this.baseUrl + urlPart;
    return this.http.delete(url, this.httpOptions).map(response => response);
  }
 getBlob(urlPart: string) {
  const url = this.baseUrl + urlPart;
  return this.http.get(url, {
    responseType: 'blob' as 'json'
  });
}
}
