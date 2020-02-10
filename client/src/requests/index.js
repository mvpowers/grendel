import axios from 'axios';
import Swal from 'sweetalert2';
import { localStorageKeys, routes } from '../constants';
import router from '../router';

export { QuestionRequests } from './question';
export { VoteOptionRequests } from './voteOption';
export { VoteRequests } from './vote';
export { UserRequests } from './user';

export const errorAlerts = (error) => {
  const message = error.response.data.message || 'Unable to process request';

  switch (error.response.status) {
    case 400:
      Swal.fire('Bad Request', message, 'error');
      break;

    case 401:
      Swal.fire('Unauthorized', 'Please login to continue', 'error');
      localStorage.clear();
      router.push({ name: routes.HOME });
      break;

    case 422:
      Swal.fire('Unprocessable Entity ', message, 'error');
      break;

    default:
      Swal.fire('Server Error ', message, 'error');
  }
};

const requestInstance = axios.create({
  baseURL: `${process.env.VUE_APP_API_URL}/api/${process.env.VUE_APP_API_VERSION}`,
});

requestInstance.interceptors.request.use(
  (config) => {
    const authToken = localStorage.getItem(localStorageKeys.AUTH_TOKEN);
    if (!authToken) {
      throw new axios.Cancel('Token not found');
    }

    // eslint-disable-next-line no-param-reassign
    config.headers.Authorization = `Bearer ${authToken}`;
    return config;
  },
  error => Promise.reject(error),
);

requestInstance.interceptors.response.use(
  response => response,
  (error) => {
    errorAlerts(error);

    const err = error.response || 'Network Response Error';
    return Promise.reject(err);
  },
);

export default requestInstance;
