<template>
  <VContainer class="justify-center align-center">
    <LoadSpinner v-if="isPageLoading" />
    <div
      v-else
      class="title text-xs-center my-3">
      {{ question.inquiry }}
    </div>
    <div class="headline text-xs-center">
      {{ `${timerHours}:${timerMinutes}:${timerSeconds} until judgement time` }}
    </div>
  </VContainer>
</template>

<script>
import { QuestionRequests } from '../requests';
import LoadSpinner from '../components/LoadSpinner';

export default {
  name: 'Wait',
  components: { LoadSpinner },
  data: () => ({
    isPageLoading: false,
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
    this.isPageLoading = true;
    await this.readActiveQuestion();

    setInterval(() => {
      const now = Date.now();
      this.countdown = this.question.timeVotingExpires - now;
    }, 100);

    this.$nextTick(() => {
      this.isPageLoading = false;
    });
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
