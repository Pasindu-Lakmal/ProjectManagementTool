import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment';
import { Todo } from '../_models/todo';

@Injectable({
  providedIn: 'root',
})
export class TodoService {
  todo: Todo[] = [];
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getTodo(workId: number) {
    return this.http.get<Todo[]>(this.baseUrl + 'todo/' + workId);
  }

  addTodo(model: any) {
    return this.http.post(this.baseUrl + 'todo', model);
  }
  deleteTodo(todoId: number) {
    return this.http.delete(this.baseUrl + 'todo/delete/' + todoId, {});
  }

  getWorkName(workId: number) {
    return this.http.get(this.baseUrl + 'todo/workName/' + workId);
  }

  getTodoByAssignee(assigneeName: string) {
    return this.http.get<Todo[]>(
      this.baseUrl + 'todo/assigneename/' + assigneeName
    );
  }
}
