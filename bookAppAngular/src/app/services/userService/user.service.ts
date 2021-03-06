import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from 'src/app/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) { }

  getAll() {
      return this.http.get<User[]>(`/users`);
  }
  register(user: User) {
    return this.http.post(`http://localhost:8090/api/Auth/register`, user);
  }
  delete(id: number) {
      return this.http.delete(`/users/${id}`);
  }
}