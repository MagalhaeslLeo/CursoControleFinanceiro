import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AtualizarCategoriaComponent } from './atualizar-categoria.component';

describe('AtualizarCategoriaComponent', () => {
  let component: AtualizarCategoriaComponent;
  let fixture: ComponentFixture<AtualizarCategoriaComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AtualizarCategoriaComponent]
    });
    fixture = TestBed.createComponent(AtualizarCategoriaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
