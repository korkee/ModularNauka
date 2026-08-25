import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './pages/register/register.component';
import { CoursesComponent } from './pages/courses/courses.component';
import { QuizComponent } from './pages/quiz/quiz.component';

const routes: Routes = [
  { path: '', component: RegisterComponent },
  { path: 'courses', component: CoursesComponent },
  { path: 'quiz', component: QuizComponent },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
