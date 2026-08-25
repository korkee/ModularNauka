import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

const API = 'http://localhost:5100/api';

export interface User { id: string; name: string; email: string; }
export interface Course { id: string; title: string; description: string; }
export interface Lesson { id: string; title: string; order: number; isCompleted: boolean; }
export interface Quiz { id: string; title: string; lessonId: string; }
export interface SubmitResult { correct: number; total: number; scorePercent: number; }

@Injectable({ providedIn: 'root' })
export class ApiService {
  constructor(private http: HttpClient) {}

  register(name: string, email: string): Observable<User> {
    return this.http.post<User>(`${API}/users/register`, { name, email });
  }

  getCourses(): Observable<Course[]> {
    return this.http.get<Course[]>(`${API}/courses`);
  }

  getLessons(courseId: string): Observable<Lesson[]> {
    return this.http.get<Lesson[]>(`${API}/courses/${courseId}/lessons`);
  }

  getQuizzesByLesson(lessonId: string): Observable<Quiz[]> {
    return this.http.get<Quiz[]>(`${API}/quizzes/lesson/${lessonId}`);
  }

  getQuizQuestions(quizId: string): Observable<any[]> {
    return this.http.get<any[]>(`${API}/quizzes/${quizId}/questions`);
  }

  submitQuiz(quizId: string, userId: string, answers: Record<string, string>): Observable<SubmitResult> {
    return this.http.post<SubmitResult>(`${API}/quizzes/${quizId}/submit`, { userId, answers });
  }
}
