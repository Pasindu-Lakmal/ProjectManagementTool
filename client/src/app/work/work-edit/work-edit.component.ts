import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Work } from 'src/app/_models/work';
import { WorkService } from 'src/app/_services/work.service';

@Component({
  selector: 'app-work-edit',
  templateUrl: './work-edit.component.html',
  styleUrls: ['./work-edit.component.css'],
})
export class WorkEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  workId: number;
  work: Work;
  @HostListener('window:beforeunload', ['$event']) unloadNotification(
    $event: any
  ) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(
    private workService: WorkService,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.workId = Number(this.route.snapshot.paramMap.get('workId'));
    this.loadWork();
    console.log(this.workId);
  }

  loadWork() {
    this.workService.getWork(this.workId).subscribe({
      next: (res) => {
        this.work = res;
        console.log(this.work);
      },
    });
  }

  updateWork() {
    this.workService.updateWork(this.work).subscribe({
      next: () => {
        this.toastr.success('Profile updated succeddfully');
        this.router.navigateByUrl('/project');
      },
    });
  }
}
