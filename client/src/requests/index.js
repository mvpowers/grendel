import axios from 'axios';
import { localStorageKeys, routes } from '../constants';
import router from '@/router';

export { QuestionRequests } from './question';
export { VoteOptionRequests } from './voteOption';
export { VoteRequests } from './vote';
export { UserRequests } from './user';

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
    if (error.response.status === 401) {
      localStorage.clear();
      router.push({ name: routes.HOME });
    }
    Promise.reject(error);
  },
);

export default requestInstance;
