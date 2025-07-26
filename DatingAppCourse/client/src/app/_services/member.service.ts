import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { Member } from '../_models/member';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class MemberService {

  baseUrel = environment.apiUrl;
  private http = inject(HttpClient);
  private aaccountService = inject(AccountService);
  constructor() { }

  getMembers(){
    return this.http.get<Member[]>(this.baseUrel + 'User/users');
  }

  getMember(username: string){
    return this.http.get<Member>(this.baseUrel + 'User/users/byname/' + username);
  }

  getHttpOptions(){
    return {
      headers: new HttpHeaders({
        Authorization: `Bearer ${this.aaccountService.currentUser()?.token}`
      })
    }
  }
}
