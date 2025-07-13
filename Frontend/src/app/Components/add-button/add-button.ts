import { Component, EventEmitter, Output, Signal, signal,WritableSignal } from '@angular/core';

@Component({
  selector: 'app-add-button',
  imports: [],
  templateUrl: './add-button.html',
  styleUrl: './add-button.css'
})
export class AddButton {
 questionCounter:WritableSignal<number> ;
@Output() AddQuestion:EventEmitter<number> = new EventEmitter<number>();

constructor() {
  this.questionCounter = signal(0);
}

AddQuestionCounter(){
this.questionCounter.update(n => n + 1) ;
console.log("Question Counter: " + this.questionCounter());
this.AddQuestion.emit(this.questionCounter());
}

}
