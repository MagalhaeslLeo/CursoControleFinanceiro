import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NovaFuncaoComponent } from './nova-funcao.component';

describe('NovaFuncaoComponent', () => {
  let component: NovaFuncaoComponent;
  let fixture: ComponentFixture<NovaFuncaoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NovaFuncaoComponent]
    });
    fixture = TestBed.createComponent(NovaFuncaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
