import { Component, inject, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { AccountService } from 'src/app/_services/account.service';
import { MemberService } from 'src/app/_services/member.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit{
  member?: Member;
  private accountService = inject(AccountService);
  private memberService = inject(MemberService);

  ngOnInit(): void {

  }

  loadMember(){
    const user = this.accountService.currentUser();
    if(!user)
      return;

    this.memberService.getMember(user.userName).subscribe({
      next: member => this.member = member
    })
  }
}
