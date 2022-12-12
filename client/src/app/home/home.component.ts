import { Component, OnInit } from '@angular/core';
import { Todo } from '../_models/todo';
import { AccountService } from '../_services/account.service';
import { TodoService } from '../_services/todo.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  currentUserName: string;
  todos: Todo[] = [];
  registerMode = false;
  users: any;
  pending: number = 0;
  complete: number = 0;
  todo: number = 0;

  constructor(
    public accountService: AccountService,
    private todoService: TodoService
  ) {}

  ngOnInit(): void {
    this.getCurrentUser();
    this.loadTodo();
    // this.getCount(this.todos);
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
    console.log(`registerMoede: ${this.registerMode}`);
  }

  cancelRegisterMode(evet: boolean) {
    this.registerMode = evet;
  }

  loadTodo() {
    this.todoService.getTodoByAssignee(this.currentUserName).subscribe({
      next: (response) => {
        this.todos = response;
        this.getCount(this.todos);
      },
    });
  }

  getCurrentUser() {
    this.accountService.currentUser$.subscribe({
      next: (res) => {
        if (res) {
          this.currentUserName = res.username;
          console.log(this.currentUserName);
        }
      },
    });
  }

  getCount(todos: Todo[]) {
    todos.forEach((todo) => {
      if (todo.status === 'pending') {
        this.pending = this.pending + 1;
      } else if (todo.status === 'complete') {
        this.complete = this.complete + 1;
      } else if (todo.status === 'todo') {
        this.todo = this.todo + 1;
      }
    });
  }
}
