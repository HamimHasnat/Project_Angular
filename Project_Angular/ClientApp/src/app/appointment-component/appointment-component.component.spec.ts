import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppointmentComponentComponent } from './appointment-component.component';

describe('AppointmentComponentComponent', () => {
  let component: AppointmentComponentComponent;
  let fixture: ComponentFixture<AppointmentComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AppointmentComponentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AppointmentComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
