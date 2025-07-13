import { Component  } from '@angular/core';
import { AddButton } from "../../Components/add-button/add-button";
import { QuestionForm } from "../../Components/question-form/question-form";

@Component({
  selector: 'app-adding-questions-page',
  imports: [AddButton, QuestionForm],
  templateUrl: './adding-questions-page.html',
  styleUrl: './adding-questions-page.css'
})
export class AddingQuestionsPage {

  QuestionNumber: number = 0;
AddQuestion(data :number) {
  this.QuestionNumber = data;
  console.log("Question Number: " + this.QuestionNumber);
  }
}
