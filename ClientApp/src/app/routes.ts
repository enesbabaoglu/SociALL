import { Routes } from "@angular/router";
import { FriendListComponent } from "./friend-list/friend-list.component";
import { HomeComponent } from "./home/home.component";
import { MemberDetailsComponent } from "./members/member-details/member-details.component";
import { MemberEditComponent } from "./members/member-edit/member-edit.component";
import { MemberListComponent } from "./members/member-list/member-list.component";
import { MessageComponent } from "./message/message.component";
import { NotfoundComponent } from "./notfound/notfound.component";
import { AuthGuard } from "./_guards/auth-guard";
import { MemberDetailsResolver } from "./_resolver/member-Details.resolver";
import { MemberEditResolver } from "./_resolver/member-edit.resolver";

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'members', component: MemberListComponent, canActivate: [AuthGuard] },
    { path: 'member/edit', component: MemberEditComponent,resolve:{user : MemberEditResolver}, canActivate: [AuthGuard] },
    { path: 'members/:id', component: MemberDetailsComponent,resolve:{user : MemberDetailsResolver}, canActivate: [AuthGuard] },
    { path: 'members/:id', component: MemberDetailsComponent, canActivate: [AuthGuard] },
    { path: 'friends', component: FriendListComponent, canActivate: [AuthGuard] },
    { path: 'home', component: HomeComponent},
    { path: 'messages', component: MessageComponent ,canActivate: [AuthGuard]},
    { path: '**', component: NotfoundComponent }
]
