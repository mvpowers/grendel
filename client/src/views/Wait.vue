<template>
  <VContainer class="justify-center align-center">
    <div>{{ timerHours }}:{{ timerMinutes }}:{{ timerSeconds }}</div>
    <div class="headline">
      until judgement time
    </div>
  </VContainer>
</template>

<script>
export default {
  name: 'Wait',
  data: () => ({
    expiration: null,
    countdown: null,
  }),
  computed: {
    timerHours() {
      return Math.floor(this.countdown % (1000 * 60 * 60 * 24) / (1000 * 60 * 60));
    },
    timerMinutes() {
      return Math.floor(this.countdown % (1000 * 60 * 60) / (1000 * 60));
    },
    timerSeconds() {
      return Math.floor((this.countdown % (1000 * 60)) / 1000);
    },
  },
  mounted() {
    const expiration = new Date();
    expiration.setHours(expiration.getHours() + 2);

    this.expiration = expiration;

    setInterval(() => {
      const now = Date.now();
      this.countdown = this.expiration - now;
    }, 100);
  },
};
</script>
