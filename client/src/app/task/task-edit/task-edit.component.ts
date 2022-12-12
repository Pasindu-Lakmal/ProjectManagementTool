import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Todo } from 'src/app/_models/todo';
import { TodoService } from 'src/app/_services/todo.service';

@Component({
  selector: 'app-task-edit',
  templateUrl: './task-edit.component.html',
  styleUrls: ['./task-edit.component.css'],
})
export class TaskEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  todoId: number;
  todo: Todo;
  @HostListener('window:beforeunload', ['$event']) unloadNotification(
    $event: any
  ) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(
    private todoService: TodoService,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.todoId = Number(this.route.snapshot.paramMap.get('taskId'));
    this.loadTodo();
  }

  loadTodo() {
    this.todoService.getTodoById(this.todoId).subscribe({
      next: (res) => {
        this.todo = res;
        console.log(this.todo);
      },
    });
  }

  updateWork() {
    this.todoService.updateWork(this.todo).subscribe({
      next: () => {
        this.toastr.success('Task updated succeddfully');
        this.editForm.reset(this.todo);
        this.router.navigateByUrl('/task/list/' + this.todo.workId);
      },
    });
  }
}
