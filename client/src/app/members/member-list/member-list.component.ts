import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css'],
})
export class MemberListComponent implements OnInit {
  members$: Observable<Member[]>;
  constructor(private memberService: MembersService) {}

  ngOnInit(): void {
    //we subscribe our obsrvable using async pipe in tmpleteb
    this.members$ = this.memberService.getMembers();
  }

  //it directy get from member service
  // loadMembers() {
  //   this.memberService.getMembers().subscribe({
  //     next: (members) => (this.members = members),
  //     error: (error) => console.log(error),
  //   });
  // }
}
