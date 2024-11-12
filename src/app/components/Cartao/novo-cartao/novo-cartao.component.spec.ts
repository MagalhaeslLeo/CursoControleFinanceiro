import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NovoCartaoComponent } from './novo-cartao.component';

describe('NovoCartaoComponent', () => {
  let component: NovoCartaoComponent;
  let fixture: ComponentFixture<NovoCartaoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NovoCartaoComponent]
    });
    fixture = TestBed.createComponent(NovoCartaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
