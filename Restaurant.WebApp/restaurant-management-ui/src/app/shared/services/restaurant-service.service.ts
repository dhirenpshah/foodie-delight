import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Restaurant } from '../domain/models/restaurant.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class RestaurantService {  

  apiUrl: string = 'http://localhost:5241';

  constructor(private http: HttpClient) { }

  loadRestaurants(): Observable<Array<Restaurant>> {
    return this.http.get<Array<Restaurant>>(`${this.apiUrl}/restaurants`);
  }

  loadRestaurantById(id: string): Observable<Restaurant> {
    return this.http.get<Restaurant>(`${this.apiUrl}/restaurants/${id}`);
  }

  saveRestaurant(restaurant: Restaurant): Observable<Restaurant> {
    return this.http.post<Restaurant>(`${this.apiUrl}/restaurants`, restaurant);
  }

  deleteRestaurant(id: string): Observable<Restaurant> {
    return this.http.delete<Restaurant>(`${this.apiUrl}/restaurants/${id}`);
  }
}
