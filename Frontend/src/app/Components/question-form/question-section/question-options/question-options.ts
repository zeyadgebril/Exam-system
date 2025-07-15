import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { OptionData } from '../../../../Model/option-data';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-question-options',
  imports: [FormsModule],
  templateUrl: './question-options.html',
  styleUrl: './question-options.css'
})
export class QuestionOptions {
@Output() RemoveOptionEvent = new EventEmitter<number|null>();
 @Output() OptionEvent = new EventEmitter<OptionData>();

  @Input() optionFormData: OptionData = {
    id: 99,
    text: '',
    isCorrect: false
  }

  onCorrectAnswerChange(): void {
    this.optionFormData.isCorrect = !this.optionFormData.isCorrect;
    console.log("Option Inner chled component is correct:", this.optionFormData.isCorrect);
    this.OptionEvent.emit(this.optionFormData);
  }

  onTextChange(): void {
    console.log("Option Inner chled component:", this.optionFormData);
    this.OptionEvent.emit(this.optionFormData);
  }

  RemoveOption(): void {
    console.log("Remove option called for option with id:", this.optionFormData.id);
    this.RemoveOptionEvent.emit(this.optionFormData.id);
  }
}
