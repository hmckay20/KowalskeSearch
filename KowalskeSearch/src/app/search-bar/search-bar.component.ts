import { Component } from '@angular/core';
import { ProductService } from './Service/product.service'; // Correct import statement based on your structure // Update the path as necessary

export class ProductInfo {
  name: string;
  description: string;
  salePrice: string;
  originalPrice: string;

  constructor() {
    this.name = '';
    this.description = '';
    this.salePrice = '';
    this.originalPrice = '';
  }
}

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.scss']
})
export class SearchBarComponent {
  searchTerm: string = '';
  productInfos: ProductInfo[] = [];

  constructor(private productService: ProductService) { }

  onSearch(event: Event): void {
    event.preventDefault(); // Prevent form from causing a page reload
    this.productService.getProductBySerialNumber(this.searchTerm).subscribe({
      next: (data) => {
        // Insert the new search result at the beginning of the array
        this.productInfos.unshift(data);
      },
      error: (error) => {
        console.error('Error fetching product info:', error);
      }
    });
  }
}
