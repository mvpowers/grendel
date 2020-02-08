import axios from 'axios';

const API_CONTROLLER = 'user';

const nonAuthRequestInstance = axios.create({
  baseURL: `${process.env.VUE_APP_API_URL}/api/${process.env.VUE_APP_API_VERSION}`,
});

export class UserRequests {
  static authenticateUser(userAuthRequest) {
    const url = `${API_CONTROLLER}/auth`;
    return nonAuthRequestInstance.post(url, userAuthRequest);
  }

  static createUserResetToken(userTokenRequest) {
    const url = `${API_CONTROLLER}/reset-token`;
    return nonAuthRequestInstance.post(url, userTokenRequest);
  }

  static getUserFromResetToken(userResetToken){
    const url = `${API_CONTROLLER}/reset-token/${userResetToken}`;
    return nonAuthRequestInstance.get(url);
  }

  static resetUserPassword(resetRequest) {
    const url = `${API_CONTROLLER}/password-reset`;
    return nonAuthRequestInstance.post(url, resetRequest);
  }
}
