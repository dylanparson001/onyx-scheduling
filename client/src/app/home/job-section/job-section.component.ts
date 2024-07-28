import {Component, OnInit, ViewChild} from '@angular/core';
import {BaseChartDirective} from "ng2-charts";
import {ChartData, ChartOptions} from "chart.js";
import {UsersService} from "../../_services/users.service";
import {User} from "../../models/user";
import {Invoice} from "../../models/invoice";
import {InvoicesService} from "../../_services/invoices.service";
import {DatePipe} from "@angular/common";
import {TechTotal} from "../../models/technicianTotal";
import {compileResults} from "@angular/compiler-cli/src/ngtsc/annotations/common";
import _default from "chart.js/dist/core/core.interaction";
import dataset = _default.modes.dataset;

@Component({
  selector: 'app-job-section',
  standalone: true,
  imports: [
    BaseChartDirective
  ],
  templateUrl: './job-section.component.html',
  styleUrl: './job-section.component.css'
})
export class JobSectionComponent implements OnInit {
  technicians: User[] = []
  todaysDate: any = ''

  techTotals: TechTotal[] = []
  invoices: Invoice[] = []
  @ViewChild(BaseChartDirective) chart?: BaseChartDirective
  chartData: ChartData = {
    datasets: [
      {
        label: `Today's Total`,
        data: [],
        fill: false,
        backgroundColor: '#025D6EE5'
      }
    ]
  };
  chartOptions: ChartOptions = {
    responsive: true,
  };


  constructor(
    private userService: UsersService,
    private invoiceService: InvoicesService,
    private datePipe: DatePipe
  ) {

  }


  ngOnInit(): void {
    let date = new Date();
    this.todaysDate = this.datePipe.transform(date, "MM-dd-yyyy");

    this.userService.getAllTechnicians().subscribe({
      next: (response) => {
        this.technicians = response
        this.updateChartData()
        this.chart?.update()

      }
    })
  }

  updateChartData() {
    if (this.technicians) {
      this.technicians.forEach(x => {
        console.log(x.dailyTotal)
      })
      console.log(this.technicians.map(x => x.dailyTotal))
      this.chartData.datasets[0].data = this.technicians.map(x => x.dailyTotal)
      this.chartData.labels = this.technicians.map(x => `${x.firstName} ${x.lastName}`)
    }
  }
}
