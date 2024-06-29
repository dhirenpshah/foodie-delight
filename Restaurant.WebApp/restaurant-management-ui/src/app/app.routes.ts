import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '', redirectTo: '/restaurants', pathMatch: 'full'
    },
    {
        path: 'restaurants',
        loadComponent: () => import('./restaurant-management/restaurants/restaurants.component')
                            .then(mod => mod.RestaurantsComponent)
    },
    {
        path: 'restaurant/edit',
        loadComponent: () => import('./restaurant-management/restaurants/restaurant-edit/restaurant-edit.component')
                            .then(mod => mod.RestaurantEditComponent)
    },
    {
        path: '**',
        loadComponent: () => import('./shared/components/not-found/not-found.component')
                            .then(mod => mod.NotFoundComponent)
    }
];
