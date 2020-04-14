import { Component } from '@angular/core';
import { AuthService } from './_services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
 model: any = {};
 constructor(private authService: AuthService) {}
login(){
  this.authService.login(this.model).subscribe(next => {
    console.log('login successed');
  },
  error => {
    console.log('login failed');
  });
}
}
