import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.component.html'
})
export class RegisterComponent {
  name = '';
  email = '';
  error = '';

  constructor(private api: ApiService, private router: Router) {
    if (localStorage.getItem('userId')) this.router.navigate(['/courses']);
  }

  register() {
    this.error = '';
    this.api.register(this.name, this.email).subscribe({
      next: user => {
        localStorage.setItem('userId', user.id);
        localStorage.setItem('userName', user.name);
        this.router.navigate(['/courses']);
      },
      error: () => this.error = 'Registration failed. Try again.'
    });
  }
}
