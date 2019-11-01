import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AppintmnentsComponent } from './appintmnents.component';

describe('AppintmnentsComponent', () => {
  let component: AppintmnentsComponent;
  let fixture: ComponentFixture<AppintmnentsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AppintmnentsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AppintmnentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
