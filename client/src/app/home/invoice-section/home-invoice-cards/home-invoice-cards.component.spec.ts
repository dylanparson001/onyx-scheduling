import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeInvoiceCardsComponent } from './home-invoice-cards.component';

describe('HomeInvoiceCardsComponent', () => {
  let component: HomeInvoiceCardsComponent;
  let fixture: ComponentFixture<HomeInvoiceCardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HomeInvoiceCardsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HomeInvoiceCardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
