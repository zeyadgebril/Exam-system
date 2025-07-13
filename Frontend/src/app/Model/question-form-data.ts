export interface QuestionFormData {
  QuestionNumber: number
  QuestionText: string
  difficulty: 'easy' | 'medium' | 'hard'
  points : number
  options : string[]
  correctAnswer?: string
}
