import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  @Input() usersFromHomeComponent: any;
  @Output() canceledRegister = new EventEmitter();

  private accountService = inject(AccountService);
  private toastr = inject(ToastrService);

  //usersFromHomeComponent = input.required<any>();
  //canceledRegister = output<boolean>();

  model: any ={}

  register(){
    console.log(this.model);
    this.accountService.register(this.model).subscribe({
      next: response => {
        this.toastr.success(response.userName);
        this.cancel();
      },
      error: error => this.toastr.error(error.error)
    });
  }

  cancel(){
    this.canceledRegister.emit(false);
  }

}
