import { Component, OnInit } from '@angular/core';
import { Work } from 'src/app/_models/work';
import { WorkService } from 'src/app/_services/work.service';

@Component({
  selector: 'app-work-list',
  templateUrl: './work-list.component.html',
  styleUrls: ['./work-list.component.css'],
})
export class WorkListComponent implements OnInit {
  work: Work[] = [];
  constructor(private workService: WorkService) {}

  ngOnInit(): void {
    this.loadWork();
  }

  loadWork() {
    this.workService.getWorks().subscribe({
      next: (response) => {
        this.work = response;
        console.log(this.work);
      },
    });
  }
}
