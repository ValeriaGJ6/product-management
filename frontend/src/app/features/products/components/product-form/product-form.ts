import { Component, inject, signal, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

import { ProductService } from '../../services/product';
import { Product, ProductCreateDto, ProductUpdateDto } from '../../interfaces';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatSnackBarModule
  ],
  templateUrl: './product-form.html',
  styleUrls: ['./product-form.scss']
})
export class ProductForm implements OnInit {
  private fb = inject(FormBuilder);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private productService = inject(ProductService);
  private snackBar = inject(MatSnackBar);

  isLoading = signal(false);
  isEditMode = signal(false);
  productId = signal<number | null>(null);

  productForm: FormGroup = this.fb.group({
    name: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(100)]],
    description: ['', [Validators.maxLength(255)]],
    price: ['', [Validators.required, Validators.min(0.01), Validators.max(99999999.99), Validators.pattern(/^\d{1,8}(\.\d{0,2})?$/)]]
  });

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode.set(true);
      this.productId.set(parseInt(id, 10));
      this.loadProduct(this.productId()!);
    }
  }

  private loadProduct(id: number): void {
    this.isLoading.set(true);

    this.productService.getById(id)
      .pipe(
        finalize(() => this.isLoading.set(false))
      )
      .subscribe({
        next: (product) => {
          this.productForm.patchValue({
            name: product.name,
            description: product.description,
            price: product.price
          });
        },
        error: (error) => {
          console.error('Error loading product:', error);
          this.snackBar.open('Error loading product. Please try again.', 'X', { duration: 5000 });
          this.router.navigate(['/products']);
        }
      });
  }

  onSubmit(): void {
    if (this.productForm.valid) {
      this.isLoading.set(true);

      const formValue = this.productForm.value;

      if (this.isEditMode()) {
        this.updateProduct(formValue);
      } else {
        this.createProduct(formValue);
      }
    } else {
      this.markFormGroupTouched();
    }
  }

  private createProduct(productData: ProductCreateDto): void {
    this.isLoading.set(true);

    this.productService.createProduct(productData)
      .pipe(
        finalize(() =>
          this.isLoading.set(false)
        )
      ).subscribe({
        next: (createdProduct) => {
          this.snackBar.open('Product created successfully!', 'X', { duration: 3000 });
          this.router.navigate(['/products']);
        },
        error: (error) => {
          console.error('Error creating product:', error);
          this.snackBar.open('Error creating product. Please try again.', 'X', { duration: 5000 });
        }
      });
  }

  private updateProduct(productData: ProductUpdateDto): void {
    this.isLoading.set(true);

    this.productService.updateProduct(this.productId()!, productData)
      .pipe(
        finalize(() => this.isLoading.set(false))
      )
      .subscribe({
        next: (updatedProduct) => {
          this.snackBar.open('Product updated successfully!', 'X', { duration: 3000 });
          this.router.navigate(['/products']);
        },
        error: (error) => {
          console.error('Error updating product:', error);
          this.snackBar.open('Error updating product. Please try again.', 'X', { duration: 5000 });
        }
      });
  }

  onCancel(): void {
    this.router.navigate(['/products']);
  }

  private markFormGroupTouched(): void {
    Object.keys(this.productForm.controls).forEach(key => {
      const control = this.productForm.get(key);
      control?.markAsTouched();
    });
  }

}