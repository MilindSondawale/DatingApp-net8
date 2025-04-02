import { AfterViewChecked, Component, inject, Input, input, OnInit, output, viewChild, ViewChild } from '@angular/core';
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
export class MemberMessagesComponent implements AfterViewChecked {

  @ViewChild('messageForm') messageForm?:NgForm;
  @ViewChild('scrollMe') scrollContainer?:any;
   messageService = inject(MessageService);
  username = input.required<string>();
  //messages = input.required<Message []>();
  messageContent = '';
  //updateMessages = output<Message>();
  loading = false; //252


 
  sendMessage(){
    this.loading = true //252
    this.messageService.sendMessage(this.username(), this.messageContent).then(() =>{
      this.messageForm?.reset();
      this.scrollToBottom();
    }).finally(() => this.loading = false); //252 from finally
       
  }

  ngAfterViewChecked(): void {
    this.scrollToBottom();
  }


  private scrollToBottom(){
    if(this.scrollContainer) {
      this.scrollContainer.nativeElement.scrollTop = this.scrollContainer.nativeElement.scrollHeight;
    }
  }
}