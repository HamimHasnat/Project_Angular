import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppointmentServiceAngularComponentComponent } from './appointment-service-angular-component.component';

describe('AppointmentServiceAngularComponentComponent', () => {
  let component: AppointmentServiceAngularComponentComponent;
  let fixture: ComponentFixture<AppointmentServiceAngularComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AppointmentServiceAngularComponentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AppointmentServiceAngularComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
