import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css'],
})
export class TaskListComponent implements OnInit {
  workId: number;
  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.workId = Number(this.route.snapshot.paramMap.get('workId'));
  }
  loadMember() {}
}
