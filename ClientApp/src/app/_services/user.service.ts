import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl: string = "http://localhost:5000/api/User/";

  constructor(private http: HttpClient) { }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl);
  }
  getUser(id: number): Observable<User> {
    return this.http.get<User>(this.baseUrl + id);
  }

  updateUser(id: number, user: User) {
    return this.http.put(this.baseUrl + id, user);
  }
  followUser(followerId: number, userId: number) {
    return this.http.post(this.baseUrl + followerId + '/follow/' + userId, {});
  }
  isFollowedUser(followerId: number, userId: number) {
    return this.http.get(this.baseUrl + followerId + '/isFollowedUser/' + userId, {});
  }
}
