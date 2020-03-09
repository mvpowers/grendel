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

  static readQuestionById(questionId) {
    const url = `${API_CONTROLLER}/${questionId}`;
    return requestInstance.get(url);
  }

  static readQuestionHistory() {
    const url = `${API_CONTROLLER}/history`;
    return requestInstance.get(url);
  }

  static readQueuedQuestions() {
    const url = `${API_CONTROLLER}/queue`;
    return requestInstance.get(url);
  }

  static deleteQuestion(questionId) {
    const url = `${API_CONTROLLER}/${questionId}`;
    return requestInstance.delete(url);
  }
}
