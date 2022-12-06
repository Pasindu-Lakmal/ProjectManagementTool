import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-task-edit',
  templateUrl: './task-edit.component.html',
  styleUrls: ['./task-edit.component.css'],
})
export class TaskEditComponent implements OnInit {
  todoId: Number;

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.todoId = Number(this.route.snapshot.paramMap.get('taskId'));
    console.log(this.todoId);
  }
}
