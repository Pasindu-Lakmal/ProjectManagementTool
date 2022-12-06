import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Todo } from 'src/app/_models/todo';
import { AccountService } from 'src/app/_services/account.service';
import { TodoService } from 'src/app/_services/todo.service';

@Component({
  selector: 'app-task-creater-list',
  templateUrl: './task-creater-list.component.html',
  styleUrls: ['./task-creater-list.component.css'],
})
export class TaskCreaterListComponent implements OnInit {
  currentUserName: string;
  workId: number;
  workName: string;
  todos: Todo[] = [];
  constructor(
    private route: ActivatedRoute,
    private todoService: TodoService,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.getCurrentUser();
    this.loadTodo();
  }

  loadTodo() {
    this.todoService.getTodoByCreater(this.currentUserName).subscribe({
      next: (response) => {
        this.todos = response;
        console.log(this.todos);
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
