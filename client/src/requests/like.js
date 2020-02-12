import requestInstance from '@/requests';

const API_CONTROLLER = 'like';

export class LikeRequests {
  static createLike(createLikeRequest) {
    const url = `${API_CONTROLLER}/`;
    return requestInstance.post(url, createLikeRequest);
  }

  static deleteLike(deleteLikeRequest) {
    const url = `${API_CONTROLLER}/`;
    return requestInstance.delete(url, { data: deleteLikeRequest });
  }
}
