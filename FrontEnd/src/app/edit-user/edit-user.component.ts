import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { UserService } from '../services/user.service';
import { User } from '../models/user.model';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule] // Ensure FormsModule is imported here
})
export class EditUserComponent {
  user: User = new User(); // Initialize the user object

  constructor(private userService: UserService, private route: ActivatedRoute, private router: Router) {}

  ngOnInit(): void {
    // Get the user ID from the route
    const userId = this.route.snapshot.paramMap.get('id');
    if (userId) {
      this.userService.getUserById(+userId).subscribe(data => {
        this.user = data; // Load the user data
      });
    }
  }

  onSubmit(form: NgForm) {
    if (form.valid) {
      this.userService.updateUser(this.user).subscribe(() => {
        alert('User updated successfully'); // Notify success
        this.router.navigate(['/users']);
      }, error => {
        alert('An error occurred while updating the user'); // Notify error
      });
    }
  }

  onCancel() {
    this.router.navigate(['/']); // Redirect to the home page when cancel is clicked
  }
}
