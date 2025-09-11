/**
 * Product interface matching backend ProductResponseDTO
 */
export interface Product {
  id: number;
  name: string;
  description?: string;
  price: number;
  createdAt: Date;
}

/**
 * DTO for product creation
 */
export interface ProductCreateDto {
  name: string;
  description?: string;
  price: number;
}

/**
 * DTO for product updates
 */
export interface ProductUpdateDto {
  name?: string;
  description?: string;
  price?: number;
}
