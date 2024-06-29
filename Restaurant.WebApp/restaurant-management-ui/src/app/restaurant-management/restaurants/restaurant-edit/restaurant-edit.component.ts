import { Component } from '@angular/core';
import { Restaurant } from '../../../shared/domain/models/restaurant.model';
import { Router } from '@angular/router';
import { RestaurantService } from '../../../shared/services/restaurant-service.service';
import { MessageService } from 'primeng/api';
import { ImportsModule } from '../../imports';

@Component({
  selector: 'app-restaurant-edit',
  standalone: true,
  imports: [ImportsModule],
  templateUrl: './restaurant-edit.component.html',
  styleUrl: './restaurant-edit.component.scss',
  providers: [MessageService]
})
export class RestaurantEditComponent {
  restaurant: Restaurant = new Restaurant();

  constructor(private router: Router,
    private restaurantService: RestaurantService,
    private messageService: MessageService
  ) {}

  navigateToRestaurants(): void {
    this.router.navigate(['restaurants']);
  }

  saveRestaurant(isValid): void {
    if(!isValid) {
      return;
    }
    this.restaurantService.saveRestaurant(this.restaurant).subscribe({next: (restaurant) => {
      this.messageService.add({ severity: 'success', summary: 'Saved', detail: 'Restaurant Saved Successfully' });
      this.navigateToRestaurants();
    }, error: (error) => {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: error.error });
    } })
  }
}
