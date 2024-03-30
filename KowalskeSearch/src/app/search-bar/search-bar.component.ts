import { Component } from '@angular/core';
import { ProductService } from './Service/product.service'; // Ensure correct import path




export class ProductInfo {
  name: string = '';
  description: string = '';
  salePrice: string = '';
  originalPrice: string = '';
}

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.scss']
})
export class SearchBarComponent {
  searchTerms: string[] = [''];
  productInfos: ProductInfo[] = [];

  constructor(private productService: ProductService) { }

  addSearchInput(): void {
    this.searchTerms.push('');
  }

  removeSearchInput(index: number): void {
    this.searchTerms.splice(index, 1);
  }

  onSearchAll(): void {
    const nonEmptySearchTerms = this.searchTerms.filter(term => term.trim() !== '');
    this.productService.getProductsBySerialNumbers(nonEmptySearchTerms).subscribe({
      next: (data) => {
        this.productInfos = data;
      },
      error: (error: any) => {
        console.error('Error fetching product info:', error);
      }
    });
  }
}
