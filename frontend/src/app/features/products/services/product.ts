import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { Product, ProductCreateDto, ProductUpdateDto } from '../interfaces';
import { PagedList } from '../../../shared/interfaces';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/products`;

  // GET /api/products - List with pagination
  getProducts(page: number = 1, pageSize: number = 10): Observable<PagedList<Product>> {
    return this.http.get<PagedList<Product>>(`${this.baseUrl}?page=${page}&pageSize=${pageSize}`);
  }

  // GET /api/products/{id} - Get by ID
  getById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.baseUrl}/${id}`);
  }

  // POST /api/products - Create
  createProduct(product: ProductCreateDto): Observable<Product> {
    return this.http.post<Product>(this.baseUrl, product);
  }

  // PUT /api/products/{id} - Update
  updateProduct(id: number, product: ProductUpdateDto): Observable<Product> {
    return this.http.put<Product>(`${this.baseUrl}/${id}`, product);
  }

  // DELETE /api/products/{id} - Delete
  deleteProduct(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
