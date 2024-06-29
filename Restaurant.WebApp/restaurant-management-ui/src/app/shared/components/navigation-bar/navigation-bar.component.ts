import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MenuItem, PrimeTemplate } from 'primeng/api';
import { MenubarModule } from 'primeng/menubar';

@Component({
  selector: 'navigation-bar',
  standalone: true,
  imports: [MenubarModule, RouterModule, CommonModule],
  templateUrl: './navigation-bar.component.html',
  styleUrl: './navigation-bar.component.scss'
})
export class NavigationBarComponent implements OnInit {
  navigationLinks: Array<MenuItem> = [];

  ngOnInit(): void {
    this.navigationLinks = [
      {
        label: 'Restaurants',
        icon: 'pi pi-home',
        route: 'restaurants'
      }
    ]
  }
}
