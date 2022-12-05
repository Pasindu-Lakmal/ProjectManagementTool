import { Component, Input, OnInit } from '@angular/core';
import { Todo } from 'src/app/_models/todo';
import { AccountService } from 'src/app/_services/account.service';
import { TodoService } from 'src/app/_services/todo.service';

@Component({
  selector: 'app-task-my-card',
  templateUrl: './task-my-card.component.html',
  styleUrls: ['./task-my-card.component.css'],
})
export class TaskMyCardComponent implements OnInit {
  @Input() todo: Todo | undefined;
  currentUserName: string;
  constructor(
    private todoService: TodoService,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.getCurrentUser();
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
