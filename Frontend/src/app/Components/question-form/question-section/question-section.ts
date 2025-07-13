import { OptionData } from './../../../Model/option-data';
import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { QuestionFormData } from '../../../Model/question-form-data';
import { QuestionOptions } from "./question-options/question-options";
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-question-section',
  imports: [FormsModule, QuestionOptions,CommonModule],
  templateUrl: './question-section.html',
  styleUrl: './question-section.css'
})
export class QuestionSection {
@Input() QuestionData!:QuestionFormData;
@Output() SaveQuestionEvent = new EventEmitter<QuestionFormData>();
OptionNumber: number = 0;
OptionData:OptionData[] = [];
correctAnswerId: number | null = null;
  AddOption():void {
    this.OptionNumber++;
      this.adjustOptions();
  }

  adjustOptions(): void {
    console.log('Adjusting options for OptionNumber:', this.OptionNumber);
    const newCount = this.OptionNumber;
    if (newCount > this.OptionData.length) {
      // Add new items
      const itemsToAdd = newCount - this.OptionData.length;
      const startIndex = this.OptionData.length;
      for (let i = 0; i < itemsToAdd; i++) {
        this.OptionData.push({
          id: startIndex + i ,
          text: '',
          isCorrect: false
        });
        console.log('Added new option:', this.OptionData[startIndex + i]);
      }
    } else if (newCount < this.OptionData.length) {
      // Remove extra items
      this.OptionData.splice(newCount);
    }
  }

// onOptionEvent(optionData: OptionData, index: number): void {
//   this.OptionData[index] = optionData;
//   if (optionData.isCorrect) {
//     this.correctAnswerId = optionData.id;
//   }
//   console.log('Option event received:', optionData, 'at index:', index);
// }

onOptionEvent(emittedOptionData: OptionData, index: number): void {
    this.OptionData[index] = emittedOptionData;
    if (emittedOptionData.isCorrect) {
      this.correctAnswerId = emittedOptionData.id;
      this.OptionData.forEach(option => {
        option.isCorrect = (option.id === emittedOptionData.id);
      });
    }
    this.QuestionData.correctAnswer = this.OptionData.find(option => option.isCorrect)?.text || '';
    console.log("right answer selected:", this.QuestionData.correctAnswer);
    if(!this.QuestionData.options.includes(emittedOptionData.text || '')) {
      this.QuestionData.options.push(emittedOptionData.text || '');
    }

    this.SaveQuestionEvent.emit(this.QuestionData);
    console.log("Question Data emitted:", this.QuestionData);
}
//   onOptionSelectedAsCorrect(optionId: number): void {
//   console.log("Correct answer selected with ID:", optionId);
//   this.correctAnswerId = optionId;
//   this.OptionData.forEach(option => {
//     option.isCorrect = (option.id === optionId);
//   });
//   this.QuestionData.correctAnswer = this.OptionData.find(option => option.id === this.correctAnswerId)?.text || '';
//   console.log("Correct answer selected:", this.QuestionData.correctAnswer);
// }

 onRemoveOption(optionIdToRemove: number|null): void {
    // Find the index of the option with the given ID
    const indexToRemove = this.OptionData.findIndex(option => option.id === optionIdToRemove);

    if (indexToRemove !== -1) {
      // Check if the option being removed was the currently correct answer
      if (this.correctAnswerId === optionIdToRemove) {
        this.correctAnswerId = null; // Reset correct answer if it's removed
      }

      // Remove the option from the array
      this.OptionData.splice(indexToRemove, 1);

      // Decrement the option counter
      this.OptionNumber--;

      // IMPORTANT: Re-assign IDs to maintain sequentiality if needed for display or logic.
      // If your IDs are purely for unique identification and not sequential order,
      // you might skip this re-assignment. However, for "Option 1, Option 2" display,
      // re-indexing is often desired.
      this.OptionData.forEach((option, idx) => {
        option.id = idx; // Re-assign ID based on its new position in the array
      });

      console.log(`Removed option with ID: ${optionIdToRemove}. New OptionData:`, this.OptionData);
    }
  }
}
