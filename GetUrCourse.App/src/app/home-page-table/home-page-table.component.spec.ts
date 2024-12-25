import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomePageTableComponent } from './home-page-table.component';

describe('HomePageTableComponent', () => {
  let component: HomePageTableComponent;
  let fixture: ComponentFixture<HomePageTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HomePageTableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HomePageTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
