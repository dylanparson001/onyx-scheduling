import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TechnicianSectionComponent } from './technician-section.component';

describe('TechnicianSectionComponent', () => {
  let component: TechnicianSectionComponent;
  let fixture: ComponentFixture<TechnicianSectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TechnicianSectionComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TechnicianSectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
