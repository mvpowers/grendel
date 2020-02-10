<template>
  <VContainer class="justify-center align-center">
    <div class="title text-xs-center my-3">
      {{ question.inquiry }}
    </div>
    <div class="headline text-xs-center">
      {{ `${timerHours}:${timerMinutes}:${timerSeconds} until judgement time` }}
    </div>
  </VContainer>
</template>

<script>
import { QuestionRequests } from '../requests';

export default {
  name: 'Wait',
  data: () => ({
    expiration: null,
    countdown: null,
    question: {
      id: 0,
      inquiry: '',
      timeVotingExpires: null,
    },
  }),
  computed: {
    timerHours() {
      const val = Math.floor(this.countdown % (1000 * 60 * 60 * 24) / (1000 * 60 * 60));
      return this.formatTwoDigitNumber(val);
    },
    timerMinutes() {
      const val = Math.floor(this.countdown % (1000 * 60 * 60) / (1000 * 60));
      return this.formatTwoDigitNumber(val);
    },
    timerSeconds() {
      const val = Math.floor((this.countdown % (1000 * 60)) / 1000);
      return this.formatTwoDigitNumber(val);
    },
  },
  async beforeMount() {
    await this.routeVoteFlow();
  },
  async mounted() {
    await this.readActiveQuestion();

    setInterval(() => {
      const now = Date.now();
      this.countdown = this.question.timeVotingExpires - now;
    }, 100);
  },
  methods: {
    formatTwoDigitNumber(val) {
      const minZero = Math.max(0, val);
      return minZero < 10 ? `0${minZero}` : minZero;
    },
    async readActiveQuestion() {
      try {
        const { data } = await QuestionRequests.readActiveQuestion();
        this.question = { ...data, timeVotingExpires: Date.parse(data.timeVotingExpires) };
      } catch (e) {
        console.error(e);
      }
    },
  },
};
</script>
