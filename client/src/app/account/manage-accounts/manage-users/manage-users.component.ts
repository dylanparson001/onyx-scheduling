import {Component, Injector, OnInit} from '@angular/core';
import {MatButton} from "@angular/material/button";
import {UsersService} from "../../../_services/users.service";
import {Invoice} from "../../../models/invoice";
import {User} from "../../../models/user";
import {FormsModule} from "@angular/forms";
import {NgForOf, NgIf} from "@angular/common";
import {AccountCardComponent} from "../account-card/account-card.component";
import {Jobs} from "../../../models/jobs";
import {Overlay, OverlayRef} from "@angular/cdk/overlay";
import {JOB_DATA} from "../../../models/edit-job-token";
import {ComponentPortal} from "@angular/cdk/portal";
import {EditJobComponent} from "../../../jobs/edit-job/edit-job.component";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-manage-users',
  standalone: true,
  imports: [
    MatButton,
    FormsModule,
    NgIf,
    NgForOf,
    AccountCardComponent,
    RouterLink
  ],
  templateUrl: './manage-users.component.html',
  styleUrl: './manage-users.component.css'
})
export class ManageUsersComponent implements OnInit {
  userList: User[] = []
  take: number = 5;
  position: number = 0;
  countOfUsers: number | undefined;
  totalPages: number = 0;
  currentPage: number = 1;
  usernameToSearch: string = ''

  constructor(
    private userService: UsersService,
    private overlay: Overlay,
    private injector: Injector,
  ) {
  }


  ngOnInit(): void {
    this.loadUsers()
    this.getCountOfUsers()
  }

  searchUsers() {
    if (this.usernameToSearch.length === 0) {
      this.loadUsers()
      this.resultCountChange()
      return
    }
    this.userService.searchUsersByUsername(this.usernameToSearch).subscribe({
      next: (response) => {
        this.userList = response;
        this.countOfUsers = response.length
        this.totalPages = Math.ceil(this.countOfUsers / this.take)

      },
      error: (err) => {

      }
    })
  }

  loadUsers() {
    this.userService.getUsers(this.position, this.take).subscribe({
      next: (response) => {
        this.userList = response
      }
    })
  }

  getCountOfUsers() {
    this.userService.getCountUsers().subscribe({
      next: (response) => {
        this.countOfUsers = response
        this.totalPages = Math.ceil(response / this.take)
      }
    })
  }

  previousButtonClick() {
    if (this.countOfUsers) {
      if ((this.position - this.take) < 0) {
        return;
      }
      this.position -= this.take;
      this.currentPage -= 1;

      this.loadUsers()
    }
  }

  nextButtonClick() {
    if (this.countOfUsers) {
      if ((this.currentPage + 1) > this.totalPages) {
        return;
      }
      this.position += this.take;
      this.currentPage += 1;
      this.loadUsers();
    }
  }

  setOffsetToZero() {
    this.currentPage = 1
    this.position = 0;
    this.loadUsers();

  }

  resultCountChange() {
    this.setOffsetToZero();
    this.loadUsers()
    this.getCountOfUsers()
  }
}
