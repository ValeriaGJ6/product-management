import { Routes } from '@angular/router';

export const routes: Routes = [
    { path: '', redirectTo: 'products', pathMatch: 'full' },
    { 
        path: 'products', 
        children: [
            { path: '', loadComponent: () => import('./features/products/components/product-list/product-list').then(m => m.ProductList) },
            { path: 'create', loadComponent: () => import('./features/products/components/product-form/product-form').then(m => m.ProductForm) },
            { path: 'edit/:id', loadComponent: () => import('./features/products/components/product-form/product-form').then(m => m.ProductForm) }
        ]
    },
    { path: '**', redirectTo: 'products' },
];
