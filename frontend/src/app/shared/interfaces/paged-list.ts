/**
 * Generic paged list interface matching backend PagedList<T>
 */
export interface PagedList<T> {
  items: T[];
  totalCount: number;
  pageSize: number;
  currentPage: number;
  totalPages: number;
}
