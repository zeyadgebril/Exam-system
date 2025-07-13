import { Component } from '@angular/core';
import { AddButton } from "./Components/add-button/add-button";
import { AddingQuestionsPage } from "./Pages/adding-questions-page/adding-questions-page";

@Component({
  selector: 'app-root',
  imports: [ AddingQuestionsPage],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'Frontend';
}
