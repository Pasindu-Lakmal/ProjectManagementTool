import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Todo } from 'src/app/_models/todo';
import { TodoService } from 'src/app/_services/todo.service';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css'],
})
export class TaskListComponent implements OnInit {
  workId: number;
  workName: string;
  todos: Todo[] = [];
  constructor(
    private route: ActivatedRoute,
    private todoService: TodoService
  ) {}

  ngOnInit(): void {
    this.workId = Number(this.route.snapshot.paramMap.get('workId'));
    this.getWorkName();
    this.loadTodo();
  }
  loadMember() {}

  loadTodo() {
    this.todoService.getTodo(this.workId).subscribe({
      next: (response) => {
        this.todos = response;
        console.log(this.todos);
      },
    });
  }

  getWorkName() {
    this.todoService.getWorkName(this.workId).subscribe({
      next: (res) => {
        console.log(res);
        this.workName = JSON.stringify(res).slice(1, -1);
      },
    });
  }
}
