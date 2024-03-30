import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProductInfo } from '../search-bar.component';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private baseUrl: string = 'http://localhost:5000/api/products'; // Adjust this URL to your backend's actual URL

  constructor(private http: HttpClient) { }

  getProductBySerialNumber(serialNumber: string): Observable<any> {
    // Ensure the correct endpoint, matching your backend route
    return this.http.get<any>(`${this.baseUrl}/${serialNumber}`);
  }

  // New method for batch search
  getProductsBySerialNumbers(serialNumbers: string[]): Observable<ProductInfo[]> {
    // Adjust the endpoint as necessary, matching your controller's route for batch search
    return this.http.post<ProductInfo[]>(`${this.baseUrl}/batchSearch`, serialNumbers);
  }
}
