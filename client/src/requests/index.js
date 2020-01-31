import axios from 'axios';

export { QuestionRequests } from './question';
export { VoteOptionRequests } from './voteOption';
export { VoteRequests } from './vote';

export const requestInstance = axios.create({
  baseURL: `${process.env.VUE_APP_API_URL}/api/${process.env.VUE_APP_API_VERSION}`,
  headers: { Authorization: 'Bearer token' },
});
