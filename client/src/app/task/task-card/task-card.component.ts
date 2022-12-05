import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Todo } from 'src/app/_models/todo';
import { TodoService } from 'src/app/_services/todo.service';

@Component({
  selector: 'app-task-card',
  templateUrl: './task-card.component.html',
  styleUrls: ['./task-card.component.css'],
})
export class TaskCardComponent implements OnInit {
  @Input() todo: Todo | undefined;
  constructor(private todoService: TodoService, private router: Router) {}

  ngOnInit(): void {}

  deleteTodo(todoId: number) {
    console.log('delete click');
    this.todoService.deleteTodo(todoId).subscribe({
      next: (responce) => {
        location.reload();
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}
