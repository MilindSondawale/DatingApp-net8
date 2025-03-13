import { Component, inject, Input, input, OnInit, output, ViewChild } from '@angular/core';
import { MessageService } from '../../_services/message.service';
import { Message } from '../../_models/message';
import { TimeagoModule } from 'ngx-timeago';
import { FormsModule, NgForm } from '@angular/forms';

@Component({
  selector: 'app-member-messages',
  imports: [TimeagoModule,FormsModule],
  templateUrl: './member-messages.component.html',
  styleUrl: './member-messages.component.css'
})
export class MemberMessagesComponent  {

  @ViewChild('messageForm') messageForm?:NgForm;
   messageService = inject(MessageService);
  username = input.required<string>();
  //messages = input.required<Message []>();
  messageContent = '';
  //updateMessages = output<Message>();


 
  sendMessage(){
    this.messageService.sendMessage(this.username(), this.messageContent).then(() =>{
      this.messageForm?.reset();
    })
       
  }
}