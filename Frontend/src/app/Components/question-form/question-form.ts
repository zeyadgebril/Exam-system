import { Exam } from './../../Model/exam';
import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { QuestionFormData } from '../../Model/question-form-data';
import { FormsModule } from '@angular/forms';
import { QuestionSection } from "./question-section/question-section";
import { CommonModule } from '@angular/common';
import { QuestionServices } from '../../Services/question-services';

@Component({
  selector: 'app-question-form',
  imports: [FormsModule,CommonModule, QuestionSection],
  templateUrl: './question-form.html',
  styleUrl: './question-form.css'
})
export class QuestionForm implements OnChanges {
  constructor(private ExamServices:QuestionServices) { }

 @Input() QuestionNumber: number = 0;
 examData: Exam={
  ExamName: '',
  ExamDuration: null,
  YearLevel: 0,
  Subject: 0,
  QuestionData: []
 };
 QuestionData: QuestionFormData [] = [];

ngOnChanges(changes: SimpleChanges): void {
    if(changes['QuestionNumber']) {
      console.log('QuestionNumber changed:', changes['QuestionNumber'].currentValue);
      this.adjustQuestions();
    }
}
  private adjustQuestions(): void {
    const newCount = this.QuestionNumber;

    if (newCount > this.QuestionData.length) {
      // Add new items
      const itemsToAdd = newCount - this.QuestionData.length;
      const startIndex = this.QuestionData.length;
      for (let i = 0; i < itemsToAdd; i++) {
        this.QuestionData.push({
          QuestionNumber: startIndex + i + 1,
          QuestionText: '',
          difficulty: 'easy',
          points: 1,
          options: [],
          correctAnswer: undefined
        });
      }
    } else if (newCount < this.QuestionData.length) {
      // Remove extra items
      this.QuestionData.splice(newCount);
    }
  }

  saveQuestion(event: QuestionFormData): void {
    console.log("*****************************************");

    // Try to find the index of an existing question with the same QuestionNumber
    const existingQuestionIndex = this.QuestionData.findIndex(
      (question) => question.QuestionNumber === event.QuestionNumber
    );

    if (existingQuestionIndex !== -1) {
      // If the question exists, update its properties
      console.log(`Updating existing question: ${event.QuestionNumber}`);
      const existingQuestion = this.QuestionData[existingQuestionIndex];
      existingQuestion.QuestionText = event.QuestionText;
      existingQuestion.difficulty = event.difficulty;
      existingQuestion.points = event.points;
      existingQuestion.options = event.options;
      existingQuestion.correctAnswer = event.correctAnswer;
    } else {
      // If the question does not exist, add it as a new entry
      console.log(`Adding new question: ${event.QuestionNumber}`);
      this.QuestionData.push(event);
    }

    console.log("Current QuestionData state:", this.QuestionData);
    // Here you can handle the actual save logic, e.g., send to a server or update global state
  }
  saveExam(): void {
    console.log('Save Question 11 called');
    this.examData.QuestionData = this.QuestionData;
    console.log('Exam Data to be saved:', this.examData);

    this.ExamServices.AddNewExam(this.examData).subscribe({
      next: (response) => {
        console.log('Exam saved successfully:', response);
        alert('Exam saved successfully!');
      },
      error: (error) => {
        console.error('Error saving exam:', error);
        alert('Failed to save exam. Please try again.');
      }
    });
  }
  resetForm(){
    if(confirm("Are you sure you want to reset the form?")) {
      this.examData = {
        ExamName: '',
        ExamDuration: null,
        YearLevel: 0,
        Subject: 0,
        QuestionData: []
      };
      this.QuestionData = [];
      this.QuestionNumber = 0;
      console.log("Form has been reset.");
    }
  }

}


