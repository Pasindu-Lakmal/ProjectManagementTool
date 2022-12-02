import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  UntypedFormGroup,
  ValidatorFn,
  Validators,
  FormBuilder,
  FormGroup,
} from '@angular/forms';
import { Router } from '@angular/router';
import { getMinutes } from 'ngx-bootstrap/chronos/utils/date-getters';
import { ToastrService } from 'ngx-toastr';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { UserParams } from 'src/app/_models/userParams';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { WorkService } from 'src/app/_services/work.service';

@Component({
  selector: 'app-work-add',
  templateUrl: './work-add.component.html',
  styleUrls: ['./work-add.component.css'],
})
export class WorkAddComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  addWork: FormGroup;
  maxDate: Date = new Date();
  validationErrors: string[] | undefined;
  user: string[] | undefined;
  userParams: UserParams | undefined;
  members: Member[] = [];

  constructor(
    private workService: WorkService,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private router: Router
  ) {}

  ngOnInit(): void {
    console.log(this.members);
    this.initializeForm();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);
  }

  initializeForm() {
    //remove new formcontroler using form builder
    this.addWork = this.fb.group({
      workName: [
        '',
        [
          Validators.required,
          Validators.minLength(5),
          Validators.maxLength(20),
        ],
      ],
      workDescription: [
        '',
        [
          Validators.required,
          Validators.minLength(15),
          Validators.maxLength(400),
        ],
      ],
    });
  }
  addWorks() {
    this.workService.addWork(this.addWork.value).subscribe({
      next: (responce) => {
        console.log(responce);
        this.router.navigateByUrl('/project');
      },
      error: (error) => {
        this.validationErrors = error;
      },
    });
  }
}
