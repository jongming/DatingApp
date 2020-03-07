import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})

export class NavComponent implements OnInit {
  model: any = {};
  constructor(public authService: AuthService, private alertify: AlertifyService,
    private router: Router) { }

  ngOnInit() {
  }

  login() {
    console.log('*****nav.components.ts login() model:' + this.model + Date().toString());
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Logged in successfuly!');
    }, error => {
      this.alertify.error(error);
    }, () => { /** anonymous function */
      this.router.navigate(['/members']);
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  loggedOut() {
    localStorage.removeItem('token');
    this.alertify.message('loggedOut');
    this.router.navigate(['/home']);
  }


}
