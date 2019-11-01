import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BcardHomeComponent } from './bcard-home.component';

describe('BcardHomeComponent', () => {
  let component: BcardHomeComponent;
  let fixture: ComponentFixture<BcardHomeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BcardHomeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BcardHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
