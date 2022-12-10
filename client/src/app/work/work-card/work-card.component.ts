import { Component, Input, NgModule, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Work } from 'src/app/_models/work';
import { AccountService } from 'src/app/_services/account.service';
import { WorkService } from 'src/app/_services/work.service';

@Component({
  selector: 'app-work-card',
  templateUrl: './work-card.component.html',
  styleUrls: ['./work-card.component.css'],
})
export class WorkCardComponent implements OnInit {
  @Input() works: Work | undefined;

  currentUserName: string;

  constructor(
    private workService: WorkService,
    private router: Router,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.getCurrentUser();
    console.log(this.currentUserName);
  }

  deleteWork(workId: number) {
    console.log('delete click');
    this.workService.deleteWork(workId).subscribe({
      next: (responce) => {
        this.works.workId = null;
      },
      error: (error) => {},
    });
  }

  getCurrentUser() {
    this.accountService.currentUser$.subscribe({
      next: (res) => {
        this.currentUserName = res.username;
      },
    });
  }
}
