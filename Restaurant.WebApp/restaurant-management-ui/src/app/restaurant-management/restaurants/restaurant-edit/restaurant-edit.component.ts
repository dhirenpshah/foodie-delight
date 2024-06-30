import { Component } from '@angular/core';
import { Restaurant } from '../../../shared/domain/models/restaurant.model';
import { ActivatedRoute, Router } from '@angular/router';
import { RestaurantService } from '../../../shared/services/restaurant-service.service';
import { MessageService } from 'primeng/api';
import { ImportsModule } from '../../imports';
import { finalize } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-restaurant-edit',
  standalone: true,
  imports: [ImportsModule],
  templateUrl: './restaurant-edit.component.html',
  styleUrl: './restaurant-edit.component.scss',
  providers: [MessageService, NgxSpinnerService]
})
export class RestaurantEditComponent {
  restaurant: Restaurant = new Restaurant();
  id: string;

  constructor(private router: Router,
    private restaurantService: RestaurantService,
    private messageService: MessageService,
    private route: ActivatedRoute,
    private spinnerService: NgxSpinnerService
  ) {
    this.route.queryParams.subscribe({ next: (params) => {
      this.id = params['id'];
      if(this.id) {
        this.loadRestaurant();
      }
    }});
  }

  navigateToRestaurants(): void {
    this.router.navigate(['restaurants']);
  }

  saveRestaurant(isValid): void {
    if(!isValid) {
      return;
    }
    this.spinnerService.show();
    this.restaurantService.saveRestaurant(this.restaurant)
    .pipe(finalize(() => {
        this.spinnerService.hide();
    }))
    .subscribe({next: (restaurant) => {
      this.messageService.add({ severity: 'success', summary: 'Saved', detail: 'Restaurant Saved Successfully' });
      const redirectTimeout = setTimeout(() => {
        this.navigateToRestaurants();
        clearTimeout(redirectTimeout);
      }, 500);
    }, error: (error) => {
      this.showErrorMessage(error.error);
    } })
  }

  loadRestaurant() {
    this.spinnerService.show();
    this.restaurantService.loadRestaurantById(this.id)
    .pipe(finalize(() => {
      setTimeout(() => {
        this.spinnerService.hide();
      }, 1000);
    })).subscribe({ next: (restaurant) => {
      this.restaurant = restaurant;
    }, error: (error) => {
      this.showErrorMessage(error.error)
    }})
  }

  showErrorMessage(message: string): void {
    this.messageService.add({ severity: 'error', summary: 'Error', detail: message });
  }
}
