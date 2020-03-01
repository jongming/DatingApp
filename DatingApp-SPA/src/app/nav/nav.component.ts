import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})

export class NavComponent implements OnInit {
  model: any = {};
  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  login() {
    console.log('*****nav.components.ts login() model:' + this.model + Date().toString());
    this.authService.login(this.model).subscribe(next => {
      console.log('Logged in successfuly!');
    }, error => {
      console.log(error);
    });
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    // Short form for true statement.  !! will return a true or false. In this case, if token is not null, it will return true
    return !!token;
  }

  loggedOut() {
    localStorage.removeItem('token');
    console.log('loggedOut');
  }


}
