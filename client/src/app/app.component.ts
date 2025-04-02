//import { CommonModule, NgFor } from '@angular/common';
//import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from "./nav/nav.component";
import { AccountService } from './_services/account.service';
import { HomeComponent } from "./home/home.component";
import { NgxSpinnerComponent } from 'ngx-spinner';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavComponent,NgxSpinnerComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  
  
  private accountService=inject(AccountService);
  // title = 'DatingApp';
 
  
  ngOnInit(): void {


  this.setCurrentUser();
  }

  setCurrentUser(){
    const userString=localStorage.getItem('user');
    if(!userString) return;
    const user =JSON.parse(userString);
    this.accountService.setCurrentUser(user);
  }

 
 
}
