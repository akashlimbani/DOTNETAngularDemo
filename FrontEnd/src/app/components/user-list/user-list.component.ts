import { Component } from '@angular/core';
import { UserService } from '../../services/user.service';
import { User } from '../../models/user.model';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
    selector: 'app-user-list',
    templateUrl: './user-list.component.html',
    styleUrls: ['./user-list.component.css'],
    standalone: true,
    imports: [CommonModule] // Include CommonModule here
})
export class UserListComponent {
  users: User[] = [];

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit() {
    this.loadUsers();
  }

  loadUsers() {
    this.userService.getUsers().subscribe((data) => {
      this.users = data;
    });
  }

  editUser(user: User) {
    // Navigate to the edit user component with the user's ID
    this.router.navigate(['/edit-user', user.id]);
  }

  deleteUser(userId: number) {
    if (confirm('Are you sure you want to delete this user?')) {
      this.userService.deleteUser(userId).subscribe(() => {
        this.users = this.users.filter(user => user.id !== userId); // Remove user from the list
        alert('User deleted successfully');
      }, error => {
        alert('An error occurred while deleting the user');
      });
    }
  }
}
