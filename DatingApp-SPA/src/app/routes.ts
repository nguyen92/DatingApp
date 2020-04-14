import { Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';
import { MemberListsComponent } from './members/member-lists/member-lists.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberDetailResolver } from './_resolver/member-detail-resolver';
import { MemberListResolver } from './_resolver/member-list-resolver';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberEditResolver } from './_resolver/member-edit-resolver';


export const appRoutes: Routes = [
    {path: 'home', component: HomeComponent},
    {path: 'member/modified', runGuardsAndResolvers: 'always', component: MemberEditComponent, resolve: {user: MemberEditResolver}},
    {path: 'member/:id', component: MemberDetailComponent, resolve: {user: MemberDetailResolver}},
    {path: 'member', component: MemberListsComponent, canActivate: [AuthGuard], resolve: {users: MemberListResolver}},
    {path: 'messages', component: MessagesComponent},
    {path: '**', redirectTo: 'home', pathMatch: 'full'}
];
