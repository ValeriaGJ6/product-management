import { Component, inject, signal, computed, effect } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { RouterLink } from '@angular/router';
import { DatePipe, CurrencyPipe } from '@angular/common';

import { Product } from '../../interfaces';
import { ProductService } from '../../services/product';
import { PagedList } from '../../../../shared/interfaces';

@Component({
  selector: 'app-product-list',
  imports: [
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatProgressSpinnerModule,
    RouterLink,
    DatePipe,
    CurrencyPipe
  ],
  templateUrl: './product-list.html',
  styleUrl: './product-list.scss'
})
export class ProductList {
  private productService = inject(ProductService);

  currentPage = signal(1);
  pageSize = signal(5);
  isLoading = signal(false);
  totalCount = signal(0);

  private productsSignal = signal<Product[]>([]);

  products = computed(() => this.productsSignal());

  displayedColumns: string[] = ['name', 'description', 'price', 'createdAt', 'actions'];

  constructor() {
    effect(() => {
      this.loadProducts();
    });
  }

  private loadProducts(): void {
    this.isLoading.set(true);
    
    this.productService.getProducts(
      this.currentPage(),
      this.pageSize()
    ).subscribe({
      next: (result) => {
        this.productsSignal.set(result.items);
        this.totalCount.set(result.totalCount);
        this.isLoading.set(false);
      },
      error: (error) => {
        console.error('Error loading products:', error);
        this.isLoading.set(false);
      }
    });
  }

  onPageChange(event: PageEvent): void {
    this.currentPage.set(event.pageIndex + 1);
    this.pageSize.set(event.pageSize);
  }

  editProduct(product: Product): void {
    console.log('Edit product:', product);
  }

  deleteProduct(product: Product): void {
    console.log('Delete product:', product);
  }
}
