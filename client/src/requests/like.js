import requestInstance from '@/requests';

const API_CONTROLLER = 'like';

export class LikeRequests {
  static createLike(createLikeRequest) {
    const url = `${API_CONTROLLER}/`;
    return requestInstance.post(url, createLikeRequest);
  }

  // static readActiveQuestion() {
  //   const url = `${API_CONTROLLER}/active`;
  //   return requestInstance.get(url);
  // }
}
