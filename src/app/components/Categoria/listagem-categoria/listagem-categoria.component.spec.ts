import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListagemCategoriaComponent } from './listagem-categoria.component';

describe('ListagemCategoriaComponent', () => {
  let component: ListagemCategoriaComponent;
  let fixture: ComponentFixture<ListagemCategoriaComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ListagemCategoriaComponent]
    });
    fixture = TestBed.createComponent(ListagemCategoriaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
