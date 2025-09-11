import { Component, inject, signal, computed, effect } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { RouterLink, Router } from '@angular/router';
import { DatePipe, CurrencyPipe } from '@angular/common';

import { Product } from '../../interfaces';
import { ProductService } from '../../services/product';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialog, ConfirmationData } from '../../../../shared';
import { filter, finalize, switchMap } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';

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
  private router = inject(Router);
  private snackBar = inject(MatSnackBar);
  readonly dialog = inject(MatDialog);

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
    this.router.navigate(['/products/edit', product.id]);
  }

  deleteProduct(product: Product): void {
    const confirmationData = {
      title: 'Confirm Deletion',
      message: `Are you sure you want to delete the product "${product.name}"? This action cannot be undone.`,
      confirmText: 'Delete',
      cancelText: 'Cancel',
      icon: 'delete'
    };

    this.dialog.open(ConfirmationDialog, {
      data: confirmationData,
      width: '400px'
    }).afterClosed()
      .pipe(
        filter(confirmed => confirmed),
        switchMap(() => {
          this.isLoading.set(true);
          return this.productService.deleteProduct(product.id);
        }),
        finalize(() => this.isLoading.set(false))
      )
      .subscribe({
        next: () => {
          this.snackBar.open('Product deleted successfully!', 'X', { duration: 3000 });
          this.loadProducts();
        },
        error: (error) => {
          console.error('Error deleting product:', error);
        }
      });
  }
}
