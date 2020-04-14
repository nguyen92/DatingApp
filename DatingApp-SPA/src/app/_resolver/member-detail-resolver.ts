import {Injectable} from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { User } from '../_models/user';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';

@Injectable()

export class MemberDetailResolver implements Resolve<User> {
    constructor(private router: Router,
                private userService: UserService,
                private alertify: AlertifyService) {}
    resolve(route: ActivatedRouteSnapshot): Observable<User> {
        return this.userService.getUser(route.params.id).pipe(
            catchError(error => {
                this.alertify.error('cannot retrieving data!');
                this.router.navigate(['/members']);
                return of(null);
            })
        );
    }
}
