import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyPostsComponent } from './myposts.component';

describe('GeneralComponent', () => {
  let component: MyPostsComponent;
  let fixture: ComponentFixture<MyPostsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MyPostsComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(MyPostsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
