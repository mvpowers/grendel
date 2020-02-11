import requestInstance from '@/requests';

const API_CONTROLLER = 'question';

export class QuestionRequests {
  static createQuestion(createQuestionRequest) {
    const url = `${API_CONTROLLER}/`;
    return requestInstance.post(url, createQuestionRequest);
  }

  static readActiveQuestion() {
    const url = `${API_CONTROLLER}/active`;
    return requestInstance.get(url);
  }
}
