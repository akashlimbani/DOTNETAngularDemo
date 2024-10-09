import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  constructor(private router: Router) {}

  navigateToUsers() {
    this.router.navigate(['/users']); // Redirects to User List
  }

  navigateToCreateUser() {
    this.router.navigate(['/create-user']); // Redirects to Create User
  }
}
