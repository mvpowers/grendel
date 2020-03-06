import requestInstance from '@/requests';

const API_CONTROLLER = 'voteoption';

export class VoteOptionRequests {
  static readVoteOptions() {
    const url = `${API_CONTROLLER}/`;
    return requestInstance.get(url);
  }

  static readActiveVoteOptions() {
    const url = `${API_CONTROLLER}/active`;
    return requestInstance.get(url);
  }
}
