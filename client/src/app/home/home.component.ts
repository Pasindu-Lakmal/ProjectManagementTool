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
  // complete: number;

  // pending: number;
  // count: number[];

  constructor(
    public accountService: AccountService,
    private todoService: TodoService
  ) {}

  ngOnInit(): void {
    this.getCurrentUser();
    this.loadTodo();
    // this.getCounts(this.todos).then((val) => console.log(val));
    let count: any = this.getCounts(this.todos);
    console.log(count);
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
        // this.getCounts(this.todos);
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

  // getCount(todos: Todo[]) {
  //   var pending: number = 0;
  //   var complete: number = 0;
  //   var tod: number = 0;
  //   todos.forEach((todo) => {
  //     if (todo.status === 'pending') {
  //       pending = pending + 1;
  //     } else if (todo.status === 'complete') {
  //       complete = complete + 1;
  //     } else if (todo.status === 'todo') {
  //       tod = tod + 1;
  //     }
  //   });
  //   console.log(pending);
  //   // return [pending, tod, complete];
  // }

  test(arg: string): Promise<number> {
    return new Promise<number>((resolve, reject) => {
      if (arg === 'a') {
        resolve(1);
      } else {
        reject('1');
      }
    });
  }

  getCounts(todos: Todo[]): Promise<number[]> {
    let pending: number = 0;
    let complete: number = 0;
    let tod: number = 0;
    return new Promise<number[]>((resolve, reject) => {
      todos.forEach((todo) => {
        if (todo.status === 'pending') {
          pending = pending + 1;
        } else if (todo.status === 'complete') {
          complete = complete + 1;
        } else if (todo.status === 'todo') {
          tod = tod + 1;
        }
      });
      resolve([pending, tod, complete]);
    });
  }
}
