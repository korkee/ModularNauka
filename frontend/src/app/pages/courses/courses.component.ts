import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService, Course, Lesson } from '../../services/api.service';

@Component({
  selector: 'app-courses',
  standalone: false,
  templateUrl: './courses.component.html'
})
export class CoursesComponent implements OnInit {
  courses: Course[] = [];
  selectedCourse: Course | null = null;
  lessons: Lesson[] = [];
  userName = localStorage.getItem('userName') || 'Student';

  constructor(private api: ApiService, private router: Router) {
    if (!localStorage.getItem('userId')) this.router.navigate(['/']);
  }

  ngOnInit() {
    this.api.getCourses().subscribe(c => this.courses = c);
  }

  selectCourse(course: Course) {
    this.selectedCourse = course;
    this.api.getLessons(course.id).subscribe(l => this.lessons = l);
  }

  openLesson(lesson: Lesson) {
    this.router.navigate(['/quiz'], { queryParams: { lessonId: lesson.id, courseId: this.selectedCourse!.id } });
  }

  logout() {
    localStorage.clear();
    this.router.navigate(['/']);
  }
}
