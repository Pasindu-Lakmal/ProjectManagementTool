import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Work } from 'src/app/_models/work';
import { WorkService } from 'src/app/_services/work.service';

@Component({
  selector: 'app-work-card',
  templateUrl: './work-card.component.html',
  styleUrls: ['./work-card.component.css'],
})
export class WorkCardComponent implements OnInit {
  @Input() works: Work | undefined;

  constructor(private workService: WorkService, private router: Router) {}

  ngOnInit(): void {}

  deleteWork(workId: number) {
    console.log('delete click');
    this.workService.deleteWork(workId).subscribe({
      next: (responce) => {
        location.reload();
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}
