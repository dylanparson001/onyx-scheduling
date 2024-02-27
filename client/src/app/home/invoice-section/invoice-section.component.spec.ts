import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InvoiceSectionComponent } from './invoice-section.component';

describe('InvoiceSectionComponent', () => {
  let component: InvoiceSectionComponent;
  let fixture: ComponentFixture<InvoiceSectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InvoiceSectionComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(InvoiceSectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
