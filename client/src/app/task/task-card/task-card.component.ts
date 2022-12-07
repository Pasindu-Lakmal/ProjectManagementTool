import { Component, Input, OnInit } from '@angular/core';
import { Todo } from 'src/app/_models/todo';
import { AccountService } from 'src/app/_services/account.service';
import { TodoService } from 'src/app/_services/todo.service';

@Component({
  selector: 'app-task-card',
  templateUrl: './task-card.component.html',
  styleUrls: ['./task-card.component.css'],
})
export class TaskCardComponent implements OnInit {
  @Input() todo: Todo | undefined;
  currentUserName: string;
  constructor(
    private todoService: TodoService,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.getCurrentUser();
    console.log(this.todo);
  }

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

  getCurrentUser() {
    this.accountService.currentUser$.subscribe({
      next: (res) => {
        this.currentUserName = res.username;
      },
    });
  }
}
