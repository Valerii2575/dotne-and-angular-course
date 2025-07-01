import { Component, inject } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  accountService = inject(AccountService);
model: any = {}

login(){
  this.accountService.login(this.model).subscribe({
    next: response => {
      console.log(response);
    },
    error: error => console.log(error)
  })
}

logout(){
  this.accountService.logout();
}
}
