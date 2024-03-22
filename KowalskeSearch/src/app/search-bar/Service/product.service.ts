import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private baseUrl: string = 'http://localhost:5000/api/products'; // Adjust this URL to your backend's actual URL

  constructor(private http: HttpClient) { }

  getProductBySerialNumber(serialNumber: string): Observable<any> {
    return this.http.get(`${this.baseUrl}/${serialNumber}`);
  }
}
