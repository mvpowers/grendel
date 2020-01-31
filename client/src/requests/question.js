import { requestInstance } from '@/requests';

const API_CONTROLLER = 'question';

export class QuestionRequests {
  static readQuestionById(questionId) {
    const url = `${API_CONTROLLER}/${questionId}`;
    return requestInstance.get(url);
  }

  static readActiveQuestion() {
    const url = `${API_CONTROLLER}/active`;
    return requestInstance.get(url);
  }
}
