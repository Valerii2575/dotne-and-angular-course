import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Action } from 'rxjs/internal/scheduler/Action';
import { Member } from 'src/app/_models/member';
import { MemberService } from 'src/app/_services/member.service';
@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit{
  private memberService = inject(MemberService);
  private route = inject(ActivatedRoute);
  member?: Member;


  ngOnInit(): void {
    this.loadMember();
  }

  loadMember(){
    const username = this.route.snapshot.paramMap.get('id');
    if(!username) return;

    this.memberService.getMember(username).subscribe({
      next: member => this.member = member
    })
  }
}
