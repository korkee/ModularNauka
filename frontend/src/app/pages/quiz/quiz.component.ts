import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService, Quiz, SubmitResult } from '../../services/api.service';

interface Question { id: string; text: string; options: string[]; }

@Component({
  selector: 'app-quiz',
  standalone: false,
  templateUrl: './quiz.component.html'
})
export class QuizComponent implements OnInit {
  quiz: Quiz | null = null;
  questions: Question[] = [];
  answers: Record<string, string> = {};
  result: SubmitResult | null = null;
  lessonId = '';
  courseId = '';
  loading = true;
  error = '';

  constructor(private api: ApiService, private route: ActivatedRoute, private router: Router) {}

  ngOnInit() {
    this.route.queryParams.subscribe(p => {
      this.lessonId = p['lessonId'];
      this.courseId = p['courseId'];
      this.loadQuiz();
    });
  }

  loadQuiz() {
    this.api.getQuizzesByLesson(this.lessonId).subscribe({
      next: quizzes => {
        if (!quizzes.length) { this.error = 'No quiz for this lesson.'; this.loading = false; return; }
        this.quiz = quizzes[0];
        this.api.getQuizQuestions(this.quiz!.id).subscribe({
          next: q => { this.questions = q; this.loading = false; },
          error: () => { this.error = 'Could not load questions.'; this.loading = false; }
        });
      },
      error: () => { this.error = 'Could not load quiz.'; this.loading = false; }
    });
  }

  select(questionId: string, option: string) {
    if (!this.result) this.answers[questionId] = option;
  }

  submit() {
    const userId = localStorage.getItem('userId')!;
    this.api.submitQuiz(this.quiz!.id, userId, this.answers).subscribe({
      next: r => this.result = r,
      error: () => this.error = 'Submission failed.'
    });
  }

  answeredCount() { return Object.keys(this.answers).length; }

  back() { this.router.navigate(['/courses']); }
}
