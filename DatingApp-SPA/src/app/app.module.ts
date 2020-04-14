import { BrowserModule, HammerGestureConfig, HAMMER_GESTURE_CONFIG  } from '@angular/platform-browser';
import { NgModule, Injectable} from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { FormsModule } from '@angular/forms';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { TabsModule } from 'ngx-bootstrap';
import { ListsComponent } from './lists/lists.component';

import { MessagesComponent } from './messages/messages.component';

import { appRoutes } from './routes';
import { UserService } from './_services/user.service';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { MemberListsComponent } from './members/member-lists/member-lists.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { NgxGalleryModule } from 'ngx-gallery';
import { RouterModule } from '@angular/router';
import { MemberListResolver } from './_resolver/member-list-resolver';
import { MemberDetailResolver } from './_resolver/member-detail-resolver';
import { MemberEditResolver } from './_resolver/member-edit-resolver';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MemberEditComponent } from './members/member-edit/member-edit.component';


export function tokenGetter() {
   return localStorage.getItem('token');
 }


@Injectable()
export class CustomHammerConfig extends HammerGestureConfig {
   overrides = {
      pinch: { enable: false},
      rotate: {enable: false}
   };
}
@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      ListsComponent,
      MemberListsComponent,
      MessagesComponent,
      MemberCardComponent,
      MemberDetailComponent,
      MemberEditComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      NgxGalleryModule,
      BrowserAnimationsModule,
      BsDropdownModule.forRoot(),
      TabsModule.forRoot(),
      RouterModule.forRoot(appRoutes)
   ],
   providers: [
      AuthService,
      UserService,
      MemberListResolver,
      MemberDetailResolver,
      MemberEditResolver,
      { provide: HAMMER_GESTURE_CONFIG, useClass: CustomHammerConfig },
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
