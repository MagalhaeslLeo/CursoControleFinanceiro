import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NovaDespesaComponent } from './nova-despesa.component';

describe('NovaDespesaComponent', () => {
  let component: NovaDespesaComponent;
  let fixture: ComponentFixture<NovaDespesaComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NovaDespesaComponent]
    });
    fixture = TestBed.createComponent(NovaDespesaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
