//import { CommonModule, NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  
  http=inject(HttpClient);
  title = 'DatingApp';
  users:any;
  
  ngOnInit(): void {

    this.http.get('http://localhost:5000/api/users').subscribe({
      next:Response=>this.users=Response,
      error:error=>console.log(error),
      complete:()=>console.log('Requests has completed')

    })


  }
 
}
