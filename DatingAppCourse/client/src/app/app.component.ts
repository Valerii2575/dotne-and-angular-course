import {  HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent  implements OnInit{
  title = 'client';
  users: any;

  constructor(private http: HttpClient){

  }

  ngOnInit(): void {
    this.http.get('https://localhost:5001/api/User/users').subscribe({
      next: result => {
        this.users = result;
      },
      error: () => {},
      complete: () => {}
    })
  }
}
