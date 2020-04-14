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
constructor(public authService: AuthService,
            private alerify: AlertifyService,
            private router: Router) {}
ngOnInit() {}
login() {
 this.authService.login(this.model).subscribe(next => {
  this.alerify.success('login successed');
 },
 error => {
   this.alerify.error(error);
 });
}
loggedIn() {
  return this.authService.loggedIn();
  }
logout() {
    localStorage.removeItem('token');
    this.alerify.message('logged out !!');
    this.authService.decodedToken = null;
    this.router.navigate(['/home']);
 }
}
