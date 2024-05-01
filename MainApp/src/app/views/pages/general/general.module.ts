import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GeneralComponent } from './All-Posts/general.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { NgSelectModule } from '@ng-select/ng-select';
import { MyPostsComponent } from './My-Posts/myposts.component';

const routes: Routes = [
  {
    path: "",
    component: GeneralComponent,
    children: [],
  },
  {
    path: 'myposts',
    component: MyPostsComponent
  },
  {
    path: 'allposts',
    component: GeneralComponent
  }
];

@NgModule({
  declarations: [
    GeneralComponent,
    MyPostsComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    PerfectScrollbarModule,
    NgSelectModule,
  ],
  exports: [RouterModule],
})
export class GeneralModule { }
