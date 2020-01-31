import { requestInstance } from '@/requests';

const API_CONTROLLER = 'voteoption';

export class VoteOptionRequests {
  static readActiveVoteOptions() {
    const url = `${API_CONTROLLER}/active`;
    return requestInstance.get(url);
  }
}
