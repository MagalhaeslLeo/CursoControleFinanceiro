import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AtualizarFuncaoComponent } from './atualizar-funcao.component';

describe('AtualizarFuncaoComponent', () => {
  let component: AtualizarFuncaoComponent;
  let fixture: ComponentFixture<AtualizarFuncaoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AtualizarFuncaoComponent]
    });
    fixture = TestBed.createComponent(AtualizarFuncaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
