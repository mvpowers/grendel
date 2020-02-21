import axios from 'axios';
import requestInstance, { errorAlerts } from './index';

const API_CONTROLLER = 'user';

const nonAuthRequestInstance = axios.create({
  baseURL: '/api/v1',
});

nonAuthRequestInstance.interceptors.response.use(
  response => response,
  (error) => {
    errorAlerts(error);

    const err = error.response || 'Network Response Error';
    return Promise.reject(err);
  },
);

export class UserRequests {
  static authenticateUser(userAuthRequest) {
    const url = `${API_CONTROLLER}/auth`;
    return nonAuthRequestInstance.post(url, userAuthRequest);
  }

  static createUserResetToken(userTokenRequest) {
    const url = `${API_CONTROLLER}/reset-token`;
    return nonAuthRequestInstance.post(url, userTokenRequest);
  }

  static getUserFromResetToken(userResetToken) {
    const url = `${API_CONTROLLER}/reset-token/${userResetToken}`;
    return nonAuthRequestInstance.get(url);
  }

  static resetUserPassword(resetRequest) {
    const url = `${API_CONTROLLER}/password-reset`;
    return nonAuthRequestInstance.post(url, resetRequest);
  }

  static createUser(userCreateRequest) {
    const url = `${API_CONTROLLER}/`;
    return requestInstance.post(url, userCreateRequest);
  }
}
