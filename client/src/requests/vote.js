import requestInstance from '@/requests';

const API_CONTROLLER = 'vote';

export class VoteRequests {
  static readActiveVotes() {
    const url = `${API_CONTROLLER}/active`;
    return requestInstance.get(url);
  }

  static createVote(voteCreateRequest) {
    const url = `${API_CONTROLLER}/`;
    return requestInstance.post(url, voteCreateRequest);
  }
}
