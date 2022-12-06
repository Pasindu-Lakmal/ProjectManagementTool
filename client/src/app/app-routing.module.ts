import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { TaskCreateComponent } from './task/task-create/task-create.component';
import { TaskCreaterListComponent } from './task/task-creater-list/task-creater-list.component';
import { TaskEditComponent } from './task/task-edit/task-edit.component';
import { TaskListComponent } from './task/task-list/task-list.component';
import { TaskMyListComponent } from './task/task-my-list/task-my-list.component';
import { WorkAddComponent } from './work/work-add/work-add.component';
import { WorkEditComponent } from './work/work-edit/work-edit.component';
import { WorkListComponent } from './work/work-list/work-list.component';
import { AuthGuard } from './_guards/auth.guard';
import { PreventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'members', component: MemberListComponent },
      { path: 'members/:username', component: MemberDetailComponent },
      {
        path: 'member/edit',
        component: MemberEditComponent,
        canDeactivate: [PreventUnsavedChangesGuard],
      },
      { path: 'list', component: ListsComponent },
      { path: 'messages', component: MessagesComponent },

      { path: 'project', component: WorkListComponent },
      { path: 'project/edit/:workId', component: WorkEditComponent },
      { path: 'project/add', component: WorkAddComponent },

      { path: 'task/list/:workId', component: TaskListComponent },
      { path: 'task/edit/:taskId', component: TaskEditComponent },
      { path: 'task/create/:workId', component: TaskCreateComponent },
      { path: 'mytask', component: TaskMyListComponent },
      { path: 'Created', component: TaskCreaterListComponent },
    ],
  },

  { path: 'server-error', component: ServerErrorComponent },
  { path: 'errors', component: TestErrorsComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: '**', component: NotFoundComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
