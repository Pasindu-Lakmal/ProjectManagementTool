import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs';
import { Work } from 'src/app/_models/work';
import { WorkService } from 'src/app/_services/work.service';

@Component({
  selector: 'app-work-list',
  templateUrl: './work-list.component.html',
  styleUrls: ['./work-list.component.css'],
})
export class WorkListComponent implements OnInit {
  works: Work[] = [];
  constructor(private workService: WorkService) {}

  ngOnInit(): void {
    this.loadWork();
  }

  loadWork() {
    this.workService.getWorks().subscribe({
      next: (response) => {
        this.works = response;
        console.log(this.works);
        console.log(this.works[1].workId);
      },
    });
  }
}
