import { Component, OnInit } from '@angular/core';
import { Restaurant } from '../../shared/domain/models/restaurant.model';
import { RestaurantService } from '../../shared/services/restaurant-service.service';
import { GridColumn } from '../../shared/domain/models/gird-column.model';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import { ImportsModule } from '../imports';

@Component({
  selector: 'app-restaurants',
  standalone: true,
  imports: [ImportsModule],
  templateUrl: './restaurants.component.html',
  styleUrl: './restaurants.component.scss',
  providers: [ConfirmationService, MessageService]
})
export class RestaurantsComponent implements OnInit{
  restaurants: Array<Restaurant> = [];
  gridColumns: Array<GridColumn> = [];
  searchText: string = ''

  constructor(private restaurantService: RestaurantService,
    private confirmationService: ConfirmationService,
    private messageService: MessageService,
    private router: Router){}

  ngOnInit(): void {
    this.fillGridColumns();
    this.loadRestaurants();
  }

  private fillGridColumns(): void {
    this.gridColumns = [
      { Header: 'Name', Field: 'name'} as GridColumn,
      { Header: 'Location', Field: 'location'} as GridColumn,
      { Header: 'Description', Field: 'description'} as GridColumn
    ]
  }

  private loadRestaurants(): void {
    this.restaurantService.loadRestaurants().subscribe({next: (restaurants) =>{
      this.restaurants = restaurants;
    }, 
    error: (error) => { this.messageService.add({ severity: 'danger', summary: 'Error', detail: error.Message });}});
  }

  getGlobalFilterFields(): Array<string> {
    return this.gridColumns.map(_ => _.Field);
  }

  deleteRestaurant(restaurant: Restaurant): void {
    this.restaurantService.deleteRestaurant(restaurant.id).subscribe({ next: (restaurant) =>{
      this.messageService.add({ severity: 'success', summary: 'Confirmed', detail: 'Restaurant deleted' });
      this.loadRestaurants(); 
    }})
  }

  confirmDelete(event: Event, restaurant: Restaurant) {
    this.confirmationService.confirm({
        target: event.target as EventTarget,
        message: 'Do you want to delete this restaurant?',
        header: 'Delete Confirmation',
        icon: 'pi pi-info-circle',
        acceptButtonStyleClass:"p-button-danger p-button-text",
        rejectButtonStyleClass:"p-button-text p-button-text",
        acceptIcon:"none",
        rejectIcon:"none",

        accept: () => {
            this.deleteRestaurant(restaurant);
        }
    });
  }

  addRestaurant(): void {
    this.router.navigate(['restaurant/edit']);
  }

  editRestaurant(id: string): void {
    this.router.navigate(['restaurant/edit', { queryParams: {id: id}}]);
  }
}

