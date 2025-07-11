import { Component, inject } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  accountService = inject(AccountService);

  model: any = {}

  private router = inject(Router);
  private toastr = inject(ToastrService);

login(){
  this.accountService.login(this.model).subscribe({
    next: _ => {
      this.router.navigateByUrl('/members');
    },
    error: error => this.toastr.error(error.error)
  })
}

logout(){
  this.accountService.logout();
  this.router.navigateByUrl('/');
}
}
