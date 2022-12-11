import { Component, Input, OnInit } from '@angular/core';

import { Chart, registerables } from 'node_modules/chart.js';
Chart.register(...registerables);
@Component({
  selector: 'app-pie-chart',
  templateUrl: './pie-chart.component.html',
  styleUrls: ['./pie-chart.component.css'],
})
export class PieChartComponent implements OnInit {
  @Input() todo: number;
  @Input() pending: number;
  @Input() complete: number;
  constructor() {}

  ngOnInit(): void {
    this.RenderChart();
    console.log(this.todo, this.pending, this.complete);
  }

  RenderChart() {
    new Chart('doughnutChart', {
      type: 'doughnut',
      data: {
        labels: ['Todo', 'Complete', 'Pending'],
        datasets: [
          {
            label: 'My First Dataset',
            data: [300, 50, 100],
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
