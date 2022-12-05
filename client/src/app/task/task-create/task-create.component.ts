import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';
import { TodoService } from 'src/app/_services/todo.service';

@Component({
  selector: 'app-task-create',
  templateUrl: './task-create.component.html',
  styleUrls: ['./task-create.component.css'],
})
export class TaskCreateComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  addTodo: FormGroup;
  minDate: Date = new Date();
  validationErrors: string[] | undefined;
  user: string[] | undefined;
  members: Member[] = [];
  workId: number;

  constructor(
    private todoService: TodoService,
    private memberService: MembersService,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.load();
    this.workId = Number(this.route.snapshot.paramMap.get('workId'));
    console.log(this.workId);
    this.minDate.setFullYear(this.minDate.getFullYear());
    this.minDate.setMonth(this.minDate.getMonth());
    this.minDate.setDate(this.minDate.getDate());
    this.initializeForm();
  }

  initializeForm() {
    this.addTodo = this.fb.group({
      title: [
        '',
        [
          Validators.required,
          Validators.minLength(5),
          Validators.maxLength(20),
        ],
      ],
      description: [
        '',
        [
          Validators.required,
          Validators.minLength(15),
          Validators.maxLength(400),
        ],
      ],
      dueDate: ['', [Validators.required]],
      assigneeName: ['', [Validators.required]],
    });
  }

  addTodos() {
    const dueDate = this.getDateOnly(this.addTodo.controls['dueDate'].value);
    const values = {
      ...this.addTodo.value,
      dueDate: dueDate,
      workId: this.workId,
    };

    this.todoService.addTodo(values).subscribe({
      next: (responce) => {
        console.log(responce);
        this.router.navigateByUrl(`/task/list/${this.workId}`);
      },
      error: (error) => {
        this.validationErrors = error;
      },
    });
  }

  load() {
    this.memberService.getMembersForSelect().subscribe({
      next: (response) => {
        this.members = response;
        console.log(this.members);
      },
    });
  }

  private getDateOnly(dueDate: string | undefined) {
    if (!dueDate) return;
    let theDueDate = new Date(dueDate);
    return new Date(
      theDueDate.setMinutes(
        theDueDate.getMinutes() - theDueDate.getTimezoneOffset()
      )
    )
      .toISOString()
      .slice(0, 10);
  }
}
