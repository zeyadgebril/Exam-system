import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Exam } from '../Model/exam';

@Injectable({
  providedIn: 'root'
})
export class QuestionServices {

  baseURL: string = 'https://localhost:7089/api/ExamForm';
  constructor(private http:HttpClient) { }

  AddNewExam(exam: Exam) {
    return this.http.post<Exam>(`${this.baseURL}`, exam);
  }
}
