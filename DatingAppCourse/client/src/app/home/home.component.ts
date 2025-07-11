import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  users: any;
  http = inject(HttpClient);

  constructor(){

  }

  ngOnInit(): void {
    this.getUser();
  }

  registerToggle(){
    this.registerMode = !this.registerMode;
  }

  getUser(){
    this.http.get('https://localhost:5001/api/User/users').subscribe({
      next: result => {
        this.users = result;
      },
      error: () => {},
      complete: () => {}
    })
  }

  cancelRegisterMode(event: boolean){
    this.registerMode = event;
  }
}
