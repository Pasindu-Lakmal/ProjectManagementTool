import { Component, Input, OnInit } from '@angular/core';
import { Work } from 'src/app/_models/work';

@Component({
  selector: 'app-work-card',
  templateUrl: './work-card.component.html',
  styleUrls: ['./work-card.component.css'],
})
export class WorkCardComponent implements OnInit {
  @Input() works: Work;

  constructor() {}

  ngOnInit(): void {
    console.log(this.works);
  }
}
