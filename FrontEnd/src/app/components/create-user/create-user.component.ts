import { Component } from '@angular/core';
import { UserService } from '../../services/user.service';
import { User } from '../../models/user.model';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule] // Only include FormsModule and CommonModule here
})
export class CreateUserComponent {
  user: User = new User();

  constructor(private userService: UserService, private router: Router) {}

  onSubmit(form: NgForm) {
    if (form.valid) {
      this.userService.createUser(this.user).subscribe(() => {
        alert('User created successfully');
        this.router.navigate(['/users']);
      }, error => {
        alert('An error occurred while creating the user');
      });
    }
  }

  onCancel() {
    this.router.navigate(['/']); // Redirect to the home page when cancel is clicked
  }
}
