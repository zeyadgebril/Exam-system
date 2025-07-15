import { QuestionFormData } from "./question-form-data";

export interface Exam {
  ExamName: string;
  ExamDuration: number |null; // in minutes
  YearLevel: number|null;
  Subject: number |null;
  QuestionData: QuestionFormData[];
}
