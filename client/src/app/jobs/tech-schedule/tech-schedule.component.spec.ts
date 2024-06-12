import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TechScheduleComponent } from './tech-schedule.component';

describe('TechScheduleComponent', () => {
  let component: TechScheduleComponent;
  let fixture: ComponentFixture<TechScheduleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TechScheduleComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TechScheduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
