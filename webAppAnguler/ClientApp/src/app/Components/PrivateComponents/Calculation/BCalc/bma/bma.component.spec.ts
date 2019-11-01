import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BmaComponent } from './bma.component';

describe('BmaComponent', () => {
  let component: BmaComponent;
  let fixture: ComponentFixture<BmaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BmaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BmaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
