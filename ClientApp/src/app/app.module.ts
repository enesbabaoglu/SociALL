import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { JwtModule } from "@auth0/angular-jwt";
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { RegisterComponent } from './register/register.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { FriendListComponent } from './friend-list/friend-list.component';
import { HomeComponent } from './home/home.component';
import { NotfoundComponent } from './notfound/notfound.component';
import { MessageComponent } from './message/message.component';
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';
import { AuthGuard } from './_guards/auth-guard';
import { ErrorIntercaptor } from './_services/error.intercaptor';
import { MemberDetailsComponent } from './members/member-details/member-details.component';
import { PhotoGalleryComponent } from './photo-gallery/photo-gallery.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberEditResolver } from './_resolver/member-edit.resolver';
import { TimeagoModule } from 'ngx-timeago';
import { MemberDetailsResolver } from './_resolver/member-Details.resolver';

export function tokenGetter(){
  return localStorage.getItem("token");
}
@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    RegisterComponent,
    MemberListComponent,
    FriendListComponent,
    HomeComponent,
    NotfoundComponent,
    MessageComponent,
    MemberDetailsComponent,
    PhotoGalleryComponent,
    MemberEditComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    TimeagoModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:5000"],
        disallowedRoutes: ["localhost:5000/api/auth"],
      },
    }),
    RouterModule.forRoot(appRoutes)
  ],
  providers: [AuthGuard,{
    provide:HTTP_INTERCEPTORS,
    useClass : ErrorIntercaptor,
    multi:true
  },
  MemberEditResolver,
  MemberDetailsResolver],
  bootstrap: [AppComponent]
})
export class AppModule { }
