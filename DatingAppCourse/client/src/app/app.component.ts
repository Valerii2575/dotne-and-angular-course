import { Component, inject, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],

})
export class AppComponent  implements OnInit{
  title = 'client';

  private accountService = inject(AccountService);

  constructor(){

  }

  ngOnInit(): void {

    this.setCurrentUser();
  }

  setCurrentUser(){
    const userString = localStorage.getItem('user');
    if(!userString)
      return;

    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);
  }


}
