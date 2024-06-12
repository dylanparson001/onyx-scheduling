import {Component, Input, OnInit} from '@angular/core';
import {User} from "../../models/user";
import {LoginServiceService} from "../../_services/login-service.service";
import {DatePipe, NgIf} from "@angular/common";
import {Jobs} from "../../models/jobs";
import {JobCardComponent} from "../job-card/job-card.component";
import {DateServiceService} from "../../_services/date-service.service";

@Component({
  selector: 'app-tech-schedule',
  standalone: true,
  imports: [
    NgIf,
    JobCardComponent
  ],
  templateUrl: './tech-schedule.component.html',
  styleUrl: './tech-schedule.component.css'
})
export class TechScheduleComponent implements OnInit {
  currentUser: User | undefined
  todaysJobs: Jobs[] = []
  today: Date = new Date();
  currentDate: string | null = ''
  constructor(private loginService: LoginServiceService,
              private dateService: DateServiceService,
              private datePipe: DatePipe
              ) {}

  ngOnInit() {
    this.loadUser()



    this.currentDate = this.datePipe.transform(this.today, 'MM-dd-yyy');
  }

  loadUser() {
    let userId = localStorage.getItem('userId');
    if (userId) {

      this.loginService.getUserFromId(userId).subscribe({
        next: (response) => {
          this.currentUser = response
        },
        error: err => {
        }
      })
    }
  }
}
