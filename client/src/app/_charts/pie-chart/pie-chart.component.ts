import {
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
} from '@angular/core';

import { Chart, registerables } from 'node_modules/chart.js';
Chart.register(...registerables);
@Component({
  selector: 'app-pie-chart',
  templateUrl: './pie-chart.component.html',
  styleUrls: ['./pie-chart.component.css'],
})
export class PieChartComponent implements OnInit, OnChanges {
  @Input() todo: number;
  @Input() pending: number;
  @Input() complete: number;
  data: number[];
  constructor() {}

  ngOnInit(): void {
    this.RenderChart();
    console.log(this.todo, this.pending, this.complete);
  }

  ngOnChanges(changes: SimpleChanges): void {
    console.log(changes);
    this.data = [
      changes.todo.currentValue,
      changes.complete.currentValue,
      changes.pending.currentValue,
    ];
    // console.log(...this.data);
  }

  RenderChart() {
    new Chart('doughnutChart', {
      type: 'doughnut',
      data: {
        labels: ['Todo', 'Complete', 'Pending'],
        datasets: [
          {
            label: 'My Work Distribution',
            data: [...this.data],
            backgroundColor: [
              'rgb(255, 99, 132)',
              'rgb(54, 162, 235)',
              'rgb(255, 205, 86)',
            ],
            hoverOffset: 10,
          },
        ],
      },
      options: {},
    });
  }
}
