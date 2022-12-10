import { Component, OnInit } from '@angular/core';
import { Work } from 'src/app/_models/work';
import { AccountService } from 'src/app/_services/account.service';
import { WorkService } from 'src/app/_services/work.service';

@Component({
  selector: 'app-work-list',
  templateUrl: './work-list.component.html',
  styleUrls: ['./work-list.component.css'],
})
export class WorkListComponent implements OnInit {
  taskMode = false;
  works: Work[] = [];
  constructor(
    private workService: WorkService,
    public accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.loadWork();
  }

  loadWork() {
    this.workService.getWorks().subscribe({
      next: (response) => {
        this.works = response;
        console.log(this.works);
      },
    });
  }

  changeToTaskMode() {
    this.taskMode = true;
  }
}
