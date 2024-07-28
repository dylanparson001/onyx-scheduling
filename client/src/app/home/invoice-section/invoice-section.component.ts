import {Component, OnInit, ViewChild} from '@angular/core';
import {CommonModule, DatePipe} from '@angular/common';
import {InvoicesService} from "../../_services/invoices.service";
import {InvoiceCardComponent} from "../../invoice/invoice-card/invoice-card.component";
import {Invoice} from "../../models/invoice";
import {User} from "../../models/user";
import {UsersService} from "../../_services/users.service";
import {invoiceCustomer} from "../../models/invoice-customer";
import {HomeInvoiceCardsComponent} from "./home-invoice-cards/home-invoice-cards.component";
import {BaseChartDirective} from 'ng2-charts';
import {ChartOptions, ChartData} from "chart.js";

@Component({
  selector: 'app-invoice-section',
  standalone: true,
  imports: [CommonModule, InvoiceCardComponent, HomeInvoiceCardsComponent, BaseChartDirective],
  templateUrl: './invoice-section.component.html',
  styleUrl: './invoice-section.component.css'
})
export class InvoiceSectionComponent implements OnInit {
  todaysDate: any = '';
  todaysInvoices: Invoice[] | undefined;
  todaysCustomers: User[] = [];
  invoiceCustomers: invoiceCustomer[] = [] ;


  @ViewChild(BaseChartDirective) chart?: BaseChartDirective
  chartData: ChartData = {
    datasets: [
      {
        label: 'Total Price',
        data: [],
        fill: false,
        borderColor: '#FF5500',
        backgroundColor: '#FF5500'
      }
    ]
  };
  chartOptions: ChartOptions = {
    responsive: true,
  };


  ngOnInit(): void {
    let date = new Date();
    this.todaysDate = this.datePipe.transform(date, "MM-dd-yyyy");

    this.invoiceService.getInvoicesByDate(this.todaysDate, 'Paid', 0, 5).subscribe({
      next: (result) => {
        this.todaysInvoices = result;
        this.updateChartData()
        this.chart?.update()

        result.forEach(x => {
          this.userService.getCustomersFromInvoiceId(x.assigned_Customer_Id).subscribe({
            next: (response) => {
              this.todaysCustomers.push(response)
            }
          })
        })
      }
    });
  }

  constructor(private datePipe: DatePipe, private invoiceService: InvoicesService, private userService: UsersService) {
  }



  updateChartData() {
    if (this.todaysInvoices) {
      this.chartData.datasets[0].data = this.todaysInvoices.map(invoice => invoice.total_Price);
      this.chartData.labels = this.todaysInvoices.map(invoice => `${invoice.finishedDateTime}`);

    }
  }


}
