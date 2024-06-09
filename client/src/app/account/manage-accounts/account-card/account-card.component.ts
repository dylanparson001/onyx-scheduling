import {Component, Injector, Input, OnInit} from '@angular/core';
import {User} from "../../../models/user";
import {Overlay, OverlayRef} from "@angular/cdk/overlay";
import {Jobs} from "../../../models/jobs";
import {JOB_DATA} from "../../../models/edit-job-token";
import {ComponentPortal} from "@angular/cdk/portal";
import {EditJobComponent} from "../../../jobs/edit-job/edit-job.component";
import {AccountComponent} from "../../account.component";
import {EditAccountsComponent} from "../edit-accounts/edit-accounts.component";
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-account-card',
  standalone: true,
  imports: [
    NgIf
  ],
  templateUrl: './account-card.component.html',
  styleUrl: './account-card.component.css'
})
export class AccountCardComponent implements OnInit {
  @Input() user: User | undefined
  private overlayRef: OverlayRef | undefined;

  constructor(
    private overlay: Overlay,
    private injector: Injector,
  ) {
  }
  ngOnInit(): void {
  }

  openUserOverlay(user: User) {
    const positionStrategy = this.overlay.position()
      .global()
      .centerVertically()
      .centerHorizontally()

    this.overlayRef = this.overlay.create({
      positionStrategy: positionStrategy
    });
    const data = { user:this.user };

    const injector = Injector.create({
      providers: [
        { provide: OverlayRef, useValue: this.overlayRef },
        { provide: JOB_DATA, useValue: data }
      ],
      parent: this.injector
    });

    const editOverlay = new ComponentPortal(
      EditAccountsComponent, null, injector);

    this.overlayRef.attach(editOverlay);

  }

  showEditJob() {
    if (this.user)
      this.openUserOverlay(this.user);
  }

}
