import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  @Input() usersFromHomeComponent: any;
  @Output() canceledRegister = new EventEmitter();

  private accountService = inject(AccountService);

  //usersFromHomeComponent = input.required<any>();
  //canceledRegister = output<boolean>();

  model: any ={}

  register(){
    console.log(this.model);
    this.accountService.register(this.model).subscribe({
      next: response => {
        console.log(response);
        this.cancel();
      },
      error: error => console.log(error)
    });
  }

  cancel(){
    this.canceledRegister.emit(false);
  }

}
