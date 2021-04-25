import { Routes } from "@angular/router";
import { FriendListComponent } from "./friend-list/friend-list.component";
import { HomeComponent } from "./home/home.component";
import { MemberListComponent } from "./member-list/member-list.component";
import { MessageComponent } from "./message/message.component";
import { NotfoundComponent } from "./notfound/notfound.component";
import { AuthGuard } from "./_guards/auth-guard";

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'members', component: MemberListComponent, canActivate: [AuthGuard] },
    { path: 'friends', component: FriendListComponent, canActivate: [AuthGuard] },
    { path: 'home', component: HomeComponent},
    { path: 'messages', component: MessageComponent ,canActivate: [AuthGuard]},
    { path: '**', component: NotfoundComponent }
]
