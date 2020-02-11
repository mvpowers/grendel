import requestInstance from '@/requests';

const API_CONTROLLER = 'session';

export class SessionRequests {
  static getUserSession() {
    const url = `${API_CONTROLLER}/user-info`;
    return requestInstance.get(url);
  }

  static startSession() {
    const url = `${API_CONTROLLER}/start`;
    return requestInstance.get(url);
  }

  static endSession() {
    const url = `${API_CONTROLLER}/start`;
    return requestInstance.get(url);
  }
}
